Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Module Module1
    Public vgControlSync As Integer
    '------------variables congelar existencias
    Public vgControlEntrada As Integer
    Public vgCongTodo As Boolean
    Public vgCongLinea As String
    Public vgCongAlm As String
    '----------------------------
    Public MI_BD As String
    'Public Conex1 As New SqlConnection(My.Settings.ConexionLocal)
    Private Conex1 As String = My.Settings.ConexionLocal
    Private Conex2 As String = My.Settings.ConexionLocal2
    Private ConSAE As String = My.Settings.ConexionSAE
    Public XRUTA_DEST As String = "C:\Program Files\Common Files\Aspel\Sistemas Aspel\SAE5.00\Empresa01\Datos\SAE50EMPRE01.FDB"
    Public vgBD As String = "F"
    'Public myConnectionString As String = "Server=CARLOS-LAPTOP\SQLEXPRESS;Database=UnitelasDB;Trusted_Connection=True; MultipleActiveResultSets=True"
    'Public myConnectionString As String = "Server=ROBERTO-PC\SQLEXPRESS;Database=UnitelasDB;Trusted_Connection=True; MultipleActiveResultSets=True"
    'Public myConnectionString As String = "Server=TECRA\SQLEXPRESS;Database=UnitelasDB;Trusted_Connection=True; MultipleActiveResultSets=True"
    ' 5.44.152.58    localhost    5.32.16.121
    Public version As String = "Almacenes V 2.02"
    Public ruta As String '= "D:\Productos ADC Movil\Programas PC\CI Corporativo\Procter\Tepeji\"
    Public rutaD As String
    Public rutaT As String      ' Ruta de datos de la terminal
    Public Archivo As String
    Public vgAutorizTraspaso As Boolean
    Public vgEmpresa As String
    Public vgVersion As String
    Public vgNumEmpresa As String
    Public vgNumEmpresa6 As String = "06"
    Public vgEspecificarDoc As Boolean
    Public vgCodigoAlterno As Boolean
    Public vgControlWin As String = "T"
    Public vgTipoBD As String
    Public vgDiasActulizarSAE As Integer
    Public vgModiAutorizTrasp As String = "N"
    Public FlagPrint As Boolean = False
    Public FlagD As Boolean = False
    Public cont As Short         ' Contador
    Public FolioSalida As String ' Folio para nota de remision
    Public svelocidad As String  ' Bauds
    Public velocidad As Short    ' Bauds, indice actual del combo
    Public nro_puerto As Short   ' Cual puerto uso
    Public Usuario As String     ' ClaveUsuario
    Public Rol As String         ' Rol de usuario
    Public BanderaErrorCom As Boolean = False
    Public TipoReporte As String ' 1 = Por Responsable, 2 = Por Ubicacion, 3 = por Clasificacion
    Public BanderaActiva As Boolean = False
    Public BPrimeravez As Boolean = False
    Public Terminal As String
    Public vgId_usurio As String
    Public vgTipo_usurio As String
    ' Flag = 0 Activo en revision
    ' Flag = 1 Activo OK
    ' Flag = 2 Activo Modificado
    ' Flag = 3 Activo Agregado
    Public Inv_cant As Decimal
    'Public lastClave As String
    'Public lastRollo As String
    'Public lastTela As String
    'Public lastColor As String
    'Public lastDibujo As String
    'Public lastTelaT As String
    'Public lastColorT As String
    'Public lastDibujoT As String
    'Public lastMetros As String
    'Public lastKilos As String
    'Public lastPedido As String
    'Public lastPedimento As String
    'Public lastProvedor As String
    'Public lastDireccion1 As String
    'Public lastDireccion2 As String
    'Public lastHechoEn As String
    'Public lastObservaciones As String

    'Public lastPedidoS As String

    'Public PedidoProcesar As String
    Public ProductoSeleccionado As String
    Public NControlP As String
    Public Sub Main()
        Dim FrmPantalla As New MenuPrincipal
        FrmPantalla.Show()
    End Sub
    Public Sub ReiniciarMenu()
        MenuPrincipal.Dispose()
        Dim FrmPantalla As New MenuPrincipal
        FrmPantalla.Show()
    End Sub
    Public Function EjecutarQuerySQL(ByVal sql As String)
        Dim cnn As SqlConnection
        Dim cmd As SqlCommand
        cnn = New SqlConnection(Conex1)
        Try
            cnn.Open()
            cmd = New SqlCommand(sql, cnn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnn.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Public Function EjecutarQuerySQL_EMPRESA_X(ByVal sql As String, ByVal bd As String)
        Dim Str As String = "Data Source=" + My.Settings.vgCrearBD_IP + ";Initial Catalog=" + bd.Trim + ";Integrated Security=False;Uid=" +
            My.Settings.vgBDOtroLado_User + "; Password=" + My.Settings.pass + ";"
        'Dim Conexion As New SqlConnection(Str)
        Dim cnn As SqlConnection
        Dim cmd As SqlCommand
        cnn = New SqlConnection(Str)
        Try
            cnn.Open()
            cmd = New SqlCommand(sql, cnn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnn.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Public Function EjecutarQuerySQL_EMPRESA(ByVal sql As String)
        Dim Str As String = "Data Source=" + My.Settings.vgCrearBD_IP + ";Initial Catalog=" + MI_BD + ";Integrated Security=False;Uid=" +
            My.Settings.vgBDOtroLado_User + "; Password=" + My.Settings.pass + ";"
        'Dim Conexion As New SqlConnection(Str)
        Dim cnn As SqlConnection
        Dim cmd As SqlCommand
        cnn = New SqlConnection(Str)
        Try
            cnn.Open()
            cmd = New SqlCommand(sql, cnn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnn.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Public Function EjecutarQuerySQL_EMPRESA6(ByVal sql As String)
        'TRAER DATOS DEL ALMACEN
        '------------------------------------------------------------------------------------
        Dim Str7 As String = "Data Source=" + My.Settings.vgCrearBD_IP + ";Initial Catalog=LogistikMasterValvulerias;Integrated Security=False;Uid=" +
            My.Settings.vgBDOtroLado_User + "; Password=" + My.Settings.pass + ";"
        Dim conn7 As New SqlConnection(Str7)
        Dim sql7 As String = "SELECT * FROM EmpresaSAE WHERE TIPO = 'Almacen'"
        Dim dataadapter As New SqlDataAdapter(sql7, conn7)
        Dim dsCargarEncabezado As New DataSet()
        dataadapter.Fill(dsCargarEncabezado, "EmpresaSAE")
        Dim oTabla7 As DataTable
        oTabla7 = dsCargarEncabezado.Tables("EmpresaSAE")
        Dim SERVIDOR As String = ""
        Dim USUARIO As String = ""
        Dim VTIPO As String = ""
        Dim PASSWROD As String = ""
        Dim BASEDATOS As String = ""
        'Dim VTIPO As String = ""
        If oTabla7.Rows.Count >= 1 Then
            Dim oFila As DataRow
            For Each oFila In oTabla7.Rows
                SERVIDOR = oFila.Item("SERVIDOR").ToString
                BASEDATOS = oFila.Item("BASEDATOS").ToString
                USUARIO = oFila.Item("USUARIO").ToString
                PASSWROD = oFila.Item("PASSWROD").ToString
                VTIPO = oFila.Item("TIPO").ToString
                'VTIPO = oFila.Item("TIPO").ToString
            Next
        Else
            VTIPO = ""
        End If
        '------------------------------------------------------------------------------------
        Dim Str As String = "Data Source=" + My.Settings.vgCrearBD_IP + ";Initial Catalog=" + BASEDATOS + ";Integrated Security=False;Uid=" +
            My.Settings.vgBDOtroLado_User + "; Password=" + My.Settings.pass + ";"
        'Dim Conexion As New SqlConnection(Str)
        Dim cnn As SqlConnection
        Dim cmd As SqlCommand
        cnn = New SqlConnection(Str)
        Try
            cnn.Open()
            cmd = New SqlCommand(sql, cnn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnn.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
    Public Function EjecutarQuerySQL_SAE(ByVal sql As String)
        Dim cnn As SqlConnection
        Dim cmd As SqlCommand
        cnn = New SqlConnection(ConSAE)
        Try
            cnn.Open()
            cmd = New SqlCommand(sql, cnn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cnn.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function
End Module