using System;
using System.Windows.Forms;

namespace RaceGame2.Lib.Cars
{
    public class Racer : Car
    {
        /// <summary>
        /// Constructor of the default pickup class
        /// </summary>
        public Racer()
        {
            this.imageLocation = "racer.png";
            this.fuel = 100;
            this.rotation = (float)Math.PI;
            this.maxSpeed = 4f;
            this.fuelCost = 0.25f;
            this.handeling = 0.02f;
            this.acceleration = 0.06f;
            positionX = position.X;
            positionY = position.Y;
            this.weight = 1;

        }
    }
}