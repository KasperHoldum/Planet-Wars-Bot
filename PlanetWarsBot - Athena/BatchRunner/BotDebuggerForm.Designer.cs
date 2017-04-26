namespace BatchRunner
{
	partial class BotDebuggerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BotDebuggerForm));
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxLost = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.labelLosses = new System.Windows.Forms.Label();
            this.labelDraw = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControlGameEngine = new System.Windows.Forms.TabControl();
            this.tabPageMaps = new System.Windows.Forms.TabPage();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.tabPageRenderer = new System.Windows.Forms.TabPage();
            this.panelRender = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxBreakpoint = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarRenderDelay = new System.Windows.Forms.TrackBar();
            this.textBoxTurns = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxOpponent1 = new System.Windows.Forms.ComboBox();
            this.cbOpponentOneOwnBot = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxOpponent2 = new System.Windows.Forms.ComboBox();
            this.cbOpponentTwoOwnBot = new System.Windows.Forms.CheckBox();
            this.button11 = new System.Windows.Forms.Button();
            this.labelTurn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxPause = new System.Windows.Forms.CheckBox();
            this.cbRender = new System.Windows.Forms.CheckBox();
            this.labelRenderDelay = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button9 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBoxWin = new System.Windows.Forms.TextBox();
            this.labelWins = new System.Windows.Forms.Label();
            this.textBoxDraw = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControlGameEngine.SuspendLayout();
            this.tabPageMaps.SuspendLayout();
            this.tabPageRenderer.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRenderDelay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Launch";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // textBoxLost
            // 
            this.textBoxLost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLost.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLost.Location = new System.Drawing.Point(0, 16);
            this.textBoxLost.Multiline = true;
            this.textBoxLost.Name = "textBoxLost";
            this.textBoxLost.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLost.Size = new System.Drawing.Size(780, 208);
            this.textBoxLost.TabIndex = 3;
            this.textBoxLost.WordWrap = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 564);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(780, 29);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(84, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3Click);
            // 
            // labelLosses
            // 
            this.labelLosses.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLosses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelLosses.ForeColor = System.Drawing.Color.DarkRed;
            this.labelLosses.Location = new System.Drawing.Point(0, 0);
            this.labelLosses.Name = "labelLosses";
            this.labelLosses.Size = new System.Drawing.Size(780, 16);
            this.labelLosses.TabIndex = 5;
            this.labelLosses.Text = "Loss";
            this.labelLosses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDraw
            // 
            this.labelDraw.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelDraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelDraw.ForeColor = System.Drawing.Color.DarkMagenta;
            this.labelDraw.Location = new System.Drawing.Point(0, 0);
            this.labelDraw.Name = "labelDraw";
            this.labelDraw.Size = new System.Drawing.Size(780, 16);
            this.labelDraw.TabIndex = 6;
            this.labelDraw.Text = "Draw";
            this.labelDraw.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(794, 622);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tabControlGameEngine);
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(786, 596);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Debug bot";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControlGameEngine
            // 
            this.tabControlGameEngine.Controls.Add(this.tabPageMaps);
            this.tabControlGameEngine.Controls.Add(this.tabPageRenderer);
            this.tabControlGameEngine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGameEngine.Location = new System.Drawing.Point(3, 128);
            this.tabControlGameEngine.Name = "tabControlGameEngine";
            this.tabControlGameEngine.SelectedIndex = 0;
            this.tabControlGameEngine.Size = new System.Drawing.Size(780, 465);
            this.tabControlGameEngine.TabIndex = 6;
            // 
            // tabPageMaps
            // 
            this.tabPageMaps.Controls.Add(this.listBox2);
            this.tabPageMaps.Location = new System.Drawing.Point(4, 22);
            this.tabPageMaps.Name = "tabPageMaps";
            this.tabPageMaps.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMaps.Size = new System.Drawing.Size(772, 439);
            this.tabPageMaps.TabIndex = 0;
            this.tabPageMaps.Text = "Maps";
            this.tabPageMaps.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(3, 3);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(766, 433);
            this.listBox2.TabIndex = 4;
            // 
            // tabPageRenderer
            // 
            this.tabPageRenderer.Controls.Add(this.panelRender);
            this.tabPageRenderer.Location = new System.Drawing.Point(4, 22);
            this.tabPageRenderer.Name = "tabPageRenderer";
            this.tabPageRenderer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRenderer.Size = new System.Drawing.Size(772, 439);
            this.tabPageRenderer.TabIndex = 1;
            this.tabPageRenderer.Text = "Display";
            this.tabPageRenderer.UseVisualStyleBackColor = true;
            // 
            // panelRender
            // 
            this.panelRender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRender.Location = new System.Drawing.Point(3, 3);
            this.panelRender.Name = "panelRender";
            this.panelRender.Size = new System.Drawing.Size(766, 433);
            this.panelRender.TabIndex = 8;
            this.panelRender.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelRenderPaint);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel3.Controls.Add(this.checkBoxBreakpoint, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.trackBarRenderDelay, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.textBoxTurns, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.button11, 2, 3);
            this.tableLayoutPanel3.Controls.Add(this.labelTurn, 5, 2);
            this.tableLayoutPanel3.Controls.Add(this.label3, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.button10, 5, 3);
            this.tableLayoutPanel3.Controls.Add(this.buttonPlay, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.textBox1, 5, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxPause, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.cbRender, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelRenderDelay, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.button8, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(780, 125);
            this.tableLayoutPanel3.TabIndex = 12;
            // 
            // checkBoxBreakpoint
            // 
            this.checkBoxBreakpoint.AutoSize = true;
            this.checkBoxBreakpoint.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxBreakpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxBreakpoint.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.checkBoxBreakpoint.Location = new System.Drawing.Point(481, 32);
            this.checkBoxBreakpoint.Name = "checkBoxBreakpoint";
            this.checkBoxBreakpoint.Size = new System.Drawing.Size(128, 28);
            this.checkBoxBreakpoint.TabIndex = 13;
            this.checkBoxBreakpoint.Text = "Breakpoint";
            this.checkBoxBreakpoint.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label4.Location = new System.Drawing.Point(481, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 24);
            this.label4.TabIndex = 13;
            this.label4.Text = "Max Turns";
            // 
            // trackBarRenderDelay
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.trackBarRenderDelay, 2);
            this.trackBarRenderDelay.LargeChange = 200;
            this.trackBarRenderDelay.Location = new System.Drawing.Point(3, 90);
            this.trackBarRenderDelay.Maximum = 1500;
            this.trackBarRenderDelay.Minimum = 1;
            this.trackBarRenderDelay.Name = "trackBarRenderDelay";
            this.trackBarRenderDelay.Size = new System.Drawing.Size(294, 32);
            this.trackBarRenderDelay.TabIndex = 8;
            this.trackBarRenderDelay.TickFrequency = 35;
            this.trackBarRenderDelay.Value = 300;
            // 
            // textBoxTurns
            // 
            this.textBoxTurns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTurns.Location = new System.Drawing.Point(655, 4);
            this.textBoxTurns.Name = "textBoxTurns";
            this.textBoxTurns.Size = new System.Drawing.Size(122, 20);
            this.textBoxTurns.TabIndex = 5;
            this.textBoxTurns.Text = "200";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel3.SetRowSpan(this.groupBox1, 3);
            this.groupBox1.Size = new System.Drawing.Size(144, 69);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opponent One";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.GreenYellow;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxOpponent1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbOpponentOneOwnBot, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(138, 50);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Predefined";
            // 
            // comboBoxOpponent1
            // 
            this.comboBoxOpponent1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOpponent1.FormattingEnabled = true;
            this.comboBoxOpponent1.Items.AddRange(new object[] {
            "Bully",
            "Dual",
            "Prospector",
            "Rage",
            "Random"});
            this.comboBoxOpponent1.Location = new System.Drawing.Point(72, 3);
            this.comboBoxOpponent1.Name = "comboBoxOpponent1";
            this.comboBoxOpponent1.Size = new System.Drawing.Size(63, 21);
            this.comboBoxOpponent1.TabIndex = 1;
            // 
            // cbOpponentOneOwnBot
            // 
            this.cbOpponentOneOwnBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOpponentOneOwnBot.AutoSize = true;
            this.cbOpponentOneOwnBot.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel1.SetColumnSpan(this.cbOpponentOneOwnBot, 2);
            this.cbOpponentOneOwnBot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOpponentOneOwnBot.ForeColor = System.Drawing.Color.Crimson;
            this.cbOpponentOneOwnBot.Location = new System.Drawing.Point(3, 30);
            this.cbOpponentOneOwnBot.Name = "cbOpponentOneOwnBot";
            this.cbOpponentOneOwnBot.Size = new System.Drawing.Size(132, 17);
            this.cbOpponentOneOwnBot.TabIndex = 3;
            this.cbOpponentOneOwnBot.Text = "My own bot please";
            this.cbOpponentOneOwnBot.UseVisualStyleBackColor = true;
            this.cbOpponentOneOwnBot.CheckedChanged += new System.EventHandler(this.CbOpponentOneOwnBotCheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(153, 3);
            this.groupBox2.Name = "groupBox2";
            this.tableLayoutPanel3.SetRowSpan(this.groupBox2, 3);
            this.groupBox2.Size = new System.Drawing.Size(144, 69);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opponent Two";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Maroon;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxOpponent2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbOpponentTwoOwnBot, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(138, 50);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Predefined";
            // 
            // comboBoxOpponent2
            // 
            this.comboBoxOpponent2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOpponent2.FormattingEnabled = true;
            this.comboBoxOpponent2.Items.AddRange(new object[] {
            "Bully",
            "Dual",
            "Prospector",
            "Rage",
            "Random"});
            this.comboBoxOpponent2.Location = new System.Drawing.Point(72, 3);
            this.comboBoxOpponent2.Name = "comboBoxOpponent2";
            this.comboBoxOpponent2.Size = new System.Drawing.Size(63, 21);
            this.comboBoxOpponent2.TabIndex = 1;
            // 
            // cbOpponentTwoOwnBot
            // 
            this.cbOpponentTwoOwnBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOpponentTwoOwnBot.AutoSize = true;
            this.cbOpponentTwoOwnBot.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutPanel2.SetColumnSpan(this.cbOpponentTwoOwnBot, 2);
            this.cbOpponentTwoOwnBot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOpponentTwoOwnBot.ForeColor = System.Drawing.Color.Crimson;
            this.cbOpponentTwoOwnBot.Location = new System.Drawing.Point(3, 30);
            this.cbOpponentTwoOwnBot.Name = "cbOpponentTwoOwnBot";
            this.cbOpponentTwoOwnBot.Size = new System.Drawing.Size(132, 17);
            this.cbOpponentTwoOwnBot.TabIndex = 3;
            this.cbOpponentTwoOwnBot.Text = "My own bot please";
            this.cbOpponentTwoOwnBot.UseVisualStyleBackColor = true;
            this.cbOpponentTwoOwnBot.CheckedChanged += new System.EventHandler(this.CbOpponentTwoOwnBotCheckedChanged);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.Location = new System.Drawing.Point(303, 90);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 32);
            this.button11.TabIndex = 4;
            this.button11.Text = "Reload";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.Button11Click);
            // 
            // labelTurn
            // 
            this.labelTurn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTurn.AutoSize = true;
            this.labelTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.labelTurn.ForeColor = System.Drawing.Color.Purple;
            this.labelTurn.Location = new System.Drawing.Point(655, 63);
            this.labelTurn.Name = "labelTurn";
            this.labelTurn.Size = new System.Drawing.Size(122, 24);
            this.labelTurn.TabIndex = 11;
            this.labelTurn.Text = "Turn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.label3.Location = new System.Drawing.Point(481, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Current turn";
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button10.Location = new System.Drawing.Point(655, 90);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(122, 32);
            this.button10.TabIndex = 5;
            this.button10.Text = "Stop";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.Button10Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonPlay.Location = new System.Drawing.Point(481, 90);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(168, 32);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.ButtonPlayClick);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(655, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "200";
            // 
            // checkBoxPause
            // 
            this.checkBoxPause.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPause.AutoSize = true;
            this.checkBoxPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.checkBoxPause.Location = new System.Drawing.Point(384, 90);
            this.checkBoxPause.Name = "checkBoxPause";
            this.checkBoxPause.Size = new System.Drawing.Size(91, 32);
            this.checkBoxPause.TabIndex = 9;
            this.checkBoxPause.Text = "Paused";
            this.checkBoxPause.UseVisualStyleBackColor = false;
            // 
            // cbRender
            // 
            this.cbRender.AutoSize = true;
            this.cbRender.Checked = true;
            this.cbRender.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRender.Location = new System.Drawing.Point(303, 32);
            this.cbRender.Name = "cbRender";
            this.cbRender.Size = new System.Drawing.Size(61, 17);
            this.cbRender.TabIndex = 7;
            this.cbRender.Text = "Render";
            this.cbRender.UseVisualStyleBackColor = true;
            // 
            // labelRenderDelay
            // 
            this.labelRenderDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRenderDelay.AutoSize = true;
            this.labelRenderDelay.Location = new System.Drawing.Point(384, 39);
            this.labelRenderDelay.Name = "labelRenderDelay";
            this.labelRenderDelay.Size = new System.Drawing.Size(91, 13);
            this.labelRenderDelay.TabIndex = 10;
            this.labelRenderDelay.Text = "Render delay";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(384, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 14;
            this.button8.Text = "Start TCP";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.flowLayoutPanel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(786, 596);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Launch bot";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(780, 561);
            this.listBox1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.button9);
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Controls.Add(this.button4);
            this.flowLayoutPanel2.Controls.Add(this.button5);
            this.flowLayoutPanel2.Controls.Add(this.button6);
            this.flowLayoutPanel2.Controls.Add(this.button7);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(780, 29);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(3, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 1;
            this.button9.Text = "Reload";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.Button9Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Bully";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(165, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "Dual";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(246, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "Prospect";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(327, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 0;
            this.button6.Text = "Rage";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button6Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(408, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "Random";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Button7Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer2);
            this.tabPage2.Controls.Add(this.flowLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(786, 596);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "All Maps";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBoxDraw);
            this.splitContainer2.Panel2.Controls.Add(this.labelDraw);
            this.splitContainer2.Size = new System.Drawing.Size(780, 561);
            this.splitContainer2.SplitterDistance = 455;
            this.splitContainer2.TabIndex = 6;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBoxWin);
            this.splitContainer1.Panel1.Controls.Add(this.labelWins);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxLost);
            this.splitContainer1.Panel2.Controls.Add(this.labelLosses);
            this.splitContainer1.Size = new System.Drawing.Size(780, 455);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 5;
            // 
            // textBoxWin
            // 
            this.textBoxWin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxWin.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxWin.Location = new System.Drawing.Point(0, 16);
            this.textBoxWin.Multiline = true;
            this.textBoxWin.Name = "textBoxWin";
            this.textBoxWin.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxWin.Size = new System.Drawing.Size(780, 211);
            this.textBoxWin.TabIndex = 3;
            this.textBoxWin.WordWrap = false;
            // 
            // labelWins
            // 
            this.labelWins.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelWins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWins.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelWins.Location = new System.Drawing.Point(0, 0);
            this.labelWins.Name = "labelWins";
            this.labelWins.Size = new System.Drawing.Size(780, 16);
            this.labelWins.TabIndex = 5;
            this.labelWins.Text = "Difficult wins";
            this.labelWins.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxDraw
            // 
            this.textBoxDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDraw.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDraw.Location = new System.Drawing.Point(0, 16);
            this.textBoxDraw.Multiline = true;
            this.textBoxDraw.Name = "textBoxDraw";
            this.textBoxDraw.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDraw.Size = new System.Drawing.Size(780, 86);
            this.textBoxDraw.TabIndex = 3;
            this.textBoxDraw.WordWrap = false;
            // 
            // BotDebuggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 622);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BotDebuggerForm";
            this.Text = "Bot debugger by: Bas Mommehof";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControlGameEngine.ResumeLayout(false);
            this.tabPageMaps.ResumeLayout(false);
            this.tabPageRenderer.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRenderDelay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button2;
		public System.Windows.Forms.TextBox textBoxLost;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label labelLosses;
		private System.Windows.Forms.Label labelDraw;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		public System.Windows.Forms.TextBox textBoxWin;
		public System.Windows.Forms.TextBox textBoxDraw;
		private System.Windows.Forms.Label labelWins;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button buttonPlay;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBoxOpponent1;
		private System.Windows.Forms.CheckBox cbOpponentOneOwnBot;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxOpponent2;
		private System.Windows.Forms.CheckBox cbOpponentTwoOwnBot;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxTurns;
		private System.Windows.Forms.TabControl tabControlGameEngine;
		private System.Windows.Forms.TabPage tabPageRenderer;
		private System.Windows.Forms.Panel panelRender;
		private System.Windows.Forms.TabPage tabPageMaps;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.CheckBox cbRender;
		private System.Windows.Forms.Label labelRenderDelay;
		private System.Windows.Forms.CheckBox checkBoxPause;
		private System.Windows.Forms.TrackBar trackBarRenderDelay;
		private System.Windows.Forms.Label labelTurn;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.CheckBox checkBoxBreakpoint;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button8;
	}
}

