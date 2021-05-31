using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Library.QR
{
    public class QrCodeOptions
    {
        /// <summary>
        /// Gets or sets the width of the barcode bitmap in pixels.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the barcode bitmap in pixels.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the content of the QR code.
        /// </summary>
        public string Content { get; set; }
    }
}
