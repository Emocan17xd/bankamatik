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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnyatır_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.Open();
                    KomutSatiri = "UPDATE müşteri SET para_miktarı=@para_miktarı yatırılacak_miktar=@yatırılacak_miktar where müşteri_id=@ID";
                    komut = new MySqlCommand(KomutSatiri, baglanti);
                    komut.Parameters.AddWithValue("@ID", int.Parse(dataGridView1.CurrentRow.Cells["müşteri_id"].Value.ToString()));

                    komut.Parameters.AddWithValue("@para_miktarı", txtpara.Text);
                    komut.Parameters.AddWithValue("@yatırılacak_miktar",txtyatırma.Text);
                    int sayi1 = Convert.ToInt32(txtyatırma.Text);
                    int sayi2 = Convert.ToInt32(txtpara.Text);
                    int toplam = sayi1 + sayi2;
                    txtpara.Text=txtyatırma.Text + toplam;
                    
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

        private void Temizle()
        {
            
            txtpara.Clear();
            txthesap.Clear();
            txtsifre.Clear();
            txtyatırma.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }
        internal class VeriTabanıİslemleri
        {
            string baglantiCumlesi = ConfigurationManager.ConnectionStrings["Emirhan"].ConnectionString;
            public MySqlConnection baglan()
            {
                MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);
                MySqlConnection.ClearPool(baglanti);
                return baglanti;
            }
        }
        VeriTabanıİslemleri vtislemleri = new VeriTabanıİslemleri();
        MySqlConnection baglanti;
        MySqlCommand komut;
        string KomutSatiri;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                txthesap.Text = dataGridView1.CurrentRow.Cells["Hesap_Numarası"].Value.ToString();
                txtsifre.Text = dataGridView1.CurrentRow.Cells["Sifre"].Value.ToString();
                txtpara.Text = dataGridView1.CurrentRow.Cells["para_miktarı"].Value.ToString();


            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void Listele()
        {
            try
            {
                baglanti = vtislemleri.baglan();
                KomutSatiri = "Select * From müşteri";
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(KomutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["Hesap_Numarası"].HeaderText = "Hesap Numarası";
                dataGridView1.Columns["Sifre"].HeaderText = "Şifre";
                dataGridView1.Columns["para_miktarı"].HeaderText = "Para Miktarı";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata Oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
