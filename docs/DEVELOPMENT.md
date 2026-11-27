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
  - `beeca871cee3812e4a5720f0dc8546e6a941381f` — pull-request development evidence
  - `26b582464693630a952a3f93aa67965ad565697c` — successful CI evidence

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
| GitHub Actions | Passed: [final run 45962876354](https://github.com/alaamadii/Face-Recognition/actions/runs/45962876354), Windows x64 job completed in 1 minute |

### Files

Changes cover root configuration and documentation, `.github/`, `assets/`,
`local-data/`, `src/`, `tests/`, and `docs/`. Use the pull request file list as
the authoritative detailed inventory after the PR is opened.

### Completion

PR #1 passed final head CI and was merged into `main` on 2026-11-27 as commit
`000dde2b2ef737f28adc5972560f241e2cab4e5e` after user review.

## PR #01 — Build WinForms application interface

- **Date:** 2026-11-27 (Asia/Riyadh)
- **Branch:** `feat/application-ui`
- **Objective:** implement a responsive, accessible, truthful WinForms
  interface without camera, image decoding, detection, training, or recognition.
- **Pull request:** [#2](https://github.com/alaamadii/Face-Recognition/pull/2)
- **Commits:**
  - `adb84f2b3e3354059506f475c92acf7e78c537e7` — responsive WinForms interface and capability mapping
  - `cd7e0e5caf3e0c5b6142d878158b3fec6034e4d5` — deterministic capability tests
  - `169529f068f9d66c467a8d685468c41e7d8f0006` — UI phase documentation

### Implemented changes

- Added Turkish navigation for Tanıma, Veri Seti, and Ayarlar.
- Added a responsive recognition workspace using built-in WinForms layout
  containers and font-based autoscaling.
- Added the required PictureBox, Start/Stop/Select/Clear controls, result and
  status cards, feedback area, and local-processing/privacy notice.
- Added truthful Dataset and Settings planned-feature views.
- Added `ApplicationCapabilities` in Core to map application modes to valid
  commands without a WinForms dependency.
- Added five deterministic capability-mapping test cases, increasing Core tests
  from five to ten.
- Added a reusable App-owned `MetricCard` and a small color palette; no external
  UI dependency was introduced.
- Added explicit PictureBox image replacement/clear/close disposal.
- Deferred Start and Select handlers show informational messages and do not
  simulate camera, file, model, detection, or recognition success.

### Problems and solutions

The first build reported WinForms analyzer `WFO1000` for a public custom-control
property without designer serialization metadata. The runtime-only property was
replaced by an update method, avoiding a warning suppression and design-time
contract that the control did not need.

The first visual capture appeared to clip the status column because the capture
process used DPI-virtualized coordinates. A DPI-aware window-only capture showed
the real layout. During that review, the form was changed from an explicit DPI
baseline to the standard font autoscaling baseline and compact status wording
was used. The final interface was checked at its minimum and a larger size.
Temporary captures used for inspection were deleted and were never tracked.

### Commands and actual results

| Command/check | Result |
|---|---|
| `dotnet restore FaceRecognition.slnx` | Passed; all projects up to date |
| `dotnet format ... --verify-no-changes --no-restore` | Passed; exit code 0 |
| `dotnet build ... --configuration Release --no-restore` | Passed; 0 warnings, 0 errors |
| `dotnet test ... --configuration Release --no-build` | Passed; Core 10/10 and native integration 1/1 |
| Release app launch | Passed; window opened responsive with expected title |
| Navigation | Passed for Recognition, Dataset, and Settings |
| Idle actions | Start/Select enabled; Stop/Clear disabled |
| Keyboard order | Tanıma, Veri Seti, Ayarlar, Kamerayı Başlat, Görüntü Seç |
| Resize | Passed at minimum and larger sizes; process remained responsive |
| Clean close | Passed; process exited after `CloseMainWindow` |
| `git diff --check` | Passed; no whitespace errors |
| GitHub Actions | Passed: [run 45974776845](https://github.com/alaamadii/Face-Recognition/actions/runs/45974776845), Windows x64 job completed successfully |

### Scope deliberately deferred

Camera hardware and capture, OpenFileDialog/image decoding, Haar detection,
rectangles, sample storage, LBPH training, predictions, model deletion, and
authentic screenshots remain planned. No hardware camera test was run.

### Remaining PR #01 tasks

1. Commit and push the confirmed CI evidence.
2. Verify the evidence-only commit's final head CI.
3. Stop for user review without merging PR #2.
