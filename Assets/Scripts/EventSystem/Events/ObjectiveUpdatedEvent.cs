public class ObjectiveUpdatedEvent : GameEvent
{
    public string ID;

    public ObjectiveUpdatedEvent(string ID)
    {
        this.ID = ID;
    }
}