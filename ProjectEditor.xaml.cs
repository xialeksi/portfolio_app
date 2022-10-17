﻿using Microsoft.Win32;
using System;
using System.IO;
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
using System.Collections;

namespace PortfolioApp
{
    /// <summary>
    /// Interaction logic for ProjectEditor.xaml
    /// </summary>
    public partial class ProjectEditor : Window
    {
        string serverImagesPath = "";
        Project currentProject;
        List<ImageModel> currentImages;
        bool changes = false;
        public ProjectEditor()
        {
            InitializeComponent();
            currentProject = new Project();
            currentImages = new List<ImageModel>();
            changes = false;

            //setting the path for where images are stored
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location; //getting current file location
            serverImagesPath = path.Substring(0, path.IndexOf("bin")) + "Images\\"; //cut it off after bin and add "Images\" after it
        }

        public void SetFields (Project proj, List<ImageModel> images)
        {
            currentProject = proj;
            tbPEditorName.Text = proj.name;
            tbPEditorDesc.Text = proj.description;

            foreach (ImageModel i in currentImages)
            {
                CreateThumbnail(i);
            }
            changes = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (changes)
            {
                if (MessageBox.Show("Quit without saving?", "Quit dialog", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnNewImg_Click(object sender, RoutedEventArgs e)
        {
            //file explorer window
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            string path = "";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
            }
            
            string fileName = path.Substring(path.LastIndexOf('\\')+1); //get filename without the gunk before the name of the file
            ImageModel image = new ImageModel(0, -1, fileName, path); //projectID should be assigned once we are about to save this
            //idimage isn't required, since we are only posting this
            currentImages.Add(image);
            CreateThumbnail(image);
        }

        private void CreateThumbnail(ImageModel img)
        {
            Image myImage = new Image();
            myImage.Tag = img.description;
            myImage.Width = 60;
            myImage.Height = 60;
            myImage.Margin = new Thickness(5, 5, 5, 5);

            //set the source
            var uriSource = new Uri(@"" + img.description, UriKind.Absolute);
            myImage.Source = new BitmapImage(uriSource);

            spEditorThumbs.Children.Add(myImage);
            myImage.AddHandler(MouseUpEvent, new MouseButtonEventHandler(Image_MouseUp));
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ImagePopup ip = new ImagePopup();
            Image im = sender as Image;
            ip.SetSource(im.Tag.ToString(),1);
            ip.Show();
        }

        private void btnSaveProject_Click(object sender, RoutedEventArgs e)
        {
            //saving project
            currentProject.idproject = 0;
            currentProject.name = tbPEditorName.Text; //"Posting from client";//
            currentProject.description = tbPEditorDesc.Text; //"This project was posted through the WPF client. If this exists, it means the POST request was successful.";//
            if (WebRequestHandler.GetAllProjects().FindIndex(proj => proj.name == currentProject.name) > -1)
            {
                MessageBox.Show("Project with that name already exists.");


            }
            else
            {
                WebRequestHandler.PostSingleProject(currentProject);

                currentProject.idproject = WebRequestHandler.GetAllProjects().Single(proj => proj.name == currentProject.name).idproject;
                MessageBox.Show("" + currentProject.idproject);

                foreach (ImageModel im in currentImages)
                {
                    im.idproject = currentProject.idproject; //update currentImages idproject,
                    MessageBox.Show(""+currentProject.idproject +" "+ im.idproject);
                    //copy each image to "server" individually if needed
                    if(!File.Exists(serverImagesPath + im.filename))
                    {
                        File.Copy(im.description, serverImagesPath + im.filename);
                    }
                    //save to db
                    WebRequestHandler.PostSingleImage(im);
                }

                MessageBox.Show("Project saved.");
                changes = false;
            }
        }

        private void tbPEditorDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            changes = true;
        }

        private void tbPEditorName_TextChanged(object sender, TextChangedEventArgs e)
        {
            changes = true;
        }
    }
}
