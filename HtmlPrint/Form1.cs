using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string PrinterName = "Microsoft XPS Document Writer";//"\\\\10.32.11.113\\三楼大厅备"

        private void button1_Click(object sender, EventArgs e)
        {
            // Allow the user to select a file.
            OpenFileDialog ofd = new OpenFileDialog();
            if (DialogResult.OK == ofd.ShowDialog(this))
            {

                //PrintDialog pd = new PrintDialog();
                // pd.PrinterSettings = new PrinterSettings();
                // //pd.PrintToFile = true;
                // if (DialogResult.OK == pd.ShowDialog(this))
                // {

                //     //RawPrinterHelper.SendFileToPrinter(pd.PrinterSettings.PrinterName, ofd.FileName);
                //     Print(pd.PrinterSettings.PrinterName, ofd.FileName);

                // }

               // Print(PrinterName, ofd.FileName);

                  PrintHtmlPage(ofd.FileName);
            }
        }

        void Print(string printerName,string fileName)
        {
            Process printjob = new Process();

            printjob.StartInfo.FileName = fileName;
            printjob.StartInfo.UseShellExecute = true;
            printjob.StartInfo.Verb = "PrintTo";
            printjob.StartInfo.Arguments = printerName;
            printjob.StartInfo.CreateNoWindow = false;
            printjob.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            printjob.Start();
        }


        private void PrintHtmlPage(string fileName)
        {
            //HtmlPrinterHelper.SetDefaultPrinter(PrinterName);
            HtmlPrinterHelper.ClearWebDocumentHeaderAndFooter();
            // Create a WebBrowser instance. 
            WebBrowser webBrowserForPrinting = new WebBrowser();

            // Add an event handler that prints the document after it loads.
            webBrowserForPrinting.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(PrintDocument);

            // Set the Url property to load the document.
            webBrowserForPrinting.Url = new Uri(fileName);
        }

        private void PrintDocument(object sender,
            WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
           ((WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();

            //HtmlPrinterHelper.ResumeWebDocumentHeaderAndFooter();
        }
    }
}
