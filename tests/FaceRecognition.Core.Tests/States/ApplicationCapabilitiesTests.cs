using FaceRecognition.Core.States;

namespace FaceRecognition.Core.Tests.States;

public sealed class ApplicationCapabilitiesTests
{
    [Fact]
    public void Idle_AllowsSourceSelectionButNotStopOrClear()
    {
        ApplicationCapabilities capabilities = ApplicationCapabilities.From(ApplicationState.Idle);

        Assert.True(capabilities.CanStartCamera);
        Assert.True(capabilities.CanSelectImage);
        Assert.False(capabilities.CanStopCamera);
        Assert.False(capabilities.CanClearPreview);
    }

    [Theory]
    [InlineData(ApplicationMode.CameraStarting)]
    [InlineData(ApplicationMode.CameraActive)]
    public void CameraModes_AllowOnlyStop(ApplicationMode mode)
    {
        ApplicationState state = mode is ApplicationMode.CameraStarting
            ? ApplicationState.Idle.TransitionTo(mode)
            : ApplicationState.Idle
                .TransitionTo(ApplicationMode.CameraStarting)
                .TransitionTo(ApplicationMode.CameraActive);

        ApplicationCapabilities capabilities = ApplicationCapabilities.From(state);

        Assert.False(capabilities.CanStartCamera);
        Assert.True(capabilities.CanStopCamera);
        Assert.False(capabilities.CanSelectImage);
        Assert.False(capabilities.CanClearPreview);
    }

    [Fact]
    public void ImageLoaded_AllowsReplacementAndClear()
    {
        ApplicationState state = ApplicationState.Idle.TransitionTo(ApplicationMode.ImageLoaded);

        ApplicationCapabilities capabilities = ApplicationCapabilities.From(state);

        Assert.True(capabilities.CanStartCamera);
        Assert.False(capabilities.CanStopCamera);
        Assert.True(capabilities.CanSelectImage);
        Assert.True(capabilities.CanClearPreview);
    }

    [Fact]
    public void Faulted_AllowsOnlyClearForRecovery()
    {
        ApplicationState state = ApplicationState.Idle
            .TransitionTo(ApplicationMode.Faulted, "Camera unavailable.");

        ApplicationCapabilities capabilities = ApplicationCapabilities.From(state);

        Assert.False(capabilities.CanStartCamera);
        Assert.False(capabilities.CanStopCamera);
        Assert.False(capabilities.CanSelectImage);
        Assert.True(capabilities.CanClearPreview);
    }
}
