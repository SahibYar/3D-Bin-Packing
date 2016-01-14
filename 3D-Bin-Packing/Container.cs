using System;

namespace _3D_Bin_Packing
{
    class Containers
    {
        #region data members
        private String c_id;
        private float c_Length;
        private float c_minLength;
        private float c_maxLength;
        private float c_stepLength;

        private float c_Width;
        private float c_minWidth;
        private float c_maxWidth;
        private float c_stepWidth;

        private float c_Height;
        private float c_minHeight;
        private float c_maxHeight;
        private float c_stepHeight;

        private float c_maxWeight;
        private Int32 c_maxCount;
        #endregion

        #region properties
        public String ContainerID
        {
            get { return this.c_id; }
            set { this.c_id = value; }
        }

        public float Length
        {
            get { return this.c_Length; }
            set { this.c_Length = value; }
        }

        public float MinLength
        {
            get { return this.c_maxLength; }
            set { this.c_maxLength = value; }
        }

        public float MaxLength
        {
            get { return this.c_maxLength; }
            set { this.c_maxLength = value; }
        }

        public float StepLenght
        {
            get { return this.c_stepLength; }
            set { this.c_stepLength = value; }
        }

        public float Width
        {
            get { return this.c_Width; }
            set { this.c_Width = value; }
        }

        public float MinWidth
        {
            get { return this.c_minWidth; }
            set { this.c_minWidth = value; }
        }

        public float MaxWidth
        {
            get { return this.c_maxWidth; }
            set { this.c_maxWidth = value; }
        }

        public float StepWidth
        {
            get { return this.c_stepWidth; }
            set { this.c_stepWidth = value; }
        }

        public float Height
        {
            get { return this.c_Height; }
            set { this.c_Height = value; }
        }

        public float MinHeight
        {
            get { return this.c_minHeight; }
            set { this.c_minHeight = value; }
        }

        public float MaxHeight
        {
            get { return this.c_maxHeight; }
            set { this.c_maxHeight = value; }
        }

        public float StepHeight
        {
            get { return this.c_stepHeight; }
            set { this.c_maxHeight = value; }
        }

        public float MaxWeight
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