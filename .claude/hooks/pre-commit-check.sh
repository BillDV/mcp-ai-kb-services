#!/usr/bin/env bash
# PreToolUse hook, fired only before `git commit` (see the "if" filter in
# .claude/settings.json). Runs the project's lint command if one is
# configured; otherwise no-ops so this doesn't block commits in a repo that
# has no tooling yet.
set -euo pipefail

if [ -f package.json ] && grep -q '"lint"[[:space:]]*:' package.json 2>/dev/null; then
  echo "pre-commit-check: running npm run lint..."
  npm run lint
  exit 0
fi

echo "pre-commit-check: no lint tooling configured yet, skipping."
exit 0
