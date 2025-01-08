Imports System.Data.SqlClient
'Imports OpenNETCF.Desktop.Communication
'Imports FirebirdSql.Data.Client
'Imports FirebirdSql.Data.FirebirdClient
Imports System
Imports System.IO
Imports System.Math
Imports System.Xml
Imports System.DateTime
Imports Microsoft.VisualBasic
'Imports Microsoft.Office.Interop.Excel
'Imports Excel = Microsoft.Office.Interop.Excel
'Imports System.Data.Sql
'Imports System.Net.Mail
'Imports System.Net.Mime
'Imports System.Security.Cryptography
'Imports System.Security.Cryptography.X509Certificates
Public Class CamposLibres
    Private sqllocal As String
    Private cmd As SqlCommand
    Private CampLib1, CampLib2, CampLib3, CampLib4, CampLib5, CampLib6, CampLib7 As String
    Private CampLib8, CampLib9, CampLib10, CampLib11, CampLib12 As String

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then CampLib1 = 1
        If CheckBox1.Checked = False Then CampLib1 = 0
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then CampLib2 = 1
        If CheckBox2.Checked = False Then CampLib2 = 0
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then CampLib3 = 1
        If CheckBox3.Checked = False Then CampLib3 = 0
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then CampLib4 = 1
        If CheckBox4.Checked = False Then CampLib4 = 0
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then CampLib5 = 1
        If CheckBox5.Checked = False Then CampLib5 = 0
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then CampLib6 = 1
        If CheckBox6.Checked = False Then CampLib6 = 0
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = True Then CampLib7 = 1
        If CheckBox7.Checked = False Then CampLib7 = 0
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked = True Then CampLib8 = 1
        If CheckBox8.Checked = False Then CampLib8 = 0
    End Sub

    Private Sub CheckBox9_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.Checked = True Then CampLib9 = 1
        If CheckBox9.Checked = False Then CampLib9 = 0
    End Sub

    Private Sub CheckBox10_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.Checked = True Then CampLib10 = 1
        If CheckBox10.Checked = False Then CampLib10 = 0
    End Sub

    Private Sub CheckBox11_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox11.CheckedChanged
        If CheckBox11.Checked = True Then CampLib11 = 1
        If CheckBox11.Checked = False Then CampLib11 = 0
    End Sub

    Private Sub CheckBox12_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox12.CheckedChanged
        If CheckBox12.Checked = True Then CampLib12 = 1
        If CheckBox12.Checked = False Then CampLib12 = 0
    End Sub

    Private Sub CheckBox13_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox13.CheckedChanged
        If CheckBox13.Checked = True Then CampLib13 = 1
        If CheckBox13.Checked = False Then CampLib13 = 0
    End Sub

    Private Sub CheckBox14_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox14.CheckedChanged
        If CheckBox14.Checked = True Then CampLib14 = 1
        If CheckBox14.Checked = False Then CampLib14 = 0
    End Sub

    Private Sub CheckBox15_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox15.CheckedChanged
        If CheckBox15.Checked = True Then CampLib15 = 1
        If CheckBox15.Checked = False Then CampLib15 = 0
    End Sub

    Private Sub CheckBox16_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox16.CheckedChanged
        If CheckBox16.Checked = True Then CampLib16 = 1
        If CheckBox16.Checked = False Then CampLib16 = 0
    End Sub

    Private Sub CheckBox17_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox17.CheckedChanged
        If CheckBox17.Checked = True Then CampLib17 = 1
        If CheckBox17.Checked = False Then CampLib17 = 0
    End Sub

    Private Sub CheckBox18_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox18.CheckedChanged
        If CheckBox18.Checked = True Then CampLib18 = 1
        If CheckBox18.Checked = False Then CampLib18 = 0
    End Sub

    Private Sub CheckBox19_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox19.CheckedChanged
        If CheckBox19.Checked = True Then CampLib19 = 1
        If CheckBox19.Checked = False Then CampLib19 = 0
    End Sub

    Private Sub CheckBox20_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox20.CheckedChanged
        If CheckBox20.Checked = True Then CampLib20 = 1
        If CheckBox20.Checked = False Then CampLib20 = 0
    End Sub

    Private Sub CheckBox21_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox21.CheckedChanged
        If CheckBox21.Checked = True Then CampLib21 = 1
        If CheckBox21.Checked = False Then CampLib21 = 0
    End Sub

    Private Sub CheckBox22_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox22.CheckedChanged
        If CheckBox22.Checked = True Then CampLib22 = 1
        If CheckBox22.Checked = False Then CampLib22 = 0
    End Sub

    Private Sub CheckBox23_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox23.CheckedChanged
        If CheckBox23.Checked = True Then CampLib23 = 1
        If CheckBox23.Checked = False Then CampLib23 = 0
    End Sub

    Private Sub CheckBox24_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox24.CheckedChanged
        If CheckBox24.Checked = True Then CampLib24 = 1
        If CheckBox24.Checked = False Then CampLib24 = 0
    End Sub

    Private Sub CheckBox25_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox25.CheckedChanged
        If CheckBox25.Checked = True Then CampLib25 = 1
        If CheckBox25.Checked = False Then CampLib25 = 0
    End Sub

    Private Sub CheckBox26_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox26.CheckedChanged
        If CheckBox26.Checked = True Then CampLib26 = 1
        If CheckBox26.Checked = False Then CampLib26 = 0
    End Sub

    Private Sub CheckBox27_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox27.CheckedChanged
        If CheckBox27.Checked = True Then CampLib27 = 1
        If CheckBox27.Checked = False Then CampLib27 = 0
    End Sub

    Private Sub CheckBox28_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox28.CheckedChanged
        If CheckBox28.Checked = True Then CampLib28 = 1
        If CheckBox28.Checked = False Then CampLib28 = 0
    End Sub

    Private Sub CheckBox29_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox29.CheckedChanged
        If CheckBox29.Checked = True Then CampLib29 = 1
        If CheckBox29.Checked = False Then CampLib29 = 0
    End Sub

    Private Sub CheckBox30_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox30.CheckedChanged
        If CheckBox30.Checked = True Then CampLib30 = 1
        If CheckBox30.Checked = False Then CampLib30 = 0
    End Sub

    Private Sub CheckBox31_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox31.CheckedChanged
        If CheckBox31.Checked = True Then CampLib31 = 1
        If CheckBox31.Checked = False Then CampLib31 = 0
    End Sub

    Private Sub CheckBox32_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox32.CheckedChanged
        If CheckBox32.Checked = True Then CampLib32 = 1
        If CheckBox32.Checked = False Then CampLib32 = 0
    End Sub

    Private Sub CheckBox33_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox33.CheckedChanged
        If CheckBox33.Checked = True Then CampLib33 = 1
        If CheckBox33.Checked = False Then CampLib33 = 0
    End Sub

    Private Sub CheckBox34_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox34.CheckedChanged
        If CheckBox34.Checked = True Then CampLib34 = 1
        If CheckBox34.Checked = False Then CampLib34 = 0
    End Sub

    Private Sub CheckBox35_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox35.CheckedChanged
        If CheckBox35.Checked = True Then CampLib35 = 1
        If CheckBox35.Checked = False Then CampLib35 = 0
    End Sub

    Private Sub CheckBox36_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox36.CheckedChanged
        If CheckBox36.Checked = True Then CampLib36 = 1
        If CheckBox36.Checked = False Then CampLib36 = 0
    End Sub

    Private Sub CheckBox37_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox37.CheckedChanged
        If CheckBox37.Checked = True Then CampLib37 = 1
        If CheckBox37.Checked = False Then CampLib37 = 0
    End Sub

    Private Sub CheckBox38_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox38.CheckedChanged
        If CheckBox38.Checked = True Then CampLib38 = 1
        If CheckBox38.Checked = False Then CampLib38 = 0
    End Sub

    Private Sub CheckBox39_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox39.CheckedChanged
        If CheckBox39.Checked = True Then CampLib39 = 1
        If CheckBox39.Checked = False Then CampLib39 = 0
    End Sub

    Private Sub CheckBox40_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox40.CheckedChanged
        If CheckBox40.Checked = True Then CampLib40 = 1
        If CheckBox40.Checked = False Then CampLib40 = 0
    End Sub

    Private Sub CheckBox41_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox41.CheckedChanged
        If CheckBox41.Checked = True Then CampLib41 = 1
        If CheckBox41.Checked = False Then CampLib41 = 0
    End Sub

    Private Sub CheckBox42_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox42.CheckedChanged
        If CheckBox42.Checked = True Then CampLib42 = 1
        If CheckBox42.Checked = False Then CampLib42 = 0
    End Sub

    Private Sub CheckBox43_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox43.CheckedChanged
        If CheckBox43.Checked = True Then CampLib43 = 1
        If CheckBox43.Checked = False Then CampLib43 = 0
    End Sub

    Private Sub CheckBox44_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox44.CheckedChanged
        If CheckBox44.Checked = True Then CampLib44 = 1
        If CheckBox44.Checked = False Then CampLib44 = 0
    End Sub

    Private Sub CheckBox45_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox45.CheckedChanged
        If CheckBox45.Checked = True Then CampLib45 = 1
        If CheckBox45.Checked = False Then CampLib45 = 0
    End Sub

    Private CampLib13, CampLib14, CampLib15, CampLib16, CampLib17 As String
    Private CampLib18, CampLib19, CampLib20, CampLib21, CampLib22 As String
    Private CampLib23, CampLib24, CampLib25, CampLib26, CampLib27 As String
    Private CampLib28, CampLib29, CampLib30, CampLib31, CampLib32 As String
    Private CampLib33, CampLib34, CampLib35, CampLib36, CampLib37 As String
    Private CampLib38, CampLib39, CampLib40, CampLib41, CampLib42 As String
    Private CampLib43, CampLib44, CampLib45 As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Str As String = "Data Source=" + My.Settings.vgCrearBD_IP + ";Initial Catalog=LogistikMasterValvulerias06;Integrated Security=False;Uid=" + My.Settings.vgBDOtroLado_User + "; Password=" + My.Settings.pass + ";"
        Dim connLg As New SqlConnection(Str)
        If connLg.State = Data.ConnectionState.Closed Then
            connLg.Open()
        End If
        sqllocal = "Update Logistik_Config_SAE set CampLib1=" + CampLib1 + ",CampLib2=" + CampLib2 +
        ",CampLib3=" + CampLib3 + ", CampLib4=" + CampLib4 + ", CampLib5=" + CampLib5 + ", CampLib6=" + CampLib6 + ", CampLib7=" + CampLib7 +
        ",CampLib8=" + CampLib8 + ", CampLib9=" + CampLib9 + ", CampLib10=" + CampLib10 + ", CampLib11=" + CampLib11 + ", CampLib12=" + CampLib12 +
        ",CampLib13=" + CampLib13 + ", CampLib14=" + CampLib14 + ", CampLib15=" + CampLib15 + ", CampLib16=" + CampLib16 + ", CampLib17=" + CampLib17 +
        ",CampLib18=" + CampLib18 + ", CampLib19=" + CampLib19 + ", CampLib20=" + CampLib20 + ", CampLib21=" + CampLib21 + ", CampLib22=" + CampLib22 +
        ",CampLib23=" + CampLib23 + ", CampLib24=" + CampLib24 + ", CampLib25=" + CampLib26 + ", CampLib26=" + CampLib26 + ", CampLib27=" + CampLib27 +
        ",CampLib28=" + CampLib28 + ", CampLib29=" + CampLib29 + ", CampLib30=" + CampLib30 + ", CampLib31=" + CampLib31 + ", CampLib32=" + CampLib32 +
        ",CampLib33=" + CampLib33 + ", CampLib34=" + CampLib34 + ", CampLib35=" + CampLib35 + ", CampLib36=" + CampLib36 + ", CampLib37=" + CampLib37 +
        ",CampLib38=" + CampLib38 + ", CampLib39=" + CampLib39 + ", CampLib40=" + CampLib40 + ", CampLib41=" + CampLib41 + ", CampLib42=" + CampLib42 +
        ",CampLib43=" + CampLib43 + ", CampLib44=" + CampLib44 + ", CampLib45=" + CampLib45
        cmd = New SqlCommand(sqllocal, connLg)
        cmd.ExecuteNonQuery()
        MsgBox("Datos Almacenas almacenados", vbInformation, "Guardar")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub


    Private Sub CamposLibres_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Str As String = "Data Source=" + My.Settings.vgCrearBD_IP + ";Initial Catalog=LogistikMasterValvulerias06;Integrated Security=False;Uid=" + My.Settings.vgBDOtroLado_User + "; Password=" + My.Settings.pass + ";"
        Dim connLg As New SqlConnection(Str)
        If connLg.State = Data.ConnectionState.Closed Then
            connLg.Open()
        End If
        '/////////////query quien si y quin no
        sqllocal = "SELECT NUM_EMP, MascaraOrdenCompra, MascaraRecepcion, MascaraPedido, MascaraRemision" +
        ",MultiAlmacen, InvNumDecimales, ActivarQuerys, CampLib1, CampLib2" +
        ",CampLib3, CampLib4, CampLib5, CampLib6, CampLib7" +
        ",CampLib8, CampLib9, CampLib10, CampLib11, CampLib12" +
        ",CampLib13, CampLib14, CampLib15, CampLib16, CampLib17" +
        ",CampLib18, CampLib19, CampLib20, CampLib21, CampLib22" +
        ",CampLib23, CampLib24, CampLib25, CampLib26, CampLib27" +
        ",CampLib28, CampLib29, CampLib30, CampLib31, CampLib32" +
        ",CampLib33, CampLib34, CampLib35, CampLib36, CampLib37" +
        ",CampLib38, CampLib39, CampLib40, CampLib41, CampLib42" +
        ",CampLib43, CampLib44, CampLib45" +
        " From Logistik_Config_SAE"
        Dim readerActivos As SqlDataReader
        If connLg.State = ConnectionState.Closed Then
            connLg.Open()
        End If
        cmd = New SqlCommand(sqllocal, connLg)
        readerActivos = cmd.ExecuteReader()
        While readerActivos.Read()
            CampLib1 = readerActivos.Item("camplib1").ToString
            CampLib2 = readerActivos.Item("camplib2").ToString
            CampLib3 = readerActivos.Item("camplib3").ToString
            CampLib4 = readerActivos.Item("camplib4").ToString
            CampLib5 = readerActivos.Item("camplib5").ToString
            CampLib6 = readerActivos.Item("camplib6").ToString
            CampLib7 = readerActivos.Item("camplib7").ToString
            CampLib8 = readerActivos.Item("camplib8").ToString
            CampLib9 = readerActivos.Item("camplib9").ToString
            CampLib10 = readerActivos.Item("camplib10").ToString
            CampLib11 = readerActivos.Item("camplib11").ToString
            CampLib12 = readerActivos.Item("camplib12").ToString
            CampLib13 = readerActivos.Item("camplib13").ToString
            CampLib14 = readerActivos.Item("camplib14").ToString
            CampLib15 = readerActivos.Item("camplib15").ToString
            CampLib16 = readerActivos.Item("camplib16").ToString
            CampLib17 = readerActivos.Item("camplib17").ToString
            CampLib18 = readerActivos.Item("camplib18").ToString
            CampLib19 = readerActivos.Item("camplib19").ToString
            CampLib20 = readerActivos.Item("camplib20").ToString
            CampLib21 = readerActivos.Item("camplib21").ToString
            CampLib22 = readerActivos.Item("camplib22").ToString
            CampLib23 = readerActivos.Item("camplib23").ToString
            CampLib24 = readerActivos.Item("camplib24").ToString
            CampLib25 = readerActivos.Item("camplib25").ToString
            CampLib26 = readerActivos.Item("camplib26").ToString
            CampLib27 = readerActivos.Item("camplib27").ToString
            CampLib28 = readerActivos.Item("camplib28").ToString
            CampLib29 = readerActivos.Item("camplib29").ToString
            CampLib30 = readerActivos.Item("camplib30").ToString
            CampLib31 = readerActivos.Item("camplib31").ToString
            CampLib32 = readerActivos.Item("camplib32").ToString
            CampLib33 = readerActivos.Item("camplib33").ToString
            CampLib34 = readerActivos.Item("camplib34").ToString
            CampLib35 = readerActivos.Item("camplib35").ToString
            CampLib36 = readerActivos.Item("camplib36").ToString
            CampLib37 = readerActivos.Item("camplib37").ToString
            CampLib38 = readerActivos.Item("camplib38").ToString
            CampLib39 = readerActivos.Item("camplib39").ToString
            CampLib40 = readerActivos.Item("camplib40").ToString
            CampLib41 = readerActivos.Item("camplib41").ToString
            CampLib42 = readerActivos.Item("camplib42").ToString
            CampLib43 = readerActivos.Item("camplib43").ToString
            CampLib44 = readerActivos.Item("camplib44").ToString
            CampLib45 = readerActivos.Item("camplib45").ToString
        End While
        readerActivos.Close()
        If CampLib1 = 1 Then CheckBox1.Checked = True
        If CampLib2 = 1 Then CheckBox2.Checked = True
        If CampLib3 = 1 Then CheckBox3.Checked = True
        If CampLib4 = 1 Then CheckBox4.Checked = True
        If CampLib5 = 1 Then CheckBox5.Checked = True
        If CampLib6 = 1 Then CheckBox6.Checked = True
        If CampLib7 = 1 Then CheckBox7.Checked = True
        If CampLib8 = 1 Then CheckBox8.Checked = True
        If CampLib9 = 1 Then CheckBox9.Checked = True
        If CampLib10 = 1 Then CheckBox10.Checked = True
        If CampLib11 = 1 Then CheckBox11.Checked = True
        If CampLib12 = 1 Then CheckBox12.Checked = True
        If CampLib13 = 1 Then CheckBox13.Checked = True
        If CampLib14 = 1 Then CheckBox14.Checked = True
        If CampLib15 = 1 Then CheckBox15.Checked = True
        If CampLib16 = 1 Then CheckBox16.Checked = True
        If CampLib17 = 1 Then CheckBox17.Checked = True
        If CampLib18 = 1 Then CheckBox18.Checked = True
        If CampLib19 = 1 Then CheckBox19.Checked = True
        If CampLib20 = 1 Then CheckBox20.Checked = True
        If CampLib21 = 1 Then CheckBox21.Checked = True
        If CampLib22 = 1 Then CheckBox22.Checked = True
        If CampLib23 = 1 Then CheckBox23.Checked = True
        If CampLib24 = 1 Then CheckBox24.Checked = True
        If CampLib25 = 1 Then CheckBox25.Checked = True
        If CampLib26 = 1 Then CheckBox26.Checked = True
        If CampLib27 = 1 Then CheckBox27.Checked = True
        If CampLib28 = 1 Then CheckBox28.Checked = True
        If CampLib29 = 1 Then CheckBox29.Checked = True
        If CampLib30 = 1 Then CheckBox30.Checked = True
        If CampLib31 = 1 Then CheckBox31.Checked = True
        If CampLib32 = 1 Then CheckBox32.Checked = True
        If CampLib33 = 1 Then CheckBox33.Checked = True
        If CampLib34 = 1 Then CheckBox34.Checked = True
        If CampLib35 = 1 Then CheckBox35.Checked = True
        If CampLib36 = 1 Then CheckBox36.Checked = True
        If CampLib37 = 1 Then CheckBox37.Checked = True
        If CampLib38 = 1 Then CheckBox38.Checked = True
        If CampLib39 = 1 Then CheckBox39.Checked = True
        If CampLib40 = 1 Then CheckBox40.Checked = True
        If CampLib41 = 1 Then CheckBox41.Checked = True
        If CampLib42 = 1 Then CheckBox42.Checked = True
        If CampLib43 = 1 Then CheckBox43.Checked = True
        If CampLib44 = 1 Then CheckBox44.Checked = True
        If CampLib45 = 1 Then CheckBox45.Checked = True
    End Sub
End Class