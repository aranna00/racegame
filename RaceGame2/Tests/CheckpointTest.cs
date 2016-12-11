using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;
using RaceGame2.Lib.Cars;
using RaceGame2.Lib.Maps;


namespace RaceGame2.Lib.Tests
{
    class CheckpointTest
    {
        [Test]
        public void testCheckpoint()
        {
            Car car = new Pickup();
            car.TestMode = true;
            car.SetImage("green");
            car.setControls(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.RControlKey);
            car.player = 1;
            List<Car> cars = new List<Car> {car};
            Map map = new DirtMap{TestMode = true};
            map.cars = cars;
            map.cars[0].position.X = 0;
            map.cars[0].position.Y = 0;

            //checkpoint test
            for (int i = 0; i < map.checkpoints.Count; i++)
            {
                map.cars[0].position.X = (map.checkpoints[i].Value[0].X + map.checkpoints[i].Value[1].X) / 2;
                map.cars[0].position.Y = (map.checkpoints[i].Value[0].Y + map.checkpoints[i].Value[1].Y) / 2;
                map.checkpointChecker();
                Assert.AreEqual(i + 1, map.cars[0].checkpointCounter);
            }


            //lapcounter test
            int lapCounter = car.lapCounter;
            map.cars[0].position.X = (map.checkpoints[0].Value[0].X + map.checkpoints[0].Value[1].X) / 2;
            map.cars[0].position.Y = (map.checkpoints[0].Value[0].Y + map.checkpoints[0].Value[1].Y) / 2;
            car.checkpointCounter = map.checkpoints.Count;
            map.checkpointChecker();
            Assert.AreEqual(lapCounter + 1, car.lapCounter);

            //respawn position

            map.cars[0].position.X = (map.pitstop[0].X + map.pitstop[1].X) / 2;
            map.cars[0].position.Y = (map.pitstop[0].Y + map.pitstop[1].Y) / 2;
            map.pitstopChecker();
            Assert.AreEqual(1, map.cars[0].checkpointCounter);

        }
    }
}