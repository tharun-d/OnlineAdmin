using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Entities;
using DAL;
using System.Data.SqlClient;
namespace Business_Layer
{
    public class BLayerCls
    {
        Entitycls entity_obj = new Entitycls();
        AddCourse Add_obj = new AddCourse();
        FileUpload File_obj = new FileUpload();
        Dal dal_obj = new Dal();
        History Hist_obj = new History();
        public string Validate(string UserName, string Password)
        {
            entity_obj.UserName = UserName;
            entity_obj.Password = Password;
            string userval = dal_obj.ValidateUser(entity_obj);
            return userval; 
        }
        public string[] ExamHistory()
        {
            string[] Userval = dal_obj.HistoryExamFinal();
            return Userval;
        }
        public List<History> DisplayHistory(string SubjectName)
        {
            return dal_obj.History(SubjectName);
        }
        public void UploadToDataBase(string SubjectName,string FilePath)
        {
            dal_obj.UploadToDataBase(SubjectName,FilePath);
        }
        public List<string> GettingSubjects()
        {
            return dal_obj.GettingSubjects();
        }
        public string DeleteSubject(string SubjectName)
        {
            return dal_obj.DeleteSubject(SubjectName);
        }
    }
}
