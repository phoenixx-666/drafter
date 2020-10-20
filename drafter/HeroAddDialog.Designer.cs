namespace drafter {
    partial class HeroAddDialog {
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
            this.cbAddHero = new System.Windows.Forms.ComboBox();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbAddHero
            // 
            this.cbAddHero.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbAddHero.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbAddHero.FormattingEnabled = true;
            this.cbAddHero.Location = new System.Drawing.Point(12, 12);
            this.cbAddHero.Name = "cbAddHero";
            this.cbAddHero.Size = new System.Drawing.Size(180, 21);
            this.cbAddHero.TabIndex = 0;
            this.cbAddHero.TextChanged += new System.EventHandler(this.CbAddHero_TextChanged);
            this.cbAddHero.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CbAddHero_KeyPress);
            // 
            // bOK
            // 
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Enabled = false;
            this.bOK.Location = new System.Drawing.Point(26, 39);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(80, 24);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "&OK";
            this.bOK.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bCancel.Location = new System.Drawing.Point(112, 39);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(80, 24);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // HeroAddDialog
            // 
            this.AcceptButton = this.bOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(204, 74);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cbAddHero);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HeroAddDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose a hero";
            this.Load += new System.EventHandler(this.HeroAddDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbAddHero;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
    }
}