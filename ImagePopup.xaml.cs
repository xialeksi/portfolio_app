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
using System.Windows.Shapes;

namespace PortfolioApp
{
    /// <summary>
    /// Interaction logic for ImagePopup.xaml
    /// </summary>
    public partial class ImagePopup : Window
    {
        public ImagePopup()
        {
            InitializeComponent();
        }
        public void SetSource(string fileName)
        {
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = path.Substring(0,path.IndexOf("bin")) + "Images\\" + fileName;
            // MessageBox.Show(path);
            var uriSource = new Uri(@""+path, UriKind.Absolute);
            ImageFull.Source = new BitmapImage(uriSource);
            ImgPopMain.Width = new BitmapImage(uriSource).PixelWidth;
            ImgPopMain.Height = new BitmapImage(uriSource).PixelHeight;
        }

        private void ImageFull_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}

