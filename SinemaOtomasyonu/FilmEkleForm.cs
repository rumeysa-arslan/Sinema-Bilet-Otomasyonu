using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SinemaOtomasyonu
{
    public partial class FilmEkleForm : Form
    {
        public FilmEkleForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {

            SqlConnection baglanti = new SqlConnection(@"Data Source=.;Initial Catalog=SinemaOtomasyonDB;Integrated Security=True");


            baglanti.Open();

            
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Filmler (FilmAdi, Salon, Seans) VALUES (@p1, @p2, @p3)", baglanti);

            
            komut.Parameters.AddWithValue("@p1", txtFilmAdi.Text);
            komut.Parameters.AddWithValue("@p2", cmbSalon.Text);
            komut.Parameters.AddWithValue("@p3", cmbSeans.Text);

            
            komut.ExecuteNonQuery();

            
            baglanti.Close();

            
            MessageBox.Show("Film başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            
            txtFilmAdi.Text = "";
            cmbSalon.Text = "";
            cmbSeans.Text = "";
        }

        private void cmbSalon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FilmEkleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
