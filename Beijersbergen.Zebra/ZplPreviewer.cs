using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Zebra
{
    public class ZplPreviewer
    {
        public Image RenderZpl(string zpl)
        {
            // Oude settings
            // const int dpi = 203; // 203 or 300
            // const int dpiMm = 12; // 6, 8, 12, 24
            // var widthInches = 1039 / dpi;
            // var heightInches = 1982 / dpi;

            const int dpi = 203; // 203 or 300
            const int dpiMm = 8; // 6, 8, 12, 24

            var widthInches = 5;
            var heightInches = 10;


            // From: http://labelary.com/service.html#csharp

            byte[] zplData = Encoding.UTF8.GetBytes(zpl);
            var url = $"http://api.labelary.com/v1/printers/{dpiMm}dpmm/labels/{widthInches}x{heightInches}/0/";

            // adjust print density (8dpmm), label width (4 inches), label height (6 inches), and label index (0) as necessary
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            //request.Accept = "application/pdf"; // omit this line to get PNG images back
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = zplData.Length;
            request.Headers.Add("X-Rotation", "90");

            var requestStream = request.GetRequestStream();
            requestStream.Write(zplData, 0, zplData.Length);
            requestStream.Close();

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                {
                    var img = Image.FromStream(responseStream);
                    return img;
                }
            }
            catch (WebException e)
            {
                Console.WriteLine("Error: {0}", e.Status);
            }

            throw new NotImplementedException();
        }
    }
}
