Namespace WinForms_MapControl_ListAdapter
    Partial Public Class Form1
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(Form1))
            Dim colorListLegend1 As New DevExpress.XtraMap.ColorListLegend()
            Dim colorLegendItem1 As New DevExpress.XtraMap.ColorLegendItem()
            Me.imageCollection = New DevExpress.Utils.ImageCollection(Me.components)
            Me.map = New DevExpress.XtraMap.MapControl()
            Me.imageLayer1 = New DevExpress.XtraMap.ImageLayer()
            Me.bingMapDataProvider1 = New DevExpress.XtraMap.BingMapDataProvider()
            Me.toolTipController = New DevExpress.Utils.ToolTipController(Me.components)
            CType(Me.imageCollection, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.map, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' imageCollection
            ' 
            Me.imageCollection.ImageSize = New System.Drawing.Size(32, 32)
            Me.imageCollection.ImageStream = (CType(resources.GetObject("imageCollection.ImageStream"), DevExpress.Utils.ImageCollectionStreamer))
            Me.imageCollection.Images.SetKeyName(0, "Ship.png")
            ' 
            ' map
            ' 
            Me.map.CenterPoint = New DevExpress.XtraMap.GeoPoint(-37.2R, 143.2R)
            Me.map.Dock = System.Windows.Forms.DockStyle.Fill
            Me.map.ImageList = Me.imageCollection
            Me.map.Layers.Add(Me.imageLayer1)
            colorLegendItem1.ImageIndex = 0
            colorLegendItem1.Text = "Shipwreck"
            colorListLegend1.CustomItems.Add(colorLegendItem1)
            colorListLegend1.Header = ""
            colorListLegend1.ImageList = Me.imageCollection
            Me.map.Legends.Add(colorListLegend1)
            Me.map.Location = New System.Drawing.Point(0, 0)
            Me.map.Name = "map"
            Me.map.Size = New System.Drawing.Size(812, 554)
            Me.map.TabIndex = 0
            Me.map.ToolTipController = Me.toolTipController
            Me.map.ZoomLevel = 5R
            Me.imageLayer1.DataProvider = Me.bingMapDataProvider1
            Me.bingMapDataProvider1.BingKey = "YOUR BING MAPS KEY"
            Me.bingMapDataProvider1.Kind = DevExpress.XtraMap.BingMapKind.Area
            ' 
            ' toolTipController
            ' 
            Me.toolTipController.AllowHtmlText = True
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(812, 554)
            Me.Controls.Add(Me.map)
            Me.Name = "Form1"
            Me.Text = "Form1"
            CType(Me.imageCollection, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.map, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private map As DevExpress.XtraMap.MapControl
        Private toolTipController As DevExpress.Utils.ToolTipController
        Private imageLayer1 As DevExpress.XtraMap.ImageLayer
        Private bingMapDataProvider1 As DevExpress.XtraMap.BingMapDataProvider
        Private imageCollection As DevExpress.Utils.ImageCollection
    End Class
End Namespace

