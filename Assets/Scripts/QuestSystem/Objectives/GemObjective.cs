using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New GemObjective", menuName = "ScriptableObjects/Objectives/GemObjective")]
public class GemObjective : ObjectiveBase
{
    public int RequiredGems;

    private int _currentGems;

    protected override void InitializeData()
    {
        _currentGems = UserManager.Instance.GetGems();
    }

    public override void Evaluate() 
    {
        if (_currentGems >= RequiredGems)
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
        return (float)_currentGems/ RequiredGems;
    }

    public override string GetProgressionText()
    {
        return $"{Mathf.Min(_currentGems, RequiredGems)}/{RequiredGems}";
    }

    protected override bool TryHandleEvent(GameEvent gameEvent)
    {
        if (gameEvent is AddGemEvent)
        {
            _currentGems += (gameEvent as AddGemEvent).Amount;
        }
        else if (gameEvent is ResetUserEvent)
        {
            _currentGems = UserManager.Instance.GetGems();
        }
        else
        {
            return false;
        }
        return true;
    }
}