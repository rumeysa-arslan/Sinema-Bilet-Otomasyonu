using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Çift yazılmıştı, birini sildim
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            FilmEkleForm fr = new FilmEkleForm();
            fr.Show();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (cmbAnaSayfaFilm.Text == "")
            {
                MessageBox.Show("Lütfen önce listeden bir film seçiniz.");
                return;
            }

            
            BiletAlForm fr = new BiletAlForm(cmbAnaSayfaFilm.Text);

            fr.Show();
            this.Hide(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            SqlConnection baglanti = new SqlConnection(@"Data Source=RUMEYSA\rumey;Initial Catalog=SinemaOtomasyonDB;Integrated Security=True");

            try
            {
                baglanti.Open();

                SqlCommand komut = new SqlCommand("SELECT FilmAdi FROM Tbl_Filmler", baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                
                cmbAnaSayfaFilm.Items.Clear();

                while (oku.Read())
                {
                    cmbAnaSayfaFilm.Items.Add(oku[0].ToString());
                }

                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Veritabanı bağlantı hatası: " + hata.Message);
            }
        }

        
        private void label1_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}