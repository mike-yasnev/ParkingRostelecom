namespace ParkingServer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.ParkingView = new System.Windows.Forms.PictureBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.StopBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ParkingView)).BeginInit();
            this.SuspendLayout();
            // 
            // ParkingView
            // 
            this.ParkingView.Location = new System.Drawing.Point(28, 21);
            this.ParkingView.Name = "ParkingView";
            this.ParkingView.Size = new System.Drawing.Size(950, 439);
            this.ParkingView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ParkingView.TabIndex = 0;
            this.ParkingView.TabStop = false;
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(784, 478);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(94, 29);
            this.StartBtn.TabIndex = 1;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StopBtn
            // 
            this.StopBtn.Enabled = false;
            this.StopBtn.Location = new System.Drawing.Point(884, 478);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(94, 29);
            this.StopBtn.TabIndex = 2;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 575);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.ParkingView);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ParkingView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ParkingView;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button StopBtn;
    }
}