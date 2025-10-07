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
            logListBox.ItemHeight = 20;
            logListBox.Location = new Point(3, 74);
            logListBox.Name = "logListBox";
            logListBox.SelectionMode = SelectionMode.MultiExtended;
            logListBox.Size = new Size(794, 484);
            logListBox.TabIndex = 0;
            logListBox.SelectedIndexChanged += logListBox_SelectedIndexChanged;
            // 
            // loqLabel
            // 
            loqLabel.AutoSize = true;
            loqLabel.Font = new Font("Segoe UI Semibold", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            loqLabel.Location = new Point(3, 9);
            loqLabel.Name = "loqLabel";
            loqLabel.Size = new Size(216, 50);
            loqLabel.TabIndex = 1;
            loqLabel.Text = "Log History";
            loqLabel.Click += loqLabel_Click;
            // 
            // DataIntegrationLogs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 559);
            Controls.Add(loqLabel);
            Controls.Add(logListBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DataIntegrationLogs";
            Text = "DataIntegrationLogs";
            Load += DataIntegrationLogs_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox logListBox;
        private Label loqLabel;
    }
}