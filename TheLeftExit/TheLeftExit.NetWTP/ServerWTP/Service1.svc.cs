using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SautinSoft;
using Word = Microsoft.Office.Interop.Word;
using System.IO;

namespace ServerWTP
{
    public class Service1 : IService1
    {
        private PdfMetamorphosis MM;
        private Word.Application WApp;

        private readonly string docname = Path.GetTempPath()+"WTPtemp.docx";
        private readonly string pdfname = Path.GetTempPath()+"WTPtemp.pdf";

        // В начале работы подгружаем обработчики.
        Service1()
        {
            MM = new PdfMetamorphosis();
            WApp = new Word.Application();
        }

        // В конце работы убиваем Word. Хотя скорее всего он не убьётся.
        ~Service1()
        {
            WApp.Quit();
        }

        public WTPResponse ConvertWithMM(byte[] binary)
        {
            // CrEaTeD uSiNg TrIaL vErSiOn Of PdFmEtAmOrPhOsIs
            try
            {
                return new WTPResponse(MM.DocxToPdfConvertByte(binary));
            }
            catch(Exception e)
            {
                return new WTPResponse(e.Message);
            }
        }

        public WTPResponse ConvertWithWord(byte[] binary)
        {
            try
            {
                // 1. Сохраняем бинарник в документ
                File.WriteAllBytes(docname, binary);
                // 2. Работаем с Interop
                var WDoc = WApp.Documents.Open(docname);
                WDoc.ExportAsFixedFormat(pdfname, Word.WdExportFormat.wdExportFormatPDF);
                WDoc.Close();
                // 3. Записываем результат в бинарник и прибираемся
                var res = File.ReadAllBytes(pdfname);
                File.Delete(docname);
                File.Delete(pdfname);

                return new WTPResponse(res);
            }
            catch(Exception e)
            {
                return new WTPResponse(e.Message);
            }
        }
    }
}
