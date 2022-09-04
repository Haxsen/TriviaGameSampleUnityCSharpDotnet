using System.Collections.Generic;

namespace Haxsen.DataStructures
{
    [System.Serializable]
    public class JsonResponseStructure
    {
        public int response_code;
        public List<QuestionStructure> results;
    }
}