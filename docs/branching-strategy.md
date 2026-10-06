<!--
File      : docs/branching-strategy.md
Developer : Ayman Ouf (M5)
Reviewer  : Omar Emam (M1)
Task(s)   : T006
-->
# Branching Strategy

Source: FSD §15.1 (`main` release, `develop` integration, `feature/*`, pull request with one review before merge) and the project rules requested for this repo.

```
main
  ↓  Pull Request when a release is ready
develop
  ↓  Pull Request per task
feature/TXXX-short-description
```

| Branch | Purpose | Who merges | Rules |
|---|---|---|---|
| `main` | Stable, release-ready, demo-ready code. Tagged at the end of each milestone. | Tech Lead (M1), by PR from `develop` | Protected. No direct pushes. |
| `develop` | Integration branch. **Default branch of the repo.** Staging auto-deploys from it (T022). | Anyone, by PR from a feature branch | Protected. No direct pushes. 1 approval + CI green. |
| `feature/TXXX-short-description` | One branch per task from `Tasks.xlsx`. | Author opens the PR, reviewer approves | Branch from `develop`. Deleted after merge. |

Why `develop` is the default branch: GitHub only auto-closes issues (`Closes #12`) when a PR merges into the default branch. Since every task PR targets `develop`, making it the default makes closing keywords work and makes new PRs target `develop` by default.

## Naming

`feature/<TaskID>-<short-description>` using the **real task ID** from the sheet, for example:

| Task | Branch |
|---|---|
| T008 API solution skeleton | `feature/T008-api-solution-skeleton` |
| T009 Core database schema + migrations + seed | `feature/T009-core-database-schema` |
| T019 Register / login / refresh (part 1/2) | `feature/T019-register-login-refresh-p1` |
| T038 Tasks API | `feature/T038-tasks-api` |

The suggested name for every task is in [task-board.md](task-board.md). Tasks split in two parts get `-p1` / `-p2`.

## Commit messages

Conventional commits: `feat:`, `fix:`, `docs:`, `test:`, `refactor:`, `chore:`, `ci:`. Example: `feat: implement guardian invitation endpoint`.

## Pull requests

1. Title: `[T019] Register / login / refresh (part 1/2)`.
2. Base: `develop`. Fill `.github/pull_request_template.md`. Write `Closes #<issue>`.
3. Reviewer: the one named in the task (column *Reviewer* in the sheet). One approval is required.
4. CI must pass (`backend` and `frontend` jobs).
5. Squash or merge, then delete the branch.

## Release

When a milestone is complete and staging is verified: PR `develop → main`, Tech Lead approves, tag the release. Do not hotfix on `main` directly. Branch `feature/...` from `develop`, or `fix/...` for urgent bugs, and merge through `develop`.

## Protection settings applied by `scripts/setup-github.mjs`

- Pull request required, 1 approving review, stale approvals dismissed.
- Status checks `backend` and `frontend` required.
- Force pushes and branch deletion disabled.
- Admin enforcement is **off** on purpose so the repo owner can recover from a CI outage. Turn it on later in *Settings → Branches* if wanted.
