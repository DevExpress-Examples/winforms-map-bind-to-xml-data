Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports DevExpress.Utils
Imports DevExpress.XtraMap

Namespace DataMapping
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Const xmlFilePath As String = "..\..\Data\Ships.xml"
		Private Const imageFilePath As String = "..\..\Image\Ship.png"
		Private Const bingKey As String = "INSERT_YOUR_BING_KEY"

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' Create a map control with initial settings.
			Dim map As New MapControl() With {.ZoomLevel = 6, .Dock = DockStyle.Fill, .CenterPoint = New GeoPoint(-35, 145)}
			Me.Controls.Add(map)

			' Create a layer to load image tiles from MS Bing.
			Dim tileLayer As New ImageTilesLayer()
			Dim bingProvider As New BingMapDataProvider() With {.BingKey = bingKey}
			tileLayer.DataProvider = bingProvider
			map.Layers.Add(tileLayer)

			' Create a layer to display vector items.
			Dim itemsLayer As New VectorItemsLayer()
			map.Layers.Add(itemsLayer)

			' Create a data adapter and load data to it.
			Dim dataAdapter As New ListSourceDataAdapter()
			dataAdapter.DataSource = LoadShipsFromXML(xmlFilePath)
			itemsLayer.Data = dataAdapter

			' Specify main mappings and additional mappings.
			dataAdapter.Mappings.Latitude = "Latitude"
			dataAdapter.Mappings.Longitude = "Longitude"
			dataAdapter.AttributeMappings.Add(New MapItemAttributeMapping() With {.Member = "Name", .Name = "Name"})
			dataAdapter.AttributeMappings.Add(New MapItemAttributeMapping() With {.Member = "Year", .Name = "Year"})
			dataAdapter.AttributeMappings.Add(New MapItemAttributeMapping() With {.Member = "Description", .Name = "Desc"})

			' Specify an image for generated vector items.
			map.ImageList = GetShipImage(imageFilePath)
			itemsLayer.ItemImageIndex = 0

			' Customize tooltips.
			map.ToolTipController = New ToolTipController() With {.AllowHtmlText = True}
			itemsLayer.ToolTipPattern = "<b>{Name} ({Year})</b> " & Constants.vbCrLf & "{Desc}"
		End Sub

		Private Function GetShipImage(ByVal filePath As String) As ImageCollection
			Dim imageCollection As New ImageCollection()
			Dim image As New Bitmap(imageFilePath)
			imageCollection.ImageSize = New Size(40, 40)
			imageCollection.Images.Add(image)

			Return imageCollection
		End Function

		Private Function LoadShipsFromXML(ByVal filePath As String) As List(Of ShipInfo)
			Dim ships As New List(Of ShipInfo)()

			' Load an XML document from the specified file path.
			Dim document As XDocument = XDocument.Load(filePath)
			If document IsNot Nothing Then
				For Each element As XElement In document.Element("Ships").Elements()
					' Load ShipInfo values and add them to the list.
					Dim latitude As Double = Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture)
					Dim longitude As Double = Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture)
					Dim name As String = element.Element("Name").Value
					Dim description As String = element.Element("Description").Value
					Dim year As String = element.Element("Year").Value

					ships.Add(New ShipInfo() With {.Latitude = latitude, .Longitude = longitude, .Description = description, .Name = name, .Year = year})
				Next element
			End If
			Return ships
		End Function
	End Class


	Public Class ShipInfo
		Private privateLatitude As Double
		Public Property Latitude() As Double
			Get
				Return privateLatitude
			End Get
			Set(ByVal value As Double)
				privateLatitude = value
			End Set
		End Property
		Private privateLongitude As Double
		Public Property Longitude() As Double
			Get
				Return privateLongitude
			End Get
			Set(ByVal value As Double)
				privateLongitude = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateYear As String
		Public Property Year() As String
			Get
				Return privateYear
			End Get
			Set(ByVal value As String)
				privateYear = value
			End Set
		End Property
		Private privateDescription As String
		Public Property Description() As String
			Get
				Return privateDescription
			End Get
			Set(ByVal value As String)
				privateDescription = value
			End Set
		End Property
	End Class

End Namespace

