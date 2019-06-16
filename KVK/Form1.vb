Public Class Form1


    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            MessageBox.Show(ListView1.SelectedItems(0).SubItems(0).Text & vbNewLine _
            & ListView1.SelectedItems(0).SubItems(1).Text & vbNewLine _
            & ListView1.SelectedItems(0).SubItems(2).Text & vbNewLine _
            & ListView1.SelectedItems(0).SubItems(3).Text & vbNewLine _
            & ListView1.SelectedItems(0).SubItems(4).Text & vbNewLine _
            & ListView1.SelectedItems(0).SubItems(5).Text,
            "Geselecteerd bedrijf:")
        Catch
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
        ListView1.BeginUpdate()
        ListView1.Items.Clear()

        Dim webClient As New System.Net.WebClient
        ' System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
        Try


            Dim result As String = webClient.DownloadString("https://zoeken.kvk.nl/Address.ashx?site=handelsregister&partialfields=&q=" & TextBox1.Text)



            Dim result1 = result.Replace("{" & Chr(34) & "resultatenHR" & Chr(34) & ":[", "")
            Dim result2 = result1.Replace("{" & Chr(34) & "handelsnaam" & Chr(34) & ": ", "")
            Dim result3 = result2.Replace(Chr(34) & "dossiernummer" & Chr(34) & ": ", "")
            Dim result4 = result3.Replace(Chr(34) & "subdossiernummer" & Chr(34) & ": ", "")
            Dim result5 = result4.Replace(Chr(34) & "vestigingsnummer" & Chr(34) & ": ", "")
            Dim result6 = result5.Replace(Chr(34) & "straat" & Chr(34) & ": ", "")
            Dim result7 = result6.Replace(Chr(34) & "huisnummer" & Chr(34) & ": ", "")
            Dim result8 = result7.Replace(Chr(34) & "huisnummertoevoeging" & Chr(34) & ": ", "")
            Dim result9 = result8.Replace(Chr(34) & "postcode" & Chr(34) & ": ", "")
            Dim result10 = result9.Replace(Chr(34) & "plaats" & Chr(34) & ": ", "")
            Dim result11 = result10.Replace(Chr(34) & "type" & Chr(34) & ":", "")
            Dim result12 = result11.Replace(Chr(34) & "vestiging" & Chr(34) & ":1", "")
            Dim result13 = result12.Replace(Chr(34) & "hoofdvestiging" & Chr(34) & ":0", "")
            Dim result14 = result13.Replace(Chr(34) & "vestiging" & Chr(34) & ":0", "")
            Dim result15 = result14.Replace(Chr(34) & "hoofdvestiging" & Chr(34) & ":1", "")
            Dim result16 = result15.Replace(", ", ",")
            Dim result17 = result16.Replace(Chr(34) & ":", Chr(34))
            Dim result18 = result17.Replace(",,},", "," & "")
            Dim result19 = result18.Replace(",}]}", "")
            Dim result20 = result19.Replace("," & Chr(34) & "Hoofdvestiging" & Chr(34) & ",", "" & Chr(13) & Chr(10))
            Dim result21 = result20.Replace("," & Chr(34) & "Nevenvestiging" & Chr(34) & ",", "" & Chr(13) & Chr(10))
            Dim result22 = result21.Replace("," & Chr(34) & "Rechtspersoon" & Chr(34) & ",", "" & Chr(13) & Chr(10))

            Dim result23 = result22.Replace(Chr(34) & "," & Chr(34), Chr(34) & ";" & Chr(34))

            Dim result24 = result23.Replace(Chr(34), "")

            TextBox2.Text = result24

            Dim TextLine As String = ""
            Dim SplitLine() As String

            For Each s As String In TextBox2.Lines
                System.Threading.Thread.Sleep(10) : Application.DoEvents()

                Dim nextLineText As String = s
                TextLine = s
                SplitLine = Split(TextLine, ";")
                Dim splitcode() As String = TextLine.Split(";")

                Try
                    Dim str(6) As String
                    Dim itm As ListViewItem
                    str(0) = splitcode(0)
                    str(1) = splitcode(1)
                    str(2) = splitcode(4)
                    str(3) = splitcode(5) & " " & splitcode(6)
                    str(4) = splitcode(7)
                    str(5) = splitcode(8)
                    itm = New ListViewItem(str)
                    ListView1.Items.Add(itm)
                Catch
                End Try
            Next

        Catch ex As Exception

        End Try

        ListView1.EndUpdate()
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)

        ToolStripStatusLabel1.Text = ListView1.Items.Count & " Resultaten."
    End Sub
End Class
