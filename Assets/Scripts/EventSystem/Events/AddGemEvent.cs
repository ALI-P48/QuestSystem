public class AddGemEvent : GameEvent
{
    public int Amount;

    public AddGemEvent(int amount)
    {
        this.Amount = amount;
    }
}