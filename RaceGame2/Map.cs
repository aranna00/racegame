using System;
using System.Drawing;
using System.Windows.Forms;

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
        public Image image;
        private Point position;

        public Map(int postionx, int positiony, Image image)
        {
            position.X = postionx;
            position.Y = positiony;
            this.image = image;
        }

        public int GetStartY()
        {
            return startY;
        }

        public void SetStartY(int y)
        {
            startY = y;
        }

        public Image getImage()
        {
            return image;
        }
    }
}