using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using DevExpress.XtraMap;

namespace WinForms_MapControl_ListAdapter {
    public partial class Form1 : DevExpress.XtraEditors.XtraForm {
        public Form1() {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap() {
            object data = LoadData(@"..\..\Data\Ships.xml");

            #region #VectorData
            // Create a vector layer.
            map.Layers.Add(new VectorItemsLayer() {
                Data = CreateAdapter(data),
                ToolTipPattern = "<b>{Name} ({Year})</b> \r\n{Description}",
                ItemImageIndex = 0
            });
            #endregion #VectorData

            #region #MiniMap
            // Create a mini map and data for it.         
            MiniMap miniMap = new MiniMap() {
                Alignment = MiniMapAlignment.BottomLeft
            };
            miniMap.Layers.Add(new MiniMapImageTilesLayer() {
                DataProvider = new BingMapDataProvider() { BingKey = "YOUR_BING_MAPS_KEY_HERE" }
            });
            miniMap.Layers.Add(new MiniMapVectorItemsLayer() { Data = CreateMiniMapAdapter(data) });
            map.MiniMap = miniMap;
            #endregion #MiniMap
        }

        #region #CreateAdapter
        // Creates an adapter for the map's vector layer.
        private IMapDataAdapter CreateAdapter(object source) {
            ListSourceDataAdapter adapter = new ListSourceDataAdapter();

            adapter.DataSource = source;

            adapter.Mappings.Latitude = "Latitude";
            adapter.Mappings.Longitude = "Longitude";

            adapter.AttributeMappings.Add(new MapItemAttributeMapping() { Member = "Name", Name = "Name" });
            adapter.AttributeMappings.Add(new MapItemAttributeMapping() { Member = "Year", Name = "Year" });
            adapter.AttributeMappings.Add(new MapItemAttributeMapping() { Member = "Description", Name = "Description" });

            return adapter;
        }
        #endregion #CreateAdapter

        #region #CreateMiniMapAdapter
        // Creates an adapter for the mini map's vector layer.
        private IMapDataAdapter CreateMiniMapAdapter(object source) {
            ListSourceDataAdapter adapter = new ListSourceDataAdapter();

            adapter.DataSource = source;

            adapter.Mappings.Latitude = "Latitude";
            adapter.Mappings.Longitude = "Longitude";

            adapter.PropertyMappings.Add(new MapItemFillMapping() { DefaultValue = Color.Red });
            adapter.PropertyMappings.Add(new MapItemStrokeMapping() { DefaultValue = Color.White });
            adapter.PropertyMappings.Add(new MapItemStrokeWidthMapping() { DefaultValue = 2 });
            adapter.PropertyMappings.Add(new MapDotSizeMapping() { DefaultValue = 8 });

            adapter.DefaultMapItemType = MapItemType.Dot;

            return adapter;
        }
        #endregion #CreateMiniMapAdapter

        #region #LoadData
        // Loads data from a XML file.
        private List<ShipwreckData> LoadData(string path) {
            return XDocument.Load(path).Element("Ships").Elements("Ship")
                .Select(e => new ShipwreckData(
                    year: Convert.ToInt32(e.Element("Year").Value, CultureInfo.InvariantCulture),
                    name: e.Element("Name").Value,
                    description: e.Element("Description").Value,
                    latitude: Convert.ToDouble(e.Element("Latitude").Value, CultureInfo.InvariantCulture),
                    longitude: Convert.ToDouble(e.Element("Longitude").Value, CultureInfo.InvariantCulture)
                ))
                .ToList();
        }

        public class ShipwreckData {
            public int Year { get; }
            public string Name { get; }
            public string Description { get; }
            public double Latitude { get; }
            public double Longitude { get; }

            public ShipwreckData(int year, string name, string description, double latitude, double longitude) {
                this.Year = year;
                this.Name = name;
                this.Description = description;
                this.Latitude = latitude;
                this.Longitude = longitude;
            }
        }
        #endregion #LoadData
    }
}