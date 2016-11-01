using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Windows.Forms;

namespace RaceGame2.Lib
{
    public class Map
    {
        public int fuelCostModifier;
        public List<Point> startingLine = new List<Point>();
        private int startX;
        public int laps;
        private bool lappable;
        //public List<List<Point>> checkpoints = new List<List<Point>>();
        public List<KeyValuePair<int, List<Point>>> checkpoints = new List<KeyValuePair<int, List<Point>>>();
        public List<Point> respawn = new List<Point>();
        public string imageLocation;
        public Image image;
        private static Bitmap mapImage;
        public List<Point> pitstop = new List<Point>();
        public List<Car> cars;

    public Map(List<Car> cars)
        {
            //setImage();
        }

        public void setImage()
        {
            imageLocation = ("assets\\maps\\" + this.imageLocation);
            imageLocation = Path.Combine(System.Environment.CurrentDirectory, imageLocation);
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(1024, 768);
            imageBitmap = new Bitmap(imageBitmap, imageSize);
            this.image = imageBitmap;
            mapImage = (Bitmap)image;
        }

        //        public void checkpointChecker()
        //        {
        //            int counter = 0;
        //            {
        //                foreach (Car car in cars)
        //                {
        //                    Point pos = car.getPosition();
        //                    if ((pos.X >= checkpoint[0].X && pos.X <= checkpoint[1].X) &&
        //                        (pos.Y >= checkpoint[0].Y && pos.Y <= checkpoint[1].Y))
        //                    {
        //                        if (car.checkpointCounter == 0 && counter == 0)
        //                        {
        //                            car.checkpointCounter++;
        //                        }
        //                    }
        //                }
        //            }
        //        }

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
        }

        public void checkpointChecker()
        {
            int counter = 0;
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
}