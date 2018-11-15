<!-- default file list -->
*Files to look at*:

* **[Form1.cs](./CS/WinForms_MapControl_ListAdapter/Form1.cs) (VB: [Form1.vb](./VB/WinForms_MapControl_ListAdapter/Form1.vb))**
<!-- default file list end -->
# XtraMap Getting Started - Lesson 3 - Binding a map control to data loaded from XML


<p>This example illustrates how to <a href="https://documentation.devexpress.com/#WindowsForms/CustomDocument15359">bind</a> a Map control <a href="https://documentation.devexpress.com/#WindowsForms/CustomDocument15359">to data</a>. This data stored in an external XML file, which contains information about wrecked ships, including ship coordinates.</p>
<p>In this example, the map control automatically generates ship images based on data from the datasource, along with a description for each image in a tooltip.</p>


<h3>Description</h3>

<p>&nbsp;To accomplish this task, do the following:</p>
<p>1. Create a <a href="https://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraMapListSourceDataAdaptertopic">DevExpress.XtraMap.ListSourceDataAdapter</a> object and assign it to the <a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapVectorItemsLayer_Datatopic">DevExpress.XtraMap.VectorItemsLayer.Data</a> property.</p>
<p>2. Create a data source (in this example, this is a data table object generated by the <strong>LoadData</strong> method) and assign it to the <a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapDataSourceAdapterBase_DataSourcetopic">DevExpress.XtraMap.DataSourceAdapterBase.DataSource</a> property of the data adapter.</p>
<p>3. To define names of data fields that contain information about latitude and longitude of vector items, specify appropriate values for <a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapMapItemMappingInfo_Latitudetopic">DevExpress.XtraMap.MapItemMappingInfo.Latitude</a> and <a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapMapItemMappingInfo_Longitudetopic">DevExpress.XtraMap.MapItemMappingInfo.Longitude</a> properties of the object, returned by the <a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapListSourceDataAdapter_Mappingstopic">DevExpress.XtraMap.ListSourceDataAdapter.Mappings</a> property.</p>
<p>4. After that, define names of other data fields that provide additional information for generated vector items. Note that these data field values are accessible via attributes - and so you should specify attribute mapping via the <a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapDataSourceAdapterBase_AttributeMappingstopic">DevExpress.XtraMap.DataSourceAdapterBase.AttributeMappings</a> object.</p>
<p>Also, this sample illustrates how to add a mini map using the <strong>MapControl.MiniMap</strong>&nbsp;property, how to&nbsp;create a legend containing images and&nbsp;how to customize tooltips via the <a href="https://documentation.devexpress.com/#WindowsForms/DevExpressXtraMapMapItemsLayerBase_ToolTipPatterntopic">DevExpress.XtraMap.MapItemsLayerBase.ToolTipPattern</a> property.</p>
<p>&nbsp;</p>
<p>Note that if you run this sample as is, you will get a warning message saying that the specified Bing Maps key is invalid. To learn more about Bing Map keys, please refer to the <a href="http://help.devexpress.com/#WindowsForms/CustomDocument15102">How to: Get a Bing Maps Key</a> tutorial.</p>

<br/>


