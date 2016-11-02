using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using RaceGame2.Lib;
using RaceGame2.Lib.Cars;

namespace RaceGame2
{
    public class RaceGame : Form
    {
        Bitmap Backbuffer;

        public List<Car> cars;
        private Label label1;
        private Label label5;
        private Label label6;
        private Label label2;
        private Label label3;
        private Label label4;
        public List<Map> maps = new List<Map>();
        public RaceGame(List<Car> cars,Map map)
        {
            this.init();
            this.InitializeComponent();
            this.cars = cars;

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
            Image background = new Bitmap(Path.Combine(Environment.CurrentDirectory, "assets\\maps\\dirtmap.png"));

            foreach (Car car in cars)
            {
                var pos = car.getPosition();
                g.TranslateTransform(pos.X, pos.Y);
                g.RotateTransform(car.getRotation()*(float)(180/Math.PI)+90);
                g.DrawImage(car.getImage(), x: -(float)car.getImage().Height/4, y: -(float)car.getImage().Width);
                g.ResetTransform();
            }
            Pen blackPen = new Pen(Color.WhiteSmoke, 3);
            Brush blackBrush = new SolidBrush(Color.Goldenrod);
            //Brush whiteBrush = new SolidBrush(Color.WhiteSmoke);
            Brush EmptyBrush = new SolidBrush(Color.Gray);
            g.FillRectangle(EmptyBrush, 816, 36, 100, 15);
            g.FillRectangle(blackBrush, 816, 36, 1 + (cars[1].fuel / cars[1].maxFuel * 100), 16);
            g.DrawRectangle(blackPen, 816, 36, 100, 15);


                    
            g.FillRectangle(EmptyBrush, 90, 36, 100, 15);
            g.FillRectangle(blackBrush, 90, 36, 1 + (cars[0].fuel / cars[0].maxFuel * 100), 16);
            g.DrawRectangle(blackPen, 90, 36, 101, 16);

           Image Speedmeter_Indic1 =  new Bitmap(Path.Combine(Environment.CurrentDirectory,"assets\\Speedmeter_Indic.png"));
           Image Speedmeter_base1 = new Bitmap(Path.Combine(Environment.CurrentDirectory,"assets\\Speedmeter_base.png"));
            Image Speedmeter_Indic2 = new Bitmap(Path.Combine(Environment.CurrentDirectory, "assets\\Speedmeter_Indic.png"));
            Image Speedmeter_base2 = new Bitmap(Path.Combine(Environment.CurrentDirectory, "assets\\Speedmeter_base.png"));
            //g.DrawImage(Speedmeter_Indic1,Speedmeter_Indic1.Width,Speedmeter_Indic1.Height);
            Image Base1 = new Bitmap(Speedmeter_base1, new Size(60, 60));
            g.DrawImage(Base1, 21,19);
            Image Base2 = new Bitmap(Speedmeter_base2, new Size(60, 60));
            g.DrawImage(Base2, 926, 19);
            g.TranslateTransform(956, 50);
            g.RotateTransform(-135 + Math.Abs((float)cars[1].speed/cars[1].maxSpeed)*270);
            g.TranslateTransform(-956, -50);
            g.DrawImage(Speedmeter_Indic2, 952, 20);
            g.ResetTransform();
            g.TranslateTransform(51, 50);
            g.RotateTransform(-135 + Math.Abs((float)cars[0].speed / cars[0].maxSpeed) * 270);
            g.TranslateTransform(-51, -50);
            g.DrawImage(Speedmeter_Indic1, 47, 20);
            g.ResetTransform();







        }


        public void timerGameTicks_Tick(object sender, EventArgs e)
        {
           
            foreach (Car car in cars)
            {
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

        #endregion
        public System.Windows.Forms.Timer timerGameTicks;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RaceGame));
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "0";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(89, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 19);
            this.label5.TabIndex = 13;
            this.label5.Text = "Player 1";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Click += new System.EventHandler(this.label5_Click_1);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(159, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "1/5";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(884, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 19);
            this.label2.TabIndex = 16;
            this.label2.Text = "1/5";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(815, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "Player 2";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(949, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 22);
            this.label4.TabIndex = 17;
            this.label4.Text = "0";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RaceGame
            // 
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "RaceGame";
            this.ResumeLayout(false);

        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}