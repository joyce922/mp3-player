using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        MediaPlayer müzikçalar = new MediaPlayer();
        private List<string> müzikler = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAKTAR_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
                listBox1.Items.Add(textBox1.Text);
                müzikler.Add(textBox1.Text);
            }
        }

        private void buttonSEÇ_Click(object sender, EventArgs e)
        {
            müzikçalar.Open(new Uri(listBox1.SelectedItem.ToString()));
            if (müzikçalar.NaturalDuration.HasTimeSpan == true)
            {
                trackBar1.Maximum = Convert.ToInt32(müzikçalar.NaturalDuration.TimeSpan.TotalSeconds);
            }
            trackBar1.Value = 0;
        }

        private void buttonBAŞLAT_Click(object sender, EventArgs e)
        {
            müzikçalar.Play();
            timer1.Start();
        }   

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (trackBar1.Value < trackBar1.Maximum)
            {
                trackBar1.Value += 1;
            }
        }

        private void buttonDURDUR_Click(object sender, EventArgs e)
        {
            müzikçalar.Pause();
            timer1.Stop();
        }

        private void buttonBAŞASAR_Click(object sender, EventArgs e)
        {
            müzikçalar.Stop();
            trackBar1.Value = 0;
            timer1.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            müzikçalar.Position = new TimeSpan(0,0,trackBar1.Value);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            müzikçalar.Volume = Convert.ToDouble(trackBar2.Value)/100;
        }

        private void buttonİLERİ_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != müzikler.Count-1)
            {
                müzikçalar.Open(new Uri(müzikler[listBox1.SelectedIndex + 1].ToString()));
                if (müzikçalar.NaturalDuration.HasTimeSpan == true)
                {
                    trackBar1.Maximum = Convert.ToInt32(müzikçalar.NaturalDuration.TimeSpan.TotalSeconds);
                }
                trackBar1.Value = 0;
                müzikçalar.Play();
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
            }

            else
            {
                müzikçalar.Open(new Uri(müzikler[0].ToString()));
                if (müzikçalar.NaturalDuration.HasTimeSpan == true)
                {
                    trackBar1.Maximum = Convert.ToInt32(müzikçalar.NaturalDuration.TimeSpan.TotalSeconds);
                }
                trackBar1.Value = 0;
                müzikçalar.Play();
                listBox1.SelectedIndex = 0;
            }
        }

        private void buttonGERİ_Click(object sender, EventArgs e)
        {
            if ((listBox1.SelectedIndex-1) != -1)
            {
                müzikçalar.Open(new Uri(müzikler[listBox1.SelectedIndex - 1].ToString()));
                if (müzikçalar.NaturalDuration.HasTimeSpan == true)
                {
                    trackBar1.Maximum = Convert.ToInt32(müzikçalar.NaturalDuration.TimeSpan.TotalSeconds);
                }
                trackBar1.Value = 0;
                müzikçalar.Play();
                listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
            }

            else
            {
                MessageBox.Show("Önceki şarkı bulunamadı.");
                müzikçalar.Pause();
                timer1.Stop();
            }
        }
    }
}
