using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PortfolioApp
{
    /// <summary>
    /// Interaction logic for ProjectViewer.xaml
    /// </summary>
    public partial class ProjectViewer : Window
    {
        List<ImageModel> currentImages;
        Project currentProject;

        public ProjectViewer()
        {
            InitializeComponent();
        }

        public void SetFields (Project proj, List<ImageModel> images)
        {
            currentProject = proj;
            tbPViewerName.Text = proj.name;
            tbPViewerDesc.Text = proj.description;

            currentImages = new List<ImageModel>();
            currentImages = images;
            foreach (ImageModel i in currentImages)
            {
                //string imgName = i.filename;
                CreateThumbnail(i);
            }
        }

        private void CreateThumbnail (ImageModel img)
        {
            // <Image x:Name="thumbnail" Width="60" Height="60" Margin="5 5 5 5"/>
            Image myImage = new Image();
            myImage.Tag = img.filename;
            myImage.Width = 60; 
            myImage.Height = 60;
            myImage.Margin = new Thickness(5, 5, 5, 5);
            

            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = path.Substring(0, path.IndexOf("bin")) + "Images\\" + img.filename;
            // MessageBox.Show(path);
            var uriSource = new Uri(@"" + path, UriKind.Absolute);
            myImage.Source = new BitmapImage(uriSource);

            spThumbs.Children.Add(myImage);
            myImage.AddHandler(MouseUpEvent, new MouseButtonEventHandler(Image_MouseUp));
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image im = sender as Image;
            ImagePopup ip = new ImagePopup();
            ip.SetSource(im.Tag.ToString());
            ip.Show();
            // MessageBox.Show("Clicked!");
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ProjectEditor projectEditor = new ProjectEditor();
            projectEditor.SetFields(currentProject,currentImages);
            Close();
            projectEditor.Show();
        }
    }
}
