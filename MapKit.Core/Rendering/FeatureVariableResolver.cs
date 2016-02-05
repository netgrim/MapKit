using System;
using Ciloci.Flee;

namespace MapKit.Core
{
    class FeatureVariableResolver
    {
        public FeatureVariableResolver()
        {
        }

        public FeatureVariableResolver(FeatureType featureType)
        {
            FeatureType = featureType;
        }

        public void BindContext(ExpressionContext context)
        {
            context.Variables.ResolveVariableType += Variables_ResolveVariableType;
            context.Variables.ResolveVariableValue += Variables_ResolveVariableValue;
        }

        private void Variables_ResolveVariableType(object sender, ResolveVariableTypeEventArgs e)
        {
            if(FeatureType != null)
                e.VariableType = FeatureType.GetAttributeType(e.VariableName);
        }

        void Variables_ResolveVariableValue(object sender, ResolveVariableValueEventArgs e)
        {
            if (Feature == null) return;

            var value = Feature[e.VariableName];
            //if(value == null && e.VariableType.IsValueType)
            e.VariableValue = Convert.ChangeType(value, e.VariableType);
            //e.VariableValue = value;
        }

        public Feature Feature { get; set; }

        public FeatureType FeatureType { get; set; }
    }
}
