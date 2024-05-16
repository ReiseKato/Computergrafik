using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MarchingCubes_cs
{
    public class VoxelReader
    {
        public string Filename;

        public float LowerCutoff = 100;
        public int Slice = 100;

        public float scale = 0.01f;

        public async void Start()
        {
            // Data from https://web.cs.ucdavis.edu/~okreylos/PhDStudies/Spring2000/ECS277/DataSets.html
            var filePath = @"C:\Users\reise\code\Computergrafik\MarchingCubes_cs\MarchingCubes_cs\frog\Frog.vol";
            var voxelData = VoxelFileReader.ReadVoxelFile(filePath);
            ShowRange(voxelData);

            // ShowSlice(voxelData);
        }

        

        static void ShowRange(VoxelData voxelData)
        {
            float min = 999, max = 0;
            for (int x = 0; x < voxelData.numX; x++)
                for (int y = 0; y < voxelData.numY; y++)
                    for (int z = 0; z < voxelData.numZ; z++)
                    {
                        var val = voxelData.data[x, y, z];
                        if (val > max) max = val;
                        if (val < min) min = val;
                    }
            Console.WriteLine($"Min: {min}, Max: {max}");
            Console.WriteLine($"NumX: {voxelData.numX}, NumY: {voxelData.numY}, NumZ: {voxelData.numZ}");
        }
    }
}
