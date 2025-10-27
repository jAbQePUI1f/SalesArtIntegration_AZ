namespace SalesArtIntegration_AZ
{
    partial class DataIntegrationsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataIntegrationsForm));
            chk = new DataGridViewCheckBoxColumn();
            chckAll = new CheckBox();
            divider = new MaterialSkin.Controls.MaterialDivider();
            bttnLogs = new MaterialSkin.Controls.MaterialButton();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            materialCard3 = new MaterialSkin.Controls.MaterialCard();
            bttnSendProducts = new MaterialSkin.Controls.MaterialButton();
            bttnGetProducts = new MaterialSkin.Controls.MaterialButton();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialCard2 = new MaterialSkin.Controls.MaterialCard();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            bttnGetCustomers = new MaterialSkin.Controls.MaterialButton();
            bttnSendCustomer = new MaterialSkin.Controls.MaterialButton();
            exitToolStripMenuItem = new ToolStripMenuItem();
            collectionToolStripMenuItem = new ToolStripMenuItem();
            waybillToolStripMenuItem = new ToolStripMenuItem();
            menüToolStripMenuItem = new ToolStripMenuItem();
            anaMenüyeDönToolStripMenuItem = new ToolStripMenuItem();
            stripInvoice = new MenuStrip();
            dataGridInvoiceList = new DataGridView();
            materialCard1.SuspendLayout();
            materialCard3.SuspendLayout();
            materialCard2.SuspendLayout();
            stripInvoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInvoiceList).BeginInit();
            SuspendLayout();
            // 
            // chk
            // 
            chk.HeaderText = "";
            chk.MinimumWidth = 6;
            chk.Name = "chk";
            chk.Width = 80;
            // 
            // chckAll
            // 
            chckAll.AutoSize = true;
            chckAll.CheckAlign = ContentAlignment.MiddleRight;
            chckAll.Cursor = Cursors.Hand;
            chckAll.FlatStyle = FlatStyle.Popup;
            chckAll.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chckAll.Location = new Point(65, 258);
            chckAll.Name = "chckAll";
            chckAll.Size = new Size(13, 12);
            chckAll.TabIndex = 27;
            chckAll.UseVisualStyleBackColor = true;
            chckAll.CheckedChanged += chckAll_CheckedChanged;
            // 
            // divider
            // 
            divider.BackColor = SystemColors.GradientActiveCaption;
            divider.Depth = 0;
            divider.Location = new Point(-1, 228);
            divider.MouseState = MaterialSkin.MouseState.HOVER;
            divider.Name = "divider";
            divider.Size = new Size(1233, 19);
            divider.TabIndex = 23;
            // 
            // bttnLogs
            // 
            bttnLogs.AutoSize = false;
            bttnLogs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnLogs.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnLogs.Depth = 0;
            bttnLogs.Dock = DockStyle.Right;
            bttnLogs.FlatStyle = FlatStyle.Popup;
            bttnLogs.HighEmphasis = false;
            bttnLogs.Icon = (Image)resources.GetObject("bttnLogs.Icon");
            bttnLogs.Location = new Point(1140, 10);
            bttnLogs.Margin = new Padding(4);
            bttnLogs.MouseState = MaterialSkin.MouseState.HOVER;
            bttnLogs.Name = "bttnLogs";
            bttnLogs.NoAccentTextColor = Color.Empty;
            bttnLogs.Size = new Size(82, 174);
            bttnLogs.TabIndex = 17;
            bttnLogs.TabStop = false;
            bttnLogs.Text = "Logs";
            bttnLogs.TextAlign = ContentAlignment.MiddleLeft;
            bttnLogs.TextImageRelation = TextImageRelation.TextBeforeImage;
            bttnLogs.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            bttnLogs.UseAccentColor = false;
            bttnLogs.UseVisualStyleBackColor = true;
            bttnLogs.Click += bttnLogs_Click;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(materialCard3);
            materialCard1.Controls.Add(materialCard2);
            materialCard1.Controls.Add(bttnLogs);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(-2, 33);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(12, 10, 12, 10);
            materialCard1.Size = new Size(1234, 194);
            materialCard1.TabIndex = 25;
            // 
            // materialCard3
            // 
            materialCard3.BackColor = Color.FromArgb(255, 255, 255);
            materialCard3.Controls.Add(bttnSendProducts);
            materialCard3.Controls.Add(bttnGetProducts);
            materialCard3.Controls.Add(materialLabel2);
            materialCard3.Depth = 0;
            materialCard3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard3.Location = new Point(572, 10);
            materialCard3.Margin = new Padding(12, 10, 12, 10);
            materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard3.Name = "materialCard3";
            materialCard3.Padding = new Padding(12, 10, 12, 10);
            materialCard3.Size = new Size(532, 174);
            materialCard3.TabIndex = 19;
            // 
            // bttnSendProducts
            // 
            bttnSendProducts.AutoSize = false;
            bttnSendProducts.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnSendProducts.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Normal;
            bttnSendProducts.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnSendProducts.Depth = 0;
            bttnSendProducts.HighEmphasis = true;
            bttnSendProducts.Icon = null;
            bttnSendProducts.Location = new Point(320, 126);
            bttnSendProducts.Margin = new Padding(4, 6, 4, 6);
            bttnSendProducts.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendProducts.Name = "bttnSendProducts";
            bttnSendProducts.NoAccentTextColor = Color.Empty;
            bttnSendProducts.Size = new Size(204, 42);
            bttnSendProducts.TabIndex = 8;
            bttnSendProducts.Text = "Ürünleri Aktar";
            bttnSendProducts.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnSendProducts.UseAccentColor = false;
            bttnSendProducts.UseVisualStyleBackColor = true;
            bttnSendProducts.Click += bttnSendProducts_Click;
            // 
            // bttnGetProducts
            // 
            bttnGetProducts.AutoSize = false;
            bttnGetProducts.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnGetProducts.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Normal;
            bttnGetProducts.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnGetProducts.Depth = 0;
            bttnGetProducts.HighEmphasis = true;
            bttnGetProducts.Icon = null;
            bttnGetProducts.Location = new Point(7, 126);
            bttnGetProducts.Margin = new Padding(4, 6, 4, 6);
            bttnGetProducts.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetProducts.Name = "bttnGetProducts";
            bttnGetProducts.NoAccentTextColor = Color.Empty;
            bttnGetProducts.Size = new Size(204, 42);
            bttnGetProducts.TabIndex = 2;
            bttnGetProducts.Text = "Ürünleri Getir";
            bttnGetProducts.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnGetProducts.UseAccentColor = false;
            bttnGetProducts.UseVisualStyleBackColor = true;
            bttnGetProducts.Click += bttnGetProducts_Click;
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            materialLabel2.Location = new Point(0, 0);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(123, 24);
            materialLabel2.TabIndex = 1;
            materialLabel2.Text = "Ürün Aktarımı";
            // 
            // materialCard2
            // 
            materialCard2.BackColor = Color.FromArgb(255, 255, 255);
            materialCard2.Controls.Add(materialLabel1);
            materialCard2.Controls.Add(bttnGetCustomers);
            materialCard2.Controls.Add(bttnSendCustomer);
            materialCard2.Depth = 0;
            materialCard2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard2.Location = new Point(14, 10);
            materialCard2.Margin = new Padding(12, 10, 12, 10);
            materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard2.Name = "materialCard2";
            materialCard2.Padding = new Padding(12, 10, 12, 10);
            materialCard2.Size = new Size(549, 174);
            materialCard2.TabIndex = 18;
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            materialLabel1.Location = new Point(0, -1);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(150, 24);
            materialLabel1.TabIndex = 0;
            materialLabel1.Text = "Müşteri Aktarımı";
            // 
            // bttnGetCustomers
            // 
            bttnGetCustomers.AutoSize = false;
            bttnGetCustomers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnGetCustomers.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Normal;
            bttnGetCustomers.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnGetCustomers.Depth = 0;
            bttnGetCustomers.HighEmphasis = true;
            bttnGetCustomers.Icon = null;
            bttnGetCustomers.Location = new Point(11, 126);
            bttnGetCustomers.Margin = new Padding(4, 6, 4, 6);
            bttnGetCustomers.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetCustomers.Name = "bttnGetCustomers";
            bttnGetCustomers.NoAccentTextColor = Color.Empty;
            bttnGetCustomers.Size = new Size(220, 42);
            bttnGetCustomers.TabIndex = 0;
            bttnGetCustomers.Text = "Müşterileri Getir";
            bttnGetCustomers.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnGetCustomers.UseAccentColor = false;
            bttnGetCustomers.UseVisualStyleBackColor = true;
            bttnGetCustomers.Click += bttnGetCustomers_Click;
            // 
            // bttnSendCustomer
            // 
            bttnSendCustomer.AutoSize = false;
            bttnSendCustomer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnSendCustomer.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Normal;
            bttnSendCustomer.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnSendCustomer.Depth = 0;
            bttnSendCustomer.HighEmphasis = true;
            bttnSendCustomer.Icon = null;
            bttnSendCustomer.Location = new Point(322, 126);
            bttnSendCustomer.Margin = new Padding(4, 6, 4, 6);
            bttnSendCustomer.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendCustomer.Name = "bttnSendCustomer";
            bttnSendCustomer.NoAccentTextColor = Color.Empty;
            bttnSendCustomer.Size = new Size(220, 42);
            bttnSendCustomer.TabIndex = 7;
            bttnSendCustomer.Text = "Müşterileri Aktar";
            bttnSendCustomer.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnSendCustomer.UseAccentColor = false;
            bttnSendCustomer.UseVisualStyleBackColor = true;
            bttnSendCustomer.Click += bttnSendCustomer_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(206, 22);
            exitToolStripMenuItem.Text = "Çıkış yap";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // collectionToolStripMenuItem
            // 
            collectionToolStripMenuItem.Name = "collectionToolStripMenuItem";
            collectionToolStripMenuItem.Size = new Size(206, 22);
            collectionToolStripMenuItem.Text = "Tahsilat/Ödeme İşlemleri";
            collectionToolStripMenuItem.Click += collectionToolStripMenuItem_Click;
            // 
            // waybillToolStripMenuItem
            // 
            waybillToolStripMenuItem.Name = "waybillToolStripMenuItem";
            waybillToolStripMenuItem.Size = new Size(206, 22);
            waybillToolStripMenuItem.Text = "İrsaliye İşlemleri";
            waybillToolStripMenuItem.Click += waybillToolStripMenuItem_Click;
            // 
            // menüToolStripMenuItem
            // 
            menüToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { waybillToolStripMenuItem, collectionToolStripMenuItem, anaMenüyeDönToolStripMenuItem, exitToolStripMenuItem });
            menüToolStripMenuItem.Name = "menüToolStripMenuItem";
            menüToolStripMenuItem.Size = new Size(50, 26);
            menüToolStripMenuItem.Text = "Menü";
            // 
            // anaMenüyeDönToolStripMenuItem
            // 
            anaMenüyeDönToolStripMenuItem.Name = "anaMenüyeDönToolStripMenuItem";
            anaMenüyeDönToolStripMenuItem.Size = new Size(206, 22);
            anaMenüyeDönToolStripMenuItem.Text = "Ana Menüye dön";
            anaMenüyeDönToolStripMenuItem.Click += anaMenüyeDönToolStripMenuItem_Click;
            // 
            // stripInvoice
            // 
            stripInvoice.AllowMerge = false;
            stripInvoice.AutoSize = false;
            stripInvoice.BackColor = Color.Transparent;
            stripInvoice.BackgroundImage = Properties.Resources.logo_1920;
            stripInvoice.BackgroundImageLayout = ImageLayout.Zoom;
            stripInvoice.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            stripInvoice.ImageScalingSize = new Size(20, 20);
            stripInvoice.Items.AddRange(new ToolStripItem[] { menüToolStripMenuItem });
            stripInvoice.Location = new Point(0, 0);
            stripInvoice.Name = "stripInvoice";
            stripInvoice.RenderMode = ToolStripRenderMode.Professional;
            stripInvoice.Size = new Size(1232, 30);
            stripInvoice.TabIndex = 24;
            stripInvoice.Text = "Fatura Menü";
            // 
            // dataGridInvoiceList
            // 
            dataGridInvoiceList.AllowUserToAddRows = false;
            dataGridInvoiceList.AllowUserToDeleteRows = false;
            dataGridInvoiceList.AllowUserToOrderColumns = true;
            dataGridInvoiceList.BorderStyle = BorderStyle.Fixed3D;
            dataGridInvoiceList.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridInvoiceList.ColumnHeadersHeight = 29;
            dataGridInvoiceList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridInvoiceList.Columns.AddRange(new DataGridViewColumn[] { chk });
            dataGridInvoiceList.Cursor = Cursors.Hand;
            dataGridInvoiceList.GridColor = SystemColors.ControlDark;
            dataGridInvoiceList.ImeMode = ImeMode.On;
            dataGridInvoiceList.Location = new Point(10, 253);
            dataGridInvoiceList.Name = "dataGridInvoiceList";
            dataGridInvoiceList.RowHeadersWidth = 51;
            dataGridInvoiceList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridInvoiceList.Size = new Size(1212, 436);
            dataGridInvoiceList.StandardTab = true;
            dataGridInvoiceList.TabIndex = 26;
            // 
            // DataIntegrationsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1232, 701);
            ControlBox = false;
            Controls.Add(chckAll);
            Controls.Add(divider);
            Controls.Add(materialCard1);
            Controls.Add(stripInvoice);
            Controls.Add(dataGridInvoiceList);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "DataIntegrationsForm";
            Text = "SalesArt Integration";
            Load += DataIntegrationsForm_Load;
            materialCard1.ResumeLayout(false);
            materialCard3.ResumeLayout(false);
            materialCard3.PerformLayout();
            materialCard2.ResumeLayout(false);
            materialCard2.PerformLayout();
            stripInvoice.ResumeLayout(false);
            stripInvoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInvoiceList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridViewCheckBoxColumn chk;
        private CheckBox chckAll;
        private MaterialSkin.Controls.MaterialDivider divider;
        private MaterialSkin.Controls.MaterialButton bttnLogs;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialButton bttnSendCustomer;
        private MaterialSkin.Controls.MaterialButton bttnGetCustomers;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem collectionToolStripMenuItem;
        private ToolStripMenuItem waybillToolStripMenuItem;
        private ToolStripMenuItem menüToolStripMenuItem;
        private ToolStripMenuItem anaMenüyeDönToolStripMenuItem;
        private MenuStrip stripInvoice;
        private DataGridView dataGridInvoiceList;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private MaterialSkin.Controls.MaterialButton bttnSendProducts;
        private MaterialSkin.Controls.MaterialButton bttnGetProducts;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
    }
}