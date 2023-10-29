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
            double closestDistance = double.MaxValue;
            TextBlock closestBlock = null;
            foreach (var block in blocks)
            {
                //if the block is the same as the selected block, ignore it
                if (block == selectedBlock) continue;

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

        public static List<TextBlock> FindTextBlocksAlongHorizontalAxis(TextBlock selectedBlock, IEnumerable<TextBlock> blocks, float tolerance)
        {
            List<TextBlock> textBlocks = new List<TextBlock>();

            foreach (var block in blocks)
            {
                //if the block is the same as the selected block, ignore it
                if (block == selectedBlock) continue;

                //if the block is not on the same horizontal axis as the selected block, ignore it
                if (Math.Abs(selectedBlock.BoundingBox.Bottom - block.BoundingBox.Bottom) < tolerance) continue;

                //add the block to the list
                textBlocks.Add(block);
            }
        }

        public static List<ServiceProvider> GetServiceProvidersFromBlocks(IEnumerable<TextBlock> blocks)
        {
            List<ServiceProvider> services = new List<ServiceProvider>();

            List<TextBlock> serviceBlocks = new List<TextBlock>();

            //This is not C++, declaring variables on the same line will guarantee that they are the same type
            TextBlock serviceHeadingBlock, providerHeadingBlock, contactInfoHeadingBlock, estimateHeadingBlock;
            try
            {
                //get the column heading blocks
                serviceHeadingBlock = FindBlockExact(blocks, "service");
                providerHeadingBlock = FindBlockExact(blocks, "provider we identified");
                estimateHeadingBlock = FindBlockExact(blocks, "estimate");
                contactInfoHeadingBlock = FindBlockExact(blocks, "contact information");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("There was an error capturing the columns");
            }

            foreach (var block in blocks)
            {
                //if the block is the same as the selected blocks, ignore it
                if (block == serviceHeadingBlock || block == providerHeadingBlock || block == contactInfoHeadingBlock || block == estimateHeadingBlock) continue;

                //if the block is above the service heading, ignore it
                if (block.BoundingBox.Bottom > serviceHeadingBlock.BoundingBox.Top) continue;

                if (FindClosestTextBlock(block, new List<TextBlock>
                {
                    serviceHeadingBlock,
                    providerHeadingBlock,
                    contactInfoHeadingBlock,
                    estimateHeadingBlock
                }) == serviceHeadingBlock)
                {
                    //if the current block contains "title" in the text, append it to serviceBlocks
                    if (block.Text.ToLower().Contains("title"))
                    {
                        serviceBlocks.Add(block);
                    }
                };
            }

            foreach(var block in serviceBlocks)
            {

            }

            return services;
        }
    }
}
