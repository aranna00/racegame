using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace RaceGame2.Lib
{
    public class Map
    {
        public int fuelCostModifier;
        public List<Point> startingLine= new List<Point>();
        private int startX;
        private int laps;
        private bool lappable;
        public List<List<Point>> checkpoints = new List<List<Point>>();
        public List<Point> respawn = new List<Point>();
        public string imageLocation;
        public Image image;
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
        }

        public void checkpointChecker()
        {
            int counter = 0;
           foreach(List<Point> checkpoint in checkpoints)
            {
                foreach (Car car in cars)
                {
                    Point pos = car.getPosition();
                    //if ((pos.X >= checkpoint[0].X && pos.X <= checkpoint[1].X) && (pos.Y <= checkpoint[0].Y && pos.Y >= checkpoint[1].Y))
                    //{
                       
                        //if (car.checkpointCounter==0 && counter==0)
                        //{
                            //car.checkpointCounter++;
                            
                        //}
                        
                    //}
                    if(pos.X > checkpoint[0].X)
                    {
                        Logger.Debug(pos.X.ToString() + "1X");
                    }
                    if(pos.X < checkpoint[1].X)
                    {
                        Logger.Debug(pos.X.ToString() + "2X");
                    }
                    if(pos.Y > checkpoint[0].Y)
                    {
                        Logger.Debug(pos.Y.ToString() + "1Y");
                    }
                    if(pos.Y < checkpoint[1].Y)
                    {
                        Logger.Debug(pos.Y.ToString() + "2Y");
                    }
                    counter++;
                }
            }


        }
    }

}