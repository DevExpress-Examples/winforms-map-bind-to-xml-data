namespace WinForms_MapControl_ListAdapter {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            DevExpress.XtraMap.ColorListLegend colorListLegend1 = new DevExpress.XtraMap.ColorListLegend();
            DevExpress.XtraMap.ColorLegendItem colorLegendItem1 = new DevExpress.XtraMap.ColorLegendItem();
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.map = new DevExpress.XtraMap.MapControl();
            this.imageLayer1 = new DevExpress.XtraMap.ImageLayer();
            this.azureMapDataProvider1 = new DevExpress.XtraMap.AzureMapDataProvider();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.map)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection
            // 
            this.imageCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "Ship.png");
            // 
            // map
            // 
            this.map.CenterPoint = new DevExpress.XtraMap.GeoPoint(-37.2D, 143.2D);
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.ImageList = this.imageCollection;
            this.map.Layers.Add(this.imageLayer1);
            colorLegendItem1.ImageIndex = 0;
            colorLegendItem1.Text = "Shipwreck";
            colorListLegend1.CustomItems.Add(colorLegendItem1);
            colorListLegend1.Header = "";
            colorListLegend1.ImageList = this.imageCollection;
            this.map.Legends.Add(colorListLegend1);
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(1624, 1065);
            this.map.TabIndex = 0;
            this.map.ToolTipController = this.toolTipController;
            this.map.ZoomLevel = 5D;
            this.imageLayer1.DataProvider = this.azureMapDataProvider1;
            this.azureMapDataProvider1.AzureKey = "YOUR azure MAPS KEY";
            this.azureMapDataProvider1.Tileset = DevExpress.XtraMap.AzureTileset.BaseRoad;
            // 
            // toolTipController
            // 
            this.toolTipController.AllowHtmlText = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1624, 1065);
            this.Controls.Add(this.map);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.map)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraMap.MapControl map;
        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraMap.ImageLayer imageLayer1;
        private DevExpress.XtraMap.AzureMapDataProvider azureMapDataProvider1;
        private DevExpress.Utils.ImageCollection imageCollection;
    }
}

