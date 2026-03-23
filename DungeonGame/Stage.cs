using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class Stage
    {
        List<Map> mapList;
        int currentMap;
        Player currentPlayer;

        public Stage()
        {
            mapList = new List<Map>();
            currentMap = 0;
        }

        public void SetPlayer(Player p)
        {
            currentPlayer = p;
        }

        public void RemovePlayer()
        {
            currentPlayer = null;
        }

        public void BuildStage(int mapCount = 0)
        {
            if(mapCount <= 0)
            {
                mapCount = Constants.DefaultMapCount;
            }

            for(int i = 0; i < mapCount; i++)
            {
                Map m = new Map();
                mapList.Add(m);

                // 임의의 크기로 맵 생성
                m.BuildMap();
            }
        }

        public void UpdateMonsters()
        {
            foreach(Map m in mapList)
            {
                m.UpdateMonsters();
            }
        }

        public void PrintCurrentMap()
        {
            if(currentMap >= 0 && currentMap < mapList.Count)
            {
                StringBuilder stringBuffer = mapList[currentMap].PrintMap();

            }
        }
    }
}
