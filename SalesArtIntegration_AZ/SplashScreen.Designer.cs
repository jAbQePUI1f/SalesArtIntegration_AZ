namespace SalesArtIntegration_AZ
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            bttnWaybill = new MaterialSkin.Controls.MaterialButton();
            bttnCollection = new MaterialSkin.Controls.MaterialButton();
            bttnInvoice = new MaterialSkin.Controls.MaterialButton();
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            dataIntegrationToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // bttnWaybill
            // 
            bttnWaybill.AutoSize = false;
            bttnWaybill.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnWaybill.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnWaybill.Depth = 0;
            bttnWaybill.HighEmphasis = true;
            bttnWaybill.Icon = null;
            bttnWaybill.Location = new Point(81, 15);
            bttnWaybill.Margin = new Padding(4, 6, 4, 6);
            bttnWaybill.MouseState = MaterialSkin.MouseState.HOVER;
            bttnWaybill.Name = "bttnWaybill";
            bttnWaybill.NoAccentTextColor = Color.Empty;
            bttnWaybill.Size = new Size(174, 59);
            bttnWaybill.TabIndex = 0;
            bttnWaybill.Text = "Aktar";
            bttnWaybill.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnWaybill.UseAccentColor = false;
            bttnWaybill.UseVisualStyleBackColor = true;
            bttnWaybill.Visible = false;
            bttnWaybill.Click += bttnWaybill_Click;
            // 
            // bttnCollection
            // 
            bttnCollection.AutoSize = false;
            bttnCollection.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnCollection.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnCollection.Depth = 0;
            bttnCollection.HighEmphasis = true;
            bttnCollection.Icon = null;
            bttnCollection.Location = new Point(399, 397);
            bttnCollection.Margin = new Padding(4, 6, 4, 6);
            bttnCollection.MouseState = MaterialSkin.MouseState.HOVER;
            bttnCollection.Name = "bttnCollection";
            bttnCollection.NoAccentTextColor = Color.Empty;
            bttnCollection.Size = new Size(174, 59);
            bttnCollection.TabIndex = 0;
            bttnCollection.Text = "Aktar";
            bttnCollection.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnCollection.UseAccentColor = false;
            bttnCollection.UseVisualStyleBackColor = true;
            bttnCollection.Click += bttnCollection_Click;
            // 
            // bttnInvoice
            // 
            bttnInvoice.AutoEllipsis = true;
            bttnInvoice.AutoSize = false;
            bttnInvoice.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnInvoice.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            bttnInvoice.Depth = 0;
            bttnInvoice.FlatAppearance.BorderColor = SystemColors.ActiveCaption;
            bttnInvoice.FlatAppearance.BorderSize = 8;
            bttnInvoice.HighEmphasis = true;
            bttnInvoice.Icon = null;
            bttnInvoice.Location = new Point(145, 397);
            bttnInvoice.Margin = new Padding(4, 6, 4, 6);
            bttnInvoice.MouseState = MaterialSkin.MouseState.HOVER;
            bttnInvoice.Name = "bttnInvoice";
            bttnInvoice.NoAccentTextColor = Color.Empty;
            bttnInvoice.Padding = new Padding(10);
            bttnInvoice.Size = new Size(174, 59);
            bttnInvoice.TabIndex = 1;
            bttnInvoice.Text = "Aktar";
            bttnInvoice.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnInvoice.UseAccentColor = false;
            bttnInvoice.UseVisualStyleBackColor = true;
            bttnInvoice.Click += bttnInvoice_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            menuStrip1.BackColor = Color.White;
            menuStrip1.BackgroundImageLayout = ImageLayout.Zoom;
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            menuStrip1.ImeMode = ImeMode.Katakana;
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem, exitToolStripMenuItem1 });
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip1.Location = new Point(496, 9);
            menuStrip1.MdiWindowListItem = exitToolStripMenuItem1;
            menuStrip1.Name = "menuStrip1";
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.RightToLeft = RightToLeft.No;
            menuStrip1.Size = new Size(202, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "stripSplash";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { dataIntegrationToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(129, 20);
            menuToolStripMenuItem.Text = "Menü - Veri Aktarımı";
            // 
            // dataIntegrationToolStripMenuItem
            // 
            dataIntegrationToolStripMenuItem.Name = "dataIntegrationToolStripMenuItem";
            dataIntegrationToolStripMenuItem.Size = new Size(142, 22);
            dataIntegrationToolStripMenuItem.Text = "Veri Aktarımı";
            dataIntegrationToolStripMenuItem.TextImageRelation = TextImageRelation.ImageAboveText;
            dataIntegrationToolStripMenuItem.Click += DataIntegrationToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(65, 20);
            exitToolStripMenuItem1.Text = "Çıkış yap";
            exitToolStripMenuItem1.TextImageRelation = TextImageRelation.Overlay;
            exitToolStripMenuItem1.Click += ExitToolStripMenuItem1_Click;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(707, 538);
            ControlBox = false;
            Controls.Add(bttnInvoice);
            Controls.Add(bttnCollection);
            Controls.Add(bttnWaybill);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "SplashScreen";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SalesArt Integration";
            Load += SplashScreen_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialButton bttnWaybill;
        private MaterialSkin.Controls.MaterialButton bttnCollection;
        private MaterialSkin.Controls.MaterialButton bttnInvoice;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem dataIntegrationToolStripMenuItem;
    }
}