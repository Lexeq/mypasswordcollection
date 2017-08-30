namespace MyPasswordCollection
{
    partial class AccauntInfo
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbSite = new System.Windows.Forms.TextBox();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnSiteToClipboard = new System.Windows.Forms.Button();
            this.btnLoginToClipboard = new System.Windows.Forms.Button();
            this.btnPasswordToClipboard = new System.Windows.Forms.Button();
            this.cbShowPassword = new System.Windows.Forms.CheckBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbSite
            // 
            this.tbSite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSite.Location = new System.Drawing.Point(3, 3);
            this.tbSite.Name = "tbSite";
            this.tbSite.Size = new System.Drawing.Size(151, 20);
            this.tbSite.TabIndex = 0;
            // 
            // tbLogin
            // 
            this.tbLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogin.Location = new System.Drawing.Point(3, 29);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(151, 20);
            this.tbLogin.TabIndex = 1;
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(3, 55);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(151, 20);
            this.tbPassword.TabIndex = 2;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnSiteToClipboard
            // 
            this.btnSiteToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSiteToClipboard.Location = new System.Drawing.Point(160, 1);
            this.btnSiteToClipboard.Name = "btnSiteToClipboard";
            this.btnSiteToClipboard.Size = new System.Drawing.Size(75, 23);
            this.btnSiteToClipboard.TabIndex = 3;
            this.btnSiteToClipboard.Text = "Copy";
            this.btnSiteToClipboard.UseVisualStyleBackColor = true;
            this.btnSiteToClipboard.Click += new System.EventHandler(this.btnSiteToClipboard_Click);
            // 
            // btnLoginToClipboard
            // 
            this.btnLoginToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoginToClipboard.Location = new System.Drawing.Point(160, 27);
            this.btnLoginToClipboard.Name = "btnLoginToClipboard";
            this.btnLoginToClipboard.Size = new System.Drawing.Size(75, 23);
            this.btnLoginToClipboard.TabIndex = 3;
            this.btnLoginToClipboard.Text = "Copy";
            this.btnLoginToClipboard.UseVisualStyleBackColor = true;
            this.btnLoginToClipboard.Click += new System.EventHandler(this.btnLoginToClipboard_Click);
            // 
            // btnPasswordToClipboard
            // 
            this.btnPasswordToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPasswordToClipboard.Location = new System.Drawing.Point(160, 53);
            this.btnPasswordToClipboard.Name = "btnPasswordToClipboard";
            this.btnPasswordToClipboard.Size = new System.Drawing.Size(75, 23);
            this.btnPasswordToClipboard.TabIndex = 3;
            this.btnPasswordToClipboard.Text = "Copy";
            this.btnPasswordToClipboard.UseVisualStyleBackColor = true;
            this.btnPasswordToClipboard.Click += new System.EventHandler(this.btnPasswordToClipboard_Click);
            // 
            // cbShowPassword
            // 
            this.cbShowPassword.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbShowPassword.AutoSize = true;
            this.cbShowPassword.Location = new System.Drawing.Point(55, 81);
            this.cbShowPassword.Name = "cbShowPassword";
            this.cbShowPassword.Size = new System.Drawing.Size(99, 17);
            this.cbShowPassword.TabIndex = 4;
            this.cbShowPassword.Text = "show password";
            this.cbShowPassword.UseVisualStyleBackColor = true;
            this.cbShowPassword.CheckedChanged += new System.EventHandler(this.cbShowPassword_CheckedChanged);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(160, 104);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(79, 104);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(3, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AccauntInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.cbShowPassword);
            this.Controls.Add(this.btnPasswordToClipboard);
            this.Controls.Add(this.btnLoginToClipboard);
            this.Controls.Add(this.btnSiteToClipboard);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.tbSite);
            this.Name = "AccauntInfo";
            this.Size = new System.Drawing.Size(240, 134);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSite;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnSiteToClipboard;
        private System.Windows.Forms.Button btnLoginToClipboard;
        private System.Windows.Forms.Button btnPasswordToClipboard;
        private System.Windows.Forms.CheckBox cbShowPassword;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
