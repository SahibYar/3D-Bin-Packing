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


        // list of the splitted containers from the given containers
        //Dictionary<Container id, list of sub containers splitted from it>
        Dictionary<String, List<Containers>> Split_Container_open_list = new Dictionary<string, List<Containers>>();
        Dictionary<String, List<Containers>> Split_Container_closed_list = new Dictionary<string, List<Containers>>();

        // list of all the boxes in the given container.
        Dictionary<String, List<Box>> Container_Containing_Boxes = new Dictionary<string, List<Box>>();

        //sort the boxes in their descending volume
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
                if (!box.Value.IsPlaced)        // if the box is not yet placed.
                {

                    if (has_free_space(key, box.Value).X != float.NegativeInfinity &&
                        has_free_space(key, box.Value).Y != float.NegativeInfinity &&
                        has_free_space(key, box.Value).Z != float.NegativeInfinity)     // checks if the box could be contained in the given container.
                    {

                    }
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


        // this function checks if the container has enough weight limit remaning
        Boolean has_free_weight(String Container_key, Box box)
        {
            List<Box> temp_list = Container_Containing_Boxes[Container_key];
            Double weight = box.Weight;

            foreach (Box ibox in temp_list) weight += ibox.Weight;
            if (weight >= ContainerList[Container_key].MaxWeight) return false;
            else return true;
        } 

        // This functions checks if the given box can be contained in the container.
        Point3D has_free_space(String Container_Key, Box box)
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
                            return container.Origin;

                        //if X rotation is allowed.
                        else if (box.AllowedRotationsX &&
                            container.Width >= box.Height &&
                            container.Height >= box.Width &&
                            container.Length >= box.Length)
                            return container.Origin;

                        //if Z rotation is allowed
                        else if (box.AllowedRotationsZ &&
                            container.Height >= box.Height &&
                            container.Width >= box.Length &&
                            container.Length >= box.Width)
                            return container.Origin;

                        // if Y rotation is allowed
                        else if (box.AllowedRotationsY &&
                            container.Width >= box.Width &&
                            container.Length >= box.Height &&
                            container.Height >= box.Length)
                            return container.Origin;
                    }
                }
                /*
                for merging
                check all the split rectangles of the given container

            the time complexity will be bigOh(n^2)
            compare each container with all other container that If they have common
            adjacent wall.
            If yes
            {
                then calculate the maximum possible free container.
                create one and split the remaining one.
            }
            if no
                ignore it.
             */
                // check all the bottom containers after merging them
            }
            else if (box.TopOnly)
            {
                foreach (Containers container in temp_list)
                {
                    //as the object is top only only so it should be placed on xy-plane +  only
                    if (container.Origin.Z + container.Height == ContainerList[Container_Key].Height
                        && container.volume() >= box.Volume()) //z-axis value should be zero.
                    {
                        //no rotation
                        if (container.Height >= box.Height &&
                            container.Length >= box.Length &&
                            container.Width >= box.Width)
                            return container.Origin;

                        //along X-axis rotation is allowed
                        else if (box.AllowedRotationsX &&
                            container.Width >= box.Height &&
                            container.Height >= box.Width &&
                            container.Length >= box.Length)
                            return container.Origin;

                        //along Z-axis rotation is allowed
                        else if (box.AllowedRotationsZ &&
                            container.Height >= box.Height &&
                            container.Width >= box.Length &&
                            container.Length >= box.Width)
                            return container.Origin;

                        //along Y-axis rotation is allowed
                        else if (box.AllowedRotationsY &&
                            container.Width >= box.Width &&
                            container.Length >= box.Height &&
                            container.Height >= box.Length)
                            return container.Origin;
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
                            return container.Origin;

                        //along X-axis rotation is allowed
                        else if (box.AllowedRotationsX &&
                            container.Width >= box.Height &&
                            container.Height >= box.Width &&
                            container.Length >= box.Length)
                            return container.Origin;

                        //along Z-axis rotation is allowed
                        else if (box.AllowedRotationsZ &&
                            container.Height >= box.Height &&
                            container.Width >= box.Length &&
                            container.Length >= box.Width)
                            return container.Origin;

                        //along Y-axis rotation is allowed
                        else if (box.AllowedRotationsY &&
                            container.Width >= box.Width &&
                            container.Length >= box.Height &&
                            container.Height >= box.Length)
                            return container.Origin;
                    }
                }
                //after mergin top_only containers
            }

            // if still not available.
            Point3D invalid_point = new Point3D();
            invalid_point.X = float.NegativeInfinity;
            invalid_point.Y = float.NegativeInfinity;
            invalid_point.Z = float.NegativeInfinity;
            return invalid_point;
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
