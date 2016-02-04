namespace MapKit.Spatialite
{
	partial class QueryWizard
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
			this.cboTable = new System.Windows.Forms.ComboBox();
			this.lblTable = new System.Windows.Forms.Label();
			this.lblColumn = new System.Windows.Forms.Label();
			this.cboColumn = new System.Windows.Forms.ComboBox();
			this.txtSql = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdCreateIndex = new System.Windows.Forms.Button();
			this.lblIndexStatus = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmdDropIndex = new System.Windows.Forms.Button();
			this.chkEnabled = new System.Windows.Forms.CheckBox();
			this.txtMinScale = new System.Windows.Forms.TextBox();
			this.txtMaxScale = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cmdScanScale = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboTable
			// 
			this.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTable.FormattingEnabled = true;
			this.cboTable.Location = new System.Drawing.Point(78, 21);
			this.cboTable.Name = "cboTable";
			this.cboTable.Size = new System.Drawing.Size(121, 21);
			this.cboTable.TabIndex = 1;
			this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
			// 
			// lblTable
			// 
			this.lblTable.AutoSize = true;
			this.lblTable.Location = new System.Drawing.Point(12, 24);
			this.lblTable.Name = "lblTable";
			this.lblTable.Size = new System.Drawing.Size(40, 13);
			this.lblTable.TabIndex = 2;
			this.lblTable.Text = "Table: ";
			// 
			// lblColumn
			// 
			this.lblColumn.AutoSize = true;
			this.lblColumn.Location = new System.Drawing.Point(12, 51);
			this.lblColumn.Name = "lblColumn";
			this.lblColumn.Size = new System.Drawing.Size(48, 13);
			this.lblColumn.TabIndex = 4;
			this.lblColumn.Text = "Column: ";
			// 
			// cboColumn
			// 
			this.cboColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboColumn.FormattingEnabled = true;
			this.cboColumn.Location = new System.Drawing.Point(78, 48);
			this.cboColumn.Name = "cboColumn";
			this.cboColumn.Size = new System.Drawing.Size(121, 21);
			this.cboColumn.TabIndex = 3;
			this.cboColumn.SelectedIndexChanged += new System.EventHandler(this.cboColumn_SelectedIndexChanged);
			// 
			// txtSql
			// 
			this.txtSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSql.BackColor = System.Drawing.SystemColors.Control;
			this.txtSql.Location = new System.Drawing.Point(12, 259);
			this.txtSql.Multiline = true;
			this.txtSql.Name = "txtSql";
			this.txtSql.ReadOnly = true;
			this.txtSql.Size = new System.Drawing.Size(510, 113);
			this.txtSql.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 243);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "SQL:";
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.Location = new System.Drawing.Point(366, 378);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(75, 23);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(447, 378);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 8;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdCreateIndex
			// 
			this.cmdCreateIndex.Location = new System.Drawing.Point(6, 43);
			this.cmdCreateIndex.Name = "cmdCreateIndex";
			this.cmdCreateIndex.Size = new System.Drawing.Size(75, 23);
			this.cmdCreateIndex.TabIndex = 10;
			this.cmdCreateIndex.Text = "Create";
			this.cmdCreateIndex.UseVisualStyleBackColor = true;
			this.cmdCreateIndex.Click += new System.EventHandler(this.cmdCreateIndex_Click);
			// 
			// lblIndexStatus
			// 
			this.lblIndexStatus.AutoSize = true;
			this.lblIndexStatus.Location = new System.Drawing.Point(6, 27);
			this.lblIndexStatus.Name = "lblIndexStatus";
			this.lblIndexStatus.Size = new System.Drawing.Size(86, 13);
			this.lblIndexStatus.TabIndex = 11;
			this.lblIndexStatus.Text = "Status: No Index";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmdDropIndex);
			this.groupBox1.Controls.Add(this.lblIndexStatus);
			this.groupBox1.Controls.Add(this.cmdCreateIndex);
			this.groupBox1.Location = new System.Drawing.Point(309, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(213, 124);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Spatial Index";
			// 
			// cmdDropIndex
			// 
			this.cmdDropIndex.Location = new System.Drawing.Point(87, 43);
			this.cmdDropIndex.Name = "cmdDropIndex";
			this.cmdDropIndex.Size = new System.Drawing.Size(75, 23);
			this.cmdDropIndex.TabIndex = 12;
			this.cmdDropIndex.Text = "Drop";
			this.cmdDropIndex.UseVisualStyleBackColor = true;
			this.cmdDropIndex.Click += new System.EventHandler(this.cmdDropIndex_Click);
			// 
			// chkEnabled
			// 
			this.chkEnabled.AutoSize = true;
			this.chkEnabled.Location = new System.Drawing.Point(15, 187);
			this.chkEnabled.Name = "chkEnabled";
			this.chkEnabled.Size = new System.Drawing.Size(65, 17);
			this.chkEnabled.TabIndex = 13;
			this.chkEnabled.Text = "Enabled";
			this.chkEnabled.UseVisualStyleBackColor = true;
			// 
			// txtMinScale
			// 
			this.txtMinScale.Location = new System.Drawing.Point(15, 161);
			this.txtMinScale.Name = "txtMinScale";
			this.txtMinScale.Size = new System.Drawing.Size(100, 20);
			this.txtMinScale.TabIndex = 14;
			// 
			// txtMaxScale
			// 
			this.txtMaxScale.Location = new System.Drawing.Point(121, 161);
			this.txtMaxScale.Name = "txtMaxScale";
			this.txtMaxScale.Size = new System.Drawing.Size(100, 20);
			this.txtMaxScale.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 145);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 13);
			this.label5.TabIndex = 16;
			this.label5.Text = "Min Scale:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(118, 145);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Max Scale:";
			// 
			// cmdScanScale
			// 
			this.cmdScanScale.Location = new System.Drawing.Point(227, 159);
			this.cmdScanScale.Name = "cmdScanScale";
			this.cmdScanScale.Size = new System.Drawing.Size(80, 23);
			this.cmdScanScale.TabIndex = 18;
			this.cmdScanScale.Text = "Scan";
			this.cmdScanScale.UseVisualStyleBackColor = true;
			// 
			// QueryWizard
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(534, 413);
			this.Controls.Add(this.cmdScanScale);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtMaxScale);
			this.Controls.Add(this.txtMinScale);
			this.Controls.Add(this.chkEnabled);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtSql);
			this.Controls.Add(this.lblColumn);
			this.Controls.Add(this.cboColumn);
			this.Controls.Add(this.lblTable);
			this.Controls.Add(this.cboTable);
			this.Name = "QueryWizard";
			this.Text = "QueryWizard";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cboTable;
		private System.Windows.Forms.Label lblTable;
		private System.Windows.Forms.Label lblColumn;
		private System.Windows.Forms.ComboBox cboColumn;
		private System.Windows.Forms.TextBox txtSql;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdCreateIndex;
		private System.Windows.Forms.Label lblIndexStatus;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button cmdDropIndex;
		private System.Windows.Forms.CheckBox chkEnabled;
		private System.Windows.Forms.TextBox txtMinScale;
		private System.Windows.Forms.TextBox txtMaxScale;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button cmdScanScale;
	}
}