using System;
using System.Drawing;
using System.Windows.Forms;

namespace FotoRoom
{
    partial class Adminka
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbProgramBackground = new System.Windows.Forms.PictureBox();
            this.pbPhotoColor = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btnGetBackgrImage = new System.Windows.Forms.Button();
            this.btnGetPhotoFon = new System.Windows.Forms.Button();
            this.btnGetLogo = new System.Windows.Forms.Button();
            this.btnGetSettingsByDefault = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPageValue = new System.Windows.Forms.Label();
            this.BtnPageValReset = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rbtMotovay = new System.Windows.Forms.RadioButton();
            this.rbtGlanec = new System.Windows.Forms.RadioButton();
            this.btnGetPhotoBackground = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgramBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhotoColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(271, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 51);
            this.button1.TabIndex = 0;
            this.button1.Text = "Сохранить и запустить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Location = new System.Drawing.Point(432, 341);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 51);
            this.button2.TabIndex = 1;
            this.button2.Text = "Выйти без сохранения";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Фон программы:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Фон фотографии:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Логотип:";
            // 
            // pbProgramBackground
            // 
            this.pbProgramBackground.Location = new System.Drawing.Point(44, 58);
            this.pbProgramBackground.Name = "pbProgramBackground";
            this.pbProgramBackground.Size = new System.Drawing.Size(103, 87);
            this.pbProgramBackground.TabIndex = 5;
            this.pbProgramBackground.TabStop = false;
            // 
            // pbPhotoColor
            // 
            this.pbPhotoColor.Location = new System.Drawing.Point(243, 58);
            this.pbPhotoColor.Name = "pbPhotoColor";
            this.pbPhotoColor.Size = new System.Drawing.Size(103, 87);
            this.pbPhotoColor.TabIndex = 6;
            this.pbPhotoColor.TabStop = false;
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(443, 58);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(103, 87);
            this.pbLogo.TabIndex = 7;
            this.pbLogo.TabStop = false;
            // 
            // btnGetBackgrImage
            // 
            this.btnGetBackgrImage.Location = new System.Drawing.Point(44, 165);
            this.btnGetBackgrImage.Name = "btnGetBackgrImage";
            this.btnGetBackgrImage.Size = new System.Drawing.Size(75, 23);
            this.btnGetBackgrImage.TabIndex = 8;
            this.btnGetBackgrImage.Text = "Выбрать";
            this.btnGetBackgrImage.UseVisualStyleBackColor = true;
            this.btnGetBackgrImage.Click += new System.EventHandler(this.btnGetBackgrImage_Click);
            // 
            // btnGetPhotoFon
            // 
            this.btnGetPhotoFon.Location = new System.Drawing.Point(243, 165);
            this.btnGetPhotoFon.Name = "btnGetPhotoFon";
            this.btnGetPhotoFon.Size = new System.Drawing.Size(51, 23);
            this.btnGetPhotoFon.TabIndex = 9;
            this.btnGetPhotoFon.Text = "Цвет";
            this.btnGetPhotoFon.UseVisualStyleBackColor = true;
            this.btnGetPhotoFon.Click += new System.EventHandler(this.btnGetPhotoFon_Click);
            // 
            // btnGetLogo
            // 
            this.btnGetLogo.Location = new System.Drawing.Point(443, 165);
            this.btnGetLogo.Name = "btnGetLogo";
            this.btnGetLogo.Size = new System.Drawing.Size(75, 23);
            this.btnGetLogo.TabIndex = 10;
            this.btnGetLogo.Text = "Выбрать";
            this.btnGetLogo.UseVisualStyleBackColor = true;
            this.btnGetLogo.Click += new System.EventHandler(this.btnGetLogo_Click);
            // 
            // btnGetSettingsByDefault
            // 
            this.btnGetSettingsByDefault.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGetSettingsByDefault.Location = new System.Drawing.Point(34, 341);
            this.btnGetSettingsByDefault.Name = "btnGetSettingsByDefault";
            this.btnGetSettingsByDefault.Size = new System.Drawing.Size(155, 51);
            this.btnGetSettingsByDefault.TabIndex = 11;
            this.btnGetSettingsByDefault.Text = "Выставить все по умолчанию";
            this.btnGetSettingsByDefault.UseVisualStyleBackColor = true;
            this.btnGetSettingsByDefault.Click += new System.EventHandler(this.btnGetSettingsByDefault_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.LightGray;
            this.colorDialog1.FullOpen = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.ShowReadOnly = true;
            this.openFileDialog1.Title = "Открыть рисунок";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(13, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(333, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "Количество распечатанных листов:\r\n";
            // 
            // lblPageValue
            // 
            this.lblPageValue.AutoSize = true;
            this.lblPageValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPageValue.Location = new System.Drawing.Point(352, 234);
            this.lblPageValue.Name = "lblPageValue";
            this.lblPageValue.Size = new System.Drawing.Size(18, 20);
            this.lblPageValue.TabIndex = 13;
            this.lblPageValue.Text = "0";
            // 
            // BtnPageValReset
            // 
            this.BtnPageValReset.Location = new System.Drawing.Point(411, 231);
            this.BtnPageValReset.Name = "BtnPageValReset";
            this.BtnPageValReset.Size = new System.Drawing.Size(75, 23);
            this.BtnPageValReset.TabIndex = 14;
            this.BtnPageValReset.Text = "Сбросить";
            this.BtnPageValReset.UseVisualStyleBackColor = true;
            this.BtnPageValReset.Click += new System.EventHandler(this.BtnPageValReset_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(240, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Выбрать:";
            // 
            // rbtMotovay
            // 
            this.rbtMotovay.AutoSize = true;
            this.rbtMotovay.Checked = true;
            this.rbtMotovay.Location = new System.Drawing.Point(17, 277);
            this.rbtMotovay.Name = "rbtMotovay";
            this.rbtMotovay.Size = new System.Drawing.Size(108, 17);
            this.rbtMotovay.TabIndex = 15;
            this.rbtMotovay.TabStop = true;
            this.rbtMotovay.Text = "Матовая бумага";
            this.rbtMotovay.UseVisualStyleBackColor = true;
            this.rbtMotovay.CheckedChanged += new System.EventHandler(this.rbtMotovay_CheckedChanged);
            // 
            // rbtGlanec
            // 
            this.rbtGlanec.AutoSize = true;
            this.rbtGlanec.Location = new System.Drawing.Point(17, 300);
            this.rbtGlanec.Name = "rbtGlanec";
            this.rbtGlanec.Size = new System.Drawing.Size(118, 17);
            this.rbtGlanec.TabIndex = 16;
            this.rbtGlanec.Text = "Глянцевая бумага";
            this.rbtGlanec.UseVisualStyleBackColor = true;
            this.rbtGlanec.CheckedChanged += new System.EventHandler(this.rbtGlanec_CheckedChanged);
            // 
            // btnGetPhotoBackground
            // 
            this.btnGetPhotoBackground.Location = new System.Drawing.Point(295, 165);
            this.btnGetPhotoBackground.Name = "btnGetPhotoBackground";
            this.btnGetPhotoBackground.Size = new System.Drawing.Size(51, 23);
            this.btnGetPhotoBackground.TabIndex = 16;
            this.btnGetPhotoBackground.Text = "Фон";
            this.btnGetPhotoBackground.UseVisualStyleBackColor = true;
            this.btnGetPhotoBackground.Click += new System.EventHandler(this.btnGetPhotoBackground_Click);
            // 
            // Adminka
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(588, 396);
            this.ControlBox = false;
            this.Controls.Add(this.btnGetPhotoBackground);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BtnPageValReset);
            this.Controls.Add(this.lblPageValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnGetSettingsByDefault);
            this.Controls.Add(this.btnGetLogo);
            this.Controls.Add(this.btnGetPhotoFon);
            this.Controls.Add(this.btnGetBackgrImage);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.pbPhotoColor);
            this.Controls.Add(this.pbProgramBackground);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rbtGlanec);
            this.Controls.Add(this.rbtMotovay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Adminka";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Админка";
            this.Load += new System.EventHandler(this.Adminka_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbProgramBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhotoColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private RadioButton rbtMotovay;
        private RadioButton rbtGlanec;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbProgramBackground;
        private System.Windows.Forms.PictureBox pbPhotoColor;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Button btnGetBackgrImage;
        private System.Windows.Forms.Button btnGetPhotoFon;
        private System.Windows.Forms.Button btnGetLogo;
        private System.Windows.Forms.Button btnGetSettingsByDefault;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPageValue;
        private System.Windows.Forms.Button BtnPageValReset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGetPhotoBackground;
    }
}