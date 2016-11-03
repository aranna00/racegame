using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RaceGame2.Lib.Maps
{   
    class DirtMap : Map
    {
        public DirtMap()
        {
            this.fuelCostModifier = 0.38f;
            this.startingLine.Add(new Point(450,285));
            this.startingLine.Add(new Point(450, 325));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(1, new List<Point> { new Point(343, 283), new Point(418, 355) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(2, new List<Point> { new Point(69, 134), new Point(146, 202) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(3, new List<Point> { new Point(71, 615), new Point(141, 687) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(4, new List<Point> { new Point(447, 462), new Point(544, 585) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(5, new List<Point> { new Point(862, 617), new Point(935, 684) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(6, new List<Point> { new Point(863, 131), new Point(834, 207) }));
            this.pitstop.Add(new Point(429, 532));
            this.pitstop.Add(new Point(572, 586));
            this.respawn.Add(new Point(493, 560));
            this.imageLocation = "dirtmap.png";
            setImage();
            
        }   
             
    }
}
