using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plants_vs_Zombie
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer("C:\\Users\\Fryzs\\source\\repos\\Plants vs Zombie\\Plants vs Zombie\\Lose.wav");
            player.Play();

        }
            private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
