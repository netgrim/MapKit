using System.ComponentModel;
using System.Windows.Forms;

namespace Cyrez.Graphics.UI
{
    partial class ThemeEditor : Control 
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator mnuThemesSep1;
            System.Windows.Forms.ToolStripSeparator mnuLayerSep;
            System.Windows.Forms.ToolStripSeparator mnuThemeSep;
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.cmnTheme = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuThemeAddLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThemeRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThemeExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThemeExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnThemes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuThemesAddNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThemesLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThemesSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThemesSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnLayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuLayerAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLayerAddText = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLayerAddStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLayerRemove = new System.Windows.Forms.ToolStripMenuItem();
            mnuThemesSep1 = new System.Windows.Forms.ToolStripSeparator();
            mnuLayerSep = new System.Windows.Forms.ToolStripSeparator();
            mnuThemeSep = new System.Windows.Forms.ToolStripSeparator();
            this.cmnTheme.SuspendLayout();
            this.cmnThemes.SuspendLayout();
            this.cmnLayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuThemesSep1
            // 
            mnuThemesSep1.Name = "mnuThemesSep1";
            mnuThemesSep1.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuLayerSep
            // 
            mnuLayerSep.Name = "mnuLayerSep";
            mnuLayerSep.Size = new System.Drawing.Size(147, 6);
            // 
            // mnuThemeSep
            // 
            mnuThemeSep.Name = "mnuThemeSep";
            mnuThemeSep.Size = new System.Drawing.Size(126, 6);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "Theme.xml";
            this.openFileDialog.Filter = "Configuration Theme (*.xml)|*.xml|Tous (*.*)|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "Theme.xml";
            this.saveFileDialog.Filter = "Configuration Theme (*.xml)|*.xml|Tous (*.*)|*.*";
            // 
            // cmnTheme
            // 
            this.cmnTheme.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuThemeAddLayer,
            mnuThemeSep,
            this.mnuThemeRemove,
            this.mnuThemeExport,
            this.mnuThemeExpandAll});
            this.cmnTheme.Name = "contextMenuStrip1";
            this.cmnTheme.Size = new System.Drawing.Size(130, 98);
            // 
            // mnuThemeAddLayer
            // 
            this.mnuThemeAddLayer.Name = "mnuThemeAddLayer";
            this.mnuThemeAddLayer.Size = new System.Drawing.Size(129, 22);
            this.mnuThemeAddLayer.Text = "Add Layer";
            this.mnuThemeAddLayer.Click += new System.EventHandler(this.mnuThemeAddLayer_Click);
            // 
            // mnuThemeRemove
            // 
            this.mnuThemeRemove.Name = "mnuThemeRemove";
            this.mnuThemeRemove.Size = new System.Drawing.Size(129, 22);
            this.mnuThemeRemove.Text = "Remove";
            this.mnuThemeRemove.Click += new System.EventHandler(this.mnuThemeRemove_Click);
            // 
            // mnuThemeExport
            // 
            this.mnuThemeExport.Name = "mnuThemeExport";
            this.mnuThemeExport.Size = new System.Drawing.Size(129, 22);
            this.mnuThemeExport.Text = "Export";
            this.mnuThemeExport.Click += new System.EventHandler(this.mnuThemeExport_Click);
            // 
            // mnuThemeExpandAll
            // 
            this.mnuThemeExpandAll.Name = "mnuThemeExpandAll";
            this.mnuThemeExpandAll.Size = new System.Drawing.Size(129, 22);
            this.mnuThemeExpandAll.Text = "Expand All";
            this.mnuThemeExpandAll.Click += new System.EventHandler(this.mnuThemeExpandAll_Click);
            // 
            // cmnThemes
            // 
            this.cmnThemes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuThemesAddNew,
            mnuThemesSep1,
            this.mnuThemesLoad,
            this.mnuThemesSave,
            this.mnuThemesSaveAs});
            this.cmnThemes.Name = "contextMenuStrip1";
            this.cmnThemes.Size = new System.Drawing.Size(124, 98);
            // 
            // mnuThemesAddNew
            // 
            this.mnuThemesAddNew.Name = "mnuThemesAddNew";
            this.mnuThemesAddNew.Size = new System.Drawing.Size(123, 22);
            this.mnuThemesAddNew.Text = "Add new";
            // 
            // mnuThemesLoad
            // 
            this.mnuThemesLoad.Name = "mnuThemesLoad";
            this.mnuThemesLoad.Size = new System.Drawing.Size(123, 22);
            this.mnuThemesLoad.Text = "Load...";
            // 
            // mnuThemesSave
            // 
            this.mnuThemesSave.Name = "mnuThemesSave";
            this.mnuThemesSave.Size = new System.Drawing.Size(123, 22);
            this.mnuThemesSave.Text = "Save";
            this.mnuThemesSave.Click += new System.EventHandler(this.mnuThemesSave_Click);
            // 
            // mnuThemesSaveAs
            // 
            this.mnuThemesSaveAs.Name = "mnuThemesSaveAs";
            this.mnuThemesSaveAs.Size = new System.Drawing.Size(123, 22);
            this.mnuThemesSaveAs.Text = "Save As...";
            this.mnuThemesSaveAs.Click += new System.EventHandler(this.mnuThemeSaveAs_Click);
            // 
            // cmnLayer
            // 
            this.cmnLayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLayerAdd,
            this.mnuLayerAddText,
            this.mnuLayerAddStyle,
            mnuLayerSep,
            this.mnuLayerRemove});
            this.cmnLayer.Name = "contextMenuStrip1";
            this.cmnLayer.Size = new System.Drawing.Size(151, 98);
            // 
            // mnuLayerAdd
            // 
            this.mnuLayerAdd.Name = "mnuLayerAdd";
            this.mnuLayerAdd.Size = new System.Drawing.Size(150, 22);
            this.mnuLayerAdd.Text = "Add Layer";
            this.mnuLayerAdd.Click += new System.EventHandler(this.mnuDisplayAdd_Click);
            // 
            // mnuLayerAddText
            // 
            this.mnuLayerAddText.Name = "mnuLayerAddText";
            this.mnuLayerAddText.Size = new System.Drawing.Size(150, 22);
            this.mnuLayerAddText.Text = "Add Text Rule";
            this.mnuLayerAddText.Click += new System.EventHandler(this.mnuDisplayAddText_Click);
            // 
            // mnuLayerAddStyle
            // 
            this.mnuLayerAddStyle.Name = "mnuLayerAddStyle";
            this.mnuLayerAddStyle.Size = new System.Drawing.Size(150, 22);
            this.mnuLayerAddStyle.Text = "Add Style Rule";
            this.mnuLayerAddStyle.Click += new System.EventHandler(this.mnuDisplayAddStyle_Click);
            // 
            // mnuLayerRemove
            // 
            this.mnuLayerRemove.Name = "mnuLayerRemove";
            this.mnuLayerRemove.Size = new System.Drawing.Size(150, 22);
            this.mnuLayerRemove.Text = "Remove";
            this.mnuLayerRemove.Click += new System.EventHandler(this.mnuDisplayRemove_Click);
            // 
            // ThemeEditor
            // 
            this.Name = "PathEditor";
            this.cmnTheme.ResumeLayout(false);
            this.cmnThemes.ResumeLayout(false);
            this.cmnLayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip cmnTheme;
        private System.Windows.Forms.ToolStripMenuItem mnuThemeAddLayer;
        private System.Windows.Forms.ToolStripMenuItem mnuThemeRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuThemeExport;
        private System.Windows.Forms.ContextMenuStrip cmnThemes;
        private System.Windows.Forms.ToolStripMenuItem mnuThemesAddNew;
        private System.Windows.Forms.ToolStripMenuItem mnuThemesLoad;
        private System.Windows.Forms.ToolStripMenuItem mnuThemesSave;
        private System.Windows.Forms.ToolStripMenuItem mnuThemesSaveAs;
        private System.Windows.Forms.ContextMenuStrip cmnLayer;
        private System.Windows.Forms.ToolStripMenuItem mnuLayerAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuLayerRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuLayerAddText;
        private System.Windows.Forms.ToolStripMenuItem mnuLayerAddStyle;
        private System.Windows.Forms.ToolStripMenuItem mnuThemeExpandAll;
    }
}
