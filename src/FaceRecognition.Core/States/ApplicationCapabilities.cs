namespace FaceRecognition.Core.States;

/// <summary>
/// Describes which user operations are valid for an application state without
/// depending on a particular user-interface framework.
/// </summary>
public sealed record ApplicationCapabilities(
    bool CanStartCamera,
    bool CanStopCamera,
    bool CanSelectImage,
    bool CanClearPreview)
{
    public static ApplicationCapabilities From(ApplicationState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        return state.Mode switch
        {
            ApplicationMode.Idle => new(
                CanStartCamera: true,
                CanStopCamera: false,
                CanSelectImage: true,
                CanClearPreview: false),
            ApplicationMode.CameraStarting or ApplicationMode.CameraActive => new(
                CanStartCamera: false,
                CanStopCamera: true,
                CanSelectImage: false,
                CanClearPreview: false),
            ApplicationMode.ImageLoaded => new(
                CanStartCamera: true,
                CanStopCamera: false,
                CanSelectImage: true,
                CanClearPreview: true),
            ApplicationMode.Faulted => new(
                CanStartCamera: false,
                CanStopCamera: false,
                CanSelectImage: false,
                CanClearPreview: true),
            _ => throw new ArgumentOutOfRangeException(nameof(state), state.Mode, "Unknown application mode."),
        };
    }
}
