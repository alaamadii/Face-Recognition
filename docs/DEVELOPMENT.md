# Development Journal

## PR #00 — Initialize .NET project foundation

- **Date:** 2026-11-27 (Asia/Riyadh)
- **Branch:** `chore/initialize-project`
- **Objective:** establish the independent .NET 10/WinForms/OpenCvSharp project,
  architecture boundaries, automated validation, privacy rules, and truthful
  initial documentation without implementing later application features.
- **Pull request:** Pending creation
- **Commits:** Pending

### Environment inspected

- Windows `10.0.22631`, x64
- .NET SDK `10.0.402`
- Microsoft.WindowsDesktop.App `10.0.13`
- Git `2.53.0.windows.1`
- GitHub account `alaamadii`
- Target repository confirmed public, initially empty, with no default branch

### Repository preparation

The empty repository was cloned into an empty workspace. A minimal `main`
baseline was created and pushed before feature work:

- `5b5fd7f6aeaac452c0a1ec6d04d4b3d2a660061f` —
  `chore: establish repository baseline`

Substantive work then moved to `chore/initialize-project`. A sandbox restriction
initially prevented the local branch ref from being written; rerunning the Git
operation with the required workspace permission created the branch. No project
change was committed to `main`.

### Implemented changes

- Added `FaceRecognition.slnx` with App, Core, Infrastructure, Core Tests, and
  Integration Tests projects.
- Enforced App/Core/Infrastructure dependency direction.
- Added centralized SDK/build/NuGet configuration and Windows x64 targets.
- Pinned full OpenCvSharp4 Windows runtime and xUnit test dependencies.
- Added a blank `MainForm` shell only; no recognition UI or camera behavior.
- Added immutable Core application state and transition tests.
- Added a camera-free native OpenCV/LBPH compatibility test.
- Added Windows GitHub Actions restore, format, Release build, and test workflow.
- Added privacy-safe ignore rules and local/model/screenshot notices.
- Added complete initial requirements, architecture, computer vision, decision,
  testing, privacy, user, troubleshooting, roadmap, and checklist documents.
- Added permanent repository instructions in `AGENTS.md`.

### Scope deliberately deferred

Camera capture, image selection, PictureBox preview, Haar detection, rectangles,
dataset UI, model training, recognition predictions, local-data deletion UI,
settings, hardware checks, and authentic screenshots remain planned for later
phases.

### Commands and actual results

| Command | Result |
|---|---|
| `dotnet --info` | SDK/environment inspection completed; exact output summarized above |
| `dotnet restore FaceRecognition.slnx` | Passed; five projects restored |
| `dotnet format ... --verify-no-changes --no-restore` | Passed; exit code 0 |
| `dotnet build ... --configuration Release --no-restore` | Passed; 0 warnings, 0 errors |
| `dotnet test ... --configuration Release --no-build` | Passed; Core 5/5 and native integration 1/1 |
| `git diff --check` | First check found two Markdown hard-break spaces; corrected, final check pending |
| GitHub Actions | Not yet run |

### Files

Changes cover root configuration and documentation, `.github/`, `assets/`,
`local-data/`, `src/`, `tests/`, and `docs/`. Use the pull request file list as
the authoritative detailed inventory after the PR is opened.

### Remaining PR #00 tasks

1. Complete the final repository diff/status checks.
2. Commit project/configuration and documentation meaningfully.
3. Push the feature branch and open the PR against `main`.
4. Record real commit and PR references here and push the journal update.
5. Wait for GitHub Actions and record its confirmed result without merging.
