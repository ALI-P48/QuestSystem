public class AddCashEvent : GameEvent
{
    public int Amount;

    public AddCashEvent(int amount)
    {
        Amount = amount;
    }
}