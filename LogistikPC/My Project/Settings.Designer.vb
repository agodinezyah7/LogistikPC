﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.42000
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.7.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "Funcionalidad para autoguardar My.Settings"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.1.117;Initial Catalog=LogistikMasterValvulerias;Integrated Se"& _ 
            "curity=False;Uid=sa; Password=Aspel2020$;MultipleActiveResultSets=true")>  _
        Public Property ConexionLocal() As String
            Get
                Return CType(Me("ConexionLocal"),String)
            End Get
            Set
                Me("ConexionLocal") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=10.0.2.15;Initial Catalog=SAE70_Emp06;Integrated Security=True;Multip"& _ 
            "leActiveResultSets=True;Uid=admin; Password=PASS")>  _
        Public Property ConexionSAE() As String
            Get
                Return CType(Me("ConexionSAE"),String)
            End Get
            Set
                Me("ConexionSAE") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("192.168.1.117")>  _
        Public Property vgCrearBD_IP() As String
            Get
                Return CType(Me("vgCrearBD_IP"),String)
            End Get
            Set
                Me("vgCrearBD_IP") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("192.168.1.117")>  _
        Public Property vgBDOtroLado_IP2() As String
            Get
                Return CType(Me("vgBDOtroLado_IP2"),String)
            End Get
            Set
                Me("vgBDOtroLado_IP2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("sa")>  _
        Public Property vgBDOtroLado_User() As String
            Get
                Return CType(Me("vgBDOtroLado_User"),String)
            End Get
            Set
                Me("vgBDOtroLado_User") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Aspel2020$")>  _
        Public Property pass() As String
            Get
                Return CType(Me("pass"),String)
            End Get
            Set
                Me("pass") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Aspel2020$")>  _
        Public Property vgBDOtroLado_Pass() As String
            Get
                Return CType(Me("vgBDOtroLado_Pass"),String)
            End Get
            Set
                Me("vgBDOtroLado_Pass") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("LogistikMasterValvulerias")>  _
        Public Property vgCrearBD_Name() As String
            Get
                Return CType(Me("vgCrearBD_Name"),String)
            End Get
            Set
                Me("vgCrearBD_Name") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.1.117;Initial Catalog=LogistikMasterValvulerias01;Integrated "& _ 
            "Security=False;Uid=sa; Password=Aspel2020$;MultipleActiveResultSets=true")>  _
        Public Property ConexionLocal2() As String
            Get
                Return CType(Me("ConexionLocal2"),String)
            End Get
            Set
                Me("ConexionLocal2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("13")>  _
        Public Property ProvEmp01() As String
            Get
                Return CType(Me("ProvEmp01"),String)
            End Get
            Set
                Me("ProvEmp01") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("C:\Program Files\Common Files\Aspel\Sistemas Aspel\SAE7.00\Empresa01\DATOS\SAE70E"& _ 
            "MPRE01.FDB")>  _
        Public Property Ruta_SAE_FB() As String
            Get
                Return CType(Me("Ruta_SAE_FB"),String)
            End Get
            Set
                Me("Ruta_SAE_FB") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property ProvEmp04() As String
            Get
                Return CType(Me("ProvEmp04"),String)
            End Get
            Set
                Me("ProvEmp04") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.LogistikPC.My.MySettings
            Get
                Return Global.LogistikPC.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
