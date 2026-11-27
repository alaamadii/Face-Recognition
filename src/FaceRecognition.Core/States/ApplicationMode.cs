namespace FaceRecognition.Core.States;

/// <summary>High-level modes used to prevent conflicting user operations.</summary>
public enum ApplicationMode
{
    Idle,
    CameraStarting,
    CameraActive,
    ImageLoaded,
    Faulted,
}
