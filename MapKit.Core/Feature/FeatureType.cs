﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using GeoAPI.Geometries;

namespace MapKit.Core
{
	[DebuggerDisplay("Name = {Name}")]
	public class FeatureType
	{
        private Dictionary<string, Attribute> _attributesByName;
        private List<Attribute> _attributes;

		public FeatureType(string name)
		{
			Name = name;
            _attributesByName = new Dictionary<string, Attribute>(StringComparer.CurrentCultureIgnoreCase);
            _attributes = new List<Attribute>();
		}

		public Feature NewFeature(long fid)
		{
			var feature = new Feature(this, fid, new object[FieldCount]);
			return feature;
		}

		public Feature NewFeature(long fid, object[] values)
		{
            if (values.Length > _attributes.Count) throw new ArgumentOutOfRangeException();
            var featureValues = new object[FieldCount];
			values.CopyTo(featureValues, 0);
			
			return new Feature(this, fid, featureValues);
		}

		public string Name { get; set; }

        //public OgcGeometryType GeometryType { get; set; }

		public string GetName(int index)
		{
            throw new NotImplementedException();
            
			//return _columns[index];
		}

		public int GetOrdinal(string name)
		{
            Attribute att;
            if (_attributesByName.TryGetValue(name, out att))
                return att.Ordinal;
            return -1;
		}

        public int FieldCount
        {
            get { return _attributes.Count; }
        }


		public string[] GetNames()
		{
            //return _columns;
            //throw new NotImplementedException();
            return null;
		}

        public Type GetAttributeType(string name)
        {
            Attribute att;
            if (_attributesByName.TryGetValue(name, out att))
                return att.Type;
            return null;
        }

        public object Clone()
        {
            var newFeatureType = new FeatureType(Name);

            foreach (var attribute in _attributes)
                newFeatureType.AddAttributes(attribute.Name, attribute.Type);

            return newFeatureType;
        }

        public Attribute AddAttributes(string name, Type type)
        {
            var attribute = new Attribute(name, type);
            attribute.FeatureType = this;
            attribute.Ordinal = FieldCount;

            _attributesByName.Add(name, attribute);
            _attributes.Add(attribute);

            return attribute;
        }
    }
}
