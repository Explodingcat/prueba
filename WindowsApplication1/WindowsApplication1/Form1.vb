Imports System.DirectoryServices

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Users.Text = Environ("username")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Usr As String = Users.Text
        Dim Pas As String = TextBox2.Text
        Dim Dom As String = TextBox3.Text
        Dim Success As Boolean = False
        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & Dom)
        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)
        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel
        Try
            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)
        Catch
            Success = False
        End Try
        If Success = True Then
            Dim currentADUser As System.DirectoryServices.AccountManagement.UserPrincipal
            currentADUser = System.DirectoryServices.AccountManagement.UserPrincipal.Current
            Dim userEmail As String = currentADUser.EmailAddress

            Dim userFullName As String = currentADUser.Name
            TextBox4.Text = userFullName
            TextBox5.Text = userEmail
            TextBox6.Text = currentADUser.Description.ToString()
            TextBox7.Text = currentADUser.DistinguishedName.ToString()
        Else
            MsgBox("No existen")
        End If
        Entry.Close()
    End Sub

    Private Sub Camb_c_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Camb_c.Click
        Dim Usr As String = Users.Text
        Dim Pas As String = TextBox2.Text
        Dim Dom As String = TextBox3.Text
        Dim Success As Boolean = False
        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & Dom, Usr, Pas)
        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)
        Dim nueva As String
        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel
        Try
            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)
        Catch
            Success = False
        End Try
        If Success = True Then
            Try
                Dim currentADUser As System.DirectoryServices.AccountManagement.UserPrincipal
                currentADUser = System.DirectoryServices.AccountManagement.UserPrincipal.Current
                nueva = camb_tb.Text
                currentADUser.ChangePassword(Pas, nueva)
                MsgBox("Contraseña Cambiada")
            Catch
                Dim currentADUser As System.DirectoryServices.AccountManagement.UserPrincipal
                currentADUser = System.DirectoryServices.AccountManagement.UserPrincipal.Current
                MsgBox(currentADUser.LastPasswordSet)
            End Try
        Else
            MsgBox("No existen")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Limpiar.Click
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Usr As String = Users.Text
        Dim Pas As String = TextBox2.Text
        Dim Dom As String = TextBox3.Text
        Dim authentic As Boolean = False
        Try
            Dim entry As New DirectoryEntry("LDAP://" & Dom, Usr, Pas)
            Dim nativeObject As Object = entry.NativeObject
            authentic = True
        Catch generatedExceptionName As DirectoryServicesCOMException
        End Try
        MsgBox(authentic)
    End Sub
End Class
