using System;
using System.Collections.Generic;
using System.Linq;
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
        List<Rectangle> itemstoremove = new List<Rectangle>();
        int escore = 0, pscore = 0;

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
            //enemyScore.Content =  escore;
            //playerScore.Content =  pscore;
            Rect playerHitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            Rect enemyHitbox = new Rect(Canvas.GetLeft(enemy), Canvas.GetTop(enemy), enemy.Width, enemy.Height);
            Rect ballHitbox = new Rect(Canvas.GetLeft(ball), Canvas.GetTop(ball), ball.Width, ball.Height);
            if (moveLeft && Canvas.GetLeft(player) > 5)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - 5);
            }
            if (moveRight && Canvas.GetLeft(player) + 100 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + 5);
            }
            if(ballLeft && Canvas.GetLeft(ball) > 5)
            {
                Canvas.SetLeft(ball, Canvas.GetLeft(ball) - 5);
                if (Canvas.GetLeft(enemy) > 5)
                {
                    if (Canvas.GetLeft(ball) < Application.Current.MainWindow.Width - enemy.Width)
                    {
                        Canvas.SetLeft(enemy, Canvas.GetLeft(ball) - 5);
                    }
                   
                }
            }
            if (ballRight && Canvas.GetLeft(ball) + 30 < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(ball, Canvas.GetLeft(ball) + 5);
                if (Canvas.GetLeft(enemy) + 100 < Application.Current.MainWindow.Width)
                {
                    Canvas.SetLeft(enemy, Canvas.GetLeft(ball) + 5);
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
            if(ballDown && Canvas.GetTop(ball) + 70 < Application.Current.MainWindow.Height)
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
