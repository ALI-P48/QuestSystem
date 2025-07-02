public class QuestUpdatedEvent : GameEvent
{
    public string ID;
    public QuestStatus Status;

    public QuestUpdatedEvent(string id, QuestStatus status)
    {
        ID = id;
        Status = status;
    }
}