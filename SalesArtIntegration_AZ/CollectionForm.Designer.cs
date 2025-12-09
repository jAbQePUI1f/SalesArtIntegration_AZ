namespace SalesArtIntegration_AZ
{
    partial class CollectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionForm));
            stripInvoice = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            invoiceToolStripMenuItem = new ToolStripMenuItem();
            collectionToolStripMenuItem = new ToolStripMenuItem();
            backToolStripMenuItem1 = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            comboboxInvoiceType = new MaterialSkin.Controls.MaterialComboBox();
            lblType = new MaterialSkin.Controls.MaterialLabel();
            bttnSendCollection = new MaterialSkin.Controls.MaterialButton();
            lblStartDate = new MaterialSkin.Controls.MaterialLabel();
            bttnGetCollection = new MaterialSkin.Controls.MaterialButton();
            dateTimeFinishDate = new DateTimePicker();
            dateTimeStartDate = new DateTimePicker();
            lblFinishDate = new MaterialSkin.Controls.MaterialLabel();
            dataGridInvoiceList = new DataGridView();
            chk = new DataGridViewCheckBoxColumn();
            divider = new MaterialSkin.Controls.MaterialDivider();
            chckAll = new CheckBox();
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
            stripInvoice.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem });
            stripInvoice.Location = new Point(0, 0);
            stripInvoice.Name = "stripInvoice";
            stripInvoice.RenderMode = ToolStripRenderMode.Professional;
            stripInvoice.Size = new Size(1008, 24);
            stripInvoice.TabIndex = 19;
            stripInvoice.Text = "Fatura Menü";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { invoiceToolStripMenuItem, collectionToolStripMenuItem, backToolStripMenuItem1, exitToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(50, 20);
            menuToolStripMenuItem.Text = "Menü";
            // 
            // invoiceToolStripMenuItem
            // 
            invoiceToolStripMenuItem.Name = "invoiceToolStripMenuItem";
            invoiceToolStripMenuItem.Size = new Size(165, 22);
            invoiceToolStripMenuItem.Text = "Fatura İşlemleri";
            invoiceToolStripMenuItem.Click += invoiceToolStripMenuItem_Click;
            // 
            // backToolStripMenuItem1
            // 
            backToolStripMenuItem1.Name = "backToolStripMenuItem1";
            backToolStripMenuItem1.Size = new Size(165, 22);
            backToolStripMenuItem1.Text = "Ana Menüye dön";
            backToolStripMenuItem1.Click += backToolStripMenuItem1_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(165, 22);
            exitToolStripMenuItem.Text = "Çıkış yap";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(comboboxInvoiceType);
            materialCard1.Controls.Add(lblType);
            materialCard1.Controls.Add(bttnSendCollection);
            materialCard1.Controls.Add(lblStartDate);
            materialCard1.Controls.Add(bttnGetCollection);
            materialCard1.Controls.Add(dateTimeFinishDate);
            materialCard1.Controls.Add(dateTimeStartDate);
            materialCard1.Controls.Add(lblFinishDate);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(9, 26);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(992, 187);
            materialCard1.TabIndex = 22;
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
            comboboxInvoiceType.Location = new Point(24, 35);
            comboboxInvoiceType.MaxDropDownItems = 4;
            comboboxInvoiceType.MouseState = MaterialSkin.MouseState.OUT;
            comboboxInvoiceType.Name = "comboboxInvoiceType";
            comboboxInvoiceType.Size = new Size(262, 49);
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
            lblType.Location = new Point(24, 9);
            lblType.MouseState = MaterialSkin.MouseState.HOVER;
            lblType.Name = "lblType";
            lblType.Size = new Size(128, 14);
            lblType.TabIndex = 9;
            lblType.Text = "Choose Collection Type";
            // 
            // bttnSendCollection
            // 
            bttnSendCollection.AutoSize = false;
            bttnSendCollection.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnSendCollection.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnSendCollection.Depth = 0;
            bttnSendCollection.HighEmphasis = true;
            bttnSendCollection.Icon = null;
            bttnSendCollection.Location = new Point(760, 113);
            bttnSendCollection.Margin = new Padding(4, 6, 4, 6);
            bttnSendCollection.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendCollection.Name = "bttnSendCollection";
            bttnSendCollection.NoAccentTextColor = Color.Empty;
            bttnSendCollection.Size = new Size(200, 42);
            bttnSendCollection.TabIndex = 15;
            bttnSendCollection.Text = "Aktar";
            bttnSendCollection.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnSendCollection.UseAccentColor = false;
            bttnSendCollection.UseVisualStyleBackColor = true;
            bttnSendCollection.Click += bttnSendCollection_Click;
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Depth = 0;
            lblStartDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblStartDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblStartDate.Location = new Point(464, 4);
            lblStartDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(99, 14);
            lblStartDate.TabIndex = 11;
            lblStartDate.Text = "Choose Start Date";
            // 
            // bttnGetCollection
            // 
            bttnGetCollection.AutoSize = false;
            bttnGetCollection.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnGetCollection.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnGetCollection.Depth = 0;
            bttnGetCollection.HighEmphasis = true;
            bttnGetCollection.Icon = null;
            bttnGetCollection.Location = new Point(24, 113);
            bttnGetCollection.Margin = new Padding(4, 6, 4, 6);
            bttnGetCollection.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetCollection.Name = "bttnGetCollection";
            bttnGetCollection.NoAccentTextColor = Color.Empty;
            bttnGetCollection.Size = new Size(262, 42);
            bttnGetCollection.TabIndex = 8;
            bttnGetCollection.Text = "Getİr";
            bttnGetCollection.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnGetCollection.UseAccentColor = false;
            bttnGetCollection.UseVisualStyleBackColor = true;
            bttnGetCollection.Click += bttnGetCollection_Click;
            // 
            // dateTimeFinishDate
            // 
            dateTimeFinishDate.Location = new Point(727, 31);
            dateTimeFinishDate.Name = "dateTimeFinishDate";
            dateTimeFinishDate.Size = new Size(233, 23);
            dateTimeFinishDate.TabIndex = 5;
            dateTimeFinishDate.Value = new DateTime(2025, 12, 3, 17, 15, 0, 0);
            // 
            // dateTimeStartDate
            // 
            dateTimeStartDate.CustomFormat = "";
            dateTimeStartDate.Location = new Point(464, 31);
            dateTimeStartDate.Name = "dateTimeStartDate";
            dateTimeStartDate.Size = new Size(228, 23);
            dateTimeStartDate.TabIndex = 5;
            dateTimeStartDate.Value = new DateTime(2025, 12, 3, 17, 15, 0, 0);
            // 
            // lblFinishDate
            // 
            lblFinishDate.AutoSize = true;
            lblFinishDate.Depth = 0;
            lblFinishDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblFinishDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblFinishDate.Location = new Point(727, 4);
            lblFinishDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblFinishDate.Name = "lblFinishDate";
            lblFinishDate.Size = new Size(106, 14);
            lblFinishDate.TabIndex = 12;
            lblFinishDate.Text = "Choose Finish Date";
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
            dataGridInvoiceList.Location = new Point(6, 235);
            dataGridInvoiceList.Name = "dataGridInvoiceList";
            dataGridInvoiceList.RowHeadersWidth = 51;
            dataGridInvoiceList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridInvoiceList.Size = new Size(995, 428);
            dataGridInvoiceList.StandardTab = true;
            dataGridInvoiceList.TabIndex = 21;
            // 
            // chk
            // 
            chk.HeaderText = "";
            chk.MinimumWidth = 6;
            chk.Name = "chk";
            chk.Width = 80;
            // 
            // divider
            // 
            divider.BackColor = SystemColors.GradientActiveCaption;
            divider.Depth = 0;
            divider.Location = new Point(-2, 217);
            divider.MouseState = MaterialSkin.MouseState.HOVER;
            divider.Name = "divider";
            divider.Size = new Size(1010, 10);
            divider.TabIndex = 20;
            // 
            // chckAll
            // 
            chckAll.AutoSize = true;
            chckAll.CheckAlign = ContentAlignment.MiddleRight;
            chckAll.Cursor = Cursors.Hand;
            chckAll.FlatStyle = FlatStyle.Popup;
            chckAll.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chckAll.Location = new Point(60, 245);
            chckAll.Name = "chckAll";
            chckAll.Size = new Size(13, 12);
            chckAll.TabIndex = 28;
            chckAll.UseVisualStyleBackColor = true;
            // 
            // CollectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 667);
            Controls.Add(chckAll);
            Controls.Add(stripInvoice);
            Controls.Add(materialCard1);
            Controls.Add(dataGridInvoiceList);
            Controls.Add(divider);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CollectionForm";
            Text = "SalesArt Integration";
            FormClosed += CollectionForm_FormClosed;
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
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem invoiceToolStripMenuItem;
        private ToolStripMenuItem collectionToolStripMenuItem;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialComboBox comboboxInvoiceType;
        private MaterialSkin.Controls.MaterialLabel lblType;
        private MaterialSkin.Controls.MaterialButton bttnSendCollection;
        private MaterialSkin.Controls.MaterialLabel lblStartDate;
        private MaterialSkin.Controls.MaterialButton bttnGetCollection;
        private DateTimePicker dateTimeFinishDate;
        private DateTimePicker dateTimeStartDate;
        private MaterialSkin.Controls.MaterialLabel lblFinishDate;
        private DataGridView dataGridInvoiceList;
        private MaterialSkin.Controls.MaterialDivider divider;
        private DataGridViewCheckBoxColumn chk;
        private ToolStripMenuItem backToolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private CheckBox chckAll;
    }
}