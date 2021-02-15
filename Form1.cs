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

namespace Forms_c_sharp
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\AppData\Database.mdf;Integrated Security = True");
        SqlDataAdapter adapter;
        SqlCommand command;
        public Form1()
        {
            InitializeComponent();
            Data();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Data()
        {
            con.Open();
            DataTable table = new DataTable();
            adapter = new SqlDataAdapter("SELECT * FROM Tooded", con);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        int Id = 0;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            NimetusBox.Text= dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            KogusBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            HindBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();  
            if (dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()=="")
            {
                PiltBox.Text = "None";
            }
            else
            {
                PiltBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            command = new SqlCommand("INSERT INTO Tooded(Nimetus, Kogus, Hind, Pilt) VALUES(@toode,@kogus,@hind,@pilt)",con);
            command.Parameters.AddWithValue("@toode",NimetusBox.Text);
            command.Parameters.AddWithValue("@kogus", KogusBox.Text);
            command.Parameters.AddWithValue("@hind", HindBox.Text);
            command.Parameters.AddWithValue("@pilt", PiltBox.Text);
            command.ExecuteNonQuery();
            con.Close();
            Data();
        }


        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
    }
}
