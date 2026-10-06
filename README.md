<!--
File      : README.md
Developer : Ayman Ouf (M5)
Reviewer  : Omar Emam (M1)
Task(s)   : T006, T091
-->
# FocusFlow

> **Study smart. Stay accountable.**
> A collaborative study and progress-tracking platform: Student and Guardian roles, study rooms, Kanban tasks, personal library with AI chat, dashboards and automated WhatsApp / email reports.
> DEPI final project, Group 1. Supervisor: Eng. Mohamed Essam.

Sources of truth: **BRD** (FOCUS FLOW-BRD-001 v1.1), **FSD** (FOCUS FLOW-FSD-001 v1.1) and the delivery plan **Tasks.xlsx**. Task IDs (`T001`…`T091`) and module IDs (`FR-01`…`FR-11`, `BR-xx`, `RULE-xx`) come from those documents. Never invent new ones.

## Main features

| Module | Feature | Priority |
|---|---|---|
| FR-01 | Authentication, roles (Student / Guardian / Admin) and guardian linking by invitation code | Must |
| FR-02 | Study rooms filtered by subject, text chat, synchronized Pomodoro (SignalR) | Must |
| FR-03 | Progress tracking: study time, streak, weekly goal | Must |
| FR-04 | Very simple to-do list (Kanban: To Do / Doing / Done) | Must |
| FR-05 | Private storage of PDFs and links per student | Must |
| FR-06 | AI chat on the student's own files (Gemini, Groq fallback) | Should |
| FR-07 | Student and Guardian dashboards | Must |
| FR-08 | Scheduled reports delivered by WhatsApp / email | Must |
| FR-09 | Gamification: points, levels, streaks | Should |
| FR-10 | In-app notifications | Must |
| FR-11 | Administration and moderation | Should |

Key business rule: **Guardians see statistics only.** File contents, AI chats and private notes are never visible to guardians (RULE-01). A student can have zero, one or many guardians (BRD §10).

## Architecture

A **modular monolith** (FSD §2.2): one ASP.NET Core backend organized by feature, a React single-page app, SQL Server, SignalR for rooms and notifications, hosted-service jobs for reports and aggregates. Details: [docs/architecture.md](docs/architecture.md).

## Technology stack (FSD §3)

| Layer | Technology |
|---|---|
| Frontend | React 18 + Vite, React Router, Tailwind CSS, TanStack Query, Zustand, Recharts, dnd-kit |
| Backend | .NET 10 (LTS), ASP.NET Core Web API (C#), FluentValidation, JWT bearer, BCrypt.Net |
| Database | SQL Server 2022 / Azure SQL Database, EF Core 10 (code-first migrations), LINQ, full-text search |
| Real-time | SignalR |
| Files / PDF | IFormFile, Supabase Storage (local disk in dev), PdfPig |
| Jobs | `BackgroundService` hosted services |
| AI / Messaging | Gemini and Groq (typed HttpClients), Twilio WhatsApp Sandbox, wa.me links, MailKit (SMTP) |
| Testing | xUnit, WebApplicationFactory, Playwright |
| DevOps | Docker Compose, GitHub Actions |

## Repository structure

```
FocusFlow/
├── client/                 React + Vite app                       (T026)
├── server/
│   ├── FocusFlow.sln                                              (T008)
│   ├── db/docker/          SQL Server 2022 + Full-Text image      (T010)
│   ├── src/FocusFlow.Api/  ASP.NET Core Web API (feature folders) (T008)
│   └── tests/FocusFlow.Api.Tests/  xUnit unit + integration       (T017, T062)
├── docs/                   architecture, guides, task board
├── scripts/                GitHub setup script and task data
├── .github/                CI, PR template, issue templates
├── docker-compose.yml
├── .env.example
└── README.md
```

## Development setup

Prerequisites: **.NET 10 SDK**, **Docker**, **Node.js 20**, EF Core CLI (`dotnet tool install --global dotnet-ef`), Git, GitHub CLI (optional).

```bash
git clone https://github.com/mohamed-elfrsisi/FocusFlow.git
cd FocusFlow
git checkout develop

# 1. database (needs MSSQL_SA_PASSWORD in your local .env)
cp .env.example .env            # then edit the password; .env is git-ignored
docker compose up -d --wait db

# 2. API (after T008 creates the solution)
cd server/src/FocusFlow.Api
dotnet user-secrets set "ConnectionStrings:Default" "Server=localhost,1433;Database=focusflow;User Id=sa;Password=<your MSSQL_SA_PASSWORD>;TrustServerCertificate=True"
dotnet user-secrets set "JWT_SECRET" "<32+ random characters>"
dotnet user-secrets set "JWT_REFRESH_SECRET" "<32+ random characters>"
dotnet ef database update
dotnet watch run                # http://localhost:5000, Swagger UI at /docs

# 3. client (second terminal, after T026 creates the app)
cd client && npm install && npm run dev     # http://localhost:5173
```

Never commit secrets. Full variable list and rules: [docs/development-guide.md](docs/development-guide.md) and [docs/environment.example](docs/environment.example).

## Branching strategy

```
main      stable / release-ready
 └─ develop   integration branch (default branch)
     └─ feature/TXXX-short-description   one branch per task
```

Nobody pushes directly to `main` or `develop`. Feature branches merge into `develop` by Pull Request with one review. `develop` merges into `main` by Pull Request when a release is ready. See [docs/branching-strategy.md](docs/branching-strategy.md).

## How to contribute (team workflow)

```bash
git checkout develop && git pull origin develop
git checkout -b feature/T019-register-login-refresh-p1     # use the real task ID
# work...
git add .
git commit -m "feat: implement register and login endpoints"
git push -u origin feature/T019-register-login-refresh-p1
```

Open a Pull Request `feature/T019-… → develop`, fill the template, write `Closes #<issue number>`, ask the reviewer named in the task. Branch names per task are listed in [docs/task-board.md](docs/task-board.md).

**Definition of Done** (FSD §15.1): acceptance criteria met, tests added, no lint errors, API documented, reviewed, deployed to staging.

## Team and plan

| Code | Name | Role / vertical slice | Owns |
|---|---|---|---|
| M1 | Omar Emam | Tech Lead: Auth, Security, Admin, Notifications API | FR-01, FR-11 (API), FR-10 (API) |
| M2 | Mohamed El-Farsisi | Real-time and DevOps | FR-02, CI/CD, deployments |
| M3 | Yasmeen Ayman | AI and Content | FR-05, FR-06 |
| M4 | Saeid El-Kholy | Frontend and UX Lead | FR-04, FR-07, design system |
| M5 | Ayman Ouf | Data, Reports and QA / PM | FR-03, FR-08, FR-09, FR-10 (bell UI) |

Plan: official start **Sat 10 Oct 2026** → feature freeze **Fri 27 Nov** → final delivery **Thu 3 Dec 2026**. Milestones, all 91 tasks and dependencies: [docs/task-board.md](docs/task-board.md). Who owns which file: [docs/file-ownership.md](docs/file-ownership.md).
