namespace SalesArtIntegration_AZ
{
    partial class invoiceListLogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(invoiceListLogs));
            invoiceLogsDelete = new MaterialSkin.Controls.MaterialButton();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // invoiceLogsDelete
            // 
            invoiceLogsDelete.AutoSize = false;
            invoiceLogsDelete.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            invoiceLogsDelete.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            invoiceLogsDelete.Depth = 0;
            invoiceLogsDelete.FlatStyle = FlatStyle.Popup;
            invoiceLogsDelete.HighEmphasis = false;
            invoiceLogsDelete.Icon = null;
            invoiceLogsDelete.Location = new Point(562, 5);
            invoiceLogsDelete.Margin = new Padding(4, 6, 4, 6);
            invoiceLogsDelete.MouseState = MaterialSkin.MouseState.HOVER;
            invoiceLogsDelete.Name = "invoiceLogsDelete";
            invoiceLogsDelete.NoAccentTextColor = Color.Empty;
            invoiceLogsDelete.Size = new Size(116, 36);
            invoiceLogsDelete.TabIndex = 1;
            invoiceLogsDelete.Text = "Clear";
            invoiceLogsDelete.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            invoiceLogsDelete.UseAccentColor = false;
            invoiceLogsDelete.UseVisualStyleBackColor = true;
            invoiceLogsDelete.Click += invoiceLogsDelete_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.HorizontalScrollbar = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(3, 50);
            listBox1.Name = "listBox1";
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.Size = new Size(675, 514);
            listBox1.TabIndex = 2;
            // 
            // invoiceListLogs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 567);
            Controls.Add(listBox1);
            Controls.Add(invoiceLogsDelete);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "invoiceListLogs";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "SalesArt Integrator";
            Load += invoiceListLogs_Load;
            ResumeLayout(false);
        }

        #endregion
        private MaterialSkin.Controls.MaterialButton invoiceLogsDelete;
        private ListBox listBox1;
    }
}