using System.Diagnostics;
using GeoAPI.Geometries;
using System.Dynamic;

namespace MapKit.Core
{
    [DebuggerDisplay("Fid = {Fid}")]
	public class Feature: DynamicObject
	{
		internal Feature(FeatureType featureType, long fid, object[] values)
		{
			FeatureType = featureType;
			Values = values;
			Fid = fid;
		}

        public Feature(Feature feature)
            :this(feature.FeatureType, feature.Fid, feature.Values)
        {
        }

		private IGeometry _geometry;
		public IGeometry Geometry
		{
			get { return _geometry; }
			set
			{
                //if (value != null && value.OgcGeometryType != FeatureType.GeometryType)
                //    throw new Exception("Geometry type mismatch");
				_geometry = value;
			}
		}

		public FeatureType FeatureType { get; private set; }
		
		public object this[int index]
		{
			get { return Values[index]; }
			set { Values[index] = value; }
		}

		public object this[string name]
		{
			get { return Values[FeatureType.GetOrdinal(name)]; }
			set { Values[FeatureType.GetOrdinal(name)] = value; }
		}

		public object[] Values { get; private set; }

		public long Fid { get; private set; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            int ordinal = FeatureType.GetOrdinal(binder.Name);
            if (ordinal >= 0)
            {
                result = 2;// Values[ordinal];
                return true;
            }
            result = null;
            return false;
        }
	}
}
