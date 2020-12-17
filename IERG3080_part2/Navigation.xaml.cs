using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IERG3080_part2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private ImageBrush UserImageUp, UserImageDown, UserImageLeft, UserImageRight;
        public Page1()
        {
            InitializeComponent();
            GameSetUp();
        }
        private void User_KeyDown(object sender, KeyEventArgs e)
        {
            Point MyMapCoordinate = Map.TranslatePoint(new Point(0, 0), MyCanvas);
            Point UserCoordinate = user.TranslatePoint(new Point(0, 0), Map);
            if (e.Key == Key.A)
            {
                Canvas.SetLeft(user, UserCoordinate.X - 6);
                Canvas.SetLeft(Map, MyMapCoordinate.X + 6);
                user.Fill = UserImageLeft;
            }

            if (e.Key == Key.D)
            {
                Canvas.SetLeft(user, UserCoordinate.X + 6);
                Canvas.SetLeft(Map, MyMapCoordinate.X - 6);
                user.Fill = UserImageRight;
            }

            if (e.Key == Key.W )
            {
                Canvas.SetTop(user, UserCoordinate.Y - 6);
                Canvas.SetTop(Map, MyMapCoordinate.Y + 6);
                user.Fill = UserImageUp;
            }
            if (e.Key == Key.S)
            {
                Canvas.SetTop(user, UserCoordinate.Y + 6);
                Canvas.SetTop(Map, MyMapCoordinate.Y - 6);
                user.Fill = UserImageDown;
            }

        }
            private void GameSetUp()
        {
            Canvas.SetLeft(user, Convert.ToDouble(770));
            Canvas.SetTop(user, Convert.ToDouble(569));

            UserImageUp = new ImageBrush();
            UserImageUp.ImageSource = new BitmapImage(new Uri("C:\\Users\\ths019\\Desktop\\IERG3080_part2\\IERG3080_part2\\images\\zup.png"));
            UserImageDown = new ImageBrush();
            UserImageDown.ImageSource = new BitmapImage(new Uri("C:\\Users\\ths019\\Desktop\\IERG3080_part2\\IERG3080_part2\\images\\zdown.png"));
            UserImageLeft = new ImageBrush();
            UserImageLeft.ImageSource = new BitmapImage(new Uri("C:\\Users\\ths019\\Desktop\\IERG3080_part2\\IERG3080_part2\\images\\zleft.png"));
            UserImageRight = new ImageBrush();
            UserImageRight.ImageSource = new BitmapImage(new Uri("C:\\Users\\ths019\\Desktop\\IERG3080_part2\\IERG3080_part2\\images\\zright.png"));
            user.Fill = UserImageDown;

        }
    }
}
