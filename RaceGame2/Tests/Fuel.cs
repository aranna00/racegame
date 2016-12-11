using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;
using RaceGame2.Lib;
using RaceGame2.Lib.Cars;

namespace RaceGame2.Test
{
    class Fuel
    {

        [Test]
        public void TestFuel()
        {

            {
                Car car = new Pickup();
                car.setControls(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.RControlKey);
                List<Car> cars = new List<Car> { car };
                ResetCar(car);
                double lastFuel, lastSpeed;

                // Fuelcost    

                
                ResetCar(car);
                lastFuel = car.fuel;
                car.speed = 1;
                car.throttlePressed = true;
                car.calculateNewPosition();
                Assert.Less(car.fuel,lastFuel);
                
                //  No Fuelcost during break

                ResetCar(car);
                car.speed = 1;
                lastFuel = car.fuel;
                car.brakePressed = true;
                car.calculateNewPosition();
                Assert.AreEqual(car.fuel,lastFuel);

                // Fuelcost during reverse

                ResetCar(car);
                car.speed = -2;
                lastFuel = car.fuel;
                car.brakePressed = true;
                car.calculateNewPosition();
                Assert.Less(car.fuel, lastFuel);

                // No movement when fuel is empty

                ResetCar(car);
                car.speed = 1;
                car.fuel = 0;
                lastSpeed = car.speed;
                car.throttlePressed = true;
                car.calculateNewPosition();
                Assert.Less(car.speed,lastSpeed);

                // No fuelcost during coast

                ResetCar(car);
                car.speed = 1;
                lastFuel = car.fuel;
                car.throttlePressed = true;
                car.throttlePressed = false;
                car.calculateNewPosition();
                Assert.AreEqual(car.fuel, lastFuel);

               



            }
        
            




        }

        public void ResetCar ( Car car)
        {
            car.throttlePressed = false;
            car.brakePressed = false;
            car.fuel = car.maxFuel;



        }
    }
}
