# Development Journal

## PR #00 — Initialize .NET project foundation

- **Date:** 2026-11-27 (Asia/Riyadh)
- **Branch:** `chore/initialize-project`
- **Objective:** establish the independent .NET 10/WinForms/OpenCvSharp project,
  architecture boundaries, automated validation, privacy rules, and truthful
  initial documentation without implementing later application features.
- **Pull request:** [#1](https://github.com/alaamadii/Face-Recognition/pull/1)
- **Commits:**
  - `8a576941dbdc0c1773ca2828c82a4e03a35d70d8` — solution, dependencies, tests, and CI
  - `ee8543011d32dc906ce33f5946297e61d76b5103` — initial technical documentation
  - `283877129442ad191cf3d5ea84e55113af900b5d` — troubleshooting formatting correction

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

The first NuGet restore attempt was blocked by the restricted network sandbox;
the approved restore rerun reached NuGet and succeeded. The first Release build
then found nullable syntax in the hand-authored WinForms designer without an
explicit generated-file nullable context. The component field was changed to
the standard designer declaration, after which format, build, and all tests
passed. A staged whitespace check later identified Markdown hard-break spaces in
the troubleshooting guide; these were removed and the final diff check passed.

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
| `git diff --check` | Passed after correcting Markdown trailing spaces |
| GitHub Actions | Run 45962585004 in progress at journal update time |

### Files

Changes cover root configuration and documentation, `.github/`, `assets/`,
`local-data/`, `src/`, `tests/`, and `docs/`. Use the pull request file list as
the authoritative detailed inventory after the PR is opened.

### Remaining PR #00 tasks

1. Commit and push this journal update.
2. Wait for GitHub Actions and record its confirmed result without merging.
