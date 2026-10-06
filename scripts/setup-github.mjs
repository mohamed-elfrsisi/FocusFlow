#!/usr/bin/env node
// File      : scripts/setup-github.mjs
// Developer : Mohamed El-Farsisi (M2)
// Reviewer  : Yasmeen Ayman (M3)
// Task(s)   : (none)
// Note      : PROPOSED maintainer: repo owner runs it once. Related task: T006 (Ayman Ouf M5).
/**
 * FocusFlow: GitHub repository setup (idempotent, safe to re-run).
 *
 * Creates / updates, in this order:
 *   branches   develop (from main), default branch = develop, branch protection on main + develop
 *   labels     type:*, area:*, priority:* (only the ones the project uses)
 *   milestones the 5 milestones from Tasks.xlsx (Overview sheet)
 *   issues     the 91 tasks from Tasks.xlsx (data in scripts/data/tasks.json), titles "[T019] FR-01: ..."
 *   project    "FocusFlow" GitHub Project with the columns Backlog > Ready > In Progress > Code Review > Testing > Done
 *
 * Prerequisites
 *   - Node 18+  and the GitHub CLI:  https://cli.github.com
 *   - gh auth login
 *   - gh auth refresh -s project      (needed for the Project step)
 *   - The repo must already contain a first commit on `main` (push this folder first, see docs/development-guide.md).
 *
 * Usage
 *   node scripts/setup-github.mjs --dry-run                 # print what would happen, change nothing
 *   node scripts/setup-github.mjs                           # do everything
 *   node scripts/setup-github.mjs --only labels,milestones  # run selected steps: branches,labels,milestones,issues,project
 *   node scripts/setup-github.mjs --repo OWNER/REPO         # default: mohamed-elfrsisi/FocusFlow
 *   node scripts/setup-github.mjs --prune-default-labels    # also delete GitHub's default labels (bug, enhancement, ...)
 *
 * Existing issues are matched by their "[Txxx]" title prefix and are never modified, except that
 * newly created issues get a second pass that turns "Depends on" task IDs into #issue links.
 * Nothing is reported as created unless the gh command succeeded.
 */
import { spawnSync } from "node:child_process";
import { readFileSync, existsSync } from "node:fs";
import { dirname, join } from "node:path";
import { fileURLToPath } from "node:url";

const here = dirname(fileURLToPath(import.meta.url));
const argv = process.argv.slice(2);
const has = (f) => argv.includes(f);
const opt = (f, d) => (argv.includes(f) ? argv[argv.indexOf(f) + 1] : d);

const REPO = opt("--repo", "mohamed-elfrsisi/FocusFlow");
const OWNER = REPO.split("/")[0];
const DRY = has("--dry-run");
const STEPS = (opt("--only", "branches,labels,milestones,issues,project")).split(",").map((s) => s.trim());
const PROJECT_TITLE = "FocusFlow";
const COLUMNS = [
  ["Backlog", "GRAY", "Planned, not ready to start"],
  ["Ready", "BLUE", "Dependencies merged, can start"],
  ["In Progress", "YELLOW", "Branch exists, work started"],
  ["Code Review", "ORANGE", "Pull request open"],
  ["Testing", "PURPLE", "Merged to develop, verifying acceptance criteria"],
  ["Done", "GREEN", "Verified and merged"],
];

const data = JSON.parse(readFileSync(join(here, "data", "tasks.json"), "utf8"));
const teamFile = join(here, "team.json");
const team = existsSync(teamFile) ? JSON.parse(readFileSync(teamFile, "utf8")) : {};

const stats = { created: [], existed: [], failed: [], skipped: [] };
const sleep = (ms) => Atomics.wait(new Int32Array(new SharedArrayBuffer(4)), 0, 0, ms);

/** Run gh. Read-only calls always run (even in --dry-run). Writes are printed only in --dry-run. */
function gh(args, { input, write = false, allowFail = false } = {}) {
  if (DRY && write) {
    console.log(`  [dry-run] gh ${args.join(" ")}`);
    return { ok: true, out: "", dry: true };
  }
  const r = spawnSync("gh", args, { encoding: "utf8", input, maxBuffer: 64 * 1024 * 1024 });
  if (r.error) {
    console.error(`\nCannot run "gh": ${r.error.message}\nInstall GitHub CLI from https://cli.github.com and run "gh auth login".`);
    process.exit(1);
  }
  const ok = r.status === 0;
  if (!ok && !allowFail) console.error(`  ! gh ${args.join(" ")}\n    ${(r.stderr || "").trim()}`);
  return { ok, out: (r.stdout || "").trim(), err: (r.stderr || "").trim() };
}
const json = (res, fallback) => {
  try { return JSON.parse(res.out); } catch { return fallback; }
};
const title = (t) => console.log(`\n== ${t} ==`);
/** Record the result of a write. In --dry-run nothing is reported as done. */
function done(r, key, msg) {
  if (r.dry) return;
  if (r.ok) { if (msg) console.log(`  ${msg}`); stats.created.push(key); } else stats.failed.push(key);
}

// ───────────────────────── preflight ─────────────────────────
title(`Preflight (${REPO}${DRY ? ", DRY RUN" : ""})`);
if (!gh(["auth", "status"], { allowFail: true }).ok) {
  console.error('Not logged in. Run "gh auth login" first.');
  process.exit(1);
}
const repoInfo = json(gh(["repo", "view", REPO, "--json", "nameWithOwner,defaultBranchRef,isEmpty"], { allowFail: true }), null);
if (!repoInfo) {
  console.error(`Repository ${REPO} not found or no access.`);
  process.exit(1);
}
console.log(`  repo OK: ${repoInfo.nameWithOwner}`);

// ───────────────────────── branches ─────────────────────────
function stepBranches() {
  title("Branches");
  const main = gh(["api", `repos/${REPO}/branches/main`], { allowFail: true });
  if (!main.ok) {
    console.error('  ! Branch "main" does not exist yet. Push the first commit to main, then re-run (see docs/development-guide.md).');
    stats.failed.push("branch main missing");
    return;
  }
  const sha = json(main, {}).commit?.sha;
  const dev = gh(["api", `repos/${REPO}/branches/develop`], { allowFail: true });
  if (dev.ok) {
    console.log("  develop already exists");
    stats.existed.push("branch develop");
  } else {
    const r = gh(["api", "-X", "POST", `repos/${REPO}/git/refs`, "-f", "ref=refs/heads/develop", "-f", `sha=${sha}`], { write: true });
    done(r, "branch develop", "created develop from main");
  }
  // default branch = develop: "Closes #N" only auto-closes issues when the PR merges into the default branch
  const d = gh(["repo", "edit", REPO, "--default-branch", "develop", "--delete-branch-on-merge"], { write: true });
  done(d, "default branch", "default branch = develop, delete-branch-on-merge on");

  const protection = JSON.stringify({
    required_status_checks: { strict: false, contexts: ["backend", "frontend"] },
    enforce_admins: false,
    required_pull_request_reviews: { required_approving_review_count: 1, dismiss_stale_reviews: true },
    restrictions: null,
    allow_force_pushes: false,
    allow_deletions: false,
  });
  for (const b of ["main", "develop"]) {
    const p = gh(["api", "-X", "PUT", `repos/${REPO}/branches/${b}/protection`, "--input", "-"], { input: protection, write: true });
    done(p, `protection ${b}`, `protection set on ${b}`);
  }
}

// ───────────────────────── labels ─────────────────────────
const LABELS = [
  ["type:feature", "0E8A16", "New functionality from the FSD"],
  ["type:bug", "D73A4A", "Something does not behave as specified"],
  ["type:task", "5319E7", "Setup, infrastructure, research or design work"],
  ["type:documentation", "0075CA", "Documentation work"],
  ["type:test", "FBCA04", "Tests and QA"],
  ["area:frontend", "C2E0C6", "React client"],
  ["area:backend", "BFD4F2", "ASP.NET Core API, SignalR, jobs"],
  ["area:database", "D4C5F9", "SQL Server schema, migrations, search"],
  ["area:devops", "F9D0C4", "CI/CD, Docker, deployment"],
  ["area:architecture", "FEF2C0", "Structure, standards, cross-cutting design"],
  ["priority:high", "B60205", "Must (MoSCoW) in Tasks.xlsx"],
  ["priority:medium", "FF9F1C", "Should (MoSCoW) in Tasks.xlsx"],
];
const GITHUB_DEFAULT_LABELS = ["bug", "documentation", "duplicate", "enhancement", "good first issue", "help wanted", "invalid", "question", "wontfix"];
function stepLabels() {
  title("Labels");
  for (const [name, color, description] of LABELS) {
    const r = gh(["label", "create", name, "--repo", REPO, "--color", color, "--description", description, "--force"], { write: true });
    done(r, `label ${name}`, name);
  }
  if (has("--prune-default-labels")) {
    for (const n of GITHUB_DEFAULT_LABELS) {
      const r = gh(["label", "delete", n, "--repo", REPO, "--yes"], { write: true, allowFail: true });
      if (r.ok && !r.dry) console.log(`  deleted default label ${n}`);
    }
  }
}

// ───────────────────────── milestones ─────────────────────────
function stepMilestones() {
  title("Milestones");
  const existing = json(gh(["api", `repos/${REPO}/milestones?state=all&per_page=100`], { allowFail: true }), []);
  for (const m of data.milestones) {
    const due = `${m.due}T23:59:59Z`;
    const found = existing.find((e) => e.title === m.title);
    if (found) {
      const r = gh(["api", "-X", "PATCH", `repos/${REPO}/milestones/${found.number}`, "-f", `description=${m.description}`, "-f", `due_on=${due}`], { write: true });
      if (!r.dry) { if (r.ok) { console.log(`  ${m.title} (exists, updated)`); stats.existed.push(`milestone ${m.title}`); } else stats.failed.push(`milestone ${m.title}`); }
    } else {
      const r = gh(["api", "-X", "POST", `repos/${REPO}/milestones`, "-f", `title=${m.title}`, "-f", `description=${m.description}`, "-f", `due_on=${due}`], { write: true });
      done(r, `milestone ${m.title}`, m.title);
    }
  }
}

// ───────────────────────── issues ─────────────────────────
const byId = Object.fromEntries(data.tasks.map((t) => [t.id, t]));

function issueBody(t, numbers) {
  const dep = t.deps.length
    ? t.deps.map((d) => (numbers[d] ? `#${numbers[d]} (${d})` : d)).join(", ")
    : "None";
  const lines = [
    `**Task ID:** ${t.id}  |  **FSD module:** ${t.module}  |  **Source:** Tasks.xlsx > Task Board, ${t.fsd_section}`,
    "",
    "## Description",
    t.title,
    "",
    "| Field | Value |",
    "|---|---|",
    `| Week | W${t.week} (${t.dates}) |`,
    `| Milestone | ${t.milestone} |`,
    `| Layer | ${t.layer} |`,
    `| Priority | ${t.priority} (MoSCoW) |`,
    `| Difficulty (1-5) | ${t.diff} |`,
    `| Estimated hours | ${t.hours} |`,
    `| Owner | ${t.owner} ${t.owner_name} |`,
    `| Reviewer | ${t.reviewer} ${t.reviewer_name} |`,
    `| Depends on | ${dep} |`,
    `| Suggested branch | \`${t.branch}\` |`,
    "",
    "## Acceptance criteria",
  ];
  if (t.ac.length) {
    lines.push(`From the FSD module ${t.module}. They apply to the module as a whole; this task delivers its part of them.`, "");
    for (const a of t.ac) lines.push(`- [ ] ${a}`);
  } else {
    lines.push(`The source documents define no task-level acceptance criteria for this task (${t.module}). Done = the task description above is delivered and the Definition of Done is met (FSD §15.1: criteria met, tests added, no lint errors, API documented, reviewed, deployed to staging).`);
  }
  lines.push("", "## Workflow", `1. \`git checkout develop && git pull origin develop\``, `2. \`git checkout -b ${t.branch}\``, `3. Pull Request \`${t.branch}\` → \`develop\`, include \`Closes #<this issue>\`.`);
  return lines.join("\n");
}

function stepIssues() {
  title(`Issues (${data.tasks.length} tasks)`);
  const list = json(gh(["issue", "list", "--repo", REPO, "--state", "all", "--limit", "1000", "--json", "number,title,url"], { allowFail: true }), []);
  const numbers = {};
  const urls = {};
  for (const i of list) {
    const m = i.title.match(/^\[(T\d{3})\]/);
    if (m) { numbers[m[1]] = i.number; urls[m[1]] = i.url; }
  }
  const fresh = [];
  for (const t of data.tasks) {
    if (numbers[t.id]) { console.log(`  ${t.id} exists (#${numbers[t.id]})`); stats.existed.push(`issue ${t.id}`); continue; }
    const args = ["issue", "create", "--repo", REPO, "--title", t.issue_title, "--body-file", "-", "--milestone", t.milestone];
    for (const l of t.labels) args.push("--label", l);
    const assignee = team[t.owner];
    if (assignee) args.push("--assignee", assignee);
    const r = gh(args, { input: issueBody(t, numbers), write: true });
    if (!r.ok) { stats.failed.push(`issue ${t.id}`); continue; }
    if (r.dry) { numbers[t.id] = 0; continue; }
    const url = r.out.split("\n").pop().trim();
    const n = Number(url.split("/").pop());
    if (!Number.isInteger(n)) { console.error(`  ! could not read the issue number for ${t.id}: ${r.out}`); stats.failed.push(`issue ${t.id}`); continue; }
    numbers[t.id] = n; urls[t.id] = url; fresh.push(t);
    console.log(`  ${t.id} -> #${n}`);
    stats.created.push(`issue ${t.id}`);
    sleep(1500); // stay under GitHub's secondary rate limit for content creation
  }
  // second pass: replace dependency IDs with #links in the issues created by this run
  for (const t of fresh.filter((x) => x.deps.length)) {
    const r = gh(["issue", "edit", String(numbers[t.id]), "--repo", REPO, "--body-file", "-"], { input: issueBody(t, numbers), write: true });
    if (!r.ok) console.error(`  ! could not link dependencies in #${numbers[t.id]} (${t.id})`);
    sleep(700);
  }
  return { numbers, urls };
}

// ───────────────────────── project ─────────────────────────
function stepProject(issueUrls) {
  title("GitHub Project");
  const projects = json(gh(["project", "list", "--owner", OWNER, "--format", "json", "--limit", "100"], { allowFail: true }), { projects: [] });
  let proj = (projects.projects || []).find((p) => p.title === PROJECT_TITLE);
  if (proj) { console.log(`  project exists: #${proj.number}`); stats.existed.push("project"); }
  else {
    const c = gh(["project", "create", "--owner", OWNER, "--title", PROJECT_TITLE, "--format", "json"], { write: true });
    if (c.dry) { console.log("  (project would be created; the remaining project steps need it, skipping in dry run)"); return; }
    proj = json(c, null);
    if (!proj) { stats.failed.push("project (did you run: gh auth refresh -s project ?)"); return; }
    console.log(`  created project #${proj.number}`); stats.created.push("project");
  }
  const N = String(proj.number);
  gh(["project", "link", N, "--owner", OWNER, "--repo", REPO], { write: true, allowFail: true });

  // Status field -> workflow columns
  let fields = json(gh(["project", "field-list", N, "--owner", OWNER, "--format", "json"]), { fields: [] }).fields;
  let status = fields.find((f) => f.name === "Status");
  if (!status) { stats.failed.push("project Status field not found"); return; }
  const want = COLUMNS.map((c) => c[0]);
  const have = (status.options || []).map((o) => o.name);
  if (want.join("|") !== have.join("|")) {
    const q = `mutation($f:ID!,$o:[ProjectV2SingleSelectFieldOptionInput!]!){updateProjectV2Field(input:{fieldId:$f,singleSelectOptions:$o}){projectV2Field{... on ProjectV2SingleSelectField{id}}}}`;
    const body = JSON.stringify({ query: q, variables: { f: status.id, o: COLUMNS.map(([name, color, description]) => ({ name, color, description })) } });
    const u = gh(["api", "graphql", "--input", "-"], { input: body, write: true });
    if (!u.dry) { if (u.ok) console.log(`  columns: ${want.join(" > ")}`); else stats.failed.push("project columns (set them by hand: Project > Status field > edit options)"); }
    fields = json(gh(["project", "field-list", N, "--owner", OWNER, "--format", "json"]), { fields: [] }).fields;
    status = fields.find((f) => f.name === "Status");
  }
  const backlog = (status.options || []).find((o) => o.name === "Backlog");
  const view = json(gh(["project", "view", N, "--owner", OWNER, "--format", "json"]), {});
  const items = json(gh(["project", "item-list", N, "--owner", OWNER, "--format", "json", "--limit", "500"]), { items: [] }).items || [];
  const inProject = new Map(items.map((i) => [i.content?.url, i]));
  let added = 0;
  for (const t of data.tasks) {
    const url = issueUrls[t.id];
    if (!url) continue;
    let item = inProject.get(url);
    if (!item) {
      const a = gh(["project", "item-add", N, "--owner", OWNER, "--url", url, "--format", "json"], { write: true });
      item = json(a, null);
      if (!item) { stats.failed.push(`project item ${t.id}`); continue; }
      added++;
    }
    if (backlog && !item.status && item.id) {
      gh(["project", "item-edit", "--id", item.id, "--project-id", view.id, "--field-id", status.id, "--single-select-option-id", backlog.id], { write: true, allowFail: true });
    }
  }
  if (!DRY) { console.log(`  ${added} issues added to the project (new ones start in Backlog)`); stats.created.push(`${added} project items`); }
}

// ───────────────────────── run ─────────────────────────
let issueResult = { urls: {} };
if (STEPS.includes("branches")) stepBranches();
if (STEPS.includes("labels")) stepLabels();
if (STEPS.includes("milestones")) stepMilestones();
if (STEPS.includes("issues")) issueResult = stepIssues();
if (STEPS.includes("project")) {
  if (!STEPS.includes("issues")) {
    const list = json(gh(["issue", "list", "--repo", REPO, "--state", "all", "--limit", "1000", "--json", "number,title,url"], { allowFail: true }), []);
    for (const i of list) { const m = i.title.match(/^\[(T\d{3})\]/); if (m) issueResult.urls[m[1]] = i.url; }
  }
  stepProject(issueResult.urls);
}

title("Summary");
console.log(`  created/updated: ${stats.created.length}`);
console.log(`  already existed: ${stats.existed.length}`);
console.log(`  failed         : ${stats.failed.length}`);
for (const f of stats.failed) console.log(`    - ${f}`);
if (DRY) console.log("\nDRY RUN: nothing was changed.");
else console.log("\nVerify in GitHub: Settings > Branches, Issues > Labels / Milestones, Projects. Then check the counts: 12 labels, 5 milestones, 91 issues.");
process.exit(stats.failed.length ? 1 : 0);
