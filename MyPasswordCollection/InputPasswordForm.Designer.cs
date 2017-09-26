namespace MyPasswordCollection
{
    partial class InputPasswordForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOk = new System.Windows.Forms.Button();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.tbConfirm = new System.Windows.Forms.TextBox();
            this.cbShowPassword = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(187, 67);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Location = new System.Drawing.Point(8, 15);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(56, 13);
            this.lblNewPassword.TabIndex = 3;
            this.lblNewPassword.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(99, 12);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(165, 20);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Location = new System.Drawing.Point(8, 41);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(93, 13);
            this.lblConfirm.TabIndex = 3;
            this.lblConfirm.Text = "Confirm password:";
            // 
            // tbConfirm
            // 
            this.tbConfirm.Location = new System.Drawing.Point(99, 38);
            this.tbConfirm.Name = "tbConfirm";
            this.tbConfirm.Size = new System.Drawing.Size(165, 20);
            this.tbConfirm.TabIndex = 2;
            this.tbConfirm.UseSystemPasswordChar = true;
            // 
            // cbShowPassword
            // 
            this.cbShowPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowPassword.AutoSize = true;
            this.cbShowPassword.Location = new System.Drawing.Point(11, 73);
            this.cbShowPassword.Name = "cbShowPassword";
            this.cbShowPassword.Size = new System.Drawing.Size(104, 17);
            this.cbShowPassword.TabIndex = 4;
            this.cbShowPassword.Text = "show passwords";
            this.cbShowPassword.UseVisualStyleBackColor = true;
            this.cbShowPassword.CheckedChanged += new System.EventHandler(this.cbShowPassword_CheckedChanged);
            // 
            // InputPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 101);
            this.Controls.Add(this.cbShowPassword);
            this.Controls.Add(this.tbConfirm);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "InputPasswordForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter password";
            this.Shown += new System.EventHandler(this.InputPasswordForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputPasswordForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox tbConfirm;
        private System.Windows.Forms.CheckBox cbShowPassword;
    }
}