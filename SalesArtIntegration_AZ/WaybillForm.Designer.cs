namespace SalesArtIntegration_AZ
{
    partial class WaybillForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaybillForm));
            stripInvoice = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            invoiceToolStripMenuItem = new ToolStripMenuItem();
            collectionToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            bttnSendWaybill = new MaterialSkin.Controls.MaterialButton();
            dateTimeFinishDate = new DateTimePicker();
            dateTimeStartDate = new DateTimePicker();
            lblFinishDate = new MaterialSkin.Controls.MaterialLabel();
            lblStartDate = new MaterialSkin.Controls.MaterialLabel();
            comboboxInvoiceType = new MaterialSkin.Controls.MaterialComboBox();
            lblType = new MaterialSkin.Controls.MaterialLabel();
            bttnGetWaybill = new MaterialSkin.Controls.MaterialButton();
            dataGridInvoiceList = new DataGridView();
            divider = new MaterialSkin.Controls.MaterialDivider();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            stripInvoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInvoiceList).BeginInit();
            materialCard1.SuspendLayout();
            SuspendLayout();
            // 
            // stripInvoice
            // 
            stripInvoice.AllowMerge = false;
            stripInvoice.BackColor = Color.Transparent;
            stripInvoice.BackgroundImage = Properties.Resources.logo_1920;
            stripInvoice.BackgroundImageLayout = ImageLayout.Zoom;
            stripInvoice.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            stripInvoice.ImageScalingSize = new Size(20, 20);
            stripInvoice.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });
            stripInvoice.Location = new Point(0, 0);
            stripInvoice.Name = "stripInvoice";
            stripInvoice.Padding = new Padding(7, 3, 0, 3);
            stripInvoice.RenderMode = ToolStripRenderMode.Professional;
            stripInvoice.Size = new Size(1250, 30);
            stripInvoice.TabIndex = 4;
            stripInvoice.Text = "Fatura Menü";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { invoiceToolStripMenuItem, collectionToolStripMenuItem, exitToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(63, 24);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // invoiceToolStripMenuItem
            // 
            invoiceToolStripMenuItem.Name = "invoiceToolStripMenuItem";
            invoiceToolStripMenuItem.Size = new Size(160, 26);
            invoiceToolStripMenuItem.Text = "Invoice";
            invoiceToolStripMenuItem.Click += invoiceToolStripMenuItem_Click;
            // 
            // collectionToolStripMenuItem
            // 
            collectionToolStripMenuItem.Name = "collectionToolStripMenuItem";
            collectionToolStripMenuItem.Size = new Size(160, 26);
            collectionToolStripMenuItem.Text = "Collection";
            collectionToolStripMenuItem.Click += collectionToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(160, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // bttnSendWaybill
            // 
            bttnSendWaybill.AutoSize = false;
            bttnSendWaybill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnSendWaybill.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnSendWaybill.Depth = 0;
            bttnSendWaybill.HighEmphasis = true;
            bttnSendWaybill.Icon = null;
            bttnSendWaybill.Location = new Point(867, 153);
            bttnSendWaybill.Margin = new Padding(5, 8, 5, 8);
            bttnSendWaybill.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendWaybill.Name = "bttnSendWaybill";
            bttnSendWaybill.NoAccentTextColor = Color.Empty;
            bttnSendWaybill.Size = new Size(229, 56);
            bttnSendWaybill.TabIndex = 15;
            bttnSendWaybill.Text = "Send Waybill";
            bttnSendWaybill.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnSendWaybill.UseAccentColor = false;
            bttnSendWaybill.UseVisualStyleBackColor = true;
            bttnSendWaybill.Click += bttnSendWaybill_Click;
            // 
            // dateTimeFinishDate
            // 
            dateTimeFinishDate.Location = new Point(867, 44);
            dateTimeFinishDate.Margin = new Padding(3, 4, 3, 4);
            dateTimeFinishDate.Name = "dateTimeFinishDate";
            dateTimeFinishDate.Size = new Size(228, 27);
            dateTimeFinishDate.TabIndex = 14;
            // 
            // dateTimeStartDate
            // 
            dateTimeStartDate.Location = new Point(592, 43);
            dateTimeStartDate.Margin = new Padding(3, 4, 3, 4);
            dateTimeStartDate.Name = "dateTimeStartDate";
            dateTimeStartDate.Size = new Size(228, 27);
            dateTimeStartDate.TabIndex = 13;
            // 
            // lblFinishDate
            // 
            lblFinishDate.AutoSize = true;
            lblFinishDate.Depth = 0;
            lblFinishDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblFinishDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblFinishDate.Location = new Point(867, 8);
            lblFinishDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblFinishDate.Name = "lblFinishDate";
            lblFinishDate.Size = new Size(106, 14);
            lblFinishDate.TabIndex = 12;
            lblFinishDate.Text = "Choose Finish Date";
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Depth = 0;
            lblStartDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblStartDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblStartDate.Location = new Point(592, 8);
            lblStartDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(99, 14);
            lblStartDate.TabIndex = 11;
            lblStartDate.Text = "Choose Start Date";
            // 
            // comboboxInvoiceType
            // 
            comboboxInvoiceType.AutoResize = false;
            comboboxInvoiceType.BackColor = Color.FromArgb(255, 255, 255);
            comboboxInvoiceType.Depth = 0;
            comboboxInvoiceType.DrawMode = DrawMode.OwnerDrawVariable;
            comboboxInvoiceType.DropDownHeight = 174;
            comboboxInvoiceType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboboxInvoiceType.DropDownWidth = 121;
            comboboxInvoiceType.FlatStyle = FlatStyle.Popup;
            comboboxInvoiceType.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            comboboxInvoiceType.ForeColor = Color.FromArgb(222, 0, 0, 0);
            comboboxInvoiceType.FormattingEnabled = true;
            comboboxInvoiceType.IntegralHeight = false;
            comboboxInvoiceType.ItemHeight = 43;
            comboboxInvoiceType.Items.AddRange(new object[] { "SEÇİNİZ...", "SELLING", "BUYING", "SELLING_RETURN", "BUYING_RETURN", "DAMAGED_SELLING_RETURN", "DAMAGED_BUYING_RETURN", "SELLING_SERVICE", "BUYING_SERVICE" });
            comboboxInvoiceType.Location = new Point(27, 39);
            comboboxInvoiceType.Margin = new Padding(3, 4, 3, 4);
            comboboxInvoiceType.MaxDropDownItems = 4;
            comboboxInvoiceType.MouseState = MaterialSkin.MouseState.OUT;
            comboboxInvoiceType.Name = "comboboxInvoiceType";
            comboboxInvoiceType.Size = new Size(252, 49);
            comboboxInvoiceType.StartIndex = 0;
            comboboxInvoiceType.TabIndex = 10;
            comboboxInvoiceType.SelectedIndexChanged += comboboxInvoiceType_SelectedIndexChanged;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Depth = 0;
            lblType.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblType.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblType.Location = new Point(27, 8);
            lblType.MouseState = MaterialSkin.MouseState.HOVER;
            lblType.Name = "lblType";
            lblType.Size = new Size(112, 14);
            lblType.TabIndex = 9;
            lblType.Text = "Choose Invoice Type";
            // 
            // bttnGetWaybill
            // 
            bttnGetWaybill.AutoSize = false;
            bttnGetWaybill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnGetWaybill.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnGetWaybill.Depth = 0;
            bttnGetWaybill.HighEmphasis = true;
            bttnGetWaybill.Icon = null;
            bttnGetWaybill.Location = new Point(27, 153);
            bttnGetWaybill.Margin = new Padding(5, 8, 5, 8);
            bttnGetWaybill.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetWaybill.Name = "bttnGetWaybill";
            bttnGetWaybill.NoAccentTextColor = Color.Empty;
            bttnGetWaybill.Size = new Size(251, 56);
            bttnGetWaybill.TabIndex = 8;
            bttnGetWaybill.Text = "Get Waybill";
            bttnGetWaybill.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnGetWaybill.UseAccentColor = false;
            bttnGetWaybill.UseVisualStyleBackColor = true;
            bttnGetWaybill.Click += bttnGetWaybill_Click;
            // 
            // dataGridInvoiceList
            // 
            dataGridInvoiceList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridInvoiceList.Location = new Point(8, 308);
            dataGridInvoiceList.Margin = new Padding(3, 4, 3, 4);
            dataGridInvoiceList.Name = "dataGridInvoiceList";
            dataGridInvoiceList.RowHeadersWidth = 51;
            dataGridInvoiceList.Size = new Size(1234, 571);
            dataGridInvoiceList.TabIndex = 17;
            // 
            // divider
            // 
            divider.BackColor = SystemColors.GradientActiveCaption;
            divider.Depth = 0;
            divider.Location = new Point(-1, 284);
            divider.Margin = new Padding(3, 4, 3, 4);
            divider.MouseState = MaterialSkin.MouseState.HOVER;
            divider.Name = "divider";
            divider.Size = new Size(1251, 13);
            divider.TabIndex = 16;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(comboboxInvoiceType);
            materialCard1.Controls.Add(lblType);
            materialCard1.Controls.Add(bttnSendWaybill);
            materialCard1.Controls.Add(lblStartDate);
            materialCard1.Controls.Add(bttnGetWaybill);
            materialCard1.Controls.Add(dateTimeFinishDate);
            materialCard1.Controls.Add(dateTimeStartDate);
            materialCard1.Controls.Add(lblFinishDate);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(11, 44);
            materialCard1.Margin = new Padding(16, 19, 16, 19);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(16, 19, 16, 19);
            materialCard1.Size = new Size(1227, 233);
            materialCard1.TabIndex = 18;
            // 
            // WaybillForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 895);
            Controls.Add(dataGridInvoiceList);
            Controls.Add(divider);
            Controls.Add(stripInvoice);
            Controls.Add(materialCard1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "WaybillForm";
            Text = "Transfer Waybill";
            stripInvoice.ResumeLayout(false);
            stripInvoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInvoiceList).EndInit();
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip stripInvoice;
        private ToolStripMenuItem menuToolStripMenuItem;
        private MaterialSkin.Controls.MaterialButton bttnSendWaybill;
        private DateTimePicker dateTimeFinishDate;
        private DateTimePicker dateTimeStartDate;
        private MaterialSkin.Controls.MaterialLabel lblFinishDate;
        private MaterialSkin.Controls.MaterialLabel lblStartDate;
        private MaterialSkin.Controls.MaterialComboBox comboboxInvoiceType;
        private MaterialSkin.Controls.MaterialLabel lblType;
        private MaterialSkin.Controls.MaterialButton bttnGetWaybill;
        private DataGridView dataGridInvoiceList;
        private MaterialSkin.Controls.MaterialDivider divider;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private ToolStripMenuItem invoiceToolStripMenuItem;
        private ToolStripMenuItem collectionToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}