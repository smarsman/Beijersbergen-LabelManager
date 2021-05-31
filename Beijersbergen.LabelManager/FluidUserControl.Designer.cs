namespace Beijersbergen.LabelManager
{
    partial class FluidUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvBatches = new System.Windows.Forms.ListView();
            this.chDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBatchnr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAantal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.btSearch = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.rtbQrCodeContent = new System.Windows.Forms.RichTextBox();
            this.pbQrCode = new System.Windows.Forms.PictureBox();
            this.btGenerateQRCode = new System.Windows.Forms.Button();
            this.pgBatch = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQrCode)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvBatches);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btSearch);
            this.splitContainer1.Panel1.Controls.Add(this.tbSearch);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbQrCodeContent);
            this.splitContainer1.Panel2.Controls.Add(this.pbQrCode);
            this.splitContainer1.Panel2.Controls.Add(this.btGenerateQRCode);
            this.splitContainer1.Panel2.Controls.Add(this.pgBatch);
            this.splitContainer1.Size = new System.Drawing.Size(1060, 628);
            this.splitContainer1.SplitterDistance = 520;
            this.splitContainer1.TabIndex = 0;
            // 
            // lvBatches
            // 
            this.lvBatches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBatches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate,
            this.chBatchnr,
            this.chName,
            this.chAantal});
            this.lvBatches.FullRowSelect = true;
            this.lvBatches.Location = new System.Drawing.Point(18, 81);
            this.lvBatches.MultiSelect = false;
            this.lvBatches.Name = "lvBatches";
            this.lvBatches.Size = new System.Drawing.Size(487, 532);
            this.lvBatches.TabIndex = 3;
            this.lvBatches.UseCompatibleStateImageBehavior = false;
            this.lvBatches.View = System.Windows.Forms.View.Details;
            this.lvBatches.SelectedIndexChanged += new System.EventHandler(this.lvBatches_SelectedIndexChanged);
            // 
            // chDate
            // 
            this.chDate.Text = "Datum";
            this.chDate.Width = 109;
            // 
            // chBatchnr
            // 
            this.chBatchnr.Text = "Batch nummer";
            this.chBatchnr.Width = 127;
            // 
            // chName
            // 
            this.chName.Text = "Naam";
            this.chName.Width = 154;
            // 
            // chAantal
            // 
            this.chAantal.Text = "Basiskleuren";
            this.chAantal.Width = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Zoek op batchnummer";
            // 
            // btSearch
            // 
            this.btSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSearch.Location = new System.Drawing.Point(436, 39);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(69, 29);
            this.btSearch.TabIndex = 1;
            this.btSearch.Text = "Zoek";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Location = new System.Drawing.Point(18, 44);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(412, 20);
            this.tbSearch.TabIndex = 0;
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            // 
            // rtbQrCodeContent
            // 
            this.rtbQrCodeContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbQrCodeContent.Location = new System.Drawing.Point(22, 308);
            this.rtbQrCodeContent.Name = "rtbQrCodeContent";
            this.rtbQrCodeContent.ReadOnly = true;
            this.rtbQrCodeContent.Size = new System.Drawing.Size(489, 139);
            this.rtbQrCodeContent.TabIndex = 3;
            this.rtbQrCodeContent.Text = "";
            // 
            // pbQrCode
            // 
            this.pbQrCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbQrCode.Location = new System.Drawing.Point(361, 463);
            this.pbQrCode.Name = "pbQrCode";
            this.pbQrCode.Size = new System.Drawing.Size(150, 150);
            this.pbQrCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbQrCode.TabIndex = 2;
            this.pbQrCode.TabStop = false;
            // 
            // btGenerateQRCode
            // 
            this.btGenerateQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btGenerateQRCode.Enabled = false;
            this.btGenerateQRCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btGenerateQRCode.Location = new System.Drawing.Point(22, 463);
            this.btGenerateQRCode.Name = "btGenerateQRCode";
            this.btGenerateQRCode.Size = new System.Drawing.Size(134, 39);
            this.btGenerateQRCode.TabIndex = 1;
            this.btGenerateQRCode.Text = "Kopieer QR";
            this.btGenerateQRCode.UseVisualStyleBackColor = true;
            this.btGenerateQRCode.Click += new System.EventHandler(this.btGenerateQRCode_Click);
            // 
            // pgBatch
            // 
            this.pgBatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgBatch.HelpVisible = false;
            this.pgBatch.Location = new System.Drawing.Point(22, 81);
            this.pgBatch.Name = "pgBatch";
            this.pgBatch.Size = new System.Drawing.Size(489, 211);
            this.pgBatch.TabIndex = 0;
            // 
            // FluidUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FluidUserControl";
            this.Size = new System.Drawing.Size(1060, 628);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbQrCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ListView lvBatches;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chBatchnr;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.PropertyGrid pgBatch;
        private System.Windows.Forms.ColumnHeader chAantal;
        private System.Windows.Forms.Button btGenerateQRCode;
        private System.Windows.Forms.PictureBox pbQrCode;
        private System.Windows.Forms.RichTextBox rtbQrCodeContent;
    }
}
