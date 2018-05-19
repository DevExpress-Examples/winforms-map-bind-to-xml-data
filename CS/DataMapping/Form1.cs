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

        const string xmlFilePath = @"..\..\Data\Ships.xml";
        const string imageFilePath = @"..\..\Image\Ship.png";
        const string bingKey = "INSERT_YOUR_BING_KEY";

        private void Form1_Load(object sender, EventArgs e) {
            // Create a map control with initial settings.
            MapControl map = new MapControl() {
                ZoomLevel = 6, Dock = DockStyle.Fill,
                CenterPoint = new GeoPoint(-35, 145)
            };
            this.Controls.Add(map);

            // Create a layer to load image tiles from MS Bing.
            ImageTilesLayer tileLayer = new ImageTilesLayer();
            BingMapDataProvider bingProvider = new BingMapDataProvider() { BingKey = bingKey };
            tileLayer.DataProvider = bingProvider;
            map.Layers.Add(tileLayer);

            // Create a layer to display vector items.
            VectorItemsLayer itemsLayer = new VectorItemsLayer();
            map.Layers.Add(itemsLayer);

            // Create a data adapter and load data to it.
            ListSourceDataAdapter dataAdapter = new ListSourceDataAdapter();
            dataAdapter.DataSource = LoadShipsFromXML(xmlFilePath);
            itemsLayer.Data = dataAdapter;

            // Specify main mappings and additional mappings.
            dataAdapter.Mappings.Latitude = "Latitude";
            dataAdapter.Mappings.Longitude = "Longitude";
            dataAdapter.AttributeMappings.Add(
                new MapItemAttributeMapping() { Member = "Name", Name = "Name" });
            dataAdapter.AttributeMappings.Add(
                new MapItemAttributeMapping() { Member = "Year", Name = "Year" });
            dataAdapter.AttributeMappings.Add(
                new MapItemAttributeMapping() { Member = "Description", Name = "Desc" });

            // Specify an image for generated vector items.
            map.ImageList = GetShipImage(imageFilePath);
            itemsLayer.ItemImageIndex = 0;

            // Customize tooltips.
            map.ToolTipController = new ToolTipController() { AllowHtmlText = true };
            itemsLayer.ToolTipPattern = "<b>{Name} ({Year})</b> \r\n{Desc}";
        }

        private ImageCollection GetShipImage(string filePath) {
            ImageCollection imageCollection = new ImageCollection();
            Bitmap image = new Bitmap(imageFilePath);
            imageCollection.ImageSize = new Size(40, 40);
            imageCollection.Images.Add(image);

            return imageCollection;
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

                    ships.Add(new ShipInfo() {
                        Latitude = latitude,
                        Longitude = longitude,
                        Description = description,
                        Name = name,
                        Year = year
                    });
                }
            }
            return ships;
        }
    }


    public class ShipInfo {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
    }

}

