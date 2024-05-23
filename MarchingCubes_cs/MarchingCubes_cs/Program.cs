using System;
using GLFW;
using OpenGL;
using System.Numerics;
using System.Diagnostics;
using static OpenTK.Graphics.OpenGL.GL;

namespace MarchingCubes_cs
{
    class Program
    {
        static void Main(string[] args) 
        {            
            VoxelReader voxelReader = new VoxelReader();
            voxelReader.Start();

            var filePath = @"C:\Users\reise\code\Computergrafik\MarchingCubes_cs\MarchingCubes_cs\frog\Frog.vol";
            VoxelData vd = VoxelFileReader.ReadVoxelFile(filePath);

            //List<Vector3> mesh = MarchingCubes.GenerateMesh(vd, 200);
            List<Vector3> mesh = MarchingCubes.GenerateMeshVis(vd, 200);

            //string combinedString = string.Join(",", mesh);
            //Console.WriteLine(combinedString);
            //Console.WriteLine(string.Join(",", vertices));
            Debug.Assert(mesh.Count() > 0);
            Debug.Assert(mesh.Count()%3 == 0);

            using (WindowTK window = new WindowTK(800, 600, "LearnOpenTK"))
            {
                window.Run();
                for (int i = 0; i < mesh.Count; i+=3)
                {
                    float[] vertices = {
                         mesh[i].X, mesh[i].Y, mesh[i].Z, //Bottom-left vertex
                         mesh[i+1].X, mesh[i+1].Y, mesh[i+1].Z, //Bottom-right vertex
                         mesh[i+2].X, mesh[i+2].Y, mesh[i+2].Z  //Top vertex
                    };
                    vertices.ToList().ForEach(i => Console.WriteLine(i.ToString()));
                    Console.WriteLine("\n");
                    // do visualisation for voxels
                }

            }


            

        }
    }
}