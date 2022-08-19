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

        Private itemsLayer As DevExpress.XtraMap.VectorItemsLayer

        Const xmlFilePath As String = "..\..\Data\Ships.xml"

        Const imageFilePath As String = "..\..\Image\Ship.png"

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Create a map control with initial settings and add it to the form.
            Dim map As DevExpress.XtraMap.MapControl = New DevExpress.XtraMap.MapControl() With {.ZoomLevel = 6, .Dock = System.Windows.Forms.DockStyle.Fill}
            map.CenterPoint = New DevExpress.XtraMap.GeoPoint(-35, 145)
            Me.Controls.Add(map)
            ' Create a layer to load image tiles from MS Bing.
            Dim tileLayer As DevExpress.XtraMap.ImageTilesLayer = New DevExpress.XtraMap.ImageTilesLayer()
            Dim bingProvider As DevExpress.XtraMap.BingMapDataProvider = New DevExpress.XtraMap.BingMapDataProvider()
            bingProvider.BingKey = "Insert Your Bing Key"
            tileLayer.DataProvider = bingProvider
            map.Layers.Add(tileLayer)
            ' Create a vector items layer.
            Me.itemsLayer = New DevExpress.XtraMap.VectorItemsLayer()
            ' Specify mappings for Latitude and Longitude coordinates.
            Me.itemsLayer.Mappings.Latitude = "Latitude"
            Me.itemsLayer.Mappings.Longitude = "Longitude"
            ' Specify attribute mappings for ship name, year and description.
            Me.itemsLayer.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Name", .Name = "Name"})
            Me.itemsLayer.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Year", .Name = "Year"})
            Me.itemsLayer.AttributeMappings.Add(New DevExpress.XtraMap.MapItemAttributeMapping() With {.Member = "Description", .Name = "Desc"})
            ' Specify a datasource. 
            Me.itemsLayer.DataSource = Me.LoadShipsFromXML(DataMapping.Form1.xmlFilePath)
            ' Specify an image for generated vector items.              
            Dim imageCollection As DevExpress.Utils.ImageCollection = New DevExpress.Utils.ImageCollection()
            Dim image As System.Drawing.Bitmap = New System.Drawing.Bitmap(DataMapping.Form1.imageFilePath)
            imageCollection.ImageSize = New System.Drawing.Size(40, 40)
            imageCollection.Images.Add(image)
            Me.itemsLayer.ItemImageIndex = 0
            map.ImageList = imageCollection
            ' Specify tooltip contents.
            map.ToolTipController = New DevExpress.Utils.ToolTipController() With {.AllowHtmlText = True}
            Me.itemsLayer.ToolTipPattern = "<b>{Name} ({Year})</b> " & Global.Microsoft.VisualBasic.Constants.vbCrLf & "{Desc}"
            ' Add a vector items layer to the map control.
            map.Layers.Add(Me.itemsLayer)
        End Sub

        Public Class ShipInfo

            Public Property Latitude As Double

            Public Property Longitude As Double

            Public Property Name As String

            Public Property Year As String

            Public Property Description As String
        End Class

        Private Function LoadShipsFromXML(ByVal filePath As String) As List(Of DataMapping.Form1.ShipInfo)
            Dim ships As System.Collections.Generic.List(Of DataMapping.Form1.ShipInfo) = New System.Collections.Generic.List(Of DataMapping.Form1.ShipInfo)()
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
                    ships.Add(New DataMapping.Form1.ShipInfo() With {.Latitude = latitude, .Longitude = longitude, .Description = description, .Name = name, .Year = year})
                Next
            End If

            Return ships
        End Function
    End Class
End Namespace
