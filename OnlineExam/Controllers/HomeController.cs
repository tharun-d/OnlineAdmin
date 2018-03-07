using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using System.Data;
using Business_Layer;
using Business_Entities;
using OnlineExam.Models;

namespace OnlineExam.Controllers
{
    public class HomeController : Controller
    {
        BLayerCls b = new BLayerCls();
       
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(string UserName, string Password)
        {   
            string result = b.Validate(UserName, Password);
            ViewBag.Message = result;
            if (result== "Success")
            {
                     Session["user"] = UserName;
                     return RedirectToAction("AdminPage");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult AdminPage()
        {
            if (Session["user"]==null)
            {
                 return RedirectToAction("AdminLogin");
            }
            else
            {
                return View();
            }
        }
        public ActionResult AddSubject()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AddSubject(string SubjectName,string examlevel,HttpPostedFileBase ExcelFile)
        {
            bool ErrorOccured = false;
            string _path = null;
            try
            {
                if (ExcelFile.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(ExcelFile.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadFiles/"), _FileName);
                    ExcelFile.SaveAs(_path);
                }
                ViewBag.Message = "File uploaded successfully";               
            }
            catch (Exception ex)
            {
                ErrorOccured = true;
                ViewBag.Message = ex.Message;
                return View();
            }
            finally
            {
                if (ErrorOccured==false && _path!=null)
                {
                    UploadToDataBase(SubjectName,examlevel,_path);
                }
            }
            return View();
        }
        public void UploadToDataBase(string SubjectName,string examlevel, string FilePath)
        {
            FileStream stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            if (reader.Name == "Sheet1")
            {
                reader.Read();
                SqlConnection con = new SqlConnection("Server=DELL-PC\\THARUN;Integrated Security=sspi;database=OnlineExam");
                while (reader.Read())
                {
                    int NumberOfQuestions;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("InsertIntoQuestionsTable @QuestionNumber,@Question,@Option1,@Option2,@Option3,@Option4,@CorrectOption,@SubjectName,@examlevel", con);
                    cmd.Parameters.AddWithValue("@QuestionNumber", reader.GetDouble(0));
                    cmd.Parameters.AddWithValue("@Question", reader.GetString(1));
                    cmd.Parameters.AddWithValue("@Option1", reader.GetString(2));
                    cmd.Parameters.AddWithValue("@Option2", reader.GetString(3));
                    cmd.Parameters.AddWithValue("@Option3", reader.GetString(4));
                    cmd.Parameters.AddWithValue("@Option4", reader.GetString(5));
                    cmd.Parameters.AddWithValue("@CorrectOption", reader.GetDouble(6));
                    cmd.Parameters.AddWithValue("@SubjectName", SubjectName);
                    cmd.Parameters.AddWithValue("@examlevel", examlevel);
                    NumberOfQuestions = cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
        }
        public ActionResult UpdateQuestions()
        {
            
            if (Session["user"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            else
            {
                List<string> Subjects = b.GettingSubjects();
                ViewBag.Subjects = Subjects;
                return View("UpdateQuestions");
            }
        }
        [HttpPost]
        public ActionResult UpdateQuestions(string SubjectName,string examlevel, HttpPostedFileBase ExcelFile)
        {
            bool ErrorOccured = false;
            string _path = null;
            try
            {
                if (ExcelFile.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(ExcelFile.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadFiles/"), _FileName);
                    ExcelFile.SaveAs(_path);
                }
                ViewBag.Message = "File uploaded successfully";
            }
            catch (Exception)
            {
                ErrorOccured = true;
                ViewBag.Message = "File not uploaded successfully";
                return View();
            }
            finally
            {
                if (ErrorOccured == false && _path != null)
                {
                    b.DeleteSubject(SubjectName);
                    UploadToDataBase(SubjectName,examlevel, _path);
                }
            }
            return View();
        }
        public ActionResult DeleteSubject()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("AdminLogin");
            }
            else           
            {
                List<string> Subjects = b.GettingSubjects();
                ViewBag.Subjects = Subjects;
                return View("DeleteSubject");
            }

        }
        [HttpPost]
        public ActionResult DeleteSubject(string SubjectName)
        {
            string Result = b.DeleteSubject(SubjectName);

            ViewBag.Message = Result;
            
            return View();
        }       
        public ActionResult ExamHistory()
        {
            if (Session["user"] == null)
                return RedirectToAction("AdminLogin");
            else
            {
                List<string> Subjects = b.GettingSubjects();
                ViewBag.Subjects = Subjects;
                return View("ExamHistory");
            }
        }
        [HttpPost]
        public ActionResult ExamHistory(string SubjectName)
        {
            List<History> ListOfStudents = b.DisplayHistory(SubjectName);
            ViewBag.List = ListOfStudents;
            return View();   
        }
        public ActionResult AdminLogout()
        {
            Session.Abandon();
            return RedirectToAction("AdminLogin");
        }
        

    }
}
       
    


         

    

