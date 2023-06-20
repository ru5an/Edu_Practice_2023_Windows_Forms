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
            this.LabelScaleX = new System.Windows.Forms.Label();
            this.LabelScaleY = new System.Windows.Forms.Label();
            this.LabelDisplacementX = new System.Windows.Forms.Label();
            this.LabelDisplacementY = new System.Windows.Forms.Label();
            this.delGraph = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.scalePerfectSize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // addGraph
            // 
            this.addGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.addGraph.Location = new System.Drawing.Point(696, 196);
            this.addGraph.Name = "addGraph";
            this.addGraph.Size = new System.Drawing.Size(104, 58);
            this.addGraph.TabIndex = 1;
            this.addGraph.Text = "Add";
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
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.graphs_Paint);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_PreviewKeyDown);
            // 
            // LabelScaleX
            // 
            this.LabelScaleX.AutoSize = true;
            this.LabelScaleX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LabelScaleX.Location = new System.Drawing.Point(696, 90);
            this.LabelScaleX.Name = "LabelScaleX";
            this.LabelScaleX.Size = new System.Drawing.Size(72, 20);
            this.LabelScaleX.TabIndex = 3;
            this.LabelScaleX.Text = "Scale X:";
            // 
            // LabelScaleY
            // 
            this.LabelScaleY.AutoSize = true;
            this.LabelScaleY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LabelScaleY.Location = new System.Drawing.Point(696, 115);
            this.LabelScaleY.Name = "LabelScaleY";
            this.LabelScaleY.Size = new System.Drawing.Size(71, 20);
            this.LabelScaleY.TabIndex = 4;
            this.LabelScaleY.Text = "Scale Y:";
            // 
            // LabelDisplacementX
            // 
            this.LabelDisplacementX.AutoSize = true;
            this.LabelDisplacementX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LabelDisplacementX.Location = new System.Drawing.Point(696, 142);
            this.LabelDisplacementX.Name = "LabelDisplacementX";
            this.LabelDisplacementX.Size = new System.Drawing.Size(133, 20);
            this.LabelDisplacementX.TabIndex = 5;
            this.LabelDisplacementX.Text = "Displacement X:";
            // 
            // LabelDisplacementY
            // 
            this.LabelDisplacementY.AutoSize = true;
            this.LabelDisplacementY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LabelDisplacementY.Location = new System.Drawing.Point(696, 168);
            this.LabelDisplacementY.Name = "LabelDisplacementY";
            this.LabelDisplacementY.Size = new System.Drawing.Size(132, 20);
            this.LabelDisplacementY.TabIndex = 6;
            this.LabelDisplacementY.Text = "Displacement Y:";
            // 
            // delGraph
            // 
            this.delGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.delGraph.Location = new System.Drawing.Point(806, 196);
            this.delGraph.Name = "delGraph";
            this.delGraph.Size = new System.Drawing.Size(90, 58);
            this.delGraph.TabIndex = 7;
            this.delGraph.Text = "Del";
            this.delGraph.UseVisualStyleBackColor = true;
            this.delGraph.Click += new System.EventHandler(this.delGraph_Click);
            // 
            // helpButton
            // 
            this.helpButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.helpButton.Location = new System.Drawing.Point(699, 13);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(54, 29);
            this.helpButton.TabIndex = 8;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // scalePerfectSize
            // 
            this.scalePerfectSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.scalePerfectSize.Location = new System.Drawing.Point(699, 48);
            this.scalePerfectSize.Name = "scalePerfectSize";
            this.scalePerfectSize.Size = new System.Drawing.Size(197, 29);
            this.scalePerfectSize.TabIndex = 9;
            this.scalePerfectSize.Text = "Scale to canvas size";
            this.scalePerfectSize.UseVisualStyleBackColor = true;
            this.scalePerfectSize.Click += new System.EventHandler(this.scalePerfectSize_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 453);
            this.Controls.Add(this.scalePerfectSize);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.delGraph);
            this.Controls.Add(this.LabelDisplacementY);
            this.Controls.Add(this.LabelDisplacementX);
            this.Controls.Add(this.LabelScaleY);
            this.Controls.Add(this.LabelScaleX);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.addGraph);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.Text = "Output of graphs";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button addGraph;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label LabelScaleX;
        private System.Windows.Forms.Label LabelScaleY;
        private System.Windows.Forms.Label LabelDisplacementX;
        private System.Windows.Forms.Label LabelDisplacementY;
        private System.Windows.Forms.Button delGraph;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button scalePerfectSize;
    }
}

