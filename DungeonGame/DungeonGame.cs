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

            if (stageManager == null)
            {
                stageManager = new StageManager();
            }

            stageManager.InitStages();
            stageManager.InitPlayer();

            GameLoop();
        }

        void TurnBaseGameLoop()
        {
            stageManager.NextStage();

            while (!stageManager.FinishFlag)
            {
                Console.SetCursorPosition(0, 0);
                stageManager.PrintCurrentMap();

                Console.WriteLine("방향을 입력해주세요. (W:위, A:왼쪽, S:아래, D:오른쪽)");

                string cmd = Console.ReadLine();
                if(string.IsNullOrEmpty(cmd))
                {
                    continue;
                }
                else
                {
                    switch (cmd.ToUpper()[0])
                    {
                        case Constants.Up:
                            stageManager.Player.MoveUp();
                            break;
                        case Constants.Left:
                            stageManager.Player.MoveLeft();
                            break;
                        case Constants.Down:
                            stageManager.Player.MoveDown();
                            break;
                        case Constants.Right:
                            stageManager.Player.MoveRight();
                            break;
                        default:
                            continue;
                    }
                }

                stageManager.Update();
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
                            stageManager.Player.MoveUp();
                            break;
                        case ConsoleKey.DownArrow:
                            stageManager.Player.MoveDown();
                            break;
                        case ConsoleKey.LeftArrow:
                            stageManager.Player.MoveLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            stageManager.Player.MoveRight();
                            break;
                        case ConsoleKey.Escape:
                            return; // 게임 종료
                    }
                }

                stageManager.Update();
            }
        }
    }
}
