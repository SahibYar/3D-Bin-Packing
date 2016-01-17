using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace _3D_Bin_Packing
{
    public partial class Form1 : Form
    {
        private string filePath;    // a variable which will save the xml file path.
        public Form1()
        {
            InitializeComponent();

            // loading the 3d_icon.png as bitmap image to be displayed as form icon
            Bitmap bmp = Properties.Resources._3d_icon;
            this.Icon = Icon.FromHandle(bmp.GetHicon());            
        }

        private void addxmlbutton_Click(object sender, EventArgs e)
        {
            DialogResult result = XML_openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {

                try
                {
                    filePath = XML_openFileDialog.FileName;
                    xmlFileLabel.Text = filePath.ToString();          //adding the selected path to the label.
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read xml file from disk.\nError Message: " + ex.Message);
                }
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            Load_Data();
        }   //ending the event handler for Generate button

        private void Load_Data()
        {
            // Created an Optimization object for storing the OptimizationID
            // and OptimizationType of given xml file.
            Optimization optimization_object = new Optimization();

            // Created an Containers object for storing the ContainerID,
            // Length, MinLength.... of the given Container
            Containers container_object = new Containers();

            // Created an Box object for storing the BoxID, Quantity,
            // Length.... of the given Box
            Box box_object = new Box();

            // Create a new xml document 
            XmlDocument document = new XmlDocument();

            // loaded the newly created xml document with the input of xml 
            // file path
            document.Load(@"" + xmlFileLabel.Text.ToString());

            // Created a list of all the child nodes of <xml> tag
            // which include OptimizationID, OptimizationType,
            // Containers, and Boxes.
            XmlNodeList xnList = document.SelectNodes("/xml[@*]");

            foreach (XmlNode xn in xnList)
            {
                // Selecting the given child nodes of <xml> tag to
                // XmlNode class objects.
                XmlNode OptimizationID = xn.SelectSingleNode("OptimizationID");
                XmlNode OptimizationType = xn.SelectSingleNode("OptimizationType");
                XmlNode Containers = xn.SelectSingleNode("Containers");
                XmlNode Boxes = xn.SelectSingleNode("Boxes");

                // assigning the text of OptimizationID to Optimization class object
                if (OptimizationID != null)
                {
                    optimization_object.OptimizationID = OptimizationID.InnerText;
                }

                // assigning the text of OptimizationType to Optimization class object
                if (OptimizationType != null)
                {
                    optimization_object.OptimizationType = OptimizationType.InnerText;
                }

                if (Containers != null)
                {
                    XmlNodeList innercontainers = Containers.SelectNodes("Containers/Containers");
                    foreach (XmlNode node in innercontainers)
                    {
                        if (node != null)
                        {
                            Point3D point = new Point3D();
                            container_object.ContainerID = node["ContainerID"].InnerText;

                            container_object.Length = Int32.Parse(node["Length"].InnerText);
                            container_object.MinLength = Int32.Parse(node["MinLength"].InnerText);
                            container_object.MaxLength = Int32.Parse(node["MaxLength"].InnerText);
                            container_object.StepLenght = Int32.Parse(node["StepLength"].InnerText);

                            container_object.Width = Int32.Parse(node["Width"].InnerText);
                            container_object.MinWidth = Int32.Parse(node["MinWidth"].InnerText);
                            container_object.MaxWidth = Int32.Parse(node["MaxWidth"].InnerText);
                            container_object.StepWidth = Int32.Parse(node["StepWidth"].InnerText);

                            container_object.Height = Int32.Parse(node["Height"].InnerText);
                            container_object.MinHeight = Int32.Parse(node["MinHeight"].InnerText);
                            container_object.MaxHeight = Int32.Parse(node["MaxHeight"].InnerText);
                            container_object.StepHeight = Int32.Parse(node["StepHeight"].InnerText);

                            container_object.MaxWeight = Double.Parse(node["MaxWeight"].InnerText);
                            container_object.MaxCount = Int32.Parse(node["MaxCount"].InnerText);
                            container_object.Still_to_Open = true;
                            container_object.Closed = false;
                            container_object.Currenlty_Open = false;

                            point.X = 0.0F;
                            point.Y = 0.0F;
                            point.Z = 0.0F;
                            container_object.Origin = point;

                            Guillotine3D.ContainerList.Add(container_object.ContainerID, container_object);
                        }   // ending if (node != null)
                    }   // ending foreach loop
                }   // ending if (Containers != null)

                if (Boxes != null)
                {
                    XmlNodeList boxlist = Boxes.SelectNodes("Boxes/Box");
                    foreach (XmlNode box in boxlist)
                    {
                        box_object.BoxID = box["BoxID"].InnerText;
                        box_object.Quantity = Int32.Parse(box["Quantity"].InnerText);
                        box_object.Length = Int32.Parse(box["Length"].InnerText);
                        box_object.Width = Int32.Parse(box["Width"].InnerText);
                        box_object.Height = Int32.Parse(box["Height"].InnerText);
                        box_object.Weight = Double.Parse(box["Weight"].InnerText);
                        box_object.AllowedRotations = box["AllowedRotations"].InnerText;
                        box_object.IsPlaced = false;

                        if (box["TopOnly"].InnerText.ToUpper() == "FALSE")
                            box_object.TopOnly = false;
                        else
                            box_object.TopOnly = true;

                        if (box["BottomOnly"].InnerText.ToUpper() == "FALSE")
                            box_object.TopOnly = false;
                        else
                            box_object.TopOnly = true;

                        Guillotine3D.BoxList.Add(box_object.BoxID, box_object);
                    }   // ending foreach (XmlNode box in boxlist)
                }   // ending If (Boxes != null)
            }   // ending foreach (XmlNode xn in xnList)
        }      //ending the Load_Data() function
    }   // ending Form1 class
}  // ending namespace
