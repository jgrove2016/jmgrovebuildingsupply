using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace JG_Prospect.Installer
{


    public class PCPrint : System.Drawing.Printing.PrintDocument
    {
        #region  Property Variables

        private Font _font;

        private string _text;

        #endregion

        public string TextToPrint
        {
            get { return _text; }
            set { _text = value; }
        }
        public Font PrinterFont
        {
            // Allows the user to override the default font
            get { return _font; }
            set { _font = value; }
        }

        static int curChar;
        public PCPrint()
            : base()
        {
            //Set the file stream
            //Instantiate out Text property to an empty string
            _text = string.Empty;
        }
        public PCPrint(string str)
            : base()
        {
            //Set the file stream
            //Set our Text property value
            _text = str;
        }
        public void PrintDocument(string text)
        {

            //Create an instance of our printer class
            PCPrint printer = new PCPrint();
            //Set the font we want to use
            printer.PrinterFont = new Font("Verdana", 10);
            //Set the TextToPrint property

            printer.TextToPrint = text;
            //Issue print command
            printer.Print();
        }
        //protected override void onbeginPrint(System.Drawing.Printing.PrintEventArgs e)
        //{
        //    // Run base code
           
        //    base.onbeginPrint(e);
        //    //Check to see if the user provided a font
        //    //if they didn't then we default to Times New Roman
        //    if (_font == null)
        //    {
        //        //Create the font we need
        //        _font = new Font("Times New Roman", 10);
        //    }
        //}

        //protected override void OnPrintPage(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    // Run base code
        //    base.OnPrintPage(e);
        //    //Declare local variables needed
        //    int printHeight;
        //    int printWidth;
        //    int leftMargin;
        //    int rightMargin;
        //    Int32 lines;
        //    Int32 chars;
        //    //Set print area size and margins
        //    {
        //        printHeight = base.DefaultPageSettings.PaperSize.Height - base.DefaultPageSettings.Margins.Top - base.DefaultPageSettings.Margins.Bottom;
        //        printWidth = base.DefaultPageSettings.PaperSize.Width - base.DefaultPageSettings.Margins.Left - base.DefaultPageSettings.Margins.Right;

        //        leftMargin = base.DefaultPageSettings.Margins.Left;  //X

        //        rightMargin = base.DefaultPageSettings.Margins.Top;  //Y
        //    }
        //    //Check if the user selected to print in Landscape mode
        //    //if they did then we need to swap height/width parameters
        //    if (base.DefaultPageSettings.Landscape)
        //    {
        //        int tmp;
        //        tmp = printHeight;
        //        printHeight = printWidth;
        //        printWidth = tmp;
        //    }
        //    //Now we need to determine the total number of lines
        //    //we're going to be printing
        //    Int32 numLines = (int)printHeight / PrinterFont.Height;
        //    //Create a rectangle printing are for our document
        //    RectangleF printArea = new RectangleF(leftMargin, rightMargin, printWidth, printHeight);
        //    //Use the StringFormat class for the text layout of our document
        //    StringFormat format = new StringFormat(StringFormatFlags.LineLimit);
        //    //Fit as many characters as we can into the print area     
        //    e.Graphics.MeasureString(_text.Substring(RemoveZeros(curChar)), PrinterFont, new SizeF(printWidth, printHeight), format, out chars, out lines);
        //    //Print the page
        //    e.Graphics.DrawString(_text.Substring(RemoveZeros(curChar)), PrinterFont, Brushes.Black, printArea, format);
        //    //Increase current char count
        //    curChar += chars;
        //    //Detemine if there is more text to print, if
        //    //there is the tell the printer there is more coming
        //    if (curChar < _text.Length)
        //    {
        //        e.HasMorePages = true;
        //    }
        //    else
        //    {
        //        e.HasMorePages = false;
        //        curChar = 0;
        //    }
        //}
        public int RemoveZeros(int value)
        {
            //Check the value passed into the function,
            //if the value is a 0 (zero) then return a 1,
            //otherwise return the value passed in
            switch (value)
            {
                case 0:
                    return 1;
                default:
                    return value;
            }
        }
       

    }
}