using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _253_EmirhanHüner
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        internal class VeriTabanıİslemleri
        {
            string baglantiCumlesi = ConfigurationManager.ConnectionStrings["Emirhan"].ConnectionString;
            public MySqlConnection baglan()
            {
                MySqlConnection baglanti =new MySqlConnection(baglantiCumlesi);
                MySqlConnection.ClearPool(baglanti);
                return baglanti;
            }
        }
        VeriTabanıİslemleri vtislemleri = new VeriTabanıİslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut;
        string KomutSatiri;
        private void Form2_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            try
            {
                baglanti= vtislemleri.baglan();
                KomutSatiri = "Select * From müşteri";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(KomutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["ad_soyad"].HeaderText = "Ad Soyad";
                dataGridView1.Columns["tc_kimlik"].HeaderText = "Tc Kimlik";
                dataGridView1.Columns["Hesap_Numarası"].HeaderText = "Hesap Numarası";
                dataGridView1.Columns["Sifre"].HeaderText = "Şifre";
                dataGridView1.Columns["para_miktarı"].HeaderText = "Para Miktarı";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Hata Oluştu",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            
            try
            {
                if(baglanti.State != ConnectionState.Open) 
                {
                    baglanti.Open();
                }
                KomutSatiri = "INSERT INTO müşteri (ad_soyad,tc_kimlik,Hesap_Numarası,Sifre,para_miktarı, " +
                    "VALUES(@ad_soyad,@tc_kimlik,@Hesap_Numarası,@Sifre,@para_miktarı,)";
                komut=new MySqlCommand(KomutSatiri,baglanti);
                
                komut.Parameters.AddWithValue("@ad_soyad", txtadsoyad.Text);
                komut.Parameters.AddWithValue("@tc_kimlik",txtkimlik.Text);
                komut.Parameters.AddWithValue("@Hesap_Numarası", txthesap.Text);
                komut.Parameters.AddWithValue("@Sifre", txtsifre.Text);
                komut.Parameters.AddWithValue("@para_miktarı",txtpara.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                Temizle();
                MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Listele();
                
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Hata Oluştu",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Temizle()
        {
            txtadsoyad.Clear();
            txthesap.Clear();
            txtkimlik.Clear();
            txtsifre.Clear();
            txtpara.Clear();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            
            try
            {
                if(baglanti.State !=ConnectionState.Open)
                {
                    baglanti.Open();
                }
                KomutSatiri = "DELETE  FROM müşteri WHERE ad_soyad = @ad_soyad";
                komut = new MySqlCommand(KomutSatiri, baglanti);
                komut.Parameters.AddWithValue("@ad_soyad", dataGridView1.CurrentRow.Cells["ad_soyad"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                Temizle();
                MessageBox.Show("İşlem Başarılı","Mesaj",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                Listele();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtadsoyad.Text = dataGridView1.CurrentRow.Cells["ad_soyad"].Value.ToString();
                txtkimlik.Text = dataGridView1.CurrentRow.Cells["tc_kimlik"].Value.ToString();
                txthesap.Text = dataGridView1.CurrentRow.Cells["Hesap_Numarası"].Value.ToString();
                txtsifre.Text = dataGridView1.CurrentRow.Cells["Sifre"].Value.ToString();
                txtpara.Text = dataGridView1.CurrentRow.Cells["para_miktarı"].Value.ToString();
                

            }catch (Exception)
            {
                MessageBox.Show("Hata Oluştu","Mesaj",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                    KomutSatiri = "UPDATE müşteri SET ad_soyad=@ad_soyad, tc_kimlik=@tc_kimlik, Hesap_Numarası=@Hesap_Numarası, Sifre=@Sifre,para_miktarı=@para_miktarı where müşteri_id=@ID";
                    komut = new MySqlCommand(KomutSatiri, baglanti);
                    komut.Parameters.AddWithValue("@ID", int.Parse(dataGridView1.CurrentRow.Cells["müşteri_id"].Value.ToString()));
                    komut.Parameters.AddWithValue("@ad_soyad", txtadsoyad.Text);
                    komut.Parameters.AddWithValue("@tc_kimlik", txtkimlik.Text);
                    komut.Parameters.AddWithValue("@Hesap_Numarası", txthesap.Text);
                    komut.Parameters.AddWithValue("@Sifre", txtsifre.Text);
                    komut.Parameters.AddWithValue("@para_miktarı",txtpara.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    Temizle();
                    MessageBox.Show("İşlem başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Listele();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnyatır_Click(object sender, EventArgs e)
        {
            
            Form3 git = new Form3();
            git.Show();
            this.Hide();
        }
    }
}
