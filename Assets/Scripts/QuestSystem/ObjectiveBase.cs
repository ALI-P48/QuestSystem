using UnityEngine;

public abstract class ObjectiveBase : ScriptableObject
{
    public string ID;
    public string Description;
    
    public bool IsCompleted
    {
        get => _isCompleted;
        protected set
        {
            _isCompleted = value;
            if (_isCompleted)
            {
                EventSystem.Raise(new ObjectiveCompletedEvent(ID));
            }
        }
    }
    
    private bool _isCompleted;

    public void Initialize()
    {
        _isCompleted = UserManager.Instance.IsObjectiveDone(ID);
        InitializeData();
    }

    protected abstract void InitializeData();

    public void HandleEventAndEvaluate(GameEvent gameEvent)
    {
        if (TryHandleEvent(gameEvent))
        {
            Evaluate();
            EventSystem.Raise(new ObjectiveUpdatedEvent(ID));
        }
    }
    
    public abstract void Evaluate();
    public abstract float GetProgressionValue();
    public abstract string GetProgressionText();
    
    protected abstract bool TryHandleEvent(GameEvent gameEvent);
}