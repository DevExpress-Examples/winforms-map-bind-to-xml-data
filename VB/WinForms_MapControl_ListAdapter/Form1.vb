Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraMap

Namespace WinForms_MapControl_ListAdapter
    Partial Public Class Form1
        Inherits Form

        Private Const bingKey As String = "YOUR_BING_MAPS_KEY_HERE"
        Private Const xmlFilepath As String = "..\..\Data\Ships.xml"
        Private Const imageFilepath As String = "..\..\Image\Ship.png"

        Public Sub New()
            InitializeComponent()
            InitializeMap()
        End Sub

        Private Sub InitializeMap()
'            #Region "#MapPreparation"
            Dim data As Object = LoadData(xmlFilepath)

            ' Create a map and data for it.
            Dim map As New MapControl() With {.CenterPoint = New GeoPoint(-37.2, 143.2), .ZoomLevel = 5, .Dock = DockStyle.Fill, .ToolTipController = New ToolTipController() With {.AllowHtmlText = True}, .ImageList = LoadImage(imageFilepath)}
            Me.Controls.Add(map)
'            #End Region ' #MapPreparation

            map.Layers.Add(New ImageTilesLayer() With { _
                .DataProvider = New BingMapDataProvider() With {.BingKey = bingKey} _
            })
'            #Region "#VectorData"
            ' Create a vector layer.
            map.Layers.Add(New VectorItemsLayer() With {.Data = CreateAdapter(data), .ToolTipPattern = "<b>{Name} ({Year})</b> " & ControlChars.CrLf & "{Description}", .ItemImageIndex = 0})
'            #End Region ' #VectorData

'            #Region "#MiniMap"
            ' Create a mini map and data for it.         
            Dim miniMap As New MiniMap() With {.Alignment = MiniMapAlignment.BottomLeft}
            miniMap.Layers.Add(New MiniMapImageTilesLayer() With { _
                .DataProvider = New BingMapDataProvider() With {.BingKey = bingKey} _
            })
            miniMap.Layers.Add(New MiniMapVectorItemsLayer() With {.Data = CreateMiniMapAdapter(data)})
            map.MiniMap = miniMap
'            #End Region ' #MiniMap

'            #Region "#Legend"
            'Create a Legend containing images.
            Dim legend As New ColorListLegend()
            legend.ImageList = map.ImageList
            legend.CustomItems.Add(New ColorLegendItem() With {.ImageIndex = 0, .Text = "Shipwreck"})
            map.Legends.Add(legend)
'            #End Region ' #Legend
        End Sub

        #Region "#CreateAdapter"
        ' Creates an adapter for the map's vector layer.
        Private Function CreateAdapter(ByVal source As Object) As IMapDataAdapter
            Dim adapter As New ListSourceDataAdapter()

            adapter.DataSource = source

            adapter.Mappings.Latitude = "Latitude"
            adapter.Mappings.Longitude = "Longitude"

            adapter.AttributeMappings.Add(New MapItemAttributeMapping() With {.Member = "Name", .Name = "Name"})
            adapter.AttributeMappings.Add(New MapItemAttributeMapping() With {.Member = "Year", .Name = "Year"})
            adapter.AttributeMappings.Add(New MapItemAttributeMapping() With {.Member = "Description", .Name = "Description"})

            Return adapter
        End Function
        #End Region ' #CreateAdapter

        #Region "#CreateMiniMapAdapter"
        ' Creates an adapter for the mini map's vector layer.
        Private Function CreateMiniMapAdapter(ByVal source As Object) As IMapDataAdapter
            Dim adapter As New ListSourceDataAdapter()

            adapter.DataSource = source

            adapter.Mappings.Latitude = "Latitude"
            adapter.Mappings.Longitude = "Longitude"

            adapter.PropertyMappings.Add(New MapItemFillMapping() With {.DefaultValue = Color.Red})
            adapter.PropertyMappings.Add(New MapItemStrokeMapping() With {.DefaultValue = Color.White})
            adapter.PropertyMappings.Add(New MapItemStrokeWidthMapping() With {.DefaultValue = 2})
            adapter.PropertyMappings.Add(New MapDotSizeMapping() With {.DefaultValue = 8})

            adapter.DefaultMapItemType = MapItemType.Dot

            Return adapter
        End Function
        #End Region ' #CreateMiniMapAdapter

        #Region "#LoadData"
        ' Loads data from a XML file.
        Private Function LoadData(ByVal path As String) As DataTable
            Dim ds As New DataSet()
            ds.ReadXml(path)
            Dim table As DataTable = ds.Tables(0)
            Return table
        End Function
        #End Region ' #LoadData

        #Region "#LoadImage"
        ' Loads an image to an image collection.
        Private Function LoadImage(ByVal path As String) As ImageCollection
            Dim imageCollection As New ImageCollection()
            Dim image As New Bitmap(path)
            imageCollection.ImageSize = New Size(50, 50)
            imageCollection.Images.Add(image)
            Return imageCollection
        End Function
        #End Region ' #LoadImage
    End Class

End Namespace