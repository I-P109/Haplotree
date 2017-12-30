Imports System.IO

Module GenFunc

    Public Sub WriteLogEntry(ByVal vstrLineToWrite As String)
        Dim strDateTime As String = String.Format("{0:G}", Now)
        Dim strFileName = App_Path() & "\" & "LogFile.txt"
        Dim str As New FileStream(strFileName, FileMode.Append)
        Dim Report As New StreamWriter(str)

        If vstrLineToWrite.Trim = "" Then
            Report.WriteLine(vstrLineToWrite)
        Else
            Report.WriteLine(vstrLineToWrite)
        End If
        Report.Close()
    End Sub


    '************************************************************************************************************
    ' - AUTHOR: Darin Flansburg 
    ' - DATE:   11/07/2011
    ' - NOTES:  This Function gets the path where the application is currently residing
    '************************************************************************************************************
    Public Function App_Path() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory()
    End Function


    Public Sub CreateDirectory(ByVal vstrDirectory As String)
        If (Not System.IO.Directory.Exists(vstrDirectory)) Then
            System.IO.Directory.CreateDirectory(vstrDirectory)
        End If
    End Sub




    '==================================================================================================
    'THIS IS FOR THE LISTVIEW.  THIS SORTS BY COLUM
    '==================================================================================================
    ' Implements the manual sorting of items by column.
    Class ListViewItemComparer
        Implements IComparer
        Private col As Integer
        Public Sub New()
            col = 0
        End Sub
        Public Sub New(ByVal column As Integer)
            col = column
        End Sub
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
                                Implements System.Collections.IComparer.Compare
            Dim returnVal As Integer = -1
            returnVal = [String].Compare(CType(x,  _
                            ListViewItem).SubItems(col).Text, _
                            CType(y, ListViewItem).SubItems(col).Text)
            Return returnVal
        End Function
    End Class
End Module
