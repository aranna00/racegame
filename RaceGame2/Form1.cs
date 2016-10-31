using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RaceGame2.Lib;
using RaceGame2.Lib.Cars;
using RaceGame2.Lib.Maps;

namespace RaceGame2
{
    public partial class Form1 : Form
    {
        Bitmap Backbuffer;
        private List<Car> cars = new List<Car>();
        private Map map;
        
        public Form1() {
            InitializeComponent();
            //aanmaken van de auto's
            Car car1 = new Pickup(30, 30, 0, 0, Keys.Left, Keys.Right, Keys.Up, Keys.Down, "blue");
            Car car2 = new Drifter(90, 20, 0, 0, Keys.A, Keys.D, Keys.W, Keys.S, "green");
            //toevoegen auto's aan de lijst cars

            cars.Add(car1);
            cars.Add(car2);
          map = new DirtMap1(cars);
         // map = new roadmap1(cars);
          map.Position();
            
            
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer, true);

            this.ResizeEnd += new EventHandler(Form1_CreateBackBuffer);
            this.Load += new EventHandler(Form1_CreateBackBuffer);
            this.Paint += new PaintEventHandler(Form1_Paint);

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
        }
       

        void Form1_KeyUp(object sender, KeyEventArgs e) {
            foreach (Car car in cars)
                car.handleKeyUpEvent(e);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e) {
            foreach (Car car in cars)
                car.handleKeyDownEvent(e);
        }

        void Form1_Paint(object sender, PaintEventArgs e) {
            if (Backbuffer != null) {
                e.Graphics.DrawImageUnscaled(Backbuffer, Point.Empty);
            }

            Draw(e.Graphics);
        }

        void Form1_CreateBackBuffer(object sender, EventArgs e)
        {
            if (Backbuffer != null)
                Backbuffer.Dispose();

            Backbuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        void Draw(Graphics g) {
            g.DrawImage(map.image, 0, 0);

            foreach (Car car in cars)
            {
                //g.TranslateTransform((float)car.getImage().Width/2, (float)car.getImage().Height/2);
                //g.RotateTransform(car.getAngle()+180);
                //g.TranslateTransform(-(float)car.getImage().Width/2, -(float)car.getImage().Height/2);
                var pos = car.getPosition();

                g.TranslateTransform(pos.X, pos.Y);
                g.RotateTransform(car.getRotation()*57.1f + 90);

                g.DrawImage(car.getImage(), x: -(float)car.getImage().Height/2, y: -(float)car.getImage().Width/2);
                g.ResetTransform();
            }
        }

        private void timerGameTicks_Tick(object sender, EventArgs e) {
            label1.Text = cars[0].checkpointCounter.ToString();
            label2.Text = cars[0].getPosition().X.ToString();
            label3.Text = cars[0].getPosition().Y.ToString();
            label4.Text = cars[0].lapCounter.ToString();
            map.checkpointChecker();
            foreach (Car car in cars)
            {
                car.calculateNewPosition();
            }
            Invalidate();
        }

        
    }
}
