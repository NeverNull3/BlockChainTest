using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public interface IBlock
    {
        byte[] Hash { get; set; }
        byte[] Data { get; }
        int Nonce { get; set; }
        byte[] PreviousHash { get; set; } 


        DateTime Time { get; set; }
        
    }
    public class Block : IBlock
    {
        public Block(byte[] data)
        {
            Data = data;
            Nonce = 0;
            PreviousHash = new byte[] { 0x00 };
            Time = DateTime.Now;
        }
        public byte[] Hash { get; set; }
        public byte[] Data { get; }
        public int Nonce { get; set; }
        public byte[] PreviousHash { get; set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"{BitConverter.ToString(Hash).Replace("-", "")} :\n {BitConverter.ToString(PreviousHash).Replace("-", "")} \n {Nonce} {Time}";
        }
    }


}
    