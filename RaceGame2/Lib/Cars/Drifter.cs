using System;

namespace RaceGame2.Lib.Cars
{
    public class Drifter : Car
    {
        /// <summary>
        /// Constructor of the default drifter car class
        /// </summary>
        public Drifter()
        {
            this.imageLocation = "drifter.png";
            this.fuel = 100;
            this.rotation = (float) Math.PI;
            this.maxSpeed = 3f;
            this.fuelCost = 0.12f;
            this.handeling = 0.02f;
            this.acceleration = 0.05f;
            positionX = position.X;
            positionY = position.Y;
            this.weight= 1;
            
        }
    }
}