<!--
File      : server/tests/README.md
Developer : Ayman Ouf (M5)
Reviewer  : Omar Emam (M1)
Task(s)   : T017, T062, T085
-->
# server/tests

Test strategy and QA checklist: **T017** (Ayman Ouf M5). Integration tests (auth, guardian linking, RBAC): **T062** (Omar Emam M1). End-to-end demo journeys: **T085** (M5).
Targets (FSD §13): xUnit unit tests ~70% of service code; xUnit + WebApplicationFactory for every Must endpoint (success, validation, 401, 403); Playwright for 5 critical journeys.
Add test projects to `server/FocusFlow.sln` so CI picks them up (as `server/tests/FocusFlow.Api.Tests/`).
