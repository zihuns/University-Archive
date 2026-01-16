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
    public partial class editMap : Form
    {
        public delegate void mapEdit(int mapData);
        public event mapEdit dataSendEvent;

        public editMap()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.dataSendEvent(0);
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.dataSendEvent(2);
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.dataSendEvent(4);
            this.Close();
        }
    }
}
