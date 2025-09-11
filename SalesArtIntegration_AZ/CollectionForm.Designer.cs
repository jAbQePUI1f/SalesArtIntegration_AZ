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
            materialCard1 = new MaterialSkin.Controls.MaterialCard();
            comboboxInvoiceType = new MaterialSkin.Controls.MaterialComboBox();
            lblType = new MaterialSkin.Controls.MaterialLabel();
            bttnSendWaybill = new MaterialSkin.Controls.MaterialButton();
            lblStartDate = new MaterialSkin.Controls.MaterialLabel();
            bttnGetWaybill = new MaterialSkin.Controls.MaterialButton();
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
            stripInvoice.Size = new Size(889, 24);
            stripInvoice.TabIndex = 19;
            stripInvoice.Text = "Fatura Menü";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { invoiceToolStripMenuItem, collectionToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(50, 20);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // invoiceToolStripMenuItem
            // 
            invoiceToolStripMenuItem.Name = "invoiceToolStripMenuItem";
            invoiceToolStripMenuItem.Size = new Size(114, 22);
            invoiceToolStripMenuItem.Text = "Invoice";
            invoiceToolStripMenuItem.Click += invoiceToolStripMenuItem_Click;
            // 
            // collectionToolStripMenuItem
            // 
            collectionToolStripMenuItem.Name = "collectionToolStripMenuItem";
            collectionToolStripMenuItem.Size = new Size(114, 22);
            collectionToolStripMenuItem.Text = "Waybill";
            collectionToolStripMenuItem.Click += collectionToolStripMenuItem_Click;
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
            materialCard1.Location = new Point(6, 35);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(875, 175);
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
            comboboxInvoiceType.Size = new Size(221, 49);
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
            // bttnSendWaybill
            // 
            bttnSendWaybill.AutoSize = false;
            bttnSendWaybill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnSendWaybill.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnSendWaybill.Depth = 0;
            bttnSendWaybill.HighEmphasis = true;
            bttnSendWaybill.Icon = null;
            bttnSendWaybill.Location = new Point(635, 117);
            bttnSendWaybill.Margin = new Padding(4, 6, 4, 6);
            bttnSendWaybill.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendWaybill.Name = "bttnSendWaybill";
            bttnSendWaybill.NoAccentTextColor = Color.Empty;
            bttnSendWaybill.Size = new Size(200, 42);
            bttnSendWaybill.TabIndex = 15;
            bttnSendWaybill.Text = "Gönder";
            bttnSendWaybill.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnSendWaybill.UseAccentColor = false;
            bttnSendWaybill.UseVisualStyleBackColor = true;
            bttnSendWaybill.Click += bttnSendWaybill_Click;
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Depth = 0;
            lblStartDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblStartDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblStartDate.Location = new Point(339, 8);
            lblStartDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(99, 14);
            lblStartDate.TabIndex = 11;
            lblStartDate.Text = "Choose Start Date";
            // 
            // bttnGetWaybill
            // 
            bttnGetWaybill.AutoSize = false;
            bttnGetWaybill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnGetWaybill.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnGetWaybill.Depth = 0;
            bttnGetWaybill.HighEmphasis = true;
            bttnGetWaybill.Icon = null;
            bttnGetWaybill.Location = new Point(24, 113);
            bttnGetWaybill.Margin = new Padding(4, 6, 4, 6);
            bttnGetWaybill.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetWaybill.Name = "bttnGetWaybill";
            bttnGetWaybill.NoAccentTextColor = Color.Empty;
            bttnGetWaybill.Size = new Size(220, 42);
            bttnGetWaybill.TabIndex = 8;
            bttnGetWaybill.Text = "Getİr";
            bttnGetWaybill.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnGetWaybill.UseAccentColor = false;
            bttnGetWaybill.UseVisualStyleBackColor = true;
            bttnGetWaybill.Click += bttnGetWaybill_Click;
            // 
            // dateTimeFinishDate
            // 
            dateTimeFinishDate.Location = new Point(602, 35);
            dateTimeFinishDate.Name = "dateTimeFinishDate";
            dateTimeFinishDate.ShowCheckBox = true;
            dateTimeFinishDate.Size = new Size(233, 23);
            dateTimeFinishDate.TabIndex = 14;
            dateTimeFinishDate.Value = new DateTime(2025, 7, 8, 0, 0, 0, 0);
            // 
            // dateTimeStartDate
            // 
            dateTimeStartDate.CustomFormat = "2025-08-08 00:00:00.000";
            dateTimeStartDate.Location = new Point(339, 35);
            dateTimeStartDate.Name = "dateTimeStartDate";
            dateTimeStartDate.ShowCheckBox = true;
            dateTimeStartDate.Size = new Size(228, 23);
            dateTimeStartDate.TabIndex = 13;
            dateTimeStartDate.Value = new DateTime(2025, 7, 8, 0, 0, 0, 0);
            // 
            // lblFinishDate
            // 
            lblFinishDate.AutoSize = true;
            lblFinishDate.Depth = 0;
            lblFinishDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblFinishDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblFinishDate.Location = new Point(602, 8);
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
            dataGridInvoiceList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridInvoiceList.Columns.AddRange(new DataGridViewColumn[] { chk });
            dataGridInvoiceList.Cursor = Cursors.Hand;
            dataGridInvoiceList.GridColor = SystemColors.ControlDark;
            dataGridInvoiceList.ImeMode = ImeMode.On;
            dataGridInvoiceList.Location = new Point(6, 235);
            dataGridInvoiceList.Name = "dataGridInvoiceList";
            dataGridInvoiceList.RowHeadersWidth = 51;
            dataGridInvoiceList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridInvoiceList.Size = new Size(875, 428);
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
            divider.Size = new Size(894, 10);
            divider.TabIndex = 20;
            // 
            // chckAll
            // 
            chckAll.AutoSize = true;
            chckAll.BackColor = SystemColors.Control;
            chckAll.CheckAlign = ContentAlignment.MiddleRight;
            chckAll.Cursor = Cursors.Hand;
            chckAll.FlatStyle = FlatStyle.Popup;
            chckAll.Font = new Font("Segoe Fluent Icons", 9F);
            chckAll.Location = new Point(62, 240);
            chckAll.Name = "chckAll";
            chckAll.Size = new Size(75, 16);
            chckAll.TabIndex = 23;
            chckAll.Text = "Tümünü Seç";
            chckAll.UseVisualStyleBackColor = true;
            chckAll.CheckedChanged += chckAll_CheckedChanged;
            // 
            // CollectionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(889, 667);
            Controls.Add(chckAll);
            Controls.Add(stripInvoice);
            Controls.Add(materialCard1);
            Controls.Add(dataGridInvoiceList);
            Controls.Add(divider);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CollectionForm";
            Text = "Transfer Collection";
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
        private MaterialSkin.Controls.MaterialButton bttnSendWaybill;
        private MaterialSkin.Controls.MaterialLabel lblStartDate;
        private MaterialSkin.Controls.MaterialButton bttnGetWaybill;
        private DateTimePicker dateTimeFinishDate;
        private DateTimePicker dateTimeStartDate;
        private MaterialSkin.Controls.MaterialLabel lblFinishDate;
        private DataGridView dataGridInvoiceList;
        private MaterialSkin.Controls.MaterialDivider divider;
        private DataGridViewCheckBoxColumn chk;
        private CheckBox chckAll;
    }
}