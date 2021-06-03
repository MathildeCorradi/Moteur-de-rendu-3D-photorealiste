using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Projet_IMA
{
    public partial class Form1 : Form
    {
        private bool _fast;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Screen.Init(pictureBox1.Width, pictureBox1.Height);
        }

        public bool FastMode() => _fast;

        public void PictureBoxInvalidate() => pictureBox1.Invalidate();

        public void PictureBoxRefresh() => pictureBox1.Refresh();

        private void ButtonFastClick(object sender, EventArgs e)
        {
            _fast = true;
            Screen.RefreshScreen(new MyColor(0, 0, 0));
            ProjetEleve.Display();
            Screen.Show();
        }

        private void ButtonSlowClick(object sender, EventArgs e)
        {
            _fast = false;
            Screen.RefreshScreen(new MyColor(0, 0, 0));
            ProjetEleve.Display();
            Screen.Show();
        }
    }
}
