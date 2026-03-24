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
        //private Player currentPlayer;
        private (int row, int col) startPosition;
        private (int row, int col) lastPosition;
        int rowLength;
        int colLength;

        public Map()
        {
        }

        public Map(int rowSize, int colSize)
        {
            BuildMap(rowSize, colSize);
        }

        //public void PlacePlayer(Player player)
        //{
        //    currentPlayer = player;
        //    currentPlayer.Position = startPosition;
        //}

        //public void RemovePlayer()
        //{
        //    if(currentPlayer != null)
        //    {
        //        lastPosition = currentPlayer.Position;
        //    }

        //    currentPlayer = null;
        //}

        public void UpdateMonsters()
        {
            foreach(Monster m in monsterList.Values)
            {
                m.Update();
            }
        }

        void RandomMapSize(out int rowSize, out int colSize)
        {
            int seed = (int)DateTime.Now.Ticks;
            Random rand = new Random(seed);

            rowSize = rand.Next(Constants.MinMapLength, Constants.MaxMapRowLength);
            colSize = rand.Next(Constants.MinMapLength, Constants.MaxMapColLength);
        }

        public void BuildMap(int rowSize = 0, int colSize = 0)
        {
            if (rowSize <= 0 || colSize <= 0)
            {
                RandomMapSize(out rowSize, out colSize);
            }

            rowLength = rowSize;
            colLength = colSize;

            mapData = new MapTile[rowLength, colLength];

            ClearMap();
            BuildWalls();

            startPosition = GetRandomStartPosition();

            CreateRandomMonsters();
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

        void BuildWalls(int count = 0)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

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

            var floors = GetAllEmptyFloorPosition();

            if(floors.Count > 0)
            {
                // 내부의 벽, 기둥을 무작위로 생성
                if (count <= 0)
                {
                    //지정된 값이 없을 경우 최대 전체 빈 바닥타일 개수의 최대 20%를 벽으로 지정
                    count = rand.Next(0, (int)(floors.Count * 0.2f));
                }

                // Player가 배치될 위치를 제외한 바닥 수 보다 생성될 벽의 수가 클 경우
                if (count > floors.Count - 1)
                {
                    count = floors.Count - 1;
                }

                for (int i = 0; i < count; i++)
                {
                    int p = rand.Next(0, floors.Count - 1);

                    int r = floors[p].r;
                    int c = floors[p].c;

                    mapData[r, c] = new WallTile();

                    // 사용한 값은 삭제
                    floors.RemoveAt(p);
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
                    if (mapData[r, c] is FloorTile)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        (int r, int c) GetRandomStartPosition()
        {
            (int r, int c) pos = (-1, -1);

            if (mapData != null)
            {
                var floors = GetAllEmptyFloorPosition();

                Random rand = new Random((int)DateTime.Now.Ticks);

                int index = rand.Next(0, floors.Count - 1);
                pos = floors[index];
            }

            return pos;
        }

        List<(int r, int c)> GetAllEmptyFloorPosition()
        {
            List<(int r, int c)> floors = new List<(int r, int c)>();

            for (int r = 0; r < rowLength; r++)
            {
                for (int c = 0; c < colLength; c++)
                {
                    if (IsFloor(r, c))
                    {
                        floors.Add((r, c));
                    }
                }
            }
            return floors;
        }

        void CreateRandomMonsters(int count = 0)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            var floors = GetAllEmptyFloorPosition();

            if (count <= 0)
            {
                count = rand.Next(0, (int)(floors.Count * 0.2f));
            }

            if(count > floors.Count -1)
            {
                count = floors.Count - 1;
            }

            for (int i = 0; i < count; i++)
            {
                int p = rand.Next(0, floors.Count - 1);

                int r = floors[p].r;
                int c = floors[p].c;

                Monster monster = new Monster(r, c);
                monsterList.Add((r,c), monster);

                // 사용한 값은 삭제
                floors.RemoveAt(p);
            }
        }

        public void Update(Player player)
        {

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

        public bool IsFloor(int r, int c)
        {
            if (mapData[r, c] is FloorTile)
            {
                return true;
            }
            return false;
        }

        //public bool IsPlayer(int r, int c)
        //{
        //    if(currentPlayer.Row == r && currentPlayer.Col == c)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public StringBuilder PrintMap(Player player)
        {
            int rsize = mapData.GetLength(0);
            int csize = mapData.GetLength(1);

            StringBuilder stringBuffer = new StringBuilder();

            for(int r = 0; r < rsize; r++)
            {
                for(int c = 0; c < csize; c++)
                {
                    if(player != null && player.Row == r && player.Col == c)
                    {
                        stringBuffer.Append(player.Mark);
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
