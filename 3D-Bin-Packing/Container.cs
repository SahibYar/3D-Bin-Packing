using System;

namespace _3D_Bin_Packing
{
    class Containers
    {
        #region data members
        private String c_id;
        private Int32 c_Length;
        private Int32 c_minLength;
        private Int32 c_maxLength;
        private Int32 c_stepLength;

        private Int32 c_Width;
        private Int32 c_minWidth;
        private Int32 c_maxWidth;
        private Int32 c_stepWidth;

        private Int32 c_Height;
        private Int32 c_minHeight;
        private Int32 c_maxHeight;
        private Int32 c_stepHeight;

        private Double c_maxWeight;
        private Int32 c_maxCount;
        #endregion

        #region properties
        public String ContainerID
        {
            get { return this.c_id; }
            set { this.c_id = value; }
        }

        public Int32 Length
        {
            get { return this.c_Length; }
            set { this.c_Length = value; }
        }

        public Int32 MinLength
        {
            get { return this.c_maxLength; }
            set { this.c_maxLength = value; }
        }

        public Int32 MaxLength
        {
            get { return this.c_maxLength; }
            set { this.c_maxLength = value; }
        }

        public Int32 StepLenght
        {
            get { return this.c_stepLength; }
            set { this.c_stepLength = value; }
        }

        public Int32 Width
        {
            get { return this.c_Width; }
            set { this.c_Width = value; }
        }

        public Int32 MinWidth
        {
            get { return this.c_minWidth; }
            set { this.c_minWidth = value; }
        }

        public Int32 MaxWidth
        {
            get { return this.c_maxWidth; }
            set { this.c_maxWidth = value; }
        }

        public Int32 StepWidth
        {
            get { return this.c_stepWidth; }
            set { this.c_stepWidth = value; }
        }

        public Int32 Height
        {
            get { return this.c_Height; }
            set { this.c_Height = value; }
        }

        public Int32 MinHeight
        {
            get { return this.c_minHeight; }
            set { this.c_minHeight = value; }
        }

        public Int32 MaxHeight
        {
            get { return this.c_maxHeight; }
            set { this.c_maxHeight = value; }
        }

        public Int32 StepHeight
        {
            get { return this.c_stepHeight; }
            set { this.c_maxHeight = value; }
        }

        public Double MaxWeight
        {
            get { return this.c_maxWeight; }
            set { this.c_maxWeight = value; }
        }

        public  Int32 MaxCount
        {
            get { return this.c_maxCount; }
            set { this.c_maxCount = value; }
        }
        #endregion
    }
}