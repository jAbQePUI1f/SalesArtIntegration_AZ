namespace SalesArtIntegration_AZ
{
    partial class loginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginForm));
            bttnLogin = new MaterialSkin.Controls.MaterialButton();
            txtboxUserName = new MaterialSkin.Controls.MaterialTextBox();
            txtBoxPassword = new MaterialSkin.Controls.MaterialTextBox();
            lblUsername = new MaterialSkin.Controls.MaterialLabel();
            lblPassword = new MaterialSkin.Controls.MaterialLabel();
            materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            rememberMeBox = new MaterialSkin.Controls.MaterialCheckbox();
            SuspendLayout();
            // 
            // bttnLogin
            // 
            bttnLogin.AutoSize = false;
            bttnLogin.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bttnLogin.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Dense;
            bttnLogin.Depth = 0;
            bttnLogin.FlatStyle = FlatStyle.Flat;
            bttnLogin.HighEmphasis = true;
            bttnLogin.Icon = null;
            bttnLogin.Location = new Point(149, 389);
            bttnLogin.Margin = new Padding(4, 6, 4, 6);
            bttnLogin.MouseState = MaterialSkin.MouseState.HOVER;
            bttnLogin.Name = "bttnLogin";
            bttnLogin.NoAccentTextColor = Color.Empty;
            bttnLogin.Size = new Size(191, 46);
            bttnLogin.TabIndex = 0;
            bttnLogin.Text = "Log in";
            bttnLogin.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            bttnLogin.UseAccentColor = false;
            bttnLogin.UseVisualStyleBackColor = true;
            bttnLogin.Click += bttnLogin_Click;
            // 
            // txtboxUserName
            // 
            txtboxUserName.AnimateReadOnly = false;
            txtboxUserName.BorderStyle = BorderStyle.FixedSingle;
            txtboxUserName.Depth = 0;
            txtboxUserName.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtboxUserName.LeadingIcon = null;
            txtboxUserName.Location = new Point(81, 227);
            txtboxUserName.MaxLength = 50;
            txtboxUserName.MouseState = MaterialSkin.MouseState.OUT;
            txtboxUserName.Multiline = false;
            txtboxUserName.Name = "txtboxUserName";
            txtboxUserName.ScrollBars = RichTextBoxScrollBars.None;
            txtboxUserName.Size = new Size(336, 50);
            txtboxUserName.TabIndex = 1;
            txtboxUserName.Text = "";
            txtboxUserName.TrailingIcon = (Image)resources.GetObject("txtboxUserName.TrailingIcon");
            // 
            // txtBoxPassword
            // 
            txtBoxPassword.AnimateReadOnly = false;
            txtBoxPassword.BorderStyle = BorderStyle.FixedSingle;
            txtBoxPassword.Depth = 0;
            txtBoxPassword.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtBoxPassword.LeadingIcon = null;
            txtBoxPassword.LeaveOnEnterKey = true;
            txtBoxPassword.Location = new Point(81, 293);
            txtBoxPassword.MaxLength = 50;
            txtBoxPassword.MouseState = MaterialSkin.MouseState.OUT;
            txtBoxPassword.Multiline = false;
            txtBoxPassword.Name = "txtBoxPassword";
            txtBoxPassword.Password = true;
            txtBoxPassword.ScrollBars = RichTextBoxScrollBars.None;
            txtBoxPassword.Size = new Size(336, 50);
            txtBoxPassword.TabIndex = 2;
            txtBoxPassword.Text = "";
            txtBoxPassword.TrailingIcon = null;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.BackColor = Color.Transparent;
            lblUsername.Depth = 0;
            lblUsername.Font = new Font("Roboto", 12F, FontStyle.Italic, GraphicsUnit.Pixel);
            lblUsername.FontType = MaterialSkin.MaterialSkinManager.fontType.SubtleEmphasis;
            lblUsername.Location = new Point(86, 229);
            lblUsername.MouseState = MaterialSkin.MouseState.HOVER;
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(56, 14);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.Depth = 0;
            lblPassword.Font = new Font("Roboto", 12F, FontStyle.Italic, GraphicsUnit.Pixel);
            lblPassword.FontType = MaterialSkin.MaterialSkinManager.fontType.SubtleEmphasis;
            lblPassword.Location = new Point(84, 295);
            lblPassword.MouseState = MaterialSkin.MouseState.HOVER;
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(55, 14);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.FlatStyle = FlatStyle.Flat;
            materialLabel1.Font = new Font("Roboto", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.Overline;
            materialLabel1.Location = new Point(0, 582);
            materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new Size(210, 13);
            materialLabel1.TabIndex = 5;
            materialLabel1.Text = "© Copyright 2025 SalesArt. All Rights Reserved.";
            // 
            // rememberMeBox
            // 
            rememberMeBox.Checked = true;
            rememberMeBox.CheckState = CheckState.Checked;
            rememberMeBox.Depth = 0;
            rememberMeBox.FlatStyle = FlatStyle.System;
            rememberMeBox.Location = new Point(86, 346);
            rememberMeBox.Margin = new Padding(0);
            rememberMeBox.MouseLocation = new Point(-1, -1);
            rememberMeBox.MouseState = MaterialSkin.MouseState.HOVER;
            rememberMeBox.Name = "rememberMeBox";
            rememberMeBox.ReadOnly = false;
            rememberMeBox.Ripple = false;
            rememberMeBox.Size = new Size(145, 37);
            rememberMeBox.TabIndex = 6;
            rememberMeBox.Text = "Remember Me";
            rememberMeBox.TextAlign = ContentAlignment.MiddleCenter;
            rememberMeBox.UseVisualStyleBackColor = true;
            // 
            // loginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Snow;
            BackgroundImage = Properties.Resources.Login;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(514, 596);
            Controls.Add(rememberMeBox);
            Controls.Add(materialLabel1);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Controls.Add(txtBoxPassword);
            Controls.Add(txtboxUserName);
            Controls.Add(bttnLogin);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "loginForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SalesArt Integration";
            //FormClosing += loginForm_FormClosing;
            FormClosed += loginForm_FormClosed;
            Load += loginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialButton bttnLogin;
        private MaterialSkin.Controls.MaterialTextBox txtboxUserName;
        private MaterialSkin.Controls.MaterialTextBox txtBoxPassword;
        private MaterialSkin.Controls.MaterialLabel lblUsername;
        private MaterialSkin.Controls.MaterialLabel lblPassword;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialCheckbox rememberMeBox;
    }
}
