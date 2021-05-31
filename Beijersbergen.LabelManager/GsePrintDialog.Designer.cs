namespace Beijersbergen.LabelManager
{
    partial class GsePrintDialog
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
            this.cbCorrosiveIndicator = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMagnificationFactor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCorrosiveIndicator
            // 
            this.cbCorrosiveIndicator.AutoSize = true;
            this.cbCorrosiveIndicator.Location = new System.Drawing.Point(138, 8);
            this.cbCorrosiveIndicator.Name = "cbCorrosiveIndicator";
            this.cbCorrosiveIndicator.Size = new System.Drawing.Size(15, 14);
            this.cbCorrosiveIndicator.TabIndex = 2;
            this.cbCorrosiveIndicator.UseVisualStyleBackColor = true;
            this.cbCorrosiveIndicator.CheckedChanged += new System.EventHandler(this.cbCorrosiveIndicator_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bijtend indicator:";
            // 
            // cbMagnificationFactor
            // 
            this.cbMagnificationFactor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMagnificationFactor.FormattingEnabled = true;
            this.cbMagnificationFactor.Location = new System.Drawing.Point(138, 36);
            this.cbMagnificationFactor.Name = "cbMagnificationFactor";
            this.cbMagnificationFactor.Size = new System.Drawing.Size(131, 21);
            this.cbMagnificationFactor.TabIndex = 4;
            this.cbMagnificationFactor.SelectedIndexChanged += new System.EventHandler(this.cbMagnificationFactor_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "QR vergrotingsfactor:";
            // 
            // pbPreview
            // 
            this.pbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPreview.Location = new System.Drawing.Point(16, 135);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(700, 367);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 6;
            this.pbPreview.TabStop = false;
            // 
            // btPrint
            // 
            this.btPrint.Location = new System.Drawing.Point(138, 80);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(131, 37);
            this.btPrint.TabIndex = 7;
            this.btPrint.Text = "Print";
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // GsePrintDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 515);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.pbPreview);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbMagnificationFactor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbCorrosiveIndicator);
            this.Name = "GsePrintDialog";
            this.Text = "Print preview";
            this.Load += new System.EventHandler(this.GsePrintDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbCorrosiveIndicator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMagnificationFactor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Button btPrint;
    }
}