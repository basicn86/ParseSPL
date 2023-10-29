using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.DocumentLayoutAnalysis;

namespace ParseSPL
{
    internal class ServiceProviderExtractor
    {
        //find block if it contains a certain text
        //not recommended to use, very slow
        public static TextBlock FindBlockContains(IEnumerable<TextBlock> blocks, string name)
        {
            foreach (var block in blocks)
            {
                if (block.Text.ToLower().Contains(name.ToLower()))
                {
                    return block;
                }
            }

            //throw an exception if the block is not found
            throw new Exception("Block not found");
        }

        //finds a block, case insensitive
        public static TextBlock FindBlockExact(IEnumerable<TextBlock> blocks, string name)
        {
            foreach (var block in blocks)
            {
                if(block.Text.ToLower() == name.ToLower())
                {
                    return block;
                }
            }

            //throw an exception if the block is not found
            throw new Exception("Block not found");
        }

        public static List<ServiceProvider> GetServiceProvidersFromBlocks(IEnumerable<TextBlock> blocks)
        {
            List<ServiceProvider> services = new List<ServiceProvider>();
            //find the block with "service" as the text
            try
            {
                var serviceBlock = FindBlockExact(blocks, "service");
                var providerBlock = FindBlockExact(blocks, "provider we identified");
                var contactInfoBlock = FindBlockExact(blocks, "contact information");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("GetServiceProvidersFromBlocks: There was an error capturing the columns");
            }
            

            return services;
        }
    }
}
