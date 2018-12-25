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
            DevExpress.XtraMap.ColorListLegend colorListLegend1 = new DevExpress.XtraMap.ColorListLegend();
            DevExpress.XtraMap.ColorLegendItem colorLegendItem1 = new DevExpress.XtraMap.ColorLegendItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.map = new DevExpress.XtraMap.MapControl();
            this.imageLayer1 = new DevExpress.XtraMap.ImageLayer();
            this.bingMapDataProvider1 = new DevExpress.XtraMap.BingMapDataProvider();
            this.toolTipController = new DevExpress.Utils.ToolTipController(this.components);
            this.imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.map)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).BeginInit();
            this.SuspendLayout();
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
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(804, 554);
            this.map.TabIndex = 0;
            this.map.ToolTipController = this.toolTipController;
            this.map.ZoomLevel = 5D;
            this.imageLayer1.DataProvider = this.bingMapDataProvider1;
            this.bingMapDataProvider1.BingKey = "YOUR BING MAPS KEY";
            this.bingMapDataProvider1.Kind = DevExpress.XtraMap.BingMapKind.Area;
            // 
            // toolTipController
            // 
            this.toolTipController.AllowHtmlText = true;
            // 
            // imageCollection
            // 
            this.imageCollection.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection.ImageStream")));
            this.imageCollection.Images.SetKeyName(0, "Ship.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 554);
            this.Controls.Add(this.map);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.map)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraMap.MapControl map;
        private DevExpress.Utils.ToolTipController toolTipController;
        private DevExpress.XtraMap.ImageLayer imageLayer1;
        private DevExpress.XtraMap.BingMapDataProvider bingMapDataProvider1;
        private DevExpress.Utils.ImageCollection imageCollection;
    }
}

