using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    internal class BlockChain : IEnumerable<IBlock>
    {
        private List<IBlock> _blocks = new List<IBlock>();
        public BlockChain(byte[] difficult, IBlock genesisBlock)
        {
            Difficult = difficult;
            genesisBlock.Hash = genesisBlock.MineHash(difficult);
            _blocks.Add(genesisBlock);
        }
        public byte[] Difficult { get; set; }
        public void Add(IBlock item)
        {
            if(_blocks.LastOrDefault().Hash != null)
            {
                item.PreviousHash = _blocks.LastOrDefault().Hash;
            }
            item.Hash = item.MineHash(Difficult);
            Blocks.Add(item);
        }
        public List<IBlock> Blocks
        {
            get => _blocks;
            set => _blocks = value;
        }
        public int Count => _blocks.Count;
        public IBlock this[int index]
        {
            get => Blocks[index];
            set => Blocks[index] = value;
        }
        public IEnumerator<IBlock> GetEnumerator()
        {
            return Blocks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Blocks.GetEnumerator();
        }
    }
}
