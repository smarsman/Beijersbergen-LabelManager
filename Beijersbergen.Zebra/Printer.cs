using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;

namespace Beijersbergen.Zebra
{
    public class Printer
    {
        private static Connection GetDefaultUsbPrinterConnection() => 
            UsbDiscoverer.GetZebraUsbPrinters()
                ?.FirstOrDefault()?.GetConnection() 
            ?? throw new ApplicationException("No Zebra printer found via USB");

        private static Connection GetNetworkPrinterConnection() =>
            ConnectionBuilder.Build($"TCP:{ConfigurationManager.AppSettings["ZebraIP"]}");


        public void PrintZpl(string zplText, ConnectionType connectionType)
        {
            switch(connectionType)
            {
                case ConnectionType.Usb:
                    PrintUsb(zplText);
                    break;
                case ConnectionType.Network:
                    PrintNetwork(zplText);
                    break;
                default:
                    throw new NotSupportedException($"Unsupported printer connection type '{connectionType}'");
            }
        }

        private void PrintUsb(string zplText)
        {
            var connection = GetDefaultUsbPrinterConnection();

            try
            {
                connection.Open();

                var data = Encoding.UTF8.GetBytes(zplText);
                connection.Write(data);

                connection.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (connection.Connected)
                {
                    connection.Close();
                }
            }
        }

        private void PrintNetwork(string zplText)
        {
            var connection = GetNetworkPrinterConnection();

            try
            {
                connection.Open();

                var data = Encoding.UTF8.GetBytes(zplText);
                connection.Write(data);

                connection.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (connection.Connected)
                {
                    connection.Close();
                }
            }
        }


        public enum ConnectionType
        {
            Usb = 1,
            Network = 2
        }
    }
}
