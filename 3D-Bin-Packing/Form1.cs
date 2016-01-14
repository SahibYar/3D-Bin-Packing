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
            XmlDocument document = new XmlDocument();
            document.Load(@""+xmlFileLabel.Text.ToString());
            XmlElement element = document.DocumentElement;
        }
    }
}
