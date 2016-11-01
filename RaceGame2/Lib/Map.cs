using System;
using System.Collections.Generic;
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
        public List<KeyValuePair<int, List<Point>>> checkpoints = new List<KeyValuePair<int, List<Point>>>();
        public List<Point> respawn = new List<Point>();
        public string imageLocation;
        public Image image;
        public List<Point> pitstop = new List<Point>();
        public List<Car> cars;
        

        public Map(List<Car> cars)
        {
        }


        public void setImage()
        {
            imageLocation = ("assets\\maps\\" + this.imageLocation);
            imageLocation = Path.Combine(System.Environment.CurrentDirectory, imageLocation);
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(1024, 768);
            imageBitmap = new Bitmap(imageBitmap, imageSize);
            this.image = imageBitmap;
        }

        public void Position()
        {
            cars[0].position = new Point(startingLine[0].X, startingLine[0].Y);
            cars[1].position = new Point(startingLine[1].X, startingLine[1].Y);
            cars[0].rotation= (float) Math.PI;
            cars[1].rotation = (float) Math.PI;

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