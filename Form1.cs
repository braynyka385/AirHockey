using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirHockey
{
    public partial class Form1 : Form
    {
        int paddle1X = 458/2;
        int paddle1Y = 570;
        int player1Score = 0;

        int paddle2X = 458/2;
        int paddle2Y = 100;
        int player2Score = 0;

        int prev1Score = 0;
        int prev2Score = 0;

        int paddleWidth = 40;
        int paddleHeight = 40;
        int paddleSpeed = 4;

        int ballX = 458/2;
        int ballY = 670/2;

        int lastX = 458/2;
        int lastY = 670/2;

        float ballXSpeed = 0;
        float ballYSpeed = 0;
        int ballWidth = 10;
        int ballHeight = 10;

        bool[] pressedKeys = new bool[10];

        float accel = 1.25f;
        float defAccel = 1.25f;
        float decel = 0.985f;

        byte maxSpeed = 25;

        Random random = new Random();

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush redBrush = new SolidBrush(Color.Crimson);
        Pen bluePen = new Pen(Color.Blue, 2);
        Pen blackPen = new Pen(Color.Black, 2);
        Pen redPen = new Pen(Color.Crimson, 4);
        Font screenFont = new Font("Consolas", 12);
        Font goalFont = new Font("Consolas", 32);

        int counter = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    pressedKeys[0] = true;
                    break;
                case Keys.S:
                    pressedKeys[1] = true;
                    break;
                case Keys.A:
                    pressedKeys[2] = true;
                    break;
                case Keys.D:
                    pressedKeys[3] = true;
                    break;
                case Keys.Up:
                    pressedKeys[4] = true;
                    break;
                case Keys.Down:
                    pressedKeys[5] = true;
                    break;
                case Keys.Left:
                    pressedKeys[6] = true;
                    break;
                case Keys.Right:
                    pressedKeys[7] = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    pressedKeys[0] = false;
                    break;
                case Keys.S:
                    pressedKeys[1] = false;
                    break;
                case Keys.A:
                    pressedKeys[2] = false;
                    break;
                case Keys.D:
                    pressedKeys[3] = false;
                    break;
                case Keys.Up:
                    pressedKeys[4] = false;
                    break;
                case Keys.Down:
                    pressedKeys[5] = false;
                    break;
                case Keys.Left:
                    pressedKeys[6] = false;
                    break;
                case Keys.Right:
                    pressedKeys[7] = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // 5% chance of super strong hit
            if (random.Next(1, 100) > 95)
            {
                accel = 2;
            }
            if (ballXSpeed <= maxSpeed && ballYSpeed <= maxSpeed)
            {
                accel = 1;
            }
            //move ball
            ballX += Convert.ToInt32(ballXSpeed);
            ballY += Convert.ToInt32(ballYSpeed);


            //Slow down ball over time

            if (ballXSpeed != 0)
            {
                ballXSpeed *= decel;
            }
            if (ballYSpeed != 0)
            {
                ballYSpeed *= decel;
            }



            //wall collisions
            if (ballX > this.Width - ballWidth)
            {
                ballXSpeed *= -1;
                ballX = this.Width - ballWidth - 1;
            }

            else if (ballX < 0)
            {
                ballXSpeed *= -1;
                ballX = 1;
            }

            if (ballY <= 0 && (ballX >= this.Width / 2 - 80 && ballX <= this.Width / 2 + 80))
            {
                player1Score++;
                ballX = this.Width / 2;
                ballY = this.Height / 2;

                ballXSpeed = 0;
                ballYSpeed = 0;

                paddle1X = this.Width / 2;
                paddle2X = this.Width / 2;

                paddle1Y = this.Height - 100;
                paddle2Y = 0 + 100;
            }
            else if (ballY <= 0)
            {
                ballYSpeed *= -1;
                ballY = 1;
            }
            if (ballY >= this.Height - ballHeight && (ballX >= this.Width / 2 - 80 && ballX <= this.Width / 2 + 80))
            {
                player2Score++;
                ballX = this.Width / 2;
                ballY = this.Height / 2;

                ballXSpeed = 0;
                ballYSpeed = 0;

                paddle1X = this.Width / 2;
                paddle2X = this.Width / 2;

                paddle1Y = this.Height - 100;
                paddle2Y = 0 + 100;
            }
            else if (ballY >= this.Height)
            {
                ballYSpeed *= -1;
                ballY = this.Height - 1;
            }

            //move player 1
            if (pressedKeys[0] == true && paddle1Y > this.Height/2)
            {
                paddle1Y -= paddleSpeed;
            }

            if (pressedKeys[1] == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }

            if (pressedKeys[2] == true && paddle1X > 0) // && paddle1X > this.Width + paddleWidth
            {
                paddle1X -= paddleSpeed;
            }

            if (pressedKeys[3] == true && paddle1X < this.Width - paddleWidth)
            {
                paddle1X += paddleSpeed;
            }

            //move player 2
            if (pressedKeys[4] == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }

            if (pressedKeys[5] == true && paddle2Y < this.Height / 2 - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }
            if (pressedKeys[6] == true && paddle2X > 0) // && paddle1X > this.Width + paddleWidth
            {
                paddle2X -= paddleSpeed;
            }

            if (pressedKeys[7] == true && paddle2X < this.Width - paddleWidth)
            {
                paddle2X += paddleSpeed;
            }

            //create Rectangles of objects on screen to be used for collision detection
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);


            if (player1Rec.IntersectsWith(ballRec))
            {
                if (ballXSpeed <= 0.01f && ballYSpeed <= 0.01f)
                {
                    if (pressedKeys[0] == true)
                    {
                        if (pressedKeys[2] == true)
                        {
                            ballXSpeed = -12;
                            ballYSpeed = -12;
                        }
                        else if (pressedKeys[3] == true)
                        {
                            ballXSpeed = 12;
                            ballYSpeed = -12;
                        }
                        else
                        {
                            ballYSpeed = -12;
                        }
                    }
                    else if (pressedKeys[1] == true)
                    {
                        if (pressedKeys[2] == true)
                        {
                            ballXSpeed = -12;
                            ballYSpeed = 12;
                        }
                        else if (pressedKeys[3] == true)
                        {
                            ballXSpeed = 12;
                            ballYSpeed = 12;
                        }
                        else
                        {
                            ballYSpeed = 12;
                        }
                    }

                    else if (pressedKeys[2] == true)
                    {
                        ballXSpeed = -12;
                    }
                    else if (pressedKeys[3] == true)
                    {
                        ballXSpeed = 12;
                    }

                    
                }

                if (pressedKeys[0] == true)
                {
                    if (pressedKeys[2] == true)
                    {
                        ballXSpeed = -12 * accel;
                        ballYSpeed = -12;
                    }
                    else if (pressedKeys[3] == true)
                    {
                        ballXSpeed = 12;
                        ballYSpeed = -12 * accel;
                    }
                    else
                    {
                        ballYSpeed = -12;
                    }
                }
                else if (pressedKeys[1] == true)
                {
                    if (pressedKeys[2] == true)
                    {
                        ballXSpeed = -12 * accel;
                        ballYSpeed = 12;
                    }
                    else if (pressedKeys[3] == true)
                    {
                        ballXSpeed = 12 * accel;
                        ballYSpeed = 12;
                    }
                    else
                    {
                        ballYSpeed = 12;
                    }
                }

                else if (pressedKeys[2] == true)
                {
                    ballXSpeed = -12;
                }
                else if (pressedKeys[3] == true)
                {
                    ballXSpeed = 12;
                }
                else
                {
                    ballXSpeed *= -1 * accel;
                    ballYSpeed *= -1 * accel;
                }
                ballX = lastX;
                ballY = lastY;
            }
            else if (player2Rec.IntersectsWith(ballRec))
            {
                if (ballXSpeed <= 0.01f && ballYSpeed <= 0.01f)
                {
                    if (pressedKeys[4] == true)
                    {
                        if (pressedKeys[6] == true)
                        {
                            ballXSpeed = -12;
                            ballYSpeed = -12;
                        }
                        else if (pressedKeys[7] == true)
                        {
                            ballXSpeed = 12;
                            ballYSpeed = -12;
                        }
                        else
                        {
                            ballYSpeed = -12;
                        }
                    }
                    else if (pressedKeys[5] == true)
                    {
                        if (pressedKeys[6] == true)
                        {
                            ballXSpeed = -12;
                            ballYSpeed = 12;
                        }
                        else if (pressedKeys[7] == true)
                        {
                            ballXSpeed = 12;
                            ballYSpeed = 12;
                        }
                        else
                        {
                            ballYSpeed = 12;
                        }
                    }

                    else if (pressedKeys[6] == true)
                    {
                        ballXSpeed = -12;
                    }
                    else if (pressedKeys[7] == true)
                    {
                        ballXSpeed = 12;
                    }

                }
                if (pressedKeys[4] == true)
                {
                    if (pressedKeys[6] == true)
                    {
                        ballXSpeed = -12 * accel;
                        ballYSpeed = -12;
                    }
                    else if (pressedKeys[7] == true)
                    {
                        ballXSpeed = 12 * accel;
                        ballYSpeed = -12;
                    }
                    else
                    {
                        ballYSpeed = -12;
                    }
                }
                else if (pressedKeys[5] == true)
                {
                    if (pressedKeys[6] == true)
                    {
                        ballXSpeed = -12 * accel;
                        ballYSpeed = 12;
                    }
                    else if (pressedKeys[7] == true)
                    {
                        ballXSpeed = 12 * accel;
                        ballYSpeed = 12;
                    }
                    else
                    {
                        ballYSpeed = 12;
                    }
                }

                else if (pressedKeys[6] == true)
                {
                    ballXSpeed = -12;
                }
                else if (pressedKeys[7] == true)
                {
                    ballXSpeed = 12;

                }
                else
                {
                    ballXSpeed *= -1 * accel;
                    ballYSpeed *= -1 * accel;
                }
                ballX = lastX;
                ballY = lastY;
            }
            accel = defAccel;
            lastX = ballX;
            lastY = ballY;
            Refresh();

            if (counter != 0)
            {
                counter++;
            }
            if (prev1Score != player1Score || prev2Score != player2Score)
            {
                counter++;
                prev1Score = player1Score;
                prev2Score = player2Score;
            }

            if (counter >= 240)
            {
                counter = 0;
            }
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle goal1 = new Rectangle(this.Width / 2 - 80, -80, 160, 160);
            Rectangle goal2 = new Rectangle(this.Width / 2 - 80, this.Height - 80, 160, 160);
            e.Graphics.DrawLine(blackPen, 0, this.Height / 2, this.Width, this.Height / 2);
            e.Graphics.DrawEllipse(redPen, goal1);
            e.Graphics.DrawEllipse(redPen, goal2);
            e.Graphics.FillRectangle(blackBrush, ballX, ballY, ballWidth, ballHeight);

            e.Graphics.FillRectangle(blueBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillRectangle(greenBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);

            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, this.Width / 2 - 20, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, this.Width / 2 + 20, 10);
            if (counter != 0)
            {
                e.Graphics.DrawString("GOAL!!!", goalFont, redBrush, this.Width / 2, this.Height / 2);
            }
            
        }
    }
}