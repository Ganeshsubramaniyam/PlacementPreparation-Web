Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Configuration
Imports System.Exception
Public Class PlacementPreparation_DLlayer

    'Dim ConnectionString As String = " Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Ganesh\Documents\Visual Studio 2012\Projects\PlacementPreparation\PlacementPreparation\View\Backend.mdf;Integrated Security=True;"
    Dim ConnectionString As String = ConfigurationManager.ConnectionStrings("backendconnection").ToString()

    Public Function LoginAction(ByVal empid As String, ByVal password As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand
        Dim read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "loginAction"
        cmdlogin.Parameters.AddWithValue("emailid", empid)
        cmdlogin.Parameters.AddWithValue("password", password)
        read = cmdlogin.ExecuteReader()
        Return read
        
    End Function

    Public Function insertcontactusdetails(ByVal userid As String, ByVal emailid As String, ByVal description As String, ByVal type_data As String) As Integer

        Dim con As New SqlConnection()
        Dim retval As Integer

        con.ConnectionString = ConnectionString
        con.Open()
        Dim cmdlogin As New SqlCommand()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "Contactus_insert"
        cmdlogin.Parameters.AddWithValue("userid", userid)
        cmdlogin.Parameters.AddWithValue("emailid", emailid)
        cmdlogin.Parameters.AddWithValue("description", description)
        cmdlogin.Parameters.AddWithValue("raiseddate", Convert.ToString(DateTime.Today))
        cmdlogin.Parameters.AddWithValue("type", type_data)
        retval = cmdlogin.ExecuteNonQuery()
        Return retval
        
    End Function

    Public Function insertprofiledata(ByVal name As String, ByVal dob As String, ByVal gender As String, ByVal displayname As String, ByVal emailid As String, ByVal password As String, ByVal contactno As String, ByVal educational_qualification As String, ByVal occupation As String)
        Dim con As New SqlConnection()
        Dim retval As Integer

        con.ConnectionString = ConnectionString
        con.Open()
        Dim cmdprofiledata As New SqlCommand()
        cmdprofiledata.Connection = con
        cmdprofiledata.CommandType = CommandType.StoredProcedure
        cmdprofiledata.CommandText = "Profile_data_Insert"
        cmdprofiledata.Parameters.AddWithValue("name", name)
        cmdprofiledata.Parameters.AddWithValue("dob", dob)
        cmdprofiledata.Parameters.AddWithValue("gender", gender)
        cmdprofiledata.Parameters.AddWithValue("displayname", displayname)
        cmdprofiledata.Parameters.AddWithValue("emailid", emailid)
        cmdprofiledata.Parameters.AddWithValue("password", password)
        cmdprofiledata.Parameters.AddWithValue("contactno", contactno)
        cmdprofiledata.Parameters.AddWithValue("educational_qualification", educational_qualification)
        cmdprofiledata.Parameters.AddWithValue("occupation", occupation)
        cmdprofiledata.Parameters.AddWithValue("profile_created_Date", Convert.ToString(DateTime.Today))
        retval = cmdprofiledata.ExecuteNonQuery()
        Return retval



    End Function

    Public Function insertAddQuestions(
                                      ByVal categorisation As String,
                                      ByVal department As String,
                                      ByVal Question As String,
                                      ByVal option1 As String,
                                      ByVal option2 As String,
                                      ByVal option3 As String,
                                      ByVal option4 As String,
                                      ByVal answer As String,
                                      ByVal explanation As String,
                                      ByVal createdby As String)
        Dim con As New SqlConnection()
        Dim retval As Integer

        con.ConnectionString = ConnectionString
        con.Open()
        Dim cmddata As New SqlCommand()
        cmddata.Connection = con
        cmddata.CommandType = CommandType.StoredProcedure

        cmddata.CommandText = "addquestions"
        cmddata.Parameters.AddWithValue("categorisation", categorisation)
        cmddata.Parameters.AddWithValue("department", department)
        cmddata.Parameters.AddWithValue("Question", Question)
        cmddata.Parameters.AddWithValue("option1", option1)
        cmddata.Parameters.AddWithValue("option2", option2)
        cmddata.Parameters.AddWithValue("option3", option3)
        cmddata.Parameters.AddWithValue("option4", option4)
        cmddata.Parameters.AddWithValue("answer", answer)
        cmddata.Parameters.AddWithValue("explanation", explanation)
        cmddata.Parameters.AddWithValue("createdby", createdby)
        cmddata.Parameters.AddWithValue("createddate", Now.Date.ToString())
        retval = cmddata.ExecuteNonQuery()
        con.Close()
        Return retval



    End Function


    ' calculate the MD5 hash of a given string 
    ' the string is first converted to a byte array
    Public Function Convertpassword(ByVal strData As String) As String

        Dim objMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim arrData() As Byte
        Dim arrHash() As Byte

        ' first convert the string to bytes (using UTF8 encoding for unicode characters)
        arrData = System.Text.Encoding.UTF8.GetBytes(strData)

        ' hash contents of this byte array
        arrHash = objMD5.ComputeHash(arrData)

        ' thanks objects
        objMD5 = Nothing

        ' return formatted hash
        Return ByteArrayToString(arrHash)

    End Function


    Private Function ByteArrayToString(ByVal arrInput() As Byte) As String

        Dim strOutput As New System.Text.StringBuilder(arrInput.Length)

        For i As Integer = 0 To arrInput.Length - 1
            strOutput.Append(arrInput(i).ToString("X2"))
        Next

        Return strOutput.ToString().ToLower

    End Function

    Public Function GetProfileName(ByVal userid As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand
        Dim read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "getprofilename"
        cmdlogin.Parameters.AddWithValue("userid", userid)
        read = cmdlogin.ExecuteReader()
        read.Read()
        Return read(0).ToString()




    End Function

    Public Function getDepartmentCategorisation(ByVal department As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand
        Dim read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "getdepartmentcategorisation"
        cmdlogin.Parameters.AddWithValue("department", department)
        read = cmdlogin.ExecuteReader()
        Return read



    End Function

    Public Function postathread(ByVal categorisation As String, ByVal threadtitle As String, ByVal desc As String, ByVal userid As String)

        Dim con As New SqlConnection()
        Dim retval As Integer

        con.ConnectionString = ConnectionString
        con.Open()
        Dim cmdlogin As New SqlCommand()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "Forum_post_thread"
        cmdlogin.Parameters.AddWithValue("categorisation", categorisation)
        cmdlogin.Parameters.AddWithValue("description", desc)
        cmdlogin.Parameters.AddWithValue("userid", userid)
        cmdlogin.Parameters.AddWithValue("threadtitle", threadtitle)
        cmdlogin.Parameters.AddWithValue("posteddate", Convert.ToString(DateTime.Today))
        retval = cmdlogin.ExecuteNonQuery()
        Return retval




    End Function

    Public Function forum_category_results_search(ByVal category As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand
        Dim read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "forumcategorysearch"
        cmdlogin.Parameters.AddWithValue("categoryvalue", category)
        read = cmdlogin.ExecuteReader()
        Return read



    End Function
    Public Function forum_myown_results_search(ByVal userid As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand
        Dim read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "forummyownsearch"
        cmdlogin.Parameters.AddWithValue("userid", userid)
        read = cmdlogin.ExecuteReader()
        Return read



    End Function

    Public Function password_retrieve(ByVal emailid As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand
        Dim read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "RetrievePassword"
        cmdlogin.Parameters.AddWithValue("emailid", emailid)
        read = cmdlogin.ExecuteReader()
        read.Read()
        Return read(0).ToString()



    End Function
    Public Function getquestionsfortest(ByVal department As String, ByVal category As String, ByVal noofquestions As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand
        Dim read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "SelectQuestionForTest"
        cmdlogin.Parameters.AddWithValue("department", department)
        cmdlogin.Parameters.AddWithValue("category", category)
        cmdlogin.Parameters.AddWithValue("noofquestions", noofquestions)
        read = cmdlogin.ExecuteReader()
        Return read



    End Function
    Public Function getforum_content_data(ByVal forum_data_id As Integer, ByVal options As Integer)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand
        Dim read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "thread_display_Data"
        cmdlogin.Parameters.AddWithValue("forum_Data", options)
        cmdlogin.Parameters.AddWithValue("threadid", forum_data_id)
        read = cmdlogin.ExecuteReader()
        Return read



    End Function

    Public Function getansforquestion(ByVal questionId As String, ByVal options As Integer)
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "getanswerforid"
        cmd.Parameters.AddWithValue("qid", questionId)
        sql_read = cmd.ExecuteReader()
        If options = 0 Then
            sql_read.Read()
            Return sql_read(6).ToString()
        Else
            Return sql_read
        End If




    End Function

    Public Function insertforumreplydata(ByVal replydata As String, ByVal userid As Integer, ByVal forumid As Integer)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "insertforumreplydata"
        cmdlogin.Parameters.AddWithValue("forumid", forumid)
        cmdlogin.Parameters.AddWithValue("replydata", replydata)
        cmdlogin.Parameters.AddWithValue("userid", userid)
        cmdlogin.Parameters.AddWithValue("posteddate", Convert.ToString(DateTime.Today))
        cmdlogin.Parameters.AddWithValue("ratingvalue", 0)
        cmdlogin.Parameters.AddWithValue("difficultyvalue", 0)
        cmdlogin.ExecuteNonQuery()



    End Function
    Public Function getinterviewmessageboarddata()
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "getinterviewboarddata"
        sql_read = cmd.ExecuteReader()
        Return sql_read



    End Function
    Public Function postinterviewmessagedata(ByVal interviewtitle As String, ByVal interviewdescription As String, ByVal userid As Integer)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "insertinterviewboarddata"
        cmdlogin.Parameters.AddWithValue("interviewtitle", interviewtitle)
        cmdlogin.Parameters.AddWithValue("description", interviewdescription)
        cmdlogin.Parameters.AddWithValue("userid", userid)
        cmdlogin.Parameters.AddWithValue("posteddate", Convert.ToString(DateTime.Today))
        cmdlogin.Parameters.AddWithValue("ratingvalue", 0)
        cmdlogin.ExecuteNonQuery()



    End Function
    Public Function gettestid(ByVal department As String, ByVal categorisation As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "gettestid"
        cmd.Parameters.AddWithValue("Department", department)
        cmd.Parameters.AddWithValue("Categorisation", categorisation)
        sql_read = cmd.ExecuteReader()
        sql_read.Read()
        Return sql_read(0).ToString()



    End Function

    Public Function puttestmarksdata(ByVal testid As Integer, ByVal testmarks As Integer, ByVal totalmarks As Integer, ByVal userid As Integer)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "insertMarksData"
        cmdlogin.Parameters.AddWithValue("teston", testid)
        cmdlogin.Parameters.AddWithValue("userid", userid)
        cmdlogin.Parameters.AddWithValue("marksscored", testmarks)
        cmdlogin.Parameters.AddWithValue("totalmarks", totalmarks)
        cmdlogin.Parameters.AddWithValue("takendate", Convert.ToString(DateTime.Today))
        cmdlogin.ExecuteNonQuery()

    End Function

    Public Function forgotpassword_check_mail(ByVal emailid As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader
        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from profile_data where email_id like '" + emailid + "'"
        sql_read = cmd.ExecuteReader()
        Return sql_read.HasRows

    End Function
    Public Function resetpasswordcheckstatus(ByVal emailid As String, ByVal hashvalue As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader
        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from ResetPassword where email_id like '" + emailid + "' And hashkey like '" + hashvalue + "'"
        sql_read = cmd.ExecuteReader()
        Return sql_read.HasRows

    End Function

    Public Function Updatepassword(ByVal emailid As String, ByVal password_val As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "updatePassword"
        cmdlogin.Parameters.AddWithValue("emailid", emailid)
        cmdlogin.Parameters.AddWithValue("password_val", password_val)
        cmdlogin.Parameters.AddWithValue("updatedate", Convert.ToString(DateTime.Today))
        cmdlogin.ExecuteNonQuery()
        Return True
    End Function
    Public Function getinterviewExperiences()
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "getinterviewexperience"
        sql_read = cmd.ExecuteReader()
        Return sql_read
    End Function
    Public Function postinterviewExperience(ByVal title As String, ByVal companyname As String, ByVal description As String, ByVal userid As Integer)
        Dim con As New SqlClient.SqlConnection
        Dim cmdlogin As New SqlClient.SqlCommand

        con.ConnectionString = ConnectionString
        con.Open()
        cmdlogin.Connection = con
        cmdlogin.CommandType = CommandType.StoredProcedure
        cmdlogin.CommandText = "insertinterviewexperience"
        cmdlogin.Parameters.AddWithValue("title", title)
        cmdlogin.Parameters.AddWithValue("companyname", companyname)
        cmdlogin.Parameters.AddWithValue("description", description)
        cmdlogin.Parameters.AddWithValue("userid", userid)
        cmdlogin.Parameters.AddWithValue("posteddate", Convert.ToString(DateTime.Today))
        cmdlogin.Parameters.AddWithValue("ratingvalue", 0)
        cmdlogin.ExecuteNonQuery()

    End Function
    Public Function getcompanynames()
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "getcompanynames"
        sql_read = cmd.ExecuteReader()
        Return sql_read
    End Function
    Public Function gettutorialcategory()
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SelectTutorialCategory"
        sql_read = cmd.ExecuteReader()

        Return sql_read
    End Function
    Public Function gettutorialiddata(ByVal id As Integer)
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "selecttutorialcategorydata"
        cmd.Parameters.AddWithValue("in_id", id)
        sql_read = cmd.ExecuteReader()

        Return sql_read
    End Function

    Public Function getprofiledata(ByVal id As Integer)
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand
        Dim sql_read As SqlClient.SqlDataReader

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "selectprofiledata"
        cmd.Parameters.AddWithValue("profile_id", id)
        sql_read = cmd.ExecuteReader()

        Return sql_read
    End Function
    Public Function updateprofiledata(ByVal id As Long, ByVal firstname As String, ByVal dob As String, ByVal gender As String, ByVal displayname As String, ByVal educationq As String, ByVal skills As String, ByVal mailid As String, ByVal mobileno As String)
        Dim con As New SqlClient.SqlConnection
        Dim cmd As New SqlClient.SqlCommand

        con.ConnectionString = ConnectionString
        con.Open()
        cmd.Connection = con
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "updateprofiledata"
        cmd.Parameters.AddWithValue("profile_id", id)
        cmd.Parameters.AddWithValue("firstname", firstname)
        cmd.Parameters.AddWithValue("dob", dob)
        cmd.Parameters.AddWithValue("gender", gender)
        cmd.Parameters.AddWithValue("dname", displayname)
        cmd.Parameters.AddWithValue("eduq", educationq)
        cmd.Parameters.AddWithValue("skils", skills)
        cmd.Parameters.AddWithValue("mailid", mailid)
        cmd.Parameters.AddWithValue("mobileno", mobileno)
        cmd.Parameters.AddWithValue("updatedby", id.ToString())
        cmd.Parameters.AddWithValue("updateddate", DateTime.Today.ToString())
        cmd.ExecuteReader()

        Return 0
    End Function
End Class
