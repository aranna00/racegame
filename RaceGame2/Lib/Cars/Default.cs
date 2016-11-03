using System;

namespace RaceGame2.Lib.Cars
{
    public class Default : Car
    {
        /// <summary>
        /// Constructor of the default car class
        /// </summary>
        public Default()
        {
            this.fuel = 100;
            this.imageLocation = "default.png";
            this.rotation = (float)Math.PI;
            this.maxSpeed = 2.5f;
            this.fuelCost = 0.05f;
            positionX = position.X;
            positionY = position.Y;
        }
    }
}