Public Class UserSchedule
    Private _userId As String
    Private _csvPath As String
    Private _outPath As String
    Private _scheduleId As String

    Property userId() As String
        Get
            Return _userId
        End Get
        Set(ByVal Value As String)
            _userId = Value
        End Set
    End Property

    Property scheduleId() As String
        Get
            Return _scheduleId
        End Get
        Set(ByVal Value As String)
            _scheduleId = Value
        End Set
    End Property

    Property csvPath() As String
        Get
            Return _csvPath
        End Get
        Set(ByVal Value As String)
            _csvPath = Value
        End Set
    End Property

    Property outPath() As String
        Get
            Return _outPath
        End Get
        Set(ByVal Value As String)
            _outPath = Value
        End Set
    End Property
    Sub New(ByVal uid As String, ByVal schid As String, ByVal csvfp As String, ByVal outfp As String)
        _userId = uid
        _csvPath = csvfp
        _outPath = outfp
        _scheduleId = schid
    End Sub
End Class
