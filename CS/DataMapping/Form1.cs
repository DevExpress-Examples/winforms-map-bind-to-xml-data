using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.Utils;
using DevExpress.XtraMap;

namespace DataMapping {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
        }

        VectorItemsLayer itemsLayer;
        const string xmlFilePath = @"..\..\Data\Ships.xml";
        const string imageFilePath = @"..\..\Image\Ship.png";


        private void Form1_Load(object sender, EventArgs e) {

            // Create a map control with initial settings and add it to the form.
            MapControl map = new MapControl() { ZoomLevel = 6, Dock = DockStyle.Fill };
            map.CenterPoint = new GeoPoint(-35, 145);
            this.Controls.Add(map);

            // Create a layer to load image tiles from MS Bing.
            ImageTilesLayer tileLayer = new ImageTilesLayer();
            BingMapDataProvider bingProvider = new BingMapDataProvider();
            bingProvider.BingKey = "Insert Your Bing Key";
            tileLayer.DataProvider = bingProvider;
            map.Layers.Add(tileLayer);

            // Create a vector items layer.
            itemsLayer = new VectorItemsLayer();

            // Specify mappings for Latitude and Longitude coordinates.
            itemsLayer.Mappings.Latitude = "Latitude";
            itemsLayer.Mappings.Longitude = "Longitude";

            // Specify attribute mappings for ship name, year and description.
            itemsLayer.AttributeMappings.Add(
                new MapItemAttributeMapping() { Member = "Name", Name = "Name" });
            itemsLayer.AttributeMappings.Add(
                new MapItemAttributeMapping() { Member = "Year", Name = "Year" });
            itemsLayer.AttributeMappings.Add(
                new MapItemAttributeMapping() { Member = "Description", Name = "Desc" });

            // Specify a datasource. 
            itemsLayer.DataSource = LoadShipsFromXML(xmlFilePath);

            // Specify an image for generated vector items.              
            ImageCollection imageCollection = new ImageCollection();
            Bitmap image = new Bitmap(imageFilePath);
            imageCollection.ImageSize = new Size(40, 40);
            imageCollection.Images.Add(image);
            itemsLayer.ItemImageIndex = 0;
            map.ImageList = imageCollection;

            // Specify tooltip contents.
            map.ToolTipController = new ToolTipController() { AllowHtmlText = true };
            itemsLayer.ToolTipPattern = "<b>{Name} ({Year})</b> \r\n{Desc}";

            // Add a vector items layer to the map control.
            map.Layers.Add(itemsLayer);
        }


        public class ShipInfo {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Name { get; set; }
            public string Year { get; set; }
            public string Description { get; set; }
        }

        private List<ShipInfo> LoadShipsFromXML(string filePath) {
            List<ShipInfo> ships = new List<ShipInfo>();

            // Load an XML document from the specified file path.
            XDocument document = XDocument.Load(filePath);
            if (document != null) {
                foreach (XElement element in document.Element("Ships").Elements()) {
                    // Load ShipInfo values and add them to the list.
                    double latitude = Convert.ToDouble(element.Element("Latitude").Value, CultureInfo.InvariantCulture);
                    double longitude = Convert.ToDouble(element.Element("Longitude").Value, CultureInfo.InvariantCulture);
                    string name = element.Element("Name").Value;
                    string description = element.Element("Description").Value;
                    string year = element.Element("Year").Value;

                    ships.Add(new ShipInfo() { Latitude = latitude, Longitude = longitude, Description = description, Name = name, Year = year });
                }
            }

            return ships;
        }

    }

}

