using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net.Mime;
using System.Windows.Forms;
using System.Xml.Serialization;

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
        public int weight = 3;
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
        private bool leftPressed = false, rightPressed = false, throttlePressed = false, brakePressed = false, triggerPressed = false;
        private Keys leftKey, rightKey, throttleKey, brakeKey, triggerKey;
        private Image image;
        public int checkpointCounter = 1;
        public int lapCounter = 0;
        public String imageLocation;
        public float handeling = 0.015f;
        private int counter;
        public float Force,ForceAngle,ForceX,ForceY;
        public bool movable = true;
        public int moveCountDown = 0;
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
            this.rotation = (float)Math.PI;
            this.speed = 0;
            positionX = position.X;
            positionY = position.Y;
        }

        public void setFuelCost(int fuelCostModifier)
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
            ForceX = (float) Math.Cos(ForceAngle) * Force;
            ForceY = (float) Math.Sin(ForceAngle) * Force;
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
            if (!this.movable)
            {
                if (moveCountDown == 0)
                {
                    movable = true;
                }
                this.moveCountDown--;
                return;
            }
            if (!Map.onTrack(position.X, position.Y))
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
            changeSpeed();
            calcForce();
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
                    fuel += 1;
                }
                if (health < maxHealth)
                {
                    health += 1;
                }
            }
        }
    }
}
