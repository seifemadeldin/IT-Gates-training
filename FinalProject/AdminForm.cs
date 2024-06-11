using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
	public partial class AdminForm : Form
	{
		public AdminForm(String username)
		{
			InitializeComponent();
			label9.Text = username + ":";
		}
		SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-SED4F7SG\VE_SERVER;Initial Catalog=projectDB;Integrated Security=True");
		private void AdminForm_Load(object sender, EventArgs e)
		{
			display_data();
			display_product_data();
		}
		private void display_data()
		{
			try
			{
					conn.Open();
					string query = "SELECT * FROM staff1";
					SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
					DataTable dataTable = new DataTable();
					adapter.Fill(dataTable);
					dataGridView1.DataSource = dataTable;
					conn.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
		}
		private void insertButton_Click(object sender, EventArgs e)
		{
			conn.Open();
			SqlCommand cmd = new SqlCommand();
			cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			string sqlCommand = "insert into staff1 " +
				"(first_name, last_name, email, phone, national_number, statue, username,[password]) " +
				"values ('" + firstnameBox.Text + "','" + lastnameBox.Text + "','" + emailBox.Text + "','" +
				phoneBox.Text + "','" + nationalBox.Text + "','" + statueBox.Text + "','" + usernameBox.Text +
				"','" + passwordBox.Text + "')";
			cmd.CommandText = sqlCommand;
			cmd.ExecuteNonQuery();

			firstnameBox.Text = ""; lastnameBox.Text = "";
			emailBox.Text = "";		phoneBox.Text = "";
			nationalBox.Text = "";  statueBox.Text = "";
			usernameBox.Text = "";  passwordBox.Text = "";

			MessageBox.Show("Insert Successful");
			conn.Close();
			display_data();
		}

		private void logoutButton_Click(object sender, EventArgs e)
		{
			Form1 login = new Form1();
			login.Show();
			this.Close();
		}

		private void updateButton_Click(object sender, EventArgs e)
		{
			try
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd = conn.CreateCommand();
				cmd.CommandType = CommandType.Text;

				string sqlCommand = $"update staff1 set first_name='{firstnameBox.Text}'," +
					$"last_name='{lastnameBox.Text}', email='{emailBox.Text}', phone='{phoneBox.Text}', " +
					$"national_number='{nationalBox.Text}', statue='{statueBox.Text}', [password]='{passwordBox.Text}'," +
                    $"username='{usernameBox.Text}'" + 
					$"where id='{Convert.ToInt32(idBox.Text)}'";

				cmd.CommandText = sqlCommand;
				cmd.ExecuteNonQuery();

				firstnameBox.Text = ""; lastnameBox.Text = "";
				emailBox.Text = ""; phoneBox.Text = "";
				nationalBox.Text = ""; statueBox.Text = "";
				usernameBox.Text = ""; passwordBox.Text = "";

				MessageBox.Show("update member successful");
				conn.Close();
                display_data();
            }
			catch (Exception ex)
			{
                MessageBox.Show("Error: " + ex.Message);
            }
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			try
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd = conn.CreateCommand();
				cmd.CommandType = CommandType.Text;
				string sqlCommand = $"delete from staff1 where id='{Convert.ToInt32(idBox.Text)}'";
				cmd.CommandText = sqlCommand;
				cmd.ExecuteNonQuery();
                firstnameBox.Text = ""; lastnameBox.Text = "";
                emailBox.Text = ""; phoneBox.Text = "";
                nationalBox.Text = ""; statueBox.Text = "";
                usernameBox.Text = ""; passwordBox.Text = "";
                MessageBox.Show("Delete member successful");
				conn.Close();
				display_data();
			}
			catch (Exception ex)
			{
                MessageBox.Show("Error: " + ex.Message);
            }
		}

		private void searchButton_Click(object sender, EventArgs e)
		{
			try
			{
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
				string sqlCommand = $"select * from staff1 where id='{Convert.ToInt32(idBox.Text)}'";
				cmd.CommandText = sqlCommand;
				cmd.ExecuteNonQuery();

				//DataTable dt = new DataTable();
				//SqlDataAdapter da = new SqlDataAdapter(cmd);
				//da.Fill(dt);
				//dataGridView1.DataSource = dt;

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                firstnameBox.Text = ""; lastnameBox.Text = "";
                emailBox.Text = ""; phoneBox.Text = "";
                nationalBox.Text = ""; statueBox.Text = "";
                usernameBox.Text = ""; passwordBox.Text = "";

                MessageBox.Show("Search member successful");
				conn.Close();
            }
			catch (Exception ex)
			{
                MessageBox.Show("Error: " + ex.Message);
            }
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

        private void homeButton_Click(object sender, EventArgs e)
        {
			display_data();
        }

		private void display_product_data()
		{
            try
            {
                conn.Open();
                string query = "SELECT * FROM product";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void productsInsert_Click(object sender, EventArgs e)
        {
			try
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd = conn.CreateCommand();
				cmd.CommandType = CommandType.Text;
				string sqlCommand = $"insert into product (product_name, quantity, price, category, brand, " +
					$"serial_number, production_date, expired_date) " +
					$"values ('{productnameBox.Text}','{Convert.ToInt32(numericUpDown1.Value)}','{float.Parse(priceBox.Text)}'" +
					$",'{categoryBox.Text}','{brandBox.Text}','{serialnumberBox.Text}'," +
					$"'{dateTimePicker1.Value.ToShortDateString()}'," +
                    $"'{dateTimePicker2.Value.ToShortDateString()}')";
				cmd.CommandText = sqlCommand;
				cmd.ExecuteNonQuery();

				productnameBox.Text = ""; numericUpDown1.Value = 0;
				priceBox.Text = ""; categoryBox.Text = "";
				brandBox.Text = ""; serialnumberBox.Text = "";
                dateTimePicker1 = new DateTimePicker(); dateTimePicker2 = new DateTimePicker();

				MessageBox.Show("Insert Successful");
				conn.Close();
				display_product_data();
			}
			catch (Exception ex)
			{
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void productsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;

                string sqlCommand = $"update product set product_name='{productnameBox.Text}'," +
                    $"quantity='{Convert.ToInt32(numericUpDown1.Value)}', price='{float.Parse(priceBox.Text)}'," +
					$"category='{categoryBox.Text}',brand='{brandBox.Text}'," +
					$"production_date='{dateTimePicker1.Value.ToShortDateString()}'," +
					$"expired_date='{dateTimePicker2.Value.ToShortDateString()}', serial_number='{serialnumberBox.Text}'" +
					$"where id='{Convert.ToInt32(idproductBox.Text)}'";

                cmd.CommandText = sqlCommand;
                cmd.ExecuteNonQuery();

                productnameBox.Text = ""; numericUpDown1.Value = 0;
                priceBox.Text = ""; categoryBox.Text = "";
                brandBox.Text = ""; serialnumberBox.Text = "";
                dateTimePicker1 = new DateTimePicker(); dateTimePicker2 = new DateTimePicker();

                MessageBox.Show("update product successful");
                conn.Close();
                display_product_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void productsDelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string sqlCommand = $"delete from product where id='{Convert.ToInt32(idproductBox.Text)}'";
                cmd.CommandText = sqlCommand;
                cmd.ExecuteNonQuery();
                productnameBox.Text = ""; numericUpDown1.Value = 0;
                priceBox.Text = ""; categoryBox.Text = "";
                brandBox.Text = ""; serialnumberBox.Text = "";
                dateTimePicker1 = new DateTimePicker(); dateTimePicker2 = new DateTimePicker();
                MessageBox.Show("Delete product successful");
                conn.Close();
                display_product_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void productSearch_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string sqlCommand = $"select * from product where id='{Convert.ToInt32(idproductBox.Text)}'";
                cmd.CommandText = sqlCommand;
                cmd.ExecuteNonQuery();

                //DataTable dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt);
                //dataGridView2.DataSource = dt;

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;

                productnameBox.Text = ""; numericUpDown1.Value = 0;
                priceBox.Text = ""; categoryBox.Text = "";
                brandBox.Text = ""; serialnumberBox.Text = "";
                dateTimePicker1 = new DateTimePicker(); dateTimePicker2 = new DateTimePicker();

                MessageBox.Show("Search product successful");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void productHome_Click(object sender, EventArgs e)
        {
			display_product_data();
        }
    }
}
