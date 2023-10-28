

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
            
            //get the blocks
            var blocks = CustomBlockExtractor.GetBlocks(page);

            //print the blocks
            int i = 0;
            foreach (var block in blocks)
            {
                Console.WriteLine("BLOCK: " + i++);
                Console.WriteLine(block.Text);
                Console.WriteLine();
            }
        }
    }
}