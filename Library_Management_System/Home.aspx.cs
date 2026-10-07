using System;

namespace Library_Management_System
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Login check
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblUser.Text =
                    "Welcome, " + Session["Username"].ToString();
            }
        }


        // ==============================
        // MANAGE BOOKS
        // ==============================

        protected void btnManageBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("Book.aspx");
        }


        // ==============================
        // MEMBERS
        // ==============================

        protected void btnMembers_Click(object sender, EventArgs e)
        {
            Response.Redirect("Members.aspx");
        }


        // ==============================
        // ISSUE BOOKS
        // ==============================

        protected void btnIssueBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("IssueBooks.aspx");
        }


        // ==============================
        // RETURN BOOKS
        // ==============================

        protected void btnReturnBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReturnBooks.aspx");
        }


        // ==============================
        // LOGOUT
        // ==============================

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}