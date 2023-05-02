namespace salty.core.Messages
{
    public class AddMoneyMessage
    {
        public AddMoneyMessage(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}