'---------EMCRIPTAR
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.UnicodeEncoding
'------------------------------------
Imports System.Data.SqlClient
'Imports System.Diagnostics
'Imports System.Runtime.InteropServices
Imports Microsoft.Office
Imports System.IO
Imports System.Data
Imports System.Threading

Public Class SQL
    '---------------------------------------------------
    Private des As New TripleDESCryptoServiceProvider 'Algorithmo TripleDES
    Private hashmd5 As New MD5CryptoServiceProvider 'objeto md5
    Private myKey As String = "MyKey2012" 'Clave secreta(puede alterarse)
    '---------------------------------------------------
    'Dim myConnection As SqlConnection = New SqlConnection(My.Settings.MicrosDBConnectionString)
    Dim myConnection As SqlConnection = New SqlConnection(My.Settings.ConexionLocal)
    'Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    '          Now.ToString("MM/dd/yyyy HH:mm") Carlos
    ' DateTime.Now.ToString("dd/MM/yyyy HH:mm") Jorge
    'Dim tipoFecha As String = "dd/MM/yyyy HH:mm"
    Dim tipoFecha As String = "MM/dd/yyyy HH:mm"
    Dim ConnectionString As String = "777Integrated Security=SSPI;" + "Initial Catalog=MicrosDB;" + "Data Source=localhost;" + "MultipleActiveResultSets=True;"
    Dim conn As New SqlConnection(ConnectionString)

    Structure Activo
        Dim Ubicacion As String
        Dim Codigo As String
        Dim Nombre As String
        Dim Documento As String
        Dim Pedimento As String
        Dim FechaP As String
        Dim Aduana As String
        Dim Serie As String
        Dim Fecha As String
        Dim Cantidad As String
        Dim Costo As String
        Dim Minimo As String
        Dim Reorden As String
        Dim Maximo As String
    End Structure
    Dim ActivoP As New Activo

    Structure Empleado
        Dim Nombre As String
        Dim Clave As String
        Dim Rol As String
    End Structure
    Dim EmpleadoP As New Empleado

    Dim oWS As Interop.Excel.Worksheet
    Dim saux As String
    Dim x As Integer = 0
    Dim y As Integer = 0
    Dim iHoja As Integer        ' Numero de Hoja
    Dim cambiohoja As Boolean
    Dim FlagIni As Boolean = True
    Dim customer As String

    Public Sub cTable() ' Crea Base de Datos

        Dim obj As SqlCommand
        Dim strSQL As String

        'Dim srv As Server
        'srv = New Server

        'srv.Settings.LoginMode = ServerLoginMode.Integrated
        ''Modify settings specific to the current connection in UserOptions.
        'srv.UserOptions.AbortOnArithmeticErrors = True
        ''Run the Alter method to make the changes on the instance of SQL Server.
        'srv.Alter()

        ' Trata de abrir la tabla, si no existe, la crea
        Try
            conn.Open()
            conn.Close()
        Catch ae As SqlException
            'MessageBox.Show(ae.Message.ToString())
            ' Create the database
            ConnectionString = "777Integrated Security=SSPI;" + "Initial Catalog=;" + "Data Source=localhost;"
            conn = New SqlConnection(ConnectionString)

            conn.Open()
            obj = conn.CreateCommand()
            strSQL = "CREATE DATABASE " & "MicrosDB"
            ' Execute
            obj.CommandText = strSQL
            obj.ExecuteNonQuery()

            'Sleep(1500)
            ' Create a table
            ConnectionString = "777Integrated Security=SSPI;" + "Initial Catalog=MicrosDB;" + "Data Source=localhost;"
            conn = New SqlConnection(ConnectionString)
            conn.Open()

            obj = conn.CreateCommand()

            strSQL = "CREATE TABLE Inventario (ID int IDENTITY(0,1) PRIMARY KEY, Ubicacion NCHAR(50), Codigo NCHAR (25), Descripcion NCHAR(100), Serie NCHAR(25), Cantidad int, Status NCHAR(5))"
            ' Execute
            obj.CommandText = strSQL
            Try
                obj.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            strSQL = "CREATE TABLE Productos (Codigo NCHAR(25), Descripcion NCHAR (100), Costo NCHAR (15), Minimo NCHAR (10), Reorden NCHAR (10), Maximo NCHAR (10), Cantidad NCHAR (10), BanderaSerie NCHAR (5))"
            ' Execute
            obj.CommandText = strSQL
            Try
                obj.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            strSQL = "CREATE TABLE Empleados (Codigo NCHAR(15), Nombre NCHAR (50), Clave NCHAR (10), Rol NCHAR (5))"
            ' Execute
            obj.CommandText = strSQL
            Try
                obj.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ' Crea el Usuario Admin
            strSQL = "INSERT INTO Empleados(Codigo, Nombre, Clave, Rol) VALUES ('123','Admin','123','1')"
            ' Execute
            obj.CommandText = strSQL
            Try
                obj.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            strSQL = "CREATE TABLE Movimientos (ID int IDENTITY(0,1) PRIMARY KEY, CodigoEmpl NCHAR(15), Movimiento NCHAR(10), Documento NCHAR(30), Codigo NCHAR(25), Ubicacion NCHAR (50), Fecha datetime, Aduana NCHAR(50), Serie NCHAR(25), Cantidad NCHAR(10), Pedimento NCHAR(20), FPedimento NCHAR(25) )"
            ' Execute
            obj.CommandText = strSQL
            Try
                obj.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            strSQL = "CREATE TABLE Ubicaciones (ID int IDENTITY(0,1) PRIMARY KEY, Ubicacion NCHAR (50))"
            ' Execute
            obj.CommandText = strSQL
            Try
                obj.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            strSQL = "CREATE TABLE Aduanas (ID int IDENTITY(0,1) PRIMARY KEY, Aduana NCHAR (50))"
            ' Execute
            obj.CommandText = strSQL
            Try
                obj.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try


            conn.Close()
            conn = Nothing

        End Try

    End Sub
    Public Function EMCRIPTAR(ByVal texto As String) As String
        Dim Encriptar As String
        If Trim(texto) = "" Then
            Encriptar = ""
        Else
            des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(myKey))
            des.Mode = CipherMode.ECB
            Dim encrypt As ICryptoTransform = des.CreateEncryptor()
            Dim buff() As Byte = UnicodeEncoding.ASCII.GetBytes(texto)
            Encriptar = System.Convert.ToBase64String(encrypt.TransformFinalBlock(buff, 0, buff.Length))
        End If
        Return Encriptar
    End Function
    Private Function Desencriptar(ByVal texto As String) As String
        If Trim(texto) = "" Then
            Desencriptar = ""
        Else
            des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(myKey))
            des.Mode = CipherMode.ECB
            Dim desencrypta As ICryptoTransform = des.CreateDecryptor()
            Dim buff() As Byte = System.Convert.FromBase64String(texto)
            Desencriptar = System.Text.UnicodeEncoding.ASCII.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length))
        End If
        Return Desencriptar
    End Function
    ' Importa a la tabla de Productos del archivo de Excell
    Public Sub convertToSQL_ProductosN()

        Dim oXL As Interop.Excel.Application
        Dim oWB As Interop.Excel.Workbook
        Dim oWS As Interop.Excel.Worksheet
        Dim codigo As String = "123"
        'Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        'Dim str As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        'Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        inicio = TimeOfDay

        oXL = New Interop.Excel.Application
        oWB = oXL.Workbooks.Open(Archivo)
        oWS = oXL.Worksheets(1)

        On Error GoTo SQLErrorSQ

        If myConnection.State = ConnectionState.Closed Then
            myConnection.Open()
        End If

        repetido = False

        y = 2
        While codigo <> ""
            codigo = oWS.Cells(y, 1).Value

            If codigo <> "" Then
                sqlInsertRow.CommandText = "INSERT INTO Productos(Codigo, Descripcion, Costo, Minimo, Reorden, Maximo, BanderaSerie) VALUES (" & _
                "'" & oWS.Cells(y, 1).Value & "','" & oWS.Cells(y, 2).Value & "','" & oWS.Cells(y, 3).Value & "','" & _
                oWS.Cells(y, 4).Value & "','" & oWS.Cells(y, 5).Value & "','" & oWS.Cells(y, 6).Value & "','" & oWS.Cells(y, 7).Value & "')"
                sqlInsertRow.ExecuteNonQuery()
            End If
            y = y + 1
        End While

        GoTo FinSQ

SQLErrorSQ:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinSQ
        End If

FinSQ:
        'Close the connection
        If myConnection.State = ConnectionState.Open Then
            myConnection.Close()
        End If

        oWB.Close()
        oXL.Quit()

        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub

    ' Importa a la tabla de Inventario del archivo de Excell
    Public Sub convertToSQL_InventarioN()

        Dim oXL As Interop.Excel.Application
        Dim oWB As Interop.Excel.Workbook
        Dim oWS As Interop.Excel.Worksheet
        Dim codigo As String = "123"
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim str As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim aux1 As String
        Dim aux2 As String
        Dim aux3 As String


        File.Delete(rutaD + "errores.txt")

        inicio = TimeOfDay

        oXL = New Interop.Excel.Application
        oWB = oXL.Workbooks.Open(Archivo)
        oWS = oXL.Worksheets(1)

        On Error GoTo SQLErrorSQ

        repetido = False

        y = 2
        While codigo <> ""
            codigo = oWS.Cells(y, 2).Value
            aux1 = oWS.Cells(y, 1).Value
            aux2 = oWS.Cells(y, 2).Value
            aux3 = oWS.Cells(y, 3).Value

            str = Get_ProdDesc(codigo)

            If myConnection.State = ConnectionState.Closed Then
                myConnection.Open()
            End If

            If codigo <> "" Then
                sqlInsertRow.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" & _
                "'" & oWS.Cells(y, 1).Value & "','" & oWS.Cells(y, 2).Value & "','" & str & "','" & oWS.Cells(y, 3).Value & "')"
                sqlInsertRow.ExecuteNonQuery()
            End If
            y = y + 1

            'Close the connection
            If myConnection.State = ConnectionState.Open Then
                myConnection.Close()
            End If
        End While

        GoTo FinSQ

SQLErrorSQ:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinSQ
        End If

FinSQ:

        oWB.Close()
        oXL.Quit()

        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub

    ' Importa a la tabla de Ubicaciones del archivo de Excell
    Public Sub convertToSQL_UbicacionesN()

        Dim oXL As Interop.Excel.Application
        Dim oWB As Interop.Excel.Workbook
        Dim oWS As Interop.Excel.Worksheet
        Dim codigo As String = "123"
        'Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        'Dim str As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        'Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        inicio = TimeOfDay

        oXL = New Interop.Excel.Application
        oWB = oXL.Workbooks.Open(Archivo)
        oWS = oXL.Worksheets(1)

        On Error GoTo SQLErrorSQ

        If myConnection.State = ConnectionState.Closed Then
            myConnection.Open()
        End If

        repetido = False

        y = 2
        While codigo <> ""
            codigo = oWS.Cells(y, 1).Value

            If codigo <> "" Then
                sqlInsertRow.CommandText = "INSERT INTO Ubicaciones(Ubicacion) VALUES (" & _
                "'" & oWS.Cells(y, 1).Value & "')"
                sqlInsertRow.ExecuteNonQuery()
            End If
            y = y + 1
        End While

        GoTo FinSQ

SQLErrorSQ:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinSQ
        End If

FinSQ:
        'Close the connection
        If myConnection.State = ConnectionState.Open Then
            myConnection.Close()
        End If

        oWB.Close()
        oXL.Quit()

        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub

    ' Importa a la tabla de Aduanas del archivo de Excell
    Public Sub convertToSQL_AduanasN()

        Dim oXL As Interop.Excel.Application
        Dim oWB As Interop.Excel.Workbook
        Dim oWS As Interop.Excel.Worksheet
        Dim codigo As String = "123"
        'Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        'Dim str As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        'Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        inicio = TimeOfDay

        oXL = New Interop.Excel.Application
        oWB = oXL.Workbooks.Open(Archivo)
        oWS = oXL.Worksheets(1)

        On Error GoTo SQLErrorSQ

        If myConnection.State = ConnectionState.Closed Then
            myConnection.Open()
        End If

        repetido = False

        y = 2
        While codigo <> ""
            codigo = oWS.Cells(y, 1).Value

            If codigo <> "" Then
                sqlInsertRow.CommandText = "INSERT INTO Aduanas(Aduana) VALUES (" & _
                "'" & oWS.Cells(y, 1).Value & "')"
                sqlInsertRow.ExecuteNonQuery()
            End If
            y = y + 1
        End While

        GoTo FinSQ

SQLErrorSQ:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinSQ
        End If

FinSQ:
        'Close the connection
        If myConnection.State = ConnectionState.Open Then
            myConnection.Close()
        End If

        oWB.Close()
        oXL.Quit()

        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub
    Public Sub convertToSQL_Logistik_stockAdd(ByVal csvLocation As String)

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)

        inicio = TimeOfDay

        'Read the output in a stream reader
        Dim r As New StreamReader(o)

        On Error GoTo SQLErrorRD

        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()

            If Not str Is Nothing Then
                indata = str.Split(";"c)

                If (indata.Length <> 2) Then

                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If

                    ' Actualiza Tabla de Logistik_stock
                    sqlInsertRow.CommandText = "update Logistik_stock set physical_quantity = physical_quantity + '" + indata(7) + _
                    "', usable_quantity = usable_quantity + '" + indata(8) + "' where id_stock = '" + indata(0) + "'" + _
                    " and id_warehouse= '" + indata(1) + "'" + _
                    " and id_product= '" + indata(2) + "'" + _
                    " and id_product_attribute= '" + indata(3) + "'"
                    'indata(0) id_stock
                    'indata(1) id_warehouse
                    'indata(2) id_product
                    'indata(3) id_product_attribute
                    'indata(4) reference
                    'indata(5) ean13
                    'indata(6) upc
                    'indata(7) physical_quantity
                    'indata(8) usable_quantity
                    'indata(9) price_te
                    sqlInsertRow.ExecuteNonQuery()

                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If

                    '' Actualiza Tabla de Inventarios de acuerdo al movimiento
                    '' 1 = Entradas, 2 = Salidas, 3 = Traspasos
                    'If indata(0) = "1" Then
                    '    ' Obtiene la cantidad inicial del codigo para la ubicacion destino
                    '    AuxI = Val(Get_CantInvent(indata(1), indata(4)))

                    '    If Val(AuxI) <> 0 Then
                    '        AuxI = AuxI + Val(indata(2))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Actualiza Tabla de Inventarios, adicionando la cantidad nueva a la actual. 
                    '        sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                    '        AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(4) + "'"
                    '        sqlInsertRowC.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    Else
                    '        ' Obtiene la Descripcion del codigo
                    '        aux = Get_ProdDesc(indata(1))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                    '        sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                    '        & "'" & indata(4) & "','" & indata(1) & "','" & aux & "','" & indata(2) & "')"
                    '        sqlInsertRowI.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    End If
                    'ElseIf indata(0) = "2" Then
                    '    ' Obtiene la cantidad inicial del codigo para la ubicacion Origen
                    '    AuxI = Val(Get_CantInvent(indata(1), indata(3)))

                    '    If Val(AuxI) <> 0 Then
                    '        AuxI = AuxI - Val(indata(2))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Actualiza Tabla de Inventarios, restando la cantidad a la actual. 
                    '        sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                    '        AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(3) + "'"
                    '        sqlInsertRowC.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    Else
                    '        ' Obtiene la Descripcion del codigo
                    '        aux = Get_ProdDesc(indata(1))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                    '        sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                    '        & "'" & indata(3) & "','" & indata(1) & "','" & aux & "','-" & indata(2) & "')"
                    '        sqlInsertRowI.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    End If
                    'Else
                    '    ' Realiza la transferencia de UbicacionO a UbicacionD
                    '    ' Movimiento, Codigo, Cantidad, UbicacionO, UbicacionD, Fecha
                    '    ' indata(0)  indata(1) indata(2) indata(3) indata(4)  indata(5)
                    '    ' Obtiene la cantidad inicial del codigo para la ubicacion Origen
                    '    AuxI = Val(Get_CantInvent(indata(1), indata(3)))

                    '    If Val(AuxI) <> 0 Then  ' Quiere decir que ya habia algo en la ubicacion origen
                    '        AuxI = AuxI - Val(indata(2))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Actualiza Tabla de Inventarios, restando la cantidad a la actual en Origen. 
                    '        sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                    '        AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(3) + "'"
                    '        sqlInsertRowC.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '        ' Obtiene la cantidad inicial del codigo para la ubicacion Destino
                    '        AuxI = Val(Get_CantInvent(indata(1), indata(4)))

                    '        If Val(AuxI) <> 0 Then
                    '            AuxI = AuxI + Val(indata(2))

                    '            If myConnection.State = ConnectionState.Closed Then
                    '                myConnection.Open()
                    '            End If

                    '            ' Actualiza Tabla de Inventarios, adicionando la cantidad nueva a la actual en Destino. 
                    '            sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                    '            AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(4) + "'"
                    '            sqlInsertRowC.ExecuteNonQuery()

                    '            'Close the connection
                    '            If myConnection.State = ConnectionState.Open Then
                    '                myConnection.Close()
                    '            End If

                    '        Else
                    '            ' Obtiene la Descripcion del codigo
                    '            aux = Get_ProdDesc(indata(1))

                    '            If myConnection.State = ConnectionState.Closed Then
                    '                myConnection.Open()
                    '            End If

                    '            ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                    '            sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                    '            & "'" & indata(4) & "','" & indata(1) & "','" & aux & "','" & indata(2) & "')"
                    '            sqlInsertRowI.ExecuteNonQuery()

                    '            'Close the connection
                    '            If myConnection.State = ConnectionState.Open Then
                    '                myConnection.Close()
                    '            End If

                    '        End If

                    '    Else ' Quiere decir que no habia nada en la ubiacion de origen. 
                    '        ' Obtiene la Descripcion del codigo
                    '        aux = Get_ProdDesc(indata(1))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                    '        sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                    '        & "'" & indata(3) & "','" & indata(1) & "','" & aux & "','-" & indata(2) & "')"
                    '        sqlInsertRowI.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    End If

                    'End If
                End If
            End If
        Loop Until str Is Nothing

        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If

FinRD:

        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub
    Public Sub convertToSQL_Logistik_supply_order_detail(ByVal csvLocation As String)

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)

        inicio = TimeOfDay

        'Read the output in a stream reader
        Dim r As New StreamReader(o)

        On Error GoTo SQLErrorRD

        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()

            If Not str Is Nothing Then
                indata = str.Split(";"c)

                If (indata.Length <> 2) Then

                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If

                    ' Actualiza Tabla de Logistik_stock
                    sqlInsertRow.CommandText = "update Logistik_supply_order_detail set quantity_received = '" + indata(7) + _
                    "' where id_supply_order_detail = '" + indata(0) + "'"
                    sqlInsertRow.ExecuteNonQuery()
                    'aumenta el stock por que es orden de cliente
                    sqlInsertRow.CommandText = "update Logistik_stock set physical_quantity = physical_quantity + '" + indata(7) + _
                    "', usable_quantity = '" + indata(7) + "' where id_warehouse= '" + indata(8) + "'" + _
                    " and id_product= '" + indata(3) + "'" + _
                    " and id_product_attribute= '" + indata(4) + "'"
                    sqlInsertRow.ExecuteNonQuery()
                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If
                End If
            End If
        Loop Until str Is Nothing

        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If

FinRD:

        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub
    Public Sub convertToSQL_Logistik_supply_order(ByVal csvLocation As String)

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)

        inicio = TimeOfDay

        'Read the output in a stream reader
        Dim r As New StreamReader(o)

        On Error GoTo SQLErrorRD

        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()

            If Not str Is Nothing Then
                indata = str.Split(";"c)

                If (indata.Length <> 2) Then

                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If

                    ' Actualiza Tabla de Logistik_stock

                    sqlInsertRow.CommandText = "update Logistik_supply_order set id_supply_order_state = '" + indata(4) + _
                    "' where id_supply_order = '" + indata(0) + "'"
                    '" and id_warehouse= '" + indata(1) + "'" + _
                    '" and id_product= '" + indata(2) + "'" + _
                    '" and id_product_attribute= '" + indata(3) + "'"
                    'indata(0) id_stock
                    'indata(1) id_warehouse
                    'indata(2) id_product
                    'indata(3) id_product_attribute
                    'indata(4) reference
                    'indata(5) ean13
                    'indata(6) upc
                    'indata(7) physical_quantity
                    'indata(8) usable_quantity
                    'indata(9) price_te
                    sqlInsertRow.ExecuteNonQuery()

                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If
                End If
            End If
        Loop Until str Is Nothing

        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If

FinRD:

        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub

    Public Sub convertToSQL_Logistik_order_detail(ByVal csvLocation As String)

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)

        inicio = TimeOfDay

        'Read the output in a stream reader
        Dim r As New StreamReader(o)

        On Error GoTo SQLErrorRD

        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()

            If Not str Is Nothing Then
                indata = str.Split(";"c)

                If (indata.Length <> 2) Then

                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If

                    ' Actualiza la catidad recibida
                    sqlInsertRow.CommandText = "update Logistik_order_detail set quantity_received = '" + indata(9) + _
                    "' where id_order_detail = '" + indata(0) + "'"
                    sqlInsertRow.ExecuteNonQuery()
                    'reduce el stock por que es orden de cliente
                    sqlInsertRow.CommandText = "update Logistik_stock set physical_quantity = physical_quantity - '" + indata(9) + _
                    "', usable_quantity = '" + indata(9) + "' where id_warehouse= '" + indata(3) + "'" + _
                    " and id_product= '" + indata(5) + "'" + _
                    " and id_product_attribute= '" + indata(6) + "'"
                    sqlInsertRow.ExecuteNonQuery()
                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If
                End If
            End If
        Loop Until str Is Nothing

        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If

FinRD:

        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub
    Public Sub convertToSQL_Logistik_orders(ByVal csvLocation As String)
        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")
        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)
        inicio = TimeOfDay
        'Read the output in a stream reader
        Dim r As New StreamReader(o)
        On Error GoTo SQLErrorRD
        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()
            If Not str Is Nothing Then
                indata = str.Split(";"c)
                If (indata.Length <> 2) Then
                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If
                    ' Actualiza Tabla de Logistik_stock
                    sqlInsertRow.CommandText = "update Logistik_orders set current_state = '" + indata(11) + _
                    "' where id_order = '" + indata(0) + "'"
                    sqlInsertRow.ExecuteNonQuery()
                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If
                End If
            End If
        Loop Until str Is Nothing
        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If
FinRD:
        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If
    End Sub
    Public Sub convertToSQL_Logistik_Inventario_TempAdd(ByVal csvLocation As String)

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)

        inicio = TimeOfDay

        'Read the output in a stream reader
        Dim r As New StreamReader(o)

        On Error GoTo SQLErrorRD

        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()

            If Not str Is Nothing Then
                indata = str.Split(";"c)

                If (indata.Length <> 2) Then

                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If

                    ' Actualiza Tabla de Logistik_stock

                    sqlInsertRow.CommandText = "update Logistik_stock set physical_quantity = physical_quantity + '" + indata(3) + _
                    "', usable_quantity = usable_quantity + '" + indata(3) + "'" + _
                    " where id_warehouse= '" + indata(15) + "'" + _
                    " and id_product= '" + indata(1) + "'" + _
                    " and id_product_attribute= '" + indata(2) + "'"
                    'indata(0) id_stock
                    'indata(1) id_warehouse
                    'indata(2) id_product
                    'indata(3) id_product_attribute
                    'indata(4) reference
                    'indata(5) ean13
                    'indata(6) upc
                    'indata(7) physical_quantity
                    'indata(8) usable_quantity
                    'indata(9) price_te
                    sqlInsertRow.ExecuteNonQuery()

                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If

                End If
            End If
        Loop Until str Is Nothing

        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If

FinRD:

        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub
    Public Sub convertToSQL_Logistik_Inventario_Temp(ByVal csvLocation As String)

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)

        inicio = TimeOfDay

        'Read the output in a stream reader
        Dim r As New StreamReader(o)

        On Error GoTo SQLErrorRD

        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()

            If Not str Is Nothing Then
                indata = str.Split(";"c)

                If (indata.Length <> 2) Then

                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If

                    ' Actualiza Tabla de Logistik_stock

                    sqlInsertRow.CommandText = "update Logistik_stock set physical_quantity = '" + indata(3) + _
                    "', usable_quantity = '" + indata(3) + "'" + _
                    " where id_warehouse= '" + indata(15) + "'" + _
                    " and id_product= '" + indata(1) + "'" + _
                    " and id_product_attribute= '" + indata(2) + "'"
                    'indata(0) id_stock
                    'indata(1) id_warehouse
                    'indata(2) id_product
                    'indata(3) id_product_attribute
                    'indata(4) reference
                    'indata(5) ean13
                    'indata(6) upc
                    'indata(7) physical_quantity
                    'indata(8) usable_quantity
                    'indata(9) price_te
                    sqlInsertRow.ExecuteNonQuery()

                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If

                End If
            End If
        Loop Until str Is Nothing

        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If

FinRD:

        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub
    Public Sub convertToSQL_Logistik_stock(ByVal csvLocation As String)

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)

        inicio = TimeOfDay

        'Read the output in a stream reader
        Dim r As New StreamReader(o)

        On Error GoTo SQLErrorRD

        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()

            If Not str Is Nothing Then
                indata = str.Split(";"c)

                If (indata.Length <> 2) Then

                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If

                    ' Actualiza Tabla de Logistik_stock

                    sqlInsertRow.CommandText = "update Logistik_stock set physical_quantity = '" + indata(7) + _
                    "', usable_quantity = '" + indata(8) + "' where id_stock = '" + indata(0) + "'" + _
                    " and id_warehouse= '" + indata(1) + "'" + _
                    " and id_product= '" + indata(2) + "'" + _
                    " and id_product_attribute= '" + indata(3) + "'"
                    'indata(0) id_stock
                    'indata(1) id_warehouse
                    'indata(2) id_product
                    'indata(3) id_product_attribute
                    'indata(4) reference
                    'indata(5) ean13
                    'indata(6) upc
                    'indata(7) physical_quantity
                    'indata(8) usable_quantity
                    'indata(9) price_te
                    sqlInsertRow.ExecuteNonQuery()

                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If

                    '' Actualiza Tabla de Inventarios de acuerdo al movimiento
                    '' 1 = Entradas, 2 = Salidas, 3 = Traspasos
                    'If indata(0) = "1" Then
                    '    ' Obtiene la cantidad inicial del codigo para la ubicacion destino
                    '    AuxI = Val(Get_CantInvent(indata(1), indata(4)))

                    '    If Val(AuxI) <> 0 Then
                    '        AuxI = AuxI + Val(indata(2))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Actualiza Tabla de Inventarios, adicionando la cantidad nueva a la actual. 
                    '        sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                    '        AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(4) + "'"
                    '        sqlInsertRowC.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    Else
                    '        ' Obtiene la Descripcion del codigo
                    '        aux = Get_ProdDesc(indata(1))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                    '        sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                    '        & "'" & indata(4) & "','" & indata(1) & "','" & aux & "','" & indata(2) & "')"
                    '        sqlInsertRowI.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    End If
                    'ElseIf indata(0) = "2" Then
                    '    ' Obtiene la cantidad inicial del codigo para la ubicacion Origen
                    '    AuxI = Val(Get_CantInvent(indata(1), indata(3)))

                    '    If Val(AuxI) <> 0 Then
                    '        AuxI = AuxI - Val(indata(2))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Actualiza Tabla de Inventarios, restando la cantidad a la actual. 
                    '        sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                    '        AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(3) + "'"
                    '        sqlInsertRowC.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    Else
                    '        ' Obtiene la Descripcion del codigo
                    '        aux = Get_ProdDesc(indata(1))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                    '        sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                    '        & "'" & indata(3) & "','" & indata(1) & "','" & aux & "','-" & indata(2) & "')"
                    '        sqlInsertRowI.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    End If
                    'Else
                    '    ' Realiza la transferencia de UbicacionO a UbicacionD
                    '    ' Movimiento, Codigo, Cantidad, UbicacionO, UbicacionD, Fecha
                    '    ' indata(0)  indata(1) indata(2) indata(3) indata(4)  indata(5)
                    '    ' Obtiene la cantidad inicial del codigo para la ubicacion Origen
                    '    AuxI = Val(Get_CantInvent(indata(1), indata(3)))

                    '    If Val(AuxI) <> 0 Then  ' Quiere decir que ya habia algo en la ubicacion origen
                    '        AuxI = AuxI - Val(indata(2))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Actualiza Tabla de Inventarios, restando la cantidad a la actual en Origen. 
                    '        sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                    '        AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(3) + "'"
                    '        sqlInsertRowC.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '        ' Obtiene la cantidad inicial del codigo para la ubicacion Destino
                    '        AuxI = Val(Get_CantInvent(indata(1), indata(4)))

                    '        If Val(AuxI) <> 0 Then
                    '            AuxI = AuxI + Val(indata(2))

                    '            If myConnection.State = ConnectionState.Closed Then
                    '                myConnection.Open()
                    '            End If

                    '            ' Actualiza Tabla de Inventarios, adicionando la cantidad nueva a la actual en Destino. 
                    '            sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                    '            AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(4) + "'"
                    '            sqlInsertRowC.ExecuteNonQuery()

                    '            'Close the connection
                    '            If myConnection.State = ConnectionState.Open Then
                    '                myConnection.Close()
                    '            End If

                    '        Else
                    '            ' Obtiene la Descripcion del codigo
                    '            aux = Get_ProdDesc(indata(1))

                    '            If myConnection.State = ConnectionState.Closed Then
                    '                myConnection.Open()
                    '            End If

                    '            ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                    '            sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                    '            & "'" & indata(4) & "','" & indata(1) & "','" & aux & "','" & indata(2) & "')"
                    '            sqlInsertRowI.ExecuteNonQuery()

                    '            'Close the connection
                    '            If myConnection.State = ConnectionState.Open Then
                    '                myConnection.Close()
                    '            End If

                    '        End If

                    '    Else ' Quiere decir que no habia nada en la ubiacion de origen. 
                    '        ' Obtiene la Descripcion del codigo
                    '        aux = Get_ProdDesc(indata(1))

                    '        If myConnection.State = ConnectionState.Closed Then
                    '            myConnection.Open()
                    '        End If

                    '        ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                    '        sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                    '        & "'" & indata(3) & "','" & indata(1) & "','" & aux & "','-" & indata(2) & "')"
                    '        sqlInsertRowI.ExecuteNonQuery()

                    '        'Close the connection
                    '        If myConnection.State = ConnectionState.Open Then
                    '            myConnection.Close()
                    '        End If

                    '    End If

                    'End If
                End If
            End If
        Loop Until str Is Nothing

        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If

FinRD:

        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub
    ' Lee archivo de texto e importa a la tabla de Productos
    Public Sub convertToSQL_Movimientos(ByVal csvLocation As String)

        Dim x As Integer = 0
        Dim inicio As Date
        Dim fin As Date
        Dim success As Boolean = True
        Dim repetido As Boolean = False
        Dim BanderaIni As Boolean = False
        Dim str As String = Nothing
        Dim AuxI As Integer
        Dim aux As String
        Dim indata() As String = Nothing
        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowC As SqlCommand = myConnection.CreateCommand()
        Dim sqlInsertRowI As SqlCommand = myConnection.CreateCommand()

        File.Delete(rutaD + "errores.txt")

        Dim o As New FileStream(csvLocation, FileMode.Open, FileAccess.Read)

        inicio = TimeOfDay

        'Read the output in a stream reader
        Dim r As New StreamReader(o)

        On Error GoTo SQLErrorRD

        o.Position = 0      ' Regresa el apuntador al inicio del archivo para importacion
        Do
            repetido = False
            str = r.ReadLine()

            If Not str Is Nothing Then
                indata = str.Split(";"c)

                If (indata.Length <> 2) Then

                    If myConnection.State = ConnectionState.Closed Then
                        myConnection.Open()
                    End If

                    ' Actualiza Tabla de Movimientos
                    sqlInsertRow.CommandText = "INSERT INTO Movimientos(Movimiento, Codigo, Cantidad, UbicacionO, UbicacionD, Fecha) VALUES (" _
                    & "'" & indata(0) & "','" & indata(1) & "','" & indata(2) & "'," _
                    & "'" & indata(3) & "','" & indata(4) & "','" & indata(5) & "')"
                    sqlInsertRow.ExecuteNonQuery()

                    'Close the connection
                    If myConnection.State = ConnectionState.Open Then
                        myConnection.Close()
                    End If

                    ' Actualiza Tabla de Inventarios de acuerdo al movimiento
                    ' 1 = Entradas, 2 = Salidas, 3 = Traspasos
                    If indata(0) = "1" Then
                        ' Obtiene la cantidad inicial del codigo para la ubicacion destino
                        AuxI = Val(Get_CantInvent(indata(1), indata(4)))

                        If Val(AuxI) <> 0 Then
                            AuxI = AuxI + Val(indata(2))

                            If myConnection.State = ConnectionState.Closed Then
                                myConnection.Open()
                            End If

                            ' Actualiza Tabla de Inventarios, adicionando la cantidad nueva a la actual. 
                            sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                            AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(4) + "'"
                            sqlInsertRowC.ExecuteNonQuery()

                            'Close the connection
                            If myConnection.State = ConnectionState.Open Then
                                myConnection.Close()
                            End If

                        Else
                            ' Obtiene la Descripcion del codigo
                            aux = Get_ProdDesc(indata(1))

                            If myConnection.State = ConnectionState.Closed Then
                                myConnection.Open()
                            End If

                            ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                            sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                            & "'" & indata(4) & "','" & indata(1) & "','" & aux & "','" & indata(2) & "')"
                            sqlInsertRowI.ExecuteNonQuery()

                            'Close the connection
                            If myConnection.State = ConnectionState.Open Then
                                myConnection.Close()
                            End If

                        End If
                    ElseIf indata(0) = "2" Then
                        ' Obtiene la cantidad inicial del codigo para la ubicacion Origen
                        AuxI = Val(Get_CantInvent(indata(1), indata(3)))

                        If Val(AuxI) <> 0 Then
                            AuxI = AuxI - Val(indata(2))

                            If myConnection.State = ConnectionState.Closed Then
                                myConnection.Open()
                            End If

                            ' Actualiza Tabla de Inventarios, restando la cantidad a la actual. 
                            sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                            AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(3) + "'"
                            sqlInsertRowC.ExecuteNonQuery()

                            'Close the connection
                            If myConnection.State = ConnectionState.Open Then
                                myConnection.Close()
                            End If

                        Else
                            ' Obtiene la Descripcion del codigo
                            aux = Get_ProdDesc(indata(1))

                            If myConnection.State = ConnectionState.Closed Then
                                myConnection.Open()
                            End If

                            ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                            sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                            & "'" & indata(3) & "','" & indata(1) & "','" & aux & "','-" & indata(2) & "')"
                            sqlInsertRowI.ExecuteNonQuery()

                            'Close the connection
                            If myConnection.State = ConnectionState.Open Then
                                myConnection.Close()
                            End If

                        End If
                    Else
                        ' Realiza la transferencia de UbicacionO a UbicacionD
                        ' Movimiento, Codigo, Cantidad, UbicacionO, UbicacionD, Fecha
                        ' indata(0)  indata(1) indata(2) indata(3) indata(4)  indata(5)
                        ' Obtiene la cantidad inicial del codigo para la ubicacion Origen
                        AuxI = Val(Get_CantInvent(indata(1), indata(3)))

                        If Val(AuxI) <> 0 Then  ' Quiere decir que ya habia algo en la ubicacion origen
                            AuxI = AuxI - Val(indata(2))

                            If myConnection.State = ConnectionState.Closed Then
                                myConnection.Open()
                            End If

                            ' Actualiza Tabla de Inventarios, restando la cantidad a la actual en Origen. 
                            sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                            AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(3) + "'"
                            sqlInsertRowC.ExecuteNonQuery()

                            'Close the connection
                            If myConnection.State = ConnectionState.Open Then
                                myConnection.Close()
                            End If

                            ' Obtiene la cantidad inicial del codigo para la ubicacion Destino
                            AuxI = Val(Get_CantInvent(indata(1), indata(4)))

                            If Val(AuxI) <> 0 Then
                                AuxI = AuxI + Val(indata(2))

                                If myConnection.State = ConnectionState.Closed Then
                                    myConnection.Open()
                                End If

                                ' Actualiza Tabla de Inventarios, adicionando la cantidad nueva a la actual en Destino. 
                                sqlInsertRowC.CommandText = "UPDATE Inventario SET Cantidad = '" & _
                                AuxI.ToString & "' WHERE Codigo = '" + indata(1) + "' AND Ubicacion = '" + indata(4) + "'"
                                sqlInsertRowC.ExecuteNonQuery()

                                'Close the connection
                                If myConnection.State = ConnectionState.Open Then
                                    myConnection.Close()
                                End If

                            Else
                                ' Obtiene la Descripcion del codigo
                                aux = Get_ProdDesc(indata(1))

                                If myConnection.State = ConnectionState.Closed Then
                                    myConnection.Open()
                                End If

                                ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                                sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                                & "'" & indata(4) & "','" & indata(1) & "','" & aux & "','" & indata(2) & "')"
                                sqlInsertRowI.ExecuteNonQuery()

                                'Close the connection
                                If myConnection.State = ConnectionState.Open Then
                                    myConnection.Close()
                                End If

                            End If

                        Else ' Quiere decir que no habia nada en la ubiacion de origen. 
                            ' Obtiene la Descripcion del codigo
                            aux = Get_ProdDesc(indata(1))

                            If myConnection.State = ConnectionState.Closed Then
                                myConnection.Open()
                            End If

                            ' Si no existe la combinacion de Codigo - UbicacionD entonces agrega a inventarios
                            sqlInsertRowI.CommandText = "INSERT INTO Inventario(Ubicacion, Codigo, Descripcion, Cantidad) VALUES (" _
                            & "'" & indata(3) & "','" & indata(1) & "','" & aux & "','-" & indata(2) & "')"
                            sqlInsertRowI.ExecuteNonQuery()

                            'Close the connection
                            If myConnection.State = ConnectionState.Open Then
                                myConnection.Close()
                            End If

                        End If

                    End If
                End If
            End If
        Loop Until str Is Nothing

        GoTo FinRD

SQLErrorRD:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 9 Then
            MsgBox("Error en el formato del Archivo!!!, revise el archivo origen e intente nuevamente. Importacion Cancelada...") ', err.ToString)
            success = False
            BanderaErrorCom = True
            GoTo FinRD
        End If

FinRD:

        r.Close()
        o.Close()
        If success Then
            fin = TimeOfDay
            BanderaErrorCom = False
            MsgBox("Importacion de datos Finalizada " + "Inicio:" + inicio + " Fin:" + fin)
            If File.Exists(rutaD + "errores.txt") Then
                MsgBox("Revise archivo de errores, hubieron errores.")
            End If
        End If

    End Sub

    Public Function DosDecimales(ByVal DatoNString As String) As String

        Dim i As Integer
        Dim aux As String = ""
        Dim caracter As Char

        For i = 0 To DatoNString.Length
            caracter = DatoNString(i)
            If caracter <> "." Then
                aux = aux + caracter
            Else
                aux = aux + caracter + DatoNString(i + 1) + DatoNString(i + 2)
                Exit For
            End If
        Next

        Return aux
    End Function

    Public Sub Update_Articulo(ByVal Codigo As String, ByVal nombre As String, ByVal cost As String, ByVal Minimo As String, ByVal Reorden As String, ByVal Maximo As String, ByVal serie As String)

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim sqlUpdateRow As SqlCommand = myConnection.CreateCommand()
        On Error GoTo SQLError9

        sqlUpdateRow.CommandText = "UPDATE Productos SET Descripcion ='" + nombre + "', Costo = '" + cost + "', " + _
        " Minimo ='" + Minimo + "', Reorden ='" + Reorden + "', Maximo ='" + Maximo + "', BanderaSerie ='" & serie & "' WHERE Codigo = '" & Codigo & "'"
        sqlUpdateRow.ExecuteNonQuery()

        GoTo Fin9

SQLError9:
        MsgBox(Err.Number)
        MsgBox(Err.Description)
        If Err.Number = 5 Then
            MsgBox("Codigo ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

Fin9:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If
    End Sub

    Public Sub Reporte_Productos(ByVal bandera As String)
        Dim oXL As Interop.Excel.Application
        Dim oWB As Interop.Excel.Workbook
        Dim oWS As Interop.Excel.Worksheet

        Dim saux As String
        Dim x As Integer = 0
        Dim y As Integer = 0

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_GrabaRI

        Dim cmd As SqlCommand = myConnection.CreateCommand
        If bandera = "0" Then
            oXL = New Interop.Excel.Application
            oWB = oXL.Workbooks.Open(ruta + "ReporteProd.xls")
            oWS = oWB.Worksheets("Reporte")

            oWS.Range("D3").Value = Now.ToString("MM/dd/yyyy")

            cmd.CommandText = "SELECT * FROM Productos ORDER BY Codigo"
        End If
        If bandera = "1" Then
            oXL = New Interop.Excel.Application
            oWB = oXL.Workbooks.Open(ruta + "ReporteExist.xls")
            oWS = oWB.Worksheets("Reporte")

            oWS.Range("D3").Value = Now.ToString("MM/dd/yyyy")

            'cmd.CommandText = "SELECT Codigo, Descripcion, SUM(Cantidad) as Contador FROM Inventario GROUP BY Codigo,Descripcion ORDER BY Codigo"
            cmd.CommandText = "SELECT * FROM Inventario WHERE Status ='1' ORDER BY Codigo"
        End If
        'If bandera = "2" Then
        '    cmd.CommandText = "SELECT * FROM Activos WHERE Flag ='2' ORDER BY Codigo"
        'End If

        ' Execute Query
        Dim thisReaderP As SqlDataReader = cmd.ExecuteReader()

        x = 6

        While thisReaderP.Read

            If bandera = "0" Then
                ActivoP.Codigo = thisReaderP.Item("Codigo")
                ActivoP.Nombre = thisReaderP.Item("Descripcion")
                If IsDBNull(thisReaderP.Item("Costo")) Then
                    ActivoP.Costo = ""
                Else
                    ActivoP.Costo = thisReaderP.Item("Costo")
                End If
                ActivoP.Minimo = thisReaderP.Item("Minimo")
                ActivoP.Reorden = thisReaderP.Item("Reorden")
                ActivoP.Maximo = thisReaderP.Item("Maximo")

                oWS.Cells(x, 1).Value = ActivoP.Codigo.Trim
                oWS.Cells(x, 2).Value = ActivoP.Nombre.Trim
                oWS.Cells(x, 3).Value = ActivoP.Costo.Trim
                oWS.Cells(x, 4).Value = ActivoP.Minimo.Trim
                oWS.Cells(x, 5).Value = ActivoP.Reorden.Trim
                oWS.Cells(x, 6).Value = ActivoP.Maximo.Trim
            End If

            If bandera = "1" Then
                ActivoP.Ubicacion = thisReaderP.Item("Ubicacion")
                ActivoP.Codigo = thisReaderP.Item("Codigo")
                ActivoP.Nombre = thisReaderP.Item("Descripcion")
                ActivoP.Cantidad = thisReaderP.Item("Serie")
                ActivoP.Costo = thisReaderP.Item("Cantidad")

                'Dim cmd2 As SqlCommand = myConnection.CreateCommand
                'cmd2.CommandText = "SELECT * FROM Productos WHERE Codigo='" + ActivoP.Codigo + "'"

                '' Execute Query
                'Dim thisReaderN As SqlDataReader = cmd2.ExecuteReader()

                'While thisReaderN.Read
                '    If IsDBNull(thisReaderN.Item("Cantidad")) Then
                '        ActivoP.Costo = ""
                '    Else
                '        ActivoP.Costo = thisReaderN.Item("Cantidad")
                '    End If
                'End While

                'If Val(ActivoP.Cantidad) <> 0 Then
                oWS.Cells(x, 1).Value = ActivoP.Ubicacion.Trim
                oWS.Cells(x, 2).Value = ActivoP.Codigo.Trim
                oWS.Cells(x, 3).Value = ActivoP.Nombre.Trim
                oWS.Cells(x, 4).Value = ActivoP.Cantidad.Trim
                oWS.Cells(x, 5).Value = ActivoP.Costo.Trim
                'End If
            End If

            x = x + 1

        End While

        GoTo FinRI

Error_GrabaRI:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinRI:

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        If bandera = "0" Then
            oWS.SaveAs(rutaD + "ReporteProd_" + Now.ToString("yyyyMMdd") + ".xls")
        End If
        If bandera = "1" Then
            oWS.SaveAs(rutaD + "ReporteExist_" + Now.ToString("yyyyMMdd") + ".xls")
        End If
        If bandera = "2" Then
            oWS.SaveAs(rutaD + "ReporteACTM_" + Now.ToString("yyyyMMdd") + ".xls")
        End If

        oXL.Quit()

    End Sub

    Public Sub Reporte_Recomienda(ByVal dato As String)
        Dim oXL As Interop.Excel.Application
        Dim oWB As Interop.Excel.Workbook
        Dim oWS As Interop.Excel.Worksheet
        Dim saux As String          ' Guarda la clasificaicon anterior
        Dim aPedir As Integer
        Dim x As Integer = 0

        oXL = New Interop.Excel.Application
        If TipoReporte = "1" Then
            oWB = oXL.Workbooks.Open(ruta + "ReporteREC.xls")
        End If

        oWS = oWB.Worksheets("Reporte")

        oWS.Range("D3").Value = Now.ToString("MM/dd/yyyy")

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_GrabaRR
        'SELECT     Inventario.Codigo, Inventario.Descripcion, SUM(Inventario.Cantidad) AS Suma, Productos.Reorden, Productos.Maximo
        'FROM         Inventario INNER JOIN
        'Productos ON Inventario.Codigo = Productos.Codigo
        'GROUP BY Inventario.Codigo, Inventario.Descripcion, Productos.Reorden, Productos.Maximo

        Dim cmd As SqlCommand = myConnection.CreateCommand
        If TipoReporte = "1" Then
            cmd.CommandText = "SELECT Inventario.Codigo, Inventario.Descripcion, SUM(Inventario.Cantidad) AS Suma, Productos.Reorden, Productos.Maximo " & _
                              "FROM Inventario INNER JOIN Productos ON Inventario.Codigo = Productos.Codigo " & _
                              "GROUP BY Inventario.Codigo, Inventario.Descripcion, Productos.Reorden, Productos.Maximo"

            'cmd.CommandText = "SELECT Inventario.Codigo, Inventario.Descripcion, COUNT(Inventario.Codigo) AS Suma, Productos.Reorden, Productos.Maximo " & _
            '                  "FROM Inventario INNER JOIN Productos ON Inventario.Codigo = Productos.Codigo " & _
            '                  "GROUP BY Inventario.Codigo, Inventario.Descripcion, Productos.Reorden, Productos.Maximo"

            'cmd.CommandText = "SELECT Codigo, Descripcion, SUM(Cantidad) as Contador FROM Inventario GROUP BY Codigo,Descripcion ORDER BY Codigo"

        End If

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        x = 6

        While thisReader.Read
            If TipoReporte = "1" Then
                ActivoP.Codigo = thisReader.Item("Codigo")
                ActivoP.Nombre = thisReader.Item("Descripcion")
                ActivoP.Cantidad = thisReader.Item("Suma")
                ActivoP.Reorden = thisReader.Item("Reorden")
                ActivoP.Maximo = thisReader.Item("Maximo")

                If Val(ActivoP.Cantidad) <= Val(ActivoP.Reorden.Trim) Then
                    aPedir = ActivoP.Maximo - ActivoP.Cantidad

                    oWS.Cells(x, 1).Value = ActivoP.Codigo.Trim
                    oWS.Cells(x, 2).Value = ActivoP.Nombre.Trim
                    oWS.Cells(x, 3).Value = aPedir
                    oWS.Cells(x, 4).Value = ActivoP.Reorden
                    oWS.Cells(x, 5).Value = ActivoP.Maximo
                    x = x + 1
                End If
            End If

        End While

        GoTo FinRR

Error_GrabaRR:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinRR:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        If TipoReporte = "1" Then
            oWS.SaveAs(rutaD + "ReporteREC_" + Now.ToString("yyyyMMdd") + ".xls")
        End If

        oXL.Quit()

    End Sub

    Public Sub Reporte_Generico(ByVal dato As String, ByVal Fecha1 As DateTime, ByVal Fecha2 As DateTime)
        Dim oXL As Interop.Excel.Application
        Dim oWB As Interop.Excel.Workbook
        Dim oWS As Interop.Excel.Worksheet
        Dim saux As String          ' Guarda la clasificaicon anterior
        Dim aPedir As Integer
        Dim x As Integer = 0

        oXL = New Interop.Excel.Application
        If TipoReporte = "1" Then
            oWB = oXL.Workbooks.Open(ruta + "ReporteENT.xls")
        End If
        If TipoReporte = "2" Then
            oWB = oXL.Workbooks.Open(ruta + "ReporteSAL.xls")
        End If
        If TipoReporte = "3" Then
            oWB = oXL.Workbooks.Open(ruta + "ReporteSalida.xlsx")
        End If

        oWS = oWB.Worksheets("Reporte")

        If TipoReporte = "1" Or TipoReporte = "2" Then
            oWS.Range("C3").Value = dato
            oWS.Range("C4").Value = Fecha1
            oWS.Range("C5").Value = Fecha2
        Else
            oWS.Range("F1").Value = dato
            oWS.Range("F3").Value = Now.ToString("dd/MM/yyyy")
        End If

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_GrabaRR

        'SELECT     ID, CodigoEmpl, Movimiento, Documento, Codigo, UbicacionO, UbicacionD, Fecha, Aduana, Serie, Pedimento
        'FROM(Movimientos)
        'WHERE     (Fecha BETWEEN '21/07/2013' AND '21/07/2013') AND (UbicacionD = 'Ubicaicon 1') AND (Movimiento = '11')
        'ORDER BY Serie

        Dim cmd As SqlCommand = myConnection.CreateCommand
        If TipoReporte = "1" Then
            cmd.CommandText = "SELECT * FROM Movimientos WHERE (Fecha BETWEEN '" + Fecha1 + "'AND '" + Fecha2 + "') AND Ubicacion = '" + dato + "' AND Movimiento='11' ORDER BY Serie"
        End If
        If TipoReporte = "2" Then
            cmd.CommandText = "SELECT * FROM Movimientos WHERE (Fecha BETWEEN '" + Fecha1 + "'AND '" + Fecha2 + "') AND Ubicacion = '" + dato + "' AND Movimiento='23' ORDER BY Serie"
        End If
        If TipoReporte = "3" Then
            cmd.CommandText = "SELECT * FROM Movimientos WHERE Documento = '" + dato + "' AND Movimiento='23' ORDER BY Serie"
        End If

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        If TipoReporte = "1" Or TipoReporte = "2" Then
            x = 8
        Else
            x = 19
        End If

        While thisReader.Read

            ActivoP.Documento = thisReader.Item("Documento")
            ActivoP.Codigo = thisReader.Item("Codigo")
            saux = Get_ProdDesc(ActivoP.Codigo.Trim)
            ActivoP.FechaP = thisReader.Item("FPedimento")
            ActivoP.Fecha = thisReader.Item("Fecha")
            ActivoP.Aduana = thisReader.Item("Aduana")
            If IsDBNull(thisReader.Item("Serie")) Then
                ActivoP.Serie = ""
            Else
                ActivoP.Serie = thisReader.Item("Serie")
            End If

            ActivoP.Pedimento = thisReader.Item("Pedimento")
            If IsDBNull(thisReader.Item("Cantidad")) Then
                ActivoP.Cantidad = ""
            Else
                ActivoP.Cantidad = thisReader.Item("Cantidad")
            End If


            If TipoReporte = "1" Or TipoReporte = "2" Then
                oWS.Cells(x, 1).Value = ActivoP.Documento.Trim
                oWS.Cells(x, 2).Value = ActivoP.Codigo.Trim
                oWS.Cells(x, 3).Value = saux
                oWS.Cells(x, 4).Value = ActivoP.Fecha.Trim
                oWS.Cells(x, 5).Value = ActivoP.Aduana.Trim
                oWS.Cells(x, 6).Value = ActivoP.Pedimento.Trim
                oWS.Cells(x, 7).Value = ActivoP.Serie.Trim
                oWS.Cells(x, 8).Value = ActivoP.Cantidad.Trim
            Else
                If ActivoP.Cantidad = "" Then
                    oWS.Cells(x, 1).Value = "1"                 ' Cantidad siempre 1 cuando es serie
                Else
                    oWS.Cells(x, 1).Value = ActivoP.Cantidad
                End If
                oWS.Cells(x, 2).Value = ActivoP.Codigo.Trim ' Codigo
                oWS.Cells(x, 4).Value = saux                ' Descripcion
                oWS.Cells(x, 5).Value = ActivoP.Serie.Trim  ' Serie
                oWS.Cells(x, 6).Value = ActivoP.Pedimento.Trim  ' Pedimento
                oWS.Cells(x, 7).Value = ActivoP.Aduana.Trim ' Aduana
                oWS.Cells(x, 8).Value = ActivoP.FechaP.Trim ' Fecha Pedimento
            End If

            x = x + 1
        End While

        GoTo FinRR

Error_GrabaRR:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinRR:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        If TipoReporte = "1" Then
            oWS.SaveAs(rutaD + "ReporteENT_" + Now.ToString("yyyyMMdd") + ".xls")
        End If
        If TipoReporte = "2" Then
            oWS.SaveAs(rutaD + "ReporteSAL_" + Now.ToString("yyyyMMdd") + ".xls")
        End If
        If TipoReporte = "3" Then
            oWS.SaveAs(rutaD + "ReporteSalida_" + dato + "_" + Now.ToString("yyyyMMdd") + ".xlsx")
        End If
        'If TipoReporte = "4" Then
        '    oWS.SaveAs(rutaD + "ReporteCCosto_" + Now.ToString("yyyyMMdd") + ".xls")
        'End If
        'If TipoReporte = "5" Then
        '    oWS.SaveAs(rutaD + "ReporteActNoU_" + Now.ToString("yyyyMMdd") + ".xls")
        'End If
        'If TipoReporte = "6" Then
        '    oWS.SaveAs(rutaD + "ReporteActNoR_" + Now.ToString("yyyyMMdd") + ".xls")
        'End If

        oXL.Quit()

    End Sub

    ' Verifica Cliente
    Public Function VerifyEstado(ByVal Codigo As String) As Empleado

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        'cmd.CommandText = "SELECT Estado FROM Estado WHERE Codigo = '" + Codigo + "'"
        cmd.CommandText = "SELECT * FROM Estado WHERE Estado = '" + Codigo + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        EmpleadoP.Nombre = ""

        While thisReader.Read()
            EmpleadoP.Nombre = thisReader.Item("Estado").ToString.Trim(" ")
            'Return aux
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return EmpleadoP
        'Return Nothing

    End Function

    ' Verifica Cliente
    Public Function VerifyInvent_Articulo(ByVal Codigo As String) As Empleado

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        'cmd.CommandText = "SELECT Estado FROM Estado WHERE Codigo = '" + Codigo + "'"
        cmd.CommandText = "SELECT * FROM Inventario WHERE Codigo = '" + Codigo + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        EmpleadoP.Nombre = ""

        While thisReader.Read()
            EmpleadoP.Nombre = thisReader.Item("Cantidad").ToString.Trim(" ")
            'Return aux
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return EmpleadoP
        'Return Nothing

    End Function

    ' Verifica Empleado
    Public Function VerifyEmpleado(ByVal Codigo As String) As Empleado

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "SELECT Nombre, Clave, Rol FROM Empleados WHERE Codigo = '" + Codigo + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        EmpleadoP.Nombre = ""

        While thisReader.Read()
            EmpleadoP.Nombre = thisReader.Item("Nombre")
            EmpleadoP.Clave = thisReader.Item("Clave")
            EmpleadoP.Rol = thisReader.Item("Rol")
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return EmpleadoP
        'Return Nothing

    End Function

    ' Verifica Cliente
    Public Function VerifyUbica(ByVal Codigo As String) As Empleado

        Dim aux As String

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        'cmd.CommandText = "SELECT Clave, Ubicacion FROM Ubicaciones WHERE Clave = '" + Codigo + "'"
        cmd.CommandText = "SELECT * FROM Ubicaciones WHERE Ubicacion = '" + Codigo + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        EmpleadoP.Nombre = ""

        While thisReader.Read()
            EmpleadoP.Nombre = thisReader.Item("Ubicacion")
            'EmpleadoP.Clave = thisReader.Item("Clave")
            'EmpleadoP.Rol = thisReader.Item("Rol")
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return EmpleadoP
        'Return Nothing

    End Function

    ' Verifica Cliente
    Public Function VerifyAduana(ByVal Codigo As String) As Empleado

        Dim aux As String

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        'cmd.CommandText = "SELECT Clave, Ubicacion FROM Ubicaciones WHERE Clave = '" + Codigo + "'"
        cmd.CommandText = "SELECT * FROM Aduanas WHERE Aduana = '" + Codigo + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        EmpleadoP.Nombre = ""

        While thisReader.Read()
            EmpleadoP.Nombre = thisReader.Item("Aduana")
            'EmpleadoP.Clave = thisReader.Item("Clave")
            'EmpleadoP.Rol = thisReader.Item("Rol")
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return EmpleadoP
        'Return Nothing

    End Function

    ' Verifica Cliente
    Public Function VerifyUbica_Articulo(ByVal Codigo As String) As Empleado

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        'cmd.CommandText = "SELECT Clave, Ubicacion FROM Ubicaciones WHERE Clave = '" + Codigo + "'"
        cmd.CommandText = "SELECT * FROM Inventario WHERE Ubicacion = '" + Codigo + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        EmpleadoP.Nombre = ""

        While thisReader.Read()
            EmpleadoP.Nombre = thisReader.Item("Ubicacion")
            'EmpleadoP.Clave = thisReader.Item("Clave")
            'EmpleadoP.Rol = thisReader.Item("Rol")
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return EmpleadoP
        'Return Nothing

    End Function

    ' Verifica Cliente
    Public Function VerifyAduana_Articulo(ByVal Codigo As String) As Empleado

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        'cmd.CommandText = "SELECT Clave, Ubicacion FROM Ubicaciones WHERE Clave = '" + Codigo + "'"
        cmd.CommandText = "SELECT * FROM Inventario WHERE Aduana = '" + Codigo + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        EmpleadoP.Nombre = ""

        While thisReader.Read()
            EmpleadoP.Nombre = thisReader.Item("Aduana")
            'EmpleadoP.Clave = thisReader.Item("Clave")
            'EmpleadoP.Rol = thisReader.Item("Rol")
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return EmpleadoP
        'Return Nothing

    End Function

    ' Verifica Articulo
    Public Function VerifyArticulo(ByVal Codigo As String) As Empleado

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "SELECT * FROM Productos WHERE Codigo = '" + Codigo + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        EmpleadoP.Nombre = ""

        While thisReader.Read()
            EmpleadoP.Nombre = thisReader.Item("Descripcion")
            'EmpleadoP.Clave = thisReader.Item("Codigo")
            'EmpleadoP.Rol = thisReader.Item("Rol")
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return EmpleadoP
        'Return Nothing

    End Function

    ' Obtiene el Index de Categoria 
    Public Function Get_CantInvent(ByVal codigo As String, ByVal Ubica As String) As String

        Dim aux As String

        aux = codigo.ToString.Trim(" ")
        codigo = aux

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "SELECT * FROM Inventario WHERE Codigo = '" + codigo.ToString.Trim(" ") + "' AND Ubicacion = '" + Ubica.ToString.Trim(" ") + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd.ExecuteReader()

        aux = ""

        While thisReader.Read()
            aux = thisReader.Item("Cantidad").ToString.Trim(" ")
            'Return aux
        End While

        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

        Return aux
        'Return Nothing

    End Function

    ' Obtiene la descripcion del producto 
    Public Function Get_ProdDesc(ByVal dato As String) As String

        Dim aux As String

        'If myConnection.State = Data.ConnectionState.Closed Then
        '    myConnection.Open()
        'End If

        Dim cmd2 As SqlCommand = myConnection.CreateCommand
        cmd2.CommandText = "SELECT * FROM Productos WHERE Codigo = '" + dato + "'"

        ' Execute Query
        Dim thisReader As SqlDataReader = cmd2.ExecuteReader()

        aux = ""

        While thisReader.Read()
            aux = thisReader.Item("Descripcion").ToString.Trim(" ")
            'Return aux
        End While

        ''Close the connection
        'If myConnection.State = Data.ConnectionState.Open Then
        '    myConnection.Close()
        'End If

        Return aux
        'Return Nothing

    End Function

    ' Guarda los articulos en archivo de texto para transferir a la terminal. 
    'Public Sub Trae_Logistik_stockHH()

    '    Dim output As New StreamWriter(rutaD + "Logistik_stock.txt", False, UnicodeEncoding.Default)

    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If

    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_stock, id_warehouse, id_product, id_product_attribute, reference," + _
    '    " ean13, upc, physical_quantity, usable_quantity, price_te, lote, id_supplier, clave_Remplace FROM Logistik_stock"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()

    '    While thisReader.Read

    '        'output.Write(thisReader.Item("Codigo").ToString.Trim(" ") & ";" & _
    '        output.Write(thisReader.Item("id_stock").ToString.Trim(" ") & ";" & thisReader.Item("id_warehouse").ToString.Trim(" ") & ";" & thisReader.Item("id_product").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_product_attribute").ToString.Trim(" ") & ";" & thisReader.Item("reference").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ean13").ToString.Trim(" ") & ";" & thisReader.Item("upc").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("physical_quantity").ToString.Trim(" ") & ";" & thisReader.Item("usable_quantity").ToString.Trim(" ") & ";" & _
    '                     thisReader.Item("price_te").ToString.Trim(" ") & ";" & thisReader.Item("lote").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_supplier").ToString.Trim(" ") & ";" & thisReader.Item("clave_Remplace").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While

    '    output.Close()

    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If

    'End Sub

    ' Guarda los Responsables en archivo de texto para transferir a la terminal. 
    'Public Sub Trae_Logistik_shop_groupHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_shop_group.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT shop_group_name, share_customer," + _
    '    "share_order ,share_stock, active, deleted FROM Logistik_shop_group"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("shop_group_name").ToString.Trim(" ") & ";" & thisReader.Item("share_customer").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("share_order").ToString.Trim(" ") & ";" & thisReader.Item("share_stock").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("active").ToString.Trim(" ") & ";" & thisReader.Item("deleted").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If

    'End Sub

    ' Guarda los proveedores en archivo de texto para transferir a la terminal. 
    'Public Sub Trae_Logistik_shop_urlHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_shop_url.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_shop, domain, domain_ssl," + _
    '    "physical_uri, virtual_uri, main, active FROM Logistik_shop_url"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_shop").ToString.Trim(" ") & ";" & thisReader.Item("domain").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("domain_ssl").ToString.Trim(" ") & ";" & thisReader.Item("physical_uri").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("virtual_uri").ToString.Trim(" ") & ";" & thisReader.Item("main").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("active").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_langHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_lang.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT lang_name, active, iso_code, language_code, date_format_lite," + _
    '    "date_format_full, is_rtl FROM Logistik_lang"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("lang_name").ToString.Trim(" ") & ";" & thisReader.Item("active").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("iso_code").ToString.Trim(" ") & ";" & thisReader.Item("language_code").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("date_format_lite").ToString.Trim(" ") & ";" & thisReader.Item("date_format_full").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("is_rtl").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_shopHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_shop.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_shop_group, shop_name," + _
    '    "id_category, id_theme, active, deleted, Logistik_shop_url_id_shop_url," + _
    '    "Logistik_employee_shop_id_employee, Logistik_employee_shop_id_shop ," + _
    '    "Logistik_shop_group_id_shop_group,  Logistik_lang_shop_id_lang ," + _
    '    "Logistik_lang_shop_id_shop ,        Logistik_lang_shop_Logistik_lang_id_lang FROM Logistik_shop"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_shop_group").ToString.Trim(" ") & ";" & thisReader.Item("shop_name").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_category").ToString.Trim(" ") & ";" & thisReader.Item("id_theme").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("active").ToString.Trim(" ") & ";" & thisReader.Item("deleted").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Logistik_shop_url_id_shop_url").ToString.Trim(" ") & ";" & thisReader.Item("Logistik_employee_shop_id_employee").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Logistik_employee_shop_id_shop").ToString.Trim(" ") & ";" & thisReader.Item("Logistik_shop_group_id_shop_group").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Logistik_lang_shop_id_lang").ToString.Trim(" ") & ";" & thisReader.Item("Logistik_lang_shop_id_shop").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Logistik_lang_shop_Logistik_lang_id_lang").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_productHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_product.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_supplier, id_manufacturer, id_category_default, id_shop_default, id_tax_rules_group," + _
    '    "on_sale, online_only, ean13, upc, ecotax, quantity, minimal_quantity, price, wholesale_price, unity, unit_price_ratio," + _
    '    "additional_shipping_cost, reference, supplier_reference, location, width, height, depth, weight, out_of_stock," + _
    '    "quantity_discount, customizable, uploadable_files, text_fields, active, redirect_type, id_product_redirected," + _
    '    "available_for_order, available_date, condition, show_price, indexed, visibility, cache_is_pack, cache_has_attachments, is_virtual, " + _
    '    "cache_default_attribute, date_add, date_upd, advanced_stock_management, pack_stock_type, upc_r1,upc_r2,upc_r3,upc_r4 FROM Logistik_product"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_supplier").ToString.Trim(" ") & ";" & thisReader.Item("id_manufacturer").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_category_default").ToString.Trim(" ") & ";" & thisReader.Item("id_shop_default").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_tax_rules_group").ToString.Trim(" ") & ";" & thisReader.Item("on_sale").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("online_only").ToString.Trim(" ") & ";" & thisReader.Item("ean13").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("upc").ToString.Trim(" ") & ";" & thisReader.Item("ecotax").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("quantity").ToString.Trim(" ") & ";" & thisReader.Item("minimal_quantity").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("price").ToString.Trim(" ") & ";" & thisReader.Item("wholesale_price").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("unity").ToString.Trim(" ") & ";" & thisReader.Item("unit_price_ratio").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("additional_shipping_cost").ToString.Trim(" ") & ";" & thisReader.Item("reference").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("supplier_reference").ToString.Trim(" ") & ";" & thisReader.Item("location").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("width").ToString.Trim(" ") & ";" & thisReader.Item("height").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("depth").ToString.Trim(" ") & ";" & thisReader.Item("weight").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("out_of_stock").ToString.Trim(" ") & ";" & thisReader.Item("quantity_discount").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("customizable").ToString.Trim(" ") & ";" & thisReader.Item("uploadable_files").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("text_fields").ToString.Trim(" ") & ";" & thisReader.Item("active").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("redirect_type").ToString.Trim(" ") & ";" & thisReader.Item("id_product_redirected").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("available_for_order").ToString.Trim(" ") & ";" & thisReader.Item("available_date").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("condition").ToString.Trim(" ") & ";" & thisReader.Item("show_price").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("indexed").ToString.Trim(" ") & ";" & thisReader.Item("visibility").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("cache_is_pack").ToString.Trim(" ") & ";" & thisReader.Item("cache_has_attachments").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("is_virtual").ToString.Trim(" ") & ";" & thisReader.Item("cache_default_attribute").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("date_add").ToString.Trim(" ") & ";" & thisReader.Item("date_upd").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("advanced_stock_management").ToString.Trim(" ") & ";" & thisReader.Item("pack_stock_type").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("upc_r1").ToString.Trim(" ") & ";" & thisReader.Item("upc_r2").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("upc_r3").ToString.Trim(" ") & ";" & thisReader.Item("upc_r4").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_product_attributeHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_product_attribute.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_product, reference, supplier_reference, location, ean13, upc, " + _
    '    "wholesale_price, price, ecotax, quantity, weight, unit_price_impact, default_on, " + _
    '    "minimal_quantity, available_date FROM Logistik_product_attribute"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_product").ToString.Trim(" ") & ";" & thisReader.Item("reference").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("supplier_reference").ToString.Trim(" ") & ";" & thisReader.Item("location").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ean13").ToString.Trim(" ") & ";" & thisReader.Item("upc").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("wholesale_price").ToString.Trim(" ") & ";" & thisReader.Item("price").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ecotax").ToString.Trim(" ") & ";" & thisReader.Item("quantity").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("weight").ToString.Trim(" ") & ";" & thisReader.Item("unit_price_impact").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("default_on").ToString.Trim(" ") & ";" & thisReader.Item("minimal_quantity").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("available_date").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_customerHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_customer.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_customer, id_shop_group, id_shop, id_gender, id_default_group," + _
    '    "id_lang, id_risk, company, siret, ape, firstname," + _
    '    "lastname, email, passwd, last_passwd_gen, birthday, newsletter," + _
    '    "ip_registration_newsletter, newsletter_date_add, optin, website, outstanding_allow_amount, show_public_prices," + _
    '    "max_payment_days, secure_key, note, active,  is_guest, deleted," + _
    '    "date_add,  date_upd, Logistik_gender_lang_id_gender, Logistik_gender_lang_id_lang FROM Logistik_customer"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_customer").ToString.Trim(" ") & ";" & thisReader.Item("id_shop_group").ToString.Trim(" ") & ";" & thisReader.Item("id_shop").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_gender").ToString.Trim(" ") & ";" & thisReader.Item("id_default_group").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_lang").ToString.Trim(" ") & ";" & thisReader.Item("id_risk").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("company").ToString.Trim(" ") & ";" & thisReader.Item("siret").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ape").ToString.Trim(" ") & ";" & thisReader.Item("firstname").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("lastname").ToString.Trim(" ") & ";" & thisReader.Item("email").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("passwd").ToString.Trim(" ") & ";" & thisReader.Item("last_passwd_gen").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("birthday").ToString.Trim(" ") & ";" & thisReader.Item("newsletter").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ip_registration_newsletter").ToString.Trim(" ") & ";" & thisReader.Item("newsletter_date_add").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("optin").ToString.Trim(" ") & ";" & thisReader.Item("website").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("outstanding_allow_amount").ToString.Trim(" ") & ";" & thisReader.Item("show_public_prices").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("max_payment_days").ToString.Trim(" ") & ";" & thisReader.Item("secure_key").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("note").ToString.Trim(" ") & ";" & thisReader.Item("active").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("is_guest").ToString.Trim(" ") & ";" & thisReader.Item("deleted").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("date_add").ToString.Trim(" ") & ";" & thisReader.Item("date_upd").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Logistik_gender_lang_id_gender").ToString.Trim(" ") & ";" & thisReader.Item("Logistik_gender_lang_id_lang").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_product_attributeHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_product_attribute.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_product, reference, supplier_reference, location, ean13, upc, " + _
    '    "wholesale_price, price, ecotax, quantity, weight, unit_price_impact, default_on, " + _
    '    "minimal_quantity, available_date FROM Logistik_product_attribute"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_product").ToString.Trim(" ") & ";" & thisReader.Item("reference").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("supplier_reference").ToString.Trim(" ") & ";" & thisReader.Item("location").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ean13").ToString.Trim(" ") & ";" & thisReader.Item("upc").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("wholesale_price").ToString.Trim(" ") & ";" & thisReader.Item("price").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ecotax").ToString.Trim(" ") & ";" & thisReader.Item("quantity").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("weight").ToString.Trim(" ") & ";" & thisReader.Item("unit_price_impact").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("default_on").ToString.Trim(" ") & ";" & thisReader.Item("minimal_quantity").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("available_date").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_product_attribute_combHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_product_attribute_comb.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_attribute, id_product_attribute FROM Logistik_product_attribute_comb"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_attribute").ToString.Trim(" ") & ";" & thisReader.Item("id_product_attribute").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_attribute_langHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_attribute_lang.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_attribute, id_lang, name FROM Logistik_attribute_lang"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_attribute").ToString.Trim(" ") & ";" & thisReader.Item("id_lang").ToString.Trim(" ") & ";" & _
    '                     thisReader.Item("name").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_supply_order_state_langHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_supply_order_state_lang.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_supply_order_state, id_lang, name FROM Logistik_supply_order_state_lang"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_supply_order_state").ToString.Trim(" ") & ";" & thisReader.Item("id_lang").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("name").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_supply_order_stateHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_supply_order_state.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT delivery_note, editable, receipt_state, pending_receipt, enclosed, color FROM Logistik_supply_order_state"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("delivery_note").ToString.Trim(" ") & ";" & thisReader.Item("editable").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("receipt_state").ToString.Trim(" ") & ";" & thisReader.Item("pending_receipt").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("enclosed").ToString.Trim(" ") & ";" & thisReader.Item("color").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_order_state_langHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_order_state_lang.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_order_state, id_lang, name, template FROM Logistik_order_state_lang"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_order_state").ToString.Trim(" ") & ";" & thisReader.Item("id_lang").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("name").ToString.Trim(" ") & ";" & thisReader.Item("template").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_logistik_order_stateHH()
    '    Dim output As New StreamWriter(rutaD + "logistik_order_state.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT invoice, send_email, module_name, color, unremovable, hidden," + _
    '    "logable, delivery, shipped, paid, pdf_invoice, pdf_delivery, deleted FROM logistik_order_state"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("invoice").ToString.Trim(" ") & ";" & thisReader.Item("send_email").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("module_name").ToString.Trim(" ") & ";" & thisReader.Item("color").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("unremovable").ToString.Trim(" ") & ";" & thisReader.Item("hidden").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("logable").ToString.Trim(" ") & ";" & thisReader.Item("delivery").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("shipped").ToString.Trim(" ") & ";" & thisReader.Item("paid").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("pdf_invoice").ToString.Trim(" ") & ";" & thisReader.Item("pdf_delivery").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("deleted").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_order_detailHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_order_detail.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_order_detail, id_order, id_order_invoice, id_warehouse, id_shop, product_id," + _
    '    "product_attribute_id, product_name, product_quantity, quantity_received, product_quantity_in_stock, product_quantity_refunded," + _
    '    "product_quantity_return, product_quantity_reinjected, product_price, reduction_percent,	reduction_amount," + _
    '    "reduction_amount_tax_incl, reduction_amount_tax_excl, group_reduction, product_quantity_discount, product_ean13," + _
    '    "product_upc, product_reference, product_supplier_reference,	product_weight,	id_tax_rules_group," + _
    '    "tax_computation_method,	tax_name, tax_rate,	ecotax,	ecotax_tax_rate," + _
    '    "discount_quantity_applied, download_hash, download_nb, download_deadline, total_price_tax_incl," + _
    '    "total_price_tax_excl, unit_price_tax_incl, unit_price_tax_excl, total_shipping_price_tax_incl,	total_shipping_price_tax_excl," + _
    '    "purchase_supplier_price, original_product_price FROM Logistik_order_detail"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_order_detail").ToString.Trim(" ") & ";" & thisReader.Item("id_order").ToString.Trim(" ") & ";" & thisReader.Item("id_order_invoice").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_warehouse").ToString.Trim(" ") & ";" & thisReader.Item("id_shop").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("product_id").ToString.Trim(" ") & ";" & thisReader.Item("product_attribute_id").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("product_name").ToString.Trim(" ") & ";" & thisReader.Item("product_quantity").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("quantity_received").ToString.Trim(" ") & ";" & thisReader.Item("product_quantity_in_stock").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("product_quantity_refunded").ToString.Trim(" ") & ";" & thisReader.Item("product_quantity_return").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("product_quantity_reinjected").ToString.Trim(" ") & ";" & thisReader.Item("product_price").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("reduction_percent").ToString.Trim(" ") & ";" & thisReader.Item("reduction_amount").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("reduction_amount_tax_incl").ToString.Trim(" ") & ";" & thisReader.Item("reduction_amount_tax_excl").ToString.Trim(" ") & ";" & thisReader.Item("group_reduction").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("product_quantity_discount").ToString.Trim(" ") & ";" & thisReader.Item("product_ean13").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("product_upc").ToString.Trim(" ") & ";" & thisReader.Item("product_reference").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("product_supplier_reference").ToString.Trim(" ") & ";" & thisReader.Item("product_weight").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_tax_rules_group").ToString.Trim(" ") & ";" & thisReader.Item("tax_computation_method").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("tax_name").ToString.Trim(" ") & ";" & thisReader.Item("tax_rate").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ecotax").ToString.Trim(" ") & ";" & thisReader.Item("ecotax_tax_rate").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("discount_quantity_applied").ToString.Trim(" ") & ";" & thisReader.Item("download_hash").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("download_nb").ToString.Trim(" ") & ";" & thisReader.Item("download_deadline").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_price_tax_incl").ToString.Trim(" ") & ";" & thisReader.Item("total_price_tax_excl").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("unit_price_tax_incl").ToString.Trim(" ") & ";" & thisReader.Item("unit_price_tax_excl").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_shipping_price_tax_incl").ToString.Trim(" ") & ";" & thisReader.Item("total_shipping_price_tax_excl").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("purchase_supplier_price").ToString.Trim(" ") & ";" & thisReader.Item("original_product_price").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_ordersHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_orders.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_order, reference, id_shop_group, id_shop, id_carrier, id_lang,  " + _
    '    "id_customer, id_cart, id_currency, id_address_delivery, id_address_invoice, current_state," + _
    '    "secure_key,	payment, conversion_rate, module, recyclable, gift," + _
    '    "gift_message,	mobile_theme, shipping_number, total_discounts,total_discounts_tax_incl, total_discounts_tax_excl," + _
    '    "total_paid,	total_paid_tax_incl, total_paid_tax_excl,total_paid_real, total_products, total_products_wt," + _
    '    "total_shipping,	total_shipping_tax_incl,total_shipping_tax_excl, carrier_tax_rate, total_wrapping, total_wrapping_tax_incl," + _
    '    " total_wrapping_tax_excl, round_mode,	invoice_number,	delivery_number, invoice_date, delivery_date," + _
    '    "valid, date_add, date_upd FROM Logistik_orders"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_order").ToString.Trim(" ") & ";" & thisReader.Item("reference").ToString.Trim(" ") & ";" & thisReader.Item("id_shop_group").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_shop").ToString.Trim(" ") & ";" & thisReader.Item("id_carrier").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_lang").ToString.Trim(" ") & ";" & thisReader.Item("id_customer").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_cart").ToString.Trim(" ") & ";" & thisReader.Item("id_currency").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_address_delivery").ToString.Trim(" ") & ";" & thisReader.Item("id_address_invoice").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("current_state").ToString.Trim(" ") & ";" & thisReader.Item("secure_key").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("payment").ToString.Trim(" ") & ";" & thisReader.Item("conversion_rate").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("module").ToString.Trim(" ") & ";" & thisReader.Item("recyclable").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("gift").ToString.Trim(" ") & ";" & thisReader.Item("gift_message").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("mobile_theme").ToString.Trim(" ") & ";" & thisReader.Item("shipping_number").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_discounts").ToString.Trim(" ") & ";" & thisReader.Item("total_discounts_tax_incl").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_discounts_tax_excl").ToString.Trim(" ") & ";" & thisReader.Item("total_paid").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_paid_tax_incl").ToString.Trim(" ") & ";" & thisReader.Item("total_paid_tax_excl").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_paid_real").ToString.Trim(" ") & ";" & thisReader.Item("total_products").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_products_wt").ToString.Trim(" ") & ";" & thisReader.Item("total_shipping").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_shipping_tax_incl").ToString.Trim(" ") & ";" & thisReader.Item("total_shipping_tax_excl").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("carrier_tax_rate").ToString.Trim(" ") & ";" & thisReader.Item("total_wrapping").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_wrapping_tax_incl").ToString.Trim(" ") & ";" & thisReader.Item("total_wrapping_tax_excl").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("round_mode").ToString.Trim(" ") & ";" & thisReader.Item("invoice_number").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("delivery_number").ToString.Trim(" ") & ";" & thisReader.Item("invoice_date").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("delivery_date").ToString.Trim(" ") & ";" & thisReader.Item("valid").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("date_add").ToString.Trim(" ") & ";" & thisReader.Item("date_upd").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub

    'Public Sub Trae_Logistik_supply_order_detailHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_supply_order_detail.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_supply_order_detail,id_supply_order, id_currency," + _
    '    "id_product, id_product_attribute, reference, supplier_reference, name," + _
    '    "ean13, upc, exchange_rate, unit_price_te, quantity_expected," + _
    '    "quantity_received, price_te, discount_rate, discount_value_te, price_with_discount_te," + _
    '    "tax_rate, tax_value, price_ti, tax_value_with_order_discount, price_with_order_discount_te FROM Logistik_supply_order_detail"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_supply_order_detail").ToString.Trim(" ") & ";" & thisReader.Item("id_supply_order").ToString.Trim(" ") & ";" & thisReader.Item("id_currency").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_product").ToString.Trim(" ") & ";" & thisReader.Item("id_product_attribute").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("reference").ToString.Trim(" ") & ";" & thisReader.Item("supplier_reference").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("name").ToString.Trim(" ") & ";" & thisReader.Item("ean13").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("upc").ToString.Trim(" ") & ";" & thisReader.Item("exchange_rate").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("unit_price_te").ToString.Trim(" ") & ";" & thisReader.Item("quantity_expected").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("quantity_received").ToString.Trim(" ") & ";" & thisReader.Item("price_te").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("discount_rate").ToString.Trim(" ") & ";" & thisReader.Item("discount_value_te").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("price_with_discount_te").ToString.Trim(" ") & ";" & thisReader.Item("tax_rate").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("tax_value").ToString.Trim(" ") & ";" & thisReader.Item("price_ti").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("tax_value_with_order_discount").ToString.Trim(" ") & ";" & thisReader.Item("price_with_order_discount_te").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_supply_orderHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_supply_order.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_supply_order,id_supplier, supplier_name," + _
    '    "id_lang, id_warehouse, id_supply_order_state, id_currency, id_ref_currency,  reference," + _
    '    "date_add, date_upd, date_delivery_expected, total_te, total_with_discount_te, total_tax," + _
    '    "total_ti, discount_rate, discount_value_te, is_template FROM Logistik_supply_order"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_supply_order").ToString.Trim(" ") & ";" & thisReader.Item("id_supplier").ToString.Trim(" ") & ";" & thisReader.Item("supplier_name").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_lang").ToString.Trim(" ") & ";" & thisReader.Item("id_warehouse").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_supply_order_state").ToString.Trim(" ") & ";" & thisReader.Item("id_currency").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_ref_currency").ToString.Trim(" ") & ";" & thisReader.Item("reference").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("date_add").ToString.Trim(" ") & ";" & thisReader.Item("date_upd").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("date_delivery_expected").ToString.Trim(" ") & ";" & thisReader.Item("total_te").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_with_discount_te").ToString.Trim(" ") & ";" & thisReader.Item("total_tax").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("total_ti").ToString.Trim(" ") & ";" & thisReader.Item("discount_rate").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("discount_value_te").ToString.Trim(" ") & ";" & thisReader.Item("is_template").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_supplierHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_supplier.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_supplier,name, date_add, date_upd, active FROM Logistik_supplier"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_supplier").ToString.Trim(" ") & ";" & thisReader.Item("name").ToString.Trim(" ") & ";" & thisReader.Item("date_add").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("date_upd").ToString.Trim(" ") & ";" & thisReader.Item("active").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_warehouseHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_warehouse.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_warehouse,id_currency, id_address, id_employee, reference," + _
    '    "name, management_type, deleted FROM Logistik_warehouse"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_warehouse").ToString.Trim(" ") & ";" & thisReader.Item("id_currency").ToString.Trim(" ") & ";" & thisReader.Item("id_address").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_employee").ToString.Trim(" ") & ";" & thisReader.Item("reference").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("name").ToString.Trim(" ") & ";" & thisReader.Item("management_type").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("deleted").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_warehouse_shopHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_warehouse_shop.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_shop, id_warehouse FROM Logistik_warehouse_shop"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_shop").ToString.Trim(" ") & ";" & thisReader.Item("id_warehouse").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_product_langHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_product_lang.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_product, id_shop, id_lang, description, description_short, link_rewrite," + _
    '    " meta_description, meta_keywords, meta_title, name, available_now, available_later FROM Logistik_product_lang"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_product").ToString.Trim(" ") & ";" & thisReader.Item("id_shop").ToString.Trim(" ") & ";" & _
    '                     thisReader.Item("id_lang").ToString.Trim(" ") & ";" & thisReader.Item("description").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("description_short").ToString.Trim(" ") & ";" & thisReader.Item("link_rewrite").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("meta_description").ToString.Trim(" ") & ";" & thisReader.Item("meta_keywords").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("meta_title").ToString.Trim(" ") & ";" & thisReader.Item("name").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("available_now").ToString.Trim(" ") & ";" & thisReader.Item("available_later").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_store_product_attributeHH()
    '    Dim output As New StreamWriter(rutaD + "store_product_attribute.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_product, reference, supplier_reference, location, ean13, upc, wholesale_price, price," + _
    '    " ecotax, quantity, weight, unit_price_impact, default_on, minimal_quantity, available_date FROM store_product_attribute"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_aditional").ToString.Trim(" ") & ";" & thisReader.Item("reference").ToString.Trim(" ") & ";" & _
    '                     thisReader.Item("supplier_reference").ToString.Trim(" ") & ";" & thisReader.Item("location").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ean13").ToString.Trim(" ") & ";" & thisReader.Item("upc").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("wholesale_price").ToString.Trim(" ") & ";" & thisReader.Item("price").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("ecotax").ToString.Trim(" ") & ";" & thisReader.Item("quantity").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("weight").ToString.Trim(" ") & ";" & thisReader.Item("unit_price_impact").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("default_on").ToString.Trim(" ") & ";" & thisReader.Item("minimal_quantity").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("available_date").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_aditional_langHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_aditional_lang.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_aditional, id_lang, name FROM Logistik_aditional_lang"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_aditional").ToString.Trim(" ") & ";" & thisReader.Item("id_lang").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("name").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    'Public Sub Trae_Logistik_lang_shopHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_lang_shop.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_lang, id_shop, Logistik_lang_id_lang FROM Logistik_lang_shop"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_lang").ToString.Trim(" ") & ";" & thisReader.Item("id_shop").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Logistik_lang_id_lang").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub


    'Public Sub Trae_Logistik_employeeHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_employee.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_employee, id_profile, id_lang, lastname, firstname," + _
    '    "email, passwd, last_passwd_gen, stats_date_from, stats_date_to," + _
    '    "stats_compare_from, stats_compare_to, stats_compare_option," + _
    '    "preselect_date_range, bo_color, bo_theme, bo_css, default_tab," + _
    '    "bo_width, bo_menu, active, optin, id_last_order, id_last_customer_message," + _
    '    "id_last_customer, last_connection_date, Logistik_lang_id_lang," + _
    '    "Logistik_customer_message_id_customer_message," + _
    '    "Logistik_customer_id_customer, login_user, Departamento, CentroCosto FROM Logistik_employee"
    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_employee").ToString.Trim(" ") & ";" & thisReader.Item("id_profile").ToString.Trim(" ") & ";" & thisReader.Item("id_lang").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("lastname").ToString.Trim(" ") & ";" & thisReader.Item("firstname").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("email").ToString.Trim(" ") & ";" & thisReader.Item("passwd").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("last_passwd_gen").ToString.Trim(" ") & ";" & thisReader.Item("stats_date_from").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("stats_date_to").ToString.Trim(" ") & ";" & thisReader.Item("stats_compare_from").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("stats_compare_to").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("stats_compare_option").ToString.Trim(" ") & ";" & thisReader.Item("preselect_date_range").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("bo_color").ToString.Trim(" ") & ";" & thisReader.Item("bo_theme").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("bo_css").ToString.Trim(" ") & ";" & thisReader.Item("default_tab").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("bo_width").ToString.Trim(" ") & ";" & thisReader.Item("bo_menu").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("active").ToString.Trim(" ") & ";" & thisReader.Item("optin").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_last_order").ToString.Trim(" ") & ";" & thisReader.Item("id_last_customer_message").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("id_last_customer").ToString.Trim(" ") & ";" & thisReader.Item("last_connection_date").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Logistik_lang_id_lang").ToString.Trim(" ") & ";" & thisReader.Item("Logistik_customer_message_id_customer_message").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Logistik_customer_id_customer").ToString.Trim(" ") & ";" & thisReader.Item("login_user").ToString.Trim(" ") & ";" & _
    '                    thisReader.Item("Departamento").ToString.Trim(" ") & ";" & thisReader.Item("CentroCosto").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub
    ' Guarda los proveedores en archivo de texto para transferir a la terminal. 
    'Public Sub Trae_Logistik_employee_shopHH()
    '    Dim output As New StreamWriter(rutaD + "Logistik_employee_shop.txt", False, UnicodeEncoding.Default)
    '    If myConnection.State = Data.ConnectionState.Closed Then
    '        myConnection.Open()
    '    End If
    '    Dim cmd As SqlCommand = myConnection.CreateCommand
    '    cmd.CommandText = "SELECT id_employee, id_shop, Logistik_employee_id_employee," + _
    '    "Logistik_employee_Logistik_lang_id_lang FROM Logistik_employee_shop"

    '    ' Execute Query
    '    Dim thisReader As SqlDataReader = cmd.ExecuteReader()
    '    While thisReader.Read
    '        output.Write(thisReader.Item("id_employee").ToString.Trim(" ") & ";" & thisReader.Item("id_shop").ToString.Trim(" ") & ";" & _
    '                     thisReader.Item("Logistik_employee_id_employee").ToString.Trim(" ") & ";" & thisReader.Item("Logistik_employee_Logistik_lang_id_lang").ToString.Trim(" "))
    '        output.WriteLine()
    '    End While
    '    output.Close()
    '    'Close the connection
    '    If myConnection.State = Data.ConnectionState.Open Then
    '        myConnection.Close()
    '    End If
    'End Sub

    Public Sub Guarda_Empleados(ByVal Codigo As String, ByVal nombre As String, ByVal clave As String, ByVal rol As String)

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        On Error GoTo SQLError5

        sqlInsertRow.CommandText = "INSERT INTO Empleados(Codigo, Nombre, Clave, Rol) VALUES (" _
        & "'" & Codigo & "'" & "," & "'" & nombre & "'" & "," & "'" & clave _
        & "'" & "," & "'" & rol & "'" & ")"
        sqlInsertRow.ExecuteNonQuery()

        GoTo Fin5

SQLError5:
        'MsgBox(Err.Number)
        'MsgBox(Err.Description)
        If Err.Number = 5 Then
            MsgBox("Empleado ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

Fin5:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If
    End Sub

    Public Sub Guarda_Ubicacion(ByVal Codigo As String, ByVal nombre As String)

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        On Error GoTo SQLError6

        sqlInsertRow.CommandText = "INSERT INTO Ubicaciones(Ubicacion) VALUES (" _
        & "'" & nombre & "'" & ")"
        sqlInsertRow.ExecuteNonQuery()

        GoTo Fin6

SQLError6:
        'MsgBox(Err.Number)
        'MsgBox(Err.Description)
        If Err.Number = 5 Then
            MsgBox("Ubicacion ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

Fin6:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If
    End Sub

    Public Sub Guarda_Aduana(ByVal Codigo As String, ByVal nombre As String)

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        On Error GoTo SQLError6

        sqlInsertRow.CommandText = "INSERT INTO Aduanas(Aduana) VALUES (" _
        & "'" & nombre & "'" & ")"
        sqlInsertRow.ExecuteNonQuery()

        GoTo Fin6

SQLError6:
        'MsgBox(Err.Number)
        'MsgBox(Err.Description)
        If Err.Number = 5 Then
            MsgBox("Aduana ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

Fin6:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If
    End Sub

    Public Sub Guarda_Articulo(ByVal Codigo As String, ByVal nombre As String, ByVal Cost As String, ByVal Min As String, ByVal Max As String, ByVal Reorden As String, ByVal serie As String)

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        Dim sqlInsertRow As SqlCommand = myConnection.CreateCommand()
        On Error GoTo SQLError9

        sqlInsertRow.CommandText = "INSERT INTO Productos(Codigo, Descripcion, Costo, Minimo, Maximo, Reorden, BanderaSerie) VALUES (" _
        & "'" & Codigo & "','" & nombre & "','" & Cost & "','" & Min & "','" & Max & "','" & Reorden & "','" & serie & "')"
        sqlInsertRow.ExecuteNonQuery()

        GoTo Fin9

SQLError9:
        'MsgBox(Err.Number)
        'MsgBox(Err.Description)
        If Err.Number = 5 Then
            MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

Fin9:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If
    End Sub

    ' Borra datos de Usuario
    Public Sub Borra_User(ByVal codigo As String)

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_BorraU

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "DELETE FROM Empleados WHERE Codigo ='" + codigo + "'"

        ' Execute Query
        cmd.ExecuteNonQuery()

        GoTo FinBU

Error_BorraU:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinBU:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

    End Sub

    ' Borra un Articulo
    Public Sub Borra_Articulo(ByVal codigo As String)

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_BorraU

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "DELETE FROM Productos WHERE Codigo ='" + codigo + "'"

        ' Execute Query
        cmd.ExecuteNonQuery()

        GoTo FinBU

Error_BorraU:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinBU:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

    End Sub

    ' Borra tabla de Productos
    Public Sub Borra_Productos()

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_BorraRES

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "DELETE FROM Productos"

        ' Execute Query
        cmd.ExecuteNonQuery()

        GoTo FinRES

Error_BorraRES:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinRES:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

    End Sub

    ' Borra tabla de Ubicaciones
    Public Sub Borra_Ubicaciones()

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_BorraRES

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "DELETE FROM Ubicaciones"

        ' Execute Query
        cmd.ExecuteNonQuery()

        GoTo FinRES

Error_BorraRES:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinRES:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

    End Sub

    ' Borra tabla de Ubicaciones
    Public Sub Borra_Aduanas()

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_BorraRES

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "DELETE FROM Aduanas"

        ' Execute Query
        cmd.ExecuteNonQuery()

        GoTo FinRES

Error_BorraRES:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinRES:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

    End Sub

    ' Borra tabla de Inventario
    Public Sub Borra_Inventario()

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_BorraRES

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "DELETE FROM Inventario"

        ' Execute Query
        cmd.ExecuteNonQuery()

        GoTo FinRES

Error_BorraRES:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinRES:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

    End Sub

    ' Borra datos de Ubicacion
    Public Sub Borra_Ubica(ByVal codigo As String)

        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If

        On Error GoTo Error_BorraU

        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "DELETE FROM Ubicaciones WHERE ID ='" + codigo + "'"

        ' Execute Query
        cmd.ExecuteNonQuery()

        GoTo FinBU

Error_BorraU:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If

FinBU:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If

    End Sub

    ' Borra datos de Aduana
    Public Sub Borra_Aduana(ByVal codigo As String)
        If myConnection.State = Data.ConnectionState.Closed Then
            myConnection.Open()
        End If
        On Error GoTo Error_BorraU
        Dim cmd As SqlCommand = myConnection.CreateCommand
        cmd.CommandText = "DELETE FROM Aduanas WHERE ID ='" + codigo + "'"
        ' Execute Query
        cmd.ExecuteNonQuery()
        GoTo FinBU
Error_BorraU:
        MsgBox(Err.Number & " " & Err.Description)
        If Err.Number = 5 Then
            'MsgBox("Producto ya existe!!!", MsgBoxStyle.Critical)
            '            Dim w As New StreamWriter(inst.GetAppPath + "\errores.txt")
            '            w.WriteLine(indata(0) + "," + indata(1) + "," + indata(2))
            '           w.Close()
            Resume Next
        End If
FinBU:
        'Close the connection
        If myConnection.State = Data.ConnectionState.Open Then
            myConnection.Close()
        End If
    End Sub
End Class
