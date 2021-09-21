using Quiz.Configuration;
using Quiz.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Json;

namespace Quiz.QuizManagerSystem
{
    public struct QuizData
    {
        //случайные вопросы викторине в случайном порядке
        public readonly QuestionModel[] questions;
        //категория викторины
        public readonly string category;
        //время начала викторины
        public DateTime startTime;
        //ответы пользователя
        public List<int> answers;
        //текущий номер вопроса
        public int currentQuestion;
        //количество ошибок до поражения
        public int mistakeAllowed;

        public QuestionModel CurrentQuestion => questions[currentQuestion];
        public bool IsAllowedMistake => mistakeAllowed > 0;
        public bool IsNextOfQuiz => currentQuestion < ConfigurationData.QuestionInQuiz;

        public QuizData(string categoryName)
        {
            //перемешивание вопросов
            List<QuestionModel> ablequestionsList = new List<QuestionModel>(QuestionParserFromJson.GetQuestionsByCategory(categoryName));
            List<QuestionModel> questionsList = new List<QuestionModel>();
            for(int i = 0; i < ConfigurationData.QuestionInQuiz && ablequestionsList.Count > 0; i++)
            {
                int index = UnityEngine.Random.Range(0, ablequestionsList.Count);
                questionsList.Add(ablequestionsList[index]);
                ablequestionsList.RemoveAt(index);
            }
            questions = questionsList.ToArray();    

            startTime = DateTime.Now;
            category = categoryName;
            answers = new List<int>();
            currentQuestion = 0;
            mistakeAllowed = ConfigurationData.CountOfMistakes;
        }

        public bool IsTrueAnswer(int answerIndex)
        {
            answers.Add(answerIndex);
            if (CurrentQuestion.rightAnswer != answerIndex)
            {
                mistakeAllowed--;
                return false;
            }
            else
                return true;
        }
    }
}
