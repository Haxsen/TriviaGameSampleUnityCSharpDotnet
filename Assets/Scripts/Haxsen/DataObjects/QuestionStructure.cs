namespace Haxsen.DataObjects
{
    [System.Serializable]
    public class QuestionStructure
    {
        public string category;
        public string type;
        public string difficulty;
        public string question;
        public string correct_answer;
        public string[] incorrect_answers;
    }
}