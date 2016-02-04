namespace GeoAPI.Geometries
{
    /// <summary>
    /// Enumeration of OGC Geometry Types
    /// </summary>
    public enum OgcGeometryType
    {
        /// <summary>
        /// Point.
        /// </summary>
        Point = 1,

        /// <summary>
        /// LineString.
        /// </summary>
        LineString = 2,

        /// <summary>
        /// Polygon.
        /// </summary>
        Polygon = 3,

        /// <summary>
        /// MultiPoint.
        /// </summary>
        MultiPoint = 4,

        /// <summary>
        /// MultiLineString.
        /// </summary>
        MultiLineString = 5,

        /// <summary>
        /// MultiPolygon.
        /// </summary>
        MultiPolygon = 6,

        /// <summary>
        /// GeometryCollection.
        /// </summary>
        GeometryCollection = 7,

        /// <summary>
        /// CircularString
        /// </summary>
        CircularString = 8,
        
        /// <summary>
        /// CompoundCurve
        /// </summary>
        CompoundCurve = 9,
        
        /// <summary>
        /// CurvePolygon
        /// </summary>
        CurvePolygon = 10,
        
        /// <summary>
        /// MultiCurve
        /// </summary>
        MultiCurve = 11,
        
        /// <summary>
        /// MultiSurface
        /// </summary>
        MultiSurface = 12,
        
        /// <summary>
        /// Curve
        /// </summary>
        Curve = 13,
        
        /// <summary>
        /// Surface
        /// </summary>
        Surface = 14,
        
        /// <summary>
        /// PolyhedralSurface
        /// </summary>
        PolyhedralSurface = 15,
        
        /// <summary>
        /// TIN
        /// </summary>
// ReSharper disable InconsistentNaming
        TIN = 16,
// ReSharper restore InconsistentNaming

        
		/// <summary>
        /// Point with Z coordinate.
        /// </summary>
        PointZ = 1001,

        /// <summary>
        /// LineString with Z coordinate.
        /// </summary>
        LineStringZ = 1002,

        /// <summary>
        /// Polygon with Z coordinate.
        /// </summary>
        PolygonZ = 1003,

        /// <summary>
        /// MultiPoint with Z coordinate.
        /// </summary>
        MultiPointZ = 1004,

        /// <summary>
        /// MultiLineString with Z coordinate.
        /// </summary>
        MultiLineStringZ = 1005,

        /// <summary>
        /// MultiPolygon with Z coordinate.
        /// </summary>
        MultiPolygonZ = 1006,

        /// <summary>
        /// GeometryCollection with Z coordinate.
        /// </summary>
        GeometryCollectionZ = 1007,

        /// <summary>
        /// Point with M ordinate value.
        /// </summary>
        PointM = 2001,

        /// <summary>
        /// LineString with M ordinate value.
        /// </summary>
        LineStringM = 2002,

        /// <summary>
        /// Polygon with M ordinate value.
        /// </summary>
        PolygonM = 2003,

        /// <summary>
        /// MultiPoint with M ordinate value.
        /// </summary>
        MultiPointM = 2004,

        /// <summary>
        /// MultiLineString with M ordinate value.
        /// </summary>
        MultiLineStringM = 2005,

        /// <summary>
        /// MultiPolygon with M ordinate value.
        /// </summary>
        MultiPolygonM = 2006,

        /// <summary>
        /// GeometryCollection with M ordinate value.
        /// </summary>
        GeometryCollectionM = 2007,

        /// <summary>
        /// Point with Z coordinate and M ordinate value.
        /// </summary>
        PointZM = 3001,

        /// <summary>
        /// LineString with Z coordinate and M ordinate value.
        /// </summary>
        LineStringZM = 3002,

        /// <summary>
        /// Polygon with Z coordinate and M ordinate value.
        /// </summary>
        PolygonZM = 3003,

        /// <summary>
        /// MultiPoint with Z coordinate and M ordinate value.
        /// </summary>
        MultiPointZM = 3004,

        /// <summary>
        /// MultiLineString with Z coordinate and M ordinate value.
        /// </summary>
        MultiLineStringZM = 3005,

        /// <summary>
        /// MultiPolygon with Z coordinate and M ordinate value.
        /// </summary>
        MultiPolygonZM = 3006,

        /// <summary>
        /// GeometryCollection with Z coordinate and M ordinate value.
        /// </summary>
        GeometryCollectionZM = 3007
    };
}