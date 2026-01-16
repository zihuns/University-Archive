namespace WindowsForm_GUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "지도 생성";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gulim", 12F);
            this.label1.Location = new System.Drawing.Point(17, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "가로 크기";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gulim", 12F);
            this.label2.Location = new System.Drawing.Point(100, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "sizeX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gulim", 12F);
            this.label3.Location = new System.Drawing.Point(17, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "세로 크기";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gulim", 12F);
            this.label4.Location = new System.Drawing.Point(100, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "sizeY";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Gulim", 12F);
            this.label5.Location = new System.Drawing.Point(405, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "목표 지점을 선택하세요";
            this.label5.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(452, 74);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "선택 완료";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(210, 25);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 72);
            this.button3.TabIndex = 7;
            this.button3.Text = "이동";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 620);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public void MapdataRecieved(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            label2.Text = this.sizeX.ToString();
            label4.Text = this.sizeY.ToString();

            map = new int[sizeX, sizeY];

            SIMInterface = new GUI.Interface();
            SIMInterface.setMapSize(sizeX, sizeY);

            this.posX = sizeX - 1;
            this.posY = 0;
            this.direction = 1;
            SIMInterface.initialize(this.posX, this.posY, this.direction);

            buttonArr = new ButtonArray[sizeX, sizeY];

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    buttonArr[i, j] = new ButtonArray();
                    buttonArr[i, j].setXY(i, j);
                    this.Controls.Add(this.buttonArr[i, j]);
                    buttonArr[i, j].Location = new System.Drawing.Point(20 + (40 * j), 200 + (40 * i));
                    buttonArr[i, j].Name = "buttonArr[" + i.ToString() + "," + j.ToString() + "]";
                    buttonArr[i, j].Size = new System.Drawing.Size(30, 30);
                    buttonArr[i, j].TabIndex = 5;
                    buttonArr[i, j].UseVisualStyleBackColor = true;
                    buttonArr[i, j].Visible = true;
                    this.buttonArr[i, j].Click += new System.EventHandler(this.ButtonArr_Click);

                }
            }

            this.label5.Visible = true;
            this.button2.Visible = true;
        }

        public void initMap()
        {
            //
            // show Form2 at initialize
            //
            Form2 startForm = new Form2();

            startForm.dataSendEvent += MapdataRecieved;

            startForm.ShowDialog();
        }

        public void editButton()
        {
            editMap editButtonForm = new editMap();

            editButtonForm.dataSendEvent += buttonDataRecieved;

            editButtonForm.ShowDialog();
        }

        public void buttonDataRecieved(int mapData)
        {
            this.target = mapData;
        }

        public void finish()
        {
            Form3 finishForm = new Form3();

            finishForm.ShowDialog();

            this.Close();
        }

        #endregion
        private ButtonArray[,] buttonArr;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

