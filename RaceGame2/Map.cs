namespace RaceGame2
{
    public class Map
    {
        public int fuelCostModifier;
        private int startY;
        private int startX;
        private int laps;
        private bool lappable;
        private int checkpoints;

        public int GetStartY()
        {
            return startY;
        }

        public void SetStartY(int y)
        {
            startY = y;
        }
    }
}