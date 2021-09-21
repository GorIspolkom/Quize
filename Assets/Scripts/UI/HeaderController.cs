using Michsky.UI.ModernUIPack;
using Quiz.Configuration;
using Quiz.QuizManagerSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// контроллер шапки викторины
    /// Управляет: 
    ///     1. Информации о вопросе (номер и прогресс бар),
    ///     2. Количетсво ошибок до поражения
    ///     3. Таймер до конца вопроса
    /// Инициализирует: 
    ///     1. Имя категории
    ///     2. Количество ошибок до поражения
    /// </summary>
    public class HeaderController : MonoBehaviour
    {
        [SerializeField] Text category;
        [SerializeField] ProgressBar countQuestion;
        [SerializeField] ProgressBar timerBar;
        [SerializeField] Transform hearts;
        [SerializeField] Animator heartPrefab;

        private void Start()
        {
            QuizManager.Instance.SubscribeOnMistake(data => PopHeart(data.mistakeAllowed));
        }
        public void InitPanel(string nameCategory)
        {
            timerBar.maxValue = ConfigurationData.TimeForAnswer;
            timerBar.currentPercent = ConfigurationData.TimeForAnswer;
            timerBar.isOn = true;
            category.text = $"Категория: {nameCategory}";
            countQuestion.currentPercent = 1;
            countQuestion.UpdateUI();
            foreach (Transform heart in hearts)
                Destroy(heart.gameObject);
            if (ConfigurationData.CountOfMistakes > 1)
            {
                for (int i = 0; i < ConfigurationData.CountOfMistakes; i++)
                    Instantiate(heartPrefab, hearts);
            }
        }
        public void OnNextQuestion()
        {
            timerBar.currentPercent = ConfigurationData.TimeForAnswer;
            timerBar.isOn = true;
            countQuestion.currentPercent += 1f;
            countQuestion.UpdateUI();
        }

        public void PopHeart(int curAllowedMistake)
        {
            Debug.Log(curAllowedMistake);
            if (hearts.childCount > curAllowedMistake)
                hearts.GetChild(curAllowedMistake).GetComponent<Animator>().Play("Out");
        }
        public void SetProgressBarActive(bool isOn)
        {
            timerBar.isOn = isOn;
        }
    }
}

