using UnityEngine;

[CreateAssetMenu(fileName = "New LevelObjective", menuName = "ScriptableObjects/Objectives/LevelObjective")]
public class LevelObjective : ObjectiveBase
{
    public int RequiredLevel;

    private int _currentLevel;

    protected override void InitializeData()
    {
        _currentLevel = UserManager.Instance.GetLevel();
    }

    public override void Evaluate() 
    {
        if (_currentLevel >= RequiredLevel)
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
        return (float)_currentLevel/ RequiredLevel;
    }

    public override string GetProgressionText()
    {
        return $"{Mathf.Min(_currentLevel, RequiredLevel)}/{RequiredLevel}";
    }

    protected override bool TryHandleEvent(GameEvent gameEvent)
    {
        if (gameEvent is LevelPassedEvent)
        {
            _currentLevel++;
        }
        else if (gameEvent is ResetUserEvent)
        {
            _currentLevel = UserManager.Instance.GetLevel();
        }
        else
        {
            return false;
        }
        return true;
    }
}