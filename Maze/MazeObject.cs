using System.Drawing;

namespace Maze
{
    class MazeObject
    {
        public enum MazeObjectType { HALL, WALL, MEDAL, ENEMY, CHAR,DRUGS };

        public static Bitmap[] images = { new Bitmap(Properties.Resources.hall),
            new Bitmap(Properties.Resources.wall), // wall
            new Bitmap(Properties.Resources.medal), // medal
            new Bitmap(Properties.Resources.enemy), // enemy
            new Bitmap(Properties.Resources.player), // player
            new Bitmap(Properties.Resources.drugs)}; //drugs

    public MazeObjectType type;

        public int width;
        public int height;
        public Image texture;
        public int health;
        public MazeObject(MazeObjectType type)
        {
            this.type = type;
            width = 16;
            height = 16;
            texture = images[(int)type];
            // start hp
          
               // health = 100;
            

        }

        public bool IsWalkable()
        {
            return type == MazeObjectType.HALL || type == MazeObjectType.MEDAL|| type == MazeObjectType.ENEMY|| type == MazeObjectType.DRUGS;
        }
    }
}
