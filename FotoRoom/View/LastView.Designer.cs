namespace FotoRoom.View
{
    partial class LastView
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
            this.components = new System.ComponentModel.Container();
            this.pbTextUp = new System.Windows.Forms.PictureBox();
            this.pbMustache = new System.Windows.Forms.PictureBox();
            this.pbTextDown = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbTextUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMustache)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTextDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pbTextUp
            // 
            this.pbTextUp.Location = new System.Drawing.Point(181, 71);
            this.pbTextUp.Name = "pbTextUp";
            this.pbTextUp.Size = new System.Drawing.Size(924, 335);
            this.pbTextUp.TabIndex = 0;
            this.pbTextUp.TabStop = false;
            // 
            // pbMustache
            // 
            this.pbMustache.Location = new System.Drawing.Point(433, 495);
            this.pbMustache.Name = "pbMustache";
            this.pbMustache.Size = new System.Drawing.Size(449, 151);
            this.pbMustache.TabIndex = 1;
            this.pbMustache.TabStop = false;
            // 
            // pbTextDown
            // 
            this.pbTextDown.Location = new System.Drawing.Point(181, 734);
            this.pbTextDown.Name = "pbTextDown";
            this.pbTextDown.Size = new System.Drawing.Size(924, 169);
            this.pbTextDown.TabIndex = 2;
            this.pbTextDown.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LastView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.pbTextDown);
            this.Controls.Add(this.pbMustache);
            this.Controls.Add(this.pbTextUp);
            this.Name = "LastView";
            this.Size = new System.Drawing.Size(1280, 1024);
            this.Load += new System.EventHandler(this.LastView_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LastView_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pbTextUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMustache)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTextDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTextUp;
        private System.Windows.Forms.PictureBox pbMustache;
        private System.Windows.Forms.PictureBox pbTextDown;
        private System.Windows.Forms.Timer timer1;

    }
}
