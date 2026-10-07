
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Library_Management_System
{
    public partial class Members : System.Web.UI.Page
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
            // -----------------------------------------------------
            // CHECK LOGIN SESSION
            // -----------------------------------------------------

            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }


            // -----------------------------------------------------
            // LOAD MEMBERS ONLY FIRST TIME
            // -----------------------------------------------------

            if (!IsPostBack)
            {
                LoadMembers();
            }
        }


        // =========================================================
        // LOAD ALL MEMBERS
        // =========================================================

        private void LoadMembers()
        {
            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query = @"
                        SELECT
                            MemberId,
                            MemberName,
                            Email,
                            Phone
                        FROM Members
                        ORDER BY MemberId";


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
                    "Database Error: " +
                    ex.Message,
                    false
                );
            }
        }


        // =========================================================
        // ADD MEMBER
        //
        // IF MEMBER ID IS EMPTY:
        //     SQL SERVER GENERATES ID AUTOMATICALLY
        //
        // IF MEMBER ID IS ENTERED:
        //     IF ID EXISTS -> ERROR
        //     IF ID DOES NOT EXIST -> USE THAT ID
        // =========================================================

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int memberId;


            // =====================================================
            // VALIDATE MEMBER NAME
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                txtMemberName.Text))
            {
                ShowMessage(
                    "Please enter Member Name.",
                    false
                );

                return;
            }


            // =====================================================
            // VALIDATE EMAIL
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                txtEmail.Text))
            {
                ShowMessage(
                    "Please enter Email.",
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
                    // MEMBER ID IS EMPTY
                    //
                    // SQL SERVER GENERATES THE ID
                    // =================================================

                    if (string.IsNullOrWhiteSpace(
                        txtMemberId.Text))
                    {
                        string query = @"
                            INSERT INTO Members
                            (
                                MemberName,
                                Email,
                                Phone
                            )
                            VALUES
                            (
                                @MemberName,
                                @Email,
                                @Phone
                            );

                            SELECT CAST(SCOPE_IDENTITY() AS INT);";


                        using (SqlCommand cmd =
                            new SqlCommand(
                                query,
                                con))
                        {
                            cmd.Parameters.AddWithValue(
                                "@MemberName",
                                txtMemberName.Text.Trim()
                            );

                            cmd.Parameters.AddWithValue(
                                "@Email",
                                txtEmail.Text.Trim()
                            );


                            // -------------------------------------------------
                            // PHONE OPTIONAL
                            // -------------------------------------------------

                            if (string.IsNullOrWhiteSpace(
                                txtPhone.Text))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@Phone",
                                    DBNull.Value
                                );
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(
                                    "@Phone",
                                    txtPhone.Text.Trim()
                                );
                            }


                            // -------------------------------------------------
                            // GET AUTOMATICALLY GENERATED MEMBER ID
                            // -------------------------------------------------

                            int newMemberId =
                                Convert.ToInt32(
                                    cmd.ExecuteScalar()
                                );


                            // -------------------------------------------------
                            // DISPLAY GENERATED ID
                            // -------------------------------------------------

                            txtMemberId.Text =
                                newMemberId.ToString();


                            ShowMessage(
                                "Member Added Successfully! " +
                                "Member ID is: " +
                                newMemberId,
                                true
                            );
                        }
                    }


                    // =================================================
                    // CASE 2:
                    // USER ENTERED MEMBER ID
                    // =================================================

                    else
                    {
                        // -------------------------------------------------
                        // VALIDATE MEMBER ID
                        // -------------------------------------------------

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


                        // -------------------------------------------------
                        // MEMBER ID MUST BE GREATER THAN 0
                        // -------------------------------------------------

                        if (memberId <= 0)
                        {
                            ShowMessage(
                                "Member ID must be greater than 0.",
                                false
                            );

                            return;
                        }


                        // -------------------------------------------------
                        // CHECK WHETHER MEMBER ID ALREADY EXISTS
                        // -------------------------------------------------

                        string checkQuery = @"
                            SELECT COUNT(*)
                            FROM Members
                            WHERE MemberId = @MemberId";


                        using (SqlCommand checkCmd =
                            new SqlCommand(
                                checkQuery,
                                con))
                        {
                            checkCmd.Parameters.AddWithValue(
                                "@MemberId",
                                memberId
                            );


                            int count =
                                Convert.ToInt32(
                                    checkCmd.ExecuteScalar()
                                );


                            // -------------------------------------------------
                            // ID ALREADY EXISTS
                            // -------------------------------------------------

                            if (count > 0)
                            {
                                ShowMessage(
                                    "Member ID " +
                                    memberId +
                                    " already exists.",
                                    false
                                );

                                return;
                            }
                        }


                        // -------------------------------------------------
                        // ENABLE MANUAL ID INSERT
                        // -------------------------------------------------

                        string identityOn =
                            "SET IDENTITY_INSERT Members ON";


                        using (SqlCommand identityCmd =
                            new SqlCommand(
                                identityOn,
                                con))
                        {
                            identityCmd.ExecuteNonQuery();
                        }


                        try
                        {
                            // -------------------------------------------------
                            // INSERT MEMBER WITH USER ENTERED ID
                            // -------------------------------------------------

                            string insertQuery = @"
                                INSERT INTO Members
                                (
                                    MemberId,
                                    MemberName,
                                    Email,
                                    Phone
                                )
                                VALUES
                                (
                                    @MemberId,
                                    @MemberName,
                                    @Email,
                                    @Phone
                                )";


                            using (SqlCommand cmd =
                                new SqlCommand(
                                    insertQuery,
                                    con))
                            {
                                cmd.Parameters.AddWithValue(
                                    "@MemberId",
                                    memberId
                                );

                                cmd.Parameters.AddWithValue(
                                    "@MemberName",
                                    txtMemberName.Text.Trim()
                                );

                                cmd.Parameters.AddWithValue(
                                    "@Email",
                                    txtEmail.Text.Trim()
                                );


                                // -------------------------------------------------
                                // PHONE OPTIONAL
                                // -------------------------------------------------

                                if (string.IsNullOrWhiteSpace(
                                    txtPhone.Text))
                                {
                                    cmd.Parameters.AddWithValue(
                                        "@Phone",
                                        DBNull.Value
                                    );
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(
                                        "@Phone",
                                        txtPhone.Text.Trim()
                                    );
                                }


                                cmd.ExecuteNonQuery();
                            }
                        }
                        finally
                        {
                            // -------------------------------------------------
                            // VERY IMPORTANT
                            // TURN IDENTITY_INSERT OFF
                            // -------------------------------------------------

                            string identityOff =
                                "SET IDENTITY_INSERT Members OFF";


                            using (SqlCommand identityCmd =
                                new SqlCommand(
                                    identityOff,
                                    con))
                            {
                                identityCmd.ExecuteNonQuery();
                            }
                        }


                        ShowMessage(
                            "Member Added Successfully! " +
                            "Member ID is: " +
                            memberId,
                            true
                        );
                    }
                }


                // =====================================================
                // REFRESH GRID
                // =====================================================

                LoadMembers();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 ||
                    ex.Number == 2601)
                {
                    ShowMessage(
                        "Member ID already exists.",
                        false
                    );
                }
                else
                {
                    ShowMessage(
                        "Add Error: " +
                        ex.Message,
                        false
                    );
                }
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Add Error: " +
                    ex.Message,
                    false
                );
            }
        }


        // =========================================================
        // SEARCH MEMBER
        // =========================================================

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int memberId;


            // =====================================================
            // VALIDATE MEMBER ID
            // =====================================================

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


            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query = @"
                        SELECT
                            MemberId,
                            MemberName,
                            Email,
                            Phone
                        FROM Members
                        WHERE MemberId = @MemberId";


                    using (SqlDataAdapter da =
                        new SqlDataAdapter(
                            query,
                            con))
                    {
                        da.SelectCommand.Parameters.AddWithValue(
                            "@MemberId",
                            memberId
                        );


                        DataTable dt =
                            new DataTable();


                        da.Fill(dt);


                        // -------------------------------------------------
                        // SHOW RESULT IN GRID
                        // -------------------------------------------------

                        GridView1.DataSource = dt;
                        GridView1.DataBind();


                        // -------------------------------------------------
                        // MEMBER FOUND
                        // -------------------------------------------------

                        if (dt.Rows.Count > 0)
                        {
                            txtMemberId.Text =
                                dt.Rows[0]["MemberId"]
                                .ToString();


                            txtMemberName.Text =
                                dt.Rows[0]["MemberName"]
                                .ToString();


                            txtEmail.Text =
                                dt.Rows[0]["Email"]
                                .ToString();


                            if (dt.Rows[0]["Phone"] ==
                                DBNull.Value)
                            {
                                txtPhone.Text = "";
                            }
                            else
                            {
                                txtPhone.Text =
                                    dt.Rows[0]["Phone"]
                                    .ToString();
                            }


                            ShowMessage(
                                "Member Found.",
                                true
                            );
                        }
                        else
                        {
                            txtMemberName.Text = "";
                            txtEmail.Text = "";
                            txtPhone.Text = "";


                            ShowMessage(
                                "Member ID Not Found.",
                                false
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Search Error: " +
                    ex.Message,
                    false
                );
            }
        }


        // =========================================================
        // UPDATE MEMBER
        // =========================================================

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int memberId;


            // =====================================================
            // VALIDATE MEMBER ID
            // =====================================================

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


            // =====================================================
            // VALIDATE MEMBER NAME
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                txtMemberName.Text))
            {
                ShowMessage(
                    "Please enter Member Name.",
                    false
                );

                return;
            }


            // =====================================================
            // VALIDATE EMAIL
            // =====================================================

            if (string.IsNullOrWhiteSpace(
                txtEmail.Text))
            {
                ShowMessage(
                    "Please enter Email.",
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
                        UPDATE Members
                        SET
                            MemberName = @MemberName,
                            Email = @Email,
                            Phone = @Phone
                        WHERE MemberId = @MemberId";


                    using (SqlCommand cmd =
                        new SqlCommand(
                            query,
                            con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MemberId",
                            memberId
                        );

                        cmd.Parameters.AddWithValue(
                            "@MemberName",
                            txtMemberName.Text.Trim()
                        );

                        cmd.Parameters.AddWithValue(
                            "@Email",
                            txtEmail.Text.Trim()
                        );


                        // -------------------------------------------------
                        // PHONE OPTIONAL
                        // -------------------------------------------------

                        if (string.IsNullOrWhiteSpace(
                            txtPhone.Text))
                        {
                            cmd.Parameters.AddWithValue(
                                "@Phone",
                                DBNull.Value
                            );
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(
                                "@Phone",
                                txtPhone.Text.Trim()
                            );
                        }


                        con.Open();


                        int result =
                            cmd.ExecuteNonQuery();


                        if (result > 0)
                        {
                            ShowMessage(
                                "Member Updated Successfully.",
                                true
                            );
                        }
                        else
                        {
                            ShowMessage(
                                "Member ID Not Found.",
                                false
                            );
                        }
                    }
                }


                LoadMembers();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Update Error: " +
                    ex.Message,
                    false
                );
            }
        }


        // =========================================================
        // DELETE MEMBER
        // =========================================================

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int memberId;


            // =====================================================
            // VALIDATE MEMBER ID
            // =====================================================

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


            try
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query = @"
                        DELETE FROM Members
                        WHERE MemberId = @MemberId";


                    using (SqlCommand cmd =
                        new SqlCommand(
                            query,
                            con))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MemberId",
                            memberId
                        );


                        con.Open();


                        int result =
                            cmd.ExecuteNonQuery();


                        if (result > 0)
                        {
                            ShowMessage(
                                "Member Deleted Successfully.",
                                true
                            );

                            ClearFields();
                        }
                        else
                        {
                            ShowMessage(
                                "Member ID Not Found.",
                                false
                            );
                        }
                    }
                }


                LoadMembers();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Delete Error: " +
                    ex.Message,
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

            LoadMembers();
        }


        // =========================================================
        // BACK TO DASHBOARD
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
            txtMemberId.Text = "";
            txtMemberName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
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
                    Color.Green;
            }
            else
            {
                lblMessage.ForeColor =
                    Color.Red;
            }
        }
    }
}
