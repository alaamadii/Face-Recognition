using FaceRecognition.Core.States;

namespace FaceRecognition.Core.Tests.States;

public sealed class ApplicationStateTests
{
    [Fact]
    public void IdleState_IsNotCameraActive()
    {
        Assert.Equal(ApplicationMode.Idle, ApplicationState.Idle.Mode);
        Assert.False(ApplicationState.Idle.IsCameraActive);
    }

    [Fact]
    public void CameraLifecycle_UsesValidTransitions()
    {
        ApplicationState starting = ApplicationState.Idle.TransitionTo(ApplicationMode.CameraStarting);
        ApplicationState active = starting.TransitionTo(ApplicationMode.CameraActive);
        ApplicationState stopped = active.TransitionTo(ApplicationMode.Idle);

        Assert.True(starting.IsCameraActive);
        Assert.True(active.IsCameraActive);
        Assert.Equal(ApplicationMode.Idle, stopped.Mode);
    }

    [Fact]
    public void ConflictingImageLoad_IsRejectedWhileCameraIsActive()
    {
        ApplicationState active = ApplicationState.Idle
            .TransitionTo(ApplicationMode.CameraStarting)
            .TransitionTo(ApplicationMode.CameraActive);

        InvalidOperationException error = Assert.Throws<InvalidOperationException>(
            () => active.TransitionTo(ApplicationMode.ImageLoaded));

        Assert.Contains("CameraActive", error.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void FaultedState_RequiresMessage()
    {
        Assert.Throws<ArgumentException>(
            () => ApplicationState.Idle.TransitionTo(ApplicationMode.Faulted, "  "));
    }

    [Fact]
    public void FaultMessage_IsTrimmedAndCanBeCleared()
    {
        ApplicationState faulted = ApplicationState.Idle
            .TransitionTo(ApplicationMode.Faulted, "  Camera unavailable.  ");
        ApplicationState recovered = faulted.TransitionTo(ApplicationMode.Idle);

        Assert.Equal("Camera unavailable.", faulted.Message);
        Assert.Null(recovered.Message);
    }
}
