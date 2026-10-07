using System;
using System.Data;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class IssueBooks : System.Web.UI.Page
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
                LoadIssuedBooks();
            }
        }


        // ==========================================
        // LOAD ISSUED BOOKS
        // ==========================================

        private void LoadIssuedBooks()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        @"SELECT 
                            IssueId,
                            MemberId,
                            BookId,
                            IssueDate
                          FROM IssueBooks
                          ORDER BY IssueId DESC";

                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        if (dt.Rows.Count == 0)
                        {
                            ShowMessage(
                                "No issued books found.",
                                false
                            );
                        }
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
        // ISSUE BOOK
        // ==========================================

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            int memberId;
            int bookId;

            // --------------------------------------
            // MEMBER ID VALIDATION
            // --------------------------------------

            if (!int.TryParse(
                txtMemberId.Text.Trim(),
                out memberId))
            {
                ShowMessage(
                    "Please enter a valid Member ID.",
                    false
                );

                return;
            }


            // --------------------------------------
            // BOOK ID VALIDATION
            // --------------------------------------

            if (!int.TryParse(
                txtBookId.Text.Trim(),
                out bookId))
            {
                ShowMessage(
                    "Please enter a valid Book ID.",
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

                    transaction = con.BeginTransaction();


                    // ==================================
                    // CHECK MEMBER
                    // ==================================

                    string memberQuery =
                        @"SELECT COUNT(*)
                          FROM Members
                          WHERE MemberId = @MemberId";

                    using (SqlCommand memberCmd =
                        new SqlCommand(
                            memberQuery,
                            con,
                            transaction))
                    {
                        memberCmd.Parameters.AddWithValue(
                            "@MemberId",
                            memberId);

                        int memberExists =
                            Convert.ToInt32(
                                memberCmd.ExecuteScalar());

                        if (memberExists == 0)
                        {
                            transaction.Rollback();

                            ShowMessage(
                                "Member ID not found.",
                                false
                            );

                            return;
                        }
                    }


                    // ==================================
                    // CHECK BOOK
                    //
                    // IMPORTANT:
                    // TABLE NAME = book
                    // NOT Books
                    // ==================================

                    string bookQuery =
                        @"SELECT Quantity
                          FROM book
                          WHERE BookId = @BookId";

                    int quantity;

                    using (SqlCommand bookCmd =
                        new SqlCommand(
                            bookQuery,
                            con,
                            transaction))
                    {
                        bookCmd.Parameters.AddWithValue(
                            "@BookId",
                            bookId);

                        object result =
                            bookCmd.ExecuteScalar();

                        if (result == null)
                        {
                            transaction.Rollback();

                            ShowMessage(
                                "Book ID not found.",
                                false
                            );

                            return;
                        }

                        quantity =
                            Convert.ToInt32(result);
                    }


                    // ==================================
                    // CHECK BOOK AVAILABLE
                    // ==================================

                    if (quantity <= 0)
                    {
                        transaction.Rollback();

                        ShowMessage(
                            "Book is not available.",
                            false
                        );

                        return;
                    }


                    // ==================================
                    // INSERT ISSUE RECORD
                    // ==================================

                    string insertQuery =
                        @"INSERT INTO IssueBooks
                          (MemberId, BookId, IssueDate)
                          VALUES
                          (@MemberId, @BookId, GETDATE())";

                    using (SqlCommand insertCmd =
                        new SqlCommand(
                            insertQuery,
                            con,
                            transaction))
                    {
                        insertCmd.Parameters.AddWithValue(
                            "@MemberId",
                            memberId);

                        insertCmd.Parameters.AddWithValue(
                            "@BookId",
                            bookId);

                        insertCmd.ExecuteNonQuery();
                    }


                    // ==================================
                    // REDUCE BOOK QUANTITY
                    //
                    // TABLE = book
                    // ==================================

                    string updateQuery =
                        @"UPDATE book
                          SET Quantity = Quantity - 1
                          WHERE BookId = @BookId";

                    using (SqlCommand updateCmd =
                        new SqlCommand(
                            updateQuery,
                            con,
                            transaction))
                    {
                        updateCmd.Parameters.AddWithValue(
                            "@BookId",
                            bookId);

                        updateCmd.ExecuteNonQuery();
                    }


                    // ==================================
                    // COMMIT
                    // ==================================

                    transaction.Commit();


                    ShowMessage(
                        "Book Issued Successfully.",
                        true
                    );


                    ClearFields();

                    LoadIssuedBooks();
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
        // SEARCH ISSUE
        // ==========================================

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int memberId;
            int bookId;

            bool hasMemberId =
                int.TryParse(
                    txtMemberId.Text.Trim(),
                    out memberId);

            bool hasBookId =
                int.TryParse(
                    txtBookId.Text.Trim(),
                    out bookId);


            if (!hasMemberId && !hasBookId)
            {
                ShowMessage(
                    "Enter Member ID or Book ID to search.",
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
                            IssueId,
                            MemberId,
                            BookId,
                            IssueDate
                          FROM IssueBooks
                          WHERE
                            (@MemberId = 0 OR MemberId = @MemberId)
                            AND
                            (@BookId = 0 OR BookId = @BookId)
                          ORDER BY IssueId DESC";

                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.AddWithValue(
                            "@MemberId",
                            hasMemberId ? memberId : 0);

                        da.SelectCommand.Parameters.AddWithValue(
                            "@BookId",
                            hasBookId ? bookId : 0);

                        DataTable dt = new DataTable();

                        da.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();


                        if (dt.Rows.Count > 0)
                        {
                            ShowMessage(
                                "Issue record found.",
                                true
                            );
                        }
                        else
                        {
                            ShowMessage(
                                "No issue record found.",
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

            LoadIssuedBooks();

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
            txtMemberId.Text = "";
            txtBookId.Text = "";
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