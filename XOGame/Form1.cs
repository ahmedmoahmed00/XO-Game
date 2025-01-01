using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XOGame.Properties;

namespace XOGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum enPlayer 
        {
            Player1,
            Player2,
        }

        enum enWinner 
        {
            Player1,
            Player2,
            Draw
        }

        struct stGameStatus
        {
            public int PlayCount;
            public enWinner Winner;
            public bool GameOver;
        }

        enPlayer PlayerTurn;
        stGameStatus GameStatus;
        private void Form1_Load(object sender, EventArgs e)
        {
            button10.Cursor = Cursors.Hand;
        }

        void EndGame() 
        {
            switch (GameStatus.Winner) 
            {
                case enWinner.Player1:
                {
                    lblhowWinner.Text = "Player 1";
                    MessageBox.Show("Player 1 wins", "Player 1 wins", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                case enWinner.Player2:
                {
                    lblhowWinner.Text = "Player 2";
                    MessageBox.Show("Player 2 wins", "Player 2 wins", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                default:
                {
                    lblhowWinner.Text = "Draw";
                    MessageBox.Show("Draw Game", "Draw Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }
                AllButtonEnabled(false);
        }

        private void AllButtonEnabled(bool Enabled = true) 
        {
            if (Enabled)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;

            }
        }

        bool CheckValues (Button btn1 , Button btn2 , Button btn3) 
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString()) 
            {
                btn1.BackColor = Color.Green;
                btn2.BackColor = Color.Green;
                btn3.BackColor = Color.Green;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    lblhowWinner.Text = "Player 1";
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    lblhowWinner.Text = "Player 1";
                    EndGame();
                    return true;
                }
            }

               GameStatus.GameOver = false;

            return false;
        }
        void CheckWinner()
        {
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return;

            if (CheckValues(button7, button8, button9))
                return;

            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;

            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button3, button5, button7))
                return;


            if (GameStatus.PlayCount == 9 && !GameStatus.GameOver)
            {
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
        }


        void ChangeImage(Button btn) 
        {
            if (btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn) 
                {
                case enPlayer.Player1:
                        btn.BackgroundImage = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        lblPlayer.Text = "Player 2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        CheckWinner();
                        break;
                case enPlayer.Player2:
                        btn.BackgroundImage = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lblPlayer.Text = "Player 1";
                        GameStatus.PlayCount++;
                        btn.Tag = "O";
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Wrong Error" ,"Wrong Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            Color White = Color.FromArgb(255, 255, 255, 255);
            Pen WhitePen = new Pen(White);
            WhitePen.Width = 15;

            WhitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            WhitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(WhitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(WhitePen, 400, 460, 1050, 460);

            e.Graphics.DrawLine(WhitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(WhitePen, 840, 140, 840, 620);


        }

        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button) sender);
        }

        void ReseteButton(Button btn) 
        {
            btn.BackgroundImage = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
            AllButtonEnabled(true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ReseteButton(button1);
            ReseteButton(button2);
            ReseteButton(button3);
            ReseteButton(button4);
            ReseteButton(button5);
            ReseteButton(button6);
            ReseteButton(button7);
            ReseteButton(button8);
            ReseteButton(button9);

            PlayerTurn = enPlayer.Player1;

            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.Player1;
            lblhowWinner.Text = "in Progress";
        }
    }
}
