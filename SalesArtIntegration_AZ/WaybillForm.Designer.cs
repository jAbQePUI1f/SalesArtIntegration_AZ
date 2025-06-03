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
            stripInvoice = new MenuStrip();
            menüToolStripMenuItem = new ToolStripMenuItem();
            çıkışToolStripMenuItem = new ToolStripMenuItem();
            çıkışToolStripMenuItem1 = new ToolStripMenuItem();
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
            SuspendLayout();
            // 
            // stripInvoice
            // 
            stripInvoice.AllowMerge = false;
            stripInvoice.BackColor = Color.Transparent;
            stripInvoice.BackgroundImage = Properties.Resources.logo_1920;
            stripInvoice.BackgroundImageLayout = ImageLayout.Zoom;
            stripInvoice.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            stripInvoice.Items.AddRange(new ToolStripItem[] { menüToolStripMenuItem, çıkışToolStripMenuItem1 });
            stripInvoice.Location = new Point(0, 0);
            stripInvoice.Name = "stripInvoice";
            stripInvoice.RenderMode = ToolStripRenderMode.Professional;
            stripInvoice.Size = new Size(1002, 24);
            stripInvoice.TabIndex = 4;
            stripInvoice.Text = "Fatura Menü";
            // 
            // menüToolStripMenuItem
            // 
            menüToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { çıkışToolStripMenuItem });
            menüToolStripMenuItem.Name = "menüToolStripMenuItem";
            menüToolStripMenuItem.Size = new Size(50, 20);
            menüToolStripMenuItem.Text = "Menu";
            // 
            // çıkışToolStripMenuItem
            // 
            çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            çıkışToolStripMenuItem.Size = new Size(180, 22);
            çıkışToolStripMenuItem.Text = "Turn Back";
            çıkışToolStripMenuItem.Click += çıkışToolStripMenuItem_Click;
            // 
            // çıkışToolStripMenuItem1
            // 
            çıkışToolStripMenuItem1.Name = "çıkışToolStripMenuItem1";
            çıkışToolStripMenuItem1.Size = new Size(38, 20);
            çıkışToolStripMenuItem1.Text = "Exit";
            çıkışToolStripMenuItem1.Click += çıkışToolStripMenuItem1_Click;
            // 
            // bttnSendWaybill
            // 
            bttnSendWaybill.AutoSize = false;
            bttnSendWaybill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnSendWaybill.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnSendWaybill.Depth = 0;
            bttnSendWaybill.HighEmphasis = true;
            bttnSendWaybill.Icon = null;
            bttnSendWaybill.Location = new Point(729, 139);
            bttnSendWaybill.Margin = new Padding(4, 6, 4, 6);
            bttnSendWaybill.MouseState = MaterialSkin.MouseState.HOVER;
            bttnSendWaybill.Name = "bttnSendWaybill";
            bttnSendWaybill.NoAccentTextColor = Color.Empty;
            bttnSendWaybill.Size = new Size(200, 42);
            bttnSendWaybill.TabIndex = 15;
            bttnSendWaybill.Text = "Send Waybill";
            bttnSendWaybill.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnSendWaybill.UseAccentColor = false;
            bttnSendWaybill.UseVisualStyleBackColor = true;
            // 
            // dateTimeFinishDate
            // 
            dateTimeFinishDate.Location = new Point(729, 67);
            dateTimeFinishDate.Name = "dateTimeFinishDate";
            dateTimeFinishDate.Size = new Size(200, 23);
            dateTimeFinishDate.TabIndex = 14;
            // 
            // dateTimeStartDate
            // 
            dateTimeStartDate.Location = new Point(488, 68);
            dateTimeStartDate.Name = "dateTimeStartDate";
            dateTimeStartDate.Size = new Size(200, 23);
            dateTimeStartDate.TabIndex = 13;
            // 
            // lblFinishDate
            // 
            lblFinishDate.AutoSize = true;
            lblFinishDate.Depth = 0;
            lblFinishDate.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblFinishDate.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblFinishDate.Location = new Point(729, 40);
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
            lblStartDate.Location = new Point(488, 42);
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
            comboboxInvoiceType.Location = new Point(16, 56);
            comboboxInvoiceType.MaxDropDownItems = 4;
            comboboxInvoiceType.MouseState = MaterialSkin.MouseState.OUT;
            comboboxInvoiceType.Name = "comboboxInvoiceType";
            comboboxInvoiceType.Size = new Size(286, 49);
            comboboxInvoiceType.StartIndex = 0;
            comboboxInvoiceType.TabIndex = 10;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Depth = 0;
            lblType.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblType.FontType = MaterialSkin.MaterialSkinManager.fontType.Caption;
            lblType.Location = new Point(16, 33);
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
            bttnGetWaybill.Location = new Point(17, 138);
            bttnGetWaybill.Margin = new Padding(4, 6, 4, 6);
            bttnGetWaybill.MouseState = MaterialSkin.MouseState.HOVER;
            bttnGetWaybill.Name = "bttnGetWaybill";
            bttnGetWaybill.NoAccentTextColor = Color.Empty;
            bttnGetWaybill.Size = new Size(220, 42);
            bttnGetWaybill.TabIndex = 8;
            bttnGetWaybill.Text = "Get Waybill";
            bttnGetWaybill.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnGetWaybill.UseAccentColor = false;
            bttnGetWaybill.UseVisualStyleBackColor = true;
            // 
            // dataGridInvoiceList
            // 
            dataGridInvoiceList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridInvoiceList.Location = new Point(4, 204);
            dataGridInvoiceList.Name = "dataGridInvoiceList";
            dataGridInvoiceList.Size = new Size(992, 428);
            dataGridInvoiceList.TabIndex = 17;
            // 
            // divider
            // 
            divider.BackColor = SystemColors.GradientActiveCaption;
            divider.Depth = 0;
            divider.Location = new Point(-1, 190);
            divider.MouseState = MaterialSkin.MouseState.HOVER;
            divider.Name = "divider";
            divider.Size = new Size(1003, 10);
            divider.TabIndex = 16;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(7, 23);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new Size(989, 164);
            materialCard1.TabIndex = 18;
            // 
            // WaybillForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1002, 633);
            Controls.Add(dataGridInvoiceList);
            Controls.Add(divider);
            Controls.Add(bttnSendWaybill);
            Controls.Add(dateTimeFinishDate);
            Controls.Add(dateTimeStartDate);
            Controls.Add(lblFinishDate);
            Controls.Add(lblStartDate);
            Controls.Add(comboboxInvoiceType);
            Controls.Add(lblType);
            Controls.Add(bttnGetWaybill);
            Controls.Add(stripInvoice);
            Controls.Add(materialCard1);
            Name = "WaybillForm";
            Text = "WaybillForm";
            stripInvoice.ResumeLayout(false);
            stripInvoice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridInvoiceList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip stripInvoice;
        private ToolStripMenuItem menüToolStripMenuItem;
        private ToolStripMenuItem çıkışToolStripMenuItem;
        private ToolStripMenuItem çıkışToolStripMenuItem1;
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
    }
}