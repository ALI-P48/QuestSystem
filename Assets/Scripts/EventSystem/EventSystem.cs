using System;

public static class EventSystem
{
    public static Action<GameEvent> OnGameEvent;

    public static void Raise(GameEvent gameEvent)
    {
        OnGameEvent?.Invoke(gameEvent);
    }
}