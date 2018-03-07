using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommonCls
    {
        public readonly string status_change = "statuschange @one";
        public readonly string admin_status = "adminstatus";
        public readonly string get_Cname = "getcoursename @one";
        public readonly string get_rowcount = "getrowcount";
        public readonly string insert_cname = "insertcname @rowcount,@name";
        public readonly string get_selectcourse = "selectcourse";
        public readonly string delete_course = "deletecoursename @name";
        public readonly string select_courseId = "selectcourseid";
        public readonly string select_MaxCourseId = "MaxCourseID";
        public readonly string Get_Details_Student= "getdetailsstudent @one,@two,@three,@four";
        public readonly string Get_Details_Student_small = "getdetailsstudentsmall @one,@two,@three";
        public readonly string ExamsHistoryOfAll = "ExamsHistoryOfAll @SubjectName";
        public readonly string Adminvalid = "adminvalid @adminname,@adminpassword";

    }
}
