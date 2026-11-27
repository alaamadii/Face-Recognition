# Submission Checklist

Status is truthful as of PR #00. `[ ]` means incomplete or unverified; `[x]`
means supported by repository evidence.

## Professor requirements

- [ ] FR-001 camera or selected image displayed in PictureBox
- [ ] FR-002 faces detected and marked with rectangles
- [ ] FR-003 at least two voluntary local labels recognized
- [ ] FR-004 predicted label and LBPH distance displayed
- [ ] FR-005 Camera Start operation
- [ ] FR-006 Camera Stop operation and resource release
- [ ] FR-007 Select Image operation
- [ ] FR-008 understandable camera/model errors
- [x] FR-009 C# OpenCV dependency and camera-free native compatibility test
- [ ] FR-010 WinForms PictureBox presentation (WinForms shell exists; PictureBox pending)

## Engineering foundation

- [x] .NET 10 solution with App, Core, Infrastructure, and test projects
- [x] Correct one-way project references
- [x] Core independent of WinForms and OpenCV
- [x] Windows x64 boundary documented
- [x] SDK and NuGet versions pinned
- [x] Full OpenCvSharp package selected; Slim excluded
- [x] State foundation and deterministic Core tests
- [x] Windows CI workflow created
- [x] GitHub CI confirmed green for PR #00

## Validation and quality

- [ ] Release restore/build completed and recorded
- [ ] Automated tests completed and recorded
- [ ] Formatting verification completed and recorded
- [ ] Physical camera start/stop manually tested
- [ ] Camera unavailable/permission scenarios tested
- [ ] Invalid image, no-face, and multiple-face scenarios tested
- [ ] Missing cascade/model scenarios tested
- [ ] Repeated stop/close disposal tested
- [ ] UI responsiveness validated
- [ ] Local data deletion validated

## Privacy and documentation

- [x] Biometric data and trained models excluded by `.gitignore`
- [x] Consent, local-only storage, limitations, and prohibited uses documented
- [x] LBPH output documented as distance, not probability
- [x] Required initial technical documents created
- [x] Planned and implemented functionality distinguished
- [ ] Official Haar asset source/license/checksum recorded (asset not added yet)
- [ ] At least two authentic consent-safe screenshots
- [ ] Final README reflects completed behavior

## Repository and identity

- [x] Public GitHub repository
- [x] Development performed on a feature branch through a pull request
- [x] PR #00 opened and CI confirmed
- [ ] All later phase PRs reviewed
- [ ] Meaningful final commit history reviewed
- [x] Student name: Alaa Madi
- [ ] Student number: Not yet provided
- [ ] Repository license decision made by owner

## Final hand-in

- [ ] Clean clone builds using documented instructions
- [ ] Application runs on the submission machine
- [ ] Requirement traceability matches actual evidence
- [ ] Known limitations can be explained
- [ ] Student can explain architecture, detection vs recognition, LBPH distance,
      state transitions, disposal, and privacy choices
- [ ] Final GitHub URL and required submission artifacts delivered
