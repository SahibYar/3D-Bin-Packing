using System;
using System.Collections.Generic;
using System.Linq;

namespace _3D_Bin_Packing
{
    class Guillotine3D
    {
        // Created a list of containers for holding all the container objects
        public static Dictionary<String, Containers> ContainerList = new Dictionary<String,Containers>();

        // Created a list of boxes for holding all the box objects
        public static Dictionary<String, Box> BoxList = new Dictionary<string, Box>();


        // list of the splitted containers from the given containers
        //Dictionary<Container id, list of sub containers splitted from it>
        Dictionary<String, List<Containers>> Split_Container_open_list = new Dictionary<string, List<Containers>>();

        // list of all the boxes in the given container.
        public static Dictionary<String, List<Box>> Container_Containing_Boxes = new Dictionary<string, List<Box>>();

        //sort the boxes in their descending volume
        List<KeyValuePair<String, Box>> sorted_box_List;
        
        /// <summary>
        /// This functions will find the smallest un-opened container from the container list.
        /// </summary>
        /// <returns>return the key of smallest un-opened container</returns>
        String find_smallest_unopen_container ()
        {
            Double volume = ContainerList.First().Value.Height * ContainerList.First().Value.Width * ContainerList.First().Value.Length;
            String key = ContainerList.First().Key;

            foreach (KeyValuePair<string,Containers> c in ContainerList)
            {
                if (volume > c.Value.Height * c.Value.Width * c.Value.Height && c.Value.Still_to_Open)
                {
                    volume = c.Value.Height * c.Value.Width * c.Value.Height;
                    key = c.Key;
                }
            }
            return key;
        }

        /*
        Fill the box with items in it not violating its maximum weight limit
        and other constraints
        */
        // This funtion will try to put the best possible object which can be fitted in the given container
        void fill_container(String key) //here the key will be of the smallest available container.
        {
            // Still to open = false
            // means that it is currenlty open.
            ContainerList[key].Still_to_Open = false;

            // added the currently opened container in its splitted container list as it is.
            Split_Container_open_list[key].Add(ContainerList[key]);

            // rearranging the boxes in descending order of its volume.
            re_arranging_boxes();

            foreach (KeyValuePair<String, Box> box in sorted_box_List)
            {
                if (box.Value.Quantity > 0)        // if the box is not yet placed.
                {
                    //to get the orientation in which the box will be placed in the given container
                    Boolean? RotationX = null;
                    Boolean? RotationY = null;
                    Boolean? RotationZ = null;
                    Int32? container_index = null;
                    Int32? orientation_case = null;

                    Point3D point = has_free_space(key, box.Value, out orientation_case, out container_index, out RotationX, out RotationY, out RotationZ);

                    // checks if the box could be contained in the given container.
                    if (point.X != float.NegativeInfinity && point.Y != float.NegativeInfinity && point.Z != float.NegativeInfinity && orientation_case.HasValue &&
                        RotationX.HasValue && RotationY.HasValue && RotationZ.HasValue && container_index.HasValue && has_free_weight(key, box.Value))
                    {
                        if (place_the_object(key, container_index.Value, orientation_case.Value, box.Value, point, RotationX, RotationY, RotationZ))
                        {
                            re_arranging_boxes();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Container_Key">Key of the container in which the object has to be placed</param>
        /// <param name="index">index of the Split_Container_open_list at which the box is placed</param>
        /// <param name="orientation_case">This parametre will help in splitting the Containers, There are total of 6 cases.</param>
        /// <param name="box">Box which is to be placed in the given container </param>
        /// <param name="point">3-Dimensional Point in the given container which will be the origin of the box</param>
        /// <param name="RotationX">if true, Rotate the box allong X-axis</param>
        /// <param name="RotationY">if true, Rotate the box allong Y-axis</param>
        /// <param name="RotationZ">if true, Rotate the box allong Z-axis</param>
        /// <returns>True if the object is placed successfully</returns>
        Boolean place_the_object(String Container_Key, Int32 index, Int32 orientation_case, Box box, Point3D point, Boolean? RotationX, Boolean? RotationY, Boolean? RotationZ)
        {
            BoxList[box.BoxID].Quantity = BoxList[Container_Key].Quantity - 1;
            BoxList[box.BoxID].ContainerID = Container_Key;
            BoxList[box.BoxID].RotationX = RotationX.Value;
            BoxList[box.BoxID].RotationY = RotationY.Value;
            BoxList[box.BoxID].RotationZ = RotationZ.Value;
            BoxList[box.BoxID].Origin = point;

            Container_Containing_Boxes[Container_Key].Add(BoxList[box.BoxID]);
            Split_Container(Container_Key, index, orientation_case, box, point, RotationX.Value, RotationY.Value, RotationZ.Value);

            return true;
        }

        void Split_Container(String Container_Key, Int32 index, Int32 orientation_case, Box box, Point3D point, Boolean RotationX, Boolean RotationY, Boolean RotationZ)
        {
            Containers old_container = Split_Container_open_list[Container_Key].ElementAt(index);
            Containers new_container1 = new Containers();
            Containers new_container2 = new Containers();
            Containers new_container3 = new Containers();

            //no-rotation
            if (orientation_case == 0)
            {
                new_container1.Length = old_container.Length;
                new_container1.Width = old_container.Width - box.Width;
                new_container1.Height = old_container.Height;

                if (new_container1.Length > 0 && new_container1.Width > 0 && new_container1.Height > 0)
                {
                    new_container1.Origin.X = point.X;
                    new_container1.Origin.Y = point.Y + box.Width;
                    new_container1.Origin.Z = point.Z;
                    new_container1.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container1);
                }

                new_container2.Length = old_container.Length - box.Length;
                new_container2.Width = old_container.Width;
                new_container2.Height = old_container.Height;

                if (new_container2.Length > 0 && new_container2.Width > 0 && new_container2.Height > 0)
                {
                    new_container2.Origin.X = point.X + box.Length;
                    new_container2.Origin.Y = point.Y;
                    new_container2.Origin.Z = point.Z;
                    new_container2.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container2);
                }

                new_container3.Length = old_container.Length;
                new_container3.Width = old_container.Width;
                new_container3.Height = old_container.Height - box.Height;

                if (new_container3.Length > 0 && new_container3.Width > 0 && new_container3.Height > 0)
                {
                    new_container3.Origin.X = point.X;
                    new_container3.Origin.Y = point.Y;
                    new_container3.Origin.Z = point.Z + box.Height;
                    new_container3.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container3);
                }
                Split_Container_open_list[Container_Key].RemoveAt(index);

                /*
                    foreach Free Container F in Split_Container_open_list
                    {
                        Compute F\Box and subdivide the result into at most six
                        new containers C1, C2, C3...C6 and add them to 
                        Split_Container_open_list;
                        and Delete F from Split_Containers_open_list.
                     }
                */
                Int32 tindex = 0;
                foreach (Containers F in Split_Container_open_list[Container_Key])
                {
                    //if the container is intersected by the container.
                    if (box_intersect_container(box, box.Length, box.Width, box.Height, F))
                    {//New node at the top side of the used node.
                        if (box.Origin.X < F.Origin.X + F.Length && box.Origin.X + box.Length > F.Origin.X )
                        {
                            if (box.Origin.Y > F.Origin.Y && box.Origin.Y < F.Origin.Y + F.Width)
                            {
                                // make new container                                
                            }
                        }
                    }
                    +tindex;
                }
            }

            //rotation along X-axis
            else if (orientation_case == 1)
            {
                new_container1.Length = old_container.Length;
                new_container1.Width = old_container.Width - box.Height;
                new_container1.Height = old_container.Height;

                if (new_container1.Length > 0 && new_container1.Width > 0 && new_container1.Height > 0)
                {
                    new_container1.Origin.X = point.X;
                    new_container1.Origin.Y = point.Y + box.Height;
                    new_container1.Origin.Z = point.Z;
                    new_container1.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container1);
                }

                new_container2.Length = old_container.Length - box.Length;
                new_container2.Width = old_container.Width;
                new_container2.Height = old_container.Height;

                if (new_container2.Length > 0 && new_container2.Width > 0 && new_container2.Height > 0)
                {
                    new_container2.Origin.X = point.X + box.Length;
                    new_container2.Origin.Y = point.Y;
                    new_container2.Origin.Z = point.Z;
                    new_container2.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container2);
                }

                new_container3.Length = old_container.Length;
                new_container3.Width = old_container.Width;
                new_container3.Height = old_container.Height - box.Width;

                if (new_container3.Length > 0 && new_container3.Width > 0 && new_container3.Height > 0)
                {
                    new_container3.Origin.X = point.X;
                    new_container3.Origin.Y = point.Y;
                    new_container3.Origin.Z = point.Z + box.Width;
                    new_container3.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container3);
                }
                Split_Container_open_list[Container_Key].RemoveAt(index);
            }

            //rotation along Y-axis
            else if (orientation_case == 2)
            {
                new_container1.Length = old_container.Length;
                new_container1.Width = old_container.Width - box.Width;
                new_container1.Height = old_container.Height;

                if (new_container1.Length > 0 && new_container1.Width > 0 && new_container1.Height > 0)
                {
                    new_container1.Origin.X = point.X;
                    new_container1.Origin.Y = point.Y + box.Width;
                    new_container1.Origin.Z = point.Z;
                    new_container1.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container1);
                }

                new_container2.Length = old_container.Length - box.Height;
                new_container2.Width = old_container.Width;
                new_container2.Height = old_container.Height;

                if (new_container2.Length > 0 && new_container2.Width > 0 && new_container2.Height > 0)
                {
                    new_container2.Origin.X = point.X + box.Height;
                    new_container2.Origin.Y = point.Y;
                    new_container2.Origin.Z = point.Z;
                    new_container2.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container2);
                }

                new_container3.Length = old_container.Length;
                new_container3.Width = old_container.Width;
                new_container3.Height = old_container.Height - box.Length;

                if (new_container3.Length > 0 && new_container3.Width > 0 && new_container3.Height > 0)
                {
                    new_container3.Origin.X = point.X;
                    new_container3.Origin.Y = point.Y;
                    new_container3.Origin.Z = point.Z + box.Length;
                    new_container3.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container3);
                }
                Split_Container_open_list[Container_Key].RemoveAt(index);
            }

            //rotation along z-axis
            else if (orientation_case == 3)
            {
                new_container1.Length = old_container.Length;
                new_container1.Width = old_container.Width - box.Length;
                new_container1.Height = old_container.Height;

                if (new_container1.Length > 0 && new_container1.Width > 0 && new_container1.Height > 0)
                {
                    new_container1.Origin.X = point.X;
                    new_container1.Origin.Y = point.Y + box.Length;
                    new_container1.Origin.Z = point.Z;
                    new_container1.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container1);
                }

                new_container2.Length = old_container.Length - box.Width;
                new_container2.Width = old_container.Width;
                new_container2.Height = old_container.Height;

                if (new_container2.Length > 0 && new_container2.Width > 0 && new_container2.Height > 0)
                {
                    new_container2.Origin.X = point.X + box.Width;
                    new_container2.Origin.Y = point.Y;
                    new_container2.Origin.Z = point.Z;
                    new_container2.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container2);
                }

                new_container3.Length = old_container.Length;
                new_container3.Width = old_container.Width;
                new_container3.Height = old_container.Height - box.Height;

                if (new_container3.Length > 0 && new_container3.Width > 0 && new_container3.Height > 0)
                {
                    new_container3.Origin.X = point.X;
                    new_container3.Origin.Y = point.Y;
                    new_container3.Origin.Z = point.Z + box.Height;
                    new_container3.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container3);
                }
                Split_Container_open_list[Container_Key].RemoveAt(index);
            }

            else if (orientation_case == 4)
            {
                new_container1.Length = old_container.Length;
                new_container1.Width = old_container.Width - box.Length;
                new_container1.Height = old_container.Height;

                if (new_container1.Length > 0 && new_container1.Width > 0 && new_container1.Height > 0)
                {
                    new_container1.Origin.X = point.X;
                    new_container1.Origin.Y = point.Y + box.Length;
                    new_container1.Origin.Z = point.Z;
                    new_container1.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container1);
                }

                new_container2.Length = old_container.Length - box.Height;
                new_container2.Width = old_container.Width;
                new_container2.Height = old_container.Height;

                if (new_container2.Length > 0 && new_container2.Width > 0 && new_container2.Height > 0)
                {
                    new_container2.Origin.X = point.X + box.Height;
                    new_container2.Origin.Y = point.Y;
                    new_container2.Origin.Z = point.Z;
                    new_container2.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container2);
                }

                new_container3.Length = old_container.Length;
                new_container3.Width = old_container.Width;
                new_container3.Height = old_container.Height - box.Width;

                if (new_container3.Length > 0 && new_container3.Width > 0 && new_container3.Height > 0)
                {
                    new_container3.Origin.X = point.X;
                    new_container3.Origin.Y = point.Y;
                    new_container3.Origin.Z = point.Z + box.Width;
                    new_container3.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container3);
                }
                Split_Container_open_list[Container_Key].RemoveAt(index);
            }

            //no-rotation
            else if (orientation_case == 5)
            {
                new_container1.Length = old_container.Length;
                new_container1.Width = old_container.Width - box.Height;
                new_container1.Height = old_container.Height;

                if (new_container1.Length > 0 && new_container1.Width > 0 && new_container1.Height > 0)
                {
                    new_container1.Origin.X = point.X;
                    new_container1.Origin.Y = point.Y + box.Height;
                    new_container1.Origin.Z = point.Z;
                    new_container1.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container1);
                }

                new_container2.Length = old_container.Length - box.Width;
                new_container2.Width = old_container.Width;
                new_container2.Height = old_container.Height;

                if (new_container2.Length > 0 && new_container2.Width > 0 && new_container2.Height > 0)
                {
                    new_container2.Origin.X = point.X + box.Width;
                    new_container2.Origin.Y = point.Y;
                    new_container2.Origin.Z = point.Z;
                    new_container2.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container2);
                }

                new_container3.Length = old_container.Length;
                new_container3.Width = old_container.Width;
                new_container3.Height = old_container.Height - box.Length;

                if (new_container3.Length > 0 && new_container3.Width > 0 && new_container3.Height > 0)
                {
                    new_container3.Origin.X = point.X;
                    new_container3.Origin.Y = point.Y;
                    new_container3.Origin.Z = point.Z + box.Length;
                    new_container3.Still_to_Open = true;
                    Split_Container_open_list[Container_Key].Add(new_container3);
                }
                Split_Container_open_list[Container_Key].RemoveAt(index);
            }
        }

        Boolean box_intersect_container(Box b, Int32 box_length, Int32 box_width, Int32 box_height, Containers c)
        {
            if (b.Origin.X >= c.Origin.X + c.Length || b.Origin.X + box_length <= c.Origin.X ||
                b.Origin.Y >= c.Origin.Y + c.Width || b.Origin.Y + box_width <= c.Origin.Y ||
                b.Origin.Z >= c.Origin.Z + c.Height || b.Origin.Z + box_height <= c.Origin.Z)
                return false;
            else
                return true;
        }
        /// <summary>this function checks if the container has enough weight limit remaning</summary>
        /// <param name="Container_key">It is the key of the Container in which the box has to be placed,
        /// I used this key for accessing the container from dictionary data-structure.</param>
        /// <param name="box">Box which is to be contained in the given contianer.</param>
        /// <returns>Returns true if weight of box is less than weight of container else false.</returns>
        Boolean has_free_weight(String Container_key, Box box)
        {
            List<Box> temp_list = Container_Containing_Boxes[Container_key];
            Double weight = box.Weight;

            foreach (Box ibox in temp_list) weight += ibox.Weight;
            if (weight >= ContainerList[Container_key].MaxWeight) return false;
            else return true;
        }

        /// <summary>
        ///     This Function checks if the given box can be contained in the container or not.
        ///     Flow:
        ///         It 1st checks for all the sub splitted containers of the given container individually that whether
        ///         they could occumulate the box or not.
        ///         Foreach container, it checks all the orientations X, Y or Z (if the rotations are allowed) for each box.
        /// </summary>
        /// 
        /// <param name="Container_Key">It is the key of the Container in which the box has to be placed,I used this key for accessing the container from dictionary data-structure.</param> 
        /// <param name="box">Box which is to be contained in the given contianer.</param> 
        /// <param name="Orientation_case">This parametre will help in splitting the Containers, There are total of 6 cases.</param>
        /// <param name="container_index">Exact index at which the box will be placed in the given container.</param>
        /// <param name="RotationX">Out pramatre showing if the box was rotated along X co-ordinate</param>
        /// <param name="RotationY">Out pramatre showing if the box was rotated along Y co-ordinate</param>
        /// <param name="RotationZ">Out pramatre showing if the box was rotated along Z co-ordinate</param>
        /// <returns>It returns the Origin of the container in which the box has to be placed. (If available)If not available, returns a point (-infinity, -infinity, -infinity)</returns>
        Point3D has_free_space(String Container_Key, Box box, out Int32? Orientation_case, out Int32? container_index, out Boolean? RotationX, out Boolean? RotationY, out Boolean? RotationZ)
        {            
            //could be placed in only touching the bottom
            if (box.BottomOnly)
            {
                Int32 index = 0;
                foreach (Containers container in Split_Container_open_list[Container_Key])
                {
                    //as the object is bottom only so it should be placed on xy-plane only
                    if (container.Origin.Z == 0.0F && container.volume() >= box.Volume()) //z-axis value should be zero.
                    {
                        //no rotation
                        if (container.Height >= box.Height && container.Length >= box.Length && container.Width >= box.Width)
                        {
                            RotationX = false;
                            RotationY = false;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 0;
                            return container.Origin;
                        }

                        //if X rotation is allowed.
                        else if (box.AllowedRotationsX && container.Width >= box.Height && container.Height >= box.Width && container.Length >= box.Length)
                        {
                            RotationX = true;
                            RotationY = false;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 1;
                            return container.Origin;
                        }

                        // if Y rotation is allowed
                        else if (box.AllowedRotationsY && container.Width >= box.Width && container.Length >= box.Height && container.Height >= box.Length)
                        {
                            RotationX = false;
                            RotationY = true;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 2;
                            return container.Origin;
                        }

                        //if Z rotation is allowed
                        else if (box.AllowedRotationsZ && container.Height >= box.Height && container.Width >= box.Length && container.Length >= box.Width)
                        {
                            RotationX = false;
                            RotationY = false;
                            RotationZ = true;
                            container_index = index;
                            Orientation_case = 3;
                            return container.Origin;
                        }
                        //along  yx-rotation
                        else if (box.AllowedRotationsY && box.AllowedRotationsX)
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = true;
                                RotationY = true;
                                RotationZ = false;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = true;
                                RotationY = true;
                                RotationZ = false;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }

                        //along  xz-rotation
                        else if (box.AllowedRotationsX && box.AllowedRotationsZ )
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = true;
                                RotationY = false;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = true;
                                RotationY = false;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }

                        //along zy-rotation
                        else if (box.AllowedRotationsZ && box.AllowedRotationsY)
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = false;
                                RotationY = true;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = false;
                                RotationY = true;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }
                    }
                    ++index;
                }
            }
            else if (box.TopOnly)
            {
                Int32 index = 0;
                foreach (Containers container in Split_Container_open_list[Container_Key])
                {
                    //as the object is top only only so it should be placed on xy-plane +  only
                    if (container.Origin.Z + container.Height == ContainerList[Container_Key].Height && container.volume() >= box.Volume())
                    {
                        //no rotation
                        if (container.Height >= box.Height && container.Length >= box.Length && container.Width >= box.Width)
                        {
                            RotationX = false;
                            RotationY = false;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 0;
                            return container.Origin;
                        }

                        //if X rotation is allowed.
                        else if (box.AllowedRotationsX && container.Width >= box.Height && container.Height >= box.Width && container.Length >= box.Length)
                        {
                            RotationX = true;
                            RotationY = false;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 1;
                            return container.Origin;
                        }

                        // if Y rotation is allowed
                        else if (box.AllowedRotationsY && container.Width >= box.Width && container.Length >= box.Height && container.Height >= box.Length)
                        {
                            RotationX = false;
                            RotationY = true;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 2;
                            return container.Origin;
                        }

                        //if Z rotation is allowed
                        else if (box.AllowedRotationsZ && container.Height >= box.Height && container.Width >= box.Length && container.Length >= box.Width)
                        {
                            RotationX = false;
                            RotationY = false;
                            RotationZ = true;
                            container_index = index;
                            Orientation_case = 3;
                            return container.Origin;
                        }
                        //along  yx-rotation
                        else if (box.AllowedRotationsY && box.AllowedRotationsX)
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = true;
                                RotationY = true;
                                RotationZ = false;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = true;
                                RotationY = true;
                                RotationZ = false;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }

                        //along  xz-rotation
                        else if (box.AllowedRotationsX && box.AllowedRotationsZ)
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = true;
                                RotationY = false;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = true;
                                RotationY = false;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }

                        //along zy-rotation
                        else if (box.AllowedRotationsZ && box.AllowedRotationsY)
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = false;
                                RotationY = true;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = false;
                                RotationY = true;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }
                    }
                    ++index;
                }
            }
            //place the box whereever it is fissible
            else
            {
                Int32 index = 0;
                foreach (Containers container in Split_Container_open_list[Container_Key])
                {
                    if (container.volume() >= box.Volume())
                    {
                        //no rotation
                        if (container.Height >= box.Height && container.Length >= box.Length && container.Width >= box.Width)
                        {
                            RotationX = false;
                            RotationY = false;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 0;
                            return container.Origin;
                        }

                        //if X rotation is allowed.
                        else if (box.AllowedRotationsX && container.Width >= box.Height && container.Height >= box.Width && container.Length >= box.Length)
                        {
                            RotationX = true;
                            RotationY = false;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 1;
                            return container.Origin;
                        }

                        // if Y rotation is allowed
                        else if (box.AllowedRotationsY && container.Width >= box.Width && container.Length >= box.Height && container.Height >= box.Length)
                        {
                            RotationX = false;
                            RotationY = true;
                            RotationZ = false;
                            container_index = index;
                            Orientation_case = 2;
                            return container.Origin;
                        }

                        //if Z rotation is allowed
                        else if (box.AllowedRotationsZ && container.Height >= box.Height && container.Width >= box.Length && container.Length >= box.Width)
                        {
                            RotationX = false;
                            RotationY = false;
                            RotationZ = true;
                            container_index = index;
                            Orientation_case = 3;
                            return container.Origin;
                        }
                        //along  yx-rotation
                        else if (box.AllowedRotationsY && box.AllowedRotationsX)
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = true;
                                RotationY = true;
                                RotationZ = false;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = true;
                                RotationY = true;
                                RotationZ = false;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }

                        //along  xz-rotation
                        else if (box.AllowedRotationsX && box.AllowedRotationsZ)
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = true;
                                RotationY = false;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = true;
                                RotationY = false;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }

                        //along zy-rotation
                        else if (box.AllowedRotationsZ && box.AllowedRotationsY)
                        {
                            if (container.Width >= box.Height && container.Length >= box.Width && container.Height >= box.Length)
                            {
                                RotationX = false;
                                RotationY = true;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 4;
                                return container.Origin;
                            }
                            else if (container.Width >= box.Length && container.Length >= box.Height && container.Height >= box.Width)
                            {
                                RotationX = false;
                                RotationY = true;
                                RotationZ = true;
                                container_index = index;
                                Orientation_case = 5;
                                return container.Origin;
                            }
                        }
                    }
                    ++index;
                }
            }

            // if still not available.
            Point3D invalid_point = new Point3D();
            invalid_point.X = float.NegativeInfinity;
            invalid_point.Y = float.NegativeInfinity;
            invalid_point.Z = float.NegativeInfinity;
            RotationX = null;
            RotationY = null;
            RotationZ = null;
            container_index = null;
            Orientation_case = null;
            return invalid_point;
        }

        /// <summary>This function rearrange the boxes in descending order of there volumes.</summary>
        void re_arranging_boxes()
        {
            sorted_box_List = BoxList.ToList();
            sorted_box_List.Sort((firstPair, nextPair) => ( firstPair.Value.Width * firstPair.Value.Height * firstPair.Value.Length).CompareTo (nextPair.Value.Length * nextPair.Value.Width * nextPair.Value.Height));
            sorted_box_List.Reverse();
        }
    }
}