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

namespace PortfolioApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Project> projectsList = new List<Project>();

        public MainWindow()
        {
            InitializeComponent();
            //LoadProjects();
        }

        private void LoadProjects ()
        {
            projectsList = new List<Project>(); //do backend shenaningans to insert the projects into this collection:
            foreach (Project p in projectsList)
            {
                CBoxProjectsList.Items.Add(new ComboBoxItem().Content=p.name);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //new
            //this is just placeholder
            //image names: kanamorisa.png, nazgul drinking coffee.png, puzzlegremlin.png, venger.jpg
            ImagePopup ip = new ImagePopup();
            ip.SetSource("puzzlegremlin.png");
            ip.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //view
            ProjectViewer viewer = new ProjectViewer();
            viewer.SetFields(projectsList[CBoxProjectsList.SelectedIndex]);
            viewer.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //delete
            if (MessageBox.Show("Delete project" + "__projectsList[CBoxProjectsList.SelectedIndex].name__" +"?", "Deleting project", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                MessageBox.Show("Whew, dodged a bullet!");
            }
            else
            {
                MessageBox.Show("Okay, deleting...");
            }
        }
    }
}
