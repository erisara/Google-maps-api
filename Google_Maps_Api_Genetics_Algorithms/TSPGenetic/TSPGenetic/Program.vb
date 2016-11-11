Module Program
    Public Sub Main(ByVal sArgs() As String)
        Dim x As New Form1
        If sArgs.Length = 0 Then                'If there are no arguments
            x.ShowDialog()
        Else                                    'We have some arguments 
            If sArgs.Length <> 2 Then
                Console.WriteLine("Usage: TSPGenetic csv output") 'Print out help
            Else
                x.RunGA(sArgs(0), sArgs(1), False)
            End If
        End If
    End Sub
End Module