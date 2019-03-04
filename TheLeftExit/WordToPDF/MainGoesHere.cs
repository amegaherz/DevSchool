using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using SautinSoft;

namespace WordToPDF
{
    public static class MainGoesHere
    {
        /// <summary>
        /// Получить пути ко всем файлам в каталоге dir, соответствующим условию формат (например, "*.docx").
        /// </summary>
        public static List<string> GetFilesByPattern(string dir, string format)
        {
            // var dinfo = new DirectoryInfo(dir);
            // var finfo = dinfo.EnumerateFiles(format).ToList();
            // return finfo.ConvertAll(item => item.FullName);
            // или же просто
            return ((new DirectoryInfo(dir)).EnumerateFiles(format).ToList()).ConvertAll(item => item.FullName);
        }

        /// <summary>
        /// Извлечь имя файла из его полного пути и сменить его расширение.
        /// </summary>
        public static string GetName(string fullname, string ext = "")
        {
            var nameonly = fullname.Split('\\').Last();
            if (ext == "")
                return nameonly;
            return nameonly.Remove(nameonly.LastIndexOf('.')+1)+ext;
        }

        public static void Main()
        {
            var p = new PdfMetamorphosis();
            // Если запущено на моём ноуте, работать с папкой с шестнадцатью DOCX с "Hello world" и одним PNG.
            var elsewhere = true;
            var frompath = elsewhere ? Console.ReadLine() : @"C:\Users\Никита\Desktop\docx";
            var topath = elsewhere ? Console.ReadLine() : @"C:\Users\Никита\Desktop\pdf";
            p.HtmlSettings.BaseUrl = frompath;

            // Шаг 1. Обрабатываем PNG файлы.
            // Так как библиотека работает только с преобразованием, НО поддерживает преобразование HTML=>PDF,
            // можно создать простейшую веб-страницу с <img>изображением</img> и преобразовать её.
            // Альтернатива в Microsoft.Office.Interop.Word: вставить картинку в новый документ, затем преобразовать документ.
            var pngs = GetFilesByPattern(frompath, "*.png");
            foreach (var x in pngs) {
                var htmlvalue = $"<html><body><img src=\"{GetName(x)}\"></body></html>";
                p.HtmlToPdfConvertStringToFile(htmlvalue, topath + '\\' + GetName(x, "pdf"));
            }

            // Шаг 2. Обрабатываем DOCX файлы.
            var docxs = GetFilesByPattern(frompath, "*.docx");
            foreach (var x in docxs)
                    p.DocxToPdfConvertFile(x, topath+'\\'+GetName(x,"pdf"));
        }
    }
}
