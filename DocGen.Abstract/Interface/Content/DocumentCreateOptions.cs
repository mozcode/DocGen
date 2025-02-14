namespace DocGen.Abstract.Interface.Content
{
    /// <summary>
    /// Represents advanced options for document creation 
    /// (e.g. background image, landscape mode, etc.).
    /// 
    /// Buraya ileride margin vb. parametreler eklenebilir, 
    /// veya IPageSettings devreye girebilir.
    /// </summary>
    public class DocumentCreateOptions
    {
        public bool AddBackgroundImage { get; set; }
        public string? BackgroundImagePath { get; set; }
        public bool IsLandscape { get; set; }
    }
}
