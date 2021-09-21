using Newtonsoft.Json.Linq;
using Quiz.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils.Json
{
    public static class QuestionParserFromJson
    {
        //все созданные викторины
        private static Dictionary<string, QuestionModel[]> _quizes = GetQuizesDatas();
        //категории викторин
        public static string[] Categories => _quizes.Keys.ToArray();
        public static QuestionModel[] GetQuestionsByCategory(string category) =>
            _quizes[category];


        private static string ReadFile()
        {
            string sFilePath = Path.Combine(Application.streamingAssetsPath, "Questions.json");
            string sJson;
            if (Application.platform == RuntimePlatform.Android)
            {
                UnityWebRequest www = UnityWebRequest.Get(sFilePath);
                www.SendWebRequest();
                while (!www.isDone) ;
                sJson = www.downloadHandler.text;
            }
            else sJson = File.ReadAllText(sFilePath, Encoding.UTF8);

            return sJson;

        }

        private static Dictionary<string, QuestionModel[]> GetQuizesDatas()
        {
            Dictionary<string, QuestionModel[]> quizes = new Dictionary<string, QuestionModel[]>();
            JObject root = JObject.Parse(ReadFile());
            JProperty property = root.First as JProperty;
            while (property != null)
            {
                QuestionModel[] quiz = GetQuestionModels(root[property.Name] as JArray);
                quizes.Add(property.Name, quiz);
                property = property.Next as JProperty;
            }
            return quizes;
        }
        public static string[] GetCategories(JObject root)
        {
            JProperty property = root.First as JProperty;
            List<string> categories = new List<string>();
                Debug.Log(property.Name);
            while(property != null)
            {
                categories.Add(property.Name);
                Debug.Log(property.Name);
                property = property.Next as JProperty;
            }
            return categories.ToArray();
        }
        private static QuestionModel[] GetQuestionModels(JArray root)
        {
            QuestionModel[] questions = new QuestionModel[root.Count];
            for (int i = 0; i < questions.Length; i++)
                questions[i] = ParseQuestion((JObject)root[i]);
            return questions;
        }
        private static QuestionModel ParseQuestion(JObject jQuestion)
        {
            string question = jQuestion.Value<string>("Question");
            string description = jQuestion.Value<string>("Description");
            //string imageName = jQuestion.ContainsKey("ImageName") ? jQuestion.Value<string>("ImageName") : null;
            string imageName = jQuestion.Value<string>("ImageName");
            JArray jAnswer = jQuestion.Value<JArray>("Answers");
            string[] answers = new string[jAnswer.Count];
            for (int i = 0; i < answers.Length; i++)
                answers[i] = jAnswer[i].Value<string>();

            int rightAnswer = jQuestion.Value<int>("RightAnswer");

            return new QuestionModel(question, description, answers, rightAnswer, imageName);
        }
    }
}