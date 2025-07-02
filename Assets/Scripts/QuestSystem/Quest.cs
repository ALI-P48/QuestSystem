using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quest")]
public class Quest : ScriptableObject
{
    public string ID;
    public string Description;
    public Sprite Icon;
    public List<ObjectiveBase> Objectives = new();
    
    public QuestStatus Status
    {
        get => _status;
        protected set
        {
            if (_status != value)
            {
                _status = value;
                EventSystem.Raise(new QuestUpdatedEvent(ID, _status));
            }
        }
    }

    private QuestStatus _status;

    public void Initialize()
    {
        _status  = UserManager.Instance.GetQuestStatus(ID);
        for (int i = 0; i < Objectives.Count; i++)
        {
            Objectives[i].Initialize();
            Objectives[i].Evaluate();
        }
    }

    public void Evaluate() 
    {
        if (Objectives.All(o => o.IsCompleted))
        {
            Status = QuestStatus.Completed;
        }
        else
        {
            Status = QuestStatus.InProgress;
        }
    }

    public void HandleEvent(GameEvent gameEvent)
    {
        foreach (var obj in Objectives)
        {
            obj.HandleEventAndEvaluate(gameEvent);
        }

        if (gameEvent is ObjectiveCompletedEvent)
        {
            Evaluate();
        }

        if (gameEvent is ResetUserEvent)
        {
            Evaluate();
        }
    }

    public int GetCompletedObjectivesCount()
    {
        return Objectives.Where(o => o.IsCompleted).Count();
    }

    public int GetObjectivesCount()
    {
        return Objectives.Count;
    }
}