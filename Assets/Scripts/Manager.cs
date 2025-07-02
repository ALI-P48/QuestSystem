using UnityEngine;

public abstract class Manager : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.OnGameEvent += OnGameEventReceived;
    }

    private void OnDisable()
    {
        EventSystem.OnGameEvent -= OnGameEventReceived;
    }
    
    public abstract void CreateInstance();

    public abstract void Initialize();

    protected abstract void OnGameEventReceived(GameEvent gameEvent);
}
