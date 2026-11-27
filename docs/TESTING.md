# Testing Strategy and Evidence

## Principles

Tests are split by dependency boundary. Core tests are deterministic and
platform-independent. Integration tests may load the pinned Windows native
runtime or use isolated temporary storage. Physical camera and consented-face
checks are manual/hardware-dependent and never block ordinary CI.

Do not infer recognition quality from unit tests and never invent accuracy or
performance figures.

## Automated suites

### FaceRecognition.Core.Tests

Current coverage verifies:

- initial Idle state;
- valid camera start/active/stop transitions;
- rejection of a conflicting image operation while camera is active;
- required understandable message for Faulted state;
- message normalization and recovery to Idle;
- Idle, CameraStarting, CameraActive, ImageLoaded, and Faulted command
  availability for Start, Stop, Select, and Clear.

Later Core tests will cover label validation, settings bounds, prediction
interpretation, unknown handling, and storage result semantics.

### FaceRecognition.IntegrationTests

The PR #00 native smoke test is camera-free. It loads the OpenCV runtime, obtains
the runtime version, allocates/disposes a `Mat`, and creates/disposes
`LBPHFaceRecognizer`. Passing proves native core and contrib face bindings load
on the tested Windows x64 environment; it does not prove camera support or
recognition quality.

Later integration tests may use temporary directories and licensed test images
that contain no private data. Camera-index tests remain excluded from CI.

## Planned scenario matrix

| Scenario | Method | Phase |
|---|---|---|
| Camera unavailable/already in use | service test double + manual hardware | PR #02/#05 |
| Repeated start/stop and form close | manual hardware; disposal instrumentation | PR #02/#05 |
| Invalid/unsupported image | automated integration | PR #02 |
| Image with no face | deterministic test image | PR #03 |
| Image with multiple faces | deterministic licensed/generated test fixture | PR #03 |
| Missing/invalid cascade | integration | PR #03 |
| Too few labels/samples | Core + storage integration | PR #04 |
| Missing/corrupt trained model | integration | PR #04/#05 |
| Known, unknown, ambiguous observations | algorithm tests + consented manual evaluation | PR #04/#05 |
| UI responsiveness/cancellation | manual and service-level tests | PR #05 |
| Local sample/model deletion | temporary-directory integration + manual UI | PR #04/#05 |
| Git privacy exclusions | repository review | Every PR |

## Manual hardware record template

Record date, Windows version, x64 CPU, camera model/index, permission state,
scenario, expected result, observed result, pass/fail, and tester. Do not include
sample images or volunteer names. A test not performed must remain “Not run.”

## PR #00 actual results

Validated locally on 2026-11-27 using Windows 10.0.22631 x64, .NET SDK
10.0.402, and the pinned package set:

```powershell
dotnet restore FaceRecognition.slnx
dotnet format FaceRecognition.slnx --verify-no-changes --no-restore
dotnet build FaceRecognition.slnx --configuration Release --no-restore
dotnet test FaceRecognition.slnx --configuration Release --no-build
```

| Check | Result |
|---|---|
| Restore | Passed; all five projects restored |
| Format verification | Passed, exit code 0 |
| Release build | Passed with 0 warnings and 0 errors |
| Core tests | Passed: 5, failed: 0, skipped: 0 |
| Native integration tests | Passed: 1, failed: 0, skipped: 0 |
| OpenCV/LBPH native load | Passed on Windows x64 |
| Privacy ignore probes | Local face, LBPH model, dataset, and draft screenshot paths ignored |
| GitHub Actions | Passed on Windows x64: run 45962670667 |

Camera lifecycle, image loading, detection, recognition quality,
responsiveness, and deletion tests are **Not run** because those features are
outside PR #00.

## PR #01 actual results

Validated locally on 2026-11-27 using Windows 10.0.22631 x64 and .NET SDK
10.0.402:

| Check | Result |
|---|---|
| Restore | Passed; all projects up to date |
| Format verification | Passed, exit code 0 |
| Release build | Passed with 0 warnings and 0 errors |
| Core tests | Passed: 10, failed: 0, skipped: 0 |
| Native integration tests | Passed: 1, failed: 0, skipped: 0 |
| Initial Release launch | Passed; responsive `Face Recognition Studio` window |
| Idle controls | Start/Select enabled; Stop/Clear disabled |
| Navigation | Recognition, Dataset, and Settings headings verified through Windows accessibility APIs |
| Keyboard navigation | Logical order verified; disabled actions skipped |
| Responsive layout | Window remained responsive at minimum and larger sizes; window-only DPI-aware visual inspection performed |
| Shutdown | Close request succeeded and process exited |
| GitHub Actions | Passed on Windows x64: run 45974776845 |

The interface test used no webcam, photograph, private data, model, or network
service. Temporary window captures were deleted and are not submission evidence.
Camera lifecycle, image loading, detection, recognition quality, and deletion
tests remain **Not run** because those features are outside PR #01.
