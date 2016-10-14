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

namespace RaceGame2
{
    public partial class Form1 : Form
    {
        Bitmap Backbuffer;              //truc om flikkeren te voorkomen bij tekenen

        float Angle { get; set; }       //rotatie van de auto
        const int carAxisSpeed = 2;

        Point carPos = new Point(30, 30); //wordt nog niet gebruikt! Maar misschien wel een idee!
        Point carSpeed = new Point(carAxisSpeed, carAxisSpeed);


        Image image = new Bitmap(Path.Combine(Environment.CurrentDirectory, "car.jpg"));
        //Opmerking: als je nieuwe plaatje wilt inladen zorg ervoor dat je de volgende Property aanpast in visual studio voor het desbtrefende plaatje:
        //Copy To Output Directory: Copy always

        public Form1()
        {
            InitializeComponent();

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            Timer GameTimer = new Timer();
            GameTimer.Interval = 10;
            GameTimer.Tick += new EventHandler(GameTimer_Tick);
            GameTimer.Start();

            this.ResizeEnd += new EventHandler(Form1_CreateBackBuffer);
            this.Load += new EventHandler(Form1_CreateBackBuffer);
            this.Paint += new PaintEventHandler(Form1_Paint);

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                carSpeed.X = -carAxisSpeed;
            else if (e.KeyCode == Keys.Right)
                carSpeed.X = carAxisSpeed;
            else if (e.KeyCode == Keys.Up)
                carSpeed.Y = -carAxisSpeed; // Y axis is downwards so -ve is up.
            else if (e.KeyCode == Keys.Down)
                carSpeed.Y = carAxisSpeed;
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (Backbuffer != null)
            {
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

        

        void Draw(Graphics g)
        {
            //doe ook iets met de carSpeed! Dit moet je zelf bedenken, de carspeed wordt veranderd in Form1_KeyDown


            g.TranslateTransform(100 + image.Width / 2.0f, image.Height / 2.0f);
            g.RotateTransform(Angle);
            g.TranslateTransform(-image.Width / 2.0f, -image.Height / 2.0f);

            g.DrawImage(image, 0, 0);


            SolidBrush brush = new SolidBrush(System.Drawing.Color.Green);            
            //recthoek die over auto staat
            g.FillRectangle(brush, 10, 10, 100, 100);
        }


        void GameTimer_Tick(object sender, EventArgs e)
        {
            Angle += 1f;

            //BallPos.X += BallSpeed.X;
            //BallPos.Y += BallSpeed.Y;
            
            Invalidate();
        }
    }
}
