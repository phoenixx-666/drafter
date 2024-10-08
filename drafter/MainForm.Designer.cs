﻿namespace drafter {
	partial class MainForm {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
            System.Windows.Forms.Label l_bg;
            System.Windows.Forms.Label l_t1b1;
            System.Windows.Forms.Label l_t1b2;
            System.Windows.Forms.Label l_t1b3;
            System.Windows.Forms.Label l_t1h1;
            System.Windows.Forms.Label l_t1h2;
            System.Windows.Forms.Label l_t1h3;
            System.Windows.Forms.Label l_t1h4;
            System.Windows.Forms.Label l_t1h5;
            System.Windows.Forms.Label l_t2b1;
            System.Windows.Forms.Label l_t2b2;
            System.Windows.Forms.Label l_t2b3;
            System.Windows.Forms.Label l_t2h1;
            System.Windows.Forms.Label l_t2h2;
            System.Windows.Forms.Label l_t2h3;
            System.Windows.Forms.Label l_t2h4;
            System.Windows.Forms.Label l_t2h5;
            this.bClear = new System.Windows.Forms.Button();
            this.bCopy = new System.Windows.Forms.Button();
            this.bPaste = new System.Windows.Forms.Button();
            this.bSwap = new System.Windows.Forms.Button();
            this.c_bg = new System.Windows.Forms.ComboBox();
            this.c_t1b1 = new System.Windows.Forms.ComboBox();
            this.c_t1b2 = new System.Windows.Forms.ComboBox();
            this.c_t1b3 = new System.Windows.Forms.ComboBox();
            this.c_t1h1 = new System.Windows.Forms.ComboBox();
            this.c_t1h2 = new System.Windows.Forms.ComboBox();
            this.c_t1h3 = new System.Windows.Forms.ComboBox();
            this.c_t1h4 = new System.Windows.Forms.ComboBox();
            this.c_t1h5 = new System.Windows.Forms.ComboBox();
            this.c_t2b1 = new System.Windows.Forms.ComboBox();
            this.c_t2b2 = new System.Windows.Forms.ComboBox();
            this.c_t2b3 = new System.Windows.Forms.ComboBox();
            this.c_t2h1 = new System.Windows.Forms.ComboBox();
            this.c_t2h2 = new System.Windows.Forms.ComboBox();
            this.c_t2h3 = new System.Windows.Forms.ComboBox();
            this.c_t2h4 = new System.Windows.Forms.ComboBox();
            this.c_t2h5 = new System.Windows.Forms.ComboBox();
            this.ch_t1w = new System.Windows.Forms.CheckBox();
            this.ch_t2w = new System.Windows.Forms.CheckBox();
            this.gbTabOrder = new System.Windows.Forms.GroupBox();
            this.rbFPRight = new System.Windows.Forms.RadioButton();
            this.rbFPLeft = new System.Windows.Forms.RadioButton();
            this.rbSimple = new System.Windows.Forms.RadioButton();
            this.l_indication = new System.Windows.Forms.Label();
            this.tResult = new System.Windows.Forms.TextBox();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.bSwapColors = new System.Windows.Forms.Button();
            this.t_s1 = new System.Windows.Forms.TextBox();
            this.t_s2 = new System.Windows.Forms.TextBox();
            l_bg = new System.Windows.Forms.Label();
            l_t1b1 = new System.Windows.Forms.Label();
            l_t1b2 = new System.Windows.Forms.Label();
            l_t1b3 = new System.Windows.Forms.Label();
            l_t1h1 = new System.Windows.Forms.Label();
            l_t1h2 = new System.Windows.Forms.Label();
            l_t1h3 = new System.Windows.Forms.Label();
            l_t1h4 = new System.Windows.Forms.Label();
            l_t1h5 = new System.Windows.Forms.Label();
            l_t2b1 = new System.Windows.Forms.Label();
            l_t2b2 = new System.Windows.Forms.Label();
            l_t2b3 = new System.Windows.Forms.Label();
            l_t2h1 = new System.Windows.Forms.Label();
            l_t2h2 = new System.Windows.Forms.Label();
            l_t2h3 = new System.Windows.Forms.Label();
            l_t2h4 = new System.Windows.Forms.Label();
            l_t2h5 = new System.Windows.Forms.Label();
            this.gbTabOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(456, 344);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(104, 32);
            this.bClear.TabIndex = 201;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.BClearClick);
            // 
            // bCopy
            // 
            this.bCopy.Location = new System.Drawing.Point(8, 344);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(104, 32);
            this.bCopy.TabIndex = 200;
            this.bCopy.Text = "Copy";
            this.bCopy.UseVisualStyleBackColor = true;
            this.bCopy.Click += new System.EventHandler(this.BCopyClick);
            // 
            // bPaste
            // 
            this.bPaste.Location = new System.Drawing.Point(224, 136);
            this.bPaste.Name = "bPaste";
            this.bPaste.Size = new System.Drawing.Size(104, 32);
            this.bPaste.TabIndex = 206;
            this.bPaste.Text = "Paste Clipboard";
            this.bPaste.UseVisualStyleBackColor = true;
            this.bPaste.Click += new System.EventHandler(this.BPaste_Click);
            // 
            // bSwap
            // 
            this.bSwap.Location = new System.Drawing.Point(224, 96);
            this.bSwap.Name = "bSwap";
            this.bSwap.Size = new System.Drawing.Size(104, 32);
            this.bSwap.TabIndex = 202;
            this.bSwap.Text = "< Swap Teams >";
            this.bSwap.UseVisualStyleBackColor = true;
            this.bSwap.Click += new System.EventHandler(this.BSwapClick);
            // 
            // c_bg
            // 
            this.c_bg.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_bg.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_bg.FormattingEnabled = true;
            this.c_bg.Location = new System.Drawing.Point(200, 192);
            this.c_bg.Name = "c_bg";
            this.c_bg.Size = new System.Drawing.Size(160, 21);
            this.c_bg.Sorted = true;
            this.c_bg.TabIndex = 120;
            // 
            // c_t1b1
            // 
            this.c_t1b1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t1b1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t1b1.FormattingEnabled = true;
            this.c_t1b1.Location = new System.Drawing.Point(8, 32);
            this.c_t1b1.Name = "c_t1b1";
            this.c_t1b1.Size = new System.Drawing.Size(80, 21);
            this.c_t1b1.Sorted = true;
            this.c_t1b1.TabIndex = 100;
            // 
            // c_t1b2
            // 
            this.c_t1b2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t1b2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t1b2.FormattingEnabled = true;
            this.c_t1b2.Location = new System.Drawing.Point(96, 32);
            this.c_t1b2.Name = "c_t1b2";
            this.c_t1b2.Size = new System.Drawing.Size(80, 21);
            this.c_t1b2.Sorted = true;
            this.c_t1b2.TabIndex = 101;
            // 
            // c_t1b3
            // 
            this.c_t1b3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t1b3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t1b3.FormattingEnabled = true;
            this.c_t1b3.Location = new System.Drawing.Point(184, 32);
            this.c_t1b3.Name = "c_t1b3";
            this.c_t1b3.Size = new System.Drawing.Size(80, 21);
            this.c_t1b3.Sorted = true;
            this.c_t1b3.TabIndex = 102;
            // 
            // c_t1h1
            // 
            this.c_t1h1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t1h1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t1h1.FormattingEnabled = true;
            this.c_t1h1.Location = new System.Drawing.Point(48, 64);
            this.c_t1h1.Name = "c_t1h1";
            this.c_t1h1.Size = new System.Drawing.Size(120, 21);
            this.c_t1h1.Sorted = true;
            this.c_t1h1.TabIndex = 106;
            // 
            // c_t1h2
            // 
            this.c_t1h2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t1h2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t1h2.FormattingEnabled = true;
            this.c_t1h2.Location = new System.Drawing.Point(72, 96);
            this.c_t1h2.Name = "c_t1h2";
            this.c_t1h2.Size = new System.Drawing.Size(120, 21);
            this.c_t1h2.Sorted = true;
            this.c_t1h2.TabIndex = 107;
            // 
            // c_t1h3
            // 
            this.c_t1h3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t1h3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t1h3.FormattingEnabled = true;
            this.c_t1h3.Location = new System.Drawing.Point(48, 128);
            this.c_t1h3.Name = "c_t1h3";
            this.c_t1h3.Size = new System.Drawing.Size(120, 21);
            this.c_t1h3.Sorted = true;
            this.c_t1h3.TabIndex = 108;
            // 
            // c_t1h4
            // 
            this.c_t1h4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t1h4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t1h4.FormattingEnabled = true;
            this.c_t1h4.Location = new System.Drawing.Point(72, 160);
            this.c_t1h4.Name = "c_t1h4";
            this.c_t1h4.Size = new System.Drawing.Size(120, 21);
            this.c_t1h4.Sorted = true;
            this.c_t1h4.TabIndex = 109;
            // 
            // c_t1h5
            // 
            this.c_t1h5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t1h5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t1h5.FormattingEnabled = true;
            this.c_t1h5.Location = new System.Drawing.Point(48, 192);
            this.c_t1h5.Name = "c_t1h5";
            this.c_t1h5.Size = new System.Drawing.Size(120, 21);
            this.c_t1h5.Sorted = true;
            this.c_t1h5.TabIndex = 110;
            // 
            // c_t2b1
            // 
            this.c_t2b1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t2b1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t2b1.FormattingEnabled = true;
            this.c_t2b1.Location = new System.Drawing.Point(304, 32);
            this.c_t2b1.Name = "c_t2b1";
            this.c_t2b1.Size = new System.Drawing.Size(80, 21);
            this.c_t2b1.Sorted = true;
            this.c_t2b1.TabIndex = 103;
            // 
            // c_t2b2
            // 
            this.c_t2b2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t2b2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t2b2.FormattingEnabled = true;
            this.c_t2b2.Location = new System.Drawing.Point(392, 32);
            this.c_t2b2.Name = "c_t2b2";
            this.c_t2b2.Size = new System.Drawing.Size(80, 21);
            this.c_t2b2.Sorted = true;
            this.c_t2b2.TabIndex = 104;
            // 
            // c_t2b3
            // 
            this.c_t2b3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t2b3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t2b3.FormattingEnabled = true;
            this.c_t2b3.Location = new System.Drawing.Point(480, 32);
            this.c_t2b3.Name = "c_t2b3";
            this.c_t2b3.Size = new System.Drawing.Size(80, 21);
            this.c_t2b3.Sorted = true;
            this.c_t2b3.TabIndex = 105;
            // 
            // c_t2h1
            // 
            this.c_t2h1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t2h1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t2h1.FormattingEnabled = true;
            this.c_t2h1.Location = new System.Drawing.Point(392, 64);
            this.c_t2h1.Name = "c_t2h1";
            this.c_t2h1.Size = new System.Drawing.Size(120, 21);
            this.c_t2h1.Sorted = true;
            this.c_t2h1.TabIndex = 111;
            // 
            // c_t2h2
            // 
            this.c_t2h2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t2h2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t2h2.FormattingEnabled = true;
            this.c_t2h2.Location = new System.Drawing.Point(368, 96);
            this.c_t2h2.Name = "c_t2h2";
            this.c_t2h2.Size = new System.Drawing.Size(120, 21);
            this.c_t2h2.Sorted = true;
            this.c_t2h2.TabIndex = 112;
            // 
            // c_t2h3
            // 
            this.c_t2h3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t2h3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t2h3.FormattingEnabled = true;
            this.c_t2h3.Location = new System.Drawing.Point(392, 128);
            this.c_t2h3.Name = "c_t2h3";
            this.c_t2h3.Size = new System.Drawing.Size(120, 21);
            this.c_t2h3.Sorted = true;
            this.c_t2h3.TabIndex = 113;
            // 
            // c_t2h4
            // 
            this.c_t2h4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t2h4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t2h4.FormattingEnabled = true;
            this.c_t2h4.Location = new System.Drawing.Point(368, 160);
            this.c_t2h4.Name = "c_t2h4";
            this.c_t2h4.Size = new System.Drawing.Size(120, 21);
            this.c_t2h4.Sorted = true;
            this.c_t2h4.TabIndex = 114;
            // 
            // c_t2h5
            // 
            this.c_t2h5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.c_t2h5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.c_t2h5.FormattingEnabled = true;
            this.c_t2h5.Location = new System.Drawing.Point(392, 192);
            this.c_t2h5.Name = "c_t2h5";
            this.c_t2h5.Size = new System.Drawing.Size(120, 21);
            this.c_t2h5.Sorted = true;
            this.c_t2h5.TabIndex = 115;
            // 
            // ch_t1w
            // 
            this.ch_t1w.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ch_t1w.Location = new System.Drawing.Point(168, 192);
            this.ch_t1w.Name = "ch_t1w";
            this.ch_t1w.Size = new System.Drawing.Size(24, 24);
            this.ch_t1w.TabIndex = 121;
            this.ch_t1w.UseVisualStyleBackColor = true;
            // 
            // ch_t2w
            // 
            this.ch_t2w.Location = new System.Drawing.Point(368, 192);
            this.ch_t2w.Name = "ch_t2w";
            this.ch_t2w.Size = new System.Drawing.Size(16, 24);
            this.ch_t2w.TabIndex = 122;
            this.ch_t2w.UseVisualStyleBackColor = true;
            // 
            // gbTabOrder
            // 
            this.gbTabOrder.Controls.Add(this.rbFPRight);
            this.gbTabOrder.Controls.Add(this.rbFPLeft);
            this.gbTabOrder.Controls.Add(this.rbSimple);
            this.gbTabOrder.Location = new System.Drawing.Point(120, 340);
            this.gbTabOrder.Name = "gbTabOrder";
            this.gbTabOrder.Size = new System.Drawing.Size(328, 40);
            this.gbTabOrder.TabIndex = 21;
            this.gbTabOrder.TabStop = false;
            this.gbTabOrder.Text = "Tab Order";
            // 
            // rbFPRight
            // 
            this.rbFPRight.Location = new System.Drawing.Point(216, 14);
            this.rbFPRight.Name = "rbFPRight";
            this.rbFPRight.Size = new System.Drawing.Size(104, 20);
            this.rbFPRight.TabIndex = 302;
            this.rbFPRight.Text = "First Pick Right";
            this.rbFPRight.UseVisualStyleBackColor = true;
            this.rbFPRight.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // rbFPLeft
            // 
            this.rbFPLeft.Location = new System.Drawing.Point(112, 14);
            this.rbFPLeft.Name = "rbFPLeft";
            this.rbFPLeft.Size = new System.Drawing.Size(104, 20);
            this.rbFPLeft.TabIndex = 301;
            this.rbFPLeft.Text = "First Pick Left";
            this.rbFPLeft.UseVisualStyleBackColor = true;
            this.rbFPLeft.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // rbSimple
            // 
            this.rbSimple.Checked = true;
            this.rbSimple.Location = new System.Drawing.Point(8, 14);
            this.rbSimple.Name = "rbSimple";
            this.rbSimple.Size = new System.Drawing.Size(104, 20);
            this.rbSimple.TabIndex = 300;
            this.rbSimple.TabStop = true;
            this.rbSimple.Text = "Bans then Picks";
            this.rbSimple.UseVisualStyleBackColor = true;
            this.rbSimple.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // l_bg
            // 
            l_bg.Location = new System.Drawing.Point(200, 176);
            l_bg.Name = "l_bg";
            l_bg.Size = new System.Drawing.Size(160, 16);
            l_bg.TabIndex = 204;
            l_bg.Text = "Battleground";
            // 
            // l_t1b1
            // 
            l_t1b1.Location = new System.Drawing.Point(8, 16);
            l_t1b1.Name = "l_t1b1";
            l_t1b1.Size = new System.Drawing.Size(80, 16);
            l_t1b1.TabIndex = 5;
            l_t1b1.Text = "Ban 1";
            // 
            // l_t1b2
            // 
            l_t1b2.Location = new System.Drawing.Point(96, 16);
            l_t1b2.Name = "l_t1b2";
            l_t1b2.Size = new System.Drawing.Size(80, 16);
            l_t1b2.TabIndex = 6;
            l_t1b2.Text = "Ban 2";
            // 
            // l_t1b3
            // 
            l_t1b3.Location = new System.Drawing.Point(184, 16);
            l_t1b3.Name = "l_t1b3";
            l_t1b3.Size = new System.Drawing.Size(80, 16);
            l_t1b3.TabIndex = 7;
            l_t1b3.Text = "Ban 3";
            // 
            // l_t1h1
            // 
            l_t1h1.Location = new System.Drawing.Point(8, 64);
            l_t1h1.Name = "l_t1h1";
            l_t1h1.Size = new System.Drawing.Size(40, 21);
            l_t1h1.TabIndex = 12;
            l_t1h1.Text = "Pick 1";
            l_t1h1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t1h2
            // 
            l_t1h2.Location = new System.Drawing.Point(32, 96);
            l_t1h2.Name = "l_t1h2";
            l_t1h2.Size = new System.Drawing.Size(40, 21);
            l_t1h2.TabIndex = 14;
            l_t1h2.Text = "Pick 2";
            l_t1h2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t1h3
            // 
            l_t1h3.Location = new System.Drawing.Point(8, 128);
            l_t1h3.Name = "l_t1h3";
            l_t1h3.Size = new System.Drawing.Size(40, 21);
            l_t1h3.TabIndex = 16;
            l_t1h3.Text = "Pick 3";
            l_t1h3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t1h4
            // 
            l_t1h4.Location = new System.Drawing.Point(32, 160);
            l_t1h4.Name = "l_t1h4";
            l_t1h4.Size = new System.Drawing.Size(40, 21);
            l_t1h4.TabIndex = 18;
            l_t1h4.Text = "Pick 4";
            l_t1h4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t1h5
            // 
            l_t1h5.Location = new System.Drawing.Point(8, 192);
            l_t1h5.Name = "l_t1h5";
            l_t1h5.Size = new System.Drawing.Size(40, 21);
            l_t1h5.TabIndex = 20;
            l_t1h5.Text = "Pick 5";
            l_t1h5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t2b1
            // 
            l_t2b1.Location = new System.Drawing.Point(304, 16);
            l_t2b1.Name = "l_t2b1";
            l_t2b1.Size = new System.Drawing.Size(80, 16);
            l_t2b1.TabIndex = 8;
            l_t2b1.Text = "Ban 1";
            // 
            // l_t2b2
            // 
            l_t2b2.Location = new System.Drawing.Point(392, 16);
            l_t2b2.Name = "l_t2b2";
            l_t2b2.Size = new System.Drawing.Size(80, 16);
            l_t2b2.TabIndex = 9;
            l_t2b2.Text = "Ban 2";
            // 
            // l_t2b3
            // 
            l_t2b3.Location = new System.Drawing.Point(480, 16);
            l_t2b3.Name = "l_t2b3";
            l_t2b3.Size = new System.Drawing.Size(80, 16);
            l_t2b3.TabIndex = 10;
            l_t2b3.Text = "Ban 3";
            // 
            // l_t2h1
            // 
            l_t2h1.Location = new System.Drawing.Point(520, 64);
            l_t2h1.Name = "l_t2h1";
            l_t2h1.Size = new System.Drawing.Size(40, 21);
            l_t2h1.TabIndex = 12;
            l_t2h1.Text = "Pick 1";
            l_t2h1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t2h2
            // 
            l_t2h2.Location = new System.Drawing.Point(496, 96);
            l_t2h2.Name = "l_t2h2";
            l_t2h2.Size = new System.Drawing.Size(40, 21);
            l_t2h2.TabIndex = 14;
            l_t2h2.Text = "Pick 2";
            l_t2h2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t2h3
            // 
            l_t2h3.Location = new System.Drawing.Point(520, 128);
            l_t2h3.Name = "l_t2h3";
            l_t2h3.Size = new System.Drawing.Size(40, 21);
            l_t2h3.TabIndex = 16;
            l_t2h3.Text = "Pick 3";
            l_t2h3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t2h4
            // 
            l_t2h4.Location = new System.Drawing.Point(496, 160);
            l_t2h4.Name = "l_t2h4";
            l_t2h4.Size = new System.Drawing.Size(40, 21);
            l_t2h4.TabIndex = 18;
            l_t2h4.Text = "Pick 4";
            l_t2h4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_t2h5
            // 
            l_t2h5.Location = new System.Drawing.Point(520, 192);
            l_t2h5.Name = "l_t2h5";
            l_t2h5.Size = new System.Drawing.Size(40, 21);
            l_t2h5.TabIndex = 20;
            l_t2h5.Text = "Pick 5";
            l_t2h5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_indication
            // 
            this.l_indication.Location = new System.Drawing.Point(181, 64);
            this.l_indication.Name = "l_indication";
            this.l_indication.Size = new System.Drawing.Size(203, 21);
            this.l_indication.TabIndex = 205;
            this.l_indication.Text = "Indication";
            this.l_indication.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.l_indication.Visible = false;
            // 
            // tResult
            // 
            this.tResult.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tResult.Location = new System.Drawing.Point(8, 256);
            this.tResult.Multiline = true;
            this.tResult.Name = "tResult";
            this.tResult.ReadOnly = true;
            this.tResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tResult.Size = new System.Drawing.Size(552, 84);
            this.tResult.TabIndex = 19;
            this.tResult.GotFocus += new System.EventHandler(this.TResultGotFocus);
            // 
            // worker
            // 
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_Completed);
            // 
            // bSwapColors
            // 
            this.bSwapColors.Location = new System.Drawing.Point(224, 219);
            this.bSwapColors.Name = "bSwapColors";
            this.bSwapColors.Size = new System.Drawing.Size(104, 32);
            this.bSwapColors.TabIndex = 207;
            this.bSwapColors.Text = "< Swap Sides >";
            this.bSwapColors.UseVisualStyleBackColor = true;
            this.bSwapColors.Click += new System.EventHandler(this.BSwapColorsCluck);
            // 
            // t_s1
            // 
            this.t_s1.BackColor = System.Drawing.SystemColors.Window;
            this.t_s1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.t_s1.ForeColor = System.Drawing.Color.Blue;
            this.t_s1.Location = new System.Drawing.Point(151, 224);
            this.t_s1.Name = "t_s1";
            this.t_s1.ReadOnly = true;
            this.t_s1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.t_s1.Size = new System.Drawing.Size(67, 20);
            this.t_s1.TabIndex = 208;
            this.t_s1.Text = "blue";
            // 
            // t_s2
            // 
            this.t_s2.BackColor = System.Drawing.SystemColors.Window;
            this.t_s2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.t_s2.ForeColor = System.Drawing.Color.Red;
            this.t_s2.Location = new System.Drawing.Point(334, 224);
            this.t_s2.Name = "t_s2";
            this.t_s2.ReadOnly = true;
            this.t_s2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.t_s2.Size = new System.Drawing.Size(67, 20);
            this.t_s2.TabIndex = 209;
            this.t_s2.Text = "red";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 387);
            this.Controls.Add(this.t_s2);
            this.Controls.Add(this.t_s1);
            this.Controls.Add(this.bSwapColors);
            this.Controls.Add(this.ch_t2w);
            this.Controls.Add(this.ch_t1w);
            this.Controls.Add(l_bg);
            this.Controls.Add(this.c_bg);
            this.Controls.Add(this.gbTabOrder);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bCopy);
            this.Controls.Add(this.bPaste);
            this.Controls.Add(this.bSwap);
            this.Controls.Add(this.tResult);
            this.Controls.Add(l_t2h5);
            this.Controls.Add(l_t1h5);
            this.Controls.Add(this.c_t2h5);
            this.Controls.Add(this.c_t1h5);
            this.Controls.Add(l_t2h4);
            this.Controls.Add(l_t1h4);
            this.Controls.Add(this.c_t2h4);
            this.Controls.Add(this.c_t1h4);
            this.Controls.Add(l_t2h3);
            this.Controls.Add(l_t1h3);
            this.Controls.Add(this.c_t2h3);
            this.Controls.Add(this.c_t1h3);
            this.Controls.Add(l_t2h2);
            this.Controls.Add(l_t1h2);
            this.Controls.Add(this.c_t2h2);
            this.Controls.Add(this.c_t1h2);
            this.Controls.Add(l_t2h1);
            this.Controls.Add(l_t1h1);
            this.Controls.Add(this.c_t2h1);
            this.Controls.Add(this.c_t1h1);
            this.Controls.Add(l_t2b3);
            this.Controls.Add(l_t2b2);
            this.Controls.Add(l_t2b1);
            this.Controls.Add(l_t1b3);
            this.Controls.Add(l_t1b2);
            this.Controls.Add(l_t1b1);
            this.Controls.Add(this.c_t2b3);
            this.Controls.Add(this.c_t2b2);
            this.Controls.Add(this.c_t2b1);
            this.Controls.Add(this.c_t1b3);
            this.Controls.Add(this.c_t1b2);
            this.Controls.Add(this.c_t1b1);
            this.Controls.Add(this.l_indication);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "drafter";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.gbTabOrder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.CheckBox ch_t2w;
		private System.Windows.Forms.CheckBox ch_t1w;
		private System.Windows.Forms.ComboBox c_bg;
		private System.Windows.Forms.RadioButton rbFPLeft;
		private System.Windows.Forms.RadioButton rbFPRight;
		private System.Windows.Forms.RadioButton rbSimple;
		private System.Windows.Forms.GroupBox gbTabOrder;
		private System.Windows.Forms.Button bClear;
		private System.Windows.Forms.Button bCopy;
        private System.Windows.Forms.Button bPaste;
        private System.Windows.Forms.Button bSwap;
        private System.Windows.Forms.TextBox tResult;
		private System.Windows.Forms.ComboBox c_t2h5;
		private System.Windows.Forms.ComboBox c_t2h4;
		private System.Windows.Forms.ComboBox c_t2h3;
		private System.Windows.Forms.ComboBox c_t2h2;
		private System.Windows.Forms.ComboBox c_t2h1;
		private System.Windows.Forms.ComboBox c_t1h5;
		private System.Windows.Forms.ComboBox c_t1h4;
		private System.Windows.Forms.ComboBox c_t1h3;
		private System.Windows.Forms.ComboBox c_t1h2;
		private System.Windows.Forms.ComboBox c_t1h1;
		private System.Windows.Forms.ComboBox c_t2b1;
		private System.Windows.Forms.ComboBox c_t2b2;
		private System.Windows.Forms.ComboBox c_t2b3;
		private System.Windows.Forms.ComboBox c_t1b3;
		private System.Windows.Forms.ComboBox c_t1b2;
		private System.Windows.Forms.ComboBox c_t1b1;
        private System.Windows.Forms.Label l_indication;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.Button bSwapColors;
        private System.Windows.Forms.TextBox t_s1;
        private System.Windows.Forms.TextBox t_s2;
    }
}
