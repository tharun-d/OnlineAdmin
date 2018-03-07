using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Entities;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Configuration;

namespace DALTest
{
    [TestClass]
    public class DALTesting
    {

        Entitycls entity_ob = new Entitycls();
        SqlConnection con = new SqlConnection("server=172.16.170.183;uid=sa;pwd=Passw0rd@12;database=OnlineQuiz");
        Dal data_obj = new Dal();

        [TestMethod]

        public void validateuser_when_username_password_are_incorrect()
        {
            con.Open();
           
            entity_ob.UserName = "smruti";
            entity_ob.Password = "satapathy";

            string expected = "Please provide a valid username and password";
            string actual = (data_obj.ValidateUser(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void validateuser_when_username_password_are_correct()
        {
            con.Open();
            entity_ob.UserName = "admin";
            entity_ob.Password = "admin123";

            string expected = "Success login";
            string actual = (data_obj.ValidateUser(entity_ob));

            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void validateuser_when_username_incorrect_password_notentered()
        {
            con.Open();
            entity_ob.UserName = "admin";
            entity_ob.Password = "";

            string expected = "Please enter Password";
            string actual = (data_obj.ValidateUser(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void validateuser_when_username_correct_password_notentered()
        {
            con.Open();
            entity_ob.UserName = "";
            entity_ob.Password = "admin123";
            string expected = "Please enter Username";
            string actual = (data_obj.ValidateUser(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void both_course_and_file_are_uploaded()
        {
            con.Open();
            entity_ob.Course = "Java";
            entity_ob.File = "h.xlsx";
            string expected = "already there is course with same name";
            string actual = (data_obj.CourseAdd(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void course_notuploaded_and_file_are_uploaded()
        {
            con.Open();
           
            entity_ob.Course = "";
            entity_ob.File = "b.xlsx";
            string expected = "Please select the correct file for this course";
            string actual = (data_obj.CourseAdd(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void course_uploaded_and_file_are_notuploaded()
        {
            con.Open();
           
            entity_ob.Course = "Java";
            entity_ob.File = "Java.doc";
            string expected = "Please upload only excel File";
            string actual = (data_obj.CourseAdd(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void course_file_are_notuploaded()
        {
            con.Open();
            
            entity_ob.Course = "c++";
            entity_ob.File = "c.xlsx";
            string expected = "Please select the correct file for this course"; ;
            string actual = (data_obj.CourseAdd(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void cname_empty()
        {
            con.Open();
            entity_ob.Course = "h";
            string expected = "The course h is successfully deleted";
            string actual = (data_obj.CourseDeleteFinal(entity_ob));


            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void cnamea_given()
        {
            con.Open();
          
            entity_ob.Course = "c";
            string expected = "The course c is successfully deleted";
            string actual = (data_obj.CourseDeleteFinal(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void questinsupload_course_and_file_given()
        {
            con.Open();
            
            entity_ob.Course = "c";
            entity_ob.File = "b.xlsx";
            string expected = "please select the correct file for this course";
            string actual = (data_obj.QuestionsUploadFinal(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void questinsupload_course_and_file_are_notgiven()
        {
            con.Open();
           
            entity_ob.Course = "";
            entity_ob.File = "";
            string expected = "All fields are mandatory";
            string actual = (data_obj.QuestionsUploadFinal(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void questinsupload_course_given_and_file_notgiven()
        {
            con.Open();
            
            entity_ob.Course = "Java";
            entity_ob.File = "Java.doc";
            string expected = "please upload only excel file";
            string actual = (data_obj.QuestionsUploadFinal(entity_ob));
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void questinsupload_course_notgiven_and_file_given()
        {
            con.Open();
          
            entity_ob.Course = "c";
            entity_ob.File = "b.xlsx";
            string expected = "please select the correct file for this course";
            string actual = (data_obj.QuestionsUploadFinal(entity_ob));
            Assert.AreEqual(expected, actual);

        }



        [TestMethod]
        public void logincheck()
        {
            con.Open();
           entity_ob.File = "b.xlsx";
            bool expected = true;
            bool actual = (data_obj.LoginCheck());
            Assert.AreEqual(expected, actual);

        }

    }
}
