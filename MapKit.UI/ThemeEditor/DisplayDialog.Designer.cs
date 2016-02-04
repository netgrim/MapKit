namespace MapKit.UI
{
	partial class DisplayDialog
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.StylePage = new System.Windows.Forms.TabPage();
			this.cmdUp = new System.Windows.Forms.Button();
			this.cmdDown = new System.Windows.Forms.Button();
			this.cmdRemove = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.split = new System.Windows.Forms.SplitContainer();
			this.listView = new System.Windows.Forms.ListView();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colCondition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colMinScale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colMaxScale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colPriority = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.modeCombo = new System.Windows.Forms.ComboBox();
			this.LabelPage = new System.Windows.Forms.TabPage();
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdApply = new System.Windows.Forms.Button();
			this.cmdHelp = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.StylePage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
			this.split.Panel1.SuspendLayout();
			this.split.Panel2.SuspendLayout();
			this.split.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.StylePage);
			this.tabControl1.Controls.Add(this.LabelPage);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(601, 438);
			this.tabControl1.TabIndex = 0;
			// 
			// StylePage
			// 
			this.StylePage.Controls.Add(this.cmdUp);
			this.StylePage.Controls.Add(this.cmdDown);
			this.StylePage.Controls.Add(this.cmdRemove);
			this.StylePage.Controls.Add(this.cmdAdd);
			this.StylePage.Controls.Add(this.split);
			this.StylePage.Controls.Add(this.modeCombo);
			this.StylePage.Location = new System.Drawing.Point(4, 22);
			this.StylePage.Name = "StylePage";
			this.StylePage.Padding = new System.Windows.Forms.Padding(3);
			this.StylePage.Size = new System.Drawing.Size(593, 412);
			this.StylePage.TabIndex = 0;
			this.StylePage.Text = "Style";
			this.StylePage.UseVisualStyleBackColor = true;
			// 
			// cmdUp
			// 
			this.cmdUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdUp.Location = new System.Drawing.Point(469, 4);
			this.cmdUp.Name = "cmdUp";
			this.cmdUp.Size = new System.Drawing.Size(56, 23);
			this.cmdUp.TabIndex = 11;
			this.cmdUp.Text = "Up";
			this.cmdUp.UseVisualStyleBackColor = true;
			this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
			// 
			// cmdDown
			// 
			this.cmdDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdDown.Location = new System.Drawing.Point(531, 4);
			this.cmdDown.Name = "cmdDown";
			this.cmdDown.Size = new System.Drawing.Size(56, 23);
			this.cmdDown.TabIndex = 10;
			this.cmdDown.Text = "Down";
			this.cmdDown.UseVisualStyleBackColor = true;
			this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
			// 
			// cmdRemove
			// 
			this.cmdRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdRemove.Location = new System.Drawing.Point(407, 4);
			this.cmdRemove.Name = "cmdRemove";
			this.cmdRemove.Size = new System.Drawing.Size(56, 23);
			this.cmdRemove.TabIndex = 9;
			this.cmdRemove.Text = "Remove";
			this.cmdRemove.UseVisualStyleBackColor = true;
			this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
			// 
			// cmdAdd
			// 
			this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdAdd.Location = new System.Drawing.Point(345, 4);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(56, 23);
			this.cmdAdd.TabIndex = 2;
			this.cmdAdd.Text = "Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// split
			// 
			this.split.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.split.Location = new System.Drawing.Point(6, 33);
			this.split.Name = "split";
			this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// split.Panel1
			// 
			this.split.Panel1.Controls.Add(this.listView);
			// 
			// split.Panel2
			// 
			this.split.Panel2.Controls.Add(this.propertyGrid);
			this.split.Size = new System.Drawing.Size(581, 373);
			this.split.SplitterDistance = 137;
			this.split.TabIndex = 8;
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colCondition,
            this.colMinScale,
            this.colMaxScale,
            this.colPriority});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(581, 137);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 113;
			// 
			// colCondition
			// 
			this.colCondition.Text = "Condition";
			this.colCondition.Width = 258;
			// 
			// colMinScale
			// 
			this.colMinScale.Text = "Min Scale";
			this.colMinScale.Width = 64;
			// 
			// colMaxScale
			// 
			this.colMaxScale.Text = "Max Scale";
			this.colMaxScale.Width = 70;
			// 
			// colPriority
			// 
			this.colPriority.Text = "Priority";
			// 
			// propertyGrid
			// 
			this.propertyGrid.CommandsVisibleIfAvailable = false;
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.HelpVisible = false;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(581, 232);
			this.propertyGrid.TabIndex = 0;
			this.propertyGrid.ToolbarVisible = false;
			// 
			// modeCombo
			// 
			this.modeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.modeCombo.FormattingEnabled = true;
			this.modeCombo.Items.AddRange(new object[] {
            "Unique Style",
            "Categorized",
            "Graduated",
            "Rules Set"});
			this.modeCombo.Location = new System.Drawing.Point(6, 6);
			this.modeCombo.Name = "modeCombo";
			this.modeCombo.Size = new System.Drawing.Size(121, 21);
			this.modeCombo.TabIndex = 7;
			this.modeCombo.SelectedIndexChanged += new System.EventHandler(this.modeCombo_SelectedIndexChanged);
			// 
			// LabelPage
			// 
			this.LabelPage.Location = new System.Drawing.Point(4, 22);
			this.LabelPage.Name = "LabelPage";
			this.LabelPage.Padding = new System.Windows.Forms.Padding(3);
			this.LabelPage.Size = new System.Drawing.Size(593, 412);
			this.LabelPage.TabIndex = 1;
			this.LabelPage.Text = "Labels";
			this.LabelPage.UseVisualStyleBackColor = true;
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOk.Location = new System.Drawing.Point(295, 456);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 1;
			this.cmdOk.Text = "OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.Location = new System.Drawing.Point(376, 456);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdApply
			// 
			this.cmdApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdApply.Location = new System.Drawing.Point(457, 456);
			this.cmdApply.Name = "cmdApply";
			this.cmdApply.Size = new System.Drawing.Size(75, 23);
			this.cmdApply.TabIndex = 3;
			this.cmdApply.Text = "Apply";
			this.cmdApply.UseVisualStyleBackColor = true;
			// 
			// cmdHelp
			// 
			this.cmdHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdHelp.Location = new System.Drawing.Point(538, 456);
			this.cmdHelp.Name = "cmdHelp";
			this.cmdHelp.Size = new System.Drawing.Size(75, 23);
			this.cmdHelp.TabIndex = 4;
			this.cmdHelp.Text = "Help";
			this.cmdHelp.UseVisualStyleBackColor = true;
			// 
			// LayerDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(625, 491);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdApply);
			this.Controls.Add(this.cmdHelp);
			this.Controls.Add(this.tabControl1);
			this.Name = "LayerDialog";
			this.Text = "Display Dialog";
			this.tabControl1.ResumeLayout(false);
			this.StylePage.ResumeLayout(false);
			this.split.Panel1.ResumeLayout(false);
			this.split.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
			this.split.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage StylePage;
		private System.Windows.Forms.TabPage LabelPage;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdApply;
		private System.Windows.Forms.Button cmdHelp;
		private System.Windows.Forms.SplitContainer split;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colCondition;
		private System.Windows.Forms.ColumnHeader colMinScale;
		private System.Windows.Forms.ColumnHeader colMaxScale;
		private System.Windows.Forms.ColumnHeader colPriority;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.ComboBox modeCombo;
		private System.Windows.Forms.Button cmdUp;
		private System.Windows.Forms.Button cmdDown;
		private System.Windows.Forms.Button cmdRemove;
		private System.Windows.Forms.Button cmdAdd;
	}
}