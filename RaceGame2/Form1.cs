using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private PictureBox selectedCar1;
        private PictureBox selectedCar2;
        private int counter;
        private List<Car> cars = new List<Car>();
        private Map selectedMap;
        private int laps;
        private RaceGame raceForm;

        public Form1()
        {
            InitializeComponent();
            selectedCar1 = Default1;
            selectedCar2 = Default2;
            rotationTimer = new System.Windows.Forms.Timer();
            rotationTimer.Tick += new EventHandler(rotationTimer_Tick);
            rotationTimer.Interval = 1;
            rotationTimer.Enabled = true;
            counter = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            raceForm?.Close();
            if (cars.Count != 0)
            {
                cars.RemoveRange(0,2);
            }
            var assembly = Assembly.GetExecutingAssembly();
            var player1Type = assembly.GetTypes().First(t => t.Name == selectedCar1.Name.Remove(selectedCar1.Name.Length-1));
            var player2Type = assembly.GetTypes().First(t => t.Name == selectedCar2.Name.Remove(selectedCar2.Name.Length-1));
            Car player1Car = (Car)Activator.CreateInstance(player1Type);
            Car player2Car = (Car)Activator.CreateInstance(player2Type);
            player1Car.SetImage(comboBox4.Text);
            player2Car.SetImage(comboBox1.Text);
            player1Car.setControls(Keys.Left,Keys.Right,Keys.Up,Keys.Down);
            player2Car.setControls(Keys.A,Keys.D,Keys.W,Keys.S);
            cars.Add(player1Car);
            cars.Add(player2Car);
            this.selectedMap = new Map(cars);
            this.selectedMap.setCarsStartingPoints();
            raceForm = new RaceGame(cars,selectedMap);
            raceForm.FormClosing += new FormClosingEventHandler(RaceFormOnClosingEventhandler);
            raceForm.Show();
            this.Hide();
            raceForm.Focus();
        }

        private void RaceFormOnClosingEventhandler(object sender, FormClosingEventArgs e)
        {
            raceForm.timerGameTicks.Tick -= raceForm.timerGameTicks_Tick;
            this.Show();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {

            Default1.BorderStyle = BorderStyle.Fixed3D;
            Muscle1.BorderStyle = BorderStyle.None;
            Drifter1.BorderStyle = BorderStyle.None;
            Pickup1.BorderStyle = BorderStyle.None;
            Racer1.BorderStyle = BorderStyle.None;
            selectedCar1 = Default1;

        }

        void rotationTimer_Tick(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)

        {
            if (comboBox4.Text == comboBox1.Text)
            {
                WarningLabel.Text = "Player 1 and Player 2 can't be the same colour";
            }
            else
            {
                WarningLabel.Text = "";
            }

            List<PictureBox> player2Images = new List<PictureBox>();
            player2Images.Add(Default2);
            player2Images.Add(Muscle2);
            player2Images.Add(Drifter2);
            player2Images.Add(Pickup2);
            player2Images.Add(Racer2);

            foreach (PictureBox imgBox in player2Images)
            {
                String imageLocation = imgBox.Name.Remove(imgBox.Name.Length - 1) + ".png";
                imageLocation = (comboBox1.Text + "\\" + imageLocation);
                imageLocation = ("assets\\cars\\" + imageLocation);
                imageLocation = Path.Combine(Environment.CurrentDirectory, imageLocation);
                Image imageBitmap = new Bitmap(imageLocation);
                Size imageSize = new Size(imageBitmap.Width / 3, imageBitmap.Height / 3);
                imageBitmap = new Bitmap(imageBitmap, imageSize);
                imgBox.Image = imageBitmap;




            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Default1.BorderStyle = BorderStyle.None;
            Muscle1.BorderStyle = BorderStyle.None;
            Drifter1.BorderStyle = BorderStyle.Fixed3D;
            Pickup1.BorderStyle = BorderStyle.None;
            Racer1.BorderStyle = BorderStyle.None;
            selectedCar1 = Drifter1;

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Default2.BorderStyle = BorderStyle.None;
            Muscle2.BorderStyle = BorderStyle.None;
            Drifter2.BorderStyle = BorderStyle.None;
            Pickup2.BorderStyle = BorderStyle.None;
            Racer2.BorderStyle = BorderStyle.Fixed3D;
            selectedCar2 = Racer2;
        }



        private void pictureBox17_Click(object sender, EventArgs e)
        {
            Default2.BorderStyle = BorderStyle.Fixed3D;
            Muscle2.BorderStyle = BorderStyle.None;
            Drifter2.BorderStyle = BorderStyle.None;
            Pickup2.BorderStyle = BorderStyle.None;
            Racer2.BorderStyle = BorderStyle.None;
            selectedCar2 = Default2;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == comboBox1.Text)
            {
                WarningLabel.Text = "Player 1 and Player 2 can't be the same colour";
            }
            else
            {
                WarningLabel.Text = "";
            }
            List<PictureBox> player1Images = new List<PictureBox>();
            player1Images.Add(Default1);
            player1Images.Add(Muscle1);
            player1Images.Add(Drifter1);
            player1Images.Add(Pickup1);
            player1Images.Add(Racer1);
            foreach (PictureBox imgBox in player1Images)
            {
                String imageLocation = imgBox.Name.Remove(imgBox.Name.Length-1)+".png";
                imageLocation = (comboBox4.Text + "\\" + imageLocation);
                imageLocation = ("assets\\cars\\" + imageLocation);
                imageLocation = Path.Combine(Environment.CurrentDirectory, imageLocation);
                Image imageBitmap = new Bitmap(imageLocation);
                Size imageSize = new Size(imageBitmap.Width/3,imageBitmap.Height/3);
                imageBitmap = new Bitmap(imageBitmap,imageSize);
                imgBox.Image = imageBitmap;
            }

        }

        private void WarningLabel_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string SelectedMap = Convert.ToString(comboBox2.SelectedItem);

            String imageLocation = SelectedMap + ".png";
            imageLocation = ("assets\\" + imageLocation);
            imageLocation = Path.Combine(Environment.CurrentDirectory, imageLocation);
            Image imageBitmap = new Bitmap(imageLocation);
            Size imageSize = new Size(imageBitmap.Width/3, imageBitmap.Height/3);
            imageBitmap = new Bitmap(imageBitmap, imageSize);
            MapPreview.Image = imageBitmap;

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void Muscle1_Click(object sender, EventArgs e)
        {
            Default1.BorderStyle = BorderStyle.None;
            Muscle1.BorderStyle = BorderStyle.Fixed3D;
            Drifter1.BorderStyle = BorderStyle.None;
            Pickup1.BorderStyle = BorderStyle.None;
            Racer1.BorderStyle = BorderStyle.None;
            selectedCar1 = Muscle1;
        }

        private void Pickup1_Click(object sender, EventArgs e)
        {
            Default1.BorderStyle = BorderStyle.None;
            Muscle1.BorderStyle = BorderStyle.None;
            Drifter1.BorderStyle = BorderStyle.None;
            Pickup1.BorderStyle = BorderStyle.Fixed3D;
            Racer1.BorderStyle = BorderStyle.None;
            selectedCar1 = Pickup1;
        }

        private void Racer1_Click(object sender, EventArgs e)
        {
            Default1.BorderStyle = BorderStyle.None;
            Muscle1.BorderStyle = BorderStyle.None;
            Drifter1.BorderStyle = BorderStyle.None;
            Pickup1.BorderStyle = BorderStyle.None;
            Racer1.BorderStyle = BorderStyle.Fixed3D;
            selectedCar1 = Racer1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Muscle2_Click(object sender, EventArgs e)
        {
            Default2.BorderStyle = BorderStyle.None;
            Muscle2.BorderStyle = BorderStyle.Fixed3D;
            Drifter2.BorderStyle = BorderStyle.None;
            Pickup2.BorderStyle = BorderStyle.None;
            Racer2.BorderStyle = BorderStyle.None;
            selectedCar2 = Muscle2;
        }

        private void Drifter2_Click(object sender, EventArgs e)
        {
            Default2.BorderStyle = BorderStyle.None;
            Muscle2.BorderStyle = BorderStyle.None;
            Drifter2.BorderStyle = BorderStyle.Fixed3D;
            Pickup2.BorderStyle = BorderStyle.None;
            Racer2.BorderStyle = BorderStyle.None;
            selectedCar2 = Drifter2;
        }

        private void Pickup2_Click(object sender, EventArgs e)
        {
            Default2.BorderStyle = BorderStyle.None;
            Muscle2.BorderStyle = BorderStyle.None;
            Drifter2.BorderStyle = BorderStyle.None;
            Pickup2.BorderStyle = BorderStyle.Fixed3D;
            Racer2.BorderStyle = BorderStyle.None;
            selectedCar2 = Pickup2;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
