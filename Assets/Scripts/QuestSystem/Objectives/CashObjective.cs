using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New CashObjective", menuName = "ScriptableObjects/Objectives/CashObjective")]
public class CashObjective : ObjectiveBase
{
    public int RequiredCash;

    private int _currentCash;

    protected override void InitializeData()
    {
        _currentCash = UserManager.Instance.GetCash();
    }

    public override void Evaluate() 
    {
        if (_currentCash >= RequiredCash)
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
        return (float)_currentCash/ RequiredCash;
    }

    public override string GetProgressionText()
    {
        return $"{Mathf.Min(_currentCash, RequiredCash)}/{RequiredCash}";
    }

    protected override bool TryHandleEvent(GameEvent gameEvent)
    {
        if (gameEvent is AddCashEvent)
        {
            _currentCash += (gameEvent as AddCashEvent).Amount;
        }
        else if (gameEvent is ResetUserEvent)
        {
            _currentCash = UserManager.Instance.GetCash();
        }
        else
        {
            return false;
        }
        return true;
    }
}