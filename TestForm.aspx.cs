using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment4
{
    public partial class TestForm : System.Web.UI.Page
    {
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ben\\Desktop\\Assignment4\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        protected void Page_Load(object sender, EventArgs e)
        {
            KarateSchoolDataContext db = new KarateSchoolDataContext(conn);
            var listItemsRecord = (from x in db.Instructors select new { InstructorId = x.InstructorID, InstructorLastName = x.InstructorLastName }).ToList();
            testView.DataSource = listItemsRecord;
            testView.DataBind();
        }
    }
}