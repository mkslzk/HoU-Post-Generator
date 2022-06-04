
namespace PostGenerator
{
    partial class SteamAppId
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamAppId));
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.importNZB = new System.Windows.Forms.Button();
            this.importNZBDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gameName = new System.Windows.Forms.Label();
            this.generatedPost = new System.Windows.Forms.RichTextBox();
            this.generatedPostLabel = new System.Windows.Forms.Label();
            this.NFOText = new System.Windows.Forms.RichTextBox();
            this.nfoLabel = new System.Windows.Forms.Label();
            this.steamGameDescription = new System.Windows.Forms.RichTextBox();
            this.gameDescriptionLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nextNZB = new System.Windows.Forms.Button();
            this.getNFO = new System.Windows.Forms.Button();
            this.gameHeaderPicture = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.imgurLink = new System.Windows.Forms.TextBox();
            this.gameTitle = new System.Windows.Forms.TextBox();
            this.NewNZBs = new System.Windows.Forms.ListBox();
            this.SteamGameGenre = new System.Windows.Forms.Label();
            this.generatePost = new System.Windows.Forms.Button();
            this.generatedPostToClipboard = new System.Windows.Forms.Button();
            this.ImgurLinkToClipboard = new System.Windows.Forms.Button();
            this.TitleToClipboard = new System.Windows.Forms.Button();
            this.nextNZBwithnewNFO = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameHeaderPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(378, 23);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(436, 20);
            this.searchTextBox.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.Location = new System.Drawing.Point(378, 83);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(440, 373);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(378, 49);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(436, 21);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Durchsuche SteamDB";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // importNZB
            // 
            this.importNZB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.importNZB.Location = new System.Drawing.Point(6, 12);
            this.importNZB.Name = "importNZB";
            this.importNZB.Size = new System.Drawing.Size(90, 30);
            this.importNZB.TabIndex = 6;
            this.importNZB.Text = "Import NZB";
            this.importNZB.UseVisualStyleBackColor = true;
            this.importNZB.Click += new System.EventHandler(this.importNZB_Click);
            // 
            // importNZBDialog
            // 
            this.importNZBDialog.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // gameName
            // 
            this.gameName.AutoSize = true;
            this.gameName.Location = new System.Drawing.Point(378, 6);
            this.gameName.Name = "gameName";
            this.gameName.Size = new System.Drawing.Size(89, 13);
            this.gameName.TabIndex = 7;
            this.gameName.Text = "Name des Spiels:";
            // 
            // generatedPost
            // 
            this.generatedPost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.generatedPost.Location = new System.Drawing.Point(823, 675);
            this.generatedPost.Name = "generatedPost";
            this.generatedPost.Size = new System.Drawing.Size(421, 207);
            this.generatedPost.TabIndex = 8;
            this.generatedPost.Text = "";
            // 
            // generatedPostLabel
            // 
            this.generatedPostLabel.AutoSize = true;
            this.generatedPostLabel.Location = new System.Drawing.Point(820, 659);
            this.generatedPostLabel.Name = "generatedPostLabel";
            this.generatedPostLabel.Size = new System.Drawing.Size(69, 13);
            this.generatedPostLabel.TabIndex = 9;
            this.generatedPostLabel.Text = "Fertiger Post:";
            // 
            // NFOText
            // 
            this.NFOText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NFOText.Location = new System.Drawing.Point(823, 484);
            this.NFOText.Name = "NFOText";
            this.NFOText.Size = new System.Drawing.Size(421, 172);
            this.NFOText.TabIndex = 10;
            this.NFOText.Text = "";
            // 
            // nfoLabel
            // 
            this.nfoLabel.AutoSize = true;
            this.nfoLabel.Location = new System.Drawing.Point(820, 468);
            this.nfoLabel.Name = "nfoLabel";
            this.nfoLabel.Size = new System.Drawing.Size(32, 13);
            this.nfoLabel.TabIndex = 11;
            this.nfoLabel.Text = "NFO:";
            // 
            // steamGameDescription
            // 
            this.steamGameDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.steamGameDescription.Location = new System.Drawing.Point(823, 286);
            this.steamGameDescription.Name = "steamGameDescription";
            this.steamGameDescription.Size = new System.Drawing.Size(421, 176);
            this.steamGameDescription.TabIndex = 12;
            this.steamGameDescription.Text = "";
            // 
            // gameDescriptionLabel
            // 
            this.gameDescriptionLabel.AutoSize = true;
            this.gameDescriptionLabel.Location = new System.Drawing.Point(820, 270);
            this.gameDescriptionLabel.Name = "gameDescriptionLabel";
            this.gameDescriptionLabel.Size = new System.Drawing.Size(75, 13);
            this.gameDescriptionLabel.TabIndex = 13;
            this.gameDescriptionLabel.Text = "Beschreibung:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(820, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Titel:";
            // 
            // nextNZB
            // 
            this.nextNZB.Location = new System.Drawing.Point(188, 12);
            this.nextNZB.Name = "nextNZB";
            this.nextNZB.Size = new System.Drawing.Size(90, 30);
            this.nextNZB.TabIndex = 20;
            this.nextNZB.Text = "Clear All";
            this.nextNZB.UseVisualStyleBackColor = true;
            this.nextNZB.Click += new System.EventHandler(this.nextNZB_Click);
            // 
            // getNFO
            // 
            this.getNFO.Location = new System.Drawing.Point(97, 12);
            this.getNFO.Name = "getNFO";
            this.getNFO.Size = new System.Drawing.Size(90, 30);
            this.getNFO.TabIndex = 21;
            this.getNFO.Text = "Lade NFO";
            this.getNFO.UseVisualStyleBackColor = true;
            this.getNFO.Click += new System.EventHandler(this.getNFO_Click);
            // 
            // gameHeaderPicture
            // 
            this.gameHeaderPicture.Location = new System.Drawing.Point(837, 6);
            this.gameHeaderPicture.Name = "gameHeaderPicture";
            this.gameHeaderPicture.Size = new System.Drawing.Size(400, 184);
            this.gameHeaderPicture.TabIndex = 22;
            this.gameHeaderPicture.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(820, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Imgur Link:";
            // 
            // imgurLink
            // 
            this.imgurLink.Location = new System.Drawing.Point(823, 247);
            this.imgurLink.Name = "imgurLink";
            this.imgurLink.Size = new System.Drawing.Size(421, 20);
            this.imgurLink.TabIndex = 25;
            // 
            // gameTitle
            // 
            this.gameTitle.Location = new System.Drawing.Point(823, 207);
            this.gameTitle.Name = "gameTitle";
            this.gameTitle.Size = new System.Drawing.Size(421, 20);
            this.gameTitle.TabIndex = 26;
            // 
            // NewNZBs
            // 
            this.NewNZBs.FormattingEnabled = true;
            this.NewNZBs.Location = new System.Drawing.Point(6, 49);
            this.NewNZBs.Name = "NewNZBs";
            this.NewNZBs.Size = new System.Drawing.Size(365, 407);
            this.NewNZBs.TabIndex = 16;
            this.NewNZBs.SelectedIndexChanged += new System.EventHandler(this.NewNZBs_SelectedIndexChanged);
            // 
            // SteamGameGenre
            // 
            this.SteamGameGenre.AutoSize = true;
            this.SteamGameGenre.Location = new System.Drawing.Point(868, 169);
            this.SteamGameGenre.Name = "SteamGameGenre";
            this.SteamGameGenre.Size = new System.Drawing.Size(0, 13);
            this.SteamGameGenre.TabIndex = 28;
            this.SteamGameGenre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // generatePost
            // 
            this.generatePost.Location = new System.Drawing.Point(614, 484);
            this.generatePost.Name = "generatePost";
            this.generatePost.Size = new System.Drawing.Size(200, 35);
            this.generatePost.TabIndex = 29;
            this.generatePost.Text = "Erstelle Post";
            this.generatePost.UseVisualStyleBackColor = true;
            this.generatePost.Click += new System.EventHandler(this.generatePost_Click);
            // 
            // generatedPostToClipboard
            // 
            this.generatedPostToClipboard.Location = new System.Drawing.Point(614, 606);
            this.generatedPostToClipboard.Name = "generatedPostToClipboard";
            this.generatedPostToClipboard.Size = new System.Drawing.Size(200, 35);
            this.generatedPostToClipboard.TabIndex = 30;
            this.generatedPostToClipboard.Text = "Kopiere Post";
            this.generatedPostToClipboard.UseVisualStyleBackColor = true;
            this.generatedPostToClipboard.Click += new System.EventHandler(this.generatedPostToClipboard_Click);
            // 
            // ImgurLinkToClipboard
            // 
            this.ImgurLinkToClipboard.Location = new System.Drawing.Point(614, 565);
            this.ImgurLinkToClipboard.Name = "ImgurLinkToClipboard";
            this.ImgurLinkToClipboard.Size = new System.Drawing.Size(200, 35);
            this.ImgurLinkToClipboard.TabIndex = 31;
            this.ImgurLinkToClipboard.Text = "Kopiere Bild";
            this.ImgurLinkToClipboard.UseVisualStyleBackColor = true;
            this.ImgurLinkToClipboard.Click += new System.EventHandler(this.ImgurLinkToClipboard_Click);
            // 
            // TitleToClipboard
            // 
            this.TitleToClipboard.Location = new System.Drawing.Point(614, 524);
            this.TitleToClipboard.Name = "TitleToClipboard";
            this.TitleToClipboard.Size = new System.Drawing.Size(200, 35);
            this.TitleToClipboard.TabIndex = 32;
            this.TitleToClipboard.Text = "Kopiere Titel";
            this.TitleToClipboard.UseVisualStyleBackColor = true;
            this.TitleToClipboard.Click += new System.EventHandler(this.TitleToClipboard_Click);
            // 
            // nextNZBwithnewNFO
            // 
            this.nextNZBwithnewNFO.Location = new System.Drawing.Point(284, 12);
            this.nextNZBwithnewNFO.Name = "nextNZBwithnewNFO";
            this.nextNZBwithnewNFO.Size = new System.Drawing.Size(90, 30);
            this.nextNZBwithnewNFO.TabIndex = 33;
            this.nextNZBwithnewNFO.Text = "Clear Titel/NFO";
            this.nextNZBwithnewNFO.UseVisualStyleBackColor = true;
            this.nextNZBwithnewNFO.Click += new System.EventHandler(this.nextNZBwithnewNFO_Click);
            // 
            // SteamAppId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1251, 884);
            this.Controls.Add(this.nextNZBwithnewNFO);
            this.Controls.Add(this.TitleToClipboard);
            this.Controls.Add(this.ImgurLinkToClipboard);
            this.Controls.Add(this.generatedPostToClipboard);
            this.Controls.Add(this.generatePost);
            this.Controls.Add(this.SteamGameGenre);
            this.Controls.Add(this.gameTitle);
            this.Controls.Add(this.imgurLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gameHeaderPicture);
            this.Controls.Add(this.getNFO);
            this.Controls.Add(this.nextNZB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NewNZBs);
            this.Controls.Add(this.gameDescriptionLabel);
            this.Controls.Add(this.steamGameDescription);
            this.Controls.Add(this.nfoLabel);
            this.Controls.Add(this.NFOText);
            this.Controls.Add(this.generatedPostLabel);
            this.Controls.Add(this.generatedPost);
            this.Controls.Add(this.gameName);
            this.Controls.Add(this.importNZB);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.searchTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SteamAppId";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "House of Usenet Games Generator v.0.1.0 (alpha)";
            this.Load += new System.EventHandler(this.SteamAppId_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameHeaderPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button importNZB;
        private System.Windows.Forms.OpenFileDialog importNZBDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label gameName;
        private System.Windows.Forms.RichTextBox generatedPost;
        private System.Windows.Forms.Label generatedPostLabel;
        private System.Windows.Forms.RichTextBox NFOText;
        private System.Windows.Forms.Label nfoLabel;
        private System.Windows.Forms.RichTextBox steamGameDescription;
        private System.Windows.Forms.Label gameDescriptionLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nextNZB;
        private System.Windows.Forms.Button getNFO;
        private System.Windows.Forms.PictureBox gameHeaderPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox imgurLink;
        private System.Windows.Forms.TextBox gameTitle;
        private System.Windows.Forms.ListBox NewNZBs;
        private System.Windows.Forms.Label SteamGameGenre;
        private System.Windows.Forms.Button generatePost;
        private System.Windows.Forms.Button generatedPostToClipboard;
        private System.Windows.Forms.Button ImgurLinkToClipboard;
        private System.Windows.Forms.Button TitleToClipboard;
        private System.Windows.Forms.Button nextNZBwithnewNFO;
    }
}

