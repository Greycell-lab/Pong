using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pong
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool moveLeft, moveRight, ballLeft = true, ballRight = false, ballUp = false, ballDown = true;        
        int escore = 0, pscore = 0;
        Random rand = new Random();
        int rnd;
        

        public MainWindow()
        {
            InitializeComponent();            
            DispatcherTimer gameTime = new DispatcherTimer();
            gameTime.Interval = TimeSpan.FromMilliseconds(20);
            gameTime.Tick += gameEngine;                        
            gameTime.Start();
            MyCanvas.Focus();         
        }
        void gameEngine(object sender, EventArgs e)
        {
            rnd = rand.Next(1, 5);
            enemyScore.Content =  escore;
            playerScore.Content =  pscore;
            Rect playerHitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            Rect enemyHitbox = new Rect(Canvas.GetLeft(enemy), Canvas.GetTop(enemy), enemy.Width, enemy.Height);
            Rect ballHitbox = new Rect(Canvas.GetLeft(ball), Canvas.GetTop(ball), ball.Width, ball.Height);
            if (moveLeft && Canvas.GetLeft(player) > 5)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - 4);
            }
            if (moveRight && Canvas.GetLeft(player) + 100 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + 4);
            }
            if(ballLeft && Canvas.GetLeft(ball) > 5)
            {
                Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 5);
                if (Canvas.GetLeft(enemy) > 5)
                {
                    if (Canvas.GetLeft(ball) < Application.Current.MainWindow.Width - enemy.Width)
                    {
                        Canvas.SetLeft(enemy, Canvas.GetLeft(enemy) - 4);
                    }
                   
                }
            }
            if (ballRight && Canvas.GetLeft(ball) + 30 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 5);
                if (Canvas.GetLeft(enemy) + 100 < Application.Current.MainWindow.Width)
                {
                    Canvas.SetLeft(enemy, Canvas.GetLeft(enemy) + 4);
                }
            }
            if(Canvas.GetLeft(ball) <= 5)
            {
                ballLeft = false;
                ballRight = true;
            }
            if (Canvas.GetLeft(ball) + 30 >= Application.Current.MainWindow.Width)
            {
                ballLeft = true;
                ballRight = false;
            }
            if (ballUp && Canvas.GetTop(ball) > 5)
            {
                Canvas.SetTop(ball, Canvas.GetTop(ball) - 5);
            }
            if(ballDown && Canvas.GetTop(ball) + 40 < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(ball, Canvas.GetTop(ball) + 5);
            }
            if(ballHitbox.IntersectsWith(playerHitbox))
            {
                ballDown = false;
                ballUp = true;
            }
            if(ballHitbox.IntersectsWith(enemyHitbox))
            {                
                ballDown = true;
                ballUp = false;
            }
            if(Canvas.GetTop(ball) + ball.Height > 400)
            {
                escore++;
                if(escore == 5)
                {
                    MessageBox.Show("You Loose!");
                    escore = 0;
                    pscore = 0;
                }
                MyCanvas.Children.Remove(ball);
                MyCanvas.Children.Remove(enemy);
                MyCanvas.Children.Remove(player);
                Canvas.SetLeft(ball, 165);
                Canvas.SetTop(ball, 200);                
                Canvas.SetLeft(enemy, 135);
                Canvas.SetTop(enemy, 10);
                Canvas.SetLeft(player, 135);
                Canvas.SetTop(player, 394);
                MyCanvas.Children.Add(ball);
                MyCanvas.Children.Add(enemy);
                MyCanvas.Children.Add(player);
                if(rnd == 1)
                {
                    ballLeft = true;
                    ballRight = false;
                    ballUp = true;
                    ballDown = false;
                }
                if (rnd == 2)
                {
                    ballLeft = false;
                    ballRight = true;
                    ballUp = true;
                    ballDown = false;
                }
                if (rnd == 3)
                {                  
                    ballLeft = true;
                    ballRight = false;
                    ballUp = false;
                    ballDown = true;
                }
                if (rnd == 4)
                {
                    ballLeft = false;
                    ballRight = true;
                    ballUp = false;
                    ballDown = true;
                }
               
            }
            if(Canvas.GetTop(ball) < 10)
            {
                pscore++;
                if (pscore == 5)
                {
                    MessageBox.Show("You Win!");
                    escore = 0;
                    pscore = 0;
                }
                MyCanvas.Children.Remove(ball);
                MyCanvas.Children.Remove(enemy);
                MyCanvas.Children.Remove(player);
                Canvas.SetLeft(ball, 165);
                Canvas.SetTop(ball, 200);
                Canvas.SetLeft(enemy, 135);
                Canvas.SetTop(enemy, 10);
                Canvas.SetLeft(player, 135);
                Canvas.SetTop(player, 394);
                MyCanvas.Children.Add(ball);
                MyCanvas.Children.Add(enemy);
                MyCanvas.Children.Add(player);
                if (rnd == 1)
                {
                    ballLeft = true;
                    ballRight = false;
                    ballUp = true;
                    ballDown = false;
                }
                if (rnd == 2)
                {
                    ballLeft = false;
                    ballRight = true;
                    ballUp = true;
                    ballDown = false;
                }
                if (rnd == 3)
                {
                    ballLeft = true;
                    ballRight = false;
                    ballUp = false;
                    ballDown = true;
                }
                if (rnd == 4)
                {
                    ballLeft = true;
                    ballRight = false;
                    ballUp = false;
                    ballDown = true;
                }
                if (rnd == 5)
                {
                    ballLeft = false;
                    ballRight = true;
                    ballUp = true;
                    ballDown = false;
                }
            }
            
        }
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }
            
        }
    }
}
