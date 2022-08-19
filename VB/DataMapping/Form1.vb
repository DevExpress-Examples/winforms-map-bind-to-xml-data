Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports DevExpress.Utils
Imports DevExpress.XtraMap

Namespace DataMapping

    Public Partial Class Form1
        Inherits System.Windows.Forms.Form

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Const xmlFilePath As String = "..\..\Data\Ships.xml"

        Const imageFilePath As String = "..\..\Image\Ship.png"

        Const bingKey As String = "INSERT_YOUR_BING_KEY"

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Create a map control with initial settings.
            Dim map As DevExpress.XtraMap.MapControl = New DevExpress.XtraMap.MapControl() With {.ZoomLevel = 6, .Dock = System.Windows.Forms.DockStyle.Fill, .CenterPoint = New DevExpress.XtraMap.GeoPoint(-35, 145)}
            Me.Controls.Add(map)
            ' Create a layer to load image tiles from MS Bing.
            Dim tileLayer As DevExpress.XtraMap.ImageTilesLayer = New DevExpress.XtraMap.ImageTilesLayer()
            Dim bingProvider As DevExpress.XtraMap.BingMapDataProvider = New DevExpress.XtraMap.BingMapDataProvider() With {.BingKey = DataMapping.Form1.bingKey}
            tileLayer.DataProvider = bingProvider
            map.Layers.Add(tileLayer)
            ' Create a layer to display vector items.
            Dim itemsLayer As DevExpress.XtraMap.VectorItemsLayer = New DevExpress.XtraMap.VectorItemsLayer()
            map.Layers.Add(itemsLayer)
            ' Create a data adapter and load data to it.
            Dim dataAdapter As DevExpress.XtraMap.ListSourceDataAdapter = New DevExpress.XtraMap.ListSourceDataAdapter()
            dataAdapter.DataSource = Me.LoadShipsFromXML(DataMapping.Form1.xmlFilePath)
            itemsLayer.Data = dataAdapter
            ' Specify main mappings and additional mappings.
            dataAdapter.Mappings.Latitude = "Latitude"
            dataAdapter.Mappings.Longitude = "Longitude"
            dataAdapter.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Name", .Name = "Name"})
            dataAdapter.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Year", .Name = "Year"})
            dataAdapter.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Description", .Name = "Desc"})
            ' Specify an image for generated vector items.
            map.ImageList = Me.GetShipImage(DataMapping.Form1.imageFilePath)
            itemsLayer.ItemImageIndex = 0
            ' Customize tooltips.
            map.ToolTipController = New DevExpress.Utils.ToolTipController() With {.AllowHtmlText = True}
            itemsLayer.ToolTipPattern = "<b>{Name} ({Year})</b> " & Global.Microsoft.VisualBasic.Constants.vbCrLf & "{Desc}"
        End Sub

        Private Function GetShipImage(ByVal filePath As String) As ImageCollection
            Dim imageCollection As DevExpress.Utils.ImageCollection = New DevExpress.Utils.ImageCollection()
            Dim image As System.Drawing.Bitmap = New System.Drawing.Bitmap(DataMapping.Form1.imageFilePath)
            imageCollection.ImageSize = New System.Drawing.Size(40, 40)
            imageCollection.Images.Add(image)
            Return imageCollection
        End Function

        Private Function LoadShipsFromXML(ByVal filePath As String) As List(Of DataMapping.ShipInfo)
            Dim ships As System.Collections.Generic.List(Of DataMapping.ShipInfo) = New System.Collections.Generic.List(Of DataMapping.ShipInfo)()
            ' Load an XML document from the specified file path.
            Dim document As System.Xml.Linq.XDocument = System.Xml.Linq.XDocument.Load(filePath)
            If document IsNot Nothing Then
                For Each element As System.Xml.Linq.XElement In document.Element(CType(("Ships"), System.Xml.Linq.XName)).Elements()
                    ' Load ShipInfo values and add them to the list.
                    Dim latitude As Double = System.Convert.ToDouble(element.Element(CType(("Latitude"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture)
                    Dim longitude As Double = System.Convert.ToDouble(element.Element(CType(("Longitude"), System.Xml.Linq.XName)).Value, System.Globalization.CultureInfo.InvariantCulture)
                    Dim name As String = element.Element(CType(("Name"), System.Xml.Linq.XName)).Value
                    Dim description As String = element.Element(CType(("Description"), System.Xml.Linq.XName)).Value
                    Dim year As String = element.Element(CType(("Year"), System.Xml.Linq.XName)).Value
                    ships.Add(New DataMapping.ShipInfo() With {.Latitude = latitude, .Longitude = longitude, .Description = description, .Name = name, .Year = year})
                Next
            End If

            Return ships
        End Function
    End Class

    Public Class ShipInfo

        Public Property Latitude As Double

        Public Property Longitude As Double

        Public Property Name As String

        Public Property Year As String

        Public Property Description As String
    End Class
End Namespace
