using Michsky.UI.ModernUIPack;
using Quiz.QuizManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class CategoryButton : MonoBehaviour
    {
        public void Init(string name, GameObject categoryPanel)
        {
            Text text = this.gameObject.transform.GetChild(0).GetComponent<Text>();
            text.text = name;

            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                QuizManager.Instance.CreateQuiz(name);
                categoryPanel.SetActive(false);
            });
        }
    }
}
