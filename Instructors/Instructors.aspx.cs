using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4.Instructors
{
    public partial class Instructors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
            KarateSchoolDataContext dbcon;

            dbcon = new KarateSchoolDataContext(conn);
            //redriect if user is not a instructor
            if (Session.Count != 0)
            {
                if (HttpContext.Current.Session["userType"].ToString().Trim() != "Instructor")
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    Response.Redirect("logon.aspx", true);
                }

            }

            int instructorId = (int)Session["userId"];

            var user = (from x in dbcon.Instructors where x.InstructorID == instructorId select x);

            var userData = user.First();

            lbl_first.Text = userData.InstructorFirstName;
            lbl_last.Text = userData.InstructorLastName;

            var memberList = from x in userData.Sections
                             select new
                             {
                                 SectionName = x.SectionName,
                                 MemberName =
                                (from y in dbcon.Members where x.Member_ID == y.Member_UserID select y).First().MemberFirstName + " " +
                                (from y in dbcon.Members where x.Member_ID == y.Member_UserID select y).First().MemberLastName
                             };


            GridView_Instructor.DataSource = memberList;
            GridView_Instructor.DataBind();

        }
    }
}