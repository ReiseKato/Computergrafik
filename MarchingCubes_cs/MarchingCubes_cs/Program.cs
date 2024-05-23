using System;
using System.Numerics;
using System.Diagnostics;


namespace MarchingCubes_cs
{
    class Program
    {
        static void Main(string[] args) 
        {            
            //VoxelReader voxelReader = new VoxelReader();
            //voxelReader.Start();
            //
            //var filePath = @"C:\Users\reise\code\Computergrafik\MarchingCubes_cs\MarchingCubes_cs\frog\Frog.vol";
            //VoxelData vd = VoxelFileReader.ReadVoxelFile(filePath);
            //
            ////List<Vector3> mesh = MarchingCubes.GenerateMesh(vd, 200);
            ////List<Vector3> mesh = MarchingCubes.GenerateMeshVis(vd, 200);
            //List<float> mesh = MarchingCubes.GenerateMeshVis(vd, 200);
            //
            ////string combinedString = string.Join(",", mesh);
            ////Console.WriteLine(combinedString);
            ////Console.WriteLine(string.Join(",", vertices));
            //Debug.Assert(mesh.Count() > 0);
            //Debug.Assert(mesh.Count()%3 == 0);

            // create Window inside using -> automatic disposal after exit
            using (Window window = new Window(2300, 1400, "Frog.vol"))
            {
                window.Run();
            }




        }
    }
}