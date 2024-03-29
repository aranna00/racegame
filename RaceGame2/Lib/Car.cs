﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RaceGame2.Lib
{
    public class Car
    {
        public float maxSpeed = 4f;
        public int currentSpeed;
        public float acceleration = 0.01f;
        public int grip;
        public int health;
        public int maxHealth;
        public float weight = 3;
        public float fuel = 100;
        public float fuelCost = 0.1f;
        public int maxFuel = 100;
        public int turningSpeed;
        private bool isAccelerating;
        public Point position;
        public float positionX, positionY;
        private Point prevPosition;
        public float rotation;
        public static float rotationRate = (float) Math.PI / 50;
        private float angle;
        public double speed;
        public bool leftPressed = false;
        public bool rightPressed = false;
        public bool throttlePressed = false;
        public bool brakePressed = false;
        private bool triggerPressed = false;
        private Keys leftKey, rightKey, throttleKey, brakeKey, triggerKey;
        private Image image;
        public int checkpointCounter = 1;
        public int lapCounter = 0;
        public String imageLocation;
        public float handeling = 0.015f;
        private int counter;
        public int pitstopCounter = 0;
        public float Force,ForceAngle,ForceX,ForceY;
        public bool movable = true;
        public int moveCountDown = 0;
        public float bulletSpeed = 10f;
        public float shotInterval = 0.5f;
        public bool alive;
        public bool shot;
        public int player;
        public bool TestMode = false;

        /// <summary>
        /// Constructor of the car class
        /// </summary>
        public Car()
        {
            this.fuel = maxFuel;
            this.imageLocation = "default.png";
            this.rotation = (float)Math.PI;
            this.speed = 0;
            positionX = position.X;
            positionY = position.Y;
        }

        public void setFuelCost(float fuelCostModifier)
        {
            this.fuelCost *= fuelCostModifier;
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

        public void setPosistion(Point position)
        {
            this.position = position;
            this.positionX = position.X;
            this.positionY = position.Y;
        }

        public void SetImage(String carColour)
        {
            this.imageLocation = (carColour+"\\"+this.imageLocation);
            if (TestMode)
            {
                this.imageLocation = ("R:\\RiderProjects\\racegame\\RaceGame2\\assets\\cars\\" + this.imageLocation);
            }
            else
            {
                this.imageLocation = ("assets\\cars\\" + this.imageLocation);
                this.imageLocation = Path.Combine(Environment.CurrentDirectory, imageLocation);
            }
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(imageBitmap.Width/5,imageBitmap.Height/5);
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

        public void calcForce()
        {
            Force = (float) speed * weight;
            ForceAngle = rotation;
            ForceX = (float) Math.Cos(ForceAngle);
            ForceY = (float) Math.Sin(ForceAngle);
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
            else if (fuel > 0 && speed < 0)
            {
                speed += .1;
            }
            else if (fuel <= 0 && speed < 0)
            {
                speed += .1;
            }
            else if (fuel <= 0 && speed > 0)
            {
                coast();
            }
        }

        private void brake()
        {
            if (fuel > 0 && speed <= 0)
            {
                speed -= acceleration;
                CalcFuel();
                if (speed <= -2.0)
                {
                    speed = -2.0;
                }
            }
            if (fuel > 0 && speed > 0)
            {
                speed -= .1;
            }
            else if (fuel <= 0 && speed > 0)
            {
                speed -= .1;
            }
            else if (fuel <= 0 && speed < 0)
            {
                coast();
            }
        }

        private void coast()
        {
            if (speed >= 0.1f)
                speed -= .004f;
            else if (speed <= -.1f)
                speed += 0.01f;
            else
                speed = 0;
        }

        private void rotateRight()
        {
            if (speed != 0)
            {
                this.rotation += (float) (handeling * speed);
            }
        }

        private void rotateLeft()
        {
            if (speed != 0)
            {
                this.rotation -= (float) (handeling * speed);
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
            if (!this.movable)
            {
                if (moveCountDown == 0)
                {
                    movable = true;
                }
                this.moveCountDown--;
                return;
            }
            if (!Map.onTrack(position.X, position.Y) && !TestMode)
            {
                speed = speed * 0.95;
            }
            prevPosition = position;
            positionX += (float) (speed * Math.Cos(rotation)); //pure magic here!
            positionY += (float) (speed * Math.Sin(rotation)); //more magic here
            if (positionX > 990)
            {
                positionX = 990;
            }
            else if (positionX < 13)
            {
                positionX = 13;
            }
            if (positionY > 710)
            {
                positionY = 710;
            }
            else if (positionY < 13)
            {
                positionY = 13;
            }
            if (RaceGame.stuck)
            {
                positionX = position.X;
                positionY = position.Y;
            }
            position.X = (int) Math.Round(positionX);
            position.Y = (int) Math.Round(positionY);
            float angle =
                (float)
                (Math.Atan2(this.getPrevPosition().Y - this.getPosition().Y,
                     this.getPrevPosition().X - this.getPosition().X) * (180 / Math.PI));
            if (Math.Abs(angle) > 2)
            {
                this.angle = angle;
            }
            calcForce();
            if (RaceGame.collision && counter > 10)
            {
                this.rotation = RaceGame.ForceResult.Item2;
                if (speed >= 0){this.speed = RaceGame.ForceResult.Item1 / weight;}
                else{this.speed = 3 * (RaceGame.ForceResult.Item1 / weight);}
                counter = 0;
            }
            counter++;
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

        public void PitStop()
        {
            if (speed == 0)
            {
                if (fuel < maxFuel)
                {
                    fuel += 1;
                }
            }
        }
    }
}
