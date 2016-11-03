using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using RaceGame2.Lib;

namespace RaceGame2
{
    public class EndScreen : Form
    {
        private Button button1;
        private Label Winner;
        private Car winner;

        public EndScreen(Car winner)
        {
            InitializeComponent();
            this.winner = winner;
            Winner.Text = "Player " + winner.player + " wins!!";
        }

        private void InitializeComponent()
        {
            this.Winner = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Winner
            // 
            this.Winner.BackColor = System.Drawing.Color.Transparent;
            this.Winner.Font = new System.Drawing.Font("Microsoft Sans Serif", 90F);
            this.Winner.Location = new System.Drawing.Point(12, 9);
            this.Winner.Name = "Winner";
            this.Winner.Size = new System.Drawing.Size(984, 205);
            this.Winner.TabIndex = 0;
            this.Winner.Text = "Player 0 Wins!!";
            this.Winner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(379, 623);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 65);
            this.button1.TabIndex = 1;
            this.button1.Text = "To main screen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EndScreen
            // 
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Winner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "EndScreen";
            this.Text = "Need For Sleep";
            this.Load += new System.EventHandler(this.EndScreen_Load);
            this.ResumeLayout(false);

        }

        private void EndScreen_Load(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}