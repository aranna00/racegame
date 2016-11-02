﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RaceGame2.Lib
{
    public class Car
    {
        public float maxSpeed = 4f;
        public int currentSpeed;
        public float acceleration = 0.03f;
        public int grip;
        public int health;
        public int maxHealth;
        public int weight;
        public float fuel = 100;
        public float fuelCost = 0.1f;
        public int maxFuel = 100;
        public int turningSpeed;
        private bool isAccelerating;
        public Point position = new Point(0,0);
        private Point prevPosition;
        public float rotation;
        public static float rotationRate = (float) Math.PI / 50;
        private float angle;
        public double speed;
        private bool leftPressed = false, rightPressed = false, throttlePressed = false, brakePressed = false, triggerPressed = false;
        private Keys leftKey, rightKey, throttleKey, brakeKey, triggerKey;
        private Image image;
        public int checkpointCounter = 1;
        public int lapCounter = 0;
        public String imageLocation;
        public float bulletSpeed = 10f;
        public float shotInterval = 0.5f;
        public bool alive;
        public bool shot;








        /// <summary>
        /// Constructor of the car class
        /// </summary>
        public Car()
        {
            this.fuel = 100;
            this.imageLocation = "default.png";
            position.X = 0;
            position.Y = 0;
            this.rotation = 0;
            this.speed = 0;
        }

        /// <param name="leftKey">the key to steer left</param>
        /// <param name="rightKey">the key to steer right</param>
        /// <param name="throttleKey">the key to throttle</param>
        /// <param name="brakeKey">the key to brake/reverse</param>
        public void setControls(Keys leftKey, Keys rightKey, Keys throttleKey, Keys brakeKey, Keys triggerKey)
        {
            this.leftKey = leftKey;
            this.rightKey = rightKey;
            this.throttleKey = throttleKey;
            this.brakeKey = brakeKey;
            this.triggerKey = triggerKey;
        }

        public void SetImage(String carColour)
        {
            this.imageLocation = (carColour+"\\"+this.imageLocation);
            this.imageLocation = ("assets\\cars\\"+this.imageLocation);
            this.imageLocation = Path.Combine(Environment.CurrentDirectory, imageLocation);
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(imageBitmap.Width/4,imageBitmap.Height/4);
            imageBitmap = new Bitmap(imageBitmap,imageSize);
            this.image = imageBitmap;
        }

        public void CalcFuel()
        {
            fuel -= fuelCost;
        }

        internal void Shoot()
        {

            bool shot = false;
            if (health > 0)
            {
                alive = true;
            }
                Bullet bullet = new Bullet();
            bullet.rotation = this.rotation;


        }


        public void handleKeyDownEvent(KeyEventArgs keys)
        {
            if (leftKey == keys.KeyCode)
                leftPressed = true;
            if (rightKey == keys.KeyCode)
                rightPressed = true;
            if (throttleKey == keys.KeyCode)
                throttlePressed = true;
            if (brakeKey == keys.KeyCode)
                brakePressed = true;
            if (triggerKey == keys.KeyCode)
                triggerPressed = true;
        }

        public void handleKeyUpEvent(KeyEventArgs keys)
        {
            ;
            if (leftKey == keys.KeyCode)
                leftPressed = false;
            if (rightKey == keys.KeyCode)
                rightPressed = false;
            if (throttleKey == keys.KeyCode)
                throttlePressed = false;
            if (brakeKey == keys.KeyCode)
                brakePressed = false;
            if (triggerKey == keys.KeyCode)
                triggerPressed = false;
        }

        public Point getPosition()
        {
            return position;
        }

        public Point getPrevPosition()
        {
            return prevPosition;
        }

        public Image getImage()
        {
            return image;
        }

        private void accelerate()
        {
            if (fuel > 0 && speed >= 0)
            {
                speed += acceleration;
                CalcFuel();
                if (speed >= maxSpeed)
                {
                   speed = maxSpeed;

                }

            }
            if (fuel > 0 && speed < 0)
            {
                speed += .1;
            }
            else
            {
                coast();
            }
        }



        private void brake()
        {
            if (fuel > 0 && speed <= 0)
            {
                speed -= 0.1;
                CalcFuel();
                if (speed >= 2.0)
                {
                    speed = 2.0;
                }
            }
            if (fuel > 0 && speed > 0)
            {
                speed -= .1;
            }
            else
            {
                coast();
            }
        }

        private void coast()
        {
            if (speed >= .008)
                speed -= .02;
            else if (speed <= -.008)
                speed += 0.02;
            else
                speed = 0;
        }

        private void rotateRight()
        {
            if (speed != 0)
            {
                this.rotation += (float) (.15f * speed / 10);
            }
        }

        private void rotateLeft()
        {
            if (speed != 0)
            {
                this.rotation -= (float) (.15f * speed / 10);
            }
        }

        private void changeSpeed()
        {
            if (throttlePressed)
                accelerate();
            else if (brakePressed)
                brake();
            else
                coast();

            if (leftPressed)
                rotateLeft();
            else if (rightPressed)
                rotateRight();
        }

        /// <summary>
        /// Calculates the new position for the car
        /// </summary>
        public void calculateNewPosition()
        {
            changeSpeed();
            prevPosition = position;
            position.X += (int)Math.Round(speed * Math.Cos(rotation)); //pure magic here!
            position.Y += (int)Math.Round(speed * Math.Sin(rotation)); //more magic here
            if (position.X > 990)
            {
                position.X = 990;
                speed = 0;
            }
            else if (position.X < 13)
            {
                position.X = 13;
                speed = 0;
            }
            if (position.Y > 710)
            {
                position.Y = 710;
                speed = 0;
            }
            else if (position.Y < 13)
            {
                position.Y = 13;
                speed = 0;
            }
            this.angle =
            position.X += (int) Math.Round(speed*Math.Cos(rotation)); //pure magic here!
            position.Y += (int) Math.Round(speed*Math.Sin(rotation)); //more magic here
            float angle =
                (float)
                (Math.Atan2(this.getPrevPosition().Y - this.getPosition().Y,
                     this.getPrevPosition().X - this.getPosition().X)*(180/Math.PI));
            if (Math.Abs(angle) > 2)
            {
                this.angle = angle;
            }
        }

        public float getAngle()
        {
            return this.angle;
        }

        public double getSpeed()
        {
            return this.speed;
        }

        public float getRotation()
        {
            return this.rotation;
        }

        private void PitStop()
        {
            if (speed == 0)
            {
                if (fuel < maxFuel)
                {
                    fuel += 10;
                }
                if (health < maxHealth)
                {
                    health += 1;
                }
            }
        }
    }

}
                    