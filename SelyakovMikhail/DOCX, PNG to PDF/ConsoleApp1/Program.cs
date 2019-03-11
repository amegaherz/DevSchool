using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Михаил\\Desktop\\HW\\PNG\\";
            string pdfPath = "C:\\Users\\Михаил\\Desktop\\HW\\PDF\\";
            var filenames = Directory.EnumerateFiles(filePath, "*").Select(Path.GetFileName);
            /*  foreach (var element in filenames)
            {
                string extension = Path.GetExtension(element);
                if (extension == ".png")
                {
                    Program.CreatePDF(filePath + element, pdfPath + element);
                }
               else if (extension == ".docx")
                {
                    Program.CreatePDFWord(filePath + element, pdfPath + element);
                }
                else
                {
                    Console.WriteLine("Nothing to do");
                }
            }*/
            Parallel.ForEach(filenames, (currentFile) =>
            {
                string extension = Path.GetExtension(currentFile);
                if (extension == ".png")
                {
                    Program.CreatePDF(filePath + currentFile, pdfPath + currentFile);
                }
                else if (extension == ".docx")
                {
                    Program.CreatePDFWord(filePath + currentFile, pdfPath + currentFile);
                }
                else
                {
                    Console.WriteLine("Nothing to do");
                }
                Console.WriteLine($"Processing {currentFile} on thread {Thread.CurrentThread.ManagedThreadId}");
            });
        }

        public static void CreatePDF(string pngPath, string pdfPath)
        {
            iTextSharp.text.Rectangle pageSize = null;
            using (var srcImage = new Bitmap(pngPath))
            {
                pageSize = new iTextSharp.text.Rectangle(0, 0, srcImage.Width, srcImage.Height);
            }
            using (var ms = new MemoryStream())
            {
                var doc = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms).SetFullCompression();
                doc.Open();
                var image = iTextSharp.text.Image.GetInstance(pngPath);
                doc.Add(image);
                doc.Close();
                pdfPath = Path.ChangeExtension(pdfPath, ".pdf");
                File.WriteAllBytes(pdfPath, ms.ToArray());
            }
        }
        public static void CreatePDFWord(string wordPath, string pdfwPath)
        {
            iDiTect.Converter.Licensing.LicenseManager.SetKey("CLTCN-MQF93-49AQ4-KCSXS-FVZWP-X49BW");
            iDiTect.Converter.DocxToPdfConverter converter = new iDiTect.Converter.DocxToPdfConverter();
            using (Stream stream = File.OpenRead(wordPath))
            {
                converter.Load(stream);
            }
            pdfwPath = Path.ChangeExtension(pdfwPath, ".pdf");
            File.WriteAllBytes(pdfwPath, converter.SaveAsBytes());
        }
    }
}