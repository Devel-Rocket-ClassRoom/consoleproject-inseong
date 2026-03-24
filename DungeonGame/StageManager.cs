using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame
{
    public class StageManager
    {
        private List<Stage> stageList;
        private Player player;
        public Player Player
        {
            get { return player; }
        }

        private Stage currentStage;

        int totalStageCount = 0;
        int currentStageIndex = -1;
        public bool FinishFlag { get; set; } = false;

        public void Start()
        {
            InitStages();
            InitPlayer();
            PlayNextStage();
        }

        public void InitStages(int stageCount = 0)
        {
            if (stageCount <= 0)
            {
                stageCount = Constants.DefaultStageCount;
            }

            totalStageCount = stageCount;

            stageList = new List<Stage>();

            for (int i = 0; i < totalStageCount; i++)
            {
                AddAndBuildStage();
            }

            //currentStageIndex = 0;
            //currentStage = stageList[currentStageIndex];
        }

        public void InitPlayer()
        {
            player = new Player();
            currentStage.SetPlayer(player);
        }

        public void NextStage()
        {
            if(currentStage != null)
            {
                currentStage.RemovePlayer();
            }

            currentStageIndex++;
            if(currentStageIndex < stageList.Count)
            {
                currentStage = stageList[currentStageIndex];
                currentStage.InitStage(player);
                currentStage.NextMap();
            }
            else
            {
                FinishFlag = true;
            }
        }

        public void AddAndBuildStage()
        {
            Stage stage = new Stage();
            if (stage != null)
            {
                stageList.Add(stage);
                stage.BuildStage();
            }
        }

        Stage NextStage()
        {
            currentStageIndex++;
            if (currentStageIndex < stageList.Count)
            {
                return stageList[currentStageIndex];
            }

            return null;
        }

        Stage GetStage(int index)
        {
            if(index < stageList.Count)
            {
                return stageList[index];
            }
            return null;
        }

        public void PrintCurrentMap()
        {
            if(currentStage != null)
            {
                currentStage.PrintCurrentMap();
            }
        }

        public void Update()
        {
            if (currentStage != null)
            {
                currentStage.UpdatePlayer();
                currentStage.UpdateMonsters();
            }
        }
    }
}
