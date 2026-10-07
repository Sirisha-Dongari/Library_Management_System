using System;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class Login : System.Web.UI.Page
    {
        string cs =
            @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Userdb;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (username == "" || password == "")
            {
                lblMessage.Text = "Please enter Username and Password.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query =
                        "SELECT Password FROM Users WHERE Username=@Username";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        con.Open();

                        object result = cmd.ExecuteScalar();

                        if (result == null)
                        {
                            lblMessage.Text =
                                "Account not found. Please register first.";
                            return;
                        }

                        string savedPassword = result.ToString();

                        if (savedPassword == password)
                        {
                            Session["Username"] = username;

                            Response.Redirect("Dashboard.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Incorrect Password.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}