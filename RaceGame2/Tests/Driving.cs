using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;
using RaceGame2.Lib;
using RaceGame2.Lib.Cars;

namespace RaceGame2.Tests
{
    public class Driving
    {
        [Test]
        public void testDriving()
        {
            Car car = new Muscle();
            car.setControls(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.RControlKey);
            List<Car> cars = new List<Car> {car};
            car.TestMode = true;
            car.fuelCost = 0;
            double lastSpeed, lastRotation, deltaCoast, deltaBrake;
            resetCar(car);

            // forward
            lastSpeed = car.speed;
            car.throttlePressed = true;
            car.calculateNewPosition();
            Assert.Greater(car.speed, lastSpeed);

            // backward
            resetCar(car);
            lastSpeed = car.speed;
            car.brakePressed = true;
            car.calculateNewPosition();
            Assert.Less(car.speed, lastSpeed);

            // coasting
            for (float i = -2; i <= car.maxSpeed; i++)
            {
                resetCar(car);
                car.speed = i;
                lastSpeed = car.speed;
                car.calculateNewPosition();
                if (car.speed == 0)
                {
                    Assert.AreEqual(car.speed, lastSpeed);
                }
                else if(car.speed > 0)
                {
                    Assert.Less(car.speed, lastSpeed);
                }
                else
                {
                    Assert.Greater(car.speed, lastSpeed);
                }
            }

            // forward brake
            resetCar(car);
            car.speed = 1;
            lastSpeed = car.speed;
            car.brakePressed = false;
            car.calculateNewPosition();
            deltaCoast = lastSpeed - car.speed;
            car.speed = 1;
            lastSpeed = car.speed;
            car.brakePressed = true;
            car.calculateNewPosition();
            deltaBrake = lastSpeed - car.speed;
            Assert.Greater(deltaBrake, deltaCoast);
            Assert.Greater(car.speed, 0);

            // backward brake
            resetCar(car);
            car.speed = -1;
            lastSpeed = car.speed;
            car.throttlePressed = false;
            car.calculateNewPosition();
            deltaCoast = Math.Abs(lastSpeed) - Math.Abs(car.speed);
            car.speed = -1;
            lastSpeed = car.speed;
            car.throttlePressed = true;
            car.calculateNewPosition();
            deltaBrake = Math.Abs(lastSpeed) - Math.Abs(car.speed);
            Assert.Greater(deltaBrake, deltaCoast);
            Assert.Less(car.speed, 0);

            // left
            for (float i = -2; i <= car.maxSpeed; i++)
            {
                resetCar(car);
                car.speed = i;
                car.throttlePressed = true;
                lastRotation = car.rotation;
                car.leftPressed = true;
                car.calculateNewPosition();
                if (car.speed == 0)
                {
                    Assert.AreEqual(car.rotation, lastRotation);
                }
                else if(car.speed > 0)
                {
                    Assert.Less(car.rotation, lastRotation);
                }
                else
                {
                    Assert.Greater(car.rotation, lastRotation);
                }
            }

            // right
            for (float i = -2; i <= car.maxSpeed; i++)
            {
                resetCar(car);
                car.speed = i;
                car.throttlePressed = true;
                lastRotation = car.rotation;
                car.rightPressed = true;
                car.calculateNewPosition();
                if (car.speed == 0)
                {
                    Assert.AreEqual(car.rotation, lastRotation);
                }
                else if (car.speed > 0)
                {
                    Assert.Greater(car.rotation, lastRotation);
                }
                else
                {
                    Assert.Less(car.rotation, lastRotation);
                }
            }

            // max-speed
            resetCar(car);
            car.speed = car.maxSpeed;
            car.throttlePressed = true;
            car.calculateNewPosition();
            Assert.AreEqual(car.maxSpeed, car.speed);
        }

        public void resetCar(Car car)
        {
            car.throttlePressed = false;
            car.brakePressed = false;
            car.leftPressed = false;
            car.rightPressed = false;
            car.positionX = 500;
            car.positionY = 500;
            car.speed = 0;
        }
    }
}