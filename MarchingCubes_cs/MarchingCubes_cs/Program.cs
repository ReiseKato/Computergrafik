using System;
using System.Numerics;
using System.Diagnostics;


namespace MarchingCubes_cs
{
    class Program
    {
        static void Main(string[] args) 
        {
            /**
             Problems:  Values are smaller than -1.0f or greater than 1.0f -> can't visualize these
             */
            var filePath = @"C:\Users\reise\code\Computergrafik\MarchingCubes_cs\MarchingCubes_cs\frog\Frog.vol";
            VoxelData vd = VoxelFileReader.ReadVoxelFile(filePath);

            List<Vector3> mesh = MarchingCubes.GenerateMesh(vd, 200);
         
            List<float> fMesh = MarchingCubes.GenerateMeshVis(vd, 200);
            
            string combinedString = string.Join(",", fMesh);
            Console.WriteLine(combinedString);
            //Console.WriteLine(mesh.Count() * 3);
            //Console.WriteLine(fMesh.Count());
            //Debug.Assert(mesh.Count() > 0);
            //Debug.Assert(mesh.Count()%3 == 0);

            // create Window inside using -> automatic disposal after exit
            //using (Window window = new Window(2300, 1400, "Frog.vol"))
            //{
            //    window.Run();
            //}




        }
    }
}