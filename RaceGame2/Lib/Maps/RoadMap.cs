using System;
using System.Collections.Generic;
using System.Drawing;

namespace RaceGame2.Lib.Maps
{
    class RoadMap : Map
    {
        public RoadMap()
        {
            this.fuelCostModifier = 0.45f;
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
            this.upgrades.Add(new Point(50,256));
            this.upgrades.Add(new Point(405,311));
            this.upgrades.Add(new Point(730,400));
            this.upgrades.Add(new Point(960,125));
            this.upgrades.Add(new Point(960,710));
            startlineNum = 1;
            this.imageLocation = "roadmap.png";
            respawnAngle = (float)Math.PI;
            setImage();
        }
    }
}
