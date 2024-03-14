using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] passwords = new string[]
            {
                "12343254", "SOBAKA221", "KFKFGKKAS", "19991019", "EYOICECREAM"
            };
            IBlock genesis = new Block(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00 });
            byte[] difficult = new byte[] { 0x00, 0x00 };
            BlockChain chain = new BlockChain(difficult, genesis);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"----------------------- BLOCK {i+1} -----------------------");
                var data = Encoding.UTF8.GetBytes(passwords[i]);
                chain.Add(new Block(data.ToArray()));
                Console.WriteLine(chain.LastOrDefault()?.ToString());
                if (chain.IsHashValid())
                {
                    Console.WriteLine("BLOCKCHAIN IS VALID");
                }
            }
            Console.Read();
        }
    }
}
