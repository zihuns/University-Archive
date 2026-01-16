using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm_GUI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public delegate void mapInitDataHandler(int sizeX, int sizeY);
        public event mapInitDataHandler dataSendEvent;

        private void Button1_Click(object sender, EventArgs e)
        {
            int sizeX = (int)numericUpDown1.Value;
            int sizeY = (int)numericUpDown2.Value;

            this.dataSendEvent(sizeX, sizeY);
            this.Close();
        }
    }
}
