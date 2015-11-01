namespace FotoRoom.View
{
    partial class PrintValueSetView
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
            this.pbBtnDown = new System.Windows.Forms.PictureBox();
            this.pbBtnUp = new System.Windows.Forms.PictureBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.pbText = new System.Windows.Forms.PictureBox();
            this.pbBtnPrint = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBtnDown
            // 
            this.pbBtnDown.Location = new System.Drawing.Point(381, 413);
            this.pbBtnDown.Name = "pbBtnDown";
            this.pbBtnDown.Size = new System.Drawing.Size(105, 96);
            this.pbBtnDown.TabIndex = 4;
            this.pbBtnDown.TabStop = false;
            // 
            // pbBtnUp
            // 
            this.pbBtnUp.Location = new System.Drawing.Point(811, 413);
            this.pbBtnUp.Name = "pbBtnUp";
            this.pbBtnUp.Size = new System.Drawing.Size(105, 96);
            this.pbBtnUp.TabIndex = 5;
            this.pbBtnUp.TabStop = false;
            // 
            // lblValue
            // 
            this.lblValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 110.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblValue.ForeColor = System.Drawing.Color.White;
            this.lblValue.Location = new System.Drawing.Point(580, 373);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(136, 136);
            this.lblValue.TabIndex = 6;
            this.lblValue.Text = "1";
            // 
            // pbText
            // 
            this.pbText.Location = new System.Drawing.Point(317, 141);
            this.pbText.Name = "pbText";
            this.pbText.Size = new System.Drawing.Size(667, 111);
            this.pbText.TabIndex = 7;
            this.pbText.TabStop = false;
            // 
            // pbBtnPrint
            // 
            this.pbBtnPrint.Location = new System.Drawing.Point(351, 660);
            this.pbBtnPrint.Name = "pbBtnPrint";
            this.pbBtnPrint.Size = new System.Drawing.Size(606, 167);
            this.pbBtnPrint.TabIndex = 8;
            this.pbBtnPrint.TabStop = false;
            // 
            // PrintValueSetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbBtnPrint);
            this.Controls.Add(this.pbText);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.pbBtnUp);
            this.Controls.Add(this.pbBtnDown);
            this.Name = "PrintValueSetView";
            this.Size = new System.Drawing.Size(1280, 1024);
            this.Load += new System.EventHandler(this.PrintValueSetView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBtnPrint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBtnDown;
        private System.Windows.Forms.PictureBox pbBtnUp;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.PictureBox pbText;
        private System.Windows.Forms.PictureBox pbBtnPrint;
    }
}
