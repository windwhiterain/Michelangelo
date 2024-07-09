using System.Numerics;
using Silk.NET.OpenGL;
namespace Michelangelo.Render;
public static class RHIImpl
{
    static public void Clear(this RHI rhi)
    {
        rhi.gl.Clear((uint)ClearBufferMask.ColorBufferBit);
    }
    static public void DrawMesh(this RHI rhi, Mesh mesh, Shader shader)
    {
        rhi.gl.BindVertexArray(mesh.vao);
        rhi.gl.UseProgram(shader.handle);
        unsafe
        {
            rhi.gl.DrawElements(PrimitiveType.Triangles, (uint)mesh.verticesCount, DrawElementsType.UnsignedInt, null);
        }
    }
}
public class Shader
{
    public string vertexCode;
    public string fragmentCode;
    public uint handle;
    public Shader(string vertexCode, string fragmentCode)
    {
        this.vertexCode = vertexCode;
        this.fragmentCode = fragmentCode;
        var gl = App.gl;
        //Creating a vertex shader.
        uint vertexShader = gl.CreateShader(ShaderType.VertexShader);
        gl.ShaderSource(vertexShader, vertexCode);
        gl.CompileShader(vertexShader);

        //Checking the shader for compilation errors.
        string infoLog = gl.GetShaderInfoLog(vertexShader);
        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            Console.WriteLine($"Error compiling vertex shader {infoLog}");
        }

        //Creating a fragment shader.
        uint fragmentShader = gl.CreateShader(ShaderType.FragmentShader);
        gl.ShaderSource(fragmentShader, fragmentCode);
        gl.CompileShader(fragmentShader);

        //Checking the shader for compilation errors.
        infoLog = gl.GetShaderInfoLog(fragmentShader);
        if (!string.IsNullOrWhiteSpace(infoLog))
        {
            Console.WriteLine($"Error compiling fragment shader {infoLog}");
        }

        //Combining the shaders under one shader program.
        handle = gl.CreateProgram();
        gl.AttachShader(handle, vertexShader);
        gl.AttachShader(handle, fragmentShader);
        gl.LinkProgram(handle);

        //Checking the linking for errors.
        gl.GetProgram(handle, GLEnum.LinkStatus, out var status);
        if (status == 0)
        {
            Console.WriteLine($"Error linking shader {gl.GetProgramInfoLog(handle)}");
        }

        //Delete the no longer useful individual shaders;
        gl.DetachShader(handle, vertexShader);
        gl.DetachShader(handle, fragmentShader);
        gl.DeleteShader(vertexShader);
        gl.DeleteShader(fragmentShader);
        unsafe
        {
            //Tell opengl how to give the data to the shaders.
            gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);
        }
        gl.EnableVertexAttribArray(0);
    }
    public void Release()
    {
        App.gl.DeleteProgram(handle);
    }
    public void SetUniform(string name, int value)
    {
        int location = App.gl.GetUniformLocation(handle, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        App.gl.Uniform1(location, value);
    }
    public unsafe void SetUniform(string name, Matrix4x4 value)
    {
        //A new overload has been created for setting a uniform so we can use the transform in our shader.
        int location = App.gl.GetUniformLocation(handle, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        App.gl.UniformMatrix4(location, 1, false, (float*)&value);
    }

    public void SetUniform(string name, float value)
    {
        int location = App.gl.GetUniformLocation(handle, name);
        if (location == -1)
        {
            throw new Exception($"{name} uniform not found on shader.");
        }
        App.gl.Uniform1(location, value);
    }
}
public class Mesh
{
    public uint vao;
    public uint vbo;
    public uint ebo;
    public float[] vertices;
    public uint[] indices;
    public int verticesCount => vertices.Length;
    public int indicesCount => indices.Length;
    public Mesh(float[] vertices, uint[] indices)
    {
        this.vertices = vertices;
        this.indices = indices;
        var gl = App.gl;
        vao = gl.GenVertexArray();
        gl.BindVertexArray(vao);
        vbo = gl.GenBuffer();
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
        unsafe
        {
            fixed (void* v = &vertices[0])
            {
                gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(uint)), v, BufferUsageARB.StaticDraw);
            }
        }
        ebo = gl.GenBuffer();
        gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);
        unsafe
        {
            fixed (void* i = &indices[0])
            {
                gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), i, BufferUsageARB.StaticDraw);
            }
        }
    }
    public void Release()
    {
        var gl = App.gl;
        gl.DeleteBuffer(vbo);
        gl.DeleteBuffer(ebo);
        gl.DeleteVertexArray(vao);
    }
}