using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using static OpenTK.Graphics.OpenGL.GL;

namespace MarchingCubes_cs
{
    public class MarchingCubes
    {
        /**
         * generate a Mesh of Triangles
         */
        public static List<Vector3> GenerateMesh(VoxelData voxelData, int isolevel)
        {            
            List<Vector3> mesh = new List<Vector3>();
            List<float> fMesh = new List<float>();
            mesh.Clear();

            // Loop through each voxel in data
            for (int x = 0; x < voxelData.numX - 1; x++)
            {
                for (int y = 0; y < voxelData.numY - 1; y++)
                {
                    for (int z = 0; z < voxelData.numZ - 1; z++)
                    {
                        // create a cube from voxels -> x, y, z+1; x, y+1, z; ...
                        //      -> marching voxels
                        // Collect cube corner values
                        byte[] cubes = new byte[8];
                        cubes[0] = voxelData.data[x, y, z];
                        cubes[1] = voxelData.data[x + 1, y, z];
                        cubes[2] = voxelData.data[x, y + 1, z];
                        cubes[3] = voxelData.data[x + 1, y + 1, z];
                        cubes[4] = voxelData.data[x, y, z + 1];
                        cubes[5] = voxelData.data[x + 1, y, z + 1];
                        cubes[6] = voxelData.data[x, y + 1, z + 1];
                        cubes[7] = voxelData.data[x + 1, y + 1, z + 1];

                        // Determine the Cubeindex for the current voxel
                        int cubeindex = 0;
                        for (int i = 0; i < 8; i++)
                        {
                            if (cubes[i] > isolevel)
                            {
                                cubeindex |= (1 << i);
                            }
                        }
                        for (int i = 0; MarchingCubesTable.TriangleTable[cubeindex, i] != -1; i+=3)
                        {
                            int edgeIndex0 = MarchingCubesTable.TriangleTable[cubeindex, i];
                            int edgeIndex1 = MarchingCubesTable.TriangleTable[cubeindex, i + 1];
                            int edgeIndex2 = MarchingCubesTable.TriangleTable[cubeindex, i + 2];

                            Vector3 vertex0 = InterpolateVertex(x, y, z, edgeIndex0, voxelData);
                            Vector3 vertex1 = InterpolateVertex(x, y, z, edgeIndex1, voxelData);
                            Vector3 vertex2 = InterpolateVertex(x, y, z, edgeIndex2, voxelData);

                            if(!(float.IsNaN(vertex0.X) || float.IsNaN(vertex1.X) || float.IsNaN(vertex2.X)))
                            {
                                mesh.Add(vertex0);
                                mesh.Add(vertex1);
                                mesh.Add(vertex2);

                                fMesh.Add(vertex0.X);
                                fMesh.Add(vertex0.Y);
                                fMesh.Add(vertex0.Z);
                                fMesh.Add(vertex1.X);
                                fMesh.Add(vertex1.Y);
                                fMesh.Add(vertex1.Z);
                                fMesh.Add(vertex2.X);
                                fMesh.Add(vertex2.Y);
                                fMesh.Add(vertex2.Z);
                            }
                            
                            //Console.WriteLine(vertex0);
                            //Console.WriteLine(vertex1);
                            //Console.WriteLine(vertex2);
                        }
                    }
                }
            }

            return mesh;
        }

        private static Vector3 InterpolateVertex(int x, int y, int z, int edgeIndex, VoxelData voxelData)
        {
            // Bestimme die Endpunkte der Kante basierend auf dem edgeIndex
            int x0 = x, y0 = y, z0 = z;
            int x1 = x, y1 = y, z1 = z;

            switch (edgeIndex)
            {
                case 0: x1 += 1; break;
                case 1: x0 += 1; x1 += 1; z1 += 1; break;
                case 2: x0 += 1; z0 += 1; z1 += 1; break;
                case 3: z1 += 1; break;
                case 4: y0 += 1; y1 += 1; x1 += 1; break;
                case 5: y0 += 1; x0 += 1; y1 += 1; x1 += 1; z1 += 1; break;
                case 6: y0 += 1; x0 += 1; y1 += 1; z0 += 1; z1 += 1; break;
                case 7: y0 += 1; y1 += 1; z1 += 1; break;
                case 8: y1 += 1; break;
                case 9: x0 += 1; x1 += 1; y1 += 1; break;
                case 10: x0 += 1; z0 += 1; z1 += 1; y1 += 1; break;
                case 11: z0 += 1; z1 += 1; y1 += 1; break;
            }

            // Überprüfe, ob die Endpunkte der Kante innerhalb der Voxel-Daten liegen
            if (x0 < 0 || x0 >= voxelData.numX ||
                y0 < 0 || y0 >= voxelData.numY ||
                z0 < 0 || z0 >= voxelData.numZ ||
                x1 < 0 || x1 >= voxelData.numX ||
                y1 < 0 || y1 >= voxelData.numY ||
                z1 < 0 || z1 >= voxelData.numZ)
            {
                return Vector3.Zero;
            }

            // Interpoliere den Vertex entlang der Kante basierend auf den Werten an den Endpunkten
            float value0 = voxelData.data[x0, y0, z0];
            float value1 = voxelData.data[x1, y1, z1];

            float t = value0 / (value0 - value1);

            Vector3 vertex0 = new Vector3(
                x0 * voxelData.sizeX,
                y0 * voxelData.sizeY,
                z0 * voxelData.sizeZ
            );

            Vector3 vertex1 = new Vector3(
                x1 * voxelData.sizeX,
                y1 * voxelData.sizeY,
                z1 * voxelData.sizeZ
            );

            return Vector3.Lerp(vertex0, vertex1, t);
        }






        // For Visualisation (ChatGPT)
        public static List<float> GenerateMeshVis(VoxelData voxelData, int isolevel)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<float> fMesh = new List<float>();

            for (int x = 0; x < voxelData.numX - 1; x++)
            {
                for (int y = 0; y < voxelData.numY - 1; y++)
                {
                    for (int z = 0; z < voxelData.numZ - 1; z++)
                    {
                        byte[] cubes = new byte[8];
                        cubes[0] = voxelData.data[x, y, z];
                        cubes[1] = voxelData.data[x + 1, y, z];
                        cubes[2] = voxelData.data[x + 1, y, z + 1];
                        cubes[3] = voxelData.data[x, y, z + 1];
                        cubes[4] = voxelData.data[x, y + 1, z];
                        cubes[5] = voxelData.data[x + 1, y + 1, z];
                        cubes[6] = voxelData.data[x + 1, y + 1, z + 1];
                        cubes[7] = voxelData.data[x, y + 1, z + 1];

                        int cubeindex = 0;
                        if (cubes[0] > isolevel) cubeindex |= 1;
                        if (cubes[1] > isolevel) cubeindex |= 2;
                        if (cubes[2] > isolevel) cubeindex |= 4;
                        if (cubes[3] > isolevel) cubeindex |= 8;
                        if (cubes[4] > isolevel) cubeindex |= 16;
                        if (cubes[5] > isolevel) cubeindex |= 32;
                        if (cubes[6] > isolevel) cubeindex |= 64;
                        if (cubes[7] > isolevel) cubeindex |= 128;

                       
                        for (int i = 0; MarchingCubesTable.TriangleTable[cubeindex, i] != -1; i += 3)
                        {
                            Vector3 vertex0 = InterpolateVertexVis(x, y, z, MarchingCubesTable.TriangleTable[cubeindex, i], cubes, voxelData);
                            Vector3 vertex1 = InterpolateVertexVis(x, y, z, MarchingCubesTable.TriangleTable[cubeindex, i + 1], cubes, voxelData);
                            Vector3 vertex2 = InterpolateVertexVis(x, y, z, MarchingCubesTable.TriangleTable[cubeindex, i + 2], cubes, voxelData);

                            if (!(float.IsNaN(vertex0.X) || float.IsNaN(vertex1.X) || float.IsNaN(vertex2.X)))
                            {
                                vertices.Add(vertex0);
                                vertices.Add(vertex1);
                                vertices.Add(vertex2);

                                fMesh.Add(vertex0.X);
                                fMesh.Add(vertex0.Y);
                                fMesh.Add(vertex0.Z);
                                fMesh.Add(vertex1.X);
                                fMesh.Add(vertex1.Y);
                                fMesh.Add(vertex1.Z);
                                fMesh.Add(vertex2.X);
                                fMesh.Add(vertex2.Y);
                                fMesh.Add(vertex2.Z);
                            }
                        }
                    }
                }
            }

            return fMesh;
        }

        private static Vector3 InterpolateVertexVis(int x, int y, int z, int edgeIndex, byte[] cubes, VoxelData voxelData)
        {
            int cornerA = MarchingCubesTable.EdgeIndices[edgeIndex, 0];
            int cornerB = MarchingCubesTable.EdgeIndices[edgeIndex, 1];

            Vector3 cornerPosA = GetCornerPosition(x, y, z, cornerA, voxelData);
            Vector3 cornerPosB = GetCornerPosition(x, y, z, cornerB, voxelData);

            float valueA = cubes[cornerA];
            float valueB = cubes[cornerB];

            float t = valueA / (valueA - valueB);

            return Vector3.Lerp(cornerPosA, cornerPosB, t);
        }

        private static Vector3 GetCornerPosition(int x, int y, int z, int cornerIndex, VoxelData voxelData)
        {
            int[,] cornerOffsets = new int[8, 3]
            {
            {0, 0, 0},
            {1, 0, 0},
            {1, 0, 1},
            {0, 0, 1},
            {0, 1, 0},
            {1, 1, 0},
            {1, 1, 1},
            {0, 1, 1}
            };

            int[] offset = new int[3] { cornerOffsets[cornerIndex, 0], cornerOffsets[cornerIndex, 1], cornerOffsets[cornerIndex, 2] };
            return new Vector3(
                (x + offset[0]) * voxelData.sizeX,
                (y + offset[1]) * voxelData.sizeY,
                (z + offset[2]) * voxelData.sizeZ
            );
        }
    }
}
