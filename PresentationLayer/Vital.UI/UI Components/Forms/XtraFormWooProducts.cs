using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Vital.UI.UI_Components.Forms
{
    public partial class XtraFormWooProducts : DevExpress.XtraEditors.XtraForm
    {
        private BindingList<WooProductView> _productsBindingList = new BindingList<WooProductView>();

        public XtraFormWooProducts()
        {
            InitializeComponent();
        }

        private async void XtraFormWooProducts_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync(string search = null)
        {
            ToggleLoading(true);
            try
            {
                var products = await FetchProductsAsync(search);
                var views = products.Select(p => new WooProductView
                {
                    Id = p.id,
                    Name = p.name,
                    Price = string.IsNullOrWhiteSpace(p.price) ? p.regular_price : p.price,
                    StockStatus = p.stock_status,
                    DescriptionHtml = p.short_description ?? p.description,
                    ImageUrl = p.images != null && p.images.Count > 0 ? p.images[0].src : null
                }).ToList();

                _productsBindingList = new BindingList<WooProductView>(views);
                gridControlProducts.DataSource = _productsBindingList;

                // Preload images in background
                await Task.Run(() =>
                {
                    foreach (var v in views)
                    {
                        if (!string.IsNullOrWhiteSpace(v.ImageUrl))
                        {
                            try
                            {
                                using (var wc = new WebClient())
                                {
                                    var bytes = wc.DownloadData(v.ImageUrl);
                                    using (var ms = new MemoryStream(bytes))
                                    {
                                        v.Image = Image.FromStream(ms);
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                });

                gridViewProducts.RefreshData();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this, ex.Message, "Load Products Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleLoading(false);
            }
        }

        private void ToggleLoading(bool isLoading)
        {
            this.Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            simpleButtonRefresh.Enabled = !isLoading;
            textEditSearch.Enabled = !isLoading;
        }

        private async Task<List<WooProduct>> FetchProductsAsync(string search)
        {
            var baseUrl = "https://standardenzyme.com/wp-json/wc/v3/products";
            var key = "ck_7581613c01e7c2c68d469fc3b3d727525833d48b";
            var secret = "cs_64c7ec8894c6b9bc4091468b524d33254c4050c6";

            var url = baseUrl + "?per_page=50&consumer_key=" + Uri.EscapeDataString(key) + "&consumer_secret=" + Uri.EscapeDataString(secret);
            if (!string.IsNullOrWhiteSpace(search))
            {
                url += "&search=" + Uri.EscapeDataString(search);
            }

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/json";

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var json = await reader.ReadToEndAsync();
                var list = JsonConvert.DeserializeObject<List<WooProduct>>(json);
                return list ?? new List<WooProduct>();
            }
        }

        private void gridViewProducts_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var product = gridViewProducts.GetFocusedRow() as WooProductView;
            if (product == null) return;

            pictureEditImage.Image = product.Image;
            memoEditDescription.Text = StripHtml(product.DescriptionHtml);
        }

        private static string StripHtml(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            try
            {
                return System.Text.RegularExpressions.Regex.Replace(html, "<.*?>", string.Empty).Trim();
            }
            catch { return html; }
        }

        private async void simpleButtonRefresh_Click(object sender, EventArgs e)
        {
            await LoadProductsAsync(textEditSearch.Text);
        }
    }

    public class WooProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string StockStatus { get; set; }
        public string DescriptionHtml { get; set; }
        public string ImageUrl { get; set; }
        public Image Image { get; set; }
    }

    // Minimal models for deserialization
    public class WooProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string regular_price { get; set; }
        public string stock_status { get; set; }
        public string description { get; set; }
        public string short_description { get; set; }
        public List<WooImage> images { get; set; }
    }

    public class WooImage
    {
        public int id { get; set; }
        public string src { get; set; }
        public string name { get; set; }
        public string alt { get; set; }
    }
}

