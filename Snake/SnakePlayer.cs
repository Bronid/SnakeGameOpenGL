using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace TetrisGameOpenGL.Snake
{
    public class SnakePlayer
    {
        public bool Alive { get; set; } = true;
        public int[] HeadCoords { get; set; }
        public Queue<int[]> TailCoords { get; set; } = new Queue<int[]>();
        public EnumDirection CurrentDirection { get; set; }
        public bool DirectionChanged { get; set; } = false;
        public int Score { get; set; } = 0;
        public float[] TailColor = null;
        public float[] HeadColor = null;
        public SnakePlayer(int _initX, int _initY, int _initBodyLength, Field _field, float[] tailColor, float[] headColor)
        {
            HeadCoords = new int[2];
            HeadCoords[0] = _initX;
            HeadCoords[1] = _initY;
            TailColor = tailColor;
            HeadColor = headColor;
            _field.setToField(new Block(EnumBlockType.Head, HeadColor), _initX, _initY);
            for (int i = _initBodyLength; i >= 0; i--)
            {
                TailCoords.Enqueue(new int[2] { _initX - i - 1, _initY });
                _field.setToField(new Block(EnumBlockType.Tail, TailColor), _initX - i - 1, _initY);
            }
            CurrentDirection = EnumDirection.Right;
        }

        public void Wipe()
        {
            HeadCoords = null;
            TailCoords = null;
        }

        public void Move(Field _field)
        {
            TailCoords.Enqueue(new int[2]{ HeadCoords[0], HeadCoords[1] });
            _field.setToField(new Block(EnumBlockType.Tail, TailColor), HeadCoords[0], HeadCoords[1]);

            switch (CurrentDirection)
            {
                case EnumDirection.Up:
                    HeadCoords[1] += 1;
                    break;
                case EnumDirection.Down:
                    HeadCoords[1] -= 1;
                    break;
                case EnumDirection.Left:
                    HeadCoords[0] -= 1;
                    break;
                case EnumDirection.Right:
                    HeadCoords[0] += 1;
                    break;
            }

            EnumBlockType currentBlockType = _field.getBlockByCoords(HeadCoords[0], HeadCoords[1]).type;

            if (currentBlockType == EnumBlockType.Border
                || currentBlockType == EnumBlockType.Tail)
            {
                Alive = false;
                Wipe();
                return;
            }

            _field.setToField(new Block(EnumBlockType.Head, HeadColor), HeadCoords[0], HeadCoords[1]);

            if (currentBlockType != EnumBlockType.Food)
            {
                int[] coordsToDelete = TailCoords.Dequeue();
                _field.setToField(new Block(EnumBlockType.Space), coordsToDelete[0], coordsToDelete[1]);
            }
            else
            {
                Score += 1;
                _field.containFood = false;
            }
        }
    }
}
