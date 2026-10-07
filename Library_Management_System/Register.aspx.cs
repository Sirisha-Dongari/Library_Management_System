using System;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class Register : System.Web.UI.Page
    {
        string cs =
            @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Userdb;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (username == "")
            {
                lblMessage.Text = "Please enter Username.";
                return;
            }

            if (password == "")
            {
                lblMessage.Text = "Please enter Password.";
                return;
            }

            if (confirmPassword == "")
            {
                lblMessage.Text = "Please confirm Password.";
                return;
            }

            if (password != confirmPassword)
            {
                lblMessage.Text =
                    "Password and Confirm Password do not match.";
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    // Check username already exists
                    string checkQuery =
                        "SELECT COUNT(*) FROM Users WHERE Username=@Username";

                    using (SqlCommand checkCmd =
                        new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue(
                            "@Username", username);

                        int count =
                            Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            lblMessage.Text =
                                "Username already exists.";
                            return;
                        }
                    }

                    // Create new user
                    string insertQuery =
                        "INSERT INTO Users (Username, Password) " +
                        "VALUES (@Username, @Password)";

                    using (SqlCommand insertCmd =
                        new SqlCommand(insertQuery, con))
                    {
                        insertCmd.Parameters.AddWithValue(
                            "@Username", username);

                        insertCmd.Parameters.AddWithValue(
                            "@Password", password);

                        insertCmd.ExecuteNonQuery();
                    }
                }

                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text =
                    "Error: " + ex.Message;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}