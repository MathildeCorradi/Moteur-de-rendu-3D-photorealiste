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
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Screen.Init(pictureBox1.Width, pictureBox1.Height);
        }

        public bool Checked()               { return checkBox1.Checked;   }
        public void PictureBoxInvalidate()  { pictureBox1.Invalidate(); }
        public void PictureBoxRefresh()     { pictureBox1.Refresh();    }

        private void ButtonClick(object sender, EventArgs e)
        {
            Screen.RefreshScreen(new MyColor(0,0,0));
            ProjetEleve.Display();
            Screen.Show();          
        }
    }
}
