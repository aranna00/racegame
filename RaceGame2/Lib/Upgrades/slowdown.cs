using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace RaceGame2.Lib.Upgrades
{
    class slowdown: Upgrade
    {
        public slowdown()
        {
            this.setImage("slow.png");
        }
    }
}
