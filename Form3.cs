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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnyatır_Click(object sender, EventArgs e)
        {
            Form2 git= new Form2();
            git.ShowDialog();
            this.Hide();
        }
    }
}
