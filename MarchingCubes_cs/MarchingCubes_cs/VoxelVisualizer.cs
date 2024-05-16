using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MarchingCubes_cs
{
    public class VoxelVisualizer
    {
        private GLFW.Window window;
        private List<Vector3> mesh;

        public VoxelVisualizer(List<Vector3> mesh)
        {
            this.mesh = mesh;
            
        }

        private void GlfwInit()
        {
            


        }
    }
}
