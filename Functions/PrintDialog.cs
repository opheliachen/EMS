using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;

namespace EMSSystem.Functions
{
    class PrintDialog
    {
        Bitmap itemBitmap;
        string printPaperSize;

        public PrintDialog()
        {
        }

        public void PrintBitmap(Bitmap img, string paperSize)
        {
            PrintDocument pdocPrintLists = new PrintDocument();
            pdocPrintLists.PrintPage += this.pdocPrintLists_PrintPage;
            //pdocPrintLists.PrintPage += new System.Drawing.Printing.PrintPageEventArgs(object, this.pdocPrintLists_PrintPage);

            //if (printPaperSize == "notice")
            //    pdocPrintLists.DefaultPageSettings.Landscape = true;
            //else
                pdocPrintLists.DefaultPageSettings.Landscape = false;

            pdocPrintLists.DefaultPageSettings.Margins = new Margins(50, 0, 50, 0);
            pdocPrintLists.PrinterSettings.PrintToFile = false;

            if (paperSize == "A4")
                pdocPrintLists.DefaultPageSettings.PaperSize = new PaperSize("A4", 900, 1100);
            else if (paperSize == "notice")
                pdocPrintLists.DefaultPageSettings.PaperSize = new PaperSize("Dot Matrix", 900, 500);
            else
                pdocPrintLists.DefaultPageSettings.PaperSize = new PaperSize("Dot Matrix", 700, 500);

            itemBitmap = img;
            printPaperSize = paperSize;

            pdocPrintLists.Print();
        }

        private void pdocPrintLists_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap itemBitmap = ((System.Windows.Forms.Application.StartupPath + ("\\temp.jpeg")));

            if (printPaperSize == "A4")
                e.Graphics.DrawImage(itemBitmap, 10, 5, 800, 1100);
            else if (printPaperSize == "notice")
                e.Graphics.DrawImage(itemBitmap, 0, 0, 750, 500);
            else if (printPaperSize == "needtopaybyclass")
                e.Graphics.DrawImage(itemBitmap, -1, -1, 800, 1100);
            else
                e.Graphics.DrawImage(itemBitmap, -1, -1, 700, 465);
        }
    }
}
