using System;

namespace MapKit.Core
{
    public interface IFeatureRenderer : IBaseRenderer
    {

        FeatureType InputFeatureType { get; set; }
    }

    public interface IGroupRenderer : IBaseRenderer
    {
        TimeSpan RenderTime { get; }

        int FeatureCount { get; }
    }

    public interface IRenderer : IBaseRenderer
    {
        //void Render();
    }

    public interface IBaseRenderer
    {
        void Render(Feature feature);
        
        void BeginScene(bool visible);

        void Compile(bool recursive);

        bool Visible { get; }

        int RenderCount { get; }

        ThemeNode Node { get; }

        IBaseRenderer Parent { get; set; }

        IBaseRenderer FindChildByName(string name);
    }

}
