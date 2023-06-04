using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _253_EmirhanHüner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="Admin" & textBox2.Text=="1234")
            {
            Form2 git=new Form2();
            git.Show();
            this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı Veya Parolanız Yanlıştır.");
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_StyleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
