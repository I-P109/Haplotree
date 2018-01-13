Imports System.Data.OleDb
Public Class clsDataAccess
    Public Function GetAllMembers() As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers "
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            dbCommandAccess.Connection.Close()

            Return dataSet
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function GetMemberByID(ByVal mintID As Integer) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers WHERE ID= " & mintID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            dbCommandAccess.Connection.Close()

            Return dataSet
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths


    Public Function GetMembersAll() As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers ORDER BY MemberName "
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            dbCommandAccess.Connection.Close()

            Return dataSet
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function GetMemberByFTDNAID(ByVal vstrFTDNAID As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers WHERE FTDNAID =" & Chr(34) & vstrFTDNAID & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            dbCommandAccess.Connection.Close()

            Return dataSet
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function GetPositionsByMemberID38(ByVal vintMemberID As Integer) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMemberPositions38 WHERE FK_MemberID =" & vintMemberID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            dbCommandAccess.Connection.Close()

            Return dataSet
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function GetSNPByPositionRefAlt(ByVal vintPosition As Integer,
                                           ByVal vstrRef As String,
                                           ByVal vstrAlt As String) As DataSet
        Dim sql As String

        sql = " SELECT tblMemberPositions38.ID,MemberName,Pos,Ref,Alt,Qual,Filter FROM tblMemberPositions38, tblMembers WHERE tblMemberPositions38.FK_MemberID = tblMembers.ID "
        sql = sql & " AND Pos=" & vintPosition
        sql = sql & " AND Ref=" & Chr(34) & vstrRef & Chr(34)
        sql = sql & " AND Alt=" & Chr(34) & vstrAlt & Chr(34)

        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            dbCommandAccess.Connection.Close()

            Return dataSet
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths






    Public Function GetMemberByYFullID(ByVal YFullID As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers WHERE YFullID =" & Chr(34) & YFullID & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            dbCommandAccess.Connection.Close()

            Return dataSet
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function InsertMember(ByVal vstrMemberName As String,
                                 ByVal vstrFTDNAID As String, _
                                 ByVal vstrYFullID As String) As Integer
        Dim sql As String
        Dim rowsAffected As Integer
        Try
            sql = "  INSERT INTO tblMembers (MemberName, FTDNAID, YFullID) VALUES ("
            sql = sql & Chr(34) & vstrMemberName & Chr(34)
            sql = sql & "," & Chr(34) & vstrFTDNAID & Chr(34)
            sql = sql & "," & Chr(34) & vstrYFullID & Chr(34)
            sql = sql & ")"

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionDocFiles()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
#Disable Warning BC42353 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42353 ' Function doesn't return a value on all code paths

    Public Function InsertPositionByMemberID38(ByVal vstrFK_MemberID As Integer,
                                             ByVal vintPosition As Integer,
                                             ByVal strRef As String,
                                             ByVal vstrAlt As String,
                                             ByVal vstrQual As String,
                                             ByVal vstrFilter As String,
                                             ByVal vstrInfo As String,
                                             ByVal vstrMutation As String
                                             ) As Integer

        Dim sql As String
        Dim rowsAffected As Integer
        Try

            sql = "  INSERT INTO tblMemberPositions38 (FK_MemberID, Pos, Ref, Alt, Qual, Filter, Info,  Mutation) VALUES ("

            sql = sql & vstrFK_MemberID
            sql = sql & "," & vintPosition
            sql = sql & "," & Chr(34) & strRef & Chr(34)
            sql = sql & "," & Chr(34) & vstrAlt & Chr(34)
            sql = sql & "," & Chr(34) & vstrQual & Chr(34)
            sql = sql & "," & Chr(34) & vstrFilter & Chr(34)
            sql = sql & "," & Chr(34) & vstrInfo & Chr(34)
            ' sql = sql & "," & Chr(34) & vstrFormat & Chr(34)
            sql = sql & "," & Chr(34) & vstrMutation & Chr(34)
            sql = sql & ")"

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            If conn.State = ConnectionState.Closed Then
                dbCommandAccess.Connection = GetConnection()
                dbCommandAccess.Connection.Open()
                rowsAffected = dbCommandAccess.ExecuteNonQuery()
            Else
                dbCommandAccess.Connection = conn
                rowsAffected = dbCommandAccess.ExecuteNonQuery()
            End If





            '     dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
#Disable Warning BC42353 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42353 ' Function doesn't return a value on all code paths


    Public Function InsertPositionByMemberID19(ByVal vstrFK_MemberID As Integer,
                                             ByVal vintPosition As Integer,
                                             ByVal strRef As String,
                                             ByVal vstrAlt As String,
                                             ByVal vstrQual As String,
                                             ByVal vstrFilter As String,
                                             ByVal vstrInfo As String,
                                             ByVal vstrFormat As String,
                                             ByVal vstrMutation As String) As Integer

        Dim sql As String
        Dim rowsAffected As Integer
        Try

            sql = "  INSERT INTO tblMemberPositions19(FK_MemberID, Pos, Ref, Alt, Qual, Filter, Info, Format, Mutation) VALUES ("

            sql = sql & vstrFK_MemberID
            sql = sql & "," & vintPosition
            sql = sql & "," & Chr(34) & strRef & Chr(34)
            sql = sql & "," & Chr(34) & vstrAlt & Chr(34)
            sql = sql & "," & Chr(34) & vstrQual & Chr(34)
            sql = sql & "," & Chr(34) & vstrFilter & Chr(34)
            sql = sql & "," & Chr(34) & vstrInfo & Chr(34)
            sql = sql & "," & Chr(34) & vstrFormat & Chr(34)
            sql = sql & "," & Chr(34) & vstrMutation & Chr(34)
            sql = sql & ")"

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            If conn.State = ConnectionState.Closed Then
                dbCommandAccess.Connection = GetConnection()
                dbCommandAccess.Connection.Open()
                rowsAffected = dbCommandAccess.ExecuteNonQuery()
            Else
                dbCommandAccess.Connection = conn
                rowsAffected = dbCommandAccess.ExecuteNonQuery()
            End If





            '     dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
#Disable Warning BC42353 ' Function doesn't return a value on all code paths
    End Function








    Public Function UpdateMember(ByVal vstrMemberName As String,
                                 ByVal vstrFTDNAID As String,
                                 ByVal vstrYFullID As String,
                                 ByVal vintID As Integer) As Integer
        Dim sql As String
        Dim rowsAffected As Integer
        Try

            sql = "  UPDATE tblMembers "
            sql = sql & " SET MemberName= " & Chr(34) & vstrMemberName & Chr(34)
            sql = sql & ",FTDNAID=" & Chr(34) & vstrFTDNAID & Chr(34)
            sql = sql & ",YFullID=" & Chr(34) & vstrYFullID & Chr(34)
            sql = sql & " WHERE ID=" & vintID

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionDocFiles()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
#Disable Warning BC42353 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42353 ' Function doesn't return a value on all code paths


    Public Function GetBranchByParentBranch(ByVal vstrParentBranch As String) As DataSet

        Dim sql As String

        sql = " SELECT * FROM tblBranches WHERE ParentBranch =" & Chr(34) & vstrParentBranch & Chr(34) '& " ORDER BY SiblingOrder "

        Try


            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            Return dataSet
            dbCommandAccess.Connection.Close()

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function


    Public Function GetBranchesByID(ByVal vintID As String) As DataSet
        Dim sql As String
        sql = " SELECT * FROM tblBranches WHERE ID =" & vintID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            Return dataSet
            dbCommandAccess.Connection.Close()

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
    End Function






    Public Function GetAllTree() As DataSet

        Dim sql As String

        sql = " SELECT ParentBranch, BranchName FROM tblBranches WHERE ParentBranch <> '0' ORDER BY ParentBranch "

        Try


            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            Return dataSet
            dbCommandAccess.Connection.Close()

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function


    Public Function ExistsBranchByParentBranch(ByVal vstrParentBranch As String) As DataSet

        Dim sql As String

        sql = " SELECT * FROM tblBranches WHERE ParentBranch =" & Chr(34) & vstrParentBranch & Chr(34) '& " ORDER BY SiblingOrder "

        Try


            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            dbCommandAccess.Connection.Close()
            Return dataSet


        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function

    Public Function DeleteNode(ByVal vstrBranchName As String)
        Dim i As Integer
        Dim sqlDelete As String = " DELETE FROM tblBranches WHERE BranchName = " & Chr(34) & vstrBranchName & Chr(34)

        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sqlDelete
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            dbCommandAccess.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
    End Function

    Public Function GetDistinctParentBranch() As DataSet
        Try



            Dim sqlGet = " SELECT DISTINCT ParentBranch FROM tblBranches WHERE ParentBranch <> '0' "

            Dim dbCommand As OleDb.OleDbCommand = New OleDbCommand
            dbCommand.CommandText = sqlGet
            dbCommand.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommand.Connection = GetConnection()
            dbCommand.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommand
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            Return dataSet
            dbCommand.Connection.Close()
        Catch ex As Exception

        End Try



    End Function

    Public Function CheckExistingBranch(ByVal vstrBranchName As String) As Boolean
        Dim i As Integer
        Dim sql As String = " SELECT * FROM tblBranches WHERE BranchName = " & Chr(34) & vstrBranchName & Chr(34)
        Try
            Dim dbCommand As OleDb.OleDbCommand = New OleDbCommand
            dbCommand.CommandText = sql
            dbCommand.CommandType = CommandType.Text
            dbCommand.Connection = GetConnection()
            dbCommand.Connection.Open()

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommand
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            If dataSet.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False

            End If
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
    End Function

    Public Function DeleteBranchesByParentBranch(ByVal vstrBranchName As String) As Integer
        Dim i As Integer
        Dim sqlDelete As String = " DELETE FROM tblBranches WHERE ParentBranch = " & Chr(34) & vstrBranchName & Chr(34)

        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sqlDelete
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            dbCommandAccess.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function

    Public Function InsertNode(ByVal vstrBranchName As String,
                          ByVal vstrParentBranch As String) As Integer

        Dim sql As String

        sql = " INSERT INTO tblBranches (BranchName, ParentBranch) VALUES ("
        sql = sql & Chr(34) & vstrBranchName & Chr(34)
        sql = sql & ", " & Chr(34) & vstrParentBranch & Chr(34)
        sql = sql & ")"


        Dim rowsAffected As Integer
        Try

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function


    Public Function UpdateBackgroundColorByID(ByVal vintID As Integer,
                                              ByVal vintColor As Integer) As Integer

        Dim sql As String
        sql = " UPDATE  tblBranches SET BackgroundColor = " & vintColor & " WHERE ID = " & vintID

        Dim rowsAffected As Integer
        Try

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function


    Public Function UpdateSNPInfo(ByVal vintID As Integer,
                                  ByVal vintPos As Integer,
                                  ByVal vstrAlt As String,
                                  ByVal vstrRef As String) As Integer

        Dim sql As String
        sql = " UPDATE  tblBranches SET Pos = " & vintPos
        sql = sql & ", Alt = " & Chr(34) & vstrAlt & Chr(34)
        sql = sql & ", Ref = " & Chr(34) & vstrRef & Chr(34)
        sql = sql & " WHERE ID = " & vintID

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function


    Public Function UpdateSNPName(ByVal vintID As Integer,
                                  ByVal vstrSNPName As String) As Integer

        Dim sql As String
        sql = " UPDATE  tblBranches SET BranchName = " & Chr(34) & vstrSNPName & Chr(34)
        sql = sql & " WHERE ID = " & vintID

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()

            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function


    Public Function UpdateSNPParentBranch(ByVal vstrSNPName As String,
                                          ByVal vstrOldSNPName As String) As Integer

        Dim sql As String
        sql = " UPDATE  tblBranches SET ParentBranch = " & Chr(34) & vstrSNPName & Chr(34)
        sql = sql & " WHERE ParentBranch  = " & Chr(34) & vstrOldSNPName & Chr(34)

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnection()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()

            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function


    Public Function UpdateTextColorByID(ByVal vintID As Integer,
                                             ByVal vintColor As Integer) As Integer

        Dim sql As String
        sql = " UPDATE  tblBranches SET TextColor = " & vintColor & " WHERE ID = " & vintID

        Dim rowsAffected As Integer
        Try
            If vintID > 0 Then
                Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
                dbCommandAccess.CommandText = sql
                dbCommandAccess.CommandType = CommandType.Text

                '  If conn.State = ConnectionState.Closed Then
                dbCommandAccess.Connection = GetConnection()
                dbCommandAccess.Connection.Open()


                rowsAffected = dbCommandAccess.ExecuteNonQuery()
                ' End If


                dbCommandAccess.Connection.Close()

                Return rowsAffected
            End If
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try

    End Function
End Class
