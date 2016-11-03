using System;
using System.Drawing;
using System.IO;

namespace RaceGame2.Lib
{
    public class Upgrade
    {
        public string imageLocation;
        private Image imageBitmap;
        public Image image;
        private Point position;
        public bool Visible = true;

        public Upgrade()
        {

        }


        public void setImage(String upgradeName)
        {
            imageLocation = (upgradeName);
            imageLocation = ("assets\\upgrades\\" + imageLocation);
            imageLocation = Path.Combine(Environment.CurrentDirectory, imageLocation);
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(imageBitmap.Width / 2, imageBitmap.Height / 2);
            imageBitmap = new Bitmap(imageBitmap, imageSize);
            this.image = imageBitmap;
        }

        public Image GetImage()
        {
            return image;
        }

        public void setPosition(Point newPosition)
        {
            position = newPosition;
        }

        public Point getPosition()
        {
            return position;
        }
    }
}
 