namespace SalesArtIntegration_AZ
{
    partial class DataIntegrationLogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataIntegrationLogs));
            logListBox = new ListBox();
            loqLabel = new Label();
            SuspendLayout();
            // 
            // logListBox
            // 
            logListBox.BackColor = SystemColors.HighlightText;
            logListBox.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            logListBox.FormattingEnabled = true;
            logListBox.ItemHeight = 25;
            logListBox.Location = new Point(3, 99);
            logListBox.Margin = new Padding(3, 4, 3, 4);
            logListBox.Name = "logListBox";
            logListBox.SelectionMode = SelectionMode.MultiExtended;
            logListBox.Size = new Size(907, 629);
            logListBox.TabIndex = 0;
            logListBox.SelectedIndexChanged += logListBox_SelectedIndexChanged;
            // 
            // loqLabel
            // 
            loqLabel.AutoSize = true;
            loqLabel.Font = new Font("Segoe UI Semibold", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            loqLabel.Location = new Point(3, 12);
            loqLabel.Name = "loqLabel";
            loqLabel.Size = new Size(274, 62);
            loqLabel.TabIndex = 1;
            loqLabel.Text = "Log History";
            loqLabel.Click += loqLabel_Click;
            // 
            // DataIntegrationLogs
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 745);
            Controls.Add(loqLabel);
            Controls.Add(logListBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "DataIntegrationLogs";
            Text = "DataIntegrationLogs";
            FormClosed += DataIntegrationLogs_FormClosed;
            Load += DataIntegrationLogs_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox logListBox;
        private Label loqLabel;
    }
}