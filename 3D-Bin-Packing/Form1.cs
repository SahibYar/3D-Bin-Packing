using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
