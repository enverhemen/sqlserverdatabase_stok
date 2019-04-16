using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stok_takip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection
            ("Data Source = DESKTOP-HI0L6HS\\SQLEXPRESS; Initial Catalog=stok; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adaptor = new SqlDataAdapter();
        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if ((baglanti.State == ConnectionState.Closed)) baglanti.Open();
            komut = new SqlCommand("SELECT * FROM urunler", baglanti);
            SqlDataReader reader = komut.ExecuteReader();
            reader.Read();

            if (reader.HasRows)
            {
                txt_ad.Text = reader["ad"].ToString();
                txt_fiyat.Text = reader.GetDecimal(4).ToString();
            }
            reader.Close();
            adaptor.SelectCommand = new SqlCommand
                ("select ad,fiyat from urunler", baglanti);
            adaptor.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                ds.Clear();
                SqlCommand komut = new SqlCommand
                    ("INSERT INTO urunler (ad, fiyat) VALUES ('" + txt_ad.Text + "','" + txt_fiyat.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                dataGridView1.Update();
                baglanti.Close();
                MessageBox.Show("Kayıt Eklendi!");
          
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
                baglanti.Close();

            }
        }

        private void txt_ad_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_fiyat_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                ds.Clone();
                SqlCommand komut = new SqlCommand
                    ("UPDATE urunler SET ad = '" + txt_ad.Text + "', fiyat='" + txt_fiyat.Text + "' WHERE ad = '" + txt_ad.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                dataGridView1.Update();
                baglanti.Close();
                MessageBox.Show("Kayıt Güncellendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                ds.Clear();
                SqlCommand komut = new SqlCommand
                    ("DELETE FROM urunler WHERE ad='" + txt_ad.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                dataGridView1.Update();
                dataGridView1.Refresh();
                baglanti.Close();
                MessageBox.Show("Kayıt Silindi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }
    }
}
