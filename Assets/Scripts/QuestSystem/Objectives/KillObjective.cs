using UnityEngine;

[CreateAssetMenu(fileName = "New KillObjective", menuName = "ScriptableObjects/Objectives/KillObjective")]
public class KillObjective : ObjectiveBase
{
    public int RequiredKills;

    private int _currentKills;

    protected override void InitializeData()
    {
        _currentKills = UserManager.Instance.GetKills();
    }

    public override void Evaluate() 
    {
        if (_currentKills >= RequiredKills)
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
        return (float)_currentKills/ RequiredKills;
    }

    public override string GetProgressionText()
    {
        return $"{Mathf.Min(_currentKills, RequiredKills)}/{RequiredKills}";
    }

    protected override bool TryHandleEvent(GameEvent gameEvent)
    {
        if (gameEvent is EnemyKilledEvent)
        {
            _currentKills++;
        }
        else if (gameEvent is ResetUserEvent)
        {
            _currentKills = UserManager.Instance.GetKills();
        }
        else
        {
            return false;
        }
        return true;
    }
}