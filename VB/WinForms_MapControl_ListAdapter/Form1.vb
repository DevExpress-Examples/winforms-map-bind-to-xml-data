Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraMap

Namespace WinForms_MapControl_ListAdapter

    Public Partial Class Form1
        Inherits System.Windows.Forms.Form

        Const bingKey As String = "YOUR_BING_MAPS_KEY_HERE"

        Const xmlFilepath As String = "..\..\Data\Ships.xml"

        Const imageFilepath As String = "..\..\Image\Ship.png"

        Public Sub New()
            Me.InitializeComponent()
            Me.InitializeMap()
        End Sub

        Private Sub InitializeMap()
'#Region "#MapPreparation"
            Dim data As Object = Me.LoadData(WinForms_MapControl_ListAdapter.Form1.xmlFilepath)
            ' Create a map and data for it.
            Dim map As DevExpress.XtraMap.MapControl = New DevExpress.XtraMap.MapControl() With {.CenterPoint = New DevExpress.XtraMap.GeoPoint(-37.2, 143.2), .ZoomLevel = 5, .Dock = System.Windows.Forms.DockStyle.Fill, .ToolTipController = New DevExpress.Utils.ToolTipController() With {.AllowHtmlText = True}, .ImageList = Me.LoadImage(WinForms_MapControl_ListAdapter.Form1.imageFilepath)}
            Me.Controls.Add(map)
'#End Region  ' #MapPreparation
            map.Layers.Add(New DevExpress.XtraMap.ImageTilesLayer() With {.DataProvider = New DevExpress.XtraMap.BingMapDataProvider() With {.BingKey = WinForms_MapControl_ListAdapter.Form1.bingKey}})
'#Region "#VectorData"
            ' Create a vector layer.
            map.Layers.Add(New DevExpress.XtraMap.VectorItemsLayer() With {.Data = Me.CreateAdapter(data), .ToolTipPattern = "<b>{Name} ({Year})</b> " & Global.Microsoft.VisualBasic.Constants.vbCrLf & "{Description}", .ItemImageIndex = 0})
'#End Region  ' #VectorData
'#Region "#MiniMap"
            ' Create a mini map and data for it.         
            Dim miniMap As DevExpress.XtraMap.MiniMap = New DevExpress.XtraMap.MiniMap() With {.Alignment = DevExpress.XtraMap.MiniMapAlignment.BottomLeft}
            miniMap.Layers.Add(New DevExpress.XtraMap.MiniMapImageTilesLayer() With {.DataProvider = New DevExpress.XtraMap.BingMapDataProvider() With {.BingKey = WinForms_MapControl_ListAdapter.Form1.bingKey}})
            miniMap.Layers.Add(New DevExpress.XtraMap.MiniMapVectorItemsLayer() With {.Data = Me.CreateMiniMapAdapter(data)})
            map.MiniMap = miniMap
'#End Region  ' #MiniMap
'#Region "#Legend"
            'Create a Legend containing images.
            Dim legend As DevExpress.XtraMap.ColorListLegend = New DevExpress.XtraMap.ColorListLegend()
            legend.ImageList = map.ImageList
            legend.CustomItems.Add(New DevExpress.XtraMap.ColorLegendItem() With {.ImageIndex = 0, .Text = "Shipwreck"})
            map.Legends.Add(legend)
'#End Region  ' #Legend
        End Sub

'#Region "#CreateAdapter"
        ' Creates an adapter for the map's vector layer.
        Private Function CreateAdapter(ByVal source As Object) As IMapDataAdapter
            Dim adapter As DevExpress.XtraMap.ListSourceDataAdapter = New DevExpress.XtraMap.ListSourceDataAdapter()
            adapter.DataSource = source
            adapter.Mappings.Latitude = "Latitude"
            adapter.Mappings.Longitude = "Longitude"
            adapter.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Name", .Name = "Name"})
            adapter.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Year", .Name = "Year"})
            adapter.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Description", .Name = "Description"})
            Return adapter
        End Function

'#End Region  ' #CreateAdapter
'#Region "#CreateMiniMapAdapter"
        ' Creates an adapter for the mini map's vector layer.
        Private Function CreateMiniMapAdapter(ByVal source As Object) As IMapDataAdapter
            Dim adapter As DevExpress.XtraMap.ListSourceDataAdapter = New DevExpress.XtraMap.ListSourceDataAdapter()
            adapter.DataSource = source
            adapter.Mappings.Latitude = "Latitude"
            adapter.Mappings.Longitude = "Longitude"
            adapter.PropertyMappings.Add(New DevExpress.XtraMap.MapItemFillMapping() With {.DefaultValue = System.Drawing.Color.Red})
            adapter.PropertyMappings.Add(New DevExpress.XtraMap.MapItemStrokeMapping() With {.DefaultValue = System.Drawing.Color.White})
            adapter.PropertyMappings.Add(New DevExpress.XtraMap.MapItemStrokeWidthMapping() With {.DefaultValue = 2})
            adapter.PropertyMappings.Add(New DevExpress.XtraMap.MapDotSizeMapping() With {.DefaultValue = 8})
            adapter.DefaultMapItemType = DevExpress.XtraMap.MapItemType.Dot
            Return adapter
        End Function

'#End Region  ' #CreateMiniMapAdapter
'#Region "#LoadData"
        ' Loads data from a XML file.
        Private Function LoadData(ByVal path As String) As DataTable
            Dim ds As System.Data.DataSet = New System.Data.DataSet()
            ds.ReadXml(path)
            Dim table As System.Data.DataTable = ds.Tables(0)
            Return table
        End Function

'#End Region  ' #LoadData
'#Region "#LoadImage"
        ' Loads an image to an image collection.
        Private Function LoadImage(ByVal path As String) As ImageCollection
            Dim imageCollection As DevExpress.Utils.ImageCollection = New DevExpress.Utils.ImageCollection()
            Dim image As System.Drawing.Bitmap = New System.Drawing.Bitmap(path)
            imageCollection.ImageSize = New System.Drawing.Size(50, 50)
            imageCollection.Images.Add(image)
            Return imageCollection
        End Function
'#End Region  ' #LoadImage
    End Class
End Namespace
