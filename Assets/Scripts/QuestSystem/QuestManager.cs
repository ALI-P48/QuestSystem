using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestManager : Manager
{
    public static QuestManager Instance;
    
    public List<Quest> Quests => _questDatabase.Quests;

    [SerializeField] private QuestDatabase _questDatabase;
    
    public override void CreateInstance()
    {
        Instance = this;
    }

    public override void Initialize()
    {
        foreach (var quest in _questDatabase.Quests)
        {
            quest.Initialize();
            quest.Evaluate();
        }
    }

    protected override void OnGameEventReceived(GameEvent gameEvent)
    {
        foreach (var quest in _questDatabase.Quests)
        {
            quest.HandleEvent(gameEvent);
        }
    }
}