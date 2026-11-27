# User Guide

## Current availability

PR #01 provides the main recognition interface and navigation. Camera, image,
detection, training, and recognition operations remain unavailable. Buttons for
deferred source operations display an informational message rather than a false
success state.

## Install and launch the application

1. Use Windows 10 or 11 x64.
2. Install the .NET 10 SDK matching `global.json`.
3. Clone the repository.
4. Run:

```powershell
dotnet restore FaceRecognition.slnx
dotnet build FaceRecognition.slnx --configuration Release --no-restore
dotnet run --project src/FaceRecognition.App/FaceRecognition.App.csproj
```

The application opens on **Tanıma**. The initial status is truthful: zero
detected faces, no predicted label or LBPH distance, camera stopped, and model
unavailable. A webcam is not opened and no personal data is created.

## Current navigation

- **Tanıma:** preview surface, source controls, result/status cards, and feedback.
- **Veri Seti:** planned-feature information for voluntary samples and training.
- **Ayarlar:** planned camera/model/storage information and privacy boundary.

Use Tab to move through navigation and available actions. While Idle,
**Kamerayı Durdur** and **Önizlemeyi Temizle** are disabled. Selecting Start or
Select Image currently explains that the operation arrives in PR #02.

## Planned recognition workflow

### Prepare voluntary samples — planned PR #04

Open Veri Seti, choose a label, and explicitly select clear images belonging to
you or an informed volunteer. The application will validate supported formats
and usable faces. At least two sample classes/labels are required. Choose Train
Model only after validation succeeds.

### Use the camera — planned PR #02–#04

Select Start Camera to open a visible preview. Face rectangles and experimental
predictions will appear only when the relevant detector/model is available.
Select Stop Camera before leaving the application. Closing the form will also
stop and release it.

### Select an image — planned PR #02–#04

Select Image, choose a supported local file, and review the preview. Every
detected face will be marked. Clear Preview disposes the displayed image and
returns to Idle.

### Understand results — planned PR #04

“Unknown” means no acceptable local match was produced. A displayed LBPH value
is distance: lower generally means closer under the trained local model, but it
is not a probability, accuracy score, or verified identity.

### Remove local data — planned PR #04

Use Dataset deletion for selected samples and Reset Local Data for all samples,
label metadata, and the trained model. Confirm the operation. The application
cannot guarantee secure erasure from filesystem backups or SSD history.

## Responsible use

Keep the camera visible, obtain consent, do not upload or commit samples, and do
not use a prediction for attendance, access, discipline, safety, or any other
high-impact decision.
