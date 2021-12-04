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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSearchBG = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSelectHero = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
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
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions,
            this.menuSelectHero});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menu";
            // 
            // menuOptions
            // 
            this.menuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSearchBG});
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(61, 20);
            this.menuOptions.Text = "&Options";
            // 
            // menuSearchBG
            // 
            this.menuSearchBG.Checked = true;
            this.menuSearchBG.CheckOnClick = true;
            this.menuSearchBG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuSearchBG.Name = "menuSearchBG";
            this.menuSearchBG.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.menuSearchBG.Size = new System.Drawing.Size(240, 22);
            this.menuSearchBG.Text = "Search for &Battleground";
            // 
            // menuSelectHero
            // 
            this.menuSelectHero.Enabled = false;
            this.menuSelectHero.Name = "menuSelectHero";
            this.menuSelectHero.Size = new System.Drawing.Size(79, 20);
            this.menuSelectHero.Text = "&Select Hero";
            // 
            // ScreenshotViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.prog);
            this.Controls.Add(this.menu);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menu;
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
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prog;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuOptions;
        private System.Windows.Forms.ToolStripMenuItem menuSelectHero;
        protected System.Windows.Forms.ToolStripMenuItem menuSearchBG;
    }
}