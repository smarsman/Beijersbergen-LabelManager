using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using Beijersbergen.Library.QR;
using System.Text;
using Beijersbergen.Library.GSE;
using Beijersbergen.Gse;

namespace Beijersbergen.LabelManager
{
    public partial class GseUserControl : UserControl
    {
        private IBucketRepository _bucketRepository;
        private IQrCodeGenerator _qrCodeGenerator;

        public GseUserControl()
        {
            InitializeComponent();
            Buckets = new List<Bucket>();
        }

        protected override void OnLoad(EventArgs e)
        {
            //_bucketRepository = new StubBucketRepository();

            _bucketRepository = new SqlBucketRepository(
                ConfigurationManager.AppSettings["GseExportConnectionString"],
                new SqlFormulaNameRepository(ConfigurationManager.AppSettings["GseInternalConnectionString"]));
            //new SqlComponentNameRepository(ConfigurationManager.AppSettings["GseInternalConnectionString"]));

            _qrCodeGenerator = new XzingQrCodeGenerator();

            Search(string.Empty); // Query latest results

            base.OnLoad(e);
        }



        public IEnumerable<Bucket> Buckets { get; set; }
        

        private void Search(string bucketNumber)
        {
            int nr = -1;
            bool useNr = false;

            if (!string.IsNullOrWhiteSpace(bucketNumber))
            {
                useNr = int.TryParse(bucketNumber, out nr);
                if (!useNr)
                {
                    throw new Exception("Voer een geldig numeriek bucketnummer in");
                }
            }

            if (useNr)
            {
                var buckets = _bucketRepository.FindByBucketNumber(nr);
                SetBuckets(buckets);
            }
            else
            {
                var list = _bucketRepository.SelectLatest(20);
                SetBuckets(list);
            }
        }

        private void SetBuckets(IEnumerable<Bucket> buckets)
        {
            lvBatches.Items.Clear();
            var items = buckets.Select(x =>
                new ListViewItem(new[] { 
                    x.Date.ToShortDateString(), 
                    x.OrderCode,
                    x.BucketNumber.ToString(), 
                    x.BucketIndex.ToString(),
                    x.FormulaCode, 
                    x.NumberOfBatchBaseColors.ToString(),
                    $"{x.TotalDispensedWeightKg.ToString("0.##")} KG"    
                })
                {
                    Tag = x
                }).ToArray();

            lvBatches.Items.AddRange(items);
            SelectBucket(null);
        }

        private void SelectBucket(Bucket bucket)
        {
            pgBatch.SelectedObject = bucket;
            btPrintLabel.Enabled = bucket != null;

            rtbQrCodeContent.Text = bucket != null
                ? GenerateContentString(bucket)
                : string.Empty;
        }

        private string GenerateContentString(Bucket bucket)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Batch: {bucket.OrderCode}");
            sb.AppendLine($"Name: {bucket.FormulaName}");
            sb.AppendLine($"Code: {bucket.FormulaCode}");
            sb.AppendLine($"Datum: {bucket.Date.ToString("dd-MM-yyyy")}");
            /* 
            sb.AppendLine("");
            sb.AppendLine("Basiskleur / Batch");

            foreach (var basiskleur in bucket.BaseColors)
            {
                sb.AppendLine($"{basiskleur.ComponentCode} {basiskleur.ComponentName} / {basiskleur.LotCode}");
            }
            */

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
            var bucket = lvBatches.SelectedItems.Count > 0
                ? (Bucket)lvBatches.SelectedItems[0].Tag
                : null;

            SelectBucket(bucket);
        }

        private void btGenerateQRCode_Click(object sender, EventArgs e)
        {
            var bucket = (Bucket)pgBatch.SelectedObject;
            using (var dialog = new GsePrintDialog())
            {
                dialog.Bucket = bucket;
                dialog.ShowDialog();
            }
        }
    }
}
