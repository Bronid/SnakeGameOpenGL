using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace TetrisGameOpenGL.Snake
{
    public readonly struct Block
    {
        public EnumBlockType type { get; }
        public float[] CustomColor { get; } = null;
        public Block(EnumBlockType _type, float[] customColor = null)
        {
            type = _type;
            if (customColor != null) { CustomColor = customColor; }
        }

        public override string ToString()
        {
            switch (type)
            {
                case EnumBlockType.Space: return "0";
                case EnumBlockType.Tail: return "-";
                case EnumBlockType.Border: return "*";
                case EnumBlockType.Head: return "H";
                case EnumBlockType.Food: return "&";
            }

            return "unknown";
        }

        public void DrawBlock(int x, int y)
        {
            if (CustomColor == null)
            {
                switch (type)
                {
                    case EnumBlockType.Border:
                        GL.Color3(0, 0, 0);
                        break;
                    case EnumBlockType.Tail:
                        GL.Color3(0, 1.0F, 0);
                        break;
                    case EnumBlockType.Head:
                        GL.Color3(0, 1.0F, 1.0F);
                        break;
                    case EnumBlockType.Food:
                        GL.Color3(1.0F, 0, 0);
                        break;
                    case EnumBlockType.Space:
                        GL.Color3(1.0F, 1.0F, 1.0F);
                        break;
                }
            }
            else
            {
                GL.Color3(CustomColor[0], CustomColor[1], CustomColor[2]);
            }
            GL.Rect(x, y, x - 1, y - 1);
        }
    }
}
