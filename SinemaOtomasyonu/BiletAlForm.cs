using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // SQL kütüphanesi
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaOtomasyonu
{
    public partial class BiletAlForm : Form
    {
        
        public BiletAlForm(string gelenFilmAdi)
        {
            InitializeComponent();
           
            textBox1.Text = gelenFilmAdi;
        }

        
        SqlConnection baglanti = new SqlConnection(@"Data Source=RUMEYSA\rumey;Initial Catalog=SinemaOtomasyonDB;Integrated Security=True");

        
        private void BiletAlForm_Load(object sender, EventArgs e)
        {
            DoluKoltuklariGetir();
        }

        void DoluKoltuklariGetir()
        {
            baglanti.Open();
            
            SqlCommand komut = new SqlCommand("SELECT KoltukNo FROM Tbl_Satislar WHERE FilmAdi = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", textBox1.Text);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                string gelenNo = oku[0].ToString();

                
                foreach (Control arac in groupBox3.Controls)
                {
                    if (arac is Button)
                    {
                        if (arac.Text == gelenNo)
                        {
                            arac.BackColor = Color.Red; 
                            arac.Enabled = false;       
                        }
                    }
                }
            }
            baglanti.Close();
        }

        
        private void OrtakKoltuk_Click(object sender, EventArgs e)
        {
            Button tiklanan = (Button)sender;

            if (tiklanan.BackColor == Color.Red)
            {
                MessageBox.Show("Bu koltuk dolu!");
            }
            else if (tiklanan.BackColor == Color.Yellow)
            {
                tiklanan.BackColor = Color.WhiteSmoke; 
            }
            else
            {
                tiklanan.BackColor = Color.Yellow; 
            }
        }

       
        private void btnBiletAl_Click(object sender, EventArgs e)
        {
            int sayac = 0;

            
            bool secimVarMi = false;
            foreach (Control arac in groupBox1.Controls)
            {
                if (arac is Button && arac.BackColor == Color.Yellow)
                    secimVarMi = true;
            }

            if (secimVarMi == false)
            {
                MessageBox.Show("Lütfen önce koltuk seçiniz.");
                return;
            }

            
            foreach (Control arac in groupBox3.Controls)
            {
                if (arac is Button && arac.BackColor == Color.Yellow)
                {
                    baglanti.Open();

                    SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Satislar (FilmAdi, KoltukNo, AdSoyad, Ucret, Tarih) VALUES (@p1, @p2, @p3, @p4, @p5)", baglanti);

                    komut.Parameters.AddWithValue("@p1", textBox1.Text);
                    komut.Parameters.AddWithValue("@p2", arac.Text); 
                    komut.Parameters.AddWithValue("@p3", textBox2.Text + " " + textBox3.Text);

                    if (rbTam.Checked) komut.Parameters.AddWithValue("@p4", "200 TL");
                    else komut.Parameters.AddWithValue("@p4", "150 TL");

                    komut.Parameters.AddWithValue("@p5", DateTime.Now.ToString());

                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    
                    arac.BackColor = Color.Red;
                    arac.Enabled = false;
                    sayac++;
                }
            }

            if (sayac > 0)
            {
                MessageBox.Show(sayac + " adet bilet başarıyla satıldı!");
                
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            
            textBox2.Text = "";
            textBox3.Text = "";

           
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            foreach (Control arac in groupBox3.Controls)
            {
                if (arac is Button)
                {
                    
                    if (arac.BackColor == Color.Yellow)
                    {
                        arac.BackColor = Color.White; 
                    }
                }
            }

            MessageBox.Show("İşlem iptal edildi, seçimler temizlendi.");
        }
    }
    }
