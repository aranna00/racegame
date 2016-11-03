using System;
using System.Windows.Forms;

namespace RaceGame2.Lib.Cars
{
    public class Muscle : Car
    {
        /// <summary>
        /// Constructor of the default muscle class
        /// </summary>
        public Muscle()
        {
            this.imageLocation = "muscle.png";
            this.fuel = 100;
            this.rotation = (float) Math.PI;
            this.maxSpeed = 3.5f;
            this.fuelCost = 0.15f;
            this.handeling = 0.02f;
            this.acceleration = 0.025f;
            positionX = position.X;
            positionY = position.Y;
            this.weight = 3;
        }
    }
}