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
            this.fuelCostModifier = 0.5f;
            this.laps = 1;
            this.startingLine.Add(new Point(466, 308));
            this.startingLine.Add(new Point(466, 351));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(1, new List<Point> { new Point(343, 302), new Point(423, 374) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(2, new List<Point> { new Point(72, 142), new Point(144, 214) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(3, new List<Point> { new Point(72, 609), new Point(144, 687) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(4, new List<Point> { new Point(426, 485), new Point(517, 569) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(5, new List<Point> { new Point(863, 614), new Point(934, 683) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(6, new List<Point> { new Point(863, 130), new Point(934, 203) }));
            this.pitstop.Add(new Point(456, 496));
            this.pitstop.Add(new Point(554, 615));
            this.respawn.Add(new Point(132, 433));
            this.imageLocation = "dirtmap.png";
            startlineNum = 4;
            respawnAngle = 0;
            setImage();

        }

    }
}
