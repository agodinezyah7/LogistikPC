<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MenuPrincipal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MenuPrincipal))
        Me.ButtonNegativos = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.LabelProcedimiento = New System.Windows.Forms.Label()
        Me.LabelEmpresa = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextHora = New System.Windows.Forms.TextBox()
        Me.DateTimePickerFecha = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxEnviado = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMostrar = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.DGV_OC = New System.Windows.Forms.DataGridView()
        Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelTABLA = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.ImportarStockToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AlmacenesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InventarioFisicoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsuariosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CatalogosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProveedoresToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrdenDeTraspsosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DiferenciasDeAlmacenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutorizaciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KardexDelProductoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguraciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TraspasosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrdenesDeCompraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PedidosDeVentaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.LabelQuery = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BttmAjustarInventario = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_OC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonNegativos
        '
        Me.ButtonNegativos.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ButtonNegativos.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonNegativos.Location = New System.Drawing.Point(635, 137)
        Me.ButtonNegativos.Name = "ButtonNegativos"
        Me.ButtonNegativos.Size = New System.Drawing.Size(140, 40)
        Me.ButtonNegativos.TabIndex = 87
        Me.ButtonNegativos.Text = "Mantenimiento"
        Me.ButtonNegativos.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'PictureBox3
        '
        Me.PictureBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(1059, 600)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(111, 23)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 80
        Me.PictureBox3.TabStop = False
        '
        'LabelProcedimiento
        '
        Me.LabelProcedimiento.AutoSize = True
        Me.LabelProcedimiento.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelProcedimiento.ForeColor = System.Drawing.Color.Blue
        Me.LabelProcedimiento.Location = New System.Drawing.Point(843, 125)
        Me.LabelProcedimiento.Name = "LabelProcedimiento"
        Me.LabelProcedimiento.Size = New System.Drawing.Size(111, 17)
        Me.LabelProcedimiento.TabIndex = 78
        Me.LabelProcedimiento.Text = "Procedimiento"
        '
        'LabelEmpresa
        '
        Me.LabelEmpresa.AutoSize = True
        Me.LabelEmpresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelEmpresa.ForeColor = System.Drawing.Color.Blue
        Me.LabelEmpresa.Location = New System.Drawing.Point(843, 94)
        Me.LabelEmpresa.Name = "LabelEmpresa"
        Me.LabelEmpresa.Size = New System.Drawing.Size(71, 17)
        Me.LabelEmpresa.TabIndex = 77
        Me.LabelEmpresa.Text = "Empresa"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(969, 40)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(108, 37)
        Me.Button2.TabIndex = 75
        Me.Button2.Text = "Completo"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(843, 40)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 37)
        Me.Button1.TabIndex = 74
        Me.Button1.Text = "Sincronizar"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TextHora
        '
        Me.TextHora.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextHora.Enabled = False
        Me.TextHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextHora.Location = New System.Drawing.Point(1095, 40)
        Me.TextHora.Name = "TextHora"
        Me.TextHora.Size = New System.Drawing.Size(70, 37)
        Me.TextHora.TabIndex = 76
        Me.TextHora.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateTimePickerFecha
        '
        Me.DateTimePickerFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFecha.Location = New System.Drawing.Point(467, 156)
        Me.DateTimePickerFecha.Name = "DateTimePickerFecha"
        Me.DateTimePickerFecha.Size = New System.Drawing.Size(109, 20)
        Me.DateTimePickerFecha.TabIndex = 85
        '
        'CheckBoxEnviado
        '
        Me.CheckBoxEnviado.AutoSize = True
        Me.CheckBoxEnviado.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxEnviado.Location = New System.Drawing.Point(251, 156)
        Me.CheckBoxEnviado.Name = "CheckBoxEnviado"
        Me.CheckBoxEnviado.Size = New System.Drawing.Size(172, 21)
        Me.CheckBoxEnviado.TabIndex = 84
        Me.CheckBoxEnviado.Text = "Mostrar enviado a SAE"
        Me.CheckBoxEnviado.UseVisualStyleBackColor = True
        '
        'CheckBoxMostrar
        '
        Me.CheckBoxMostrar.AutoSize = True
        Me.CheckBoxMostrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBoxMostrar.Location = New System.Drawing.Point(39, 157)
        Me.CheckBoxMostrar.Name = "CheckBoxMostrar"
        Me.CheckBoxMostrar.Size = New System.Drawing.Size(155, 21)
        Me.CheckBoxMostrar.TabIndex = 83
        Me.CheckBoxMostrar.Text = "Mostrar solo errores"
        Me.CheckBoxMostrar.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(0, 37)
        Me.Label5.TabIndex = 81
        Me.Label5.UseMnemonic = False
        '
        'Timer3
        '
        Me.Timer3.Interval = 7000
        '
        'DGV_OC
        '
        Me.DGV_OC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_OC.Location = New System.Drawing.Point(12, 184)
        Me.DGV_OC.Name = "DGV_OC"
        Me.DGV_OC.Size = New System.Drawing.Size(1166, 388)
        Me.DGV_OC.TabIndex = 82
        '
        'Timer4
        '
        Me.Timer4.Interval = 1000
        '
        'LabelTABLA
        '
        Me.LabelTABLA.AutoSize = True
        Me.LabelTABLA.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTABLA.ForeColor = System.Drawing.Color.Blue
        Me.LabelTABLA.Location = New System.Drawing.Point(843, 157)
        Me.LabelTABLA.Name = "LabelTABLA"
        Me.LabelTABLA.Size = New System.Drawing.Size(57, 17)
        Me.LabelTABLA.TabIndex = 79
        Me.LabelTABLA.Text = "TABLA"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(12, 27)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(161, 33)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 73
        Me.PictureBox2.TabStop = False
        '
        'ImportarStockToolStripMenuItem
        '
        Me.ImportarStockToolStripMenuItem.Name = "ImportarStockToolStripMenuItem"
        Me.ImportarStockToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ImportarStockToolStripMenuItem.Text = "Importar Inv. Inicial"
        Me.ImportarStockToolStripMenuItem.Visible = False
        '
        'SalirToolStripMenuItem
        '
        Me.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem"
        Me.SalirToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.SalirToolStripMenuItem.Text = "Salir"
        '
        'AlmacenesToolStripMenuItem
        '
        Me.AlmacenesToolStripMenuItem.Name = "AlmacenesToolStripMenuItem"
        Me.AlmacenesToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.AlmacenesToolStripMenuItem.Text = "Almacenes"
        Me.AlmacenesToolStripMenuItem.Visible = False
        '
        'InventarioFisicoToolStripMenuItem
        '
        Me.InventarioFisicoToolStripMenuItem.Name = "InventarioFisicoToolStripMenuItem"
        Me.InventarioFisicoToolStripMenuItem.Size = New System.Drawing.Size(105, 20)
        Me.InventarioFisicoToolStripMenuItem.Text = "Inventario Fisico"
        Me.InventarioFisicoToolStripMenuItem.Visible = False
        '
        'UsuariosToolStripMenuItem
        '
        Me.UsuariosToolStripMenuItem.Name = "UsuariosToolStripMenuItem"
        Me.UsuariosToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.UsuariosToolStripMenuItem.Text = "Usuarios"
        '
        'CatalogosToolStripMenuItem
        '
        Me.CatalogosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProductosToolStripMenuItem, Me.ProveedoresToolStripMenuItem, Me.UsuariosToolStripMenuItem, Me.AlmacenesToolStripMenuItem, Me.ImportarStockToolStripMenuItem})
        Me.CatalogosToolStripMenuItem.Name = "CatalogosToolStripMenuItem"
        Me.CatalogosToolStripMenuItem.Size = New System.Drawing.Size(72, 20)
        Me.CatalogosToolStripMenuItem.Text = "Catalogos"
        Me.CatalogosToolStripMenuItem.Visible = False
        '
        'ProductosToolStripMenuItem
        '
        Me.ProductosToolStripMenuItem.Name = "ProductosToolStripMenuItem"
        Me.ProductosToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ProductosToolStripMenuItem.Text = "Productos"
        Me.ProductosToolStripMenuItem.Visible = False
        '
        'ProveedoresToolStripMenuItem
        '
        Me.ProveedoresToolStripMenuItem.Name = "ProveedoresToolStripMenuItem"
        Me.ProveedoresToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.ProveedoresToolStripMenuItem.Text = "Proveedores"
        Me.ProveedoresToolStripMenuItem.Visible = False
        '
        'OrdenDeTraspsosToolStripMenuItem
        '
        Me.OrdenDeTraspsosToolStripMenuItem.Name = "OrdenDeTraspsosToolStripMenuItem"
        Me.OrdenDeTraspsosToolStripMenuItem.Size = New System.Drawing.Size(121, 20)
        Me.OrdenDeTraspsosToolStripMenuItem.Text = "Orden de Traspasos"
        Me.OrdenDeTraspsosToolStripMenuItem.Visible = False
        '
        'DiferenciasDeAlmacenToolStripMenuItem
        '
        Me.DiferenciasDeAlmacenToolStripMenuItem.Name = "DiferenciasDeAlmacenToolStripMenuItem"
        Me.DiferenciasDeAlmacenToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.DiferenciasDeAlmacenToolStripMenuItem.Text = "Diferencias de Almacen"
        Me.DiferenciasDeAlmacenToolStripMenuItem.Visible = False
        '
        'AutorizaciónToolStripMenuItem
        '
        Me.AutorizaciónToolStripMenuItem.Name = "AutorizaciónToolStripMenuItem"
        Me.AutorizaciónToolStripMenuItem.Size = New System.Drawing.Size(139, 20)
        Me.AutorizaciónToolStripMenuItem.Text = "Autorización Traspasos"
        Me.AutorizaciónToolStripMenuItem.Visible = False
        '
        'KardexDelProductoToolStripMenuItem
        '
        Me.KardexDelProductoToolStripMenuItem.Name = "KardexDelProductoToolStripMenuItem"
        Me.KardexDelProductoToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.KardexDelProductoToolStripMenuItem.Text = "Kardex del producto"
        Me.KardexDelProductoToolStripMenuItem.Visible = False
        '
        'ConfiguraciónToolStripMenuItem
        '
        Me.ConfiguraciónToolStripMenuItem.Name = "ConfiguraciónToolStripMenuItem"
        Me.ConfiguraciónToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.ConfiguraciónToolStripMenuItem.Text = "Configuración"
        Me.ConfiguraciónToolStripMenuItem.Visible = False
        '
        'TraspasosToolStripMenuItem
        '
        Me.TraspasosToolStripMenuItem.Name = "TraspasosToolStripMenuItem"
        Me.TraspasosToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.TraspasosToolStripMenuItem.Text = "Traspasos"
        '
        'ReportesToolStripMenuItem
        '
        Me.ReportesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OrdenesDeCompraToolStripMenuItem, Me.PedidosDeVentaToolStripMenuItem, Me.TraspasosToolStripMenuItem, Me.KardexDelProductoToolStripMenuItem, Me.DiferenciasDeAlmacenToolStripMenuItem})
        Me.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        Me.ReportesToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.ReportesToolStripMenuItem.Text = "Reportes"
        Me.ReportesToolStripMenuItem.Visible = False
        '
        'OrdenesDeCompraToolStripMenuItem
        '
        Me.OrdenesDeCompraToolStripMenuItem.Name = "OrdenesDeCompraToolStripMenuItem"
        Me.OrdenesDeCompraToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.OrdenesDeCompraToolStripMenuItem.Text = "Ordenes de Compra"
        '
        'PedidosDeVentaToolStripMenuItem
        '
        Me.PedidosDeVentaToolStripMenuItem.Name = "PedidosDeVentaToolStripMenuItem"
        Me.PedidosDeVentaToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.PedidosDeVentaToolStripMenuItem.Text = "Pedidos de Venta"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CatalogosToolStripMenuItem, Me.InventarioFisicoToolStripMenuItem, Me.OrdenDeTraspsosToolStripMenuItem, Me.AutorizaciónToolStripMenuItem, Me.ReportesToolStripMenuItem, Me.ConfiguraciónToolStripMenuItem, Me.SalirToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1190, 24)
        Me.MenuStrip1.TabIndex = 72
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'LabelQuery
        '
        Me.LabelQuery.Location = New System.Drawing.Point(12, 578)
        Me.LabelQuery.Multiline = True
        Me.LabelQuery.Name = "LabelQuery"
        Me.LabelQuery.Size = New System.Drawing.Size(1042, 73)
        Me.LabelQuery.TabIndex = 88
        Me.LabelQuery.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(179, 27)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(596, 33)
        Me.TextBox1.TabIndex = 89
        Me.TextBox1.Visible = False
        '
        'BttmAjustarInventario
        '
        Me.BttmAjustarInventario.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BttmAjustarInventario.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttmAjustarInventario.Location = New System.Drawing.Point(658, 578)
        Me.BttmAjustarInventario.Name = "BttmAjustarInventario"
        Me.BttmAjustarInventario.Size = New System.Drawing.Size(182, 58)
        Me.BttmAjustarInventario.TabIndex = 90
        Me.BttmAjustarInventario.Text = "Ajustar Inventario"
        Me.BttmAjustarInventario.UseVisualStyleBackColor = False
        Me.BttmAjustarInventario.Visible = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(846, 578)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(182, 58)
        Me.Button3.TabIndex = 91
        Me.Button3.Text = "Ajustar Inventario"
        Me.Button3.UseVisualStyleBackColor = False
        Me.Button3.Visible = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(467, 578)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(185, 58)
        Me.Button4.TabIndex = 92
        Me.Button4.Text = "Campos Libres"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'MenuPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.MediumAquamarine
        Me.ClientSize = New System.Drawing.Size(1190, 673)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.BttmAjustarInventario)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.LabelQuery)
        Me.Controls.Add(Me.ButtonNegativos)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.LabelProcedimiento)
        Me.Controls.Add(Me.LabelEmpresa)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextHora)
        Me.Controls.Add(Me.DateTimePickerFecha)
        Me.Controls.Add(Me.CheckBoxEnviado)
        Me.Controls.Add(Me.CheckBoxMostrar)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DGV_OC)
        Me.Controls.Add(Me.LabelTABLA)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MenuPrincipal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Valvuleria GDL y MX - Sincronizador SAE versión 12.9"
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_OC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonNegativos As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents LabelProcedimiento As System.Windows.Forms.Label
    Friend WithEvents LabelEmpresa As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextHora As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents CheckBoxEnviado As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxMostrar As System.Windows.Forms.CheckBox
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents DGV_OC As System.Windows.Forms.DataGridView
    Friend WithEvents Timer4 As System.Windows.Forms.Timer
    Friend WithEvents LabelTABLA As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents ImportarStockToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AlmacenesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InventarioFisicoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsuariosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CatalogosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProveedoresToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrdenDeTraspsosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DiferenciasDeAlmacenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutorizaciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KardexDelProductoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfiguraciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TraspasosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrdenesDeCompraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PedidosDeVentaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents LabelQuery As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents BttmAjustarInventario As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As Button
End Class
