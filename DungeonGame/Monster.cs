using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonGame.Player;

namespace DungeonGame
{
    public class Monster : Actor, IActorMove
    {
        public enum MonsterState
        {
            Idle,
            Patrol,
            Battle,
            Chase,
            Runaway,
            Died
        }

        public MonsterState State { get; set; }
        int nextR;
        int nextC;

        private Map currentMap;
        public Map CurrentMap
        {
            set { currentMap = value; }
        }

        public Monster()
        {
            Mark = Constants.MonsterMark;
            State = MonsterState.Idle;
        }
        
        public Monster(int r, int c)
        {
            Mark = Constants.MonsterMark;
            State = MonsterState.Idle;
            Row = r;
            Col = c;
        }

        public void MoveLeft()
        {

        }

        public void MoveRight()
        {

        }

        public void MoveUp()
        {

        }

        public void MoveDown()
        {

        }

        public void Update()
        {

        }
    }
}
