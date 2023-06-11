using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace TetrisGameOpenGL.Snake
{
    public class Field
    {
        Block[,] matrix;
        public bool containFood = false;

        public Field(int fieldWidth, int fieldHeight)
        {
            matrix = new Block[fieldWidth, fieldHeight];
            FillMap();
        }

        public void DrawBlocks()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j].DrawBlock(i + 1, j + 1);
                    Console.Write(matrix[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
        }

        public void DrawFieldGrid()
        {
            GL.Color3(0, 0, 0);
            GL.Begin(PrimitiveType.Lines);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                GL.Vertex2(i, 0);
                GL.Vertex2(i, matrix.GetLength(1));
            }
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                GL.Vertex2(0, i);
                GL.Vertex2(matrix.GetLength(0), i);
            }
            GL.End();
        }

        public void FillMap()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = new Block(EnumBlockType.Space);
                }
            }
            

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, 0] = new Block(EnumBlockType.Border);
                matrix[i, matrix.GetLength(1) - 1] = new Block(EnumBlockType.Border);
            }
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[0, i] = new Block(EnumBlockType.Border);
                matrix[matrix.GetLength(0) - 1, i] = new Block(EnumBlockType.Border);
            }
            
        }

        public void setToField(Block _block, int _x, int _y)
        {
            matrix[_x, _y] = _block;
        }

        public Block getBlockByCoords(int _x, int _y) => matrix[_x, _y];

        public void generateFood()
        {
            List<int[]> freeSpaceCoordList = new List<int[]>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].type == EnumBlockType.Space) freeSpaceCoordList.Add(new int[2]{ i, j });
                }
            }
            Random rand = new Random();
            int genNum = rand.Next(0, freeSpaceCoordList.Count);
            int[] newFoodCoords = freeSpaceCoordList[genNum];
            matrix[newFoodCoords[0], newFoodCoords[1]] = new Block(EnumBlockType.Food);
            containFood = true;
        }
    }
}
