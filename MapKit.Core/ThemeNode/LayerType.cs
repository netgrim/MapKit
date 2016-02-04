using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapKit.Core
{
    class LayerType : NodeType
    {
        public LayerType()
            :base("Layer", Layer.ElementName, typeof(Layer))
        {



            //var menu = (ToolStripDropDownItem)sender;
            //var parent = SelectedThemeNode as ContainerNode;

            //menu.DropDownItems.Clear();
            //if (parent != null && _map.SourceTypes.Count > 0)
            //{
            //    foreach (var sourceType in _map.SourceTypes)
            //        if (sourceType.CanAddTo(parent))
            //        {

            //            var sourceMenu = (ToolStripMenuItem)menu.DropDownItems.Add(sourceType.Text ?? sourceType.Name);
            //            sourceMenu.Tag = sourceType;
            //            sourceMenu.Click += new EventHandler(sourceMenu_Click);
            //        }
            //}
            //else
            //{
            //    var none = menu.DropDownItems.Add("None");
            //    none.Enabled = false;
            //}

        }
    }


               
}
