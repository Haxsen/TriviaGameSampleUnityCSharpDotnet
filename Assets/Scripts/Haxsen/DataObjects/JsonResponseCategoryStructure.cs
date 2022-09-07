namespace Haxsen.DataObjects
{
    /// <summary>
    /// Structure of the fetched categories in JSON from OpenTdb server.
    /// </summary>
    [System.Serializable]
    public class JsonResponseCategoryStructure
    {
        public CategoryStructure[] trivia_categories;
    }
}