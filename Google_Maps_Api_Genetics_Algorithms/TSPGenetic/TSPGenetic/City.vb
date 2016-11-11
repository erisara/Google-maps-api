Public Class City
    Private _X As Double = 0
    Private _Y As Double = 0
    Private _CityName As String = ""

    Public Sub New(ByVal cityName As String, ByVal x As Double, ByVal y As Double)
        _CityName = cityName
        _X = x
        _Y = y
    End Sub

    Public Sub New(ByVal cityName As String)
        _CityName = cityName
    End Sub

    Public Property X() As Double
        Get
            Return _X
        End Get
        Set(ByVal Xpos As Double)
            _X = Xpos
        End Set
    End Property

    Public Property Y() As Double
        Get
            Return _Y
        End Get
        Set(ByVal Ypos As Double)
            _Y = Ypos
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _CityName
        End Get
        Set(ByVal cityName As String)
            _CityName = cityName
        End Set
    End Property
End Class
