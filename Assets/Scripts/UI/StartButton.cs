using Assets.Scripts.ProfileData;
using Michsky.UI.ModernUIPack;
using Quiz.QuizManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Json;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// контроллер кнопки для инициализации пользователя
    /// Кнопка активно при условии заполненности всех полей
    /// В случае пустого ввода выводится уведомление
    /// </summary>
    public class StartButton : MonoBehaviour
    {
        //поля ввода
        [SerializeField] CustomInputField[] inputFields;
        //уведомление о пустом вводе
        [SerializeField] NotificationManager notification;
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Button clicked");
                CreateUser();
                if (QuestionParserFromJson.Categories.Length == 1)
                {
                    QuizManager.Instance.ActivateCategory(false);
                    Destroy(transform.parent.gameObject);

                    QuizManager.Instance.CreateQuiz(QuestionParserFromJson.Categories[0]);
                }
                else
                {
                    Destroy(transform.parent.gameObject);
                }
            });
        }
        public void ResetInput()
        {
            for (int i = 0; i < inputFields.Length; i++)
            {
                inputFields[i].inputText.text = "";
                inputFields[i].AnimateIn();
                inputFields[i].UpdateState();
            }
            animator.Play("Out");
        }

        private void CreateUser()
        {
            string name = inputFields[0].inputText.text;
            string mail = inputFields[1].inputText.text;
            string number = inputFields[2].inputText.text;
            Profile.InitProfile(name, mail, number);
        }

        public void CheckAbilityToStart()
        {
            foreach(CustomInputField inputField in inputFields)
                if (inputField.inputText.text == "")
                {
                    animator.SetBool("IsAbleStart", false);
                    return;
                }
            animator.SetBool("IsAbleStart", true);
        }

        public void CheckInput(CustomInputField inputField)
        {
            if (inputField.inputText.text == "")
                notification.OpenNotification();
        }
    }
}

