# Scaffold Claude Code project configuration + enforced session logging

_Verbatim copy of the approved plan for [2026-07-05_scaffold-claude-config.md](2026-07-05_scaffold-claude-config.md), captured per the session logging policy in [CLAUDE.md](../../CLAUDE.md)._

## Context

`mcp-ai-kb-services` is a brand-new repo (one commit, `docs/` and `src/` both empty, no manifests, no `.gitignore`). There is no existing `CLAUDE.md` or `.claude/` directory anywhere in the tree. The goal is two-fold:

1. Lay down the standard Claude Code project scaffold — instructions, a custom-agent folder, a custom-skill folder, and session hooks — so that as real source code lands in `src/`, there's already a place to record conventions, add project-specific subagents/skills, and get automatic session context.
2. Establish a durable audit trail: **every session that creates a plan, does debugging, answers an `AskUserQuestion`, or has an agent action that alters code must produce a record under `docs/sessions/`** capturing the prompt(s), the plan (if any), the files touched, and a summary/outcome. This isn't just documented as a policy — it's mechanically enforced by a `Stop` hook that refuses to let the session end if code changed (or a plan was made) with no matching session-log entry.

Since this is a single project (not a monorepo), everything lives at the repo root. There's no tech stack yet, so `CLAUDE.md` and the hook scripts are written to degrade gracefully (no-op) rather than assuming a stack that isn't there. Schemas below (frontmatter fields, hook event list/JSON shape, settings precedence) were verified against current Claude Code docs, not assumed.

**Granularity decision:** one `docs/sessions/` entry per Claude Code conversation session (not per sub-action within it) — simplest boundary to detect from a `Stop` hook.

## Files to create

### 1. `CLAUDE.md` (repo root)
Team-shared project instructions, loaded every session. Sections:
- **Project Overview** — placeholder paragraph (to be filled in as the project takes shape)
- **Architecture** — placeholder noting `src/` is currently empty; update once the stack is chosen
- **Build / Test / Run** — placeholder commands, marked TODO until a manifest exists
- **Conventions** — placeholder for coding standards once established
- **Working with Claude** — pointer to `.claude/agents/`, `.claude/skills/`
- **Session logging policy (required)** — explicit statement of the rule:
  > Any debugging session, plan creation, `AskUserQuestion` exchange, or agent action that alters code must result in an entry under `docs/sessions/`, created from `docs/sessions/_template.md`, before the session ends. This is enforced automatically by `.claude/hooks/enforce-session-log.sh` (registered on `Stop`) — it will block session end and re-prompt if a code-altering or plan-creating session has no matching log file. Fill in the prompt(s), plan (or a link to the plan file), files touched, and a short summary/outcome.

### 2. `docs/sessions/_template.md`
New template for the audit-trail entries. Fields:
```markdown
# Session: <date> <short-session-id>

## Trigger
<debugging | plan | ask | agent-action-altering-code>

## Prompt(s)
<verbatim user prompt(s) that started/drove this session>

## Plan
<full plan content, or a relative link to the plan file if one was written, or "N/A">

## Files touched
- <path> — <created|modified|deleted>

## Summary & outcome
<short prose: what was done, and how it was verified>
```

### 3. `.claude/agents/_template.md`
Template for a new project-specific subagent:
```markdown
---
name: template-agent
description: Describe what this agent does and when it should be used (drives auto-invocation).
tools: Read, Grep, Glob
model: sonnet
---

You are a [role]. Your job is to [responsibility].

Focus on:
- ...
```
A short comment at the top: copy, rename (kebab-case, matching `name:`), trim `tools:` to only what's needed.

### 4. `.claude/skills/_template/SKILL.md`
Template skill following the `.claude/skills/<skill-name>/SKILL.md` convention:
```markdown
---
description: One sentence describing what this skill does and when Claude should invoke it (drives auto-invocation).
---

## Steps

1. ...
2. ...

Report back: ...
```
Comment notes optional `reference.md` / `scripts/` companion files for larger skills.

### 5. `.claude/settings.json`
Checked-in, team-shared config with hooks:

```json
{
  "hooks": {
    "SessionStart": [
      { "hooks": [{ "type": "command", "command": "bash .claude/hooks/session-start.sh" }] }
    ],
    "PreToolUse": [
      {
        "matcher": "Bash",
        "if": "Bash(git commit *)",
        "hooks": [{ "type": "command", "command": "bash .claude/hooks/pre-commit-check.sh" }]
      }
    ],
    "Stop": [
      { "hooks": [{ "type": "command", "command": "bash .claude/hooks/enforce-session-log.sh" }] }
    ]
  }
}
```

### 6. `.claude/hooks/session-start.sh`
Prints current git branch, `git status -s`, and last commit as extra session context. Read-only, no side effects.

### 7. `.claude/hooks/pre-commit-check.sh`
Fires only before `git commit` (via the `if` filter). Looks for known lint/format entry points; if none exist yet, prints a one-line notice and exits 0 (non-blocking). Written so the one conditional branch gets filled in once real lint tooling exists, rather than stubbing every possible stack.

### 8. `.claude/hooks/enforce-session-log.sh` (the enforcement mechanism)
Registered on `Stop`. Reads the hook's JSON stdin (`session_id`, `transcript_path`, `cwd`, `stop_hook_active`). Logic:
1. If `stop_hook_active` is `true` (this session was already re-prompted once by this same hook), exit 0 and allow the stop — prevents an infinite re-prompt loop if Claude can't or won't comply.
2. Check whether `docs/sessions/` already contains a file for this `session_id` (by filename match) — if yes, exit 0 (already logged).
3. Otherwise, scan `transcript_path` (JSONL) for evidence this session did code-altering or planning work: any `tool_use` entries named `Edit`, `Write`, `NotebookEdit`, or `ExitPlanMode`, or a git-modifying `Bash` call. Also treat any uncommitted `git status --porcelain` changes as evidence.
4. If evidence is found and no log file exists: exit with the blocking exit code and a message telling Claude exactly what to do — create `docs/sessions/<date>_<session_id-short>.md` from `docs/sessions/_template.md`, filling in prompt/plan/files-touched/summary — then stop again.
5. If no evidence of code changes or planning (e.g., a pure read-only Q&A session), exit 0 — no log required.

This keeps content generation (prompt, plan, summary) with Claude, where it belongs, while the hook only mechanically verifies a log exists.

### 9. `.gitignore` (new, repo root)
Repo has none today. Add:
```
.claude/settings.local.json
```
(No `.claude/logs/` entry needed — logging now lives in the intentionally-committed `docs/sessions/`, not a scratch log file.)

## Verification

- List `.claude/agents/`, `.claude/skills/`, `docs/sessions/` to confirm files land in the right place; confirm `.claude/settings.json` is valid JSON.
- Start a fresh Claude Code session in this repo and confirm the `SessionStart` hook's git summary appears as injected context.
- Run `git commit --allow-empty -m "test"` on a scratch branch to confirm `pre-commit-check.sh` fires and no-ops cleanly without blocking.
- **Enforcement test:** in a session, make a trivial code edit (or run `/plan`) and then try to end the session without creating a `docs/sessions/` entry — confirm `enforce-session-log.sh` blocks the stop with a clear instruction, then confirm creating the session file from the template allows the session to end normally.
- Confirm a read-only session (e.g., just asking a question, no edits) is **not** blocked at `Stop`.
