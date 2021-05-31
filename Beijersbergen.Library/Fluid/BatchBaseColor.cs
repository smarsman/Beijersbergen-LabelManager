
using System.ComponentModel;

namespace Beijersbergen.Library.Fluid
{
    [DisplayName("Base color")]
    public class BatchBaseColor
    {
        [DisplayName("Batchnummer")]
        public string BatchNumber { get; set; }

        [DisplayName("Mengsysteem")]
        public string MixingSystem { get; set; }

        [DisplayName("Kleur")]
        public string BaseColorName { get; set; }

        [DisplayName("Omschrijving")]
        public string BaseColorDescription { get; set; }

        [DisplayName("Basis batchnummer")]
        public string BaseColorBatchNumber { get; set; }
    }
}
