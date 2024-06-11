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
	public partial class UserForm : Form
	{
		double finalPrice = 0;
		public UserForm(string username)
		{
			InitializeComponent();
			label9.Text = username + ":";



			conn.Open();
			SqlCommand cmd = new SqlCommand();
			cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			string sqlCommand = "select product_name from product";
			cmd.CommandText = sqlCommand;
			cmd.ExecuteNonQuery();

			DataTable dt = new DataTable();
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			DataRow dr;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dr = dt.Rows[i];
				productNameBox.Items.Add(dr["product_name"].ToString());
			}
			conn.Close();
		}
		SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-SED4F7SG\VE_SERVER;Initial Catalog=projectDB;Integrated Security=True");
		private void UserForm_Load(object sender, EventArgs e)
		{

		}

		private void logoutButton_Click(object sender, EventArgs e)
		{
			Form1 login = new Form1();
			login.Show();
			this.Close();
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

		private void insertButton_Click(object sender, EventArgs e)
		{
			conn.Open();
			if(serialBox.Text == null)
			{
				if(productNameBox.SelectedItem.ToString() != null)
				{
					SqlCommand cmd = new SqlCommand();
					cmd = conn.CreateCommand();
					cmd.CommandType = CommandType.Text;
					string sqlCommand = $"select product_name,serial_number,price from product " +
						$"where product_name='{productNameBox.Items[productNameBox.SelectedIndex].ToString()}'";
					cmd.CommandText = sqlCommand;
					cmd.ExecuteNonQuery();

					DataTable dt = new DataTable();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dt);
					DataRow dr;
					for(int i = 0; i < dt.Rows.Count; i++)
					{
						dr = dt.Rows[i];
						finalPrice = finalPrice + (Convert.ToInt32(quantityBox.Value) * Convert.ToInt32(dr["price"].ToString()));

						dataGridView1.Rows.Add(dr["serial_number"].ToString(), productNameBox.Items[productNameBox.SelectedIndex].ToString(),
							quantityBox.Value.ToString(), dr["price"].ToString(), (Convert.ToInt32(quantityBox.Value) *
							Convert.ToInt32(dr["price"].ToString())).ToString());
					}
				}
			}
			else
			{
				if (productNameBox.SelectedItem != null)
				{
					SqlCommand cmd = new SqlCommand();
					cmd = conn.CreateCommand();
					cmd.CommandType = CommandType.Text;
					string sqlCommand = $"select product_name,serial_number,price from product " +
						$"where product_name='{productNameBox.Items[productNameBox.SelectedIndex].ToString()}'";
					cmd.CommandText = sqlCommand;
					cmd.ExecuteNonQuery();

					DataTable dt = new DataTable();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(dt);
					DataRow dr;
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						dr = dt.Rows[i];
						finalPrice = finalPrice + (Convert.ToInt32(quantityBox.Value) * Convert.ToInt32(dr["price"].ToString()));

						dataGridView1.Rows.Add(dr["serial_number"].ToString(), productNameBox.Items[productNameBox.SelectedIndex].ToString(),
							quantityBox.Value.ToString(), dr["price"].ToString(), (Convert.ToInt32(quantityBox.Value) *
							Convert.ToInt32(dr["price"].ToString())).ToString());
					}
				}
				else
				{
					try
					{
						SqlCommand cmd = new SqlCommand();
						cmd = conn.CreateCommand();
						cmd.CommandType = CommandType.Text;
						string sqlCommand = $"select product_name,serial_number,price from product" +
							$"where serial_number='{serialBox.Text.ToString()}'";
						cmd.CommandText = sqlCommand;
						cmd.ExecuteNonQuery();

						DataTable dt = new DataTable();
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(dt);
						DataRow dr;
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							dr = dt.Rows[i];
							finalPrice = finalPrice + (Convert.ToInt32(quantityBox.Value) * Convert.ToInt32(dr["price"].ToString()));

							dataGridView1.Rows.Add(dr["serial_number"].ToString(), dr["product_name"].ToString(),
								quantityBox.Value.ToString(), dr["price"].ToString(), (Convert.ToInt32(quantityBox.Value) *
								Convert.ToInt32(dr["price"].ToString())).ToString());
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Serial number is wrong" + ex);
					}
				}
			}
			MessageBox.Show($"Total price: {finalPrice.ToString()}");
			conn.Close();
		}
		private void printButton_Click(object sender, EventArgs e)
		{
			printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprm", 550, 600);
			if(printPreviewDialog1.ShowDialog() == DialogResult.OK )
			{
				printDocument1.Print();
			}
		}

		int product_quantity, product_price, total, pos = 100;
		string product_id, product_name;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Color headingColor = Color.FromArgb(0, 102, 204);
            Color contentColor = Color.Black;
            Font headingFont = new Font("Arial", 14, FontStyle.Bold);
            Font contentFont = new Font("Arial", 10);

            e.Graphics.DrawString("Super Market Receipt", headingFont, new SolidBrush(headingColor), new Point(185));

			e.Graphics.DrawString("ID\t\t  Product\t Price\t  Quantity\t Total Price",
				new Font("century Gothic", 10, FontStyle.Bold), Brushes.Blue, new Point(26,80));

			foreach(DataGridViewRow item in dataGridView1.Rows)
			{
				product_id = "" + item.Cells["product_serial"].Value;
				product_name = "" + item.Cells["name"].Value;
				product_price = Convert.ToInt32(item.Cells["quantity"].Value);
				product_quantity = Convert.ToInt32(item.Cells["quantity"].Value);
				total = Convert.ToInt32(item.Cells["totalprice"].Value);

                e.Graphics.DrawString(product_id, contentFont, new SolidBrush(contentColor), new Point(25, pos));
                e.Graphics.DrawString(product_name, contentFont, new SolidBrush(contentColor), new Point(150, pos));
                e.Graphics.DrawString(product_price.ToString(), contentFont, new SolidBrush(Color.DarkRed), new Point(265, pos));
                e.Graphics.DrawString(product_quantity.ToString(), contentFont, new SolidBrush(Color.Green), new Point(335, pos));
                e.Graphics.DrawString(total.ToString(), contentFont, new SolidBrush(contentColor), new Point(450, pos));

                //e.Graphics.DrawString("" + product_id, new Font("century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(26, pos));
                //e.Graphics.DrawString("		" + product_name, new Font("century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(60, pos));
                //e.Graphics.DrawString("		" + product_price, new Font("century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(150, pos));
                //e.Graphics.DrawString("		" + product_quantity, new Font("century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(200, pos));
                //e.Graphics.DrawString("		" + total, new Font("century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(270, pos));

                pos += 20;
            }

            e.Graphics.DrawString("\t Total: " + finalPrice, new Font("Arial", 12, FontStyle.Bold), Brushes.Green, new Point(350, pos + 20));
            e.Graphics.DrawString("Thank you for shopping with us!", new Font("Arial", 10, FontStyle.Italic), new SolidBrush(contentColor), new Point(175, pos + 60));
            e.Graphics.DrawString("********************* Super-Market-Name ********************", headingFont, new SolidBrush(headingColor), new Point(10, pos + 100));
        }
    }
}
