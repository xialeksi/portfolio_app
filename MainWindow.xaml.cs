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
using System.Windows.Markup;

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

        private void LoadProjects()
        {
            //WebRequestHandler.PostSingleProject();
            projectsList = new List<Project>(); //clear projectsList
            projectsList = WebRequestHandler.GetAllProjects(); //webrequest to get all projects

            //inserting into UI
            CBoxProjectsList.Items.Clear();
            foreach (Project p in projectsList)
            {
                CBoxProjectsList.Items.Add(new ComboBoxItem().Content = p.name);
            }
            CBoxProjectsList.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //new
            ProjectEditor projectEditor = new ProjectEditor();
            projectEditor.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //view
            //MessageBox.Show(CBoxProjectsList.Items[CBoxProjectsList.SelectedIndex].ToString());
            ProjectViewer viewer = new ProjectViewer();
            Project p = projectsList.Single(proj => proj.name == CBoxProjectsList.Items[CBoxProjectsList.SelectedIndex].ToString()); //get the correct project from list
            //list of images from the project
            List<ImageModel> imgList = WebRequestHandler.GetAllImages();
            imgList.RemoveAll(item => item.idproject != p.idproject);
            viewer.SetFields(p, imgList);
            viewer.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //delete
            if (MessageBox.Show("Delete project \"" + CBoxProjectsList.Items[CBoxProjectsList.SelectedIndex].ToString() + "\"?", "Deleting project", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                
            }
            else
            {
                //delete this from backend method
                Project p = projectsList.Single(proj => proj.name == CBoxProjectsList.Items[CBoxProjectsList.SelectedIndex].ToString());
                WebRequestHandler.DeleteSingleProject(p.idproject);
                LoadProjects();
                MessageBox.Show("Project deleted.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //refresh
            LoadProjects();
        }
    }
}
