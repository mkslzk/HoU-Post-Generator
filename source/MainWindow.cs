using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Ookii.Dialogs.WinForms;
using System.Windows;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using Application = System.Windows.Forms.Application;
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

        private const string CLIENT_ID = "ae579a2e87cd3ad";
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
            catch (Exception ex) { }
        }

        private void importNZB_Click(object sender, EventArgs e)
        {
            int TotalNZB = ImportNZB();
            System.Windows.MessageBox.Show("NZB imported.\nNumber of NZB´s: " + TotalNZB, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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


                new DriverManager().SetUpDriver(new EdgeConfig());
                var edgeconf = new EdgeOptions();
                edgeconf.AddArguments("--window-size=1920,1080");
                edgeconf.AddArguments("--start-maximized");
                edgeconf.AddArguments("--log-level=OFF");
                edgeconf.AddArguments("headless");

                var driverService = EdgeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;

                var webpage = new EdgeDriver(driverService, edgeconf)
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
                    MessageBox.Show("No Release on xRel found!\nPlease search manually for it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("All done");
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
            }
        }


        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string appIDvalueAsString = ""; //Leere Zeichenkette initialisieren um diese als Grundlage für weitere Schritte zu verwenden
                appIDvalueAsString = dataGridView1.Rows[e.RowIndex].Cells["AppID"].Value.ToString(); //Der Wert der ausgewählten Zelle wird in die Variable geschrieben
                int appIDValueAsINT = 0; //Integer Wert initialiseren
                Int32.TryParse(appIDvalueAsString, out appIDValueAsINT); //Konvertierung von Zeichenkette zu Integer Wert

                int GameId = appIDValueAsINT; //Übergabe an weitere Variable
                Game game = GetGameData(GameId); //Aufruf der Methode mit passender AppID
                if (game.success) //Wenn Abfrage erfolgreich
                {
                    Data data = game.data; //Erhält alle allgemeinen Informationen der Steam Shop Seite für die korrespondierende AppID
                    HtmlDocument htmlDoc = new HtmlDocument(); //Erstelle neues temporäres HTML Dokument
                    htmlDoc.LoadHtml(data.detailed_description); //Lade Inhalt aus Zeile 213 in das Dokument
                    string result = htmlDoc.DocumentNode.InnerText; //Extrahiere die Beschreibung ohne ignoriere HTML Tags
                    steamGameDescription.Text = result; //Ergebnis wird in der Oberfläche ausgegeben
                    gameHeaderPicture.SizeMode = PictureBoxSizeMode.StretchImage; //Funktion um Bild passend darzustellen
                    var downloadHeaderImage = WebRequest.Create(data.header_image); //Anfrage um Bild herunterzuladen
                    using (var response = downloadHeaderImage.GetResponse()) //Erhalte oben gestellte Anfrage
                    using (var stream = response.GetResponseStream()) //Schreibe Inhalt der Anfrage in eine Variable
                    {
                        gameHeaderPicture.Image = Bitmap.FromStream(stream); //Inhalt der Variable wird als Bild interpretiert und in der vorher definierten Bildbox ausgegeben
                    }
                    string steamHeaderURL = data.header_image.ToString();
                    uploadToImgur(steamHeaderURL);
                }
                else
                {
                    MessageBox.Show("Keine Beschreibung gefunden.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
