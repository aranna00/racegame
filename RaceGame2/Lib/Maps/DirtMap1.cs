using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RaceGame2.Lib.Maps
{
    class DirtMap1 : Map
    {
        public DirtMap1(List<Car> cars) : base(cars)
        {
            this.cars = cars;
            this.startingLine.Add(new Point(464, 315));
            this.startingLine.Add(new Point(464, 360));
            this.checkpoints.Add(new List<Point> { new Point(y:411, x:298), new Point(y:470, x:378) });
            this.checkpoints.Add(new List<Point> { new Point(54, 142), new Point(132, 214) });
            this.checkpoints.Add(new List<Point> { new Point(54, 648), new Point(132, 729) });
            this.checkpoints.Add(new List<Point> { new Point(426, 485), new Point(517, 569) });
            this.checkpoints.Add(new List<Point> { new Point(891, 648), new Point(972, 721) });
            this.checkpoints.Add(new List<Point> { new Point(891, 135), new Point(972, 222) });
            this.pitstop.Add(new Point(103, 377));
            this.pitstop.Add(new Point(163, 484));
            this.respawn.Add(new Point(132, 433));

            this.imageLocation = "dirtmap1.png";
            setImage();
            
        }   
             
    }
}
