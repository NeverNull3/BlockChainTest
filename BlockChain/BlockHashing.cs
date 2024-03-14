using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    static class BlockHashing
    {
        public static byte[] GenerateHash(this IBlock block)
        {
            using(SHA512 sha512 = new SHA512Managed())
            {
                using (MemoryStream stream = new MemoryStream())
                using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                {
                    binaryWriter.Write(block.Data);
                    binaryWriter.Write(block.Nonce);
                    binaryWriter.Write(block.PreviousHash);
                    binaryWriter.Write(block.Time.ToString());
                    var s = stream.ToArray();
                    return sha512.ComputeHash(s);
                }
            }
        }

        public static byte[] MineHash(this IBlock block, byte[] difficult)
        {
            if (difficult == null)
            {
                throw new ArgumentNullException(nameof(difficult));
            }
            byte[] hash = new byte[0];
            while (!hash.Take(2).SequenceEqual(difficult))
            {
                block.Nonce++;
                hash = block.GenerateHash();
            }
            return hash;
        }

        public static bool IsHashValid(this IBlock block)
        {
            var b = block.GenerateHash();
            return block.Hash.SequenceEqual(b);
        }
        public static bool IsPreviousHashValid(this IBlock block, IBlock prevBlock)
        {
            if(prevBlock == null)
                throw new ArgumentNullException(nameof(prevBlock));
            return prevBlock.IsHashValid() && block.PreviousHash.SequenceEqual(prevBlock.Hash);
        }

        public static bool IsHashValid(this IEnumerable<IBlock> items)
        {
            var enums = items.ToList();
            return enums.Zip(enums.Skip(1), Tuple.Create).All(block => block.Item2.IsHashValid() && block.Item2.IsPreviousHashValid(block.Item2));
        }
    }
}
