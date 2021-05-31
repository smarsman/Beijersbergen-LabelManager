using Beijersbergen.Library.GSE;
using Beijersbergen.Zebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beijersbergen.LabelManager
{
    public partial class GsePrintDialog : Form
    {
        private static int defaultMagnificationFactor = 4;

        public GsePrintDialog()
        {
            InitializeComponent();
        }

        public Bucket Bucket { get; set; }

        private void GsePrintDialog_Load(object sender, EventArgs e)
        {
            cbCorrosiveIndicator.Checked = true;
            populateMagnificationFactors(defaultMagnificationFactor.ToString());
            renderPreview();
        }

        private void populateMagnificationFactors(string defaultValue)
        {
            // Add magnifications factors to dropdown
            var factors = Enumerable.Range(1, 10)
                .Select(x => x.ToString())
                .ToArray();
            cbMagnificationFactor.Items.AddRange(factors);

            cbMagnificationFactor.SelectedIndex = cbMagnificationFactor.FindStringExact(defaultValue);
        }

        private void renderPreview()
        {
            var label = GenerateLabel();

            var zpl = label.GenerateZPL("template.txt");
            var previewer = new ZplPreviewer();
            var image = previewer.RenderZpl(zpl);

            pbPreview.Image = image;
        }

        private Zebra.Label GenerateLabel()
        {
            var label = new Beijersbergen.Zebra.Label();
            label.ShowCorrosiveIndicator = cbCorrosiveIndicator.Checked;

            label.Name = Bucket.FormulaName;
            label.ArticleCode = Bucket.FormulaCode;
            label.BatchNumber = Bucket.OrderCode;
            label.ProductionDate = Bucket.Date;
            label.Weight = $"{string.Format("{0:0}", Bucket.TotalDispensedWeightKg)} kilo";
            label.QrCodeContent = GenerateContentString(label, Bucket);

            var factor = cbMagnificationFactor.SelectedItem != null
                ? int.Parse(cbMagnificationFactor.SelectedItem.ToString())
                : defaultMagnificationFactor;

            label.MagnificationFactor = factor;

            return label;
        }

        private string GenerateContentString(Zebra.Label label, Bucket bucket)
        {
            
            const string newLine = "_0A"; // _0D_0A

            var lines = new[]
            {
                $"Batch: {label.BatchNumber}",
                $"Name: {label.Name}",
                $"Code: {label.ArticleCode}",
                $"Datum: {label.ProductionDate.ToString("dd-MM-yyyy")}",
                //$"Basiskleur / Batch"
            };

            /*
            foreach (var basiskleur in bucket.BaseColors)
            {
                lines = lines.Append($"{basiskleur.ComponentCode} {basiskleur.ComponentName} / {basiskleur.LotCode}").ToArray();
            }
            */

            var charCount =  lines.Sum(x => x.Length);
            var newLineCount = (lines.Length - 1) * 1;

            var sb = new StringBuilder();
            sb.Append(lines.First());

            foreach(var line in lines.Skip(1))
            {
                sb.Append($"{newLine}{line}");
            };


            var prefix = $"B{(charCount + newLineCount).ToString("D4")}";
            var final = prefix + sb.ToString();

            return final;
        }

        private void cbCorrosiveIndicator_CheckedChanged(object sender, EventArgs e)
        {
            renderPreview();
        }

        private void tbBatchnumber_TextChanged(object sender, EventArgs e)
        {
            renderPreview();
        }

        private void cbMagnificationFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            renderPreview();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            var label = GenerateLabel();
            //if (string.IsNullOrWhiteSpace(label.BatchNumber))
            //{
            //    MessageBox.Show("U dient een batchnummer op te geven");
            //    return;
            //}

            var printer = new Printer();
            printer.PrintZpl(label.GenerateZPL("template.txt"), Printer.ConnectionType.Network);
        }
    }
}
