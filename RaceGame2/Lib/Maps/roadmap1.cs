﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceGame2.Lib.Maps
{
    class roadmap1 : Map
    {
        public roadmap1(List<Car> cars) : base(cars)
        {
            this.cars = cars;
            this.laps = 3;
            this.startingLine.Add(new Point(213, 730));
            this.startingLine.Add(new Point(239, 710));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(1, new List<Point> { new Point(116, 701), new Point(168, 753) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(2, new List<Point> { new Point(12, 80), new Point(94, 164) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(3, new List<Point> { new Point(121, 457), new Point(199, 535) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(4, new List<Point> { new Point(785, 229), new Point(859, 302) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(5, new List<Point> { new Point(926, 80), new Point(1009, 164) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(6, new List<Point> { new Point(930, 676), new Point(1008, 758) }));
            this.pitstop.Add(new Point(82, 608));
            this.pitstop.Add(new Point(358, 689));
            this.respawn.Add(new Point(886, 200));

           // this.imageLocation = "road1.png";
           
           // setImage();
        }
    }
}