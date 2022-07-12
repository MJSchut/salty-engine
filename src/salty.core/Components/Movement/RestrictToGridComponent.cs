namespace salty.core.Components.Movement
{
    public class RestrictToGridComponent
    {
        public readonly int GridSizeX;
        public readonly int GridSizeY;
        
        public RestrictToGridComponent(int gridSizeX = 16, int gridSizeY = 16)
        {
            GridSizeX = gridSizeX;
            GridSizeY = gridSizeY;
        }
    }
}