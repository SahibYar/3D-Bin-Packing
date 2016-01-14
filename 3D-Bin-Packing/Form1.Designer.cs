namespace _3D_Bin_Packing
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.descriptionLabel1 = new System.Windows.Forms.Label();
            this.xmlFileLabel = new System.Windows.Forms.Label();
            this.addxmlbutton = new System.Windows.Forms.Button();
            this.XML_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.generateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(616, 104);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // descriptionLabel1
            // 
            this.descriptionLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel1.AutoSize = true;
            this.descriptionLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.descriptionLabel1.Location = new System.Drawing.Point(21, 109);
            this.descriptionLabel1.Name = "descriptionLabel1";
            this.descriptionLabel1.Size = new System.Drawing.Size(315, 17);
            this.descriptionLabel1.TabIndex = 5;
            this.descriptionLabel1.Text = "Add XML file showing total Boxes and Containers";
            // 
            // xmlFileLabel
            // 
            this.xmlFileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xmlFileLabel.AutoSize = true;
            this.xmlFileLabel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.xmlFileLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xmlFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlFileLabel.Location = new System.Drawing.Point(166, 129);
            this.xmlFileLabel.MinimumSize = new System.Drawing.Size(430, 20);
            this.xmlFileLabel.Name = "xmlFileLabel";
            this.xmlFileLabel.Size = new System.Drawing.Size(430, 20);
            this.xmlFileLabel.TabIndex = 4;
            // 
            // addxmlbutton
            // 
            this.addxmlbutton.Location = new System.Drawing.Point(21, 129);
            this.addxmlbutton.Name = "addxmlbutton";
            this.addxmlbutton.Size = new System.Drawing.Size(130, 23);
            this.addxmlbutton.TabIndex = 3;
            this.addxmlbutton.Text = "Add XML file";
            this.addxmlbutton.UseVisualStyleBackColor = true;
            this.addxmlbutton.Click += new System.EventHandler(this.addxmlbutton_Click);
            // 
            // XML_openFileDialog
            // 
            this.XML_openFileDialog.Filter = "XML files (*.xml)|*.xml;";
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(469, 161);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(127, 27);
            this.generateButton.TabIndex = 8;
            this.generateButton.Text = "Calculate Results";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 199);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.descriptionLabel1);
            this.Controls.Add(this.xmlFileLabel);
            this.Controls.Add(this.addxmlbutton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "3D Bin Packing";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label descriptionLabel1;
        private System.Windows.Forms.Label xmlFileLabel;
        private System.Windows.Forms.Button addxmlbutton;
        private System.Windows.Forms.OpenFileDialog XML_openFileDialog;
        private System.Windows.Forms.Button generateButton;
    }
}

