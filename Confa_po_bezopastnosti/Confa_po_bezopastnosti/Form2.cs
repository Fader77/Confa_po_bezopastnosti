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

namespace Confa_po_bezopastnosti
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox2.Text = Properties.Settings.Default.Save_password;
            maskedTextBox1.Text = Properties.Settings.Default.Save_telefon;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.Save_password != "" || Properties.Settings.Default.Save_telefon != "")
                {
                    checkBox1.Checked = true;
                }
                SqlConnection connect = new SqlConnection(Properties.Settings.Default.connection);
                string Telefon = maskedTextBox1.Text.Trim();
                string Password = textBox2.Text.Trim();
                connect.Open();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Avtorization";
                command.Parameters.AddWithValue("@Telefon", Telefon);
                command.Parameters.AddWithValue("@Password", Password);
                command.Connection = connect;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ClassID.ID_user = reader.GetInt32(0);
                        ClassID.ID_role = reader.GetInt32(1);
                        if (ClassID.ID_role == 1)
                        {
                            MessageBox.Show("вы успешно вошли как пользователь");
                            Form3 frmm = new Form3();
                            frmm.Show();
                            Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Save_password = textBox2.Text;
                Properties.Settings.Default.Save();

                Properties.Settings.Default.Save_telefon = maskedTextBox1.Text;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else if (!checkBox2.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
