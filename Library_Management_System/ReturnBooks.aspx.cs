using System;
using System.Data;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class ReturnBooks : System.Web.UI.Page
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
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadReturnedBooks();
            }
        }


        // ==========================================
        // LOAD RETURNED BOOKS
        // ==========================================

        private void LoadReturnedBooks()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        @"SELECT
                            ReturnId,
                            IssueId,
                            ReturnDate
                          FROM ReturnBooks
                          ORDER BY ReturnId DESC";

                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Database Error: " + ex.Message,
                    false
                );
            }
        }


        // ==========================================
        // RETURN BOOK
        // ==========================================

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            int issueId;

            // --------------------------------------
            // ISSUE ID VALIDATION
            // --------------------------------------

            if (!int.TryParse(
                txtIssueId.Text.Trim(),
                out issueId))
            {
                ShowMessage(
                    "Please enter a valid Issue ID.",
                    false
                );

                return;
            }


            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    con.Open();

                    transaction =
                        con.BeginTransaction();


                    // ==================================
                    // GET BOOK ID FROM ISSUEBOOKS
                    // ==================================

                    string issueQuery =
                        @"SELECT BookId
                          FROM IssueBooks
                          WHERE IssueId = @IssueId";

                    int bookId;

                    using (SqlCommand issueCmd =
                        new SqlCommand(
                            issueQuery,
                            con,
                            transaction))
                    {
                        issueCmd.Parameters.AddWithValue(
                            "@IssueId",
                            issueId);

                        object result =
                            issueCmd.ExecuteScalar();

                        if (result == null)
                        {
                            transaction.Rollback();

                            ShowMessage(
                                "Issue ID not found.",
                                false
                            );

                            return;
                        }

                        bookId =
                            Convert.ToInt32(result);
                    }


                    // ==================================
                    // CHECK ALREADY RETURNED
                    // ==================================

                    string checkReturnQuery =
                        @"SELECT COUNT(*)
                          FROM ReturnBooks
                          WHERE IssueId = @IssueId";

                    using (SqlCommand checkCmd =
                        new SqlCommand(
                            checkReturnQuery,
                            con,
                            transaction))
                    {
                        checkCmd.Parameters.AddWithValue(
                            "@IssueId",
                            issueId);

                        int alreadyReturned =
                            Convert.ToInt32(
                                checkCmd.ExecuteScalar());

                        if (alreadyReturned > 0)
                        {
                            transaction.Rollback();

                            ShowMessage(
                                "This book has already been returned.",
                                false
                            );

                            return;
                        }
                    }


                    // ==================================
                    // INSERT RETURN RECORD
                    // ==================================

                    string insertQuery =
                        @"INSERT INTO ReturnBooks
                          (IssueId, ReturnDate)
                          VALUES
                          (@IssueId, GETDATE())";

                    using (SqlCommand insertCmd =
                        new SqlCommand(
                            insertQuery,
                            con,
                            transaction))
                    {
                        insertCmd.Parameters.AddWithValue(
                            "@IssueId",
                            issueId);

                        insertCmd.ExecuteNonQuery();
                    }


                    // ==================================
                    // INCREASE BOOK QUANTITY
                    //
                    // IMPORTANT:
                    // TABLE NAME = book
                    // NOT Books
                    // ==================================

                    string updateBookQuery =
                        @"UPDATE book
                          SET Quantity = Quantity + 1
                          WHERE BookId = @BookId";

                    using (SqlCommand updateCmd =
                        new SqlCommand(
                            updateBookQuery,
                            con,
                            transaction))
                    {
                        updateCmd.Parameters.AddWithValue(
                            "@BookId",
                            bookId);

                        int updated =
                            updateCmd.ExecuteNonQuery();

                        if (updated == 0)
                        {
                            transaction.Rollback();

                            ShowMessage(
                                "Book ID not found in book table.",
                                false
                            );

                            return;
                        }
                    }


                    // ==================================
                    // COMMIT TRANSACTION
                    // ==================================

                    transaction.Commit();


                    ShowMessage(
                        "Book Returned Successfully.",
                        true
                    );


                    ClearFields();

                    LoadReturnedBooks();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch
                    {
                    }
                }

                ShowMessage(
                    "Database Error: " + ex.Message,
                    false
                );
            }
        }


        // ==========================================
        // SEARCH RETURN
        // ==========================================

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int issueId;

            if (!int.TryParse(
                txtIssueId.Text.Trim(),
                out issueId))
            {
                ShowMessage(
                    "Please enter a valid Issue ID.",
                    false
                );

                return;
            }


            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        @"SELECT
                            ReturnId,
                            IssueId,
                            ReturnDate
                          FROM ReturnBooks
                          WHERE IssueId = @IssueId
                          ORDER BY ReturnId DESC";

                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.AddWithValue(
                            "@IssueId",
                            issueId);

                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();


                        if (dt.Rows.Count > 0)
                        {
                            ShowMessage(
                                "Return record found.",
                                true
                            );
                        }
                        else
                        {
                            ShowMessage(
                                "Return record not found.",
                                false
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Search Error: " + ex.Message,
                    false
                );
            }
        }


        // ==========================================
        // CLEAR
        // ==========================================

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();

            LoadReturnedBooks();

            lblMessage.Text = "";
        }


        // ==========================================
        // BACK
        // ==========================================

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }


        // ==========================================
        // CLEAR FIELDS
        // ==========================================

        private void ClearFields()
        {
            txtIssueId.Text = "";
        }


        // ==========================================
        // MESSAGE
        // ==========================================

        private void ShowMessage(
            string message,
            bool success)
        {
            lblMessage.Text = message;

            if (success)
            {
                lblMessage.ForeColor =
                    System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.ForeColor =
                    System.Drawing.Color.Red;
            }
        }
    }
}