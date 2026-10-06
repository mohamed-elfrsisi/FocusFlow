<!--
File      : docs/development-guide.md
Developer : Mohamed El-Farsisi (M2)
Reviewer  : Yasmeen Ayman (M3)
Task(s)   : T010, T008
Note      : Setup part: T010 (Mohamed). Conventions part: T008 (Omar).
-->
# Development Guide

## 0. Repository bootstrap (repo owner, once)

The GitHub repo is empty, so the first commit goes straight to `main` (branch protection is applied afterwards by the script).

```bash
git clone https://github.com/mohamed-elfrsisi/FocusFlow.git && cd FocusFlow
# copy the contents of this setup folder into the clone (including the hidden .github folder and .gitignore)
git checkout -b main            # only if Git did not create it already
git add .
git commit -m "chore: initial repository setup"
git push -u origin main

# 1. Invite the 4 teammates as collaborators (Settings > Collaborators) and put their GitHub usernames in scripts/team.json
# 2. GitHub CLI
gh auth login
gh auth refresh -s project
# 3. Preview, then run (creates develop, protection, labels, milestones, 91 issues, Project)
node scripts/setup-github.mjs --dry-run
node scripts/setup-github.mjs
# 4. Get develop locally
git fetch origin && git checkout develop
```

After this, nobody pushes to `main` or `develop` directly. Check the result in GitHub: *Settings > Branches*, *Issues* (91 issues, 12 labels, 5 milestones) and *Projects*. If the Project columns were not created by the script, rename the Status options by hand: `Backlog, Ready, In Progress, Code Review, Testing, Done`.

## 1. Prerequisites (FSD §4.3)

.NET 10 SDK, Docker, Node.js 20, Git, the EF Core CLI (`dotnet tool install --global dotnet-ef`). Optional: GitHub CLI (`gh`) for the setup script.

## 2. First-time setup

```bash
git clone https://github.com/mohamed-elfrsisi/FocusFlow.git && cd FocusFlow
git checkout develop

cp .env.example .env                  # set MSSQL_SA_PASSWORD (git-ignored)
docker compose up -d --wait db        # SQL Server 2022 with Full-Text Search (T010)

cd server/src/FocusFlow.Api           # exists after T008
dotnet user-secrets set "ConnectionStrings:Default" "Server=localhost,1433;Database=focusflow;User Id=sa;Password=<MSSQL_SA_PASSWORD>;TrustServerCertificate=True"
dotnet user-secrets set "JWT_SECRET" "<32+ random characters>"
dotnet user-secrets set "JWT_REFRESH_SECRET" "<32+ random characters>"
dotnet ef database update
dotnet watch run                      # API http://localhost:5000, Swagger at /docs

cd ../../../client && npm install && npm run dev   # exists after T026, http://localhost:5173
```

Run the tests: `dotnet test server/FocusFlow.sln`. Build the client: `cd client && npm run build`.

## 3. Secrets and environment variables

| Rule | How |
|---|---|
| No password, key or token in Git, ever | `.gitignore` blocks `.env*` (except `.env.example`), key and certificate files |
| API secrets locally | `dotnet user-secrets` (stored outside the repo) |
| Docker Compose password | `.env` at the repo root (git-ignored), template in `.env.example` |
| Staging / production | Environment variables in the hosting provider; separate keys per environment (FSD §12) |
| CI | Throwaway values generated per run inside `ci.yml`; nothing stored |
| Provider keys (Gemini, Groq, Supabase, Twilio, SMTP) | Created in T012 by M3, shared with the team through a password manager or the provider dashboards. **Never** in chat, issues or commits |

Full variable list with placeholders: [environment.example](environment.example). Nested keys use `:` in user-secrets and `__` in environment variables.

If a secret is committed by mistake: rotate it immediately at the provider, then remove it from history. Deleting it in a later commit is not enough.

## 4. How to start a task

```bash
git checkout develop
git pull origin develop
git checkout -b feature/T019-register-login-refresh-p1     # real task ID from docs/task-board.md

# work, commit small and often
git add .
git commit -m "feat: implement register and login endpoints"

git push -u origin feature/T019-register-login-refresh-p1
```

Then open a Pull Request:

```
feature/T019-register-login-refresh-p1
        ↓
develop
```

1. Fill the PR template, write `Closes #<issue number>`.
2. Request the reviewer listed for that task. One approval and a green CI are required.
3. After the merge, delete the branch and move the card to *Done*.
4. Respect **Depends on**: do not start a task whose dependencies are not merged (check the issue and the board).

## 5. GitHub Project columns

`Backlog → Ready → In Progress → Code Review → Testing → Done`

| Column | Meaning |
|---|---|
| Backlog | Planned, dependencies not finished or week not started |
| Ready | Dependencies merged, week has started |
| In Progress | Branch exists, work started |
| Code Review | PR open |
| Testing | Merged to `develop`, being verified on staging against the acceptance criteria |
| Done | Acceptance criteria verified, PR merged |

## 6. Conventions (FSD §11)

- C#: `dotnet format` and Roslyn analyzers (CI runs `dotnet format --verify-no-changes`). JavaScript: ESLint + Prettier (CI runs `npm run lint` if the script exists).
- Features by folder. One `XxxController`, `XxxService`, `XxxDtos`, `XxxValidators` per feature.
- Always filter by `student_id` from the token, never from the request.
- Errors: throw `AppException(status, CODE)`; codes are in the FSD Appendix A.
- Guardian endpoints return statistics-only DTOs (RULE-01).
- Every Must endpoint gets integration tests for success, validation, 401 and 403 (FSD §13).

## 7. CI (T021)

`.github/workflows/ci.yml` runs on every pull request and every push to `main` / `develop`:

- **backend**: restore → `dotnet format --verify-no-changes` → build → SQL Server (Full-Text) in Docker → `dotnet test`. Needs `server/FocusFlow.sln`.
- **frontend**: `npm ci` → `npm run lint --if-present` → `npm run build` on Node 20. Needs `client/package.json` and `package-lock.json`.

Until T008 and T026 add those files the matching job passes with a "skipped" notice. From then on any failing step fails the job.

## 8. GitHub setup script

`node scripts/setup-github.mjs --repo mohamed-elfrsisi/FocusFlow` creates branches, protection, labels, milestones, the 91 issues and the Project. See the header of the script. Run with `--dry-run` first.
