using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RaceGame2.Lib
{
    public class Map
    {
        public int fuelCostModifier = 1;
        public List<Point> startingLine = new List<Point>();
        private int startX;
        public int laps;
        private bool lappable;
        public List<KeyValuePair<int, List<Point>>> checkpoints = new List<KeyValuePair<int, List<Point>>>();
        public List<Point> respawn = new List<Point>();
        public string imageLocation;
        public Image image;
        private static Bitmap mapImage;
        public List<Point> pitstop = new List<Point>();
        public List<Car> cars;

        public void setCarsStartingPoints()
        {
            cars[0].setPosistion(startingLine[0]);
            cars[1].setPosistion(startingLine[1]);
        }


        public void setImage()
        {
            imageLocation = ("assets\\maps\\" + this.imageLocation);
            imageLocation = Path.Combine(System.Environment.CurrentDirectory, imageLocation);
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(1007, 728);
            imageBitmap = new Bitmap(imageBitmap, imageSize);
            this.image = imageBitmap;
            mapImage = (Bitmap)image;
        }

        public void Position()
        {
            cars[0].position = new Point(startingLine[0].X, startingLine[0].Y);
            cars[1].position = new Point(startingLine[1].X, startingLine[1].Y);
            cars[0].rotation= (float) Math.PI;
            cars[1].rotation = (float) Math.PI;
        }

        public Image getImage()
        {
            return this.image;
        }

        public static Boolean onTrack(int x, int y)
        {
            if (mapImage == null)
            {
                return false;
            }
            if (x > 0 && y > 0 && x < mapImage.Width && y < mapImage.Height)
            {
                Color color = mapImage.GetPixel(x, y);
                if (color.R > 150)
                {
                    return true;
                }
                return false;

            }
            return false;
            return true;
        }

        public void checkpointChecker()
        {
            foreach (Car car in cars)
            {
                Point pos = car.getPosition();
                foreach (var checkpointList in checkpoints)
                {
                    if ((pos.X >= checkpointList.Value[0].X && pos.X <= checkpointList.Value[1].X) &&
                        (pos.Y >= checkpointList.Value[0].Y && pos.Y <= checkpointList.Value[1].Y))
                    {
                        if (car.checkpointCounter == checkpoints.Count && checkpointList.Key == 1)
                        {
                            car.checkpointCounter = 1;
                            car.lapCounter++;
                        }
                        else if (car.checkpointCounter + 1 == checkpointList.Key)
                        {
                            car.checkpointCounter = checkpointList.Key;
                        }
                    }
                }
                if (car.lapCounter == laps)
                {
                    Application.Exit();
                }
            }
        }
    }
}