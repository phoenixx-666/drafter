namespace drafter {
    partial class ScreenshotViewer {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.prog = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // prog
            // 
            this.prog.Location = new System.Drawing.Point(12, 382);
            this.prog.MarqueeAnimationSpeed = 10;
            this.prog.Maximum = 10000;
            this.prog.Name = "prog";
            this.prog.Size = new System.Drawing.Size(776, 56);
            this.prog.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prog.TabIndex = 0;
            this.prog.Visible = false;
            // 
            // ScreenshotViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.prog);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.Name = "ScreenshotViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Screenshot Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenshotViewer_Closing);
            this.Load += new System.EventHandler(this.ScreenshotViewer_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ScreenshotViewer_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScreenshotViewer_MouseClick);
            this.Resize += new System.EventHandler(this.ScreenshotViewer_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prog;
    }
}