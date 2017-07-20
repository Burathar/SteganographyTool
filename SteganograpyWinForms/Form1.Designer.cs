namespace SteganograpyWinForms
{
    partial class Form1
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
            this.tbSelSourceImg = new System.Windows.Forms.TextBox();
            this.btnSelSourceImg = new System.Windows.Forms.Button();
            this.radioEncrypt = new System.Windows.Forms.RadioButton();
            this.radioDecrypt = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbSelSourceData = new System.Windows.Forms.TextBox();
            this.btnSelSourceData = new System.Windows.Forms.Button();
            this.tbSelSaveImg = new System.Windows.Forms.TextBox();
            this.btnSelSaveImg = new System.Windows.Forms.Button();
            this.tbSelSaveData = new System.Windows.Forms.TextBox();
            this.btnSelSaveData = new System.Windows.Forms.Button();
            this.checkGrayscale = new System.Windows.Forms.CheckBox();
            this.checkAscii = new System.Windows.Forms.CheckBox();
            this.checkFillRandom = new System.Windows.Forms.CheckBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbSelSourceImg
            // 
            this.tbSelSourceImg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelSourceImg.Location = new System.Drawing.Point(162, 81);
            this.tbSelSourceImg.Name = "tbSelSourceImg";
            this.tbSelSourceImg.Size = new System.Drawing.Size(250, 20);
            this.tbSelSourceImg.TabIndex = 0;
            // 
            // btnSelSourceImg
            // 
            this.btnSelSourceImg.Location = new System.Drawing.Point(12, 79);
            this.btnSelSourceImg.Name = "btnSelSourceImg";
            this.btnSelSourceImg.Size = new System.Drawing.Size(144, 23);
            this.btnSelSourceImg.TabIndex = 1;
            this.btnSelSourceImg.Text = "Select Source Image";
            this.btnSelSourceImg.UseVisualStyleBackColor = true;
            this.btnSelSourceImg.Click += new System.EventHandler(this.btnSelSourceImg_Click);
            // 
            // radioEncrypt
            // 
            this.radioEncrypt.AutoSize = true;
            this.radioEncrypt.Location = new System.Drawing.Point(12, 12);
            this.radioEncrypt.Name = "radioEncrypt";
            this.radioEncrypt.Size = new System.Drawing.Size(61, 17);
            this.radioEncrypt.TabIndex = 2;
            this.radioEncrypt.Text = "Encrypt";
            this.radioEncrypt.UseVisualStyleBackColor = true;
            this.radioEncrypt.CheckedChanged += new System.EventHandler(this.radioEncryptionDecrypyionChanged);
            // 
            // radioDecrypt
            // 
            this.radioDecrypt.AutoSize = true;
            this.radioDecrypt.Location = new System.Drawing.Point(12, 35);
            this.radioDecrypt.Name = "radioDecrypt";
            this.radioDecrypt.Size = new System.Drawing.Size(62, 17);
            this.radioDecrypt.TabIndex = 2;
            this.radioDecrypt.Text = "Decrypt";
            this.radioDecrypt.UseVisualStyleBackColor = true;
            this.radioDecrypt.CheckedChanged += new System.EventHandler(this.radioEncryptionDecrypyionChanged);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(418, 81);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(84, 108);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbSelSourceData
            // 
            this.tbSelSourceData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelSourceData.Location = new System.Drawing.Point(162, 110);
            this.tbSelSourceData.Name = "tbSelSourceData";
            this.tbSelSourceData.Size = new System.Drawing.Size(250, 20);
            this.tbSelSourceData.TabIndex = 0;
            // 
            // btnSelSourceData
            // 
            this.btnSelSourceData.Location = new System.Drawing.Point(12, 108);
            this.btnSelSourceData.Name = "btnSelSourceData";
            this.btnSelSourceData.Size = new System.Drawing.Size(144, 23);
            this.btnSelSourceData.TabIndex = 1;
            this.btnSelSourceData.Text = "Select Source Data";
            this.btnSelSourceData.UseVisualStyleBackColor = true;
            this.btnSelSourceData.Click += new System.EventHandler(this.btnSelSourceData_Click);
            // 
            // tbSelSaveImg
            // 
            this.tbSelSaveImg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelSaveImg.Location = new System.Drawing.Point(162, 139);
            this.tbSelSaveImg.Name = "tbSelSaveImg";
            this.tbSelSaveImg.Size = new System.Drawing.Size(250, 20);
            this.tbSelSaveImg.TabIndex = 0;
            // 
            // btnSelSaveImg
            // 
            this.btnSelSaveImg.Location = new System.Drawing.Point(12, 137);
            this.btnSelSaveImg.Name = "btnSelSaveImg";
            this.btnSelSaveImg.Size = new System.Drawing.Size(144, 23);
            this.btnSelSaveImg.TabIndex = 1;
            this.btnSelSaveImg.Text = "Select Save Image";
            this.btnSelSaveImg.UseVisualStyleBackColor = true;
            this.btnSelSaveImg.Click += new System.EventHandler(this.btnSelSaveImg_Click);
            // 
            // tbSelSaveData
            // 
            this.tbSelSaveData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSelSaveData.Location = new System.Drawing.Point(162, 168);
            this.tbSelSaveData.Name = "tbSelSaveData";
            this.tbSelSaveData.Size = new System.Drawing.Size(250, 20);
            this.tbSelSaveData.TabIndex = 0;
            // 
            // btnSelSaveData
            // 
            this.btnSelSaveData.Location = new System.Drawing.Point(12, 166);
            this.btnSelSaveData.Name = "btnSelSaveData";
            this.btnSelSaveData.Size = new System.Drawing.Size(144, 23);
            this.btnSelSaveData.TabIndex = 1;
            this.btnSelSaveData.Text = "Select Save Data";
            this.btnSelSaveData.UseVisualStyleBackColor = true;
            this.btnSelSaveData.Click += new System.EventHandler(this.btnSelSaveData_Click);
            // 
            // checkGrayscale
            // 
            this.checkGrayscale.AutoSize = true;
            this.checkGrayscale.Location = new System.Drawing.Point(162, 12);
            this.checkGrayscale.Name = "checkGrayscale";
            this.checkGrayscale.Size = new System.Drawing.Size(73, 17);
            this.checkGrayscale.TabIndex = 3;
            this.checkGrayscale.Text = "Grayscale";
            this.checkGrayscale.UseVisualStyleBackColor = true;
            // 
            // checkAscii
            // 
            this.checkAscii.AutoSize = true;
            this.checkAscii.Location = new System.Drawing.Point(162, 35);
            this.checkAscii.Name = "checkAscii";
            this.checkAscii.Size = new System.Drawing.Size(132, 17);
            this.checkAscii.TabIndex = 3;
            this.checkAscii.Text = "ASCII insead of UTF-8";
            this.checkAscii.UseVisualStyleBackColor = true;
            // 
            // checkFillRandom
            // 
            this.checkFillRandom.AutoSize = true;
            this.checkFillRandom.Checked = true;
            this.checkFillRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkFillRandom.Location = new System.Drawing.Point(162, 58);
            this.checkFillRandom.Name = "checkFillRandom";
            this.checkFillRandom.Size = new System.Drawing.Size(238, 17);
            this.checkFillRandom.TabIndex = 3;
            this.checkFillRandom.Text = "Fill available bits after data with random noise";
            this.checkFillRandom.UseVisualStyleBackColor = true;
            // 
            // tbOutput
            // 
            this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutput.Location = new System.Drawing.Point(12, 195);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(490, 126);
            this.tbOutput.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 332);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.checkFillRandom);
            this.Controls.Add(this.checkAscii);
            this.Controls.Add(this.checkGrayscale);
            this.Controls.Add(this.radioDecrypt);
            this.Controls.Add(this.radioEncrypt);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSelSourceData);
            this.Controls.Add(this.tbSelSourceData);
            this.Controls.Add(this.btnSelSaveData);
            this.Controls.Add(this.tbSelSaveData);
            this.Controls.Add(this.btnSelSaveImg);
            this.Controls.Add(this.tbSelSaveImg);
            this.Controls.Add(this.btnSelSourceImg);
            this.Controls.Add(this.tbSelSourceImg);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(417, 268);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Steganography Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSelSourceImg;
        private System.Windows.Forms.Button btnSelSourceImg;
        private System.Windows.Forms.RadioButton radioEncrypt;
        private System.Windows.Forms.RadioButton radioDecrypt;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbSelSourceData;
        private System.Windows.Forms.Button btnSelSourceData;
        private System.Windows.Forms.TextBox tbSelSaveImg;
        private System.Windows.Forms.Button btnSelSaveImg;
        private System.Windows.Forms.TextBox tbSelSaveData;
        private System.Windows.Forms.Button btnSelSaveData;
        private System.Windows.Forms.CheckBox checkGrayscale;
        private System.Windows.Forms.CheckBox checkAscii;
        private System.Windows.Forms.CheckBox checkFillRandom;
        private System.Windows.Forms.TextBox tbOutput;
    }
}

