using System;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class Dashboard : System.Web.UI.Page
    {
        // ==========================================
        // DATABASE CONNECTION
        // ==========================================

        private string cs =
            @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Userdb;Integrated Security=True";


        // ==========================================
        // PAGE LOAD
        // ==========================================

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check login
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Show username
            lblUsername.Text = Session["Username"].ToString();

            // Load dashboard only first time
            if (!IsPostBack)
            {
                LoadDashboardData();
            }
        }


        // ==========================================
        // LOAD DASHBOARD
        // ==========================================

        private void LoadDashboardData()
        {
            LoadTotalBooks();

            LoadTotalMembers();

            LoadIssuedBooks();

            LoadReturnedBooks();
        }


        // ==========================================
        // TOTAL BOOKS
        // ==========================================

        private void LoadTotalBooks()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        "SELECT COUNT(BookId) FROM book";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        con.Open();

                        int totalBooks =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());

                        lblTotalBooks.Text =
                            totalBooks.ToString();
                    }
                }
            }
            catch (Exception)
            {
                lblTotalBooks.Text = "0";
            }
        }


        // ==========================================
        // TOTAL MEMBERS
        // ==========================================

        private void LoadTotalMembers()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        "SELECT COUNT(*) FROM Members";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        con.Open();

                        int totalMembers =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());

                        lblTotalMembers.Text =
                            totalMembers.ToString();
                    }
                }
            }
            catch (Exception)
            {
                lblTotalMembers.Text = "0";
            }
        }


        // ==========================================
        // ISSUED BOOKS
        // ==========================================

        private void LoadIssuedBooks()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        "SELECT COUNT(*) FROM IssueBooks";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        con.Open();

                        int issuedBooks =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());

                        lblIssuedBooks.Text =
                            issuedBooks.ToString();
                    }
                }
            }
            catch (Exception)
            {
                lblIssuedBooks.Text = "0";
            }
        }


        // ==========================================
        // RETURNED BOOKS
        // ==========================================

        private void LoadReturnedBooks()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        "SELECT COUNT(*) FROM ReturnBooks";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        con.Open();

                        int returnedBooks =
                            Convert.ToInt32(
                                cmd.ExecuteScalar());

                        lblReturnedBooks.Text =
                            returnedBooks.ToString();
                    }
                }
            }
            catch (Exception)
            {
                lblReturnedBooks.Text = "0";
            }
        }


        // ==========================================
        // LOGOUT
        // ==========================================

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}