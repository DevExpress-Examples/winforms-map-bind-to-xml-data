Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Linq
Imports System.Xml.Linq
Imports DevExpress.XtraMap

Namespace WinForms_MapControl_ListAdapter
    Partial Public Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()
            InitializeMap()
        End Sub

        Private Sub InitializeMap()
            Dim data As Object = LoadData("..\..\Data\Ships.xml")

'            #Region "#VectorData"
            ' Create a vector layer.
            map.Layers.Add(New VectorItemsLayer() With { _
                .Data = CreateAdapter(data), _
                .ToolTipPattern = "<b>{Name} ({Year})</b> " & ControlChars.CrLf & "{Description}", _
                .ItemImageIndex = 0 _
            })
'            #End Region ' #VectorData

'            #Region "#MiniMap"
            ' Create a mini map and data for it.
            Dim miniMap As New MiniMap()
            miniMap.Alignment = MiniMapAlignment.BottomLeft
            miniMap.Layers.AddRange(New MiniMapLayerBase() { _
                New MiniMapImageTilesLayer() With { _
                    .DataProvider = New BingMapDataProvider() With {.BingKey = "YOUR_BING_MAPS_KEY_HERE"} _
                }, _
                _
                New MiniMapVectorItemsLayer() With {.Data = CreateMiniMapAdapter(data)} _
            })
            map.MiniMap = miniMap
'            #End Region ' #MiniMap
        End Sub

        #Region "#CreateAdapter"
        ' Creates an adapter for the map's vector layer.
        Private Function CreateAdapter(ByVal source As Object) As IMapDataAdapter
            Dim adapter As New ListSourceDataAdapter()

            adapter.DataSource = source

            adapter.Mappings.Latitude = "Latitude"
            adapter.Mappings.Longitude = "Longitude"

            adapter.AttributeMappings.Add(New MapItemAttributeMapping() With { _
                .Member = "Name", _
                .Name = "Name" _
            })
            adapter.AttributeMappings.Add(New MapItemAttributeMapping() With { _
                .Member = "Year", _
                .Name = "Year" _
            })
            adapter.AttributeMappings.Add(New MapItemAttributeMapping() With { _
                .Member = "Description", _
                .Name = "Description" _
            })

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
        Private Function LoadData(ByVal path As String) As List(Of ShipwreckData)
            Return XDocument.Load(path).Element("Ships").Elements("Ship").Select(Function(e) New ShipwreckData(year:= Convert.ToInt32(e.Element("Year").Value, CultureInfo.InvariantCulture), name:= e.Element("Name").Value, description:= e.Element("Description").Value, latitude:= Convert.ToDouble(e.Element("Latitude").Value, CultureInfo.InvariantCulture), longitude:= Convert.ToDouble(e.Element("Longitude").Value, CultureInfo.InvariantCulture))).ToList()
        End Function

        Public Class ShipwreckData
            Public ReadOnly Property Year() As Integer
            Public ReadOnly Property Name() As String
            Public ReadOnly Property Description() As String
            Public ReadOnly Property Latitude() As Double
            Public ReadOnly Property Longitude() As Double

            Public Sub New(ByVal year As Integer, ByVal name As String, ByVal description As String, ByVal latitude As Double, ByVal longitude As Double)
                Me.Year = year
                Me.Name = name
                Me.Description = description
                Me.Latitude = latitude
                Me.Longitude = longitude
            End Sub
        End Class
        #End Region ' #LoadData
    End Class
End Namespace