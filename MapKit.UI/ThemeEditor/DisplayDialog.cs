using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapKit.Core;
using System.Collections;

namespace MapKit.UI
{
	public partial class DisplayDialog : Form
	{
		public DisplayDialog()
		{
			InitializeComponent();
		}

		public Data.ThemeDs.LayerRow RuleSet { get; set; }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			FillStyleRules();

			if (listView.Items.Count > 1)
				modeCombo.SelectedIndex = 3;
			else
				modeCombo.SelectedIndex = 0;
		}

		private void FillStyleRules()
		{
			listView.Items.Clear();
			
			if (RuleSet == null) return;

			foreach (var rule in RuleSet.GetStyleRuleRows())
			    AddItem(rule);

			if (listView.Items.Count > 0)
			    listView.Items[0].Selected = true;
		}

		private ListViewItem AddItem(Data.ThemeDs.StyleRuleRow rule)
		{
			var item = listView.Items.Add(rule.Name);
			item.SubItems.Add(rule.Filter);
			//item.SubItems.Add(rule.MinScale.ToString());
			//item.SubItems.Add(rule.MaxScale.ToString());
			item.SubItems.Add(rule.Sequence.ToString());
			item.Tag = new StyleRule(rule);
			return item;
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void listView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count > 0)
			{
				var items = new object[listView.SelectedItems.Count];
				for (int i = 0; i < listView.SelectedItems.Count; i++)
					items[i] = listView.SelectedItems[i].Tag;
				propertyGrid.SelectedObjects = items;
			}
			else
				propertyGrid.SelectedObject = null;
		}

		private void modeCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			split.Panel1Collapsed = modeCombo.SelectedIndex == 0;
			cmdAdd.Visible =
				cmdRemove.Visible =
				cmdUp.Visible =
				cmdDown.Visible = !split.Panel1Collapsed;
		}

		private void cmdAdd_Click(object sender, EventArgs e)
		{
			var ds = RuleSet.Table.DataSet as ThemeDs;

			var newRule = ds.StyleRule.NewStyleRuleRow();
			newRule.LayerRow = RuleSet;
			ds.StyleRule.AddStyleRuleRow(newRule);

			var item = AddItem(newRule);
				
			listView.SelectedItems.Clear();
			item.Selected = true;
		}

		private void cmdRemove_Click(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count == 0) return;

			listView.BeginUpdate();
			foreach (ListViewItem item in new ArrayList(listView.SelectedItems))
			{
				var rule = item.Tag as StyleRule;
				rule.Row.Delete();

				item.Remove();
			}

			listView.EndUpdate();
		}

		private void cmdUp_Click(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count == 0) return;

		}

		private void cmdDown_Click(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count == 0) return;

		}
	}
}
