using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Beijersbergen.Library.Fluid;
using Beijersbergen.Fluid.Access;
using System.Configuration;
using Beijersbergen.Library.QR;
using System.Drawing;
using System.Text;

namespace Beijersbergen.LabelManager
{
    public partial class FluidUserControl : UserControl
    {
        private IBatchRepository _batchRepository;
        private IQrCodeGenerator _qrCodeGenerator;

        public FluidUserControl()
        {
            InitializeComponent();
            Batches = new List<Batch>();
        }

        protected override void OnLoad(EventArgs e)
        {
            var excludeMixingSystems = ConfigurationManager.AppSettings["ExcludeMixingSystems"]
                .Split(',')
                .ToList();

            _batchRepository = new AccessBatchRepository(ConfigurationManager.AppSettings["accessDbPath"], excludeMixingSystems);
            _qrCodeGenerator = new XzingQrCodeGenerator();

            Search(string.Empty); // Query latest results

            base.OnLoad(e);
        }



        public IEnumerable<Batch> Batches { get; set; }
        

        private void Search(string batchNumber)
        {
            var batches = (string.IsNullOrEmpty(batchNumber))
                ? _batchRepository.SelectLatest(20)
                : _batchRepository.FindByBatchNumber(batchNumber);

            SetBatches(batches);
        }

        private void SetBatches(IEnumerable<Batch> batches)
        {
            lvBatches.Items.Clear();
            var items = batches.Select(x =>
                new ListViewItem(new[] { x.Date.ToShortDateString(), x.BatchNumber, x.Code, x.NumberOfBatchBaseColors.ToString() })
                {
                    Tag = x
                }).ToArray();

            lvBatches.Items.AddRange(items);
            SelectBatch(null);
        }

        private void SelectBatch(Batch batch)
        {
            pgBatch.SelectedObject = batch;
            btGenerateQRCode.Enabled = batch != null;

            pbQrCode.Image = batch != null
                ? GenerateQrCode(batch)
                : null;

            rtbQrCodeContent.Text = batch != null
                ? GenerateContentString(batch)
                : string.Empty;
        }

        private Bitmap GenerateQrCode(Batch batch)
        {
            var content = GenerateContentString(batch);

            // Set options
            var options = new QrCodeOptions
            {
                Width = int.Parse(ConfigurationManager.AppSettings["fluidQrCodeWidth"]),
                Height = int.Parse(ConfigurationManager.AppSettings["fluidQrCodeHeight"]),
                Content = content
            };

            // Generate QR
            var bitmap = _qrCodeGenerator.GenerateQrCode(options);
            return bitmap;
        }

        private void GenerateQrCodeToClipboard(Batch batch)
        {
            try
            {
                var bitmap = GenerateQrCode(batch);
                Clipboard.SetImage(bitmap);

                MessageBox.Show($"QR Code voor batch '{batch.BatchNumber}' is op het klembord opgeslagen", "Gekopieerd", MessageBoxButtons.OK);
            }
            catch
            {

            }
        }

        private string GenerateContentString(Batch batch)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Batchnummer: {batch.BatchNumber}");
            sb.AppendLine($"Code: {batch.Code}");
            sb.AppendLine($"Omschrijving: {batch.Description}");
            sb.AppendLine($"Datum: {batch.Date.ToString("dd-MM-yyyy")}");
            sb.AppendLine("");
            sb.AppendLine("Basiskleur / Batch");

            foreach (var basiskleur in batch.BaseColors)
            {
                sb.AppendLine($"{basiskleur.BaseColorName} {basiskleur.BaseColorDescription} / {basiskleur.BaseColorBatchNumber}");
            }

            return sb.ToString();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            Search(tbSearch.Text);
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search(tbSearch.Text);
            }
        }

        private void lvBatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            var batch = lvBatches.SelectedItems.Count > 0
                ? (Batch)lvBatches.SelectedItems[0].Tag
                : null;

            SelectBatch(batch);
        }

        private void btGenerateQRCode_Click(object sender, EventArgs e)
        {
            var batch = (Batch)pgBatch.SelectedObject;
            GenerateQrCodeToClipboard(batch);
        }
    }
}
