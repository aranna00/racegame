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
using RaceGame2.Cars;

namespace RaceGame2
{
    public partial class Form1 : Form
    {
        Bitmap Backbuffer;

        List<Car> cars = new List<Car>();
        private Map map;
        private Map trees;

        public Form1() {
            InitializeComponent();

            //aanmaken van de auto's
            Image image1 = new Bitmap(Path.Combine(Environment.CurrentDirectory, "car_black_1.png"));
            Size image1Size = new Size(23,40);
            image1 = new Bitmap(image1,image1Size);
            Image image2 = new Bitmap(Path.Combine(Environment.CurrentDirectory, "car.jpg"));
            Car car1 = new Car(300, 300, 0, 0, Keys.Left, Keys.Right, Keys.Up, Keys.Down, image1);
            Car car2 = new Car(90, 20, 0, 0, Keys.A, Keys.D, Keys.W, Keys.S, image2);

            //map
            Image mapbg = new Bitmap(Path.Combine(Environment.CurrentDirectory, "background.png"));
            Size mapbgSize = new Size(mapbg.Width,mapbg.Height);
            mapbg = new Bitmap(mapbg,mapbgSize);
            map  = new Map(0, 0, mapbg);

            Image trees = new Bitmap(Path.Combine(Environment.CurrentDirectory, "trees.png"));
            trees = new Bitmap(trees,mapbgSize);

            //toevoegen auto's aan de lijst cars
            cars.Add(car1);
            cars.Add(car2);

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
            g.DrawImage(map.image,-800,-2600);
            foreach (Car car in cars)
            {

                //g.TranslateTransform((float)car.getImage().Width/2, (float)car.getImage().Height/2);
                //g.RotateTransform(car.getAngle()+180);
                //g.TranslateTransform(-(float)car.getImage().Width/2, -(float)car.getImage().Height/2);
                var pos = car.getPosition();

                g.TranslateTransform(pos.X, pos.Y);
                g.RotateTransform(car.getAngle() - 90);

                g.DrawImage(car.getImage(), x: -(float)car.getImage().Height/2, y: -(float)car.getImage().Width/2);
                g.ResetTransform();
            }
        }

        private void timerGameTicks_Tick(object sender, EventArgs e) {
            foreach (Car car in cars)
                car.calculateNewPosition();

            Invalidate();
        }
    }
}
