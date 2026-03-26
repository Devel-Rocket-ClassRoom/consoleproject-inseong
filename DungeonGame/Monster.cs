using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
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
            Encounter,
            Patrol,
            Battle,
            Chase,
            Runaway,
            Died
        }

        public MonsterState State { private get; set; }
        int moveTurnCount;
        int battleTurnCount;
        int nextR;
        int nextC;

        public Map CurrentMap { private get; set; }

        public int MoveTurns { get; private set; }
        public int BattleTurns { get; private set; }
        public int BattleRange { get; private set; }

        public Monster()
        {
            Mark = Constants.MonsterMark;
            State = MonsterState.Idle;

            Random rand = new Random((int)DateTime.Now.Ticks);
            MoveTurns = rand.Next(1, Constants.MonsterMaxMoveTurns);
            BattleTurns = rand.Next(1, Constants.MonsterMaxBattleTurns);
            BattleRange = rand.Next(1, Constants.MonsterMaxBattleRange);
        }
        
        public Monster(int r, int c, int moveTurns = 0, int battleTurns = 0, int range = 0)
        {
            Mark = Constants.MonsterMark;
            State = MonsterState.Idle;
            Row = r;
            Col = c;
            
            BattleRange = range;

            Random rand = new Random((int)DateTime.Now.Ticks);

            if (moveTurns == 0)
            {
                MoveTurns = rand.Next(1, Constants.MonsterMaxMoveTurns);
            }
            else
            {
                BattleTurns = moveTurns;
            }

            if (battleTurns == 0)
            {
                BattleTurns = rand.Next(1, Constants.MonsterMaxBattleTurns);
            }
            else
            {
                BattleTurns = battleTurns;
            }

            if (range == 0)
            {
                BattleRange = rand.Next(1, Constants.MonsterMaxBattleRange);
            }
            else
            {
                BattleRange = range;
            }
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
            
            switch (State)
            {
                case MonsterState.Idle:
                    break;

                // Encounter 상태일 경우 Battle 상태 돌입, Turn 수 체크
                case MonsterState.Encounter:
                    break;

                case MonsterState.Patrol:
                    break;

                case MonsterState.Battle:
                    break;

                default:
                    break;
            }
            // Turn 종료일 경우, Range 안에 Player가 있을 경우 Battle 유지, 없을 경우 idle 또는 Patrol 상태일지 체크
            // Turn 수 만큼 대기

            // Patrol 일 경우 이동
            // Idle 상태일 때는 턴 수만 증가
        }
    }
}
