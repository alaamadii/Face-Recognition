# Troubleshooting

## SDK selected incorrectly

**Symptom:** `A compatible .NET SDK was not found.`

**Check:** run `dotnet --info` and inspect `global.json`.

**Recovery:** install .NET SDK 10.0.402 or a permitted patch in its feature band.
Do not change the target framework merely to hide an environment problem.

## NuGet restore fails

**Symptom:** packages cannot be downloaded or resolved.

**Check:** network access, NuGet source configuration, and the exact versions in
`Directory.Packages.props`.

**Recovery:** restore connectivity to `https://api.nuget.org/v3/index.json`, then
run `dotnet nuget locals all --list` and retry. Clear caches only when corruption
is evidenced; do not remove package pins.

## OpenCvSharp native DLL does not load

**Symptom:** `DllNotFoundException`, `TypeInitializationException`, or a native
load failure.

**Check:** Windows x64 process, full `OpenCvSharp4.Windows` package, Release
output runtimes, and integration smoke test.

**Recovery:** restore/build for x64, confirm antivirus did not quarantine native
files, and avoid the Slim or wrong-architecture runtime. Record the complete
inner exception.

## Camera unavailable — planned feature

**Symptom:** Start cannot open the selected index.

**Check:** Windows Settings > Privacy & security > Camera, another application
using the device, camera index, driver, and physical connection.

**Recovery:** close competing software, grant desktop-app permission, reconnect
the camera, or choose another index. The app must remain usable and show an
understandable error.

## Camera frame cannot be read — planned feature

Stop capture, release the device, and retry once after checking the connection.
Do not leave the application in an active state after read failure.

## Selected image is invalid — planned feature

Choose a readable local JPEG, PNG, or BMP with a supported extension and valid
content. Renaming a non-image file does not make it valid. Very large images may
be rejected to protect memory and responsiveness.

## Detection model missing or invalid — planned feature

The official reviewed Haar XML must exist at the documented application asset
path and match its recorded SHA-256. Do not download a random copy. Reinstall or
restore the reviewed asset from the repository once PR #03 adds it.

## Trained model unavailable — planned feature

Add sufficient voluntary samples and train again. If model metadata and files
disagree, remove/reset the local model rather than attempting recognition with
unknown label mappings.

## No face or multiple faces during enrollment — planned feature

Use a clear, front-facing sample containing exactly one participating person.
Improve even lighting, reduce occlusion, and avoid small faces. Detection during
normal recognition may show multiple faces, but an enrollment sample should be
unambiguous.

## CI differs from local results

Confirm the committed `global.json`, package lock/pins, x64 target, clean Release
build, format check, and absence of machine-local files. CI intentionally has no
camera and no private dataset.
