using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RaceGame2.Lib
{
    public class Map
    {
        public float fuelCostModifier = 1f;
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
        public List<Point> upgrades = new List<Point>();
        private int upgradeCounter = 0;
        public bool pitstopbool = false;
        public int startlineNum;
        public float respawnAngle;
        private bool hasWon;
        public bool TestMode = false;


        public void setCarsStartingPoints()
        {
            cars[0].setPosistion(startingLine[0]);
            cars[1].setPosistion(startingLine[1]);
        }


        public void setImage()
        {
            if (TestMode)
            {
                this.imageLocation = ("R:\\RiderProjects\\racegame\\RaceGame2\\assets\\maps\\" + this.imageLocation);
            }
            else
            {
                imageLocation = ("assets\\maps\\" + this.imageLocation);
                this.imageLocation = Path.Combine(Environment.CurrentDirectory, imageLocation);
            }
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(1007, 728);
            imageBitmap = new Bitmap(imageBitmap, imageSize);
            this.image = imageBitmap;
            mapImage = (Bitmap) image;
        }

        public void Position()
        {
            cars[0].position = new Point(startingLine[0].X, startingLine[0].Y);
            cars[1].position = new Point(startingLine[1].X, startingLine[1].Y);
            cars[0].rotation = (float) Math.PI;
            cars[1].rotation = (float) Math.PI;
        }

        public void upgradeCirculation()
        {
            if (upgradeCounter == 300)
            {
                upgradeCounter = 0;
            }
            upgradeCounter++;
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
        }

        public void upgradeHit(List<Upgrade> upgradesAvailable)
        {
            foreach (Car car in cars)
            {
                Point pos = car.getPosition();
                foreach (Upgrade upgrade in upgradesAvailable)
                {
                    if (true && upgrade.Visible)
                    {
                        upgrade.Visible = false;
                    }
                }
            }
        }

        public Car checkpointChecker()
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
                        if (checkpointList.Key != startlineNum)
                        {
                            pitstopbool = false;
                        }
                    }
                }
                if (car.lapCounter == laps && !hasWon)
                {
                    hasWon = true;
                    return car;
                }
            }
            return null;
        }

        public void endScreenFormOnCloseEventHandler(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void pitstopChecker()
        {
            foreach (Car car in cars)
            {
                Point pos = car.getPosition();

                if ((pos.X >= pitstop[0].X && pos.X <= pitstop[1].X) &&
                    (pos.Y >= pitstop[0].Y && pos.Y <= pitstop[1].Y))
                {
                    if (pitstopbool == false)
                    {
                        pitstopbool = true;
                        car.pitstopCounter += 1;
                    }
                    car.PitStop();
                }
            }
        }
    }
}