using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Beijersbergen.Library.Fluid
{
    public class Batch
    {
        [Category("Batch")]
        [DisplayName("Batchnummer")]
        [ReadOnly(true)]
        public string BatchNumber { get; set; }

        [Category("Batch")]
        [DisplayName("Datum")]
        [ReadOnly(true)]
        public DateTime Date { get; set; }

        [Category("Batch")]
        [DisplayName("Code")]
        [ReadOnly(true)]
        public string Code { get; set; }

        [Category("Batch")]
        [DisplayName("Omschrijving")]
        [ReadOnly(true)]
        public string Description { get; set; }

        [Category("Samenstelling")]
        [DisplayName("Basiskleuren")]
        public List<BatchBaseColor> BaseColors { get; set; } = new List<BatchBaseColor>();

        [Category("Kleuren")]
        [DisplayName("Aantal")]
        public int NumberOfBatchBaseColors => BaseColors.Count;
    }
}
