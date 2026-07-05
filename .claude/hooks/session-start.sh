#!/usr/bin/env bash
# SessionStart hook: prints a short git summary as extra session context.
set -uo pipefail

branch="$(git rev-parse --abbrev-ref HEAD 2>/dev/null || echo "unknown")"
echo "Current branch: $branch"
echo
echo "Working tree status:"
git status -s 2>/dev/null || echo "(not a git repo or no changes)"
echo
echo "Last commit:"
git log -1 --oneline 2>/dev/null || echo "no commits yet"

exit 0
