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
        public float distance;
        private float moveX, moveY;
        public static bool collision, stuck;
        public float ResForce, ResForceX, ResForceY, ResForceAngle, colX, colY;
        public static Tuple<float, float> ForceResult;

        public RaceGame(List<Car> cars, Map map)
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

        void Draw(Graphics g)
        {
//            g.DrawImage(map.getImage(),x: map.getImage().Height,y: map.getImage().Width);
            foreach (Car car in cars)
            {
                var pos = car.getPosition();
                float moveX = car.getImage().Height / 2f + pos.X;
                float moveY = car.getImage().Width / 2f + pos.Y;
                g.TranslateTransform(moveX , moveY );
                g.RotateTransform(car.getRotation()*(float)(180/Math.PI)+90);
                g.TranslateTransform(-moveX , -moveY );
                g.DrawImage(car.getImage(), pos.X, pos.Y);
                g.ResetTransform();
            }
        }

        public void timerGameTicks_Tick(object sender, EventArgs e) {
            foreach (Car car in cars)
            {
                collideCar();
                car.calculateNewPosition();

            }
            Invalidate();
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

        public void collideCar()
        {
            distance = (float) Math.Sqrt(Math.Pow(cars[0].position.X - cars[1].position.X, 2) + Math.Pow(cars[0].position.Y - cars[1].position.Y, 2));
            if (distance < 20)
            {
                ForceRes();
                colX = (cars[0].position.X + cars[1].position.X) / 2;
                colY = (cars[0].position.Y + cars[1].position.Y) / 2;
                collision = true;
            }
            else
            {
                collision = false;
            }
        }

        public void ForceRes()
        {
            ResForceX = cars[0].ForceX + cars[1].ForceX;
            ResForceY = cars[0].ForceY + cars[1].ForceY;
            ResForce = (float) Math.Sqrt(Math.Pow(ResForceX, 2) + Math.Pow(ResForceY, 2));
            ResForceAngle = (float) Math.Atan2(ResForceY, ResForceX) - 2* (float)Math.PI;
            ForceResult = new Tuple<float,float>(ResForce, ResForceAngle);
            stuckFix();
        }

        public void stuckFix()
        {

        }

        #endregion
        public System.Windows.Forms.Timer timerGameTicks;
    }
}