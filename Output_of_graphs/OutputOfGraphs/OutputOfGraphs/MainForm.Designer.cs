namespace OutputOfGraphs
{
    partial class MainForm
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
            this.addGraph = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // addGraph
            // 
            this.addGraph.Location = new System.Drawing.Point(726, 203);
            this.addGraph.Name = "addGraph";
            this.addGraph.Size = new System.Drawing.Size(84, 58);
            this.addGraph.TabIndex = 1;
            this.addGraph.Text = "add graph";
            this.addGraph.UseVisualStyleBackColor = true;
            this.addGraph.Click += new System.EventHandler(this.addGraph_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(13, 13);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(680, 420);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            this.pictureBox.Tag = " ";
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_PreviewKeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 453);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.addGraph);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.Text = "Output of graphs";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            //this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addGraph;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

