using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace _3D_Bin_Packing
{
    class Guillotine3D
    {
        // Created a list of containers for holding all the container objects
        public static Dictionary<String, Containers> ContainerList = new Dictionary<String,Containers>();

        // Created a list of boxes for holding all the box objects
        public static Dictionary<String, Box> BoxList = new Dictionary<string, Box>();

        // list of the splitted containers from the given containers.
        Dictionary<String, List<Containers>> Split_Container_open_list = new Dictionary<string, List<Containers>>();
        Dictionary<String, List<Containers>> Split_Container_closed_list = new Dictionary<string, List<Containers>>();

        // list of all the boxes in the given container.
        Dictionary<String, List<Box>> Container_Containing_Boxes = new Dictionary<string, List<Box>>();

        List<KeyValuePair<String, Box>> sorted_box_List;
        
        
        /*
        return the container which has the 
        smallest volume of all the unopened containers;
        */
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
                if (!box.Value.IsPlaced)        // if the box is not yet placed.
                {
                    
                }
                //if this box can be contained in the container(splitted continers)
                // with out violating the weight and other constraint then
                //add this box to the list of container_containing_boxes with Key value
                // passed in argument of the funtion

            // also check the placing order by merging 2 or more locations.

                //while placing it in the container, marks its accurate origin.

                // if the box is placed. mark it as is_placed.

                // the box will be considered closed if total unconvered area is < 5%
            }
        }


        Boolean has_free_space(String Container_Key, Box box)
        {
            // list of all the splitted-containers of the given container-key
            List<Containers> temp_list = Split_Container_open_list[Container_Key];

            //could be placed in only touching the bottom
            if (box.BottomOnly)
            {
                foreach (Containers container in temp_list)
                {
                    //as the object is bottom only so it should be placed on xy-plane only
                    if (container.Origin.Z == 0.0F && container.volume() >= box.Volume()) //z-axis value should be zero.
                    {
                        //no rotation
                        if (container.Height >= box.Height &&
                            container.Length >= box.Length &&
                            container.Width >= box.Width)
                            return true;

                        else if (box.AllowedRotationsX &&
                            container.Width >= box.Height &&
                            container.Height >= box.Width &&
                            container.Length >= box.Length)
                            return true;

                        else if (box.AllowedRotationsZ &&
                            container.Height >= box.Height &&
                            container.Width >= box.Length &&
                            container.Length >= box.Width)
                            return true;

                        else if (box.AllowedRotationsY &&
                            container.Width >= box.Width &&
                            container.Length >= box.Height &&
                            container.Height >= box.Length)
                            return true;
                    }
                }

                // check all the bottom containers after merging them
            }
            else if (box.TopOnly)
            {
                foreach (Containers container in temp_list)
                {
                    //as the object is bottom only so it should be placed on xy-plane only
                    if (container.Origin.Z + container.Height == ContainerList[Container_Key].Height
                        && container.volume() >= box.Volume()) //z-axis value should be zero.
                    {
                        //no rotation
                        if (container.Height >= box.Height &&
                            container.Length >= box.Length &&
                            container.Width >= box.Width)
                            return true;

                        //along X-axis rotation is allowed
                        else if (box.AllowedRotationsX &&
                            container.Width >= box.Height &&
                            container.Height >= box.Width &&
                            container.Length >= box.Length)
                            return true;

                        //along Z-axis rotation is allowed
                        else if (box.AllowedRotationsZ &&
                            container.Height >= box.Height &&
                            container.Width >= box.Length &&
                            container.Length >= box.Width)
                            return true;

                        //along Y-axis rotation is allowed
                        else if (box.AllowedRotationsY &&
                            container.Width >= box.Width &&
                            container.Length >= box.Height &&
                            container.Height >= box.Length)
                            return true;
                    }
                }

                //after mergin top_only containers
            }
            else
            {
                foreach (Containers container in temp_list)
                {
                    if (container.volume() >= box.Volume())
                    {
                        //no rotation
                        if (container.Height >= box.Height &&
                            container.Length >= box.Length &&
                            container.Width >= box.Width)
                            return true;

                        //along X-axis rotation is allowed
                        else if (box.AllowedRotationsX &&
                            container.Width >= box.Height &&
                            container.Height >= box.Width &&
                            container.Length >= box.Length)
                            return true;

                        //along Z-axis rotation is allowed
                        else if (box.AllowedRotationsZ &&
                            container.Height >= box.Height &&
                            container.Width >= box.Length &&
                            container.Length >= box.Width)
                            return true;

                        //along Y-axis rotation is allowed
                        else if (box.AllowedRotationsY &&
                            container.Width >= box.Width &&
                            container.Length >= box.Height &&
                            container.Height >= box.Length)
                            return true;
                    }
                }
                //after mergin top_only containers
            }

            // if still not available.
            return false;
        }
        //This function rearrange the items in descending order of there volumes.
        void re_arranging_boxes()
        {
            sorted_box_List = BoxList.ToList();
            sorted_box_List.Sort((firstPair, nextPair) => ( firstPair.Value.Width * firstPair.Value.Height * firstPair.Value.Length).CompareTo (nextPair.Value.Length * nextPair.Value.Width * nextPair.Value.Height));
            sorted_box_List.Reverse();
        }
    }
}
