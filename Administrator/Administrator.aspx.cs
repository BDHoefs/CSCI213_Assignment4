using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace Assignment4
{
    public partial class Administrator : System.Web.UI.Page
    {
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        KarateSchoolDataContext db;

        struct MemberNames
        {
            public int UserId;
            public string MemberFirstLast;
        }

        // Stores user information for populating the user and section gridviews respectively
        List<int> userIds;
        List<MemberNames> memberNames;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userId"] = 3; /* TODO: This is a temporary session variable, this should be set by the login page */

            int userId = (int)Session["userId"];
            db = new KarateSchoolDataContext(conn);
            string userType = (from x in db.NetUsers where x.UserID==userId select x).First().UserType;

            if (userType != "Administrator")
            {
                // TODO: Kick the user back to the login page
            }

            if (!IsPostBack)
            {
                refreshGridViews();
            }
        }

        private void refreshGridViews()
        {
            {
                var members = (from x in db.Members
                               select new
                               {
                                   UserId = x.NetUser.UserID,
                                   UserName = x.NetUser.UserName,
                                   FirstName = x.MemberFirstName,
                                   LastName = x.MemberLastName,
                                   DateJoined = x.MemberDateJoined.ToString(),
                                   PhoneNumber = x.MemberPhoneNumber,
                                   Email = x.MemberEmail,
                                   UserType = "Member",
                               });

                var instructors = (from x in db.Instructors
                                   select new
                                   {
                                       UserId = x.NetUser.UserID,
                                       UserName = x.NetUser.UserName,
                                       FirstName = x.InstructorFirstName,
                                       LastName = x.InstructorLastName,
                                       DateJoined = "",
                                       PhoneNumber = x.InstructorPhoneNumber,
                                       Email = "",
                                       UserType = "Instructor",
                                   });

                var administrators = (from x in db.NetUsers
                                      where x.UserType == "Administrator"
                                      select new
                                      {
                                          UserId = x.UserID,
                                          UserName = x.UserName,
                                          FirstName = "",
                                          LastName = "",
                                          DateJoined = "",
                                          PhoneNumber = "",
                                          Email = "",
                                          UserType = "Administrator",
                                      });

                
                var users = members.Concat(instructors.Concat(administrators)).OrderBy(x => x.LastName);
                userIds = (from x in users select x.UserId).ToList();


                userView.DataSource = users.OrderBy(x => x.LastName);
                userView.DataBind();
            }

            {
                var members = (from x in db.Members
                                select new
                                {
                                    UserId = x.Member_UserID,
                                    MemberFirstLast = x.MemberFirstName + " " + x.MemberLastName,
                                }).ToList();

                memberNames = (from x in members
                                select new MemberNames
                                {
                                    UserId = x.UserId,
                                    MemberFirstLast = x.MemberFirstLast,
                                }).ToList();

                memberSectionsView.DataSource = members;
                memberSectionsView.DataBind();
            }
            
        }

        protected void onUserRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                int userId = userIds[e.Row.RowIndex];

                Button button = (Button)e.Row.FindControl("deleteUser");
                button.CommandArgument = userId.ToString();
            }
        }

        protected void onSectionRowDataBound(object sender, GridViewRowEventArgs e)
        {     
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int userId = memberNames[e.Row.RowIndex].UserId;
                // Populate gridview
                {
                    GridView sectionsView = (GridView)e.Row.FindControl("sectionsView");

                    var sections = (from x in db.Sections
                                    where x.Member_ID == userId
                                    select new
                                    {
                                        SectionID = x.SectionID,
                                        SectionName = x.SectionName,
                                        StartDate = x.SectionStartDate,
                                        InstructorName =
                                            (from y in db.Instructors where x.Instructor_ID == y.InstructorID select y).First().InstructorFirstName + " " +
                                            (from y in db.Instructors where x.Instructor_ID == y.InstructorID select y).First().InstructorLastName,
                                        SectionFee = x.SectionFee,
                                    });

                    sectionsView.DataSource = sections;
                    sectionsView.DataBind();
                }


                // Populate dropdown and button
                {
                    DropDownList sectionsDropDown = (DropDownList)e.Row.FindControl("sectionsDropDown");

                    var sections = (from x in db.Sections select x);

                    sectionsDropDown.DataSource = sections;
                    sectionsDropDown.DataTextField = "SectionName";
                    sectionsDropDown.DataValueField = "SectionID";
                    sectionsDropDown.DataBind();

                    Button button = (Button)e.Row.FindControl("assignSection");
                    button.CommandArgument = e.Row.RowIndex+","+userId;
                }
            }
        }

        private bool check_validation()
        {
            return userNameValidator.IsValid
                && passwordValidator.IsValid
                && firstNameValidator.IsValid
                && lastNameValidator.IsValid
                && phoneValidator.IsValid
                && emailValidator.IsValid;
        }

        protected void userDelete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int userId = int.Parse(button.CommandArgument);
            NetUser user = (from x in db.NetUsers where x.UserID == userId select x).First();

            switch (user.UserType) {
                case "Member":
                    Member member = (from x in db.Members where x.Member_UserID == userId select x).First();
                    db.Members.DeleteOnSubmit(member);
                    break;
                case "Instructor":
                    Instructor instructor = (from x in db.Instructors where x.InstructorID == userId select x).First();
                    db.Instructors.DeleteOnSubmit(instructor);
                    break;
                default:
                    return;
            }
            db.NetUsers.DeleteOnSubmit(user);
            db.SubmitChanges();

            refreshGridViews();
        }

        protected void userAdd_Click(object sender, EventArgs e)
        {
            if ((from x in db.NetUsers select x).Count() == 1 || !check_validation()) // Make sure this user doesn't already exist and validation has passed
            {
                return;
            }

            NetUser user = new NetUser();

            switch(userType.Text)
            {
                case "Member":
                    Member member = new Member();
                    member.MemberFirstName = firstName.Text;
                    member.MemberLastName = lastName.Text;
                    member.MemberDateJoined = DateTime.Parse(dateJoined.Text);
                    member.MemberPhoneNumber = phone.Text;
                    member.MemberEmail = email.Text;

                    user.Member = member;
                    user.UserType = userType.Text;
                    break;

                case "Instructor":
                    Instructor instructor = new Instructor();
                    instructor.InstructorFirstName = firstName.Text;
                    instructor.InstructorLastName = lastName.Text;
                    instructor.InstructorPhoneNumber = phone.Text;

                    user.Instructor = instructor;
                    user.UserType = userType.Text;
                    break;

                case "Administrator":
                    user.UserType = "Administrator";
                    break;

                default:
                    throw new Exception("Invalid userType");
            }
            
            user.UserName = userName.Text;
            user.UserPassword = password.Text;

            db.NetUsers.InsertOnSubmit(user);
            db.SubmitChanges();

            refreshGridViews();
        }

        protected void assignSection_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string[] arguments = button.CommandArgument.Split(',');
            int rowIndex = int.Parse(arguments[0]);
            int userId = int.Parse(arguments[1]);
            DropDownList sectionList = (DropDownList)memberSectionsView.Rows[rowIndex].FindControl("sectionsDropDown");

            int sectionId = int.Parse(sectionList.SelectedValue);

            Section section = (from x in db.Sections where x.SectionID == sectionId select x).First();
            section.Member_ID = userId;

            db.SubmitChanges();

            refreshGridViews();
        }

        protected void firstNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (userType.Text == "Member" || userType.Text == "Instructor")
            {
                args.IsValid = firstName.Text.Length > 0;
            } 
            else
            {
                args.IsValid = true;
            }
        }

        protected void lastNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (userType.Text == "Member" || userType.Text == "Instructor")
            {
                args.IsValid = lastName.Text.Length > 0;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void dateJoinedValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (userType.Text == "Member")
            {
                args.IsValid = dateJoined.Text.Length > 0;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void phoneValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (userType.Text == "Member" || userType.Text == "Instructor")
            {
                args.IsValid = phone.Text.Length > 0;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void emailValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (userType.Text == "Member")
            {
                args.IsValid = email.Text.Length > 0;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}