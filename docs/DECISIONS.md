# Architecture Decision Records

## ADR-001 — Use layered .NET 10 Windows architecture

- **Status:** Accepted
- **Context:** The assignment requires C#, Windows Forms, OpenCV, OOP, and
  testable organization without recreating a large business system.
- **Alternatives:** single WinForms project; full clean-architecture solution;
  lightweight App/Core/Infrastructure layers.
- **Decision:** Use App, Core, and Infrastructure plus separate Core and native
  integration tests.
- **Rationale:** This isolates native/UI dependencies while remaining small
  enough to explain in a course review.
- **Consequences:** Mapping is required at layer boundaries; Core can run without
  Windows or OpenCV.

## ADR-002 — Retain OpenCvSharp4 full Windows package

- **Status:** Accepted for this assignment
- **Context:** OpenCvSharp5 is recommended upstream for many new applications,
  but the project brief and verified examples use OpenCvSharp4/LBPH. The deadline
  favors a stable, known API.
- **Alternatives:** OpenCvSharp5; Emgu CV; OpenCvSharp4.Windows Slim; full
  OpenCvSharp4.Windows.
- **Decision:** Pin `OpenCvSharp4.Windows` 4.13.0.20261107.
- **Rationale:** It supports .NET 10 through its .NET 8+ assets and bundles the
  Windows x64 runtime, video I/O, GDI extensions, and OpenCV contrib face module.
  Slim omits video I/O and contrib and therefore cannot meet camera/LBPH needs.
- **Consequences:** Windows x64 is the supported deployment boundary. Upgrading
  package family requires a dedicated compatibility decision and tests.

## ADR-003 — Haar cascade plus LBPH

- **Status:** Proposed; implementation and thresholds pending
- **Context:** The assignment needs understandable offline detection and basic
  recognition for a small voluntary dataset.
- **Alternatives:** DNN detector/embedding model; Eigenfaces/Fisherfaces; Haar +
  LBPH.
- **Decision:** Plan Haar frontal-face detection and LBPH recognition.
- **Rationale:** Both are available in the selected OpenCV runtime, run locally
  on CPU, and are appropriate for an educational demonstration.
- **Consequences:** Accuracy is limited; normalization and unknown thresholds
  need local evaluation. No score may be called probability.

## ADR-004 — Local filesystem without a database

- **Status:** Accepted
- **Context:** Samples, label metadata, and one model need simple local storage.
- **Alternatives:** SQLite; cloud storage; local files and JSON.
- **Decision:** Use ignored local directories and System.Text.Json metadata.
- **Rationale:** This minimizes scope and makes consented data easy to inspect and
  delete.
- **Consequences:** Later code must validate paths, write atomically, handle
  corruption, and delete related files consistently.

## ADR-005 — Explicit application state

- **Status:** Accepted
- **Context:** Camera, image, and training actions can conflict and native
  resources require ordered cleanup.
- **Alternatives:** infer state from enabled controls; mutable flags; immutable
  state transitions.
- **Decision:** Keep a UI-independent immutable `ApplicationState` and reject
  invalid transitions.
- **Rationale:** State rules can be tested without a camera or UI and later map
  directly to command availability.
- **Consequences:** New modes require transition and test updates.

## ADR-006 — Windows CI without camera hardware

- **Status:** Accepted
- **Context:** WinForms and the native Windows package must build in CI, but
  hosted runners do not provide a reliable physical webcam.
- **Alternatives:** Ubuntu CI with Windows cross-targeting; Windows CI; skip
  native tests.
- **Decision:** Run restore, format, Release build, Core tests, and a camera-free
  OpenCV/LBPH smoke test on `windows-latest`.
- **Rationale:** This validates the real native runtime without unreliable
  hardware assumptions.
- **Consequences:** Camera lifecycle acceptance remains a separately recorded
  manual/hardware test.

## ADR-007 — No license selected in PR #00

- **Status:** Accepted
- **Context:** Selecting a repository license is an ownership decision not
  provided by the user.
- **Decision:** Do not add a license until the repository owner chooses one.
- **Consequences:** Public visibility alone does not grant reuse rights; a later
  decision should address the project and third-party model notices.

## ADR-008 — Turkish presentation with English code

- **Status:** Accepted
- **Context:** The professor-facing assignment and mandatory operation names are
  Turkish, while the codebase and technical documentation use English.
- **Alternatives:** entirely English UI; bilingual labels; Turkish visible UI
  with English identifiers and documentation.
- **Decision:** Use concise Turkish labels and feedback in the desktop interface
  while keeping C# identifiers and technical documentation in English.
- **Rationale:** This matches the course context without duplicating every label
  or reducing code readability.
- **Consequences:** Future UI text must remain consistent and should be prepared
  for resource-based localization if a second language is later required.

## ADR-009 — Built-in responsive WinForms controls

- **Status:** Accepted
- **Context:** PR #01 needs a polished, scalable interface without adding a UI
  framework or coupling Core to WinForms.
- **Alternatives:** third-party component library; absolute-positioned controls;
  built-in layout containers plus a small repeated MetricCard control.
- **Decision:** Use TableLayoutPanel, FlowLayoutPanel, docking, font-based
  autoscaling, built-in controls, and one repository-owned MetricCard.
- **Rationale:** This keeps dependencies unchanged, supports the tested display
  scaling, and makes the interface explainable.
- **Consequences:** Visual behavior needs manual Windows validation; pixel-level
  UI tests remain intentionally excluded.
