using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis;

namespace ParseSPL
{
    internal class Program
    {
        public static DateTime GetDateFromBlocks(IEnumerable<TextBlock> blocks)
        {
            //iterate through each block
            foreach (var block in blocks)
            {
                Regex dateRegex = new Regex(@"\d{1,2}[\/\- ]\d{1,2}[\/\- ]\d{2,4}");
                Match match = dateRegex.Match(block.Text);

                if (match.Success)
                {
                    //try to parse the datetime, on fail: continue to the next iteration
                    if (DateTime.TryParse(match.Value, out DateTime date))
                    {
                        return date;
                    }
                }
            }
            //if no date is found, throw an exception that the date was not found
            throw new Exception("Date not found");
        }

        static void Main(string[] args)
        {
            //if args are empty, then return
            if (args.Length == 0) return;

            PdfDocument doc = PdfDocument.Open(args[0]);
            Page page = doc.GetPage(1); //assume the first page is the SPL
            
            //get the blocks
            var blocks = CustomBlockExtractor.GetBlocks(page);

            //try to get the date from the blocks
            try
            {
                DateTime date = GetDateFromBlocks(blocks);
                Console.WriteLine("Date: " + date);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //get the service providers
            try
            {
                var services = ServiceProviderExtractor.GetServiceProvidersFromBlocks(blocks);

                //print the service providers
                uint i = 0;
                foreach (var service in services)
                {
                    Console.WriteLine("SERVICE NO: " + i++);
                    Console.WriteLine("SERVICE: " + service.Service);
                    Console.WriteLine("NAME: " + service.Provider);
                    Console.WriteLine("CONTACT INFO: " + service.ContactInfo);
                    Console.WriteLine();
                }
            } catch (Exception e)
            {
                Console.WriteLine("Could not parse SPL: " + e.Message);
            }
        }
    }
}