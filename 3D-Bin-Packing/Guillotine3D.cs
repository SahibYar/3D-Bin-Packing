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

        Dictionary<String, List<Containers>> Split_Container_list = new Dictionary<string, List<Containers>>();

        /*
        return the container which has the 
        smallest volume of all the unopened containers;
        */
        Containers find_smallest_unopen_container ()
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
            return ContainerList[key];
        }
    }
}
