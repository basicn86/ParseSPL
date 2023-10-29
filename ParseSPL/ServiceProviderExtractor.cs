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

        //finds the closest textblock to the selected block from a list of blocks
        public static TextBlock FindClosestTextBlock(TextBlock selectedBlock, IEnumerable<TextBlock> blocks)
        {
            double closestDistance = int.MaxValue;
            TextBlock closestBlock = null;
            foreach (var block in blocks)
            {
                //if the block is the same as the selected block, ignore it
                if (block == selectedBlock) continue;

                //if the block is above the selected block, ignore it
                if (block.BoundingBox.Bottom > selectedBlock.BoundingBox.Top) continue;

                //get the distance between the blocks
                double distance = Math.Abs(selectedBlock.BoundingBox.Left - block.BoundingBox.Left);

                //if the distance is less than the closest distance, set the closest block to the current block
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBlock = block;
                }
            }

            //if the closest block is null, throw an exception
            if (closestBlock == null) throw new Exception("No closest block found");

            //return the closest block
            return closestBlock;
        }

        public static List<ServiceProvider> GetServiceProvidersFromBlocks(IEnumerable<TextBlock> blocks)
        {
            List<ServiceProvider> services = new List<ServiceProvider>();

            //This is not C++, declaring variables on the same line will guarantee that they are the same type
            TextBlock serviceBlock, providerBlock, contactInfoBlock;
            try
            {
                //get the column heading blocks
                serviceBlock = FindBlockExact(blocks, "service");
                providerBlock = FindBlockExact(blocks, "provider we identified");
                contactInfoBlock = FindBlockExact(blocks, "contact information");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("GetServiceProvidersFromBlocks: There was an error capturing the columns");
            }

            foreach (var block in blocks)
            {
                //if the block is the same as the selected blocks, ignore it
                if (block == serviceBlock || block == providerBlock || block == contactInfoBlock) continue;

                //if the block is above the service block, ignore it
                if (block.BoundingBox.Bottom > serviceBlock.BoundingBox.Top) continue;

                
            }

            return services;
        }
    }
}
