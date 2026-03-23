using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public interface IActorMove
    {
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
    }

    public class Actor
    {
        protected (int row, int col) position;
        public int Row
        { 
            get { return position.row; } 
            set { position.row = value; }
        }

        public int Col
        {
            get { return position.col; }
            set { position.col = value; }
        }

        public (int row, int col) Position
        {
            get { return position; }
            set { position = value; }
        }

        protected int health;
        public int Health
        {
            get { return health; }
        }

        public char Mark { get; set; }
    }
}
