namespace salty.core.Messages
{
    public class AddMoneyMessage
    {
        public int Value { get; }
        public AddMoneyMessage(int value)
        {
            Value = value;
        }
    }
}