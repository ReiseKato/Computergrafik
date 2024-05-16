using System;
using GLFW;
using static OpenGL.GL;
using System.Numerics;

namespace MarchingCubes_cs
{
    class Program
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Hello World.");
            
            VoxelReader voxelReader = new VoxelReader();
            voxelReader.Start();

            var filePath = @"C:\Users\reise\code\Computergrafik\MarchingCubes_cs\MarchingCubes_cs\frog\Frog.vol";
            VoxelData vd = VoxelFileReader.ReadVoxelFile(filePath);
            Console.WriteLine(vd.data[100,50,100]);

            List<Vector3> mesh = MarchingCubes.GenerateMesh1(vd);

            string combinedString = string.Join(",", mesh);
            Console.WriteLine(combinedString);
            //Console.WriteLine(MarchingCubesTable.EdgeTable[0, 1]);
        }
    }
}