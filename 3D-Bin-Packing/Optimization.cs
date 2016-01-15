using System;

namespace _3D_Bin_Packing
{
    class Optimization
    {
        private String id;
        private String type;

        public String OptimizationID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public String OptimizationType
        {
            get { return this.type; }
            set { this.type = value; }
        }
    }
}
