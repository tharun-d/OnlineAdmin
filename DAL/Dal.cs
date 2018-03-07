using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Entities;
using Common;
using ExcelDataReader;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;
namespace DAL
{
    public class Dal
    {
        string connection = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        CommonCls com = new CommonCls();

        public string ValidateUser(Entitycls log)
        {
            string UserName = log.UserName;
            string Password = log.Password;
            if (UserName == "" && Password == "")
                return "Please enter username and password";
            else if (UserName == "" && Password != "")
                return "Please enter Username";
            else if (UserName != "" && Password == "")
                return "Please enter Password";
            else
            {
                SqlCommand sda;
                SqlConnection con = new SqlConnection(connection);

                con.Open();
                sda = new SqlCommand(com.Adminvalid, con);
                SqlParameter p1 = new SqlParameter("@adminname", log.UserName);
                SqlParameter p2 = new SqlParameter("@adminpassword", log.Password);
                sda.Parameters.Add(p1);
                sda.Parameters.Add(p2);

                SqlDataReader dr = sda.ExecuteReader();
                while (dr.HasRows)
                {
                    return "Success";
                }
                con.Close();
                return "Please provide a valid username and password";
            }
        }
        public string[] HistoryExamFinal()
        {

            SqlConnection con1 = new SqlConnection(connection);
            con1.Open();
            SqlCommand cmd1 = new SqlCommand(com.get_rowcount, con1);
            SqlDataReader dr1 = cmd1.ExecuteReader();


            int k = 0;
            while (dr1.Read())
            {
                k = Convert.ToInt32(dr1[0]);
            }
            dr1.Close();
            SqlCommand cmd2 = new SqlCommand(com.get_selectcourse, con1);

            SqlDataReader dr2 = cmd2.ExecuteReader();


            int p = 0;
            string[] a = new string[k];

            while (dr2.Read())
            {

                a[p] = dr2[0].ToString();
                p++;
            }
            con1.Close();
            return a;


        }
        //public List<History> History(string SubjectName)
        //{

        //        SqlConnection con = new SqlConnection(connection);
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(com.Get_Details_Student, con);
        //        SqlParameter p = new SqlParameter("@one", add.StartDate);
        //        cmd.Parameters.Add(p);
        //        SqlParameter p1 = new SqlParameter("@two", add.EndDate);
        //        cmd.Parameters.Add(p1);
        //        SqlParameter p2 = new SqlParameter("@three", add.Category);
        //        cmd.Parameters.Add(p2);
        //        SqlParameter p3 = new SqlParameter("@four", add.CouseName);
        //        cmd.Parameters.Add(p3);


        //        SqlDataReader dr1 = cmd.ExecuteReader();

        //        List<History> li = new List<History>();

        //        while (dr1.Read())
        //        {

        //            History h = new History()
        //            {
        //                StudentName = Convert.ToString(dr1[0]),
        //                Marks = Convert.ToString(dr1[1]),
        //                Category = Convert.ToString(dr1[2]),
        //                Date = Convert.ToString(dr1[3]),
        //            };
        //            li.Add(h);

        //        }
        //        con.Close();
        //        return li;
        //    }

        //    }
        //}
        public List<History> History(string SubjectName)
        {

            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand(com.ExamsHistoryOfAll, con);
            SqlParameter p = new SqlParameter("@SubjectName", SubjectName);
            cmd.Parameters.Add(p);

            SqlDataReader dr = cmd.ExecuteReader();

            List<History> ListOfStudents = new List<History>();

            while (dr.Read())
            {

                History h = new History()
                {
                    UserName = Convert.ToString(dr[0]),
                    SubjectName = Convert.ToString(dr[1]),
                    DateOfExam = Convert.ToDateTime(dr[2]),
                    Percentage = Convert.ToDouble(dr[3]),
                    StatusOfExam=Convert.ToString(dr[4])
                };
                ListOfStudents.Add(h);

            }
            con.Close();
            return ListOfStudents;
        }

        public void UploadToDataBase(string SubjectName, string FilePath)
        {
            FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            if (reader.Name == "Sheet1")
            {
                reader.Read();
                SqlConnection con = new SqlConnection(connection);
                while (reader.Read())
                {
                    int NumberOfQuestions;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("InsertIntoQuestionsTable @QuestionNumber,@Question,@Option1,@Option2,@Option3,@Option4,@CorrectOption,@SubjectName", con);
                    cmd.Parameters.AddWithValue("@QuestionNumber", reader.GetInt16(0));
                    cmd.Parameters.AddWithValue("@Question", reader.GetString(1));
                    cmd.Parameters.AddWithValue("@Option1", reader.GetString(2));
                    cmd.Parameters.AddWithValue("@Option2", reader.GetString(3));
                    cmd.Parameters.AddWithValue("@Option3", reader.GetString(4));
                    cmd.Parameters.AddWithValue("@Option4", reader.GetString(5));
                    cmd.Parameters.AddWithValue("@CorrectOption", reader.GetString(6));
                    cmd.Parameters.AddWithValue("@SubjectName", SubjectName);
                    NumberOfQuestions = cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
        }
        public List<string> GettingSubjects()
        {
            List<string> Subjects = new List<String>();
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("GettingSubjects", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Subjects.Add(Convert.ToString(dr[0]));
            }
            con.Close();
            return Subjects;
        }
        public string DeleteSubject(string SubjectName)
        {
            {
                int i;
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteSubject @SubjectName", con);
                SqlParameter p = new SqlParameter("@SubjectName", SubjectName);
                cmd.Parameters.Add(p);
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return e.Message; ;
                }
                con.Close();
                return "The course " + SubjectName + " is successfully deleted";
            }
        }
        
    }
}