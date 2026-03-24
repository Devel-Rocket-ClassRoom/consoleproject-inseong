using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public static class Constants
    {
        public const char PlayerMark = 'P';
        public const char MonsterMark = 'M';
        public const char FloorMark = ' ';
        public const char WallMark = '#';
        public const char DoorMark = 'D';
        public const char Up = 'W';
        public const char Down = 'S';
        public const char Left = 'A';
        public const char Right = 'D';

        public const int DefaultStageCount = 1;
        public const int DefaultMapCount = 1;
        public const int MinMapLength = 10;
        public const int MaxMapRowLength = 24;
        public const int MaxMapColLength = 40;
        public const int TargetFPS = 60;
        public const double FrameTime = 1000.0 / TargetFPS;
    }
}
