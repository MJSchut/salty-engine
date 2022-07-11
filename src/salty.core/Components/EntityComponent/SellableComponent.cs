namespace salty.core.Components.EntityComponent
{
    public class SellableComponent
    {
        /// <summary>
        /// Value when this item is sold.
        /// </summary>
        public readonly int Value;

        public SellableComponent(int value)
        {
            Value = value;
        }
    }
}