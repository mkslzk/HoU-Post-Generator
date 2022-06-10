using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using MessageBox = System.Windows.MessageBox;

namespace PostGenerator
{
    public partial class SteamAppId : Form
    {
        private const string PT1 = "[align=center][size=x-large][color=#00FF00][font=Times New Roman][b]";
        private const string PT2 = "[/font][/color][/b][/size]\n\n";
        private const string ImgurSizeGame = "[img=600x300]";
        private const string ImgurSizeProgram = "[img]";
        private const string PT3 = "[/img]\n\n[size=xx-large][color=#00FF00][font=Algerian][b]Beschreibung:[/b][/font][/color][/size]\n\n";
        private const string PT4 = "[phide=NFO]\n";
        private const string PT5 = "[/phide]\n[hide][color=#3366ff]\n\n[color=#ff3333]Passwort: [color=#ff3333]Steht in der geschweiften Klammer oder ist in der NZB enthalten.\n\n[/color][/color][/color][/hide][/align]";

        protected DataTableGeneration dataTableGeneration;

        public string dirName = string.Empty;
        public string chosenNZB = string.Empty;

        public const string IMGUR_IMAGE_LINK_START = "link\":\"";
        public const string IMGUR_IMAGE_LINK_END = "\"},";


        public SteamAppId()
        {
            dataTableGeneration = new DataTableGeneration();
            try
            {
                Task.Run(async () => await dataTableGeneration.GetDataTableAsync(dataTableGeneration)).Wait();


                InitializeComponent();
                pictureSizeSelection.Items.Insert(0, "600x300 (Spiele)");
                pictureSizeSelection.Items.Insert(1, "Originalgröße (Progs, Tutorials, etc.)");
                pictureSizeSelection.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No connection to Steam Database possible.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                InitializeComponent();
            }
        }


        private void generatePost_Click(object sender, EventArgs e)
        {
            if (pictureSizeSelection.SelectedItem.ToString() == "600x300 (Spiele)")
            {
                generatedPost.Text = PT1 + gameTitle.Text + PT2 + ImgurSizeGame + imgurLink.Text + PT3 + steamGameDescription.Text + PT4 + NFOText.Text + PT5;
            }
            else if (pictureSizeSelection.SelectedItem.ToString() == "Originalgröße (Progs, Tutorials, etc.)")
            {
                generatedPost.Text = PT1 + gameTitle.Text + PT2 + ImgurSizeProgram + imgurLink.Text + PT3 + steamGameDescription.Text + PT4 + NFOText.Text + PT5;
            }
        }

        private void SteamAppId_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataTableGeneration.DataTableToGenerate;
            dataGridView1.Columns[0].Width = 275; //Column width for Name
            dataGridView1.Columns[1].Width = 75; //Column width for appid

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Name like '%{0}%'", searchTextBox.Text.Replace("'", "''"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void importNZB_Click(object sender, EventArgs e)
        {
            ImportNZB();
            //System.Windows.MessageBox.Show("NZB imported.\nNumber of NZB´s: " + TotalNZB, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public int ImportNZB()
        {
            Ookii.Dialogs.WinForms.VistaFolderBrowserDialog dialog = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            dialog.ShowDialog();
            dirName = dialog.SelectedPath;
            DirectoryInfo d = new DirectoryInfo(dirName);
            string[] files = new DirectoryInfo(dirName).GetFiles().Select(o => o.Name).ToArray();
            foreach (string s in files)
            {
                NewNZBs.Items.Add(s);
            }
            var TotalNZB = files.Count();
            return TotalNZB;
        }

        private void getNFO_Click(object sender, EventArgs e)
        {
            Object chosenNZBasString = NewNZBs.SelectedItem;
            String str = chosenNZBasString.ToString();
            str = str.Substring(0, str.Length - 30); // Cuts away NZB Password and .nzb extension

            //xRel API Request
            WebRequest request = HttpWebRequest.Create("https://api.xrel.to/v2/release/info.xml?dirname=" + str);
            try
            {
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string nfo = reader.ReadToEnd();

                //Extracts Link to xRel Release
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(nfo);
                var htmlbody = doc.DocumentNode.SelectSingleNode("//link_href");
                Convert.ToString(htmlbody);
                String url = htmlbody.InnerText;
                //description.Text = nfo; //Prints API result

                HtmlDocument doc1 = new HtmlDocument();
                doc.LoadHtml(nfo);
                var htmlbody1 = doc.DocumentNode.SelectSingleNode("//title");
                Convert.ToString(htmlbody1);
                String steamTitle = htmlbody1.InnerText;
                searchTextBox.Text = steamTitle;

                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                
                var browserConfig = new ChromeOptions();
                browserConfig.AddArguments("--window-size=1920,1080");
                browserConfig.AddArguments("--start-maximized");
                browserConfig.AddArguments("--log-level=OFF");
                browserConfig.AddArguments("headless");

                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;

                var webpage = new ChromeDriver(driverService, browserConfig)
                {

                    //Launch the xRel Website and get NFO
                    Url = url
                };
                webpage.FindElement(By.XPath("//a[@class='confirm-big-misunderstanding']")).Click();
                webpage.FindElement(By.XPath("//a[@class='view_nfo']")).Click();
                webpage.FindElement(By.XPath("//a[@class='switch-nfo-view-button']")).Click();
                String finalNFO = webpage.FindElement(By.XPath(@"//pre")).GetAttribute("innerHTML");
                NFOText.Text = finalNFO;
                webpage.Quit();
                
            }
            catch (WebException error)
            {
                if (error.Status == WebExceptionStatus.ProtocolError)
                {
                    MessageBox.Show("Kein Release auf xRel gefunden\nscnlog.me Integration folgt in einem Update", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void NewNZBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeListSelection();
        }

        public void changeListSelection()
        {
            Object selectedItem = NewNZBs.SelectedItem;

            if (NewNZBs.SelectedItem == null)
            {
                if (selectedItem != null)
                {
                    String str = selectedItem.ToString();
                    str = str.Substring(0, str.Length - 30);
                    str = str.Replace('.', ' ');
                    str = str.Replace('_', ' ');
                    gameTitle.Text = str; //NZB Title without _ / . and PW
                }
                else
                {
                }
            }
            else
            {
                chosenNZB = NewNZBs.SelectedItem.ToString();
            }

            if (selectedItem != null)
            {
                String str = selectedItem.ToString();
                str = str.Substring(0, str.Length - 30);
                str = str.Replace('.', ' ');
                str = str.Replace('_', ' ');
                gameTitle.Text = str;
            }
            else
            {

            }
        }
        private void nextNZB_Click(object sender, EventArgs e)
        {
            if (NewNZBs.SelectedIndex != -1)
            {
                string targetPath = dirName + @"\posted";
                string sourceFile = Path.Combine(dirName, chosenNZB);
                string destFile = Path.Combine(targetPath, chosenNZB);
                NewNZBs.Items.RemoveAt(NewNZBs.SelectedIndex);
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
                File.Move(sourceFile, destFile);
                gameTitle.Text = "";
                imgurLink.Text = "";
                steamGameDescription.Text = "";
                NFOText.Text = "";
                generatedPost.Text = "";
                gameHeaderPicture.Image = null;
                steamGameGenres.Text = "";
            }
        }


        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string appIDvalueAsString;
                appIDvalueAsString = dataGridView1.Rows[e.RowIndex].Cells["AppID"].Value.ToString();
                Int32.TryParse(appIDvalueAsString, out int appIDValueAsINT);

                int GameId = appIDValueAsINT;
                Game game = GetGameData(GameId);
                if (game.success)
                {
                    Data data = game.data;
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(data.detailed_description);
                    string result = htmlDoc.DocumentNode.InnerText;
                    steamGameDescription.Text = result;
                    gameHeaderPicture.SizeMode = PictureBoxSizeMode.StretchImage;
                    var downloadHeaderImage = WebRequest.Create(data.header_image);
                    using (var response = downloadHeaderImage.GetResponse())
                    using (var stream = response.GetResponseStream())
                    {
                        gameHeaderPicture.Image = Bitmap.FromStream(stream);
                    }
                    string steamHeaderURL = data.header_image.ToString();
                    string steamGenres = JsonConvert.SerializeObject(data.genres);
                    StringBuilder sb = new StringBuilder(steamGenres);
                    sb.Replace("[", "");
                    sb.Replace("{", "");
                    sb.Replace("]", "");
                    sb.Replace("}", "");
                    sb.Replace("id", "");
                    sb.Replace(":", "");
                    sb.Replace("description", "");
                    sb.Replace(",", "");
                    sb.Replace("\"", "");
                    sb.Replace("0", " ");
                    sb.Replace("1", " ");
                    sb.Replace("2", " ");
                    sb.Replace("3", " ");
                    sb.Replace("4", " ");
                    sb.Replace("5", " ");
                    sb.Replace("6", " ");
                    sb.Replace("7", " ");
                    sb.Replace("8", " ");
                    sb.Replace("9", " ");
                    steamGameGenres.Text = sb.ToString();
                    uploadToImgur(steamHeaderURL);
                }
                else
                {
                    MessageBox.Show("Keine Beschreibung gefunden.\nSpiel besitzt keine Shop Seite auf Steam", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void uploadToImgur(string steamHeaderURL)
        {
            var client = new RestClient("https://api.imgur.com/3/image");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Client-ID ae579a2e87cd3ad");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("image", steamHeaderURL);
            IRestResponse response = client.Execute(request);
            string responseFromImgur = response.Content;
            string uploadedImageLink = getImgurLink(responseFromImgur, IMGUR_IMAGE_LINK_START, IMGUR_IMAGE_LINK_END);
            uploadedImageLink = uploadedImageLink.Replace(@"\", "");
            imgurLink.Text = uploadedImageLink;
        }

        public static string getImgurLink(string responseFromImgur, string strStart, string strEnd)
        {
            if (responseFromImgur.Contains(strStart) && responseFromImgur.Contains(strEnd))
            {
                int Start, End;
                Start = responseFromImgur.IndexOf(strStart, 0) + strStart.Length;
                End = responseFromImgur.IndexOf(strEnd, Start);
                return responseFromImgur.Substring(Start, End - Start);
            }

            return "";
        }

        private void TitleToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(gameTitle.Text);
        }

        private void ImgurLinkToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(imgurLink.Text);
        }

        private void generatedPostToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(generatedPost.Text);
        }

        private void nextNZBwithnewNFO_Click(object sender, EventArgs e)
        {
            if (NewNZBs.SelectedIndex != -1)
            {
                string targetPath = dirName + @"\posted";
                string sourceFile = Path.Combine(dirName, chosenNZB);
                string destFile = Path.Combine(targetPath, chosenNZB);
                NewNZBs.Items.RemoveAt(NewNZBs.SelectedIndex);
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
                File.Move(sourceFile, destFile);
                gameTitle.Text = "";
                NFOText.Text = "";
                generatedPost.Text = "";
            }
        }

        public Game GetGameData(int GameId)
        {
            var url = " https://store.steampowered.com/api/appdetails/?appids=" + GameId.ToString() + "&l=german";
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
            WebReq.Method = "GET";
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            string steamDescriptionAsHTML;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                steamDescriptionAsHTML = reader.ReadToEnd();
            }
            var dict = JsonConvert.DeserializeObject<Dictionary<string, Game>>(steamDescriptionAsHTML);
            Game gameData = dict[GameId.ToString()];
            return gameData;
        }

        public class Rootobject
        {
            public Game _9050 { get; set; }
        }

        public class Game
        {
            public bool success { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string type { get; set; }
            public string name { get; set; }
            public int steam_appid { get; set; }
            public int required_age { get; set; }
            public bool is_free { get; set; }
            public string detailed_description { get; set; }
            public string about_the_game { get; set; }
            public string short_description { get; set; }
            public string supported_languages { get; set; }
            public string header_image { get; set; }
            public string website { get; set; }
            //public Pc_Requirements pc_requirements { get; set; }
            //public object[] mac_requirements { get; set; }
            //public object[] linux_requirements { get; set; }
            public string[] developers { get; set; }
            public string[] publishers { get; set; }
            public Price_Overview price_overview { get; set; }
            public int[] packages { get; set; }
            public Package_Groups[] package_groups { get; set; }
            public Platforms platforms { get; set; }
            public Metacritic metacritic { get; set; }
            public Category[] categories { get; set; }
            public Genre[] genres { get; set; }
            public Screenshot[] screenshots { get; set; }
            public Recommendations recommendations { get; set; }
            public Release_Date release_date { get; set; }
            public Support_Info support_info { get; set; }
            public string background { get; set; }
        }

        public class Pc_Requirements
        {
            public string minimum { get; set; }
        }

        public class Price_Overview
        {
            public string currency { get; set; }
            public int initial { get; set; }
            public int final { get; set; }
            public int discount_percent { get; set; }
        }

        public class Platforms
        {
            public bool windows { get; set; }
            public bool mac { get; set; }
            public bool linux { get; set; }
        }

        public class Metacritic
        {
            public int score { get; set; }
            public string url { get; set; }
        }

        public class Recommendations
        {
            public int total { get; set; }
        }

        public class Release_Date
        {
            public bool coming_soon { get; set; }
            public string date { get; set; }
        }

        public class Support_Info
        {
            public string url { get; set; }
            public string email { get; set; }
        }

        public class Package_Groups
        {
            public string name { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string selection_text { get; set; }
            public string save_text { get; set; }
            public int display_type { get; set; }
            public string is_recurring_subscription { get; set; }
            public Sub[] subs { get; set; }
        }

        public class Sub
        {
            public int packageid { get; set; }
            public string percent_savings_text { get; set; }
            public int percent_savings { get; set; }
            public string option_text { get; set; }
            public string option_description { get; set; }
            public string can_get_free_license { get; set; }
            public bool is_free_license { get; set; }
            public int price_in_cents_with_discount { get; set; }
        }

        public class Category
        {
            public int id { get; set; }
            public string description { get; set; }
        }

        public class Genre
        {
            public string id { get; set; }
            public string description { get; set; }
        }

        public class Screenshot
        {
            public int id { get; set; }
            public string path_thumbnail { get; set; }
            public string path_full { get; set; }
        }

    }
}
