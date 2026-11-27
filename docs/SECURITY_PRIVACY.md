# Security and Privacy

## Boundary and data classification

Face photographs, cropped faces, labels linked to people, and trained LBPH
models are sensitive biometric-related data. This educational application keeps
them on the user's machine under the ignored `local-data/` boundary. Source
code, public documentation, and a reviewed detector cascade are non-private.

No biometric data exists in PR #00.

## Threat model

| Threat | Impact | Required control |
|---|---|---|
| Accidental Git commit | Public biometric disclosure | Ignore rules, status review, PR privacy checklist |
| Covert/background camera use | Loss of consent/privacy | Visible user start, active indicator, foreground-only lifecycle |
| Unauthorized local file access | Sample/model disclosure | Clear storage path, OS account permissions, no secret copies |
| Path traversal through labels | File overwrite/read | Treat labels as metadata; generate internal IDs; validate paths |
| Corrupt or replaced model | Crash or misleading output | Validate metadata/version, handle load failure, fail closed |
| False match or spoof | Harmful reliance | Unknown threshold, experimental wording, no high-impact use, no liveness claims |
| Residual data after deletion | Continued retention | Confirmed deletion of samples, metadata, model, and temporary files |

## Consent requirements

- Use only the student's own face or an informed volunteer's samples.
- Explain the educational purpose, local storage, limitations, retention, and
  deletion before collecting a sample.
- Participation must be voluntary and revocable.
- Do not scrape social media, public cameras, class lists, or third-party
  datasets.

## Camera safety

Camera activation must require a visible click. The UI must show when capture is
active and expose Stop. Closing the form must cancel capture and release the
device. The application must not start with Windows, run a hidden background
camera, upload frames, or track a person across locations.

## Local storage and deletion

Future local data will be placed below `local-data/`, never `assets/`. The
folder is Git-ignored. Planned metadata will avoid unnecessary personal fields.
Delete must remove selected samples and invalidate/retrain or remove the model;
Reset must remove the entire local dataset and model after confirmation.

The project does not promise secure erasure from SSD media or backups. Users
must manage OS backups and device access appropriately.

## Result presentation

- Display “Unknown” when the model is unavailable or distance exceeds the
  evaluated threshold.
- Call the numeric output “LBPH distance,” never confidence percentage,
  probability, accuracy, or identity proof.
- State that predictions are experimental and affected by light, pose, camera,
  sample quality, false matches, and lack of liveness detection.

## Prohibited use

Do not use or extend this repository for covert surveillance, public-space
identification, stranger tracking, social-media scraping, attendance, access
control, examinations, discipline, employment decisions, policing, financial
decisions, medical decisions, or production identity verification.

## Incident response

If private data is accidentally committed, stop distribution, notify the owner,
remove the data from current history using an explicitly approved process,
rotate any exposed secrets, and treat prior clones/caches as potentially
retaining it. Ordinary feature work must never rewrite history without explicit
authorization.
