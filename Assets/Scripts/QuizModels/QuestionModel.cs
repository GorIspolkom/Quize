using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz.Models
{
    [Serializable]
    public struct QuestionModel
    {
        //текст вопроса
        [JsonProperty("Question")]
        public string question;
        //описание при неправильном выборе
        [JsonProperty("Description")]
        public string description;

        //варианты ответа
        [JsonProperty("Answers")]
        public string[] answers;
        //номер правильного ответа
        [JsonProperty("RightAnswer")]
        public int rightAnswer;
        //название изображения (если есть)
        [JsonProperty("ImageName")]
        public string imageName;
        //текст верного ответа
        public string RightAnswerText => answers[rightAnswer];

        public QuestionModel(string question, string description, string[] answers, int rightAnswer, string imageName = "")
        {
            this.question = question;
            this.description = description;
            this.answers = answers;
            this.rightAnswer = rightAnswer;
            this.imageName = imageName;
        }
    }
}

