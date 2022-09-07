using System.Collections.Generic;

namespace Haxsen.DataObjects
{
    [System.Serializable]
    public class JsonResponseQuestionStructure
    {
        public int response_code;
        public List<QuestionStructure> results;
    }
}