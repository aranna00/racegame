using System;
using System.Windows.Forms;

namespace RaceGame2.Lib.Cars
{
    public class Pickup : Car
    {
        /// <summary>
        /// Constructor of the default pickup class
        /// </summary>
        public Pickup()
        {
            this.imageLocation = "pickup.png";
            this.fuel = 100;
            this.rotation = (float)Math.PI;
            this.maxSpeed = 2.5f;
            this.fuelCost = 0.1f;
            this.handeling = 0.025f;
            this.acceleration = 0.015f;
            positionX = position.X;
            positionY = position.Y;
            this.weight = 5;
        }
    }
}