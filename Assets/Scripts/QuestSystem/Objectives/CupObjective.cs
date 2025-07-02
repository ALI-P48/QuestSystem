using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New CupObjective", menuName = "ScriptableObjects/Objectives/CupObjective")]
public class CupObjective : ObjectiveBase
{
    public int RequiredCups;

    private int _currentCups;

    protected override void InitializeData()
    {
        _currentCups = UserManager.Instance.GetCups();
    }

    public override void Evaluate() 
    {
        if (_currentCups >= RequiredCups)
        {
            IsCompleted = true;
        }
        else
        {
            IsCompleted = false;
        }
    }

    public override float GetProgressionValue()
    {
        return (float)_currentCups/ RequiredCups;
    }

    public override string GetProgressionText()
    {
        return $"{Mathf.Min(_currentCups, RequiredCups)}/{RequiredCups}";
    }

    protected override bool TryHandleEvent(GameEvent gameEvent)
    {
        if (gameEvent is AddCupEvent)
        {
            _currentCups += (gameEvent as AddCupEvent).Amount;
        }
        else if (gameEvent is ResetUserEvent)
        {
            _currentCups = UserManager.Instance.GetCups();
        }
        else
        {
            return false;
        }
        return true;
    }
}