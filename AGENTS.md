# Repository instructions for coding agents

These rules apply to the entire repository.

## Purpose and scope

Build a small, explainable YMH219 Windows Forms demonstration of local face
detection and recognition. Finish mandatory professor requirements before
optional work. Do not add attendance, employee management, reporting, a
database, cloud services, telemetry, remote identification, or unrelated
frameworks.

## Layer boundaries

- `FaceRecognition.Core` targets `net10.0` and contains domain models,
  validation, states, and service contracts. It must not reference App,
  Infrastructure, WinForms, `System.Drawing`, OpenCvSharp, or filesystem/native
  implementation details.
- `FaceRecognition.Infrastructure` targets `net10.0-windows`, references Core,
  and owns OpenCV, camera, image decoding, training, and local storage.
- `FaceRecognition.App` targets `net10.0-windows`, references Core and
  Infrastructure, and owns forms, controls, UI events, UI-thread dispatch, and
  composition.
- App and Infrastructure may depend on Core. Core never depends outward.
- Core-facing contracts use domain types and primitives, never `Mat`, `Bitmap`,
  controls, or native handles.

## C# conventions

- Keep nullable reference types and implicit usings enabled.
- Treat compiler warnings as errors. Do not suppress warnings without a written
  justification.
- Prefer small focused types, clear names, immutable result models, guard
  clauses, and constructor injection at composition boundaries.
- Use async APIs for I/O or expensive processing. Accept `CancellationToken`
  where cancellation matters; do not block the UI thread.
- Marshal UI updates to the UI thread. Never manipulate controls from a camera
  worker or processing callback.
- Avoid speculative interfaces, generic repositories, service locators, global
  mutable state, and design patterns without a concrete need.

## Native resource ownership

- Every owner of `Mat`, `VideoCapture`, recognizers, classifiers, bitmaps,
  streams, timers, and cancellation sources must dispose them deterministically.
- State ownership explicitly. Clone frames when transferring ownership.
- Stop capture, cancel work, await completion, detach handlers, and release
  native resources when stopping the camera or closing a form.
- Do not catch and discard native errors. Convert expected failures into clear,
  actionable messages while preserving diagnostic details for development.

## Tests

- Add or update tests with every behavior change.
- Core tests must be deterministic and independent of Windows, OpenCV, files,
  network, camera, and wall-clock time.
- Integration tests may load OpenCV or use temporary directories but must not
  require a physical camera, private sample, or mutable developer data in CI.
- Mark physical-camera and consented-sample tests as hardware/manual and record
  them separately in `docs/TESTING.md`.
- Never invent results, accuracy, timing, or coverage figures.
- Before commit run restore, format verification, Release build, automated tests,
  and `git diff --check`.

## Dependencies

- Pin NuGet versions centrally in `Directory.Packages.props`.
- PR #00 uses `OpenCvSharp4.Windows` 4.13.0.20261107 for the full Windows x64
  runtime. Do not replace it with Slim; Slim lacks required modules.
- Review license, source, compatibility, and native footprint before adding a
  dependency or model file. Do not add Python, cloud APIs, paid services, a
  database, or GPU requirements.

## Privacy and safety

- Use only the student's own samples or informed volunteers' samples.
- Keep photos, labels, encodings, trained models, captures, and exports out of
  Git and inside the ignored local-data boundary.
- Camera activation must be visible and initiated by the user. No background or
  covert capture.
- Provide deletion for local samples and models before calling dataset features
  complete.
- Label results as experimental predictions. LBPH output is distance, not a
  probability or verified identity.
- Do not implement public-space surveillance, stranger identification, social
  media scraping, remote tracking, attendance, access control, disciplinary
  decisions, or production identity verification.

## Documentation

- Update requirement status, traceability, architecture, decisions, test
  evidence, user instructions, troubleshooting, roadmap, checklist, and
  development journal whenever affected.
- Distinguish implemented, tested, planned, manual, and hardware-dependent work.
- Do not use mockups as evidence or add fake screenshots.
- Record actual commands, results, dates, commit hashes, and PR links. Do not
  claim CI success until GitHub confirms it.

## Git workflow

- Work on one phase branch and keep changes inside that phase.
- Use meaningful commits and a pull request targeting `main`.
- Never force-push, rewrite shared history, commit private data, or merge without
  the user's explicit approval.
- PR descriptions must list scope, requirement IDs, validation evidence,
  privacy impact, limitations, and deferred work.
