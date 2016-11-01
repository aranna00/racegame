using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using RaceGame2.Lib;

namespace RaceGame2
{
    public class RaceGame : Form
    {
        Bitmap Backbuffer;

        public List<Car> cars;
        public List<Map> maps = new List<Map>();
        public Map map;
        private int upgradeTimer = 0;
        public List<Upgrade> upgrades = new List<Upgrade>();
        private int curUpgrade;
        private Point newPosition;

        public RaceGame(List<Car> cars,Map map)
        {
            this.BackgroundImage = map.getImage();
            this.init();
            this.cars = cars;
            this.map = map;

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer, true);

            this.ResizeEnd += new EventHandler(RaceGame_CreateBackBuffer);
            this.Load += new EventHandler(RaceGame_CreateBackBuffer);
            this.Paint += new PaintEventHandler(RaceGame_Paint);

            this.KeyDown += new KeyEventHandler(RaceGame_KeyDown);
            this.KeyUp += new KeyEventHandler(RaceGame_KeyUp);
            upgradeslist();
        }

        public void upgradeslist()
        {
            List<String> upgradeImage = new List<String>();
            upgradeImage.Add("tool.png");
            upgradeImage.Add("star.png");
        }

        void RaceGame_KeyUp(object sender, KeyEventArgs e) {
            foreach (Car car in cars)
                car.handleKeyUpEvent(e);
        }

        void RaceGame_KeyDown(object sender, KeyEventArgs e) {
            foreach (Car car in cars)
                car.handleKeyDownEvent(e);
        }

        void RaceGame_Paint(object sender, PaintEventArgs e) {
            if (Backbuffer != null) {
                e.Graphics.DrawImageUnscaled(Backbuffer, Point.Empty);
            }

            Draw(e.Graphics);
        }

        void RaceGame_CreateBackBuffer(object sender, EventArgs e)
        {
            if (Backbuffer != null)
                Backbuffer.Dispose();

            Backbuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        void Draw(Graphics g) {

            foreach (Car car in cars)
            {
                var pos = car.getPosition();
                g.TranslateTransform(pos.X, pos.Y);
                g.RotateTransform(car.getRotation()*(float)(180/Math.PI)+90);
                g.DrawImage(car.getImage(), x: -(float)car.getImage().Height/4, y: -(float)car.getImage().Width);
                g.ResetTransform();
            }
            foreach (Upgrade upgrade in upgrades)
            {
                var pos = upgrade.getPosition();
                g.TranslateTransform(pos.X, pos.Y);
                g.DrawImage(upgrade.GetImage(), x: -(float)upgrade.GetImage().Height / 4, y: -(float)upgrade.GetImage().Width);
            }
        }

        public void timerGameTicks_Tick(object sender, EventArgs e) {
            foreach (Car car in cars)
            {
                car.calculateNewPosition();
            }
            Invalidate();
        }

        public void spawnUpgrade()
        {
            if (upgradeTimer == 300)
            {
                upgrades.Add(new Upgrade());
                curUpgrade = upgrades.Count - 1;
                Random rnd = new Random();
                int randomUpgrade = rnd.Next(0, map.upgrades.Count);
                newPosition = map.upgrades[randomUpgrade];
                upgrades[curUpgrade].setPosition(newPosition);
                upgradeTimer = 0;
            }
            else
            {
                upgradeTimer++;
            }
        }



        #region Windows Form Designer generated code
        private void init()
        {
            Size size = new Size(1024, 768);
            this.Location = new Point(0,0);
            this.Size = size ;
            this.MaximumSize = size;
            this.Name = "RaceGame";
            this.Text = "De echte racegame";
            timerGameTicks = new Timer();
            this.timerGameTicks.Interval = 1;
            this.timerGameTicks.Enabled = true;
            this.timerGameTicks.Tick += new System.EventHandler(this.timerGameTicks_Tick);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;

        }

        #endregion
        public System.Windows.Forms.Timer timerGameTicks;
    }
}