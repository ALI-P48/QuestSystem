using UnityEngine;

public abstract class BaseScreen : MonoBehaviour
{
    public abstract ScreenID ID
    {
        get;
    }

    public abstract void Initialize();

    public abstract void Show();

    public abstract void OnGameEventReceived(GameEvent gameEvent);
}
