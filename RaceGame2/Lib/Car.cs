using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RaceGame2.Lib
{
    public class Car
    {
        public float maxSpeed = 4f;
        public int currentSpeed;
        public int acceleration;
        public int grip;
        public int health;
        public int maxHealth;
        public int weight;
        public int fuel;
        public int fuelCost;
        public int maxFuel = 100;
        public int turningSpeed;
        private bool isAccelerating;
        public Point position;
        public Point prevPosition;
        public float rotation;
        public static float rotationRate = (float) Math.PI/50;
        private float angle;
        private double speed;
        private bool leftPressed = false, rightPressed = false, throttlePressed = false, brakePressed = false;
        private Keys leftKey, rightKey, throttleKey, brakeKey;
        private Image image;
        public int checkpointCounter = 1;
        public int lapCounter = 0;




        /// <summary>
        /// Constructor of the car class
        /// </summary>
        /// <param name="postionx">starting position of the car (Horiz)</param>
        /// <param name="positiony">starting position of the car (Vert)</param>
        /// <param name="rotation">starting rotation of the car (0 for car is pointing left)</param>
        /// <param name="speed">starting speed of the car</param>
        /// <param name="leftKey">the key to steer left</param>
        /// <param name="rightKey">the key to steer right</param>
        /// <param name="throttleKey">the key to throttle</param>
        /// <param name="brakeKey">the key to brake/reverse</param>
        /// <param name="carColour">the colour of the players car</param>
        /// <param name="imageLocation">the image name used to draw the car</param>
        public Car(int postionx, int positiony, float rotation, double speed, Keys leftKey, Keys rightKey,
            Keys throttleKey, Keys brakeKey, String carColour = "black", String imageLocation = "default.png")
        {
            imageLocation = (carColour + "\\" + imageLocation);
            imageLocation = ("assets\\cars\\" + imageLocation);
            imageLocation = Path.Combine(Environment.CurrentDirectory, imageLocation);
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(imageBitmap.Width/5, imageBitmap.Height/5);
            imageBitmap = new Bitmap(imageBitmap, imageSize);
            position.X = postionx;
            position.Y = positiony;
            this.rotation = rotation;
            this.speed = speed;
            this.leftKey = leftKey;
            this.rightKey = rightKey;
            this.throttleKey = throttleKey;
            this.brakeKey = brakeKey;
            this.image = imageBitmap;
        }

        public void CalcFuel()
        {
            fuel = fuel - fuelCost;
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
            speed = speed + .1;

            if (speed >= maxSpeed)
                speed = maxSpeed;
        }

        private void brake()
        {
            speed = speed - .1;

            if (speed <= -2.0)
                speed = -2.0;
        }

        private void coast()
        {
            if (speed >= .02)
                speed -= .05;
            else if (speed <= -.02)
                speed += 0.05;
            else
                speed = 0;
        }

        private void rotateRight()
        {
            if (speed != 0)
            {
                this.rotation += (float) (.07f*speed/10);
            }
        }

        private void rotateLeft()
        {
            if (speed != 0)
            {
                this.rotation -= (float) (.07f*speed/10);
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
    }

}
                    