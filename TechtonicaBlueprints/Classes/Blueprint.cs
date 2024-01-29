using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechtonicaBlueprints.Classes
{
    public class Blueprint
    {
        public int id = -1;
        public int parentID;
        public string name;
        public MyVector3 size;
        public int rotation = 0;
        public List<uint> machineIDs = new List<uint>();
        public List<MyVector3> machineRelativePositions = new List<MyVector3>();
    }

    public class MyVector3 {
        public float x, y, z;
    }
}
