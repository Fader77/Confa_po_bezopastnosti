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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Confa_po_bezopastnosti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(Properties.Settings.Default.connection);
                DataSet dataset = new DataSet();
                SqlCommand profiletext = new SqlCommand();
                profiletext.CommandType = CommandType.StoredProcedure;
                profiletext.CommandText = "Event_grid";
                profiletext.Connection = con;
                SqlDataAdapter adapter = new SqlDataAdapter(profiletext);
                adapter.Fill(dataset);
                dataGridView1.DataSource = dataset.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Мероприятия";
                dataGridView1.Columns[2].HeaderText = "Дата мероприятия";
                dataGridView1.Columns[3].HeaderText = "Длительность меропрятия";
                dataGridView1.Columns[4].HeaderText = "Город";
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Filter()
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = $"[Event] LIKE '%{textBox1.Text}%'";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frmm = new Form2();
            frmm.Show();
            Hide();
        }
    }
}
