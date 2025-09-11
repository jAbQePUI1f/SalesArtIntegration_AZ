namespace SalesArtIntegration_AZ.Resources
{
    partial class DataIntegrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataIntegrationForm));
            bttnTransferToCustomer = new MaterialSkin.Controls.MaterialButton();
            bttnTransferToProducts = new MaterialSkin.Controls.MaterialButton();
            menuStrip1 = new MenuStrip();
            menüToolStripMenuItem = new ToolStripMenuItem();
            anaMenüyeDönToolStripMenuItem = new ToolStripMenuItem();
            çıkışToolStripMenuItem1 = new ToolStripMenuItem();
            bttnLogs = new MaterialSkin.Controls.MaterialButton();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // bttnTransferToCustomer
            // 
            bttnTransferToCustomer.AutoSize = false;
            bttnTransferToCustomer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnTransferToCustomer.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnTransferToCustomer.Depth = 0;
            bttnTransferToCustomer.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            bttnTransferToCustomer.FlatAppearance.BorderSize = 8;
            bttnTransferToCustomer.HighEmphasis = true;
            bttnTransferToCustomer.Icon = null;
            bttnTransferToCustomer.Location = new Point(167, 398);
            bttnTransferToCustomer.Margin = new Padding(4, 6, 4, 6);
            bttnTransferToCustomer.MouseState = MaterialSkin.MouseState.HOVER;
            bttnTransferToCustomer.Name = "bttnTransferToCustomer";
            bttnTransferToCustomer.NoAccentTextColor = Color.Empty;
            bttnTransferToCustomer.Padding = new Padding(10);
            bttnTransferToCustomer.Size = new Size(207, 59);
            bttnTransferToCustomer.TabIndex = 2;
            bttnTransferToCustomer.Text = "Müşterı Aktar";
            bttnTransferToCustomer.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnTransferToCustomer.UseAccentColor = false;
            bttnTransferToCustomer.UseVisualStyleBackColor = true;
            bttnTransferToCustomer.Click += bttnTransferToCustomer_Click_1;
            // 
            // bttnTransferToProducts
            // 
            bttnTransferToProducts.AutoSize = false;
            bttnTransferToProducts.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnTransferToProducts.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnTransferToProducts.Depth = 0;
            bttnTransferToProducts.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            bttnTransferToProducts.FlatAppearance.BorderSize = 8;
            bttnTransferToProducts.HighEmphasis = true;
            bttnTransferToProducts.Icon = null;
            bttnTransferToProducts.Location = new Point(500, 398);
            bttnTransferToProducts.Margin = new Padding(4, 6, 4, 6);
            bttnTransferToProducts.MouseState = MaterialSkin.MouseState.HOVER;
            bttnTransferToProducts.Name = "bttnTransferToProducts";
            bttnTransferToProducts.NoAccentTextColor = Color.Empty;
            bttnTransferToProducts.Padding = new Padding(10);
            bttnTransferToProducts.Size = new Size(209, 59);
            bttnTransferToProducts.TabIndex = 3;
            bttnTransferToProducts.Text = "Ürün Aktar";
            bttnTransferToProducts.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnTransferToProducts.UseAccentColor = false;
            bttnTransferToProducts.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            menuStrip1.BackColor = Color.White;
            menuStrip1.BackgroundImageLayout = ImageLayout.Zoom;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            menuStrip1.ImeMode = ImeMode.Katakana;
            menuStrip1.Items.AddRange(new ToolStripItem[] { menüToolStripMenuItem, çıkışToolStripMenuItem1 });
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip1.Location = new Point(752, 9);
            menuStrip1.MdiWindowListItem = çıkışToolStripMenuItem1;
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.RightToLeft = RightToLeft.No;
            menuStrip1.Size = new Size(96, 24);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "stripSplash";
            // 
            // menüToolStripMenuItem
            // 
            menüToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { anaMenüyeDönToolStripMenuItem });
            menüToolStripMenuItem.Name = "menüToolStripMenuItem";
            menüToolStripMenuItem.Size = new Size(50, 20);
            menüToolStripMenuItem.Text = "Menu";
            // 
            // anaMenüyeDönToolStripMenuItem
            // 
            anaMenüyeDönToolStripMenuItem.Name = "anaMenüyeDönToolStripMenuItem";
            anaMenüyeDönToolStripMenuItem.Size = new Size(165, 22);
            anaMenüyeDönToolStripMenuItem.Text = "Ana Menüye dön";
            anaMenüyeDönToolStripMenuItem.Click += anaMenüyeDönToolStripMenuItem_Click;
            // 
            // çıkışToolStripMenuItem1
            // 
            çıkışToolStripMenuItem1.Name = "çıkışToolStripMenuItem1";
            çıkışToolStripMenuItem1.Size = new Size(38, 20);
            çıkışToolStripMenuItem1.Text = "Exit";
            çıkışToolStripMenuItem1.Click += çıkışToolStripMenuItem1_Click;
            // 
            // bttnLogs
            // 
            bttnLogs.AutoSize = false;
            bttnLogs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnLogs.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnLogs.Depth = 0;
            bttnLogs.HighEmphasis = true;
            bttnLogs.Icon = (Image)resources.GetObject("bttnLogs.Icon");
            bttnLogs.Location = new Point(679, 489);
            bttnLogs.Margin = new Padding(4);
            bttnLogs.MouseState = MaterialSkin.MouseState.HOVER;
            bttnLogs.Name = "bttnLogs";
            bttnLogs.NoAccentTextColor = Color.Empty;
            bttnLogs.Size = new Size(163, 46);
            bttnLogs.TabIndex = 18;
            bttnLogs.Text = "Logs";
            bttnLogs.TextAlign = ContentAlignment.MiddleLeft;
            bttnLogs.TextImageRelation = TextImageRelation.TextBeforeImage;
            bttnLogs.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            bttnLogs.UseAccentColor = false;
            bttnLogs.UseVisualStyleBackColor = true;
            // 
            // DataIntegrationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(848, 538);
            ControlBox = false;
            Controls.Add(bttnLogs);
            Controls.Add(menuStrip1);
            Controls.Add(bttnTransferToProducts);
            Controls.Add(bttnTransferToCustomer);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "DataIntegrationForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SalesArt Integration";
            Load += DataIntegrationForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialButton bttnTransferToCustomer;
        private MaterialSkin.Controls.MaterialButton bttnTransferToProducts;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menüToolStripMenuItem;
        private ToolStripMenuItem çıkışToolStripMenuItem1;
        private ToolStripMenuItem anaMenüyeDönToolStripMenuItem;
        private MaterialSkin.Controls.MaterialButton bttnLogs;
    }
}