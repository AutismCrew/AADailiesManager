using System;
using System.Drawing;

namespace ArcheAge_Dailies_Manager
{
    public class Quest
    {
        public string description = "";
        public string name = "";
        public Image location = null;
        public QUEST_STATUS questStatus;
        public long handInTime = 0L;
        public Quest(string fileContents, Image location)
        {
            this.name = fileContents.Substring(0, fileContents.IndexOf(Environment.NewLine));
            this.description = fileContents;
            this.location = location;
            this.questStatus = QUEST_STATUS.NotReceived;
        }
        public override string ToString()
        {
            return name;
        }

        public QUEST_STATUS getQuestStatus()
        {
            return questStatus;
        }

        public void completeQuest()
        {
            this.questStatus = QUEST_STATUS.Completed;
            this.handInTime = DateTime.UtcNow.Millisecond;
        }
        public void increaseProgress()
        {
            switch (this.questStatus)
            {
                case QUEST_STATUS.NotReceived:
                    this.questStatus = QUEST_STATUS.InProgress;
                    break;
                case QUEST_STATUS.InProgress:
                    this.questStatus = QUEST_STATUS.Completed;
                    handInTime = DateTime.UtcNow.Millisecond;
                    break;
                default:
                    this.questStatus = QUEST_STATUS.NotReceived;
                    break;
            }
        }
    }
}