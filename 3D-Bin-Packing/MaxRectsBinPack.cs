using System;
using System.Collections.Generic;

namespace _3D_Bin_Packing
{
    class MaxRectsBinPack
    {
        public Int32 boxWidth = 0;
        public Int32 boxLength = 0;
        public Int32 boxHeight = 0;

        public Boolean allowRotations;

        public List<Containers> usedContainers = new List<Containers>();
        public List<Containers> freeContainers = new List<Containers>();

        public enum FreeContianerChoiceHeuristic
        {
            /// <summary>
            /// BSSF: Positions the Box against the short side of a free container into which it fits the best.
            /// </summary>
            ContainerBestShortSideFit,

            /// <summary>
            /// BLSF: Positions the Box against the long side of a free container into which it fits the best.
            /// </summary>
            ContainerBestLongSideFit,

            /// <summary>
            /// BAF: Positions the Box into the smallest free container into which it fits.
            /// </summary>
            ContainerBestVolumeFit,

            /// <summary>
            /// BL: Does the tetris placement.
            /// </summary>
            ContainerBottomLeftRule,

            /// <summary>
            /// CP: Chooses the placement where the Box touches other Containers/Box as much as possible.
            /// </summary>
            ContainerContactPointRule
        }

        public MaxRectsBinPack(Int32 width, Int32 length, Int32 height, Boolean rotations = true)
        {
            Init(width, length, height, rotations);
        }

        public void Init (Int32 width, Int32 length, Int32 height, Boolean rotations = true)
        {
            boxWidth = width;
            boxHeight = height;
            allowRotations = rotations;

            Containers n = new Containers();
            n.Origin.X = 0;
            n.Origin.Y = 0;
            n.Origin.Z = 0;

            n.Width = width;
            n.Height = height;
            n.Length = length;

            usedContainers.Clear();
            freeContainers.Clear();
            freeContainers.Add(n);
        }

        public 
    }
}
