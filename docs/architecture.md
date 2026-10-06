<!--
File      : docs/architecture.md
Developer : Omar Emam (M1)
Reviewer  : Mohamed El-Farsisi (M2)
Task(s)   : T008
-->
# Architecture

Everything here comes from the FSD (FOCUS FLOW-FSD-001 v1.1) and BRD. Where the FSD leaves a point open, it is listed under *Open items* instead of being decided here. Owner: Tech Lead (T008).

## 1. Style: modular monolith (confirmed, FSD §2.2)

One backend deployable, organized by feature folders. Chosen because it is simple to build, test and deploy for a student team and can be split later. SQL Server (not NoSQL) for strong relations and SQL aggregates; full-text search instead of a vector DB (zero cost); the server holds all API keys; stateless API with JWT; guardian model is many-to-many.

```
Web Client (React + Vite + Tailwind, SPA)
   │ REST /api/v1 (JSON)             │ WebSocket /hubs/rooms (SignalR)
   ▼                                  ▼
ASP.NET Core Web API  ────────────  SignalR hub (rooms chat + Pomodoro, notifications push)
   │        │          │               │
   │        │          │               └─ Scheduler (BackgroundService): hourly reports, nightly streaks/aggregates
   ▼        ▼          ▼
SQL Server  File Storage   AI providers (Gemini → Groq fallback)      Messaging (Twilio WhatsApp Sandbox / wa.me / SMTP)
```

## 2. Components (FSD §2.1)

| Component | Responsibility |
|---|---|
| Web Client | Responsive SPA for Student and Guardian; routing by role; charts; Kanban; rooms; AI chat |
| REST API | Authentication, business logic, validation, role checks, aggregation |
| SignalR | Room membership, text chat, synchronized Pomodoro timer; per-user group for notifications |
| SQL Server | Relational data with constraints and indexes; full-text search for AI retrieval |
| File Storage | Private PDF storage per student, short-lived signed URLs (60 s) |
| Scheduler | Hourly report dispatcher, nightly streak and aggregate jobs |
| AI Providers | Gemini (primary), Groq (fallback) |
| Messaging | WhatsApp (wa.me link, Twilio Sandbox) and SMTP email |

## 3. Backend layout (FSD §4.1)

```
server/
  FocusFlow.sln
  db/docker/Dockerfile            SQL Server 2022 + Full-Text Search
  src/FocusFlow.Api/
    Program.cs                    composition root: DI, DbContext, auth, CORS, rate limits, hubs
    Common/                       AppException, ErrorHandler, UserExtensions, DbExtensions, Pagination
    Data/                         FocusFlowDbContext, Entities/, Migrations/, Seed
    Infrastructure/               storage drivers (Supabase, local disk), Gemini/Groq clients, mail
    Features/<Feature>/           XxxController, XxxService, XxxDtos, XxxValidators
    Hubs/                         RoomsHub, RoomTimerService
    Jobs/                         HourlyReportsJob, NightlyAggregatesJob
  tests/FocusFlow.Api.Tests/      xUnit + WebApplicationFactory (unit and integration)
```

| Module | Feature folder(s) | Main tables | Real-time / jobs |
|---|---|---|---|
| FR-01 Auth, roles, guardian linking | Auth, Guardians | users, guardian_links | |
| FR-02 Study rooms | Rooms | rooms, room_members, room_messages, message_reports | RoomsHub, RoomTimerService |
| FR-03 Progress | Sessions, Progress | study_sessions, weekly_goals | nightly aggregates |
| FR-04 Kanban | Tasks | tasks | |
| FR-05 Library | Library | library_items, library_chunks | IndexWorker (Channel queue) |
| FR-06 AI chat | Ai | chat_sessions, chat_messages | |
| FR-07 Dashboards | Dashboard | (aggregates) | |
| FR-08 Reports | Reports | report_settings, reports, report_deliveries, guardian_actions | HourlyReportsJob |
| FR-09 Gamification | Gamification | gamification_profiles, point_events, badges, student_badges | |
| FR-10 Notifications | Notifications | notifications | SignalR push |
| FR-11 Admin | Admin | subjects (+ reports queue) | |

## 4. Design patterns and principles visible in the FSD

Used where the FSD reference code already does it, not added on top:

- **Dependency injection** everywhere (constructor injection; scoped `DbContext`, singleton `RoomTimerService`).
- **Service layer per feature** (controllers stay thin; one service owns the rules).
- **Strategy / adapter behind an interface**: `IStorage` (Supabase vs local disk), AI providers behind one `GenerateAsync` with Gemini → Groq **fallback chain**.
- **Policy / requirement-handler authorization**: roles + `ViewStudentRequirement` (a guardian only sees students with an active link).
- **Producer-consumer background work**: `Channel<IndexJob>` + `BackgroundService` worker for PDF indexing; `BackgroundService` + `PeriodicTimer` for reports.
- **Idempotency through database constraints**: unique indexes for one-report-per-period, one-point-per-action-and-reference, one-badge-per-student; atomic conditional update (`ExecuteUpdateAsync`) for accepting an invitation.
- **Centralized error handling** (`AppException` + `IExceptionHandler`) with one error format.
- **Rules in one place**: gamification rules live in a single config object.

## 5. API conventions (FSD §8)

Base `/api/v1`, JSON only (multipart for uploads). Bearer JWT (access 15 min, refresh 7 days in httpOnly cookie). Success `{ "data": …, "meta": … }`, error `{ "error": { "code", "message", "details" } }`. Pagination `?page=1&pageSize=20` (max 100). Dates ISO 8601 UTC. Rate limits: global 100 req/min/IP, auth 10/min, AI 30/day/student. Error codes: FSD Appendix A. Swagger UI at `/docs`.

## 6. Roles and data visibility (FSD §5, BRD §9)

| Capability | Student | Guardian | Admin |
|---|---|---|---|
| Invite / revoke guardians | Yes | No | No |
| Accept invitation | No | Yes | No |
| Tasks, library, AI chat, log sessions | Yes | No | No |
| View progress statistics | Own | Linked students only | Aggregate only |
| Rooms | Create / join | No | Moderate |
| Receive reports | If no guardian | Yes | No |
| Encourage / remind | No | Yes | No |
| Subjects, users, abuse reports | No | No | Yes |

Guardians receive dedicated DTOs with **statistics only** (RULE-01). Ownership checks always use the `student_id` from the token.

## 7. Security (FSD §12)

Role policies on every endpoint; bcrypt cost 12; short-lived access tokens; rate limiting; parameterized queries only; FluentValidation; HTTPS + HSTS + security headers; CORS restricted to `CLIENT_URL`; PDF type/size checks, random file names, private bucket, signed URLs; secrets only in environment variables; AI context treated as data, daily limits.

## 8. Integrations and fallbacks (FSD §10)

| Service | Fallback |
|---|---|
| Gemini | Groq, then friendly `AI_UNAVAILABLE` |
| Twilio WhatsApp Sandbox | Retry (5, 30, 120 min) then email |
| wa.me link | none needed (manual send) |
| SMTP (MailKit) | log and retry |
| Supabase Storage | fail upload with 503 |

## 9. Environments (FSD §14)

Local: Docker Compose (database) + `dotnet watch run`. Staging: Vercel (client), Render/Railway (API as Docker container), Azure SQL Database free offer, auto-deploy from `develop` (T022). Production/demo: same stack, separate project and keys (T065).

## 10. Open items (FSD Appendix B, plus gaps found during setup)

- Final stack confirmation by the supervisor, minimum student age and consent wording (RULE-10), WhatsApp approach for the demo, final subject list, Arabic/RTL scope.
- The FSD does not say how the client reaches the API in development (its axios code uses relative `/api/v1/...` while the API runs on `http://localhost:5000`). T026 / T008 must decide (a Vite dev proxy is the likely choice) and document it here.
- The FSD writes the project folder as `Focus Flow.Api` in places. This repo uses `FocusFlow.Api`.
- Tests: this repo follows the FSD layout, `server/tests/FocusFlow.Api.Tests/` (not a root `tests/` folder). The FSD does not say where the Playwright end-to-end tests live; T085 decides.
