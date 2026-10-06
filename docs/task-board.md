<!--
File      : docs/task-board.md
Developer : Ayman Ouf (M5)
Reviewer  : Omar Emam (M1)
Task(s)   : T006, T018
Note      : Generated from Tasks.xlsx. Change the sheet first.
-->
# Task Board

Source of truth: `Tasks.xlsx` (sheet **Task Board**), 91 tasks, plan Sat 10 Oct → Thu 3 Dec 2026. Task IDs, names, modules, priorities, owners, reviewers and dependencies are copied from the sheet unchanged.

Derived by the Tech Lead setup (not in the sheet): **Milestone** (by week, see below), **Area**, **Type** labels and the suggested **Branch** name.

## Milestones

| Milestone | Due | Tasks by week | Done when (from Tasks.xlsx) |
|---|---|---|---|
| M0: Foundation | 2026-10-23 | W1–W3 | Repo, CI + staging, DB schema, API skeleton, design system and app shell ready; library upload and sessions API started. Includes W1 prep tasks. |
| MVP-1: Core | 2026-11-06 | W4–W5 | Register / login with roles, guardian linking, Kanban board, study sessions + progress API, library upload and search work on staging. |
| Backend complete + production live | 2026-11-20 | W6–W7 | Rooms, reports + delivery, AI retrieval and notifications working on the API; production deployment running; student dashboard usable. |
| FEATURE FREEZE | 2026-11-27 | W8 | Room screen, AI chat UI, guardian dashboard and security review finished. No new features after this day. |
| FINAL DELIVERY | 2026-12-03 | W9 | Docs final, demo rehearsed twice, no critical bugs. Fri 4 Dec is the emergency buffer (deadline is before 5 Dec). |

Rule used: a task belongs to the first milestone whose date falls on or after the end of the task's week. Week-1 prep tasks (4–9 Oct) go to *M0: Foundation*.

## Project workflow (GitHub Project columns)

`Backlog → Ready → In Progress → Code Review → Testing → Done`

## Team

| Code | Name | Role / vertical slice |
|---|---|---|
| M1 | Omar Emam | Tech Lead: Auth, Security, Admin, Notifications API |
| M2 | Mohamed El-Farsisi | Real-time and DevOps |
| M3 | Yasmeen Ayman | AI and Content |
| M4 | Saeid El-Kholy | Frontend and UX Lead |
| M5 | Ayman Ouf | Data, Reports and QA / PM |

## Task → Name → Area → Priority → Milestone → Dependencies

| ID | Task | Module | Area | Priority | Milestone | Depends on | Owner → Reviewer | Week | Branch |
|---|---|---|---|---|---|---|---|---|---|
| T001 | Learning spike: JWT, EF Core migrations, ASP.NET authorization policies | Foundation | backend | Must | M0: Foundation | — | M1 → M2 | W1 | `feature/T001-jwt-ef-core-migrations` |
| T002 | Learning spike: SignalR hubs, Docker, GitHub Actions | Foundation | backend, devops | Must | M0: Foundation | — | M2 → M3 | W1 | `feature/T002-signalr-hubs-docker-github` |
| T003 | Learning spike: full-text search, text chunking, Gemini / Groq API basics | FR-06 | backend | Should | M0: Foundation | — | M3 → M4 | W1 | `feature/T003-full-text-search-text` |
| T004 | Evaluate PDF text-extraction libraries on sample Arabic and English PDFs | FR-05 | backend | Must | M0: Foundation | — | M3 → M4 | W1 | `feature/T004-evaluate-pdf-text-extraction` |
| T005 | Learning spike: React + Vite, TanStack Query, dnd-kit | Foundation | frontend | Must | M0: Foundation | — | M4 → M5 | W1 | `feature/T005-react-vite-tanstack-query` |
| T006 | Repo rules (main / develop / feature), PR template, issue board with one epic per FR | Foundation | — | Must | M0: Foundation | — | M5 → M1 | W1 | `feature/T006-repo-rules-main-develop` |
| T007 | Learning spike: hosted services, time zones in LINQ, Twilio Sandbox | FR-08 | backend | Should | M0: Foundation | — | M5 → M1 | W1 | `feature/T007-hosted-services-time-zones` |
| T008 | API solution skeleton: feature folders, error format, validation, Swagger, API conventions | Foundation | backend, architecture | Must | M0: Foundation | — | M1 → M2 | W2 | `feature/T008-api-solution-skeleton` |
| T009 | Core database schema + EF Core migrations + seed data (subjects, admin account) | Foundation | backend, database | Must | M0: Foundation | T008 | M1 → M2 | W2 | `feature/T009-core-database-schema` |
| T010 | Docker compose: SQL Server with Full-Text image + local setup guide | Foundation | devops, database | Must | M0: Foundation | — | M2 → M3 | W2 | `feature/T010-docker-compose` |
| T011 | Rooms REST: create / list / join / leave with capacity check in a transaction | FR-02 | backend | Must | M0: Foundation | — | M2 → M3 | W2 | `feature/T011-rooms-rest` |
| T012 | Create provider accounts and keys (Gemini, Groq, Supabase bucket) + env variables | FR-05 | devops | Must | M0: Foundation | — | M3 → M4 | W2 | `feature/T012-create-provider-accounts` |
| T013 | Private storage prototype: upload + signed URL (60 seconds) | FR-05 | backend | Must | M0: Foundation | T012 | M3 → M4 | W2 | `feature/T013-private-storage-prototype` |
| T014 | Library tables + PDF upload endpoint (type, size, quota validation) | FR-05 | backend | Must | M0: Foundation | T013 | M3 → M4 | W2 | `feature/T014-library-tables-pdf-upload` |
| T015 | Design system: purple tokens, typography, core components | Foundation | frontend | Must | M0: Foundation | — | M4 → M5 | W2 | `feature/T015-design-system` |
| T016 | Wireframes: student screens (dashboard, Kanban, library, rooms) | Foundation | frontend | Must | M0: Foundation | — | M4 → M5 | W2 | `feature/T016-wireframes` |
| T017 | Test strategy, QA checklist, seed-data scripts | Foundation | — | Should | M0: Foundation | — | M5 → M1 | W2 | `feature/T017-test-strategy-qa-checklist` |
| T018 | Sprint plan and ownership review with supervisor; keep BRD / FSD updated | Foundation | — | Must | M0: Foundation | — | M5 → M1 | W2 | `feature/T018-sprint-plan-and-ownership` |
| T019 | Register / login / refresh: bcrypt hashing, JWT access token + httpOnly refresh cookie (part 1/2) | FR-01 | backend | Must | M0: Foundation | T009 | M1 → M2 | W3 | `feature/T019-register-login-refresh-p1` |
| T020 | Register / login / refresh: bcrypt hashing, JWT access token + httpOnly refresh cookie (part 2/2) | FR-01 | backend | Must | M0: Foundation | T019 | M1 → M2 | W3 | `feature/T020-register-login-refresh-p2` |
| T021 | CI pipeline: build, test and lint on every pull request | Foundation | devops | Must | M0: Foundation | T008 | M2 → M3 | W3 | `feature/T021-ci-pipeline` |
| T022 | Staging environment + auto-deploy from develop branch | Foundation | devops | Must | M0: Foundation | T021 | M2 → M3 | W3 | `feature/T022-staging-environment-auto-deploy` |
| T023 | Link items: URL validation + CRUD | FR-05 | backend | Must | M0: Foundation | T014 | M3 → M4 | W3 | `feature/T023-link-items` |
| T024 | Library list / search / filter by subject + rename, move, delete | FR-05 | backend | Must | M0: Foundation | T014 | M3 → M4 | W3 | `feature/T024-library-list-search-filter` |
| T025 | Background text extraction into page chunks (hosted service) (part 1/2) | FR-05 | backend | Should | M0: Foundation | T014, T004 | M3 → M4 | W3 | `feature/T025-background-text-extraction-into-p1` |
| T026 | React app shell: routing, role guards, axios + token refresh, Zustand store | Foundation | frontend | Must | M0: Foundation | T015 | M4 → M5 | W3 | `feature/T026-react-app-shell` |
| T027 | Auth screens: register (role cards), login, forgot password | FR-01 | frontend | Must | M0: Foundation | T026, T020 | M4 → M5 | W3 | `feature/T027-auth-screens` |
| T028 | Study sessions API (timer / manual validation) + weekly goals | FR-03 | backend | Must | M0: Foundation | — | M5 → M1 | W3 | `feature/T028-study-sessions-api-timer` |
| T029 | Personal focus timer UI + manual session entry + weekly goal form | FR-03 | frontend | Must | M0: Foundation | T028, T026 | M5 → M1 | W3 | `feature/T029-personal-focus-timer-ui` |
| T030 | Role policies (Student / Guardian / Admin / ViewStudent) + rate limiting on auth endpoints | FR-01 | backend | Must | MVP-1: Core | T020 | M1 → M2 | W4 | `feature/T030-role-policies-student-guardian` |
| T031 | Password reset by email (MailKit SMTP, single-use tokens); email verification postponed | FR-01 | backend | Must | MVP-1: Core | T020 | M1 → M2 | W4 | `feature/T031-password-reset-by-email` |
| T032 | Guardian linking API: invite code/link, accept, revoke (atomic update, 7-day expiry, max 5 pending) (part 1/2) | FR-01 | backend | Must | MVP-1: Core | T030 | M1 → M2 | W4 | `feature/T032-guardian-linking-api-p1` |
| T033 | SignalR hub: join / leave, members list, JWT auth via query token, groups (part 1/2) | FR-02 | backend | Must | MVP-1: Core | T011 | M2 → M3 | W4 | `feature/T033-signalr-hub-p1` |
| T034 | SignalR hub: join / leave, members list, JWT auth via query token, groups (part 2/2) | FR-02 | backend | Must | MVP-1: Core | T033 | M2 → M3 | W4 | `feature/T034-signalr-hub-p2` |
| T035 | Background text extraction into page chunks (hosted service) (part 2/2) | FR-05 | backend | Should | MVP-1: Core | T025 | M3 → M4 | W4 | `feature/T035-background-text-extraction-into-p2` |
| T036 | Full-text index on chunks + ranked search query | FR-05 | backend, database | Should | MVP-1: Core | T035 | M3 → M4 | W4 | `feature/T036-full-text-index-on` |
| T037 | Wireframes: guardian + admin screens, quick usability check with 3 students | Foundation | frontend | Must | MVP-1: Core | T016 | M4 → M5 | W4 | `feature/T037-wireframes` |
| T038 | Tasks API: CRUD, move between columns, positions, done_at | FR-04 | backend | Must | MVP-1: Core | — | M4 → M5 | W4 | `feature/T038-tasks-api` |
| T039 | Kanban UI: 3 columns, quick-add, drag and drop, optimistic updates (part 1/2) | FR-04 | frontend | Must | MVP-1: Core | T038, T026 | M4 → M5 | W4 | `feature/T039-kanban-ui-p1` |
| T040 | Progress aggregates: per subject / day, time zones, streak, goal percent (part 1/2) | FR-03 | backend | Must | MVP-1: Core | T028 | M5 → M1 | W4 | `feature/T040-progress-aggregates-p1` |
| T041 | Progress aggregates: per subject / day, time zones, streak, goal percent (part 2/2) | FR-03 | backend | Must | MVP-1: Core | T040 | M5 → M1 | W4 | `feature/T041-progress-aggregates-p2` |
| T042 | Guardian linking API: invite code/link, accept, revoke (atomic update, 7-day expiry, max 5 pending) (part 2/2) | FR-01 | backend | Must | MVP-1: Core | T032 | M1 → M2 | W5 | `feature/T042-guardian-linking-api-p2` |
| T043 | My Guardians screen + Invite modal (code, link, copy, WhatsApp share) | FR-01 | frontend | Must | MVP-1: Core | T042, T027 | M1 → M2 | W5 | `feature/T043-my-guardians-screen-invite` |
| T044 | Room chat: keep last 50 messages, rate limit, profanity filter, report message | FR-02 | backend | Should | MVP-1: Core | T034 | M2 → M3 | W5 | `feature/T044-room-chat` |
| T045 | Synced Pomodoro timer: server end time, host controls, auto-log sessions at block end (part 1/2) | FR-02 | backend | Should | MVP-1: Core | T034, T028 | M2 → M3 | W5 | `feature/T045-synced-pomodoro-timer-p1` |
| T046 | Library UI: upload, list, subject filter, rename / move, download, delete (part 1/2) | FR-05 | frontend | Must | MVP-1: Core | T024, T026 | M3 → M4 | W5 | `feature/T046-library-ui-p1` |
| T047 | Library UI: upload, list, subject filter, rename / move, download, delete (part 2/2) | FR-05 | frontend | Must | MVP-1: Core | T046 | M3 → M4 | W5 | `feature/T047-library-ui-p2` |
| T048 | Kanban UI: 3 columns, quick-add, drag and drop, optimistic updates (part 2/2) | FR-04 | frontend | Must | MVP-1: Core | T039 | M4 → M5 | W5 | `feature/T048-kanban-ui-p2` |
| T049 | Kanban on mobile: tabs, Move menu, edit / delete / reopen | FR-04 | frontend | Must | MVP-1: Core | T048 | M4 → M5 | W5 | `feature/T049-kanban-on-mobile` |
| T050 | Guardian progress endpoint (ViewStudent policy, statistics only) | FR-03 | backend | Must | MVP-1: Core | T041, T030 | M5 → M1 | W5 | `feature/T050-guardian-progress-endpoint-viewstudent` |
| T051 | Gamification service: points, daily cap, levels, unique point events | FR-09 | backend | Should | MVP-1: Core | T028, T038 | M5 → M1 | W5 | `feature/T051-gamification-service` |
| T052 | Admin API: subjects CRUD, user activate/deactivate, aggregate stats | FR-11 | backend | Should | Backend complete + production live | T030 | M1 → M2 | W6 | `feature/T052-admin-api` |
| T053 | Moderation queue: message reports, delete message, ban from rooms (API + UI) | FR-11 | backend, frontend | Must | Backend complete + production live | T052, T044 | M1 → M2 | W6 | `feature/T053-moderation-queue` |
| T054 | Synced Pomodoro timer: server end time, host controls, auto-log sessions at block end (part 2/2) | FR-02 | backend | Should | Backend complete + production live | T045 | M2 → M3 | W6 | `feature/T054-synced-pomodoro-timer-p2` |
| T055 | Rooms list UI: subject chips, search, create-room modal | FR-02 | frontend | Must | Backend complete + production live | T011, T026 | M2 → M3 | W6 | `feature/T055-rooms-list-ui` |
| T056 | Retrieval: top-5 chunks + prompt builder (history, numbered sources) (part 1/2) | FR-06 | backend | Should | Backend complete + production live | T036 | M3 → M4 | W6 | `feature/T056-retrieval-p1` |
| T057 | Retrieval: top-5 chunks + prompt builder (history, numbered sources) (part 2/2) | FR-06 | backend | Should | Backend complete + production live | T056 | M3 → M4 | W6 | `feature/T057-retrieval-p2` |
| T058 | Student dashboard: today, streak / points, goal ring, charts, deadlines, live rooms (part 1/2) | FR-07 | frontend | Must | Backend complete + production live | T041, T051 | M4 → M5 | W6 | `feature/T058-student-dashboard-p1` |
| T059 | Student dashboard: today, streak / points, goal ring, charts, deadlines, live rooms (part 2/2) | FR-07 | frontend | Must | Backend complete + production live | T058 | M4 → M5 | W6 | `feature/T059-student-dashboard-p2` |
| T060 | Gamification UI: points + level bar on the dashboard (no leaderboard) | FR-09 | frontend | Should | Backend complete + production live | T051 | M5 → M1 | W6 | `feature/T060-gamification-ui` |
| T061 | Report builder: period data + template message | FR-08 | backend | Must | Backend complete + production live | T041 | M5 → M1 | W6 | `feature/T061-report-builder` |
| T062 | Integration tests: auth, guardian linking, RBAC (403 / 404 rules) | Testing | — | Must | Backend complete + production live | T042 | M1 → M2 | W7 | `feature/T062-integration-tests` |
| T063 | Notification service: table, REST endpoints, SignalR push | FR-10 | backend | Should | Backend complete + production live | T020 | M1 → M2 | W7 | `feature/T063-notification-service` |
| T064 | Room screen UI: members, timer, chat, host controls, reconnect states (part 1/2) | FR-02 | frontend | Must | Backend complete + production live | T054, T044 | M2 → M3 | W7 | `feature/T064-room-screen-ui-p1` |
| T065 | Production deployment: API container, frontend hosting, Azure SQL, env variables | Release | devops | Must | Backend complete + production live | T022 | M2 → M3 | W7 | `feature/T065-production-deployment` |
| T066 | Gemini client + Groq fallback + daily limit (30 questions) + error codes | FR-06 | backend | Should | Backend complete + production live | T003 | M3 → M4 | W7 | `feature/T066-gemini-client-groq-fallback` |
| T067 | Chat sessions and messages saved with sources (file, page) | FR-06 | backend | Should | Backend complete + production live | T057 | M3 → M4 | W7 | `feature/T067-chat-sessions-and-messages` |
| T068 | Guardian dashboard: student cards, status rules, detail charts (part 1/2) | FR-07 | frontend | Must | Backend complete + production live | T050 | M4 → M5 | W7 | `feature/T068-guardian-dashboard-p1` |
| T069 | Dashboard empty states + 'invite a guardian' prompt | FR-07 | frontend | Must | Backend complete + production live | T059 | M4 → M5 | W7 | `feature/T069-dashboard-empty-states-invite` |
| T070 | Responsive pass on every screen (phones first) | Release | frontend | Must | Backend complete + production live | — | M4 → M5 | W7 | `feature/T070-responsive-pass-on-every` |
| T071 | Hourly scheduler: due schedules, one report per period, secure link page | FR-08 | backend | Must | Backend complete + production live | T061 | M5 → M1 | W7 | `feature/T071-hourly-scheduler` |
| T072 | Delivery: Twilio WhatsApp / wa.me + email fallback + retries | FR-08 | backend | Should | Backend complete + production live | T071 | M5 → M1 | W7 | `feature/T072-delivery` |
| T073 | OWASP review: input validation, CORS, headers, secrets, dependency scan | Security | backend | Must | FEATURE FREEZE | T062 | M1 → M2 | W8 | `feature/T073-owasp-review` |
| T074 | Production hardening: HTTPS, security headers, database backups, rate limits | Release | devops | Must | FEATURE FREEZE | T065 | M1 → M2 | W8 | `feature/T074-production-hardening` |
| T075 | Production secrets and environment variables (pair with M2) | Release | devops | Must | FEATURE FREEZE | T065 | M1 → M2 | W8 | `feature/T075-production-secrets-and-environment` |
| T076 | Room screen UI: members, timer, chat, host controls, reconnect states (part 2/2) | FR-02 | frontend | Must | FEATURE FREEZE | T064 | M2 → M3 | W8 | `feature/T076-room-screen-ui-p2` |
| T077 | Host moderation: remove member, close room, auto-close empty rooms | FR-02 | backend | Must | FEATURE FREEZE | T076 | M2 → M3 | W8 | `feature/T077-host-moderation` |
| T078 | AI chat UI: file selector, source chips, conversation history (part 1/2) | FR-06 | frontend | Should | FEATURE FREEZE | T067, T026 | M3 → M4 | W8 | `feature/T078-ai-chat-ui-p1` |
| T079 | AI chat UI: file selector, source chips, conversation history (part 2/2) | FR-06 | frontend | Should | FEATURE FREEZE | T078 | M3 → M4 | W8 | `feature/T079-ai-chat-ui-p2` |
| T080 | AI quality tuning: prompt tests, 'not in your files' handling, Arabic questions | FR-06 | backend | Should | FEATURE FREEZE | T079 | M3 → M4 | W8 | `feature/T080-ai-quality-tuning` |
| T081 | Guardian dashboard: student cards, status rules, detail charts (part 2/2) | FR-07 | frontend | Must | FEATURE FREEZE | T068 | M4 → M5 | W8 | `feature/T081-guardian-dashboard-p2` |
| T082 | UI polish and consistency review | Release | frontend | Should | FEATURE FREEZE | — | M4 → M5 | W8 | `feature/T082-ui-polish-and-consistency` |
| T083 | Notification bell + list UI (unread count, mark as read) | FR-10 | frontend | Should | FEATURE FREEZE | T063, T026 | M5 → M1 | W8 | `feature/T083-notification-bell-list-ui` |
| T084 | Report page + Encourage / Remind actions (default weekly schedule, no settings page) | FR-08 | backend, frontend | Should | FEATURE FREEZE | T071 | M5 → M1 | W8 | `feature/T084-report-page-encourage-remind` |
| T085 | End-to-end tests of the main demo journeys | Testing | — | Must | FEATURE FREEZE | — | M5 → M1 | W8 | `feature/T085-end-to-end-tests` |
| T086 | Privacy audit: guardian sees statistics only, library files private, signed URLs | Security | backend | Must | FINAL DELIVERY | T073, T047 | M1 → M2 | W9 | `feature/T086-privacy-audit` |
| T087 | Demo support: bug fixes, final data seeding, demo accounts | Release | backend, frontend | Must | FINAL DELIVERY | — | M1 → M2 | W9 | `feature/T087-demo-support` |
| T088 | Demo environment, uptime checks, deployment freeze | Release | devops | Must | FINAL DELIVERY | T065 | M2 → M3 | W9 | `feature/T088-demo-environment-uptime-checks` |
| T089 | Demo dataset: sample PDFs + AI demo script | Release | — | Should | FINAL DELIVERY | T080 | M3 → M4 | W9 | `feature/T089-demo-dataset` |
| T090 | Final UI polish + screenshots for documentation | Release | frontend | Should | FINAL DELIVERY | — | M4 → M5 | W9 | `feature/T090-final-ui-polish-screenshots` |
| T091 | Final docs sync (BRD / FSD / README / API docs), demo script, final presentation | Release | — | Must | FINAL DELIVERY | — | M5 → M1 | W9 | `feature/T091-final-docs-sync-brd` |

## Deferred (not issues)

Cut to meet 5 December, listed in the sheet **Backlog (after 5 Dec)**. Bring back only if the Dashboard shows *On track* at the end of week 6 (Fri 13 Nov), in this order:

1. FR-04 Task analytics API (M4, 10 h)
2. FR-04 Analytics tab UI (M4, 8 h)
3. FR-09 Badges engine + notification hook (M5, 8 h)
4. FR-10 Daily job: tasks due within 24 hours (M2, 8 h)
5. FR-08 Report settings: frequency, channel, phone, self-report toggle (M5, 6 h)
6. Release: Accessibility + Arabic / RTL (M4, 14 h)
7. FR-11 Admin UI (M1, 14 h)
8. FR-08 AI summary service for reports (M3, 8 h)
9. FR-08 Integrate AI summary into reports (M5, 6 h)
10. Testing: real-time load test (M2, 8 h)
11. FR-06 Provider quota monitoring (M3, 6 h)

## Where the sheet and the FSD differ

The sheet is the delivery plan, so it wins for scope. Known differences: email verification is postponed (FSD FR-01 includes it), the report settings page is cut (FSD FR-08), analytics tab and badges are cut (FSD FR-04, FR-09), leaderboard is not planned (FSD FR-09).
