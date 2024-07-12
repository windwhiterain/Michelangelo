using System.Diagnostics;
using System.Numerics;
using Michelangelo.Render;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.SDL;
using Silk.NET.Windowing;

namespace Michelangelo.UI;

public interface IUI
{
    string Title { get; }
    void AddMesh(Mesh mesh);
    void OnLoad();
    void OnRender(double obj);
    void OnUpdate(double obj);
    void OnFramebufferResize(Vector2D<int> newSize);
    void OnInput(InputData inputData);
    void Release();
}
public interface ICameraView
{
    Matrix4x4 View { get; }
}
public interface ICameraProject
{
    Matrix4x4 Project { get; }
}
public interface ICamera : ICameraView, ICameraProject;
public interface IController
{
    void OnInput(InputData inputData);
    void Tick(float deltaTime);
}
public interface ICameraController : IAgent<ICamera>, IAgents<IController>;
public class DefualtUI : IUI
{
    public string Title => "Michelangelo";
    List<Mesh> meshes = new();
    Shader shader;
    Mesh mesh;
    ICameraController cameraController;
    public void AddMesh(Mesh mesh)
    {
        meshes.Add(mesh);
    }

    public void OnLoad()
    {
        shader = new(
                @"
        #version 330 core 

        layout (location = 0) in vec4 vPos;
        
        uniform mat4 View;
        uniform mat4 Project;

        void main()
        {
            gl_Position = Project*View*vec4(vPos.x, vPos.y, vPos.z, 1.0);
        }
        ",
                @"
        #version 330 core

        out vec4 FragColor;

        void main()
        {
            FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
        }
        "
        );

        cameraController = new CameraControllers.DefualtCameraController(1);

        var geometry = new Michelangelo.Geometry.Mesh.HalfMesh();
        var v0 = geometry.AddVertex(new(-10, -10, -10));
        var v1 = geometry.AddVertex(new(20, 0, 0));
        var v2 = geometry.AddVertex(new(10, 10, 10));
        var (edge01, halfedge01) = geometry.AddEdge(v0, v1);
        var (edge12, halfedge12) = geometry.AddEdge(v1, v2);
        var (edge20, halfedge20) = geometry.AddEdge(v2, v0);
        geometry.AddFace(halfedge01, halfedge12, halfedge20);
        mesh = Mesh.FromGeometry(geometry);

        AddMesh(mesh);
    }

    public void OnRender(double obj)
    {
        shader.SetUniform("View", cameraController.agent.View);
        shader.SetUniform("Project", cameraController.agent.Project);
        var rhi = App.Rhi;
        rhi.Clear();
        foreach (var i in meshes)
        {
            rhi.DrawMesh(i, shader);
        }

    }
    DateTime time1 = DateTime.Now;
    DateTime time2 = DateTime.Now;
    public void OnUpdate(double obj)
    {
        time2 = DateTime.Now;
        float deltaTime = (time2.Ticks - time1.Ticks) / 10000000f;
        time1 = time2;
        foreach (var controller in cameraController.agents)
        {
            controller.Tick(deltaTime);
        }
    }

    public void OnFramebufferResize(Vector2D<int> newSize)
    {
        App.gl.Viewport(newSize);
    }

    public void OnInput(InputData inputData)
    {
        foreach (var controller in cameraController.agents)
        {
            controller.OnInput(inputData);
        }
    }
    public void Release()
    {
        shader.Release();
        mesh.Release();
    }

}


