using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;

namespace ParseSPL
{
    static class CustomBlockExtractor
    {
        //Returns a list of words, filtered by their font size
        public static IEnumerable<IEnumerable<Word>> GetWords(Page page)
        {
            var fontSizes = page.Letters.GroupBy(x => Math.Round(x.FontSize)).Distinct();
            var words = new List<Word>();


            foreach (var fontSize in fontSizes)
            {
                var letters = page.Letters.Where(x => Math.Round(x.FontSize) == fontSize.Key).ToList();

                yield return NearestNeighbourWordExtractor.Instance.GetWords(letters);
            }
        }

        public static IEnumerable<TextBlock> GetBlocks(Page page)
        {
            IEnumerable<TextBlock> blocks = new List<TextBlock>();

            foreach(IEnumerable<Word> words in GetWords(page))
            {
                blocks = blocks.Concat(RecursiveXYCut.Instance.GetBlocks(words));
            }

            return blocks;
        }
    }
}
