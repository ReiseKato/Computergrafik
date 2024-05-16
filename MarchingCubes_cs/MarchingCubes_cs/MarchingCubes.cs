using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MarchingCubes_cs
{
    public class MarchingCubes
    {
        /**
         * generate a Mesh of Triangles
         */
        public static List<Vector3> GenerateMesh1(VoxelData voxelData, int isolevel)
        {            
            List<Vector3> mesh = new List<Vector3>();
            mesh.Clear();

            // Loop through each voxel in data
            for (int x = 0; x < voxelData.numX - 1; x++)
            {
                for (int y = 0; y < voxelData.numY - 1; y++)
                {
                    for (int z = 0; z < voxelData.numZ - 1; z++)
                    {
                        byte[] cubes = new byte[8];

                        // create a cube from voxels -> x, y, z+1; x, y+1, z; ...
                        //      -> marching voxels
                        cubes[0] = voxelData.data[x, y, z];
                        cubes[1] = voxelData.data[x + 1, y, z];
                        cubes[2] = voxelData.data[x, y + 1, z];
                        cubes[3] = voxelData.data[x + 1, y + 1, z];
                        cubes[4] = voxelData.data[x, y, z + 1];
                        cubes[5] = voxelData.data[x + 1, y, z + 1];
                        cubes[6] = voxelData.data[x, y + 1, z + 1];
                        cubes[7] = voxelData.data[x + 1, y + 1, z + 1];

                        int cubeindex = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            if (cubes[i] >= isolevel) cubeindex |= 1 << 1;
                        }
                        for (int i = 0; MarchingCubesTable.TriangleTable[cubeindex, i] != -1; i+=3)
                        {
                            int edgeIndex0 = MarchingCubesTable.TriangleTable[cubeindex, i];
                            int edgeIndex1 = MarchingCubesTable.TriangleTable[cubeindex, i + 1];
                            int edgeIndex2 = MarchingCubesTable.TriangleTable[cubeindex, i + 2];

                            Vector3 vertex0 = InterpolateVertex(x, y, z, edgeIndex0, voxelData);
                            Vector3 vertex1 = InterpolateVertex(x, y, z, edgeIndex1, voxelData);
                            Vector3 vertex2 = InterpolateVertex(x, y, z, edgeIndex2, voxelData);

                            mesh.Add(vertex0);
                            mesh.Add(vertex1);
                            mesh.Add(vertex2);
                        }
                    }
                }
            }

            return mesh;
        }

        private static Vector3 InterpolateVertex(int x, int y, int z, int edgeIndex, VoxelData voxelData)
        {
            // Bestimme die Endpunkte der Kante basierend auf dem edgeIndex
            int edgeX0, edgeY0, edgeZ0, edgeX1, edgeY1, edgeZ1;

            switch (edgeIndex)
            {
                case 0: edgeX0 = x; edgeY0 = y; edgeZ0 = z; edgeX1 = x + 1; edgeY1 = y; edgeZ1 = z; break;
                case 1: edgeX0 = x + 1; edgeY0 = y; edgeZ0 = z; edgeX1 = x + 1; edgeY1 = y; edgeZ1 = z + 1; break;
                case 2: edgeX0 = x + 1; edgeY0 = y; edgeZ0 = z + 1; edgeX1 = x; edgeY1 = y; edgeZ1 = z + 1; break;
                case 3: edgeX0 = x; edgeY0 = y; edgeZ0 = z + 1; edgeX1 = x; edgeY1 = y; edgeZ1 = z; break;
                case 4: edgeX0 = x; edgeY0 = y + 1; edgeZ0 = z; edgeX1 = x + 1; edgeY1 = y + 1; edgeZ1 = z; break;
                case 5: edgeX0 = x + 1; edgeY0 = y + 1; edgeZ0 = z; edgeX1 = x + 1; edgeY1 = y + 1; edgeZ1 = z + 1; break;
                case 6: edgeX0 = x + 1; edgeY0 = y + 1; edgeZ0 = z + 1; edgeX1 = x; edgeY1 = y + 1; edgeZ1 = z + 1; break;
                case 7: edgeX0 = x; edgeY0 = y + 1; edgeZ0 = z + 1; edgeX1 = x; edgeY1 = y + 1; edgeZ1 = z; break;
                case 8: edgeX0 = x; edgeY0 = y; edgeZ0 = z; edgeX1 = x; edgeY1 = y + 1; edgeZ1 = z; break;
                case 9: edgeX0 = x + 1; edgeY0 = y; edgeZ0 = z; edgeX1 = x + 1; edgeY1 = y + 1; edgeZ1 = z; break;
                case 10: edgeX0 = x + 1; edgeY0 = y; edgeZ0 = z + 1; edgeX1 = x + 1; edgeY1 = y + 1; edgeZ1 = z + 1; break;
                case 11: edgeX0 = x; edgeY0 = y; edgeZ0 = z + 1; edgeX1 = x; edgeY1 = y + 1; edgeZ1 = z + 1; break;
                default: return Vector3.Zero;
            }

            // Überprüfe, ob die Endpunkte der Kante innerhalb der Voxel-Daten liegen
            if (edgeX0 < 0 || edgeX0 >= voxelData.numX ||
                edgeY0 < 0 || edgeY0 >= voxelData.numY ||
                edgeZ0 < 0 || edgeZ0 >= voxelData.numZ ||
                edgeX1 < 0 || edgeX1 >= voxelData.numX ||
                edgeY1 < 0 || edgeY1 >= voxelData.numY ||
                edgeZ1 < 0 || edgeZ1 >= voxelData.numZ)
            {
                return Vector3.Zero;
            }

            // Interpoliere den Vertex entlang der Kante basierend auf den Werten an den Endpunkten
            float value0 = voxelData.data[edgeX0, edgeY0, edgeZ0];
            float value1 = voxelData.data[edgeX1, edgeY1, edgeZ1];

            float t = value0 / (value0 - value1);

            Vector3 vertex0 = new Vector3(
                edgeX0 * voxelData.sizeX,
                edgeY0 * voxelData.sizeY,
                edgeZ0 * voxelData.sizeZ
            );

            Vector3 vertex1 = new Vector3(
                edgeX1 * voxelData.sizeX,
                edgeY1 * voxelData.sizeY,
                edgeZ1 * voxelData.sizeZ
            );

            return Vector3.Lerp(vertex0, vertex1, t);
        }
    }
}
