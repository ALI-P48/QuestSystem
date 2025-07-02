using System.Collections.Generic;

[System.Serializable]
public class UserData
{
    public int Level;
    public int Cash;
    public int Gems;
    public int Kills;
    public int Cups;
    public Dictionary<string, QuestStatus> QuestsStatus;
    public List<string> ObjectivesDone;

    public UserData()
    {
        Level = 1;
        Cash = 0;
        Gems = 0;
        Kills = 0;
        Cups = 0;
        QuestsStatus = new Dictionary<string, QuestStatus>();
        ObjectivesDone = new List<string>();
    }
}
