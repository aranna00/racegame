using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RaceGame2.Lib;
using RaceGame2.Lib.Cars;
using RaceGame2.Lib.Maps;

namespace RaceGame2.Tests
{
    public class Pitstop
    {
        private Map map;
        private Car car;
        private float middleX;
        private float middleY;
        private float lastFuel;

        public void init()
        {
            car = new Pickup();
            map = new DirtMap();
            car.SetImage("Green");
            car.setControls(Keys.Left, Keys.Right, Keys.Up, Keys.Down, Keys.RControlKey);
            car.player = 1;
            car.fuel = 0;
            List<Car> cars = new List<Car> {car};
            map.cars = cars;
            middleX = (map.pitstop[0].X + map.pitstop[0].X) / 2;
            middleY = (map.pitstop[0].Y + map.pitstop[0].Y) / 2;
        }

        [Test]
        public void testPistopOutside()
        {
            init();
            map.pitstopChecker();
            Assert.AreEqual(0f, map.cars[0].fuel);
            Assert.AreEqual(0, map.cars[0].pitstopCounter);
        }

        [Test]
        public void testPistopWithSpeed()
        {
            init();
            map.cars[0].position = new Point((int) middleX, (int) middleY);
            map.cars[0].speed = 1f;
            map.pitstopChecker();
            Assert.AreEqual(0f, map.cars[0].fuel);
            Assert.AreEqual(1, map.cars[0].pitstopCounter);
            lastFuel = map.cars[0].fuel;
        }

        [Test]
        public void testPitstopWithoutSpeed()
        {
            init();
            map.cars[0].position = new Point((int) middleX, (int) middleY);
            map.cars[0].speed = 0f;
            map.pitstopChecker();
            Assert.Greater(map.cars[0].fuel, 0);
            Assert.AreEqual(1, map.cars[0].pitstopCounter);
        }

        [Test]
        public void testPistopReset()
        {
            init();
            map.cars[0].position = new Point((int) middleX, (int) middleY);
            map.pitstopChecker();
            map.cars[0].position = new Point((map.checkpoints[0].Value[0].X+map.checkpoints[0].Value[1].X)/2, (map.checkpoints[0].Value[0].Y+map.checkpoints[0].Value[1].Y)/2);
            map.pitstopChecker();
            map.checkpointChecker();
            Assert.Greater(map.cars[0].fuel,lastFuel);
            Assert.AreEqual(1, map.cars[0].pitstopCounter);
        }

        [Test]
        public void testPitstopCounter()
        {
            init();
            map.cars[0].position = new Point((int) middleX, (int) middleY);
            map.pitstopChecker();
            map.cars[0].position = new Point((map.checkpoints[0].Value[0].X+map.checkpoints[0].Value[1].X)/2, (map.checkpoints[0].Value[0].Y+map.checkpoints[0].Value[1].Y)/2);
            map.pitstopChecker();
            map.checkpointChecker();
            map.cars[0].position = new Point((int) middleX, (int) middleY);
            for (int i = 0; i < 150; i++)
            {
                map.pitstopChecker();
            }
            Assert.AreEqual(map.cars[0].maxFuel, map.cars[0].fuel);
            Assert.AreEqual(2, map.cars[0].pitstopCounter);
        }
    }
}