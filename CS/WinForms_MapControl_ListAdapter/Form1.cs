using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraMap;

namespace WinForms_MapControl_ListAdapter {
    public partial class Form1 : Form {
        const string bingKey = "YOUR_BING_MAPS_KEY_HERE";
        const string xmlFilepath = @"..\..\Data\Ships.xml";
        const string imageFilepath = @"..\..\Image\Ship.png";

        public Form1() {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap() {
            #region #MapPreparation
            object data = LoadData(xmlFilepath);
            
            // Create a map and data for it.
            MapControl map = new MapControl() {
                CenterPoint = new GeoPoint(-37.2, 143.2),
                ZoomLevel = 5,
                Dock = DockStyle.Fill,
                ToolTipController = new ToolTipController() { AllowHtmlText = true },
                ImageList = LoadImage(imageFilepath)
            };
            this.Controls.Add(map);
            #endregion #MapPreparation

            map.Layers.Add(new ImageTilesLayer() {
                DataProvider = new BingMapDataProvider() { BingKey = bingKey }
            });
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
                DataProvider = new BingMapDataProvider() { BingKey = bingKey }
            });
            miniMap.Layers.Add(new MiniMapVectorItemsLayer() { Data = CreateMiniMapAdapter(data) });
            map.MiniMap = miniMap;
            #endregion #MiniMap

            #region #Legend
            //Create a Legend containing images.
            ColorListLegend legend = new ColorListLegend();
            legend.ImageList = map.ImageList;
            legend.CustomItems.Add(new ColorLegendItem() { ImageIndex = 0, Text = "Shipwreck" });
            map.Legends.Add(legend);
            #endregion #Legend
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
        private DataTable LoadData(string path) {
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            DataTable table = ds.Tables[0];
            return table;
        }
        #endregion #LoadData

        #region #LoadImage
        // Loads an image to an image collection.
        private ImageCollection LoadImage(string path) {
            ImageCollection imageCollection = new ImageCollection();
            Bitmap image = new Bitmap(path);
            imageCollection.ImageSize = new Size(50, 50);
            imageCollection.Images.Add(image);
            return imageCollection;
        }
        #endregion #LoadImage
    }

}