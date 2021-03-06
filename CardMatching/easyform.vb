﻿Imports System.IO
Public Class easy
    Dim es As New StartingForm
    Public score, counter, storehighscore, tries As Integer
    Private random As New Random
    Private icons =
        New List(Of String) From {"z", "z", "q", "q", ".", ".", "t", "t"}

    'for each block in tabellayout1 if that block is a label (checked by TryCast(variable , type) ) then on those label 
    'any random variable is generated by random function
    Private Sub AssignIconsToSquares()

        For Each variable In eTableLayoutPanel1.Controls
            Dim iconlabel = TryCast(variable, Label)
            If iconlabel IsNot Nothing Then
                Dim randomNumber = random.Next(icons.Count)
                iconlabel.Text = icons(randomNumber)
                'iconlabel.ForeColor = iconlabel.BackColor
                icons.removeat(randomNumber)
            End If
        Next
        eTimer2.Start()
        eTimer3.Start()

    End Sub
    'function like constructor
    Public Sub New()
        InitializeComponent()

        AssignIconsToSquares()


    End Sub

    Private firstClicked As Label = Nothing

    Private secondClicked As Label = Nothing
    'handles the user selected two labels are same then make them visible otherwise hide them
    Private Sub label_Click(ByVal sender As System.Object,
                    ByVal e As System.EventArgs) Handles eLabel9.Click,
eLabel8.Click, eLabel7.Click, eLabel6.Click, eLabel4.Click,
eLabel3.Click, eLabel2.Click, eLabel1.Click

        If eTimer1.Enabled Then Exit Sub

        Dim clickedLabel As Label = TryCast(sender, Label)

        If clickedLabel IsNot Nothing Then


            If clickedLabel.ForeColor = Color.Black Then Exit Sub

            'clickedLabel.ForeColor = Color.Black

            If firstClicked Is Nothing Then
                firstClicked = clickedLabel
                firstClicked.ForeColor = Color.Black
                Exit Sub
            End If

            secondClicked = clickedLabel
            secondClicked.ForeColor = Color.Black

            CheckForWinner()

            If firstClicked.Text = secondClicked.Text Then
                tries = tries + 1
                es.playoktone()
                score = score + 10
                firstClicked = Nothing
                secondClicked = Nothing
                eTextBox6.Text = score
                Exit Sub
            Else
                tries = tries + 1
                es.playerrortone()
                score = score - 5
                eTextBox6.Text = score
            End If

            eTimer1.Start()

        End If


    End Sub
    'This will manage if the two icons don’t match then after how long time hide both of them.
    Private Sub eTimer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eTimer1.Tick
        eTimer1.Stop()
        firstClicked.ForeColor = firstClicked.BackColor
        secondClicked.ForeColor = secondClicked.BackColor

        firstClicked = Nothing
        secondClicked = Nothing

    End Sub

    'This will manage the manage the starting display time
    Private Sub eTimer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eTimer2.Tick
        eTimer2.Stop()

        For Each variable In eTableLayoutPanel1.Controls
            Dim iconlabel = TryCast(variable, Label)
            If iconlabel IsNot Nothing Then
                iconlabel.ForeColor = iconlabel.BackColor
            End If
        Next

    End Sub

    'This timer is for managing the time taken by the user to complete the level
    Private Sub eTimer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles eTimer3.Tick
        counter = counter + 1
        eTextBox4.Text = counter
        eTextBox2.Text = tries
    End Sub

    Private Sub CheckForWinner()

        ' Go through all of the labels in the TableLayoutPanel,  
        ' checking each one to see if its icon is matched 
        For Each control In eTableLayoutPanel1.Controls
            Dim iconLabel = TryCast(control, Label)
            If iconLabel IsNot Nothing AndAlso
               iconLabel.ForeColor = iconLabel.BackColor Then Exit Sub
        Next

        ' If the loop didn't return, it didn't find  
        ' any unmatched icons 
        ' That means the user won. Show a message and close the form
        score = score + 10
        tries = tries + 1
        eTextBox6.Text = score
        eTextBox2.Text = tries
        eTimer3.Enabled = False
        es.playdonetone()
        MessageBox.Show("You matched all the icons! and you scored " & score & "and taken " & counter & "seconds ", "Congratulations ")
        storehighscore = score

        Dim filerd As StreamReader
        filerd = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\ehighscores.txt")
        Dim intscorearray(5), tempinteger, i As Integer
        Dim temps, namearray(5), scorearray(5), tempstring As String

        'read line from file and split them in scores and names

        For i = 0 To 4
            temps = filerd.ReadLine
            Dim array2() As String = Split(temps, "*", 2)
            namearray(i) = array2(0)
            scorearray(i) = array2(1)
        Next
        filerd.Close()

        'convert in int from string
        For i = 0 To 4
            intscorearray(i) = scorearray(i)
        Next

        ''sorting them and setting them according to value

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



        If (storehighscore > intscorearray(0)) Then
            intscorearray(0) = storehighscore
        End If


        scorearray(0) = intscorearray(0)
        namearray(0) = InputBox("Enter Your Name Here  ", " New HighScore", "Name")

        Dim file As StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "\ehighscores.txt", False)
        file.WriteLine(namearray(4) & "*" & scorearray(4))
        file.WriteLine(namearray(3) & "*" & scorearray(3))
        file.WriteLine(namearray(2) & "*" & scorearray(2))
        file.WriteLine(namearray(1) & "*" & scorearray(1))
        file.WriteLine(namearray(0) & "*" & scorearray(0))
        file.Close()


        StartingForm.Show()
        Close()

    End Sub

    Private Sub easy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
