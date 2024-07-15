using System.Numerics;

namespace Michelangelo.UI.Cameras;
public class LookAt : ICameraView
{
    public Vector3 position = new(0, -10, 0);
    public Vector3 direction => new(0, 1, 0);
    public Matrix4x4 project;
    public Matrix4x4 View => Matrix4x4.CreateLookAt(position, position + direction, new(0, 0, 1));

    public ICameraView agent => this;
}
public class Perspective : ICameraProject
{
    public Matrix4x4 Project => Matrix4x4.CreatePerspectiveFieldOfView((float)Math.General.PI * 2 / 3, (float)App.windowSize.X / App.windowSize.Y, 0.1f, 100000.0f);
}
public class DefualtCamera : ICamera, IAgent<ICamera>
{
    public LookAt lookAt = new();
    public Perspective perspective = new();
    public Matrix4x4 View => lookAt.View;

    public Matrix4x4 Project => perspective.Project;

    public ICamera agent => this;
}