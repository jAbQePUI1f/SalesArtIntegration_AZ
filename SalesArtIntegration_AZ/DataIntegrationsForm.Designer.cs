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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            chk = new DataGridViewCheckBoxColumn();
            chckAll = new CheckBox();
            divider = new MaterialSkin.Controls.MaterialDivider();
            bttnLogs = new MaterialSkin.Controls.MaterialButton();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            materialCard3 = new MaterialSkin.Controls.MaterialCard();
            bttnSendProducts = new MaterialSkin.Controls.MaterialButton();
            bttnGetProducts = new MaterialSkin.Controls.MaterialButton();
            materialCard2 = new MaterialSkin.Controls.MaterialCard();
            bttnGetCustomers = new MaterialSkin.Controls.MaterialButton();
            bttnSendCustomer = new MaterialSkin.Controls.MaterialButton();
            exitToolStripMenuItem = new ToolStripMenuItem();
            collectionToolStripMenuItem = new ToolStripMenuItem();
            waybillToolStripMenuItem = new ToolStripMenuItem();
            menüToolStripMenuItem = new ToolStripMenuItem();
            anaMenüyeDönToolStripMenuItem = new ToolStripMenuItem();
            stripInvoice = new MenuStrip();
            dataGridDataList = new DataGridView();
            materialCard1.SuspendLayout();
            materialCard3.SuspendLayout();
            materialCard2.SuspendLayout();
            stripInvoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridDataList).BeginInit();
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
            chckAll.Location = new Point(65, 275);
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
            divider.Location = new Point(-2, 248);
            divider.MouseState = MaterialSkin.MouseState.HOVER;
            divider.Name = "divider";
            divider.Size = new Size(952, 20);
            divider.TabIndex = 23;
            // 
            // bttnLogs
            // 
            bttnLogs.AutoSize = false;
            bttnLogs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnLogs.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Dense;
            bttnLogs.Depth = 0;
            bttnLogs.HighEmphasis = false;
            bttnLogs.Icon = (Image)resources.GetObject("bttnLogs.Icon");
            bttnLogs.Location = new Point(836, 39);
            bttnLogs.Margin = new Padding(4);
            bttnLogs.MouseState = MaterialSkin.MouseState.HOVER;
            bttnLogs.Name = "bttnLogs";
            bttnLogs.NoAccentTextColor = Color.Empty;
            bttnLogs.Size = new Size(82, 157);
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
            materialCard1.Controls.Add(materialLabel1);
            materialCard1.Controls.Add(materialLabel2);
            materialCard1.Controls.Add(materialCard3);
            materialCard1.Controls.Add(materialCard2);
            materialCard1.Controls.Add(bttnLogs);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(11, 35);
            materialCard1.Margin = new Padding(14, 15, 14, 15);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(12, 11, 12, 11);
            materialCard1.Size = new Size(930, 207);
            materialCard1.TabIndex = 25;
            // 
            // materialLabel1
            // 
            materialLabel1.Depth = 0;
            materialLabel1.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            materialLabel1.Location = new Point(161, 25);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(92, 25);
            materialLabel1.TabIndex = 0;
            materialLabel1.Text = "  Müşteri";
            // 
            // materialLabel2
            // 
            materialLabel2.Depth = 0;
            materialLabel2.Font = new Font("Roboto Medium", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialLabel2.FontType = MaterialSkin.MaterialSkinManager.fontType.H6;
            materialLabel2.Location = new Point(604, 25);
            materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new Size(60, 25);
            materialLabel2.TabIndex = 1;
            materialLabel2.Text = "  Ürün";
            // 
            // materialCard3
            // 
            materialCard3.BackColor = Color.FromArgb(255, 255, 255);
            materialCard3.BorderStyle = BorderStyle.FixedSingle;
            materialCard3.Controls.Add(bttnSendProducts);
            materialCard3.Controls.Add(bttnGetProducts);
            materialCard3.Depth = 0;
            materialCard3.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard3.Location = new Point(439, 39);
            materialCard3.Margin = new Padding(12, 11, 12, 11);
            materialCard3.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard3.Name = "materialCard3";
            materialCard3.Padding = new Padding(12, 11, 12, 11);
            materialCard3.Size = new Size(383, 157);
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
            bttnSendProducts.Location = new Point(65, 97);
            bttnSendProducts.Margin = new Padding(4, 6, 4, 6);
            bttnSendProducts.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendProducts.Name = "bttnSendProducts";
            bttnSendProducts.NoAccentTextColor = Color.Empty;
            bttnSendProducts.Size = new Size(253, 43);
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
            bttnGetProducts.Location = new Point(65, 27);
            bttnGetProducts.Margin = new Padding(4, 6, 4, 6);
            bttnGetProducts.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetProducts.Name = "bttnGetProducts";
            bttnGetProducts.NoAccentTextColor = Color.Empty;
            bttnGetProducts.Size = new Size(254, 58);
            bttnGetProducts.TabIndex = 2;
            bttnGetProducts.Text = "Ürünleri Getir";
            bttnGetProducts.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnGetProducts.UseAccentColor = false;
            bttnGetProducts.UseVisualStyleBackColor = true;
            bttnGetProducts.Click += bttnGetProducts_Click;
            // 
            // materialCard2
            // 
            materialCard2.BackColor = Color.FromArgb(255, 255, 255);
            materialCard2.BorderStyle = BorderStyle.FixedSingle;
            materialCard2.Controls.Add(bttnGetCustomers);
            materialCard2.Controls.Add(bttnSendCustomer);
            materialCard2.Depth = 0;
            materialCard2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard2.Location = new Point(14, 39);
            materialCard2.Margin = new Padding(12, 11, 12, 11);
            materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard2.Name = "materialCard2";
            materialCard2.Padding = new Padding(12, 11, 12, 11);
            materialCard2.Size = new Size(387, 157);
            materialCard2.TabIndex = 18;
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
            bttnGetCustomers.Location = new Point(64, 27);
            bttnGetCustomers.Margin = new Padding(4, 6, 4, 6);
            bttnGetCustomers.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetCustomers.Name = "bttnGetCustomers";
            bttnGetCustomers.NoAccentTextColor = Color.Empty;
            bttnGetCustomers.Size = new Size(246, 58);
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
            bttnSendCustomer.Location = new Point(64, 97);
            bttnSendCustomer.Margin = new Padding(4, 6, 4, 6);
            bttnSendCustomer.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendCustomer.Name = "bttnSendCustomer";
            bttnSendCustomer.NoAccentTextColor = Color.Empty;
            bttnSendCustomer.Size = new Size(246, 44);
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
            menüToolStripMenuItem.Size = new Size(50, 28);
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
            stripInvoice.Size = new Size(947, 32);
            stripInvoice.TabIndex = 24;
            stripInvoice.Text = "Fatura Menü";
            // 
            // dataGridDataList
            // 
            dataGridDataList.AllowUserToAddRows = false;
            dataGridDataList.AllowUserToDeleteRows = false;
            dataGridDataList.AllowUserToOrderColumns = true;
            dataGridDataList.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            dataGridDataList.ColumnHeadersHeight = 29;
            dataGridDataList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridDataList.Columns.AddRange(new DataGridViewColumn[] { chk });
            dataGridDataList.Cursor = Cursors.Hand;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridDataList.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridDataList.GridColor = SystemColors.ControlDark;
            dataGridDataList.ImeMode = ImeMode.On;
            dataGridDataList.Location = new Point(12, 272);
            dataGridDataList.Name = "dataGridDataList";
            dataGridDataList.RowHeadersWidth = 51;
            dataGridDataList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridDataList.Size = new Size(923, 471);
            dataGridDataList.StandardTab = true;
            dataGridDataList.TabIndex = 26;
            // 
            // DataIntegrationsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(947, 748);
            ControlBox = false;
            Controls.Add(divider);
            Controls.Add(chckAll);
            Controls.Add(stripInvoice);
            Controls.Add(dataGridDataList);
            Controls.Add(materialCard1);
            Font = new Font("Cascadia Mono", 9F, FontStyle.Regular, GraphicsUnit.Point, 162);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "DataIntegrationsForm";
            Text = "SalesArt Integration";
            Load += DataIntegrationsForm_Load;
            materialCard1.ResumeLayout(false);
            materialCard3.ResumeLayout(false);
            materialCard2.ResumeLayout(false);
            stripInvoice.ResumeLayout(false);
            stripInvoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridDataList).EndInit();
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
        private DataGridView dataGridDataList;
        private MaterialSkin.Controls.MaterialCard materialCard3;
        private MaterialSkin.Controls.MaterialButton bttnSendProducts;
        private MaterialSkin.Controls.MaterialButton bttnGetProducts;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
    }
}