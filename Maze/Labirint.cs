using System;
using System.Drawing;
using System.Windows.Forms;

namespace Maze
{
    class Labirint
    {
        // позиция главного персонажа
        public int CharacterPositionX { get; set; }
        public int CharacterPositionY { get; set; }

        int height; // высота лабиринта (количество строк)
        int width; // ширина лабиринта (количество столбцов в каждой строке)

        public MazeObject[,] objects;

        public PictureBox[,] images;

        public static Random r = new Random();

        public Form parent;

        public Labirint(Form parent, int width, int height)
        {
            this.width = width;
            this.height = height;
            this.parent = parent;

            objects = new MazeObject[height, width];
            images = new PictureBox[height, width];

            CharacterPositionX = 0;
            CharacterPositionY = 2;

            Generate();
        }
        public int moneyAmount = 0;
        void Generate()
        {


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    MazeObject.MazeObjectType current = MazeObject.MazeObjectType.HALL;
                    // стены по периметру обязательны
                    if (y == 0 || x == 0 || y == height - 1 | x == width - 1)
                    {

                        current = MazeObject.MazeObjectType.WALL;

                    }
                    // в 1 случае из 250 - кладём денежку
                    else if (r.Next(200) == 0)
                    {
                        current = MazeObject.MazeObjectType.MEDAL;
                        moneyAmount++;
                    }
                    else if (r.Next(200) == 0)
                    {
                        current = MazeObject.MazeObjectType.DRUGS;
                        

                    }
                    // в 1 случае из 250 - размещаем врага
                    else if (r.Next(200) == 0)
                    {
                        current = MazeObject.MazeObjectType.ENEMY;
                    }
                    // в 1 случае из 5 - ставим стену
                    else if (r.Next(5) == 0)
                    {
                        current = MazeObject.MazeObjectType.WALL;
                    }






                    // наш персонажик
                    if (x == CharacterPositionX && y == CharacterPositionY)
                    {
                        current = MazeObject.MazeObjectType.CHAR;
                    }

                    // есть выход, и соседняя ячейка справа всегда свободна
                    if (x == CharacterPositionX + 1 && y == CharacterPositionY || x == width - 1 && y == height - 3)
                    {
                        current = MazeObject.MazeObjectType.HALL;
                    }

                    objects[y, x] = new MazeObject(current);
                    images[y, x] = new PictureBox();
                    images[y, x].Location = new Point(x * objects[y, x].width, y * objects[y, x].height);
                    images[y, x].Parent = parent;
                    images[y, x].Width = objects[y, x].width;
                    images[y, x].Height = objects[y, x].height;
                    images[y, x].BackgroundImage = objects[y, x].texture;
                }
            }
        }
    }
}
