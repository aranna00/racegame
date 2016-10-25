﻿using System.Drawing;
using System.Windows.Forms;

namespace RaceGame2.Cars
{
    public class Drifter : Car
    {
        public Drifter(int postionx, int positiony, float rotation, double speed, Keys leftKey, Keys rightKey, Keys throttleKey, Keys brakeKey, Image image) : base(postionx, positiony, rotation, speed, leftKey, rightKey, throttleKey, brakeKey, image)
        {
        }
    }
}