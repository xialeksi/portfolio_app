using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

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
            LoadProjects();
        }

        private void LoadProjects ()
        {
            //ReloadProjectsList();
            projectsList = new List<Project>(); //reset projectsList
            //do backend shenaningans to insert the projects into this collection: //temp fix:
            projectsList.Add(new Project(1, "First Project", "This is an example placeholder project. #1"));
            projectsList.Add(new Project(1, "Second Project", "This is an example placeholder project. #2"));
            projectsList.Add(new Project(1, "Third Project", "This is an example placeholder project. #3"));


            //inserting into UI
            CBoxProjectsList.Items.Clear();
            foreach (Project p in projectsList)
            {
                CBoxProjectsList.Items.Add(new ComboBoxItem().Content=p.name);
            }
            CBoxProjectsList.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //new
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //view
            //MessageBox.Show(CBoxProjectsList.Items[CBoxProjectsList.SelectedIndex].ToString());
            ProjectViewer viewer = new ProjectViewer();
            Project p = projectsList.Single(proj => proj.name == CBoxProjectsList.Items[CBoxProjectsList.SelectedIndex].ToString()); //get the correct project from list
            //list of images from the project
            List<ImageModel> imgList = new List<ImageModel>(); //GetImagesList backend thing
            viewer.SetFields(p, imgList);
            viewer.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //delete
            if (MessageBox.Show("Delete project \"" + CBoxProjectsList.Items[CBoxProjectsList.SelectedIndex].ToString() + "\"?", "Deleting project", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //MessageBox.Show("Whew, dodged a bullet!");
            }
            else
            {
                //delete this from backend method
                LoadProjects();
                MessageBox.Show("Project deleted.");
            }
        }
    }
}
