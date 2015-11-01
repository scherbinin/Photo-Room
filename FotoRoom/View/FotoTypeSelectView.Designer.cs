namespace FotoRoom.View
{
    partial class FotoTypeSelectView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb1PhotoBigPhoto = new System.Windows.Forms.PictureBox();
            this.pb1Photo4 = new System.Windows.Forms.PictureBox();
            this.pb1BigPhoto = new System.Windows.Forms.PictureBox();
            this.pb2Strips4Photo = new System.Windows.Forms.PictureBox();
            this.pb2Strips3Photo = new System.Windows.Forms.PictureBox();
            this.pbTitleText = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb1PhotoBigPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb1Photo4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb1BigPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2Strips4Photo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2Strips3Photo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitleText)).BeginInit();
            this.SuspendLayout();
            // 
            // pb1PhotoBigPhoto
            // 
            this.pb1PhotoBigPhoto.Location = new System.Drawing.Point(19, 185);
            this.pb1PhotoBigPhoto.Name = "pb1PhotoBigPhoto";
            this.pb1PhotoBigPhoto.Size = new System.Drawing.Size(400, 290);
            this.pb1PhotoBigPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb1PhotoBigPhoto.TabIndex = 7;
            this.pb1PhotoBigPhoto.TabStop = false;
            this.pb1PhotoBigPhoto.Click += new System.EventHandler(this.rbFoto2_CheckedChanged);
            // 
            // pb1Photo4
            // 
            this.pb1Photo4.Location = new System.Drawing.Point(464, 352);
            this.pb1Photo4.Name = "pb1Photo4";
            this.pb1Photo4.Size = new System.Drawing.Size(344, 513);
            this.pb1Photo4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb1Photo4.TabIndex = 8;
            this.pb1Photo4.TabStop = false;
            this.pb1Photo4.Click += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // pb1BigPhoto
            // 
            this.pb1BigPhoto.Location = new System.Drawing.Point(855, 185);
            this.pb1BigPhoto.Name = "pb1BigPhoto";
            this.pb1BigPhoto.Size = new System.Drawing.Size(400, 290);
            this.pb1BigPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb1BigPhoto.TabIndex = 9;
            this.pb1BigPhoto.TabStop = false;
            this.pb1BigPhoto.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // pb2Strips4Photo
            // 
            this.pb2Strips4Photo.Location = new System.Drawing.Point(33, 528);
            this.pb2Strips4Photo.Name = "pb2Strips4Photo";
            this.pb2Strips4Photo.Size = new System.Drawing.Size(365, 482);
            this.pb2Strips4Photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb2Strips4Photo.TabIndex = 10;
            this.pb2Strips4Photo.TabStop = false;
            this.pb2Strips4Photo.Click += new System.EventHandler(this.rbFoto1_CheckedChanged);
            // 
            // pb2Strips3Photo
            // 
            this.pb2Strips3Photo.Location = new System.Drawing.Point(879, 528);
            this.pb2Strips3Photo.Name = "pb2Strips3Photo";
            this.pb2Strips3Photo.Size = new System.Drawing.Size(365, 482);
            this.pb2Strips3Photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb2Strips3Photo.TabIndex = 11;
            this.pb2Strips3Photo.TabStop = false;
            this.pb2Strips3Photo.Click += new System.EventHandler(this.rbFoto4_CheckedChanged);
            // 
            // pbTitleText
            // 
            this.pbTitleText.Location = new System.Drawing.Point(267, 35);
            this.pbTitleText.Name = "pbTitleText";
            this.pbTitleText.Size = new System.Drawing.Size(735, 122);
            this.pbTitleText.TabIndex = 12;
            this.pbTitleText.TabStop = false;
            // 
            // FotoTypeSelectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbTitleText);
            this.Controls.Add(this.pb2Strips3Photo);
            this.Controls.Add(this.pb2Strips4Photo);
            this.Controls.Add(this.pb1BigPhoto);
            this.Controls.Add(this.pb1Photo4);
            this.Controls.Add(this.pb1PhotoBigPhoto);
            this.Name = "FotoTypeSelectView";
            this.Size = new System.Drawing.Size(1280, 1024);
            this.Load += new System.EventHandler(this.FotoTypeSelectView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb1PhotoBigPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb1Photo4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb1BigPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2Strips4Photo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2Strips3Photo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitleText)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb1PhotoBigPhoto;
        private System.Windows.Forms.PictureBox pb1Photo4;
        private System.Windows.Forms.PictureBox pb1BigPhoto;
        private System.Windows.Forms.PictureBox pb2Strips4Photo;
        private System.Windows.Forms.PictureBox pb2Strips3Photo;
        private System.Windows.Forms.PictureBox pbTitleText;
    }
}
