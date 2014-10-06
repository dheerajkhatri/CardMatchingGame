Imports System.IO
Public Class StartingForm
    Public paths(7) As String

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sButton4.Click
        Me.Close()
    End Sub


    Private Sub readingfile()
        Dim basepath As String = "C:\Users\Public\Documents\resources\pathsoffiles.txt"
        Dim line As String
        Dim reader As StreamReader = New StreamReader(basepath)

        For i = 0 To 6
            line = reader.ReadLine()
            paths(i) = line
        Next

    End Sub


    Private Sub sButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sButton1.Click
        Me.Hide()
        sTimer1.Enabled = True
        If shardradio.Checked = True Then
            hard.Show()
        ElseIf smediumradio.Checked = True Then
            medium.Show()
        Else
            easy.Show()
        End If
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sButton3.Click
        MsgBox("Display all cards for a second than conceal. Match 2 cards and get points. For incorrect match charge penalties and increase the level of difficulty by increasing the card grid size(i.e., by increasing the number of cards in a grid) and reducing the display time", , "Rules For Card Matching Game")
    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sTimer1.Tick
        If Application.OpenForms().OfType(Of medium).Any Then

        ElseIf Application.OpenForms().OfType(Of easy).Any Then

        ElseIf Application.OpenForms().OfType(Of hard).Any Then

        Else
            Me.Show()
        End If

    End Sub



    Private Sub sButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sButton2.Click
        Dim filereader As StreamReader
        Dim highscores(5) As String
        Dim i, integerhighscores(5), intscorearray(5), tempinteger As Integer
        Dim temps, namearray(5), scorearray(5), tempstring As String
        readingfile()

        If sRadioButton2.Checked = True Then
            filereader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\mhighscores.txt")
        ElseIf sRadioButton3.Checked = True Then
            filereader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\ehighscores.txt")
        Else
            filereader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\hhighscores.txt")
        End If


        For i = 0 To 4
            temps = filereader.ReadLine
            Dim array2() As String = Split(temps, "*", 2)
            namearray(i) = array2(0)
            scorearray(i) = array2(1)
        Next
        filereader.Close()

        For i = 0 To 4
            intscorearray(i) = scorearray(i)
        Next

        For i = 0 To 4
            For j = 0 To 3

                If (intscorearray(j + 1) < intscorearray(j)) Then

                    tempinteger = intscorearray(j)
                    intscorearray(j) = intscorearray(j + 1)
                    intscorearray(j + 1) = tempinteger

                    tempstring = scorearray(j)
                    scorearray(j) = scorearray(j + 1)
                    scorearray(j + 1) = tempstring

                    tempstring = namearray(j)
                    namearray(j) = namearray(j + 1)
                    namearray(j + 1) = tempstring

                End If

            Next
        Next

        MsgBox(namearray(4) & Space(10) & scorearray(4) & vbNewLine &
                namearray(3) & Space(10) & scorearray(3) & vbNewLine &
                namearray(2) & Space(10) & scorearray(2) & vbNewLine &
                namearray(1) & Space(10) & scorearray(1) & vbNewLine &
                namearray(0) & Space(10) & scorearray(0) & vbNewLine, , "High Scores :")

    End Sub

    Private Sub StartingForm_Load()
        readingfile()
    End Sub

    Private Sub startingform_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim result As DialogResult = MessageBox.Show("Do you want to close the window?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.Yes Then
            e.Cancel = False
        ElseIf result = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Public Sub playoktone()
        readingfile()
        My.Computer.Audio.Play(Application.StartupPath & "\ok.wav", AudioPlayMode.Background)
    End Sub

    Public Sub playerrortone()
        readingfile()
        My.Computer.Audio.Play(Application.StartupPath & "\error.wav", AudioPlayMode.Background)
    End Sub


    Public Sub playdonetone()
        readingfile()
        My.Computer.Audio.Play(Application.StartupPath & "\done.wav", AudioPlayMode.Background)
    End Sub


    Private Sub StartingForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
'start form will not change the values in text files they will only be changed by easy medium or hard forms