namespace SalesArtIntegration_AZ
{
    partial class InvoiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceForm));
            stripInvoice = new MenuStrip();
            menüToolStripMenuItem = new ToolStripMenuItem();
            waybillToolStripMenuItem = new ToolStripMenuItem();
            collectionToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            lblFinishDate = new MaterialSkin.Controls.MaterialLabel();
            lblStartDate = new MaterialSkin.Controls.MaterialLabel();
            lblType = new MaterialSkin.Controls.MaterialLabel();
            bttnSendInvoice = new MaterialSkin.Controls.MaterialButton();
            dateTimeFinishDate = new DateTimePicker();
            dateTimeStartDate = new DateTimePicker();
            comboboxInvoiceType = new MaterialSkin.Controls.MaterialComboBox();
            bttnGetInvoice = new MaterialSkin.Controls.MaterialButton();
            divider = new MaterialSkin.Controls.MaterialDivider();
            dataGridInvoiceList = new DataGridView();
            stripInvoice.SuspendLayout();
            materialCard1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInvoiceList).BeginInit();
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
            stripInvoice.Items.AddRange(new ToolStripItem[] { menüToolStripMenuItem });
            stripInvoice.Location = new Point(0, 0);
            stripInvoice.Name = "stripInvoice";
            stripInvoice.Padding = new Padding(7, 3, 0, 3);
            stripInvoice.RenderMode = ToolStripRenderMode.Professional;
            stripInvoice.Size = new Size(1239, 30);
            stripInvoice.TabIndex = 3;
            stripInvoice.Text = "Fatura Menü";
            // 
            // menüToolStripMenuItem
            // 
            menüToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { waybillToolStripMenuItem, collectionToolStripMenuItem, exitToolStripMenuItem });
            menüToolStripMenuItem.Name = "menüToolStripMenuItem";
            menüToolStripMenuItem.Size = new Size(63, 24);
            menüToolStripMenuItem.Text = "Menu";
            // 
            // waybillToolStripMenuItem
            // 
            waybillToolStripMenuItem.Name = "waybillToolStripMenuItem";
            waybillToolStripMenuItem.Size = new Size(160, 26);
            waybillToolStripMenuItem.Text = "Waybill";
            waybillToolStripMenuItem.Click += waybillToolStripMenuItem_Click;
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
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(lblFinishDate);
            materialCard1.Controls.Add(lblStartDate);
            materialCard1.Controls.Add(lblType);
            materialCard1.Controls.Add(bttnSendInvoice);
            materialCard1.Controls.Add(dateTimeFinishDate);
            materialCard1.Controls.Add(dateTimeStartDate);
            materialCard1.Controls.Add(comboboxInvoiceType);
            materialCard1.Controls.Add(bttnGetInvoice);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(8, 44);
            materialCard1.Margin = new Padding(16, 19, 16, 19);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(16, 19, 16, 19);
            materialCard1.Size = new Size(1219, 233);
            materialCard1.TabIndex = 4;
            // 
            // lblFinishDate
            // 
            lblFinishDate.AutoSize = true;
            lblFinishDate.BackColor = SystemColors.Control;
            lblFinishDate.Depth = 0;
            lblFinishDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblFinishDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblFinishDate.Location = new Point(863, 11);
            lblFinishDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblFinishDate.Name = "lblFinishDate";
            lblFinishDate.Size = new Size(106, 14);
            lblFinishDate.TabIndex = 4;
            lblFinishDate.Text = "Choose Finish Date";
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.BackColor = SystemColors.Control;
            lblStartDate.Depth = 0;
            lblStartDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblStartDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblStartDate.Location = new Point(587, 11);
            lblStartDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(99, 14);
            lblStartDate.TabIndex = 3;
            lblStartDate.Text = "Choose Start Date";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.BackColor = SystemColors.Control;
            lblType.Depth = 0;
            lblType.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblType.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblType.Location = new Point(38, 11);
            lblType.MouseState = MaterialSkin.MouseState.HOVER;
            lblType.Name = "lblType";
            lblType.Size = new Size(112, 14);
            lblType.TabIndex = 1;
            lblType.Text = "Choose Invoice Type";
            // 
            // bttnSendInvoice
            // 
            bttnSendInvoice.AutoSize = false;
            bttnSendInvoice.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnSendInvoice.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnSendInvoice.Depth = 0;
            bttnSendInvoice.HighEmphasis = true;
            bttnSendInvoice.Icon = null;
            bttnSendInvoice.Location = new Point(863, 151);
            bttnSendInvoice.Margin = new Padding(5, 8, 5, 8);
            bttnSendInvoice.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendInvoice.Name = "bttnSendInvoice";
            bttnSendInvoice.NoAccentTextColor = Color.Empty;
            bttnSendInvoice.Size = new Size(229, 56);
            bttnSendInvoice.TabIndex = 7;
            bttnSendInvoice.Text = "Send to 1C";
            bttnSendInvoice.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnSendInvoice.UseAccentColor = false;
            bttnSendInvoice.UseVisualStyleBackColor = true;
            // 
            // dateTimeFinishDate
            // 
            dateTimeFinishDate.Location = new Point(863, 45);
            dateTimeFinishDate.Margin = new Padding(3, 4, 3, 4);
            dateTimeFinishDate.Name = "dateTimeFinishDate";
            dateTimeFinishDate.Size = new Size(228, 27);
            dateTimeFinishDate.TabIndex = 6;
            // 
            // dateTimeStartDate
            // 
            dateTimeStartDate.Location = new Point(587, 45);
            dateTimeStartDate.Margin = new Padding(3, 4, 3, 4);
            dateTimeStartDate.Name = "dateTimeStartDate";
            dateTimeStartDate.Size = new Size(228, 27);
            dateTimeStartDate.TabIndex = 5;
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
            comboboxInvoiceType.Location = new Point(38, 48);
            comboboxInvoiceType.Margin = new Padding(3, 4, 3, 4);
            comboboxInvoiceType.MaxDropDownItems = 4;
            comboboxInvoiceType.MouseState = MaterialSkin.MouseState.OUT;
            comboboxInvoiceType.Name = "comboboxInvoiceType";
            comboboxInvoiceType.Size = new Size(252, 49);
            comboboxInvoiceType.StartIndex = 0;
            comboboxInvoiceType.TabIndex = 2;
            // 
            // bttnGetInvoice
            // 
            bttnGetInvoice.AutoSize = false;
            bttnGetInvoice.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnGetInvoice.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnGetInvoice.Depth = 0;
            bttnGetInvoice.HighEmphasis = true;
            bttnGetInvoice.Icon = null;
            bttnGetInvoice.Location = new Point(39, 151);
            bttnGetInvoice.Margin = new Padding(5, 8, 5, 8);
            bttnGetInvoice.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetInvoice.Name = "bttnGetInvoice";
            bttnGetInvoice.NoAccentTextColor = Color.Empty;
            bttnGetInvoice.Size = new Size(251, 56);
            bttnGetInvoice.TabIndex = 0;
            bttnGetInvoice.Text = "Get Invoices";
            bttnGetInvoice.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnGetInvoice.UseAccentColor = false;
            bttnGetInvoice.UseVisualStyleBackColor = true;
            bttnGetInvoice.Click += bttnGetInvoice_Click;
            // 
            // divider
            // 
            divider.BackColor = SystemColors.GradientActiveCaption;
            divider.Depth = 0;
            divider.Location = new Point(0, 283);
            divider.Margin = new Padding(3, 4, 3, 4);
            divider.MouseState = MaterialSkin.MouseState.HOVER;
            divider.Name = "divider";
            divider.Size = new Size(1240, 13);
            divider.TabIndex = 3;
            // 
            // dataGridInvoiceList
            // 
            dataGridInvoiceList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridInvoiceList.Location = new Point(8, 308);
            dataGridInvoiceList.Margin = new Padding(3, 4, 3, 4);
            dataGridInvoiceList.Name = "dataGridInvoiceList";
            dataGridInvoiceList.RowHeadersWidth = 51;
            dataGridInvoiceList.Size = new Size(1224, 519);
            dataGridInvoiceList.TabIndex = 5;
            // 
            // InvoiceForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1239, 835);
            Controls.Add(dataGridInvoiceList);
            Controls.Add(divider);
            Controls.Add(materialCard1);
            Controls.Add(stripInvoice);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "InvoiceForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Transfer Invoice";
            stripInvoice.ResumeLayout(false);
            stripInvoice.PerformLayout();
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInvoiceList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip stripInvoice;
        private ToolStripMenuItem menüToolStripMenuItem;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialLabel lblType;
        private MaterialSkin.Controls.MaterialButton bttnGetInvoice;
        private MaterialSkin.Controls.MaterialComboBox comboboxInvoiceType;
        private MaterialSkin.Controls.MaterialDivider divider;
        private MaterialSkin.Controls.MaterialLabel lblStartDate;
        private MaterialSkin.Controls.MaterialLabel lblFinishDate;
        private MaterialSkin.Controls.MaterialButton bttnSendInvoice;
        private DateTimePicker dateTimeFinishDate;
        private DateTimePicker dateTimeStartDate;
        private DataGridView dataGridInvoiceList;
        private ToolStripMenuItem waybillToolStripMenuItem;
        private ToolStripMenuItem collectionToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}