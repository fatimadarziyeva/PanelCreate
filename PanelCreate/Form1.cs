using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace PanelCreate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.ImageLocation = "bomba.png";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cavab = MessageBox.Show(
        "Proqramdan çıxmaq istədiyinizə əminsiniz?",
        "Çıxış",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (cavab == DialogResult.Yes)
            {
                Close();
            }
        }

        Random r = new Random();
        Random g = new Random();
        Random b = new Random();

        Random xL = new Random();
        Random yL = new Random();
        int count = 0;

        List<Panel> paneller = new List<Panel>();

        private void button1_Click(object sender, EventArgs e)
        {
            count++;
            Panel p = new Panel();
            p.Size = new Size(60, 60);
            p.BackColor = Color.FromArgb(r.Next(0, 256), g.Next(0, 256), b.Next(0, 256));
            p.Location = new Point(xL.Next(0, 636), yL.Next(0, 300));
            this.Controls.Add(p);
            paneller.Add(p); 
            label2.Text = count.ToString();

            if (p.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                OyunBitdi();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(xL.Next(0, 636), yL.Next(0, 300));

            foreach (Panel p in paneller)
            {
                if (pictureBox1.Bounds.IntersectsWith(p.Bounds))
                {
                    OyunBitdi();
                    break;
                }
            }
        }

        private void OyunBitdi()
        {
            try
            {
                SoundPlayer ses = new SoundPlayer("explosion.wav");
                ses.Play();
            }
            catch
            {
                SystemSounds.Hand.Play();
            }

            DialogResult cavab = MessageBox.Show(
                "Oyun bitdi! Xalınız: " + count + "\nYeni oyun üçün OK düyməsinə basın.",
                "Bomba partladı",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            if (cavab == DialogResult.OK)
            {
                count = 0;
                label2.Text = "0";

                foreach (Panel p in paneller)
                {
                    this.Controls.Remove(p);
                    p.Dispose();
                }
                paneller.Clear();

                pictureBox1.Location = new Point(xL.Next(0, 636), yL.Next(0, 300));
            }
        }
    }
}