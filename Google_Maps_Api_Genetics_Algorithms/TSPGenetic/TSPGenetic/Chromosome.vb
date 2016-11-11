Public Class Chromosome
    Public Shared _Map As List(Of City)     ' όλα τα χρωματοσώματα μοιράζονται τον ίδιο χάρτη
    Public Path As List(Of Double)         ' η διαδρομή με τις πόλεις, σαν δείκτες στη λίστα Map.  
    Public _ChromosomeLength As Double   ' το μήκος της διαδρομής
    Private _Fitness As Double              ' η αξία της διαδρομής

    '******************************************************************************
    ' PathToString - βοηθητική ρουτίνα για την εμφάνιση του μονοπατιού σαν string
    '                δέχεται σαν είσοδο το διαχωριστικό κείμενο, αν θα συμπεριληφθεί
    '                η αξία του μονοπατιού και το διαχωριστικό ανάμεσα στο μονοπάτι
    '                και την αξία του.  
    '******************************************************************************
    Public Function PathToString(Optional ByVal strSeparator As String = ",", _
                                 Optional ByVal IncludeFitness As Boolean = False, _
                                 Optional ByVal strFitSeparator As String = ",") As String
        Dim i As Double

        Dim message As String = Path(0)

        For i = 1 To Path.Count - 1
            message = message & strSeparator & Path(i)
        Next

        If IncludeFitness Then
            message = message & strFitSeparator & _Fitness.ToString
        End If

        Return message

    End Function

    '******************************************************************************
    ' Shuffle  -  ρουτίνα για το ανακάτεμα μιας διαδρομής. Κάνουμε επανάληψη όσα
    '             είναι και τα στοιχεία της διαδρομής και αφού επιλέξουμε τυχαία δύο 
    '             διαφορετικές πόλεις τις ανταλλάσουμε μεταξύ τους.
    '******************************************************************************
    Private Sub Shuffle()
        Dim i, temp, index1, index2 As Double

        Randomize()

        For i = 1 To _ChromosomeLength
            Do
                index1 = CDbl(Int((_ChromosomeLength * Rnd()) + 1)) - 1
                index2 = CDbl(Int((_ChromosomeLength * Rnd()) + 1)) - 1
            Loop While index1 = index2

            temp = Path(index1)
            Path(index1) = Path(index2)
            Path(index2) = temp
        Next
    End Sub

    '******************************************************************************
    ' New  - Δημιουργία νέου χρωματοσώματος.Όλη διαδρομή αποτελείται από τους δείκτες  
    '        των πόλεων στο χάρτη. Οπότε γεμίζουμε στη σειρά ένα arraylist με τιμές      
    '        από 0 έως τις πόλεις του χάρτη -1 και μετά το ανακατεύουμε ώστε να κάνουμε
    '        διαφορετικές διαδρομές και εκτιμούμε  την αξία του
    '
    '        Προσοχή - θα πρέπει να έχει αρχικοποιηθεί ο χάρτης για να δουλέψει σωστά
    '        η ρουτίνα.
    '******************************************************************************

    Public Sub New()
        Dim i As Integer

        _ChromosomeLength = _Map.Count
        Path = New List(Of Double)

        For i = 0 To _ChromosomeLength - 1
            Path.Add(i)
        Next

        Shuffle()
        Evaluate()
    End Sub

    '******************************************************************************
    ' New  - Δημιουργία νέου χρωματοσώματος. Χρησιμοποιείται κατά την δημιουργία 
    '        νέου πληθυσμού όπου τα χρωματοσώματα έχουν ήδη διαδρομές και αξίες και 
    '        επιλέγονται για να μετέχουν στο νέο πληθυσμό, οπότε οι παράμετροι
    '        δημιουργίας είναι η διαδρομή των πόλεων και η αξία της διαδρομής, την 
    '        οποία φυσικά μπορώ να ξαναυπολογίσω αλλά το κάνω για λόγους ταχύτητας.
    '
    '        Επίσης ο συγκεκριμένος κατασκευαστής με διευκολύνει να δοκιμάζω 
    '        συγκεκριμένες διαδρομές.
    '******************************************************************************

    Public Sub New(ByRef PathOfCities As List(Of Double), Optional ByVal PathFitness As Double = 0)
        Dim i As Double

        _ChromosomeLength = PathOfCities.Count

        Path = New List(Of Double)

        For i = 0 To _ChromosomeLength - 1
            Path.Add(PathOfCities.Item(i))
        Next

        If (PathFitness <= 0) Then
            Evaluate()
        Else
            _Fitness = PathFitness
        End If

    End Sub

    '******************************************************************************
    ' getDistance  - Έυρεση της ευκλειδιας απόστασης μεταξύ δύο πόλεων (σημείων 
    '                στο χάρτη). Η συνάρτηση δέχεται δύο δείκτες στο χάρτη και
    '                υπολογίζει την απόσταση μεταξύ τους.    
    '******************************************************************************

    Private Function getDistance(ByVal FirstCity As Double,
                               ByVal SecondCity As Double) As Double
        Dim CityA, CityB As City
        Dim dx, dy As Double
        CityA = _Map(FirstCity)
        CityB = _Map(SecondCity)
        dx = CityA.X - CityB.X
        dy = CityA.Y - CityB.Y

        Return Math.Sqrt((dx * dx) + (dy * dy))

    End Function

    '******************************************************************************
    ' Evaluate  - Έυρεση της συνολικής απόστασης ενός μονοπατιού στο χάρτη.
    '******************************************************************************

    Public Sub Evaluate()
        Dim i As Double
        _Fitness = 0

        For i = 0 To Path.Count - 2
            _Fitness += getDistance(Path(i), Path(i + 1))
        Next

        _Fitness += getDistance(Path(_ChromosomeLength - 1), Path(0))

    End Sub

    '******************************************************************************
    ' Fitness  - Η ιδιότητα Fitness επιστρέφει τη συνολική απόσταση μιας διαδρομής.
    '******************************************************************************
    Public ReadOnly Property Fitness() As Double
        Get
            Return _Fitness
        End Get
    End Property

    '******************************************************************************
    ' IndexOf  - Εύρεση της θέσης μια πόλης στη διαδρομή.
    '******************************************************************************
    Public Function IndexOf(ByVal value As Double) As Double
        Dim i As Double
        Dim pos As Double = -1

        For i = 0 To Path.Count - 1
            If value = Path(i) Then
                pos = i
                Exit For
            End If
        Next

        Return pos
    End Function

    '******************************************************************************
    ' CompareFittness  - Η συνάρτηση χρησιμοποιείται για την ταξινόμηση των στοιχείων 
    '                    του πληθυσμού κατά φθίνουσα σειρά.
    '******************************************************************************
    Public Shared Function Max2Min(ByVal x As Chromosome, ByVal y As Chromosome) As Double
        If x.Fitness < y.Fitness Then
            Return 1
        ElseIf x.Fitness > y.Fitness Then
            Return -1
        Else
            Return 0
        End If
    End Function

    '******************************************************************************
    ' CompareFittness  - Η συνάρτηση χρησιμοποιείται για την ταξινόμηση των στοιχείων 
    '                    του πληθυσμού κατά αύξουσα σειρά.
    '******************************************************************************
    Public Shared Function Min2Max(ByVal x As Chromosome, ByVal y As Chromosome) As Double
        If x.Fitness < y.Fitness Then
            Return -1
        ElseIf x.Fitness > y.Fitness Then
            Return 1
        Else
            Return 0
        End If
    End Function

    '******************************************************************************
    ' Replace  - Αντικατάσταση του χρωματοσώματος με νέο μονοπάτι και εκτίμηση του.
    '******************************************************************************
    Public Sub Replace(ByRef newPath As List(Of Double), Optional ByVal PathFitness As Double = 0)
        Dim i As Double
        For i = 0 To newPath.Count - 1
            Path(i) = newPath(i)
        Next

        If (PathFitness <= 0) Then
            Evaluate()
        Else
            _Fitness = PathFitness
        End If
    End Sub

End Class