# Software Requirements Specification

## 1. Purpose and scope

Face Recognition Studio is a local Windows desktop demonstration for YMH219.
It will display camera or image input, detect and mark faces, and predict labels
from a small voluntary local dataset. It is not a production biometric system.

PR #00 establishes the engineering foundation only. Status values below are:
**Implemented**, **Planned**, **Manual**, or **Hardware-dependent**.

## 2. Actors and scenarios

The single actor is a student or informed volunteer operating the application
in the foreground.

- **S-01 Camera demonstration:** the user starts a visible camera, observes face
  rectangles/results, and stops it.
- **S-02 Image demonstration:** the user selects a supported local image and
  reviews all detected faces.
- **S-03 Dataset preparation:** the user explicitly adds voluntary samples under
  at least two labels and trains a local model.
- **S-04 Data removal:** the user removes samples and the trained local model.
- **S-05 Recovery:** the user receives an understandable message when the
  camera, image, cascade, or trained model is unavailable.

## 3. Functional requirements

| ID | Requirement | Acceptance criterion | PR #00 status |
|---|---|---|---|
| FR-001 | Display a webcam frame or selected image in a PictureBox. | Foreground preview visibly matches the chosen source. | Planned, PR #02 |
| FR-002 | Detect every visible face and draw rectangles. | A verified image with multiple faces shows one rectangle per detected face. | Planned, PR #03 |
| FR-003 | Recognize at least two voluntary labels. | A locally trained model accepts two valid classes and produces experimental predictions. | Planned, PR #04 |
| FR-004 | Display predicted label and score where available. | Each prediction shows a label and LBPH distance clearly identified as distance. | Planned, PR #04 |
| FR-005 | Provide Camera Start. | User action opens an available camera once and shows active state. | Planned, PR #02 |
| FR-006 | Provide Camera Stop. | Stop ends capture and releases native resources without closing the app. | Planned, PR #02 |
| FR-007 | Provide Select Image. | Supported image selection stops conflicting capture and displays the image. | Planned, PR #02 |
| FR-008 | Show understandable camera/model errors. | Missing/busy camera and missing/invalid model produce actionable feedback without a crash. | Planned, PRs #02–#05 |
| FR-009 | Use a C# OpenCV library. | Full OpenCvSharp Windows runtime loads in an automated smoke test. | Implemented in foundation; feature use planned |
| FR-010 | Use Windows Forms and PictureBox. | Application is WinForms; the recognition screen uses PictureBox. | WinForms shell implemented; PictureBox planned PR #01 |
| FR-011 | Clear the preview. | Clear returns to a safe idle display and disposes the prior image. | Planned, PR #02 |
| FR-012 | Manage voluntary samples explicitly. | Add/delete actions require user choice; no background collection occurs. | Planned, PR #04 |
| FR-013 | Delete local biometric state. | User can remove samples, metadata, and trained model with confirmation. | Planned, PR #04 |
| FR-014 | Prevent conflicting operations. | Camera/image/training commands are enabled only in valid states. | Core state foundation implemented; UI wiring planned |

## 4. Non-functional requirements

| ID | Requirement | Verification |
|---|---|---|
| NFR-001 | UI remains responsive during capture/training. | Manual responsiveness checks and cancellation tests. |
| NFR-002 | Native resources are disposed deterministically. | Integration tests plus repeated start/stop manual test. |
| NFR-003 | Application targets Windows x64 and .NET 10. | Project inspection and Release build. |
| NFR-004 | Core remains independent of UI/OpenCV. | Project-reference inspection and automated build. |
| NFR-005 | CI does not require camera or private data. | Windows GitHub Actions run. |
| NFR-006 | Errors are actionable and do not disclose private data. | Unit/manual error scenarios. |
| NFR-007 | Dependencies and SDK versions are pinned. | `global.json` and `Directory.Packages.props`. |
| NFR-008 | Documentation distinguishes plans from evidence. | Documentation review in every PR. |

## 5. Privacy and security requirements

| ID | Requirement |
|---|---|
| SEC-001 | Camera starts only after a visible user action and active state is visible. |
| SEC-002 | Images, labels, captures, and trained models remain local and Git-ignored. |
| SEC-003 | Samples require informed voluntary participation. |
| SEC-004 | Users can delete stored samples and models. |
| SEC-005 | No network transmission, telemetry, scraping, or remote identification. |
| SEC-006 | Results are experimental; distance is not reported as probability. |
| SEC-007 | The product is not used for attendance, access, discipline, or safety decisions. |

## 6. Constraints

- Windows 10/11 x64 and .NET 10.
- Windows Forms with PictureBox.
- OpenCvSharp full Windows runtime; Haar cascade and LBPH planned.
- Local files only; no database or cloud dependency.
- No physical-camera dependency in ordinary automated tests.
- Private or third-party face datasets may not enter the repository.

## 7. Traceability matrix

| Requirement | Planned implementation | Verification evidence |
|---|---|---|
| FR-001, FR-005–007, FR-011 | MainForm, CameraService, ImageService | PR #02 tests and manual camera record |
| FR-002 | FaceDetectionService and overlay renderer | PR #03 deterministic image tests |
| FR-003, FR-004 | DatasetService, TrainingService, FaceRecognitionService | PR #04 tests with consented local manual samples |
| FR-008 | Service result/error mapping and UI feedback | PRs #02–#05 negative tests |
| FR-009 | Infrastructure package and native smoke test | `OpenCvRuntimeCompatibilityTests` |
| FR-010 | WinForms App and planned recognition PictureBox | App Release build; PR #01 UI inspection |
| FR-012, FR-013 | TrainingForm and ignored local storage | PR #04 storage/deletion tests |
| FR-014 | `ApplicationState` and command availability | `ApplicationStateTests`; PR #01 UI tests/manual review |
| SEC-001–007 | UI behavior, ignored local-data, deletion workflow, documentation | Privacy review and manual acceptance checklist |

## 8. PR #00 acceptance

- Solution and dependency directions build in Release mode.
- Core application-state tests pass.
- Camera-free OpenCV/LBPH native compatibility test passes on Windows x64.
- CI executes restore, format verification, build, and tests on Windows.
- Required documentation is substantive and truthful.
- No biometric sample, trained model, capture, or secret is tracked.
