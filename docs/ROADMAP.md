# Roadmap

Mandatory functionality takes priority over optional polish. A phase is complete
only after implementation, relevant documentation, local validation, and CI.

| Phase | Branch | Objective | Dependencies | Status |
|---|---|---|---|---|
| PR #00 | `chore/initialize-project` | Solution, boundaries, dependency pins, tests, CI, documentation | Empty repository | In progress |
| PR #01 | `feat/application-ui` | Recognition layout, PictureBox, navigation, state-driven commands and feedback | PR #00 | Planned |
| PR #02 | `feat/camera-and-images` | Visible camera lifecycle, image selection, preview, cancellation and errors | PR #01 | Planned; hardware-dependent validation |
| PR #03 | `feat/face-detection` | Reviewed Haar asset, all-face detection, rectangles, no-face handling | PR #02 | Planned |
| PR #04 | `feat/face-recognition` | Voluntary dataset management, LBPH training, labels, distance, Unknown, deletion | PR #03 | Planned |
| PR #05 | `fix/quality-and-errors` | Edge cases, disposal, responsiveness, privacy and acceptance testing | PR #04 | Planned |
| PR #06 | `docs/final-submission` | Authentic screenshots, final evidence, explanation and submission review | PR #05 | Planned |

## Milestone gates

### Foundation gate

- Release build and camera-free tests pass locally and in Windows CI.
- Core has no native/UI dependency.
- Private-data exclusions and documentation are reviewed.

### Mandatory feature gate

- Camera and selected image display in PictureBox.
- Start, Stop, and Select Image work with understandable failure messages.
- Multiple detected faces receive rectangles.
- At least two voluntary labels can be trained and predicted locally.
- Labels and LBPH distance/Unknown are displayed honestly.

### Submission gate

- Repeated camera lifecycle and form-close disposal pass on real hardware.
- Local deletion works and no private data is tracked.
- Two authentic consent-safe screenshots are included.
- Traceability and checklist link each claim to evidence.
- Student number is supplied.

## Optional work

Dataset/training UI and multiple-face detection support the assignment and are
planned. Automatic face-image saving is excluded unless later explicitly
requested with consent and a visible action. More advanced models, cloud sync,
attendance, and surveillance are outside scope.
