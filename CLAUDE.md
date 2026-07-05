# mcp-ai-kb-services

## Project overview

TODO: this repo is a fresh scaffold. Fill in a short description of what
`mcp-ai-kb-services` does once the initial implementation lands.

## Architecture

TODO: `src/` is currently empty. Once a stack is chosen, describe the
high-level architecture here (services, entry points, data flow).

## Build / test / run

TODO: no manifest (package.json / .csproj / pyproject.toml / go.mod) exists
yet. Add the real build/test/run commands here once one does.

## Conventions

TODO: document coding standards and repo-specific conventions here as they
are established.

## Working with Claude

- Project-specific subagents live in [.claude/agents/](.claude/agents/) —
  copy [_template.md](.claude/agents/_template.md) to add a new one.
- Project-specific skills live in [.claude/skills/](.claude/skills/) — copy
  [_template/](.claude/skills/_template/) to add a new one.
- Session hooks are configured in [.claude/settings.json](.claude/settings.json),
  with scripts under [.claude/hooks/](.claude/hooks/).

## Session logging policy (required)

Any debugging session, plan creation, `AskUserQuestion` exchange, or agent
action that alters code must result in an entry under
[docs/sessions/](docs/sessions/), created from
[docs/sessions/_template.md](docs/sessions/_template.md), before the session
ends. Fill in the prompt(s), the plan (or a link to the plan file), the files
touched, and a short summary/outcome.

This is enforced automatically by `.claude/hooks/enforce-session-log.sh`
(registered on the `Stop` hook event): it blocks session end and re-prompts
if the session altered code or produced a plan and no matching
`docs/sessions/` entry exists. Read-only sessions (no edits, no plan) are not
blocked.
