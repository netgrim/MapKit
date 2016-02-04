using System;
using MapKit.Core;
using System.ComponentModel;

namespace MapKit.UI
{
	class LayerWrapper : ContainerNodeWrapper
	{
		public LayerWrapper(Layer layer)
            : base(layer)
		{
			Layer = layer;
		}

		[Browsable(false)]
		public Layer Layer { get; set; }
      	
		public double MinScale
		{
			get { return Layer.MinScale; }
			set
			{
				Layer.MinScale = value;
				//LayerRow._LayerRow.UpdateMinScale();
			}
		}
		
		public string MaxScale
		{
			get { return !Layer.MaxScale.HasValue ? "Max" : Layer.MaxScale.ToString(); }
			set
			{
				if (string.IsNullOrEmpty(value) || value.ToLower() == "max")
					Layer.MaxScale = null;
				else
					Layer.MaxScale = double.Parse(value);

				//LayerRow._LayerRow.UpdateMaxScale();
                throw new NotImplementedException();
			}

		
		}

        public override bool CanMoveTo(ContainerNode destination, int oldIndex, int newIndex)
		{
			return destination is Group;
		}
	}
}
