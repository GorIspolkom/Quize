using UnityEngine;
using System;
using Assets.Scripts.UI;
using Quiz.Configuration;
using System.Collections;
using Michsky.UI.ModernUIPack;
using Quiz.Models;

namespace Quiz.QuizManagerSystem
{
    class QuizManager : MonoBehaviour
    {
        //синглетон
        private static QuizManager _instance;
        public static QuizManager Instance => _instance;
        
        //интерфейс виктоирны
        [SerializeField] QuestionController questionController;
        [SerializeField] HeaderController header;
        //панели поражения и победы
        [SerializeField] GameObject losePanel, winPanel;
        //панель с вводом данных о пользователе
        [SerializeField] GameObject startPanel;
        //панель категорий
        [SerializeField] GameObject categoryPanel;
        //панель регистрации
        [SerializeField] GameObject logPanel;

        //данные о текущей викторине
        private QuizData quizInfo;
        //события верного и ошибочного ответа
        private Action<QuizData> onRightAnswer, onMistakeAnswer;
        //время на ответ
        private float timer;

        private void Awake()
        {
            _instance = this;
            losePanel.GetComponentInChildren<ProgressBar>().maxValue = ConfigurationData.TimeForLeave;
        }
        public void CreateQuiz(string category)
        {
            quizInfo = new QuizData(category);
            timer = Time.time + ConfigurationData.TimeForAnswer;
            questionController.InitPanel(quizInfo.CurrentQuestion);
            header.InitPanel(category);
            StartCoroutine(CheckTimer());
        }
        public void SetNextQuestion()
        {
            quizInfo.currentQuestion++;
            //поражение
            if (!quizInfo.IsAllowedMistake)
            {
                StopAllCoroutines();
                StartCoroutine(LoseWinAction(losePanel));
            }
            //следующий вопрос
            else if (quizInfo.IsNextOfQuiz)
            {
                timer = Time.time + ConfigurationData.TimeForAnswer;
                header.OnNextQuestion();
                questionController.InitPanel(quizInfo.CurrentQuestion);
            }
            //победа
            else
            {
                StopAllCoroutines();
                StartCoroutine(LoseWinAction(winPanel));
            }
        }
        //подписки на события
        public void SubscribeOnMistake(Action<QuizData> sub) => onMistakeAnswer += sub;
        public void SubscribeOnRight(Action<QuizData> sub) => onRightAnswer += sub;
        public void SubscribeOnAny(Action<QuizData> sub)
        {
            onRightAnswer += sub;
            onMistakeAnswer += sub;
        }
        //при выборе ответа (метод на кнопки ответа)
        public void ChoiceAnswer(int answerId)
        {
            header.SetProgressBarActive(false);
            timer = float.MaxValue;
            if (quizInfo.IsTrueAnswer(answerId))
            {
                onRightAnswer?.Invoke(quizInfo);
                questionController.PlayRightAnimation(answerId);
            }
            else
            {
                questionController.PlayWrongAnimation(answerId, quizInfo.CurrentQuestion.rightAnswer);
                onMistakeAnswer?.Invoke(quizInfo);
            }
        }
        //корутина проверки времени на ответ
        IEnumerator CheckTimer()
        {
            while (quizInfo.IsAllowedMistake && quizInfo.IsNextOfQuiz)
            {
                if (timer < Time.time)
                {
                    for (int i = 0; i < quizInfo.CurrentQuestion.answers.Length; i++)
                        if (i != quizInfo.CurrentQuestion.rightAnswer)
                            questionController.PlayWrongAnimation(i, quizInfo.CurrentQuestion.rightAnswer);
                    quizInfo.mistakeAllowed--;
                    onMistakeAnswer?.Invoke(quizInfo);
                    timer = float.MaxValue;
                }
                yield return null;
            }
        }
        //завершение викторины
        IEnumerator LoseWinAction(GameObject panel)
        {
            MailSender mailSender = new MailSender();
            mailSender.SendMail(quizInfo);
            panel.GetComponentInChildren<ProgressBar>().currentPercent = ConfigurationData.TimeForLeave;
            panel.SetActive(true);
            yield return new WaitForSecondsRealtime(ConfigurationData.TimeForLeave);
            panel.SetActive(false);
            SpawnStartPanel();
        }
        //создание панели для ввода данных пользователя
        public void SpawnStartPanel()
        {
            Instantiate(startPanel, GameObject.Find("Canvas").transform);

            logPanel.SetActive(true);
            categoryPanel.SetActive(true);
        }

        public void ActivateCategory(bool key)
        {
            categoryPanel.SetActive(key);
        }

        public void SpawnLogPanel()
        {
            Instantiate(logPanel, GameObject.Find("Canvas").transform);
        }
    }
}
