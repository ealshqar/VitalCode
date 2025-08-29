namespace Vital.UI.UI_Components.Forms
{
    partial class XtraFormWooProducts
    {
        private DevExpress.XtraGrid.GridControl gridControlProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewProducts;
        private DevExpress.XtraEditors.PictureEdit pictureEditImage;
        private DevExpress.XtraEditors.MemoEdit memoEditDescription;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRefresh;
        private DevExpress.XtraEditors.TextEdit textEditSearch;

        private void InitializeComponent()
        {
            this.gridControlProducts = new DevExpress.XtraGrid.GridControl();
            this.gridViewProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pictureEditImage = new DevExpress.XtraEditors.PictureEdit();
            this.memoEditDescription = new DevExpress.XtraEditors.MemoEdit();
            this.simpleButtonRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.textEditSearch = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlProducts
            // 
            this.gridControlProducts.Location = new System.Drawing.Point(12, 48);
            this.gridControlProducts.MainView = this.gridViewProducts;
            this.gridControlProducts.Name = "gridControlProducts";
            this.gridControlProducts.Size = new System.Drawing.Size(580, 520);
            this.gridControlProducts.TabIndex = 0;
            this.gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewProducts});
            // 
            // gridViewProducts
            // 
            this.gridViewProducts.GridControl = this.gridControlProducts;
            this.gridViewProducts.Name = "gridViewProducts";
            this.gridViewProducts.OptionsBehavior.Editable = false;
            this.gridViewProducts.OptionsView.ShowGroupPanel = false;
            this.gridViewProducts.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewProducts_FocusedRowChanged);
            // 
            // pictureEditImage
            // 
            this.pictureEditImage.Location = new System.Drawing.Point(610, 48);
            this.pictureEditImage.Name = "pictureEditImage";
            this.pictureEditImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEditImage.Size = new System.Drawing.Size(360, 240);
            this.pictureEditImage.TabIndex = 1;
            // 
            // memoEditDescription
            // 
            this.memoEditDescription.Location = new System.Drawing.Point(610, 300);
            this.memoEditDescription.Name = "memoEditDescription";
            this.memoEditDescription.Properties.ReadOnly = true;
            this.memoEditDescription.Size = new System.Drawing.Size(360, 268);
            this.memoEditDescription.TabIndex = 2;
            // 
            // simpleButtonRefresh
            // 
            this.simpleButtonRefresh.Location = new System.Drawing.Point(497, 12);
            this.simpleButtonRefresh.Name = "simpleButtonRefresh";
            this.simpleButtonRefresh.Size = new System.Drawing.Size(95, 28);
            this.simpleButtonRefresh.TabIndex = 3;
            this.simpleButtonRefresh.Text = "Refresh";
            this.simpleButtonRefresh.Click += new System.EventHandler(this.simpleButtonRefresh_Click);
            // 
            // textEditSearch
            // 
            this.textEditSearch.Location = new System.Drawing.Point(12, 14);
            this.textEditSearch.Name = "textEditSearch";
            this.textEditSearch.Size = new System.Drawing.Size(479, 22);
            this.textEditSearch.TabIndex = 4;
            // 
            // XtraFormWooProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 580);
            this.Controls.Add(this.textEditSearch);
            this.Controls.Add(this.simpleButtonRefresh);
            this.Controls.Add(this.memoEditDescription);
            this.Controls.Add(this.pictureEditImage);
            this.Controls.Add(this.gridControlProducts);
            this.Name = "XtraFormWooProducts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WooCommerce Products";
            this.Load += new System.EventHandler(this.XtraFormWooProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSearch.Properties)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

