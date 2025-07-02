public class ObjectiveCompletedEvent : GameEvent
{
    public string ID;

    public ObjectiveCompletedEvent(string ID)
    {
        this.ID = ID;
    }
}