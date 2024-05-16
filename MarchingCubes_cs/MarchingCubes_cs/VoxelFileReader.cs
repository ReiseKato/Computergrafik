using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarchingCubes_cs
{
    public static class VoxelFileReader
    {
        public static VoxelData ReadVoxelFile(String path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                using (var br = new BigEndianBinaryReader(fs))
                {
                    var vd = new VoxelData();

                    // Read resolution
                    vd.numX = br.ReadInt32();
                    vd.numY = br.ReadInt32();
                    vd.numZ = br.ReadInt32();
                    // Read size of saved border (not used)
                    vd.borderSize = br.ReadInt32();

                    // Read true size
                    vd.sizeX = br.ReadSingle();
                    vd.sizeY = br.ReadSingle();
                    vd.sizeZ = br.ReadSingle();

                    // Read data values
                    int totalValues = vd.numX * vd.numY * vd.numZ;
                    byte[] dataValues = br.ReadBytes(totalValues);

                    // Convert to multidimensional array
                    vd.data = new byte[vd.numX, vd.numY, vd.numZ];
                    int index = 0;
                    for (int x = 0; x < vd.numX; x++)
                    {
                        for (int y = 0; y < vd.numY; y++)
                        {
                            for (int z = 0; z < vd.numZ; z++)
                            {
                                vd.data[x, y, z] = dataValues[index++];
                            }
                        }
                    }

                    return vd;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error reading voxel data.", e);
            }
        }
    }
}
