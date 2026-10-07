using System;
using System.Data;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class Book : System.Web.UI.Page
    {
        // =========================================================
        // DATABASE CONNECTION
        // =========================================================

        private string cs =
            @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Userdb;Integrated Security=True";


        // =========================================================
        // PAGE LOAD
        // =========================================================

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadBooks();
            }
        }


        // =========================================================
        // LOAD ALL BOOKS
        // =========================================================

        private void LoadBooks()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query = @"
                        SELECT
                            BookId,
                            BookName,
                            Author,
                            Quantity
                        FROM Book
                        ORDER BY BookId";


                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        DataTable dt =
                            new DataTable();

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


        // =========================================================
        // ADD BOOK
        //
        // IF BOOK ID IS EMPTY:
        //     SQL SERVER GENERATES ID AUTOMATICALLY
        //
        // IF BOOK ID IS ENTERED:
        //     IF ID EXISTS -> ERROR
        //     IF ID DOES NOT EXIST -> USE THAT ID
        // =========================================================

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int quantity;
            int bookId;


            // =====================================================
            // VALIDATE BOOK NAME
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                txtBookName.Text))
            {
                ShowMessage(
                    "Please enter Book Name.",
                    false
                );

                return;
            }


            // =====================================================
            // VALIDATE AUTHOR
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                txtAuthor.Text))
            {
                ShowMessage(
                    "Please enter Author.",
                    false
                );

                return;
            }


            // =====================================================
            // VALIDATE QUANTITY
            // =====================================================

            if (!int.TryParse(
                txtQuantity.Text.Trim(),
                out quantity))
            {
                ShowMessage(
                    "Quantity must be a number.",
                    false
                );

                return;
            }


            if (quantity < 0)
            {
                ShowMessage(
                    "Quantity cannot be negative.",
                    false
                );

                return;
            }


            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    con.Open();


                    // =================================================
                    // CASE 1:
                    // BOOK ID IS EMPTY
                    //
                    // SQL SERVER GENERATES THE ID
                    // =================================================

                    if (string.IsNullOrWhiteSpace(
                        txtBookId.Text))
                    {
                        string query = @"
                            INSERT INTO Book
                            (
                                BookName,
                                Author,
                                Quantity
                            )
                            VALUES
                            (
                                @BookName,
                                @Author,
                                @Quantity
                            );

                            SELECT CAST(SCOPE_IDENTITY() AS INT);";


                        using (SqlCommand cmd =
                            new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue(
                                "@BookName",
                                txtBookName.Text.Trim()
                            );

                            cmd.Parameters.AddWithValue(
                                "@Author",
                                txtAuthor.Text.Trim()
                            );

                            cmd.Parameters.AddWithValue(
                                "@Quantity",
                                quantity
                            );


                            // -----------------------------------------
                            // GET AUTOMATICALLY GENERATED ID
                            // -----------------------------------------

                            int newBookId =
                                Convert.ToInt32(
                                    cmd.ExecuteScalar()
                                );


                            // -----------------------------------------
                            // DISPLAY GENERATED ID
                            // -----------------------------------------

                            txtBookId.Text =
                                newBookId.ToString();


                            ShowMessage(
                                "Book Added Successfully! " +
                                "Book ID is: " +
                                newBookId,
                                true
                            );
                        }
                    }


                    // =================================================
                    // CASE 2:
                    // USER ENTERED BOOK ID
                    // =================================================

                    else
                    {
                        // ---------------------------------------------
                        // VALIDATE BOOK ID
                        // ---------------------------------------------

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


                        if (bookId <= 0)
                        {
                            ShowMessage(
                                "Book ID must be greater than 0.",
                                false
                            );

                            return;
                        }


                        // ---------------------------------------------
                        // CHECK WHETHER BOOK ID ALREADY EXISTS
                        // ---------------------------------------------

                        string checkQuery = @"
                            SELECT COUNT(*)
                            FROM Book
                            WHERE BookId = @BookId";


                        using (SqlCommand checkCmd =
                            new SqlCommand(
                                checkQuery,
                                con))
                        {
                            checkCmd.Parameters.AddWithValue(
                                "@BookId",
                                bookId
                            );


                            int count =
                                Convert.ToInt32(
                                    checkCmd.ExecuteScalar()
                                );


                            // -----------------------------------------
                            // ID ALREADY EXISTS
                            // -----------------------------------------

                            if (count > 0)
                            {
                                ShowMessage(
                                    "Book ID " +
                                    bookId +
                                    " already exists.",
                                    false
                                );

                                return;
                            }
                        }


                        // ---------------------------------------------
                        // ID DOES NOT EXIST
                        //
                        // Allow manually entering identity value
                        // ---------------------------------------------

                        string identityOn =
                            "SET IDENTITY_INSERT Book ON";


                        using (SqlCommand identityCmd =
                            new SqlCommand(
                                identityOn,
                                con))
                        {
                            identityCmd.ExecuteNonQuery();
                        }


                        try
                        {
                            string insertQuery = @"
                                INSERT INTO Book
                                (
                                    BookId,
                                    BookName,
                                    Author,
                                    Quantity
                                )
                                VALUES
                                (
                                    @BookId,
                                    @BookName,
                                    @Author,
                                    @Quantity
                                )";


                            using (SqlCommand cmd =
                                new SqlCommand(
                                    insertQuery,
                                    con))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@BookId",
                                    bookId
                                );

                                cmd.Parameters.AddWithValue(
                                    "@BookName",
                                    txtBookName.Text.Trim()
                                );

                                cmd.Parameters.AddWithValue(
                                    "@Author",
                                    txtAuthor.Text.Trim()
                                );

                                cmd.Parameters.AddWithValue(
                                    "@Quantity",
                                    quantity
                                );


                                cmd.ExecuteNonQuery();
                            }
                        }
                        finally
                        {
                            // -----------------------------------------
                            // VERY IMPORTANT:
                            // Turn IDENTITY_INSERT OFF
                            // -----------------------------------------

                            string identityOff =
                                "SET IDENTITY_INSERT Book OFF";


                            using (SqlCommand identityCmd =
                                new SqlCommand(
                                    identityOff,
                                    con))
                            {
                                identityCmd.ExecuteNonQuery();
                            }
                        }


                        ShowMessage(
                            "Book Added Successfully! " +
                            "Book ID is: " +
                            bookId,
                            true
                        );
                    }
                }


                // =====================================================
                // REFRESH GRID
                // =====================================================

                LoadBooks();
            }
            catch (SqlException ex)
            {
                ShowMessage(
                    "Add Error: " + ex.Message,
                    false
                );
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Add Error: " + ex.Message,
                    false
                );
            }
        }


        // =========================================================
        // SEARCH BOOK
        // =========================================================

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int bookId;


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


            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query = @"
                        SELECT
                            BookId,
                            BookName,
                            Author,
                            Quantity
                        FROM Book
                        WHERE BookId = @BookId";


                    using (SqlDataAdapter da =
                        new SqlDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.AddWithValue(
                            "@BookId",
                            bookId
                        );


                        DataTable dt =
                            new DataTable();


                        da.Fill(dt);


                        GridView1.DataSource = dt;
                        GridView1.DataBind();


                        if (dt.Rows.Count > 0)
                        {
                            txtBookId.Text =
                                dt.Rows[0]["BookId"].ToString();

                            txtBookName.Text =
                                dt.Rows[0]["BookName"].ToString();

                            txtAuthor.Text =
                                dt.Rows[0]["Author"].ToString();

                            txtQuantity.Text =
                                dt.Rows[0]["Quantity"].ToString();


                            ShowMessage(
                                "Book Found.",
                                true
                            );
                        }
                        else
                        {
                            txtBookName.Text = "";
                            txtAuthor.Text = "";
                            txtQuantity.Text = "";


                            ShowMessage(
                                "Book ID Not Found.",
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


        // =========================================================
        // UPDATE BOOK
        // =========================================================

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int bookId;
            int quantity;


            // =====================================================
            // VALIDATE BOOK ID
            // =====================================================

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


            // =====================================================
            // VALIDATE BOOK NAME
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                txtBookName.Text))
            {
                ShowMessage(
                    "Please enter Book Name.",
                    false
                );

                return;
            }


            // =====================================================
            // VALIDATE AUTHOR
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                txtAuthor.Text))
            {
                ShowMessage(
                    "Please enter Author.",
                    false
                );

                return;
            }


            // =====================================================
            // VALIDATE QUANTITY
            // =====================================================

            if (!int.TryParse(
                txtQuantity.Text.Trim(),
                out quantity))
            {
                ShowMessage(
                    "Quantity must be a number.",
                    false
                );

                return;
            }


            if (quantity < 0)
            {
                ShowMessage(
                    "Quantity cannot be negative.",
                    false
                );

                return;
            }


            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query = @"
                        UPDATE Book
                        SET
                            BookName = @BookName,
                            Author = @Author,
                            Quantity = @Quantity
                        WHERE BookId = @BookId";


                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@BookId",
                            bookId
                        );

                        cmd.Parameters.AddWithValue(
                            "@BookName",
                            txtBookName.Text.Trim()
                        );

                        cmd.Parameters.AddWithValue(
                            "@Author",
                            txtAuthor.Text.Trim()
                        );

                        cmd.Parameters.AddWithValue(
                            "@Quantity",
                            quantity
                        );


                        con.Open();


                        int result =
                            cmd.ExecuteNonQuery();


                        if (result > 0)
                        {
                            ShowMessage(
                                "Book Updated Successfully.",
                                true
                            );
                        }
                        else
                        {
                            ShowMessage(
                                "Book ID Not Found.",
                                false
                            );
                        }
                    }
                }


                LoadBooks();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Update Error: " + ex.Message,
                    false
                );
            }
        }


        // =========================================================
        // DELETE BOOK
        // =========================================================

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int bookId;


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


            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query = @"
                        DELETE FROM Book
                        WHERE BookId = @BookId";


                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@BookId",
                            bookId
                        );


                        con.Open();


                        int result =
                            cmd.ExecuteNonQuery();


                        if (result > 0)
                        {
                            ShowMessage(
                                "Book Deleted Successfully.",
                                true
                            );

                            ClearFields();
                        }
                        else
                        {
                            ShowMessage(
                                "Book ID Not Found.",
                                false
                            );
                        }
                    }
                }


                LoadBooks();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Delete Error: " + ex.Message,
                    false
                );
            }
        }


        // =========================================================
        // CLEAR BUTTON
        // =========================================================

        protected void btnClear_Click(
            object sender,
            EventArgs e)
        {
            ClearFields();

            lblMessage.Text = "";

            LoadBooks();
        }


        // =========================================================
        // BACK BUTTON
        // =========================================================

        protected void btnBack_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "Dashboard.aspx"
            );
        }


        // =========================================================
        // CLEAR ALL FIELDS
        // =========================================================

        private void ClearFields()
        {
            txtBookId.Text = "";
            txtBookName.Text = "";
            txtAuthor.Text = "";
            txtQuantity.Text = "";
        }


        // =========================================================
        // SHOW MESSAGE
        // =========================================================

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