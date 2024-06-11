using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinalProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-SED4F7SG\VE_SERVER;Initial Catalog=projectDB;Integrated Security=True");
        

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit?", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string userName = userNameBox.Text;
            string password = passwordBox.Text;
            string statue = "";

            try
            {
                string query = "select statue from staff1 where username='" +
                                userNameBox.Text + "'and password='" + passwordBox.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                //SqlCommand cmd1 = new SqlCommand(query, con);
                // SqlDataReader rdr = cmd1.ExecuteReader();
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    userName = userNameBox.Text;
                    password = passwordBox.Text;
                    DataRow dr = dt.Rows[0];
                    statue = dr["statue"].ToString();
                    if (statue == "admin")
                    {
                        MessageBox.Show("Hello  " + statue);
                        AdminForm adminform = new AdminForm(userName);
                        adminform.Show();
                        this.Hide();
                    }
                    else
                    {
                        UserForm userform = new UserForm(userName);
                        userform.Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("Invalid login data");
                    userNameBox.Clear();
                    passwordBox.Clear();
                    userNameBox.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("error" + ex);

            }
            finally
            {
                con.Close();
            }
        }
    }
}
