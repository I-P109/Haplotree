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
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try
        '#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
    '#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function GetMemberByID(ByVal mintID As Integer) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers WHERE ID= " & mintID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try

        '#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
    '#Enable Warning BC42105 ' Function doesn't return a value on all code paths


    Public Function GetMembersAll() As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers ORDER BY MemberName "
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try

        '#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
    '#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function GetMemberByFTDNAID(ByVal vstrFTDNAID As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers WHERE FTDNAID =" & Chr(34) & vstrFTDNAID & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try
        '#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
    '#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function GetHg38VariantsByMemberID(ByVal vintMemberID As Integer) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMemberVariantHg38 WHERE FK_MemberID =" & vintMemberID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try
        '#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
    '#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    Public Function GetHg19VariantsByMemberID(ByVal vintMemberID As Integer) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMemberVariantHg19 WHERE FK_MemberID =" & vintMemberID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try
    End Function

    Public Function GetSNPByPositionRefAlt(ByVal vintPosition As Integer,
                                           ByVal vstrRef As String,
                                           ByVal vstrAlt As String) As DataSet
        Dim sql As String

        sql = " SELECT tblMemberVariantHg38.ID,MemberName,Pos,Ref,Alt,Qual,Filter FROM tblMemberVariantHg38, tblMembers WHERE tblMemberVariantHg38.FK_MemberID = tblMembers.ID "
        sql = sql & " AND Pos=" & vintPosition
        sql = sql & " AND Ref=" & Chr(34) & vstrRef & Chr(34)
        sql = sql & " AND Alt=" & Chr(34) & vstrAlt & Chr(34)

        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try
    End Function

    Public Function GetMemberByYFullID(ByVal YFullID As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers WHERE YFullID =" & Chr(34) & YFullID & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try

    End Function

    Public Function InsertMember(ByVal vstrMemberName As String,
                                 ByVal vstrFTDNAID As String,
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
            'dbCommandAccess.Connection = GetConnectionDocFiles() '=old code!?
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function InsertMember(ByVal vstrMemberName As String,
                                 ByVal vstrFTDNAID As String,
                                 ByVal vstrYFullID As String,
                                 ByVal vstrMutationsIDs As String,
                                 ByVal vstrPrivateMutationsIDs As String,
                                 ByVal vstrPutativeMutationsIDs As String,
                                 ByVal vstrCurrentParentNodeID As String,
                                 ByVal vstrIsPlacedInTheTree As Boolean) As Integer


        Dim sql As String
        Dim rowsAffected As Integer
        Try
            'sql = "  INSERT INTO tblMembers (MemberName, FTDNAID, YFullID,MutationsIDs,PrivateMutationsIDs,PutativeMutationsIDs,CurrentParentNodeID,IsPlacedInTheTree) VALUES ("
            sql = "  INSERT INTO tblMembers (MemberName, FTDNAID, YFullID,CurrentParentNodeID,IsPlacedInTheTree) VALUES ("
            sql = sql & Chr(34) & vstrMemberName & Chr(34)
            sql = sql & "," & Chr(34) & vstrFTDNAID & Chr(34)
            sql = sql & "," & Chr(34) & vstrYFullID & Chr(34)
            sql = sql & "," & Chr(34) & vstrMutationsIDs & Chr(34)
            sql = sql & "," & Chr(34) & vstrPrivateMutationsIDs & Chr(34)
            sql = sql & "," & Chr(34) & vstrPutativeMutationsIDs & Chr(34)
            sql = sql & "," & Chr(34) & vstrCurrentParentNodeID & Chr(34)
            sql = sql & "," & Chr(34) & vstrIsPlacedInTheTree & Chr(34)
            sql = sql & ")"

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            'dbCommandAccess.Connection = GetConnectionDocFiles() '=old code!?
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function


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

            sql = "  INSERT INTO tblMemberVariantHg38 (FK_MemberID, Pos, Ref, Alt, Qual, Filter, Info,  Mutation) VALUES ("

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
                dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return 0
        End Try

    End Function



    Public Function InsertPositionByMemberID19(ByVal vstrFK_MemberID As Integer,
                                             ByVal vintPosition As Integer,
                                             ByVal strRef As String,
                                             ByVal vstrAlt As String,
                                             ByVal vstrQual As String,
                                             ByVal vstrFilter As String,
                                             ByVal vstrInfo As String,
                                             ByVal vstrMutation As String) As Integer

        Dim sql As String
        Dim rowsAffected As Integer
        Try

            sql = "  INSERT INTO tblMemberVariantHg19(FK_MemberID, Pos, Ref, Alt, Qual, Filter, Info, Format, Mutation) VALUES ("

            sql = sql & vstrFK_MemberID
            sql = sql & "," & vintPosition
            sql = sql & "," & Chr(34) & strRef & Chr(34)
            sql = sql & "," & Chr(34) & vstrAlt & Chr(34)
            sql = sql & "," & Chr(34) & vstrQual & Chr(34)
            sql = sql & "," & Chr(34) & vstrFilter & Chr(34)
            sql = sql & "," & Chr(34) & vstrInfo & Chr(34)
            'sql = sql & "," & Chr(34) & vstrFormat & Chr(34)
            sql = sql & "," & Chr(34) & vstrMutation & Chr(34)
            sql = sql & ")"

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            If conn.State = ConnectionState.Closed Then
                dbCommandAccess.Connection = GetConnectionVariantDB()
                dbCommandAccess.Connection.Open()
                rowsAffected = dbCommandAccess.ExecuteNonQuery()
            Else
                dbCommandAccess.Connection = conn
                rowsAffected = dbCommandAccess.ExecuteNonQuery()
            End If
            'dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try

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
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try

    End Function

    Public Function UpdateMember(ByVal vintID As Integer,
                                 ByVal vstrMemberName As String,
                                 ByVal vstrFTDNAID As String,
                                 ByVal vstrYFullID As String,
                                 ByVal vstrMutationsIDs As String, 'need to find a way to store arrays first
                                 ByVal vstrPrivateMutationsIDs As String,
                                 ByVal vstrPutativeMutationsIDs As String,
                                 ByVal vstrCurrentParentNodeID As String,
                                 ByVal vstrIsPlacedInTheTree As Boolean) As Integer
        Dim sql As String
        Dim rowsAffected As Integer
        Try

            sql = "  UPDATE tblMembers "
            sql = sql & " SET MemberName= " & Chr(34) & vstrMemberName & Chr(34)
            sql = sql & ",FTDNAID=" & Chr(34) & vstrFTDNAID & Chr(34)
            sql = sql & ",YFullID=" & Chr(34) & vstrYFullID & Chr(34)
            sql = sql & ",MutationsIDs=" & Chr(34) & vstrMutationsIDs & Chr(34)
            sql = sql & ",PrivateMutationsIDs=" & Chr(34) & vstrPrivateMutationsIDs & Chr(34)
            sql = sql & ",PutativeMutationsIDs=" & Chr(34) & vstrPutativeMutationsIDs & Chr(34)
            sql = sql & ",CurrentParentNodeID=" & Chr(34) & vstrCurrentParentNodeID & Chr(34)
            sql = sql & ",IsPlacedInTheTree=" & vstrIsPlacedInTheTree
            sql = sql & " WHERE ID=" & vintID

            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try

    End Function


    Public Function GetBranchByParentBranch(ByVal vstrParentBranch As String) As DataSet

        Dim sql As String

        sql = " SELECT * FROM tblBranches WHERE ParentBranch =" & Chr(34) & vstrParentBranch & Chr(34) '& " ORDER BY SiblingOrder "

        Try


            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommandAccess
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            Return dataSet
            dbCommandAccess.Connection.Close()
            Return Nothing

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
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
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
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
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
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try

    End Function

    Public Function DeleteNode(ByVal vstrBranchName As String) As Integer
        Dim sqlDelete As String = " DELETE FROM tblBranches WHERE BranchName = " & Chr(34) & vstrBranchName & Chr(34)
        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sqlDelete
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function GetDistinctParentBranch() As DataSet
        Try

            Dim sqlGet = " SELECT DISTINCT ParentBranch FROM tblBranches WHERE ParentBranch <> '0' "

            Dim dbCommand As OleDb.OleDbCommand = New OleDbCommand
            dbCommand.CommandText = sqlGet
            dbCommand.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommand.Connection = GetConnectionVariantDB()
            dbCommand.Connection.Open()
            ' End If

            Dim dataAdapter As OleDb.OleDbDataAdapter = New OleDbDataAdapter
            dataAdapter.SelectCommand = dbCommand
            Dim dataSet As System.Data.DataSet = New System.Data.DataSet()
            dataAdapter.Fill(dataSet)

            Return dataSet
            dbCommand.Connection.Close()
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return Nothing
        End Try

    End Function

    Public Function CheckExistingBranch(ByVal vstrBranchName As String) As Boolean

        Dim sql As String = " SELECT * FROM tblBranches WHERE BranchName = " & Chr(34) & vstrBranchName & Chr(34)
        Try
            Dim dbCommand As OleDb.OleDbCommand = New OleDbCommand
            dbCommand.CommandText = sql
            dbCommand.CommandType = CommandType.Text
            dbCommand.Connection = GetConnectionVariantDB()
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
            Return False
        End Try
    End Function

    Public Function DeleteBranchesByParentBranch(ByVal vstrBranchName As String) As Integer

        Dim sqlDelete As String = " DELETE FROM tblBranches WHERE ParentBranch = " & Chr(34) & vstrBranchName & Chr(34)

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sqlDelete
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
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
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
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
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            ' End If

            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            ' End If


            dbCommandAccess.Connection.Close()

            Return rowsAffected

        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
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
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            dbCommandAccess.Connection.Close()

            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
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
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()

            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
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
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()

            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
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
                dbCommandAccess.Connection = GetConnectionVariantDB()
                dbCommandAccess.Connection.Open()


                rowsAffected = dbCommandAccess.ExecuteNonQuery()
                ' End If


                dbCommandAccess.Connection.Close()

                Return rowsAffected
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try

    End Function

    Public Function GetPositionByID(PosID As Integer) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblPosition WHERE ID= " & PosID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
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
            Return Nothing
        End Try

    End Function

    Public Function GetPositionByPosHg19(PosHg19 As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblPosition WHERE PosHg19= " & Chr(34) & PosHg19 & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
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
            Return Nothing
        End Try
    End Function

    Public Function GetPositionByPosHg38(PosHg38 As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblPosition WHERE PosHg38= " & Chr(34) & PosHg38 & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
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
            Return Nothing
        End Try
    End Function

    Public Function GetNodeByName(NodeName As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblNode WHERE NodeName= " & Chr(34) & NodeName & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
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
            Return Nothing
        End Try
    End Function

    Public Function GetNodeByID(NodeID As Integer) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblNode WHERE ID= " & NodeID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
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
            Return Nothing
        End Try
    End Function

    Public Function CheckValidCallInDB(CallToValidate As String) As Integer 'returns 1 if valid, 0 if not
        Dim sql As String

        sql = "SELECT COUNT(ID) FROM tblValidCall WHERE ValidBase=" & Chr(34) & CallToValidate & Chr(34)

        Dim NbValidCallInDB As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            NbValidCallInDB = dbCommandAccess.ExecuteScalar()
            dbCommandAccess.Connection.Close()
            Return NbValidCallInDB
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function GetNBNodesInDB() As Integer
        Dim sql As String

        sql = "SELECT COUNT(ID) FROM tblNode"

        Dim NbNodesInDB As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            NbNodesInDB = dbCommandAccess.ExecuteScalar()
            dbCommandAccess.Connection.Close()
            Return NbNodesInDB
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function GetNBRowsInTableHPDB(TblName As String) As Integer ' in HaploTreeDB
        Dim sql As String

        sql = "SELECT COUNT(ID) FROM " & TblName

        Dim NbRowsInDB As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            NbRowsInDB = dbCommandAccess.ExecuteScalar()
            dbCommandAccess.Connection.Close()
            Return NbRowsInDB
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function GetNBRowsInTableVTDB(TblName As String) As Integer ' in VariantDB
        Dim sql As String

        sql = "SELECT COUNT(ID) FROM " & TblName

        Dim NbRowsInDB As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            NbRowsInDB = dbCommandAccess.ExecuteScalar()
            dbCommandAccess.Connection.Close()
            Return NbRowsInDB
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function GetNbHG38Variants(MembID As String) As Integer
        Dim sql As String

        sql = "  SELECT COUNT(ID) FROM  tblMemberVariantHg38 WHERE FK_MemberID =" & MembID

        Dim NbRowsInDB As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            NbRowsInDB = dbCommandAccess.ExecuteScalar()
            dbCommandAccess.Connection.Close()
            Return NbRowsInDB
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function GetNbHG19Variants(MembID As String) As Integer
        Dim sql As String

        sql = "  SELECT COUNT(ID) FROM  tblMemberVariantHg19 WHERE FK_MemberID =" & MembID

        Dim NbRowsInDB As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionVariantDB()
            dbCommandAccess.Connection.Open()
            NbRowsInDB = dbCommandAccess.ExecuteScalar()
            dbCommandAccess.Connection.Close()
            Return NbRowsInDB
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function GetMemberByName(MemberName As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMembers WHERE MemberName= " & Chr(34) & MemberName & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionVariantDB()
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
            Return Nothing
        End Try
    End Function

    Public Function GetMutationByID(MutationID As Integer) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMutation WHERE ID= " & MutationID
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
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
            Return Nothing
        End Try
    End Function

    Public Function GetMutationByPosAndAltCall(PosID As String, AlternCall As String) As DataSet
        Dim sql As String

        sql = "  SELECT * FROM  tblMutation WHERE PositionID= " & Chr(34) & PosID & Chr(34)
        sql = sql & " AND AltCall=" & Chr(34) & AlternCall & Chr(34)
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text

            '  If conn.State = ConnectionState.Closed Then
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
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
            Return Nothing
        End Try
    End Function

    Public Function InsertPosition(PosHg19 As Integer,
                                   PosHg38 As Integer,
                                   AncestrallCall As String) As Integer
        Dim sql As String


        sql = " INSERT INTO tblPosition (PosHg19, PosHg38, AncestrallCall) VALUES ("
        sql = sql & PosHg19
        sql = sql & "," & PosHg38
        sql = sql & "," & Chr(34) & AncestrallCall & Chr(34)
        sql = sql & ")"

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function UpdatePosition(PosID As Integer,
                                   PosHg19 As Integer,
                                   PosHg38 As Integer,
                                   AncestrallCall As String) As Integer
        Dim sql As String

        sql = "  UPDATE tblPosition "
        sql = sql & " SET PosHg19= " & PosHg19
        sql = sql & ",PosHg38=" & PosHg38
        sql = sql & ",AncestrallCall=" & Chr(34) & AncestrallCall & Chr(34)
        sql = sql & " WHERE ID=" & PosID

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()

            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function InsertNodeInTree(NodeName As String,
                                     ParentNodeID As String,
                                     ChildrenNodesIDs As String,
                                     ChildrenMembersIDs As String,
                                     MutationsIDs As String,
                                     HasParent As Boolean) As Integer
        Dim sql As String


        sql = " INSERT INTO tblNode (NodeName, ParentNodeID, ChildrenNodesIDs, ChildrenMembersIDs, MutationsIDs, HasParent) VALUES ("
        sql = sql & Chr(34) & NodeName & Chr(34)
        sql = sql & "," & Chr(34) & ParentNodeID & Chr(34)
        sql = sql & "," & Chr(34) & ChildrenNodesIDs & Chr(34)
        sql = sql & "," & Chr(34) & ChildrenMembersIDs & Chr(34)
        sql = sql & "," & Chr(34) & MutationsIDs & Chr(34)
        sql = sql & "," & HasParent
        sql = sql & ")"

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function UpdateNodeInTree(NodeID As Integer,
                                     NodeName As String,
                                     ParentNodeID As String,
                                     ChildrenNodesIDs As String,
                                     ChildrenMembersIDs As String,
                                     MutationsIDs As String,
                                     HasParent As Boolean) As Integer
        Dim sql As String

        sql = "  UPDATE tblNode "
        sql = sql & " SET NodeName= " & Chr(34) & NodeName & Chr(34)
        sql = sql & ",ParentNodeID=" & Chr(34) & ParentNodeID & Chr(34)
        sql = sql & ",ChildrenNodesIDs=" & Chr(34) & ChildrenNodesIDs & Chr(34)
        sql = sql & ",ChildrenMembersIDs=" & Chr(34) & ChildrenMembersIDs & Chr(34)
        sql = sql & ",MutationsIDs=" & Chr(34) & MutationsIDs & Chr(34)
        sql = sql & ",HasParent=" & HasParent
        sql = sql & " WHERE ID=" & NodeID

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()

            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function UpdateMutation(MutationID As Integer,
                                   ByVal PositionID As String,
                                   ByVal AltCall As String,
                                   ByVal RefSNPID As String,
                                   ByVal MutationNames As String,
                                   ByVal IsISOGGOfficial As Boolean,
                                   ByVal IsPrivate As Boolean,
                                   ByVal IsIgnored As Boolean,
                                   ByVal CurrentParentNodeID As String) As Integer
        Dim sql As String

        sql = "  UPDATE tblMutation "
        sql = sql & " SET PositionID= " & Chr(34) & PositionID & Chr(34)
        sql = sql & ",AltCall=" & Chr(34) & AltCall & Chr(34)
        sql = sql & ",RefSNPID=" & Chr(34) & RefSNPID & Chr(34)
        sql = sql & ",MutationNames=" & Chr(34) & MutationNames & Chr(34)
        sql = sql & ",IsISOGGOfficial=" & IsISOGGOfficial
        sql = sql & ",IsPrivate=" & IsPrivate
        sql = sql & ",IsIgnored=" & IsIgnored
        sql = sql & ",CurrentParentNodeID=" & Chr(34) & CurrentParentNodeID & Chr(34)
        sql = sql & " WHERE ID=" & MutationID

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()

            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function

    Public Function InsertMutation(ByVal PositionID As String,
                                   ByVal AltCall As String,
                                   ByVal RefSNPID As String,
                                   ByVal MutationNames As String,
                                   ByVal IsISOGGOfficial As Boolean,
                                   ByVal IsPrivate As Boolean,
                                   ByVal IsIgnored As Boolean,
                                   ByVal CurrentParentNodeID As String) As Integer
        Dim sql As String

        sql = " INSERT INTO tblMutation (PositionID, AltCall, MutationNames, IsISOGGOfficial, IsPrivate, RefSNPID, IsIgnored, CurrentParentNodeID) VALUES ("
        sql = sql & Chr(34) & PositionID & Chr(34)
        sql = sql & "," & Chr(34) & AltCall & Chr(34)
        sql = sql & "," & Chr(34) & MutationNames & Chr(34)
        sql = sql & "," & IsISOGGOfficial
        sql = sql & "," & IsPrivate
        sql = sql & "," & Chr(34) & RefSNPID & Chr(34)
        sql = sql & "," & IsIgnored
        sql = sql & "," & Chr(34) & CurrentParentNodeID & Chr(34)
        sql = sql & ")"

        Dim rowsAffected As Integer
        Try
            Dim dbCommandAccess As OleDb.OleDbCommand = New OleDbCommand
            dbCommandAccess.CommandText = sql
            dbCommandAccess.CommandType = CommandType.Text
            dbCommandAccess.Connection = GetConnectionHaploTreeDB()
            dbCommandAccess.Connection.Open()
            rowsAffected = dbCommandAccess.ExecuteNonQuery()
            dbCommandAccess.Connection.Close()
            Return rowsAffected
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
            Return 0
        End Try
    End Function
End Class
