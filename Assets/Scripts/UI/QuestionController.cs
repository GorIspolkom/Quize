using Michsky.UI.ModernUIPack;
using Quiz.Configuration;
using Quiz.Models;
using Quiz.QuizManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// ���������� ������ ������� �� ���������
    /// ������� ����� �������, ��������, �������� �������
    /// </summary>
    public class QuestionController : MonoBehaviour
    {
        [SerializeField] ModalWindowManager Tip;
        [SerializeField] Text questionText;
        [SerializeField] Image questionImage;
        [SerializeField] Transform answers;
        [SerializeField] ButtonManagerBasic button;
        [SerializeField] Animation questionAnimation;

        private List<Animator> buttons;

        private void Awake()
        {
            //�������� �� ����� ���������
            QuizManager.Instance.SubscribeOnAny(data => StartCoroutine(ShowTipCorutine(data.CurrentQuestion)));
        }
        //������������� ��������� ����������
        public void InitPanel(QuestionModel model)
        {
            questionText.text = model.question;
            if (model.imageName != null)
            {
                questionImage.gameObject.SetActive(true);
                questionImage.sprite = Resources.Load<Sprite>("Icons/" + model.imageName);
            }
            else
                questionImage.gameObject.SetActive(false);

            InitAnswers(model);
        }

        private void InitAnswers(QuestionModel model)
        {
            foreach (Transform answer in answers)
                Destroy(answer.gameObject);

            buttons = new List<Animator>();
            for (int i = 0; i < model.answers.Length; i++)
            {
                int indexBtn = i;
                ButtonManagerBasic btn = Instantiate(button, answers);
                btn.GetComponent<CanvasGroup>().alpha = 0;
                btn.buttonText = model.answers[i];
                buttons.Add(btn.GetComponent<Animator>());
                btn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Debug.Log(indexBtn);
                    QuizManager.Instance.ChoiceAnswer(indexBtn);
                    foreach (Animator button in buttons)
                        button.GetComponent<Button>().onClick = new ButtonClickedEvent();
                });
            }
            StartCoroutine(OutputQuestion());
        }
        //����� �������
        private IEnumerator OutputQuestion()
        {
            questionAnimation.Play();
            yield return new WaitForSecondsRealtime(ConfigurationData.TimeBetweenElementOutput);
            if(questionImage.IsActive()) 
                yield return new WaitForSecondsRealtime(ConfigurationData.TimeBetweenElementOutput);

            foreach (Animator animator in buttons)
            {
                animator.SetTrigger("In");
                yield return new WaitForSecondsRealtime(ConfigurationData.TimeBetweenElementOutput);
            }
        }
        //�������� ������
        public void PlayRightAnimation(int rightAnswer)
        {
            buttons[rightAnswer].SetTrigger("RightAnswer");
        }
        public void PlayWrongAnimation(int answer, int rightAnswer)
        {
            buttons[answer].SetTrigger("WrongAnswer");
            buttons[rightAnswer].SetTrigger("RightAnswer");
        }
        //����� ��������� � ������
        private IEnumerator ShowTipCorutine(QuestionModel model)
        {
            yield return new WaitForSecondsRealtime(1);
            ShowTip(model);
        }
        private void ShowTip(QuestionModel model)
        {
            Tip.windowTitle.text = model.answers[model.rightAnswer];
            Tip.windowDescription.text = model.description;

            Tip.OpenWindow();
        }

    }
}

