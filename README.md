# Face Recognition Studio

[![.NET CI](https://github.com/alaamadii/Face-Recognition/actions/workflows/dotnet.yml/badge.svg)](https://github.com/alaamadii/Face-Recognition/actions/workflows/dotnet.yml)

An educational Windows desktop application for **YMH219 Object-Oriented
Programming** at Fırat University. The finished application will display a
webcam or selected image, mark detected faces, and demonstrate local recognition
against voluntary samples.

**Student:** Alaa Madi

**Student number:** Not yet provided

**Current phase:** PR #01 — WinForms application interface

> This is a local classroom demonstration, not an identity-verification,
> surveillance, attendance, access-control, or safety system. Predictions are
> experimental and the application has no liveness detection.

## Status

PR #01 adds the responsive WinForms recognition workspace, Turkish navigation,
PictureBox preview surface, truthful status cards, accessible command states,
and planned Dataset/Settings views. Camera input, image loading, detection,
sample management, training, and recognition are not yet implemented.

| Capability | Status |
|---|---|
| Layered .NET 10 solution | Implemented |
| Responsive Windows Forms interface | Implemented and manually validated |
| PictureBox preview and truthful result/status display | Implemented; no source connected yet |
| Core state and operation availability | Implemented and unit tested |
| Native OpenCV and LBPH loading | Compatibility test implemented |
| Camera start/stop and preview | Planned for PR #02 |
| Face detection rectangles | Planned for PR #03 |
| Local sample recognition | Planned for PR #04 |
| Authentic screenshots | Planned for PR #06 |

## Technology

- C# and .NET 10
- Windows Forms on Windows x64
- OpenCvSharp4.Windows 4.13.0.20261107 (full runtime)
- Haar cascade detection and LBPH recognition, planned
- xUnit and GitHub Actions
- System.Text.Json for planned local metadata

The full OpenCvSharp Windows package is intentional: its native runtime includes
video input and OpenCV contrib modules required by LBPH. The Slim runtime is not
suitable for this project.

## Prerequisites

- Windows 10 or 11, x64
- .NET SDK 10.0.402 (pinned by `global.json`)
- Git
- A webcam only for later manual camera tests

No Python, database, cloud API, paid service, GPU, or webcam is needed to build
and run the automated tests.

## Build and test

```powershell
git clone https://github.com/alaamadii/Face-Recognition.git
cd Face-Recognition
dotnet restore FaceRecognition.slnx
dotnet format FaceRecognition.slnx --verify-no-changes --no-restore
dotnet build FaceRecognition.slnx --configuration Release --no-restore
dotnet test FaceRecognition.slnx --configuration Release --no-build
```

Run the application interface:

```powershell
dotnet run --project src/FaceRecognition.App/FaceRecognition.App.csproj
```

## Architecture

```text
FaceRecognition.App (WinForms)
    ├── FaceRecognition.Core
    └── FaceRecognition.Infrastructure ──> FaceRecognition.Core

FaceRecognition.Core.Tests ──> FaceRecognition.Core
FaceRecognition.IntegrationTests ──> Infrastructure + Core
```

Core is platform-independent and must not reference WinForms, `System.Drawing`,
OpenCvSharp, or native resources. Infrastructure owns OpenCV and filesystem
integration. App owns controls, UI-thread dispatch, and presentation.

See [Architecture](docs/ARCHITECTURE.md),
[Requirements](docs/REQUIREMENTS.md), and
[Computer Vision](docs/COMPUTER_VISION.md) for the planned design.

## User workflow

The current interface supports navigation among Tanıma, Veri Seti, and Ayarlar,
shows the inactive camera/model state, and explains when a requested operation
belongs to a later phase. It does not open a camera or image yet.

The planned complete workflow is:

1. Add clear, voluntary local samples under a label and give informed consent.
2. Train a local LBPH model after dataset validation.
3. Start the visible camera preview or select a local image.
4. Review face rectangles, experimental labels, and LBPH distance values.
5. Stop the camera and delete local samples/model when finished.

Only the interface portion is implemented in PR #01. The operational steps are
deliberately deferred to PRs #02–#04.

## Private data

Photographs, labels, camera captures, and trained models remain in ignored
`local-data/` storage. Never commit them. Only use your own face or samples from
informed volunteers. See [Security and Privacy](docs/SECURITY_PRIVACY.md).

## Known limitations

- Camera and image operations are not implemented yet.
- No detector or verified Haar model is included yet.
- No dataset, training workflow, prediction, or deletion UI exists yet.
- The interface has been manually validated, but camera hardware has not.
- LBPH quality will depend on lighting, pose, camera, and sample diversity.
- There is no liveness or spoof detection.

## Documentation

- [Requirements](docs/REQUIREMENTS.md)
- [Architecture](docs/ARCHITECTURE.md)
- [Computer vision design](docs/COMPUTER_VISION.md)
- [Development journal](docs/DEVELOPMENT.md)
- [Architecture decisions](docs/DECISIONS.md)
- [Testing](docs/TESTING.md)
- [Security and privacy](docs/SECURITY_PRIVACY.md)
- [User guide](docs/USER_GUIDE.md)
- [Troubleshooting](docs/TROUBLESHOOTING.md)
- [Roadmap](docs/ROADMAP.md)
- [Submission checklist](docs/SUBMISSION_CHECKLIST.md)
