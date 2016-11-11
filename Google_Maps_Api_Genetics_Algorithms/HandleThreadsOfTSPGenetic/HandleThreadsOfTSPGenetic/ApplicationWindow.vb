'************************************************************************************
'*  A thread server for running TSPGenetic processes
'
'   Νικόλαος Ζ. Ζάχαρης (c)
'   Αναπληρωτής Καθηγητής
'   Email : nzach@teipir.gr
'
'   Τμήμα Μηχανικών Ηλεκτρονικών Υπολογιστικών Συστημάτων
'   Ανώτατο Εκπαιδευτικό Ίδρυμα Τεχνολογικού Τομέα Πειραιά
'   Πέτρου Ράλλη και Θηβών 250
'   Β Κτίριο - Γραφείο ΔΑ5
'   Αιγάλεω 12244
'   Αθήνα
'

Imports System.Threading

Public Class ApplicationWindow

    ' Show the path of TSPGenetic executable
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        If OpenFileDialog1.ShowDialog <> System.Windows.Forms.DialogResult.Cancel Then
            txtExePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    ' Run an instance os TSPGenetic weith parameters
    Public Sub ExecuteTSP(ByVal csvPath As String, ByVal outPath As String)
        Try

            ' New ProcessStartInfo created
            Dim p As New ProcessStartInfo

            ' Specify the location of the binary
            p.FileName = txtExePath.Text

            ' Use these arguments for the process
            p.Arguments = csvPath & " " & outPath

            ' Use a hidden window
            p.WindowStyle = ProcessWindowStyle.Hidden

            ' Start the process
            Process.Start(p)
        Catch ex As Exception

        End Try
    End Sub
    Dim Clients As New List(Of UserSchedule)

    ' Fill the info of database (table schedules) 
    Public Sub ReadDatabase()

        Clients.Clear()
        ' You must fill Clients list with all the users waiting for processing 
        ' but no more than Integer.Parse(txtNoThreads.Text) in order to 
        ' avoid overloading
        Clients.Add(New UserSchedule(1, 2, "c:\temp\Thessalia.csv", "c:\temp\_out1.txt"))
        Clients.Add(New UserSchedule(2, 3, "c:\temp\_patra.csv", "c:\temp\_out2.txt"))
        ' each item of list contais userid, scheduleid, csv path, outpath
        ' you can fill the object with more info
    End Sub


    Public Sub ExecuteThreads()

        Dim tasksList As List(Of Task) = New List(Of Task)

        For i As Integer = 0 To Clients.Count - 1
            Dim x As UserSchedule = Clients.Item(i)
            tasksList.Add(Task.Factory.StartNew(Sub() ExecuteTSP(x.csvPath, x.outPath)))
        Next

        ' wait for all threads to finish.
        ' The loop will only exit once all threads have completed their work.
        Task.WaitAll(tasksList.ToArray())

        ' Inform back the DB that TSP fIninshed execution
        MsgBox("finished")
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        If System.IO.File.Exists(txtExePath.Text) And IsNumeric(txtNoThreads.Text) Then

            txtExePath.Enabled = False
            txtNoThreads.Enabled = False

            While True
                ReadDatabase()
                ExecuteThreads()
                Application.DoEvents()
                Threading.Thread.Sleep(1000)
            End While
        Else
            MsgBox("The path must not be empty and the number of threads must be a number.")
        End If
    End Sub
End Class
