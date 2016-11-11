'************************************************************************************
'*  Travelling Salesman Problem - Επίλυση με γενετικούς αλγορίθμους
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
'   H εικόνα με την Ευρώπη καθώς και τα δικαιωματα της ανήκουν στο δικτυακό τόπο 
'   http://www.nationsonline.org/oneworld/europe_map.htm
'************************************************************************************
Imports System.IO

Public Class MyApp

    Private Map As New List(Of City)
    Private Population As New List(Of Chromosome)
    Private PopSize As Integer = 100
    Private MaxGenerations As Integer = 500
    Private ProbCrossOver As Single = 0.8
    Private ProbInversion As Single = 0.5
    Private ProbMutation As Single = 0.01
    Private ShortestPath As Chromosome

    Dim BmpSource As Bitmap  ' H εικόνα του χάρτη
    '************************************************************************************
    '  Form1_Load - Έναρξη της εφαρμογής όπου φορτώνουμε την εικόνα από αρχείο και τη 
    '               τοποθετούμε στο picture box. H εικόνα BmpSource θα χρησιμοποιηθεί για 
    '               για υπόβαθρο κατά το σχεδιασμό.
    '************************************************************************************

    Private Sub MyApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BmpSource = New Bitmap("europe.png")
        PictureBox1.SizeMode = PictureBoxSizeMode.AutoSize
        PictureBox1.Image = BmpSource.Clone()  ' Εμφάνιση στο PictureBox1

    End Sub

    '************************************************************************************
    '  PictureBox1_MouseDown - βοηθητική συνάρτηση για να χειρίζεται το mousedown γεγονός
    '                          πάνω στην εικόνα. Ο σκοπός είναι να καταγράφω τις θέσεις 
    '                          και τις συνταταγμένες των πόλεων που εμφανίζονται στο 
    '                          χάρτη ώστε να κατασκευάσω το αρχείο cities.csv 
    '************************************************************************************
    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        Dim city As String = ""
        If chkCoords.Checked Then
            city = InputBox("Type city name", "Capital City", "")
            If city <> "" Then
                Text1.Text = Text1.Text + city + "," + e.X.ToString() + "," + e.Y.ToString() + vbNewLine
            End If
        End If
    End Sub

    '************************************************************************************
    '  InitializeMap - η συνάρτηση διαβάζει το αρχείο cities.csv το οποίο περιέχει
    '                  όλες τις πόλεις της διαδρομής. Η κάθε γραμμή περιέχει το όνομα μιας
    '                  πόλης, καθώς και τις συνεταγμένες Χ, Υ. Τα δεδομενα διαχωρίζονται με
    '                  το χαρακτήρα ;
    '************************************************************************************

    Private Function InitializeMap() As Boolean
        Dim xCity As City
        Dim noLine As Integer = 1

        Try
            If Not File.Exists("cities.csv") Then
                MessageBox.Show("File -cities.csv- does not exists.")
                Return False
            End If

            Using sr As New StreamReader("cities.csv")

                Dim line As String


                Do While (sr.Peek() >= 0)
                    line = sr.ReadLine().Trim

                    If line.Length > 0 Then
                        Dim words As String() = line.Split(New Char() {";"c})
                        xCity = New City(words(0), Double.Parse(words(1)), Double.Parse(words(2)))
                        Map.Add(xCity)
                        noLine = noLine + 1
                    Else
                        Exit Do
                    End If

                Loop

                sr.Close()

            End Using
        Catch e As Exception
            MessageBox.Show("Error reading the file in line : " & noLine.ToString())
            Return False
        End Try

        Return True
    End Function

    '************************************************************************************
    ' btnStart_Click - βοηθητική συνάρτηση για να χειρίζεται το κλικ γεγονός στο btnStart, 
    '                  μέσω του οποίου αρχίζει η εκτέλεση του γενετικού αλγορίθμου
    '************************************************************************************
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        Timer1.Start()
        RunGA()

    End Sub

    '************************************************************************************
    '  RunGA - O Γενετικός Αλγόριθμος
    '
    '
    '************************************************************************************
    Public Sub RunGA()
        Dim j As Integer

        Map.Clear()
        Population.Clear()

        Randomize()
        InitializeMap()
        Chromosome._Map = Map

        CreatePopulation()
        FindBestChromosome()

        Application.DoEvents()

        For j = 1 To MaxGenerations
            CreateNewPopulation()
            Application.DoEvents()

            CrossOver()
            Application.DoEvents()

            Inversion()
            Application.DoEvents()

            Mutation()
            Application.DoEvents()

            DrawChromosome(ShortestPath)
            Text1.Text = j.ToString() & ")  " & ShortestPath.PathToString(",", True, "  :  ") & vbNewLine
        Next
    End Sub

    '************************************************************************************
    '  CreatePopulation - η συνάρτηση δημιουργεί τον αρχικό πληθυσμό από χρωματοσώματα, 
    '                     όπου το καθένα περιέχει μια διαδρομη από πόλεις.
    '************************************************************************************
    Private Sub CreatePopulation()
        Dim i As Integer

        For i = 1 To PopSize
            Population.Add(New Chromosome())
        Next

        FindBestChromosome()
    End Sub

    '************************************************************************************
    '  CheckForBest - η συνάρτηση συγκρίνει τη μέχρι τώρα καλύτερη διαδρομή με μια νέα 
    '                 διαδρομή και αποθηκεύει τη καλύτερη αναμεσα στις δύο.
    '************************************************************************************
    Private Sub CheckForBest(ByRef Competitor As Chromosome)
        If Competitor.Fitness < ShortestPath.Fitness Then
            ShortestPath.Replace(Competitor.Path, Competitor.Fitness)
        End If
    End Sub

    '************************************************************************************
    '  FindBestChromosome - εύρεση της καλύτερη διαδρομής στο πληθυσμό.
    '************************************************************************************

    Private Sub FindBestChromosome()
        Dim minValue As Double
        Dim i, minIndex As Integer

        minIndex = 0
        minValue = Population(minIndex).Fitness

        For i = 1 To Population.Count - 1
            If Population(i).Fitness < minValue Then
                minValue = Population(i).Fitness
                minIndex = i
            End If
        Next

        ShortestPath = New Chromosome(Population(minIndex).Path, minValue)
    End Sub

    '************************************************************************************
    '  SortPopulation - η συνάρτηση ταξινομεί το πληθυσμό από τη μικρότερη προς τη 
    '                   μεγαλύτερη τιμή
    '************************************************************************************
    Private Sub SortPopulation(Optional ByVal decrOrder As Boolean = True)
        If decrOrder Then
            Population.Sort(AddressOf Chromosome.Max2Min)
        Else
            Population.Sort(AddressOf Chromosome.Min2Max)
        End If
    End Sub

    '************************************************************************************
    '*  shiftRight - σε μια λίστα ακεραίων μετακυλεί όλα τα στοιχεία μια θέση προς 
    '*               τα δεξια. π.χ. από 0 1 2 3 θα γίνει 3 0 1 2. Χρησιμοποιείται στo
    '*               ordered crossover  
    '************************************************************************************
    Public Sub shiftRight(ByRef path As List(Of Double))
        If path.Count = 1 Then
            Exit Sub
        End If

        Dim i, last As Integer

        last = path.Item(path.Count - 1)
        For i = (path.Count - 1) To 1 Step -1
            path(i) = path(i - 1)
        Next
        path(0) = last
    End Sub

    '************************************************************************************
    '*
    '*  shiftRight - σε μια λίστα ακεραίων μετακυλεί όλα τα στοιχεία κατά pos θέσεις προς 
    '*               τα δεξια. π.χ. από 0 1 2 3 για 2 θέσεις θα γίνει 2 3 0 1  
    '*
    '************************************************************************************
    Public Sub shiftRight(ByRef path As List(Of Double), ByVal pos As Integer)
        Dim i As Integer

        For i = 1 To pos
            shiftRight(path)
        Next
    End Sub

    '************************************************************************************
    '*
    '*  OrderCrossOver - διατεταγμένη διασταύρωση ανάμεσα σε δύο χρωματοσώματα. 
    '*
    '************************************************************************************
    Public Sub OrderCrossOver(ByVal parent1 As Double, ByVal parent2 As Double)
        Dim pos1, pos2, tmp As Integer
        Dim child1 As New List(Of Double)
        Dim child2 As New List(Of Double)
        Dim i As Integer

        ' Επέλεξε τυχαία δύο διαφορετικές θέσεις στο μονοπάτι της διαδρομής
        Do
            pos1 = CInt(Int((Map.Count * Rnd()) + 1)) - 1
            pos2 = CInt(Int((Map.Count * Rnd()) + 1)) - 1
        Loop While pos1 = pos2

        ' Έλεγξε αν ειναι στη σωστή σειρά pos1 < pos2
        If pos1 > pos2 Then
            tmp = pos1
            pos1 = pos2
            pos2 = tmp
        End If

        ' Αντέγραψε τις πολεις που ορίζονται στο τμήμα από pos1 εως pos2 από το
        ' πατέρα1 στο παιδί1. Δεν πειράζει που αντιγράφω τις πόλεις με τη σειρά
        ' αντι να τις τοποθετω στις θέσεις από pos1 εως pos2 γιατί στο τέλος 
        ' κάνω shift κατά pos1 θέσεις

        For i = pos1 To pos2
            child1.Add(Population(parent1).Path(i))
            child2.Add(Population(parent2).Path(i))
        Next

        ' Για τις υπόλοιπες πόλεις εκτός από το τμήμα pos1 ως pos2 
        Dim chpos As Integer

        For i = 1 To Map.Count
            ' ξεκινήσε απο δεξιά από τη θέση pos2 
            chpos = (pos2 + i) Mod Map.Count

            ' και έλεγξε αν η τρέχουσα πόλη από το πατέρα2 δεν εμπεριέχεται 
            ' στο παιδί 1 και αν ναι τότε προσθεσε σε την πόλη
            If Not child1.Contains(Population(parent2).Path(chpos)) Then
                child1.Add(Population(parent2).Path(chpos))
            End If
            ' το ίδιο για το δεύτερο ζευγάρι παιδί2 και πατέρας1
            If Not child2.Contains(Population(parent1).Path(chpos)) Then
                child2.Add(Population(parent1).Path(chpos))
            End If
        Next

        ' Μετακύλισε προς τα δεξιά το μονοπάτι΄κατά pos1 θέσεις ώστε να 
        ' είναι στη σωστή σειρα
        shiftRight(child1, pos1)
        shiftRight(child2, pos1)

        CheckForBest(New Chromosome(child1, 0))
        CheckForBest(New Chromosome(child2, 0))

        ' Αντικατέστησε το πατέρα με το παιδί
        Population(parent1).Replace(child1)
        Population(parent2).Replace(child2)
    End Sub

    '************************************************************************************
    '  CrossOver - η συνάρτηση επιλέγει μέλη για διασταύρωση. Η διαδικασία επαναλαμβάνεται
    '              τόσες φορές όσες ορίζει η πιθανότητα διασταύρωσης.   
    '************************************************************************************

    Public Sub CrossOver(Optional ByVal doPmx As Boolean = True)
        Dim i, plithos As Integer
        Dim parent1, parent2 As Integer

        plithos = CInt(ProbCrossOver * PopSize)

        For i = 1 To plithos
            Do
                parent1 = CInt(Int((PopSize * Rnd()) + 1)) - 1
                parent2 = CInt(Int((PopSize * Rnd()) + 1)) - 1
            Loop While parent1 = parent2

            If doPmx Then
                PMXCrossOver(parent1, parent2)
            Else
                OrderCrossOver(parent1, parent2)
            End If


        Next

    End Sub

    '************************************************************************************
    '  ChromosomeInversion - η συνάρτηση επιλέγει σε ένα μέλος δύο σημεία  στη διαδρομή του και 
    '                        αντιστρέφει τη σειρά διασχισης των πόλεων.   
    '************************************************************************************
    Public Sub ChromosomeInversion(ByVal Index As Integer)
        Dim pos1, pos2, tmp As Integer

        ' Επέλεξε τυχαία δύο διαφορετικές θέσεις στο μονοπάτι της διαδρομής
        Do
            pos1 = CInt(Int((Map.Count * Rnd()) + 1)) - 1
            pos2 = CInt(Int((Map.Count * Rnd()) + 1)) - 1
        Loop While pos1 = pos2

        ' Έλεγξε αν ειναι στη σωστή σειρά pos1 < pos2
        If pos1 > pos2 Then
            tmp = pos1
            pos1 = pos2
            pos2 = tmp
        End If

        Population(Index).Path.Reverse(pos1, (pos2 - pos1) + 1)
        Population(Index).Evaluate()

    End Sub

    '************************************************************************************
    '  Inversion - η συνάρτηση επιλέγει μέλη για αντιστροφή. Η διαδικασία επαναλαμβάνεται
    '              τόσες φορές όσες ορίζει η πιθανότητα αντιστροφής.   
    '************************************************************************************
    Public Sub Inversion()
        Dim i, plithos As Integer
        Dim parent1 As Integer

        plithos = CInt(ProbInversion * PopSize)

        For i = 1 To plithos
            parent1 = CInt(Int((PopSize * Rnd()) + 1)) - 1
            ChromosomeInversion(parent1)
        Next
    End Sub

    '************************************************************************************
    '  ChromosomeMutation -  η συνάρτηση επιλέγει σε ένα μέλος δύο σημεία  στη διαδρομή του και 
    '                        τα αντιμεταθέτει μεταξύ τους.   
    '************************************************************************************
    Public Sub ChromosomeMutation(ByVal Index As Integer)
        Dim pos1, pos2, tmp As Integer

        ' Επέλεξε τυχαία δύο διαφορετικές θέσεις στο μονοπάτι της διαδρομής
        Do
            pos1 = CInt(Int((Map.Count * Rnd()) + 1)) - 1
            pos2 = CInt(Int((Map.Count * Rnd()) + 1)) - 1
        Loop While pos1 = pos2


        tmp = Population(Index).Path(pos1)
        Population(Index).Path(pos1) = Population(Index).Path(pos2)
        Population(Index).Path(pos2) = tmp

        Population(Index).Evaluate()
    End Sub

    '************************************************************************************
    '  Mutation - η συνάρτηση επιλέγει μέλη για μετάλλαξη. Η διαδικασία επαναλαμβάνεται
    '              τόσες φορές όσες ορίζει η πιθανότητα μετάλλαξης.   
    '************************************************************************************
    Public Sub Mutation()
        Dim i, plithos As Integer
        Dim parent1 As Integer

        plithos = CInt(ProbMutation * PopSize)

        For i = 1 To plithos
            parent1 = CInt(Int((PopSize * Rnd()) + 1)) - 1
            ChromosomeMutation(parent1)
        Next
    End Sub


    '************************************************************************************
    '  CreateNewPopulation - η συνάρτηση δημιουργεί ένα νέο πληθυσμό χρησιμοποιώντας τη 
    '                        ρουλέτα επιλογής, όπου το κάθε μέλος απεικονίζεται με 
    '                        την αξία του.
    '************************************************************************************
    Private Sub __CreateNewPopulation()
        Dim msg As New System.Text.StringBuilder

        SortPopulation()

        Dim sum As Double = 0

        For Each x As Chromosome In Population
            sum = sum + x.Fitness
        Next

        Dim tmp As New List(Of Chromosome)

        Dim thesi As Integer

        For i As Integer = 1 To Population.Count
            Dim roulette As Double = sum * Rnd()
            thesi = 0
            Dim check As Double = Population.Item(thesi).Fitness

            While check < roulette
                thesi = thesi + 1
                check = check + Population.Item(thesi).Fitness
            End While

            tmp.Add(Population.Item(thesi))

            msg.Append(thesi.ToString())
            msg.Append(vbTab)
            msg.AppendLine(roulette.ToString())
        Next

        msg.AppendLine()
        ' Text1.Text = Text1.Text + msg.ToString()

        Population.Clear()

        For Each x In tmp
            Population.Add(New Chromosome(x.Path, x.Fitness))
        Next

        tmp.Clear()
    End Sub

    Private Sub CreateNewPopulation(Optional ByVal RankOrder As Boolean = True)
        Dim msg As New System.Text.StringBuilder
        Dim i, j As Integer

        SortPopulation()

        Dim sum As Double = 0

        i = 1
        For Each x As Chromosome In Population
            If RankOrder Then
                sum = sum + i
                i = i + 1
            Else
                sum = sum + x.Fitness
            End If

        Next

        Dim Pri As Double
        Dim CumProbs As New List(Of Double)
        Dim CumSum As Double

        CumSum = 0                             ' Αθροιστική πιθανότητα  
        For i = 0 To PopSize - 1
            If RankOrder Then
                Pri = (i + 1) / sum  ' Πιθανότητα επιλογής αξία / σύνολο αξιών
            Else
                Pri = Population(i).Fitness / sum  ' Πιθανότητα επιλογής αξία / σύνολο αξιών
            End If
            CumSum += Pri                      ' Όλα οι προηγούμενες πιθανότητες + η τρέχουσα
            CumProbs.Add(CumSum)
        Next i

        Dim r As Double
        Dim newPopIndexes As New List(Of Double)
        For i = 0 To PopSize - 1
            r = Rnd()
            j = 0
            While r >= CumProbs(j) And (j < PopSize - 1)
                j = j + 1
            End While
            newPopIndexes.Add(j)
        Next

        Dim tmp As New List(Of Chromosome)

        For Each thesi As Integer In newPopIndexes
            tmp.Add(Population.Item(thesi))
        Next

        Population.Clear()

        For Each x In tmp
            Population.Add(New Chromosome(x.Path, x.Fitness))
        Next

        tmp.Clear()
    End Sub

    '************************************************************************************
    '  DisplayPopulation - βοηθητική συνάρτηση για τηn εμφάνιση όλων των διαδρομών και των
    '                      αντίστοιχων αξιών τους.
    '************************************************************************************
    Private Sub DisplayPopulation(Optional ByVal strTitle As String = "")
        Dim x As Chromosome
        Dim msg As New System.Text.StringBuilder

        If (strTitle.Length > 0) Then
            msg.AppendLine(strTitle)
        End If

        For Each x In Population
            msg.AppendLine(x.PathToString(, True, "   "))
        Next
        Text1.Text = Text1.Text & msg.ToString()
    End Sub

    '************************************************************************************
    '  DrawPopulation - βοηθητική συνάρτηση για τη σχεδίαση στο χάρτη, με τη σειρά όλων 
    '                   των διαδρομών.
    '************************************************************************************

    Private Sub DrawChromosome(ByRef Chromo As Chromosome)
        Dim Bmp As Bitmap
        Dim grBmp As Graphics

        Bmp = BmpSource.Clone
        grBmp = Graphics.FromImage(Bmp)

        grBmp.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim points As New List(Of Point)

        For i = 0 To Map.Count - 1
            Dim y As City = Map(Chromo.Path(i))
            points.Add(New Point(y.X, y.Y))
        Next

        Dim myPen As Pen
        myPen = New Pen(Drawing.Color.Red, 2)

        grBmp.DrawPolygon(myPen, points.ToArray)
        PictureBox1.Image = Bmp
    End Sub

    '************************************************************************************
    '  DrawPopulation - βοηθητική συνάρτηση για τη σχεδίαση στο χάρτη, με τη σειρά όλων 
    '                   των διαδρομών.
    '************************************************************************************
    Private Sub DrawPopulation()

        For j = 0 To PopSize - 1
            DrawChromosome(Population(j))
            MsgBox("press any key to continue")
        Next

    End Sub

    



    Private Sub btnInfo_Click(sender As Object, e As EventArgs) Handles btnInfo.Click
        Dim x As New frmHelp
        x.ShowDialog(Me)
    End Sub


    '************************************************************************************
    '*
    '*  PMXCrossOver - PMX διασταύρωση ανάμεσα σε δύο χρωματοσώματα. 
    '*
    '************************************************************************************
    Public Sub PMXCrossOver(ByVal parent1 As Double, ByVal parent2 As Double)
        Dim pos1, pos2, tmp As Integer
        Dim child1 As New Hashtable
        Dim child2 As New Hashtable
        Dim i As Integer

        ' Επέλεξε τυχαία δύο διαφορετικές θέσεις στο μονοπάτι της διαδρομής
        Do
            pos1 = CInt(Int((Map.Count * Rnd()) + 1)) - 1
            pos2 = CInt(Int((Map.Count * Rnd()) + 1)) - 1
        Loop While pos1 = pos2

        ' Έλεγξε αν ειναι στη σωστή σειρά pos1 < pos2
        If pos1 > pos2 Then
            tmp = pos1
            pos1 = pos2
            pos2 = tmp
        End If

        ' Αντέγραψε τις πολεις που ορίζονται στο τμήμα από pos1 εως pos2 ανάμεσα 
        ' στους δύο γονείς και σημειώνω τις αλλαγές.
        For i = pos1 To pos2
            ' Καταγραφή αλλαγών στους δύο γονείς
            child1.Add(Population(parent1).Path(i), Population(parent2).Path(i))
            child2.Add(Population(parent2).Path(i), Population(parent1).Path(i))

            tmp = Population(parent1).Path(i)        ' αντιμετάθεση των πόλεων  
            Population(parent1).Path(i) = Population(parent2).Path(i)
            Population(parent2).Path(i) = tmp        ' μεταξύ των γονέων
        Next

        Dim k, stPos, endPos, searchValue As Double
        stPos = 0                        ' Αρχικά ελέγχω από την αρχή μέχρι   
        endPos = pos1 - 1                ' την έναρξη της ανταλλαγής 

        For k = 1 To 2     ' και για τα δύο τμήματα, πριν και μετά, το τμήμα της ανταλλαγής            

            For i = stPos To endPos     ' Για το εκάστοτε τμήμα, εκτός της ανταλλαγής                     
                searchValue = Population(parent1).Path(i)  ' και για τη κάθε πόλη του
                If child2.Contains(searchValue) Then       ' αν εμπεριέχεται στο τμήμα ανταλλαγής  
                    Do                                      ' τότε 
                        searchValue = child2(searchValue)   ' ψάχνω επαναληπτικά να βρώ ποια 
                    Loop While child2.Contains(searchValue)    ' την αντικαθιστά  
                    Population(parent1).Path(i) = searchValue  ' και αυτή γίνεται η νέα πόλη 
                End If
            Next

            For i = stPos To endPos                       ' το ίδιο για το δεύτερο γονέα
                searchValue = Population(parent2).Path(i)
                If child1.Contains(searchValue) Then
                    Do
                        searchValue = child1(searchValue)
                    Loop While child1.Contains(searchValue)
                    Population(parent2).Path(i) = searchValue
                End If
            Next

            stPos = pos2 + 1                             ' κάνω το ίδιο για το δεύτερο τμήμα 
            endPos = Population(parent1).Path.Count - 1  ' εκτός ανταλλαγής 
        Next


        Population(parent1).Evaluate()       ' Αξιολόγησε τις νέες διαδρομές
        Population(parent2).Evaluate()

        CheckForBest(Population(parent1))    ' Ελεγχος αν βελτιώνουν τη μέχρι τώρα καλύτερη
        CheckForBest(Population(parent2))    ' διαδρομή
    End Sub

    Private Sub MyApp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Dim dialog As DialogResult

        dialog = MessageBox.Show("Do you really want to close the app ?", "Exit", MessageBoxButtons.YesNo)

        If dialog = DialogResult.No Then

            e.Cancel = True

        Else

            Application.ExitThread()

        End If

    End Sub

    Private Sub SignOut_Click(sender As Object, e As EventArgs) Handles SignOut.Click

        Me.Hide()
        Form1.Show()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        TextBox1.Text = TextBox1.Text + 1
        If TextBox1.Text = 28 Then
            End
        End If

    End Sub
End Class
