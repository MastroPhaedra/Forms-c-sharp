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
            pictureBox1.Image = Image.FromFile("../../Images/meme1.png");
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
                pictureBox1.Image = Image.FromFile("../../Images/meme1.png");
            }
            else
            {
                PiltBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                pictureBox1.Image = Image.FromFile("../../Images/"+dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
        }

        private void ClearData()
        {
            NimetusBox.Clear();
            KogusBox.Clear();
            HindBox.Clear();
            PiltBox.Clear();
            pictureBox1.Image = Image.FromFile("../../Images/meme1.png");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (NimetusBox.Text != "" && KogusBox.Text != "" && HindBox.Text != "")
            {
                try
                {
                    con.Open();
                    command = new SqlCommand("INSERT INTO Tooded(Nimetus, Kogus, Hind, Pilt) VALUES(@toode,@kogus,@hind,@pilt)", con);
                    command.Parameters.AddWithValue("@toode", NimetusBox.Text);
                    command.Parameters.AddWithValue("@kogus", KogusBox.Text);
                    command.Parameters.AddWithValue("@hind", HindBox.Text.Replace(",", "."));
                    command.Parameters.AddWithValue("@pilt", PiltBox.Text.Replace(",", "."));
                    command.ExecuteNonQuery();
                    con.Close();
                    Data();
                    ClearData();
                    MessageBox.Show("Dannie dobavleni");
                }
                catch (Exception)
                {
                    MessageBox.Show("Oshibka s bazoi dannih!");
                }
            }
            else
            {
                MessageBox.Show("Neobhodimo vvesti dannie!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NimetusBox.Text != "" && KogusBox.Text != "" && HindBox.Text != "")
            {
                try
                {
                    con.Open();
                    command = new SqlCommand("UPDATE Tooded SET Nimetus=@toode,Kogus=@kogus,Hind=@hind,Pilt=@pilt WHERE Id=@id", con);
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@toode", NimetusBox.Text);
                    command.Parameters.AddWithValue("@kogus", KogusBox.Text);
                    command.Parameters.AddWithValue("@hind", HindBox.Text.Replace(",", "."));
                    //command.Parameters.AddWithValue("@pilt", PiltBox.Text.Replace(",","."));
                    command.ExecuteNonQuery();
                    con.Close();
                    Data();
                    ClearData();
                    MessageBox.Show("Dannie obnovleni");
                }
                catch (Exception)
                {
                    MessageBox.Show("Oshibka s bazoi dannih!");
                }
            }
            else
            {
                MessageBox.Show("Neobhodimo vibrat' dannie!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Id!=0)
            {
                try
                {
                    string andmed = "Andmed" + NimetusBox.Text + " on kustutatud";
                    command = new SqlCommand("DELETE Tooded WHERE Id=@id", con);
                    con.Open();
                    command.Parameters.AddWithValue("@id", Id);
                    command.ExecuteNonQuery();
                    con.Close();
                    Data();
                    ClearData();
                    MessageBox.Show(andmed);
                }
                catch (Exception)
                {
                    MessageBox.Show("Oshibka s bazoi dannih!");
                }
            }
            else
            {
                MessageBox.Show("Oshibka udalenia!");
            }
        }



        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}
    }
}
