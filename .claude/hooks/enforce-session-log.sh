#!/usr/bin/env bash
# Stop hook: enforces the session-logging policy documented in CLAUDE.md.
#
# If this session altered code (uncommitted changes, or Edit/Write/
# NotebookEdit/ExitPlanMode tool use in the transcript) and no matching
# docs/sessions/ entry exists yet, blocks the stop (exit 2) and tells Claude
# what file to create. Content generation (prompt, plan, summary) is left
# to Claude — this script only verifies a log exists.
set -uo pipefail

INPUT="$(cat)"

extract_str() {
  printf '%s' "$INPUT" | sed -E -n "s/.*\"$1\"[[:space:]]*:[[:space:]]*\"([^\"]*)\".*/\1/p"
}

extract_bool() {
  printf '%s' "$INPUT" | sed -E -n "s/.*\"$1\"[[:space:]]*:[[:space:]]*(true|false).*/\1/p"
}

# Avoid an infinite re-prompt loop if Claude doesn't/can't comply.
stop_hook_active="$(extract_bool stop_hook_active)"
if [ "$stop_hook_active" = "true" ]; then
  exit 0
fi

session_id="$(extract_str session_id)"
transcript_path="$(extract_str transcript_path)"
short_id="${session_id:0:8}"
sessions_dir="docs/sessions"

if [ -d "$sessions_dir" ] && [ -n "$short_id" ] && ls "$sessions_dir"/*"$short_id"* >/dev/null 2>&1; then
  exit 0
fi

evidence=""

if [ -n "$(git status --porcelain 2>/dev/null)" ]; then
  evidence="uncommitted changes"
fi

if [ -z "$evidence" ] && [ -n "$transcript_path" ] && [ -f "$transcript_path" ]; then
  if grep -Eq '"name"[[:space:]]*:[[:space:]]*"(Edit|Write|NotebookEdit|ExitPlanMode)"' "$transcript_path" 2>/dev/null; then
    evidence="code-altering or plan-creating tool use"
  fi
fi

if [ -n "$evidence" ]; then
  {
    echo "Session logging policy: this session shows evidence of $evidence, but no docs/sessions/ entry exists for session ${short_id:-unknown}."
    echo "Before stopping: copy docs/sessions/_template.md to docs/sessions/$(date +%Y-%m-%d)_${short_id}.md and fill in the prompt(s), plan, files touched, and summary/outcome."
  } >&2
  exit 2
fi

exit 0
