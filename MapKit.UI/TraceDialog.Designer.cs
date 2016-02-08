namespace MapKit.UI
{
    partial class TraceDialog
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
            this.components = new System.ComponentModel.Container();
            this.startNodeLabel = new System.Windows.Forms.Label();
            this.txtStartNode = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // startNodeLabel
            // 
            this.startNodeLabel.AutoSize = true;
            this.startNodeLabel.Location = new System.Drawing.Point(12, 9);
            this.startNodeLabel.Name = "startNodeLabel";
            this.startNodeLabel.Size = new System.Drawing.Size(61, 13);
            this.startNodeLabel.TabIndex = 0;
            this.startNodeLabel.Text = "Start Node:";
            // 
            // txtStartNode
            // 
            this.errorProvider1.SetError(this.txtStartNode, "Value must be numeric");
            this.txtStartNode.Location = new System.Drawing.Point(12, 25);
            this.txtStartNode.Name = "txtStartNode";
            this.txtStartNode.Size = new System.Drawing.Size(100, 20);
            this.txtStartNode.TabIndex = 1;
            this.txtStartNode.Text = "10005464";
            this.txtStartNode.Validated += new System.EventHandler(this.txtStartNode_Validated);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(213, 23);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(213, 52);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 3;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 100);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(61, 13);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "Start Node:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Trace Mode";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // TraceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 123);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.txtStartNode);
            this.Controls.Add(this.startNodeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TraceDialog";
            this.Text = "TraceDialog";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label startNodeLabel;
        private System.Windows.Forms.TextBox txtStartNode;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}