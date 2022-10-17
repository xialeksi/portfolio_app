using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Diagnostics;

namespace PortfolioApp
{
    internal class WebRequestHandler
    {
        private WebRequestHandler() { }
        private static WebRequestHandler instance = null;
        public static WebRequestHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WebRequestHandler();
                }
                return instance;
            }
        }

        //Get Projects
        public static List<Project> GetAllProjects ()
        {
            List<Project> projectsList = new List<Project>();
            var data = Task.Run(() => GETRequest("Project"));
            data.Wait();
            //MessageBox.Show(data.Result.ToString());
            if (data.Result.Length > 0)
            {
                //JObject j = JObject.Parse(data.Result);
                //MessageBox.Show(j["idproject"].ToString() + " " + j["name"].ToString() + " " + j["description"].ToString());
                JArray JArr = JArray.Parse(data.Result);
                foreach (var item in JArr)
                {
                    projectsList.Add(new Project(int.Parse(item["idproject"].ToString()), item["name"].ToString(), item["description"].ToString()));
                }
            }
            return projectsList;
        }
        public static Project GetSingleProject(int id)
        {
            Project p = new Project();
            var data = Task.Run(() => GETRequest("Project/" + id.ToString()));
            data.Wait();
            //MessageBox.Show(data.Result.ToString());
            if (data.Result.Length > 0)
            {
                JObject j = JObject.Parse(data.Result);
                p.idproject = int.Parse(j["idproject"].ToString());
                p.name = j["name"].ToString(); 
                p.description = j["description"].ToString();
            }
            return p;
        }

        //Get Images
        public static List<ImageModel> GetAllImages()
        {
            List<ImageModel> imageList = new List<ImageModel>();
            var data = Task.Run(() => GETRequest("Image"));
            data.Wait();
            //MessageBox.Show(data.Result.ToString());
            if (data.Result.Length > 0)
            {
                JArray JArr = JArray.Parse(data.Result);
                foreach (var item in JArr)
                {
                    imageList.Add(new ImageModel(int.Parse(item["idimage"].ToString()), int.Parse(item["idproject"].ToString()), item["name"].ToString(), item["description"].ToString()));
                }
            }
            return imageList;
        }
        public static ImageModel GetSingleImage(int id)
        {
            ImageModel i = new ImageModel();
            var data = Task.Run(() => GETRequest("Image/" + id.ToString()));
            data.Wait();
            //MessageBox.Show(data.Result.ToString());
            if (data.Result.Length > 0)
            {
                JObject j = JObject.Parse(data.Result);
                i.idimage = int.Parse(j["idimage"].ToString());
                i.idproject = int.Parse(j["idproject"].ToString());
                i.filename = j["filename"].ToString();
                i.description = j["description"].ToString();
            }
            return i;
        }

        static async Task<string> GETRequest(string command)
        {
            var response = string.Empty;
            var url = MyEnvironment.GetBaseUrl() + command;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("None");

            HttpResponseMessage result = await client.GetAsync(url);
            response = await result.Content.ReadAsStringAsync();
            //Console.WriteLine(response);
            return response;
        }



        public static Project PostSingleProject(Project proj)
        {
            var data = Task.Run(() => PostRequest(proj));
            data.Wait();
            return proj;
        }

        static async Task<string> PostRequest(Project proj)
        {
            var response = string.Empty;

            var json = JsonConvert.SerializeObject(proj);
            //MessageBox.Show(json.ToString());
            var postData = new StringContent(json, Encoding.UTF8, "application/json");

            var url = MyEnvironment.GetBaseUrl() + "Project";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("None");
            HttpResponseMessage result = await client.PostAsync(url, postData);
            response = await result.Content.ReadAsStringAsync();
            return response;
        }

        public static ImageModel PostSingleImage(ImageModel img)
        {
            var data = Task.Run(() => PostRequest(img));
            data.Wait();
            return img;
        }
        static async Task<string> PostRequest(ImageModel img)
        {
            var response = string.Empty;

            var json = JsonConvert.SerializeObject(img);
            MessageBox.Show(json.ToString());
            var postData = new StringContent(json, Encoding.UTF8, "application/json");

            var url = MyEnvironment.GetBaseUrl() + "Image";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("None");
            HttpResponseMessage result = await client.PostAsync(url, postData);
            response = await result.Content.ReadAsStringAsync();
            return response;
        }



        public static Project DeleteSingleProject(int id)
        {
            Project p = new Project();
            var data = Task.Run(() => DeleteRequest("Project/" + id));
            data.Wait();
            //MessageBox.Show(data.Result.ToString());
            if (data.Result.Length > 0)
            {
                JObject j = JObject.Parse(data.Result);
                p.idproject = int.Parse(j["idproject"].ToString());
                p.name = j["name"].ToString();
                p.description = j["description"].ToString();
            }
            return p;
        }

        public static ImageModel DeleteSingleImage(int id)
        {
            ImageModel i = new ImageModel();
            var data = Task.Run(() => DeleteRequest("Image/" + id));
            data.Wait();
            //MessageBox.Show(data.Result.ToString());
            if (data.Result.Length > 0)
            {
                JObject j = JObject.Parse(data.Result);
                i.idimage = int.Parse(j["idimage"].ToString());
                i.idproject = int.Parse(j["idproject"].ToString());
                i.filename = j["filename"].ToString();
                i.description = j["description"].ToString();
            }
            return i;
        }

        static async Task<string> DeleteRequest(string command)
        {
            var response = string.Empty;
            var url = MyEnvironment.GetBaseUrl() + command;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("None");

            HttpResponseMessage result = await client.DeleteAsync(url);
            response = await result.Content.ReadAsStringAsync();
            //Console.WriteLine(response);
            return response;
        }
    }
}
