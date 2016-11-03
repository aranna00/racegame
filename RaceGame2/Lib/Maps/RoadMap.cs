using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceGame2.Lib.Maps
{
    class RoadMap : Map
    {
        public RoadMap()
        {
            this.startingLine.Add(new Point(235, 665));
            this.startingLine.Add(new Point(218, 690));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(1, new List<Point> { new Point(109, 586), new Point(179, 713) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(2, new List<Point> { new Point(32, 77), new Point(108, 152) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(3, new List<Point> { new Point(132, 430),new Point(211, 510) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(4, new List<Point> { new Point(766, 211), new Point(836, 381) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(5, new List<Point> { new Point(900, 76), new Point(974, 159) }));
            this.checkpoints.Add(new KeyValuePair<int, List<Point>>(6, new List<Point> { new Point(900, 638), new Point(974, 710) }));
            this.pitstop.Add(new Point(109, 571));
            this.pitstop.Add(new Point(360, 636));
            this.respawn.Add(new Point(400, 200));
            startlineNum = 1;
            this.imageLocation = "roadmap.png";
            respawnAngle = (float)Math.PI;
            setImage();
        }
    }
}
