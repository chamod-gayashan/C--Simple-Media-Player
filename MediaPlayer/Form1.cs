using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] paths, files;
        int Startindex = 0;
        string[] FileName, FilePath;
        Boolean playnext = false;

        bool _playing = false;

        public bool isplaying 
        {
            get 
            {
                return _playing;
            }
            set
            {
               _playing = value;
                if (_playing)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.pause(); actionbtn.BackgroundImage = play.BackgroundImage;
                }
                else
                {

                    axWindowsMediaPlayer1.Ctlcontrols.play(); actionbtn.BackgroundImage = pause.BackgroundImage;
                }
            }
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
         listBox1.BringToFront();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSlider1_ValueChanged(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = bunifuSlider1.Value;
            bunifuCustomLabel4.Text = bunifuSlider1.Value.ToString();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
           
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnmaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else 
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            if(Startindex >0)
            {
                Startindex = Startindex - 1;
            }
            playfile(Startindex);
        }

        public EventHandler onAction = null;
        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            isplaying = !isplaying;
            if (onAction != null)
            {
                onAction.Invoke(this, e);
            }
        }

        private void bunifuFlatButton14_Click(object sender, EventArgs e)
        {
            if (Startindex == listBox1.Items.Count - 1)
            {
                Startindex = listBox1.Items.Count - 1;
            }
            else if(Startindex <listBox1.Items.Count)
            {
                Startindex = Startindex + 1;
            }
            playfile(Startindex);
        }

        private void bunifuFlatButton15_Click(object sender, EventArgs e)
        {
            StopPlayer();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Startindex = listBox1.SelectedIndex;
            playfile(Startindex);
            bunifuCustomLabel3.Text = listBox1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Startindex = 0;
            playnext = false;
            StopPlayer();
        }
        public void StopPlayer()
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        public void playfile(int playlistindex) 
        {
            if (listBox1.Items.Count <=0)
            {
                return;
            }
            if (playlistindex < 0)
            {
                return;
            }
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.URL = FilePath[playlistindex];
            axWindowsMediaPlayer1.Ctlcontrols.next();
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void pause_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void play_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bunifuCustomLabel1.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            bunifuCustomLabel2.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString.ToString();
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                bunifuProgressBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            }
        }

        private void axWindowsMediaPlayer1_PlaylistChange(object sender, AxWMPLib._WMPOCXEvents_PlaylistChangeEvent e)
        {
           
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                bunifuProgressBar1.MaximumValue = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                timer1.Start();
            }
            else if (axWindowsMediaPlayer1.playState==WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                timer1.Stop();
                bunifuProgressBar1.Value = 0;
            }
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            Startindex = 0;
            playnext = false;
            OpenFileDialog opnFileDlg = new OpenFileDialog();
            opnFileDlg.Multiselect = true;
            opnFileDlg.Filter="(mp3,wav,mp4,mov,wmv,mpg,avi,3gp,flv)|*.mp3;*.wav;*.mp4;*.3gp;*.avi;*.flv;*.wmv;*.mpg|all files|*.*";
            if (opnFileDlg.ShowDialog() == DialogResult.OK)
            {
                FileName = opnFileDlg.SafeFileNames;
                FilePath = opnFileDlg.FileNames;
                for (int i = 0; i <= FileName.Length -1; i++)
                {
                    listBox1.Items.Add(FileName[i]);
                }
                Startindex = 0;
                playfile(0);
            }
        }

        private void bunifuFlatButton4_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.BringToFront();
        }
    }
}
