create database OnlineExam

create table userdet(uname varchar(50),pwd varchar(50))

insert into userdet values('venu','venu05')
alter  procedure validuser(@uname varchar(50),@pwd varchar(50))
as 
begin
select * from userregister where username=@uname and passwrd=@pwd
end

drop procedure validuser
validuser venu,venu05
select * from userregister

create table userregister(firstname varchar(50),lastname varchar(50),username varchar(50),passwrd varchar(50),gender varchar(50),email varchar(100),phno varchar(100),defaultstatus int)
drop table userregister
create procedure insertuser(@fname varchar(50),@lname varchar(50),@uname varchar(50),@pwd varchar(50),@gender varchar(50),@email varchar(100),@phno varchar(100),@def int)
as 
begin
insert into userregister values(@fname,@lname,@uname,@pwd,@gender,@email,@phno,@def)
end

delete  from userregister
drop procedure insertuser

alter procedure get_user(@username varchar(50))
as 
begin
select * from userregister where username=@username
end
drop procedure get_user
get_user venuprasad
alter procedure get_email(@email varchar(100))
as
begin
select * from userregister where email=@email
end

alter procedure get_phno(@phno varchar(100))
as
begin
select * from userregister where phno=@phno
end

create procedure sp_getdetails(@username varchar(50),@Email varchar(100),@Phno varchar(100))as
begin
select * from userregister where username=@username and email=@Email and Phno=@Phno
end

create procedure Sp_changepassword(@username varchar(50),@pwd varchar(50))as
begin
update userregister set passwrd=@pwd where username=@username
end

select * from userregister
create procedure gettingdetails(@username varchar(50))as
begin
select firstname,lastname,passwrd,email,Phno from userregister where username=@username
end

create procedure editprofile(@FirstName varchar(50),@LastName varchar(50),@Pwd varchar(50),@Email varchar(50),@Phno varchar(50),@UserName varchar(50))as
begin
update userregister 
set firstname=@FirstName,lastname=@LastName,passwrd=@Pwd,Email=@Email,Phno=@phno,username=@UserName
where username=@username
end

---------------------------Admin------------
create  table QuestionsTable
(
QuestionNumber int,
Question varchar(max),
Option1 varchar(max),
Option2 varchar(max),
Option3 varchar(max),
Option4 varchar(max),
CorrectOption int,
SubjectName varchar(max)
)

alter table QuestionsTable add  examlevel varchar(max);
select * from QuestionsTable;

alter procedure InsertIntoQuestionsTable
(
@QuestionNumber int,
@Question varchar(max),
@Option1 varchar(max),
@Option2 varchar(max),
@Option3 varchar(max),
@Option4 varchar(max),
@CorrectOption int,
@SubjectName varchar(max),
@examlevel varchar(max)
)
as
begin
insert into QuestionsTable values(@QuestionNumber,@Question,@Option1,@Option2,@Option3,@Option4,@CorrectOption,@SubjectName,@examlevel)
end
select * from questionstable

create procedure GettingSubjects as
begin 
select distinct SubjectName from QuestionsTable
end

create procedure DeleteSubject(@SubjectName varchar(max))as
begin
delete from QuestionsTable where subjectname=@SubjectName
end

create procedure getcoursecount
as
begin
select  COUNT(distinct (SubjectName)) from QuestionsTable
end
drop procedure getcoursecount
getcoursecount
select * from QuestionsTable

create procedure set_user_status_to_zero(@name varchar(50))
as
begin
update userregister set defaultstatus=0 where username=@name
end
set_user_status_to_zero 'venuprasad'
create procedure set_userstatus (@name varchar(50))
as
begin
update userregister set defaultstatus=1 where username=@name
end

create procedure get_status (@name varchar(50))
as
begin
select defaultstatus from userregister where username=@name
end

create table UpdateReportsTable(
									UserName varchar(max),
									DateofExam datetime,
									SubjectName varchar(max),
									CorrectAnswers int,
									Percentage float,
									StatusOfExam varchar(max)
								)

create procedure UpdateReportsTableProcedure(
										@UserName varchar(max),
										@DateofExam datetime,
										@SubjectName varchar(max),
										@CorrectAnswers int,
										@Percentage float,
										@StatusOfExam varchar(max)
									)
as
begin
insert into UpdateReportsTable values(@UserName,@DateofExam,@SubjectName,@CorrectAnswers,@Percentage,@StatusOfExam)
end
select * from QuestionsTable

alter procedure RandomQuestions(@SubjectName varchar(max))
as
begin
select a.* from (select top 5 Question,Option1,Option2,Option3,Option4,CorrectOption from QuestionsTable where SubjectName='Hadoop'/*@SubjectName*/ and examlevel='Hard' ORDER BY NEWID()) as a
UNION all
select b.* from (select top 5 Question,Option1,Option2,Option3,Option4,CorrectOption from QuestionsTable where SubjectName='Hadoop'/*@SubjectName*/ and examlevel='Easy' order by NEWID()) as b
end
select * from QuestionsTable

RandomQuestions 'Hadoop'

select * from UpdateReportsTable

create procedure ExamsHistory(@UserName varchar(max))
as
begin
select SubjectName,DateOfExam,Percentage,StatusOfExam from UpdateReportsTable where UserName=@UserName order by DateOfExam
end

create procedure ExamsHistoryOfAll(@SubjectName varchar(max))
as
 
select Username,SubjectName,DateOfExam,Percentage,StatusOfExam from UpdateReportsTable where SubjectName=@SubjectName
order by DateOfExam
end

studenthistory 'venuprasad'


create procedure validateexamstatus(@username varchar(max),@subjectname varchar(max))
as
begin
select DateOfExam from UpdateReportsTable where UserName=@username and SubjectName=@subjectname



create table Admintable (adminname varchar(max),adminpassword varchar(max));

insert into Admintable values ('admin','admin123');

alter procedure Adminvalid (@adminname varchar(max),@adminpassword varchar(max))
as
begin
select * from Admintable where adminname=@adminname and adminpassword=@adminpassword;
end

adminvalid 'admin','admin123'


create procedure changeadminpass (@adminname varchar(max),@adminpassword varchar(max))
as
begin
update Admintable set adminpassword=@adminpassword where adminname=@adminname;
end

select * from admintable
changeadminpass 'admin','admin123'