using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace TaskMDesktop
{
    public partial class LoginForm : Form
    {
        string connectionString = "server=localhost;database=RDP_desktop;integrated security=true;TrustServerCertificate=true";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string password = textBox1.Text;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter password.");
            }
            else if (LoginUser(password) == 0)
            {
                Form1 form1 = new Form1();
                form1.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid password. Please try again.");
            }
        }


        private int LoginUser(string password)
        {
            string query = $"loginuser @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Create and open the connection
                    connection.Open();

                    // Create the command and add the parameter to prevent SQL injection
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@password", password);

                        // Execute the query and read the results
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())  // Ensure there's data
                            {
                                return int.Parse(reader["loginstatus"]?.ToString());
                            }
                            throw new Exception();
                        }
                    }
                }
                catch
                {
                    return 1;
                }
            }
        }
    }
}
