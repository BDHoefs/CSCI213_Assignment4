using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4
{
    public partial class Member1 : System.Web.UI.Page
    {
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userId"] = 1; /* TODO: This is a temporary session variable, this should be set by the login page */
            
            int memberId = (int)Session["userId"];
            KarateSchoolDataContext db = new KarateSchoolDataContext(conn);
            var userOrEmpty = (from x in db.Members where x.Member_UserID == memberId select x);

            if (userOrEmpty.Count() != 1)
            {
                // TODO: This user is not a member, throw them back to the login page
            }
            var user = userOrEmpty.First();

            userFirstLastName.Text = user.MemberFirstName + " " + user.MemberLastName;

            var sections = from x in db.Sections where x.Member_ID == memberId select new
            {
                SectionName = x.SectionName,
                InstructorName = 
                    (from y in db.Instructors where x.Instructor_ID == y.InstructorID select y).First().InstructorFirstName + " " +
                    (from y in db.Instructors where x.Instructor_ID == y.InstructorID select y).First().InstructorLastName,
                AmountPaid = x.SectionFee,
                PaymentDate = x.SectionStartDate,
            };


            memberView.DataSource = sections;
            memberView.DataBind();
        }
    }
}