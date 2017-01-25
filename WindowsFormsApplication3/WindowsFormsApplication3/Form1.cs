using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {

        //kamus global
        string data;
        string path = AppDomain.CurrentDomain.BaseDirectory;
        //end kamus global

        public class imdb
        {
            public string Title { get; set; }
            public string Year { get; set; }
            public string Rated { get; set; }
            public string Released { get; set; }
            public string Runtime { get; set; }
            public string Genre { get; set; }
            public string Director { get; set; }
            public string Writer { get; set; }
            public string Actors { get; set; }
            public string Plot { get; set; }
            public string Language { get; set; }
            public string Country { get; set; }
            public string Awards { get; set; }
            public string Poster { get; set; }
            public string Metascore { get; set; }
            public string imdbRating { get; set; }
            public string imdbVotes { get; set; }
            public string imdbID { get; set; }
            public string Type { get; set; }
            public string Response { get; set; }
        }
        
        public Form1()
        {
            InitializeComponent();
        }

        
        public void dl_hmtl(string url, ref string output)
        {
            WebClient client = new WebClient();
            button1.Invoke(new MethodInvoker(delegate 
                {
                    button1.Text = "tunggu";
                    button1.Enabled = false;
                }));
            output = client.DownloadString(url);
            imdb isi_data = JsonConvert.DeserializeObject<imdb>(data);
            MessageBox.Show(data);
            MessageBox.Show(isi_data.Title);
            MessageBox.Show(isi_data.Year);
            MessageBox.Show(isi_data.Plot);
            MessageBox.Show(isi_data.Poster);
            if (isi_data.Poster != null)
            { 
                client.DownloadFile(isi_data.Poster, path + "thumb");
               
                //output =  "test";
                pic.ImageLocation = path + "thumb";
                pic.Load();
            }
            button1.Invoke(new MethodInvoker(delegate 
            {
                button1.Text = "ok";
                button1.Enabled = true;
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string raw_text, search_term="";
            
            raw_text = this.txt_search.Text;
            for (int i=0; i < raw_text.Length; i++)
            {
                if (raw_text[i] !=' ')
                {
                    search_term = search_term + raw_text[i]; 
                }
                else
                {
                    search_term = search_term + '+';
                }
            }


            MessageBox.Show(path);
            MessageBox.Show(search_term);
            kerja.RunWorkerAsync(search_term);
           
        }

        private void kerja_DoWork(object sender, DoWorkEventArgs e)
        {
            //MessageBox.Show(e.Argument.ToString);
            dl_hmtl("http://www.omdbapi.com/?t=" + e.Argument + "&y=&plot=short&r=json", ref data);
        }

       
    }
}
