using System.Collections.Generic;

namespace Haxsen.DataObjects
{
    /// <summary>
    /// Structure of the question and answers in JSON fetched from the OpenTdb server.
    /// </summary>
    [System.Serializable]
    public class JsonResponseQuestionStructure
    {
        public int response_code;
        public List<QuestionStructure> results;
    }
}