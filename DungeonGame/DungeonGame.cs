using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class DungeonGame
    {
        StageManager stageManager;
        delegate void PlayGameLoop();

        bool realtimeGame = false;

        public DungeonGame()
        {
        }

        public void StartGame()
        {
            PlayGameLoop GameLoop = TurnBaseGameLoop;

            if (realtimeGame)
                GameLoop = RealTimeGameLoop;

            InitGame();
            GameLoop();
        }

        public void InitGame()
        {
            if(stageManager == null)
            {
                stageManager = new StageManager();
            }

            stageManager.InitStages();
        }

        void TurnBaseGameLoop()
        {
            bool finishFlag = false;
            while (!finishFlag)
            {
                Console.SetCursorPosition(0, 0);
                stageManager.PrintCurrentMap();

                Console.WriteLine("방향을 입력해주세요. (W:위, A:왼쪽, S:아래, D:오른쪽)");
            }
        }

        void RealTimeGameLoop()
        {
            bool finishFlag = false;
            while (!finishFlag)
            {
                Console.SetCursorPosition(0, 0);
                stageManager.PrintCurrentMap();

                // 입력 처리
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            player.MoveUp(stageManager);
                            break;
                        case ConsoleKey.DownArrow:
                            player.MoveDown(stageManager);
                            break;
                        case ConsoleKey.LeftArrow:
                            player.MoveLeft(stageManager);
                            break;
                        case ConsoleKey.RightArrow:
                            player.MoveRight(stageManager);
                            break;
                        case ConsoleKey.Escape:
                            return; // 게임 종료
                    }
                }
            }
        }
    }
}
