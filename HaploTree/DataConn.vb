Imports System.Data.OleDb
Module DataConn

    '***************************************************************************************************
    ' - AUTHOR: Darin Flansburg
    ' - DATE:   6/20/2017
    ' - NOTES:  This connects to the access database which contains the document information.

    '***************************************************************************************************
    Public Function GetConnection() As OleDb.OleDbConnection
        Dim strConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & App_Path() & "VariantDB.accdb;Persist Security Info=False"

        Try
            conn = New OleDbConnection
            conn.ConnectionString = strConnectionString
            Return conn
        Catch ex As Exception
            ' Throws a new exception that provides information in the message
            ' string and provides the original exception as the InnerException, e.
            ' Call WriteToLogFile(Now, "DataConn:GetConnection",ex.Message) 'Wite to the Logfile
            Throw ex

        End Try
    End Function


    '***************************************************************************************************
    ' - AUTHOR: Darin Flansburg
    ' - DATE:   6/20/2017
    ' - NOTES:  This connects to the access database which contains the document information.

    '***************************************************************************************************
    Public Function GetConnectionHaploTreeDB() As OleDb.OleDbConnection
        Dim strConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & App_Path() & "HaploTreeDB.accdb;Persist Security Info=False"

        Try
            conn = New OleDbConnection
            conn.ConnectionString = strConnectionString
            Return conn
        Catch ex As Exception
            ' Throws a new exception that provides information in the message
            ' string and provides the original exception as the InnerException, e.
            ' Call WriteToLogFile(Now, "DataConn:GetConnection",ex.Message) 'Wite to the Logfile
            Throw ex

        End Try
    End Function


    '***************************************************************************************************
    ' - AUTHOR: Darin Flansburg
    ' - DATE:   6/20/2017
    ' - NOTES:  This connects to the access database which contains the document information.

    '***************************************************************************************************
    Public Function GetConnectionDocFiles() As OleDb.OleDbConnection
        Dim strConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & App_Path() & "Haplotree.accdb;Persist Security Info=False"

        Try
            connDoc = New OleDbConnection
            connDoc.ConnectionString = strConnectionString
            Return connDoc
        Catch ex As Exception
            ' Throws a new exception that provides information in the message
            ' string and provides the original exception as the InnerException, e.
            ' Call WriteToLogFile(Now, "DataConn:GetConnection",ex.Message) 'Wite to the Logfile
            Throw ex

        End Try
    End Function
End Module
