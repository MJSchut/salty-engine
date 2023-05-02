using Microsoft.Xna.Framework;

namespace salty.core.Messages
{
    public class PlaceItemMessage
    {
        public PlaceItemMessage(string itemPlaced, Vector2 location)
        {
            ItemPlaced = itemPlaced;
            ItemLocation = location;
        }

        public string ItemPlaced { get; }
        public Vector2 ItemLocation { get; }
    }
}