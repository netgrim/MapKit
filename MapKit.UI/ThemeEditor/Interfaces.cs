using MapKit.Core;
using System;

namespace MapKit.UI
{
    interface IThemeNodeWrapper
    {
        ThemeNode Node { get; }
    }

    interface IEditableNode
    {
        event EventHandler LabelChanged;
        
        string Label { get; set; }
        bool CanMoveTo(ContainerNode target, int oldIndex, int newIndex);
        bool CanCopyTo(object destination);
        void MoveTo(ContainerNode destination, int index);
        void CopyTo(object destination);
        bool CanShowProperties();
        bool CanRename();

   }
}
