using OpenCvSharp;
using OpenCvSharp.Face;

namespace FaceRecognition.IntegrationTests;

public sealed class OpenCvRuntimeCompatibilityTests
{
    [Fact]
    [Trait("Category", "NativeCompatibility")]
    public void FullWindowsRuntime_LoadsCoreAndLbphModules()
    {
        Assert.True(OperatingSystem.IsWindows());
        Assert.False(string.IsNullOrWhiteSpace(Cv2.GetVersionString()));

        using var image = new Mat(1, 1, MatType.CV_8UC1, Scalar.Black);
        using LBPHFaceRecognizer recognizer = LBPHFaceRecognizer.Create();

        Assert.False(image.Empty());
        Assert.NotNull(recognizer);
    }
}
