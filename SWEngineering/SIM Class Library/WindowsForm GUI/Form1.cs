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
    public partial class Form1 : Form
    {
        private int sizeX, sizeY;
        private int[,] map;
        private int phaze = 0;
        private int x, y, target;
        private int posX, posY, direction;

        private GUI.Interface SIMInterface;

        
        // 목표지점 선택 완료 버튼
        private void Button2_Click(object sender, EventArgs e)
        {
            phaze = 1;
            this.label5.Visible = false;
            this.button2.Visible = false;
            this.button3.Visible = true;

            buttonArr[posX, posY].Text = "^";
        }

        // 이동 버튼
        private void Button3_Click(object sender, EventArgs e)
        {
            buttonArr[posX, posY].Text = "";
            SIMInterface.doNext();
            for (int i = 0; i < sizeX; i++)
            {
                for(int j = 0; j < sizeY; j++)
                {
                    if (map[i, j] == 1)
                        continue;
                    map[i, j] = SIMInterface.readMap(i, j);

                        switch (map[i,j])
                    {
                        case 0:
                            buttonArr[i, j].Text = "";
                            break;
                        case 1:
                            buttonArr[i, j].Text = "☆";
                            break;
                        case 2:
                            buttonArr[i, j].Text = "X";
                            break;
                        case 3:
                            buttonArr[i, j].Text = "!";
                            break;
                        case 4:
                            buttonArr[i, j].Text = "?";
                            break;
                    }
                }
            }

            this.posX = SIMInterface.getPosX();
            this.posY = SIMInterface.getPosY();
            this.direction = SIMInterface.getDirection();

            switch(direction)
            {
                case 1:
                    buttonArr[posX, posY].Text = "^";
                    break;
                case 2:
                    buttonArr[posX, posY].Text = ">";
                    break;
                case 3:
                    buttonArr[posX, posY].Text = "<";
                    break;
                case 4:
                    buttonArr[posX, posY].Text = "v";
                    break;
            }

            if (SIMInterface.getIsDone() == 1)
            {
                finish();
            }
            
        }

        // 지도 생성 버튼
        private void Button1_Click(object sender, EventArgs e)
        {
            this.initMap();
            button1.Visible = false;
        }

        private void ButtonArr_Click(object sender, EventArgs e)
        {
            ButtonArray button = (ButtonArray)sender;
            this.x = button.getX();
            this.y = button.getY();

            SIMInterface.selectMap(this.x, this.y);

            if (phaze == 1)
            {
                this.editButton();
                switch (target)
                {
                    case 0:
                        button.Text = "";
                        SIMInterface.editMap(0);
                        break;
                    case 2:
                        button.Text = "X";
                        SIMInterface.editMap(2);
                        break;
                    case 4:
                        button.Text = "?";
                        SIMInterface.editMap(3);
                        break;
                }
            }
            else if (phaze == 0)
            {
                button.Text = "☆";
                SIMInterface.editMap(1);
                map[this.x, this.y] = 1;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

    }

    // 지도가 될 버튼의 2차원 배열
    public partial class ButtonArray : Button
    {
        private int x, y;

        public void setXY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
    }
}
