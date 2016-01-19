using System;

namespace _3D_Bin_Packing
{
    class Box
    {
        #region Private Data Members
        private String b_id;
        private Int32 b_quantity;
        private Int32 b_length;
        private Int32 b_width;
        private Int32 b_height;
        private Double b_weight;
        private Boolean b_allowedRotation_x;
        private Boolean b_allowedRotation_y;
        private Boolean b_allowedRotation_z;
        private Boolean b_toponly;
        private Boolean b_bottomonly;
        private Boolean b_is_placed;
        #endregion

        #region Functions

        //Returns true if Box i > Box j else false
        public static Boolean compareVolume(Box i, Box j)
        {
            return ((i.Height * i.Width * i.Length) > (j.Height * j.Width * j.Length));
        }

        //returns volume of a box;
        public Double Volume ()
        {
            return Height * Width * Length;
        }
        #endregion

        #region Properties
        public Boolean IsPlaced
        {
            get { return this.b_is_placed; }
            set { this.b_is_placed = value; }
        }
        public String BoxID
        {
            get { return this.b_id; }
            set { this.b_id = value; }
        }

        public Int32 Quantity
        {
            get { return this.b_quantity; }
            set { this.b_quantity = value; }
        }

        public Int32 Length
        {
            get { return this.b_length; }
            set { this.b_length = value; }
        }

        public Int32 Width
        {
            get { return this.b_width; }
            set { this.b_width = value; }
        }

        public Int32 Height
        {
            get { return this.b_height; }
            set { this.b_height = value; }
        }

        public Double Weight
        {
            get { return this.b_weight; }
            set { this.b_weight = value; }
        }

        public Boolean TopOnly
        {
            get { return this.b_toponly; }
            set { this.b_toponly = value; }
        }

        public Boolean BottomOnly
        {
            get { return this.b_bottomonly; }
            set { this.b_bottomonly = value; }
        }

        public Boolean AllowedRotationsX
        {
            get { return this.b_allowedRotation_x; }
            set { this.b_allowedRotation_x = value; }
        }

        public Boolean AllowedRotationsY
        {
            get { return this.b_allowedRotation_y; }
            set { this.b_allowedRotation_y = value; }
        }

        public Boolean AllowedRotationsZ
        {
            get { return this.b_allowedRotation_z; }
            set { this.b_allowedRotation_z = value; }
        }
        #endregion
    }
}
