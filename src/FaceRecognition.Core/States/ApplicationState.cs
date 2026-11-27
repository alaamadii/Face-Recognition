namespace FaceRecognition.Core.States;

/// <summary>
/// Immutable, UI-independent application state. Camera and image services will
/// drive these transitions in later phases.
/// </summary>
public sealed record ApplicationState
{
    private ApplicationState(ApplicationMode mode, string? message)
    {
        Mode = mode;
        Message = message;
    }

    public ApplicationMode Mode { get; }

    public string? Message { get; }

    public bool IsCameraActive => Mode is ApplicationMode.CameraStarting or ApplicationMode.CameraActive;

    public static ApplicationState Idle { get; } = new(ApplicationMode.Idle, null);

    public ApplicationState TransitionTo(ApplicationMode nextMode, string? message = null)
    {
        if (!IsAllowed(Mode, nextMode))
        {
            throw new InvalidOperationException($"Cannot transition from {Mode} to {nextMode}.");
        }

        string? normalizedMessage = string.IsNullOrWhiteSpace(message) ? null : message.Trim();
        if (nextMode is ApplicationMode.Faulted && normalizedMessage is null)
        {
            throw new ArgumentException("A faulted state requires an understandable message.", nameof(message));
        }

        return new ApplicationState(nextMode, normalizedMessage);
    }

    private static bool IsAllowed(ApplicationMode current, ApplicationMode next) => (current, next) switch
    {
        (ApplicationMode.Idle, ApplicationMode.CameraStarting or ApplicationMode.ImageLoaded or ApplicationMode.Faulted) => true,
        (ApplicationMode.CameraStarting, ApplicationMode.CameraActive or ApplicationMode.Idle or ApplicationMode.Faulted) => true,
        (ApplicationMode.CameraActive, ApplicationMode.Idle or ApplicationMode.Faulted) => true,
        (ApplicationMode.ImageLoaded, ApplicationMode.Idle or ApplicationMode.ImageLoaded or ApplicationMode.CameraStarting or ApplicationMode.Faulted) => true,
        (ApplicationMode.Faulted, ApplicationMode.Idle) => true,
        _ => false,
    };
}
