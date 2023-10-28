

using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace ParseSPL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //if args are empty, then return
            if (args.Length == 0) return;

            PdfDocument doc = PdfDocument.Open(args[0]);
            Page page = doc.GetPage(1); //assume the first page is the SPL
            IEnumerable<Word> words = CustomWordExtractor.GetWords(page);
        }
    }
}