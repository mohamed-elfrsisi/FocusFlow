<!--
File      : docs/file-ownership.md
Developer : Ayman Ouf (M5)
Reviewer  : Omar Emam (M1)
Task(s)   : T006, T018
-->
# File Ownership

Every file with a header comment names its **Developer** and **Reviewer**. Names come from `Tasks.xlsx` (Owner / Reviewer columns): the developer of the task that builds the file, and the reviewer fixed by the sheet for that developer (M1→M2, M2→M3, M3→M4, M4→M5, M5→M1). Files that no task in the sheet covers are marked **PROPOSED** and need a decision from the Tech Lead.

| Code | Developer | Role (Tasks.xlsx) | Files |
|---|---|---|---|
| M1 | Omar Emam | Tech Lead: Auth, Security, Admin, Notifications API | 55 |
| M2 | Mohamed El-Farsisi | Real-time and DevOps | 22 |
| M3 | Yasmeen Ayman | AI and Content | 31 |
| M4 | Saeid El-Kholy | Frontend and UX Lead | 39 |
| M5 | Ayman Ouf | Data, Reports and QA / PM | 34 |

## Proposed owners (no task in the sheet)

| File | Proposed developer | Why |
|---|---|---|
| `.gitignore` | Mohamed El-Farsisi (M2) | no task in Tasks.xlsx covers .gitignore; repo owner / DevOps assumed. |
| `scripts/setup-github.mjs` | Mohamed El-Farsisi (M2) | repo owner runs it once. Related task: T006 (Ayman Ouf M5). |
| `server/src/FocusFlow.Api/Features/Dashboard/DashboardController.cs` | Saeid El-Kholy (M4) | Tasks.xlsx has no backend task for the dashboard API (T058, T059, T068, T081 are UI). Confirm with the Tech Lead. |
| `server/src/FocusFlow.Api/Features/Dashboard/DashboardDtos.cs` | Saeid El-Kholy (M4) | Tasks.xlsx has no backend task for the dashboard API (T058, T059, T068, T081 are UI). Confirm with the Tech Lead. |
| `server/src/FocusFlow.Api/Features/Dashboard/DashboardService.cs` | Saeid El-Kholy (M4) | Tasks.xlsx has no backend task for the dashboard API (T058, T059, T068, T081 are UI). Confirm with the Tech Lead. |
| `server/src/FocusFlow.Api/Jobs/NightlyAggregatesJob.cs` | Ayman Ouf (M5) | Tasks.xlsx has no task for the nightly aggregate job (FSD §2.1). Confirm with the Tech Lead. |

## Omar Emam (M1)

| File | Task(s) |
|---|---|
| `client/src/features/auth/InviteModal.jsx` | T043 |
| `client/src/features/auth/MyGuardians.jsx` | T043 |
| `client/src/pages/AdminPage.jsx` | T053 |
| `client/src/pages/GuardiansPage.jsx` | T043 |
| `docs/architecture.md` | T008 |
| `server/db/schema.sql` | T009 |
| `server/src/FocusFlow.Api/Common/AppException.cs` | T008 |
| `server/src/FocusFlow.Api/Common/DbExtensions.cs` | T008 |
| `server/src/FocusFlow.Api/Common/ErrorHandler.cs` | T008 |
| `server/src/FocusFlow.Api/Common/Pagination.cs` | T008 |
| `server/src/FocusFlow.Api/Common/UserExtensions.cs` | T008 |
| `server/src/FocusFlow.Api/Common/ViewStudentHandler.cs` | T030 |
| `server/src/FocusFlow.Api/Data/Entities/Badge.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/GamificationProfile.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/GuardianAction.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/GuardianLink.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/MessageReport.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/Notification.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/PointEvent.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/Report.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/ReportDelivery.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/ReportSetting.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/Room.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/RoomMember.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/RoomMessage.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/StudentBadge.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/StudySession.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/Subject.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/TaskItem.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/User.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Entities/WeeklyGoal.cs` | T009 |
| `server/src/FocusFlow.Api/Data/FocusFlowDbContext.cs` | T009 |
| `server/src/FocusFlow.Api/Data/Migrations/.gitkeep` | T009 |
| `server/src/FocusFlow.Api/Data/Seed.cs` | T009 |
| `server/src/FocusFlow.Api/Features/Admin/AdminController.cs` | T052, T053 |
| `server/src/FocusFlow.Api/Features/Admin/AdminDtos.cs` | T052 |
| `server/src/FocusFlow.Api/Features/Admin/AdminService.cs` | T052, T053 |
| `server/src/FocusFlow.Api/Features/Admin/AdminValidators.cs` | T052 |
| `server/src/FocusFlow.Api/Features/Auth/AuthController.cs` | T019, T020 |
| `server/src/FocusFlow.Api/Features/Auth/AuthDtos.cs` | T019 |
| `server/src/FocusFlow.Api/Features/Auth/AuthService.cs` | T019, T020, T031 |
| `server/src/FocusFlow.Api/Features/Auth/AuthValidators.cs` | T019 |
| `server/src/FocusFlow.Api/Features/Guardians/GuardiansController.cs` | T032, T042 |
| `server/src/FocusFlow.Api/Features/Guardians/GuardiansDtos.cs` | T032 |
| `server/src/FocusFlow.Api/Features/Guardians/GuardiansService.cs` | T032, T042 |
| `server/src/FocusFlow.Api/Features/Guardians/GuardiansValidators.cs` | T032 |
| `server/src/FocusFlow.Api/Features/Notifications/NotificationsController.cs` | T063 |
| `server/src/FocusFlow.Api/Features/Notifications/NotificationsDtos.cs` | T063 |
| `server/src/FocusFlow.Api/Features/Notifications/NotificationsService.cs` | T063 |
| `server/src/FocusFlow.Api/Infrastructure/Mail/IEmailSender.cs` | T031 |
| `server/src/FocusFlow.Api/Infrastructure/Mail/MailKitEmailSender.cs` | T031 |
| `server/src/FocusFlow.Api/Program.cs` | T008 |
| `server/src/FocusFlow.Api/appsettings.json` | T008 |
| `server/tests/FocusFlow.Api.Tests/ApiFactory.cs` | T062 |
| `server/tests/FocusFlow.Api.Tests/GuardianAccessTests.cs` | T062 |

## Mohamed El-Farsisi (M2)

| File | Task(s) |
|---|---|
| `.env.example` | T010 |
| `.github/workflows/ci.yml` | T021 |
| `.gitignore` | (none) |
| `client/src/features/rooms/CreateRoomModal.jsx` | T055 |
| `client/src/features/rooms/RoomScreen.jsx` | T064, T076 |
| `client/src/features/rooms/RoomsList.jsx` | T055 |
| `client/src/features/rooms/useRoomHub.js` | T064 |
| `client/src/pages/RoomPage.jsx` | T064 |
| `client/src/pages/RoomsPage.jsx` | T055 |
| `docker-compose.yml` | T010 |
| `docs/development-guide.md` | T010, T008 |
| `scripts/setup-github.mjs` | (none) |
| `server/db/docker/Dockerfile` | T010 |
| `server/src/FocusFlow.Api/Features/Rooms/ChatRateLimiter.cs` | T044 |
| `server/src/FocusFlow.Api/Features/Rooms/Profanity.cs` | T044 |
| `server/src/FocusFlow.Api/Features/Rooms/RoomsController.cs` | T011, T077 |
| `server/src/FocusFlow.Api/Features/Rooms/RoomsDtos.cs` | T011 |
| `server/src/FocusFlow.Api/Features/Rooms/RoomsDtos_Hub.cs` | T033 |
| `server/src/FocusFlow.Api/Features/Rooms/RoomsService.cs` | T011, T077, T044 |
| `server/src/FocusFlow.Api/Features/Rooms/RoomsValidators.cs` | T011 |
| `server/src/FocusFlow.Api/Hubs/RoomTimerService.cs` | T045, T054 |
| `server/src/FocusFlow.Api/Hubs/RoomsHub.cs` | T033, T034 |

## Yasmeen Ayman (M3)

| File | Task(s) |
|---|---|
| `client/src/features/ai/AiChat.jsx` | T078, T079 |
| `client/src/features/ai/FilePicker.jsx` | T078 |
| `client/src/features/ai/SourceChips.jsx` | T079 |
| `client/src/features/library/LibraryView.jsx` | T046, T047 |
| `client/src/features/library/LinkForm.jsx` | T047 |
| `client/src/features/library/UploadArea.jsx` | T046 |
| `client/src/pages/AiChatPage.jsx` | T078 |
| `client/src/pages/LibraryPage.jsx` | T046 |
| `docs/environment.example` | T012, T075 |
| `server/src/FocusFlow.Api/Data/Entities/ChatMessage.cs` | T067 |
| `server/src/FocusFlow.Api/Data/Entities/ChatSession.cs` | T067 |
| `server/src/FocusFlow.Api/Data/Entities/LibraryChunk.cs` | T014 |
| `server/src/FocusFlow.Api/Data/Entities/LibraryItem.cs` | T014 |
| `server/src/FocusFlow.Api/Features/Ai/AiController.cs` | T066, T067 |
| `server/src/FocusFlow.Api/Features/Ai/AiDtos.cs` | T066 |
| `server/src/FocusFlow.Api/Features/Ai/AiService.cs` | T056, T057, T066 |
| `server/src/FocusFlow.Api/Features/Ai/AiValidators.cs` | T066 |
| `server/src/FocusFlow.Api/Features/Ai/ChatStore.cs` | T067 |
| `server/src/FocusFlow.Api/Features/Ai/Retrieval.cs` | T036, T056 |
| `server/src/FocusFlow.Api/Features/Library/IndexQueue.cs` | T025 |
| `server/src/FocusFlow.Api/Features/Library/IndexWorker.cs` | T025 |
| `server/src/FocusFlow.Api/Features/Library/LibraryController.cs` | T014, T023, T024 |
| `server/src/FocusFlow.Api/Features/Library/LibraryDtos.cs` | T014 |
| `server/src/FocusFlow.Api/Features/Library/LibraryIndexer.cs` | T025, T035 |
| `server/src/FocusFlow.Api/Features/Library/LibraryService.cs` | T014, T023, T024 |
| `server/src/FocusFlow.Api/Features/Library/LibraryValidators.cs` | T014, T023 |
| `server/src/FocusFlow.Api/Infrastructure/Ai/GeminiClient.cs` | T066 |
| `server/src/FocusFlow.Api/Infrastructure/Ai/GroqClient.cs` | T066 |
| `server/src/FocusFlow.Api/Infrastructure/Storage/IStorage.cs` | T013 |
| `server/src/FocusFlow.Api/Infrastructure/Storage/LocalDiskStorage.cs` | T013 |
| `server/src/FocusFlow.Api/Infrastructure/Storage/SupabaseStorage.cs` | T013 |

## Saeid El-Kholy (M4)

| File | Task(s) |
|---|---|
| `client/src/api/http.js` | T026 |
| `client/src/components/charts/GoalRing.jsx` | T058, T059 |
| `client/src/components/charts/StudyChart.jsx` | T058, T059 |
| `client/src/components/kanban/.gitkeep` | T039, T048 |
| `client/src/components/layout/AppShell.jsx` | T026 |
| `client/src/components/layout/Sidebar.jsx` | T026 |
| `client/src/components/ui/.gitkeep` | T015 |
| `client/src/features/auth/ForgotPasswordForm.jsx` | T027 |
| `client/src/features/auth/LoginForm.jsx` | T027 |
| `client/src/features/auth/RegisterForm.jsx` | T027 |
| `client/src/features/dashboard/EmptyStates.jsx` | T069 |
| `client/src/features/dashboard/GuardianDashboard.jsx` | T068, T081 |
| `client/src/features/dashboard/StudentCard.jsx` | T068 |
| `client/src/features/dashboard/StudentDashboard.jsx` | T058, T059 |
| `client/src/features/tasks/Board.jsx` | T039, T048 |
| `client/src/features/tasks/Column.jsx` | T039 |
| `client/src/features/tasks/MoveMenu.jsx` | T049 |
| `client/src/features/tasks/TaskCard.jsx` | T039 |
| `client/src/features/tasks/TaskModal.jsx` | T048 |
| `client/src/features/tasks/useMoveTask.js` | T039 |
| `client/src/hooks/.gitkeep` | T026 |
| `client/src/pages/GuardianDashboardPage.jsx` | T068 |
| `client/src/pages/LoginPage.jsx` | T027 |
| `client/src/pages/RegisterPage.jsx` | T027 |
| `client/src/pages/StudentDashboardPage.jsx` | T058 |
| `client/src/pages/StudentDetailPage.jsx` | T081 |
| `client/src/pages/TasksPage.jsx` | T039 |
| `client/src/routes/AppRoutes.jsx` | T026 |
| `client/src/routes/RequireRole.jsx` | T026 |
| `client/src/store/useAuth.js` | T026 |
| `client/src/styles/index.css` | T015 |
| `client/tailwind.config.js` | T015 |
| `server/src/FocusFlow.Api/Features/Dashboard/DashboardController.cs` | T058 |
| `server/src/FocusFlow.Api/Features/Dashboard/DashboardDtos.cs` | T058 |
| `server/src/FocusFlow.Api/Features/Dashboard/DashboardService.cs` | T058 |
| `server/src/FocusFlow.Api/Features/Tasks/TasksController.cs` | T038 |
| `server/src/FocusFlow.Api/Features/Tasks/TasksDtos.cs` | T038 |
| `server/src/FocusFlow.Api/Features/Tasks/TasksService.cs` | T038 |
| `server/src/FocusFlow.Api/Features/Tasks/TasksValidators.cs` | T038 |

## Ayman Ouf (M5)

| File | Task(s) |
|---|---|
| `.github/ISSUE_TEMPLATE/bug.yml` | T006 |
| `.github/ISSUE_TEMPLATE/config.yml` | T006 |
| `.github/ISSUE_TEMPLATE/feature-task.yml` | T006 |
| `.github/ISSUE_TEMPLATE/technical-task.yml` | T006 |
| `README.md` | T006, T091 |
| `client/src/components/layout/NotificationBell.jsx` | T083 |
| `client/src/features/dashboard/FocusTimer.jsx` | T029 |
| `client/src/features/dashboard/ManualSessionForm.jsx` | T029 |
| `client/src/features/dashboard/WeeklyGoalForm.jsx` | T029 |
| `client/src/features/gamification/PointsLevelBar.jsx` | T060 |
| `client/src/features/reports/GuardianActions.jsx` | T084 |
| `client/src/features/reports/ReportPage.jsx` | T084 |
| `client/src/pages/ProgressPage.jsx` | T029 |
| `client/src/pages/ReportViewPage.jsx` | T084 |
| `docs/branching-strategy.md` | T006 |
| `docs/task-board.md` | T006, T018 |
| `server/src/FocusFlow.Api/Features/Gamification/GamificationController.cs` | T051 |
| `server/src/FocusFlow.Api/Features/Gamification/GamificationDtos.cs` | T051 |
| `server/src/FocusFlow.Api/Features/Gamification/GamificationService.cs` | T051 |
| `server/src/FocusFlow.Api/Features/Progress/ProgressController.cs` | T050 |
| `server/src/FocusFlow.Api/Features/Progress/ProgressDtos.cs` | T040 |
| `server/src/FocusFlow.Api/Features/Progress/ProgressService.cs` | T040, T041, T050 |
| `server/src/FocusFlow.Api/Features/Reports/DeliveryService.cs` | T072 |
| `server/src/FocusFlow.Api/Features/Reports/GuardianActionsController.cs` | T084 |
| `server/src/FocusFlow.Api/Features/Reports/ReportsController.cs` | T071, T084 |
| `server/src/FocusFlow.Api/Features/Reports/ReportsDtos.cs` | T061 |
| `server/src/FocusFlow.Api/Features/Reports/ReportsService.cs` | T061, T071 |
| `server/src/FocusFlow.Api/Features/Sessions/SessionsController.cs` | T028 |
| `server/src/FocusFlow.Api/Features/Sessions/SessionsDtos.cs` | T028 |
| `server/src/FocusFlow.Api/Features/Sessions/SessionsService.cs` | T028 |
| `server/src/FocusFlow.Api/Features/Sessions/SessionsValidators.cs` | T028 |
| `server/src/FocusFlow.Api/Jobs/HourlyReportsJob.cs` | T071 |
| `server/src/FocusFlow.Api/Jobs/NightlyAggregatesJob.cs` | T041 |
| `server/tests/README.md` | T017, T062, T085 |

Generated files that are not hand-written (do not stamp or edit): EF Core `Migrations/*`, `server/db/schema.sql` content, `package-lock.json`.
