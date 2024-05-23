using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarchingCubes_cs
{
    public class Window : GameWindow
    {
        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private Shader _shader;
        //private float[] _vertices =
        //{
        //    0.25f, 0.25E-6f, 0.0f, // Bottom-left vertex
        //    0.5f, 0.25f, 0.0f,  // Bottom-right vertex
        //    0.5f, 0.5f, 0.0f,   // Top vertex
        //    0.5f, 0.5f, 0.0f,
        //    0.75f, 0.5f, 0.0f,
        //    0.6f, 0.75f, 0.0f
        //};
        private float[] _vertices;


        public Window(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings()
        {
            Size = (width, height),
            Title = title
        })
        { }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(OpenTK.Mathematics.Color4.White); // background color

            //float[] new_vert =
            //{
            //    -2.2118915E-23f, -1.868906E+16f, 5.3367E-39f,
            //    -2.1166514E-23f, -1.8689086E+16f, 6.684014E-39f,
            //    -2.1166514E-23f, -2.9214505E+16f, 5.336694E-39f
            //};
            //
            //_vertices = new_vert;

            // Init code
            // get Mesh from Marching Cube Algorithm and save it to _vertices
            VoxelReader voxelReader = new VoxelReader();
            voxelReader.Start();

            var filePath = @"C:\Users\reise\code\Computergrafik\MarchingCubes_cs\MarchingCubes_cs\frog\Frog.vol";
            VoxelData vd = VoxelFileReader.ReadVoxelFile(filePath);

            List<float> mesh = MarchingCubes.GenerateMeshVis(vd, 200);
            _vertices = mesh.ToArray();

            // Vertex Buffer Object (VBO)
            // 1. Create Buffer
            // 2. Bind buffer -> modification to VBO will be applied to this buffer until another buffer is bound instead
            // 3. Upload Vertices to Buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            // Vertex Array Object (VAO)
            // Interpret 12 Bytes as 3 floats and divide the buffer into vertices
            // Create and Bind a VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            // How should Vertex Shader interpret the VB data?
            // Tell OpenGL about format of the data and associate the current array buffer and interpret it in the specified way
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // Shader
            // Vertex-to-Pixel pipeline
            // Shader code files in bin/net8.0
            // global -> every function that uses shader modifies this shader until a new one is bound instead
            _shader = new Shader("shader.vert", "shader.frag");
            _shader.Use();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _shader.Use();
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count() / 3);
            Context.SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnUnload()
        {
            // Unbind all resources by binding the targets to 0/null
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            _shader.Dispose();
            GL.UseProgram(0);

            // Delete all resources
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteProgram(_shader.Handler);

            base.OnUnload();
        }
    }
}
