using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Map
    {
        private MapTile[,] mapData;
        private Dictionary<(int, int), Monster> monsterList;
        private Player currentPlayer;

        int rowLength;
        int colLength;

        public Map()
        {
        }

        public Map(int rowSize, int colSize)
        {
            BuildMap(rowSize, colSize);
        }

        public void SetPlayer(Player p)
        {
            currentPlayer = p;
        }

        public void RemovePlayer()
        {
            currentPlayer = null;
        }

        public void UpdateMonsters()
        {
            foreach(Monster m in monsterList.Values)
            {
                m.Update();
            }
        }

        public void BuildMap(int rowSize = 0, int colSize = 0)
        {
            int seed = (int)DateTime.Now.Ticks;
            Random rand = new Random(seed);

            if (rowSize <= 0)
            {
                rowSize = rand.Next(Constants.MinMapLength, Constants.MaxMapRowLength);
            }

            if (colSize <= 0)
            {
                colSize = rand.Next(Constants.MinMapLength, Constants.MaxMapColLength);
            }

            rowLength = rowSize;
            colLength = colSize;

            mapData = new MapTile[rowLength, colLength];

            ClearMap();
            BuildWalls();
        }

        void ClearMap()
        {
            // 맵 초기화
            for (int r = 0; r < rowLength; r++)
            {
                for (int c = 0; c < colLength; c++)
                {
                    mapData[r, c] = new FloorTile();
                }
            }
        }

        int CountFloors()
        {
            int count = 0;
            for (int r = 0; r < rowLength; r++)
            {
                for (int c = 0; c < colLength; c++)
                {
                    if(mapData[r, c] is FloorTile)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        void BuildWalls(int count = 0)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            if (rowLength < Constants.MinMapLength)
                rowLength = Constants.MinMapLength;

            if (colLength < Constants.MinMapLength)
                colLength = Constants.MinMapLength;

            // 외곽을 벽으로 채움
            for (int r = 0; r < rowLength; r++)
            {
                mapData[r, 0] = new WallTile();
                mapData[r, colLength - 1] = new WallTile();
            }

            for (int c = 1; c < colLength - 1; c++)
            {
                mapData[0, c] = new MapTile();
                mapData[rowLength - 1, c] = new WallTile();
            }

            int floorCount = CountFloors();

            if(floorCount > 0)
            {
                // 내부의 벽, 기둥을 무작위로 생성
                if (count <= 0)
                {
                    //최대 전체 타일맵 크기의 15% 와 바닥 타일 수/2 중 작은 값을 벽 개수로 지정
                    count = rand.Next(0, (int)((rowLength * colLength) * 0.15f));
                }

                // Player가 배치될 위치를 제외한 바닥 수 보다 생성될 벽의 수가 클 경우
                if (count > floorCount - 1)
                {
                    count = floorCount - 1;
                }

                for (int i = 0; i < count;)
                {
                    int r = rand.Next(0, rowLength - 1);
                    int c = rand.Next(0, colLength - 1);

                    // 바닥일 경우에만 벽으로 대체
                    if (mapData[r, c] is FloorTile)
                    {
                        mapData[r, c] = new WallTile();
                        i++;
                    }
                }
            }
        }

        public bool IsWall(int r, int c)
        {
            if (mapData[r, c] is WallTile)
            {
                return true;
            }
            return false;
        }

        public Monster TryGetMonster(int r, int c)
        {
            if (monsterList.TryGetValue((r, c), out Monster monster))
            {
                return monster;
            }
            return null;
        }

        public bool IsMonster(int r, int c)
        {
            if (monsterList.TryGetValue((r, c), out Monster monster))
            {
                return true;
            }
            return false;
        }

        public bool IsSpace(int r, int c)
        {
            if (mapData[r, c] is FloorTile)
            {
                return true;
            }
            return false;
        }


        public StringBuilder PrintMap()
        {
            int rsize = mapData.GetLength(0);
            int csize = mapData.GetLength(1);

            StringBuilder stringBuffer = new StringBuilder();

            for(int r = 0; r < rsize; r++)
            {
                for(int c = 0; c < csize; c++)
                {
                    if(currentPlayer != null && currentPlayer.Row == r && currentPlayer.Col == c)
                    {
                        stringBuffer.Append(currentPlayer.Mark);
                    }
                    else if(monsterList.TryGetValue((r,c), out Monster mon))
                    {
                        stringBuffer.Append(mon.Mark);
                    }
                    else
                    {
                        stringBuffer.Append(mapData[r, c].Mark);
                    }
                }
                stringBuffer.AppendLine();
            }

            return stringBuffer;
        }
    }
}
