Public Class frmTest
    Dim cDataAccess As New clsDataAccess
    Dim ds As DataSet

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Call cDataAccess.InsertMutation("123456", "G", "A", "asdas", True, True, False, "123")
        'Call cDataAccess.InsertPosition("123", "567", "A")
        'Call cDataAccess.InsertNodeInTree("Bla", "122451", "876,987,01", "82,92", "2626,987,987,0987", True)
        'Call cDataAccess.UpdatePosition(2, "111", "333", "C")
        'Call cDataAccess.UpdateNodeInTree(1, "Blabla", "1", "1,1,1", "1,1", "1,1,1,1", False)
        'Call cDataAccess.UpdateMutation(1, "2", "R", "S", "turlututu", False, False, True, "2")
        'ds = cDataAccess.GetMutationByPosAndAltCall("2", "R")
        'MsgBox(ds.GetXml)
        'ds = cDataAccess.GetMutationByID(1)
        'MsgBox(ds.GetXml)
        'Dim Nd1 As New Node
        'Dim Nd2 As New Node
        'Nd1.LoadWithID(1)
        'Nd1.AppendMutationsID("3452862")
        'Nd1.SavetoDB()
        'Dim pos As New Position
        'pos.LoadWithID(1)
        'MsgBox("position id is " & pos.ID)
        'MsgBox("pos hg19 is " & pos.PosHg19)
        'MsgBox("pos hg38 is " & pos.PosHg38)

        'Dim pos1 As New Position
        'pos1.LoadWithPos19("111")
        'MsgBox("position 1 id is " & pos1.ID)
        'MsgBox("pos 1 hg19 is " & pos1.PosHg19)
        'MsgBox("pos 1 hg38 is " & pos1.PosHg38)

        'Dim pos2 As New Position
        'pos2.LoadWithPos38("333")
        ' pos2.PosHg19 = "777"
        'pos2.SavetoDB()
        'MsgBox("position 2 id is " & pos2.ID)
        'MsgBox("pos 2 hg19 is " & pos2.PosHg19)
        'MsgBox("pos 2 hg38 is " & pos2.PosHg38)

        'Dim mut As New Mutation
        'Dim Nam As String
        'Dim i As Integer
        'mut.Load(1)
        'MsgBox("mutation 1 name is " & mut.Name(0))
        'mut.AppendName("we try a new name here")
        'mut.SavetoDB()
        'MsgBox("mutation 1 id is " & mut.ID)
        'Nam = mut.Name(0)
        'For i = 1 To mut.Name.Count - 1
        'Nam = Nam & "," & mut.Name(i)
        'Next
        'MsgBox("mutation 1 name is " & Nam)
        'MsgBox("mutation 1 AltCall is " & mut.AltCall)
        'MsgBox("mutation 1 RefSNPID is " & mut.RefSNPID)

        'Dim Mb As New Member
        'Mb.LoadWithName("Darin")
        'MsgBox("we loaded " & Mb.Name & " and his ID is " & Mb.ID)
        'MsgBox("his/hers placed in the tree is " & Mb.IsPlacedInTheTree)

        'Mb.FTDNAKit = "DarinFTDNAKitnb"
        'Mb.SavetoDB()

        MsgBox("validcall A is " & cDataAccess.CheckValidCallInDB("A"))
        MsgBox("validcall T is " & cDataAccess.CheckValidCallInDB("T"))
        MsgBox("validcall G is " & cDataAccess.CheckValidCallInDB("G"))
        MsgBox("validcall C is " & cDataAccess.CheckValidCallInDB("C"))
        MsgBox("validcall AA is " & cDataAccess.CheckValidCallInDB("AA"))
        MsgBox("validcall V is " & cDataAccess.CheckValidCallInDB("V"))

    End Sub
End Class