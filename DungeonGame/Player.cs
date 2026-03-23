using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Player : Actor, IActorMove
    {
        public enum PlayerState
        {
            Idle,
            Encounter,
            Died
        }

        private PlayerState state;
        private Monster encounteredMonster;

        int nextR;
        int nextC;

        Map currentMap;
        public Map CurrentMap
        {
            set { currentMap = value; }
        }

        public Player()
        {
            Mark = Constants.PlayerMark;
            state = PlayerState.Idle;
        }

        void UpdateMove(int nextR, int nextC)
        {
            if (currentMap != null)
            {
                encounteredMonster = currentMap.TryGetMonster(nextR, nextC);
                if (encounteredMonster != null)
                {
                    state = PlayerState.Encounter;
                    Console.WriteLine("몬스터와 만났습니다..");
                }
                else if (currentMap.IsWall(nextR, nextC))
                {
                    state = PlayerState.Idle;
                    Console.WriteLine("이동할 수 없습니다.");
                }
                else
                {
                    state = PlayerState.Idle;
                    Position = (nextR, nextC);
                }
            }
        }

        public void MoveLeft()
        {
            nextR = Row;
            nextC = Col - 1;
            UpdateMove(nextR, nextC);
        }

        public void MoveRight()
        {
            nextR = Row;
            nextC = Col + 1;
            UpdateMove(nextR, nextC);
        }

        public void MoveUp()
        {
            nextR = Row - 1;
            nextC = Col;
            UpdateMove(nextR, nextC);
        }

        public void MoveDown()
        {
            nextR = Row + 1;
            nextC = Col;
            UpdateMove(nextR, nextC);
        }
    }
}
