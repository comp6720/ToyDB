using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientConnect;

namespace ToyDB
{
    public partial class TOYODB : Form
    {
        public TOYODB()
        {
            InitializeComponent();
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String statement = sqlStatement.Text;

            Client client = new Client();
            client.ConnectSocket(11111);
            client.SendQuery(statement);

            MessageBox.Show("SQL command executed");
        }
    }
}
