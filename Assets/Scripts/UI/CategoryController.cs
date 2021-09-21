using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Json;

namespace Assets.Scripts.UI
{
    public class CategoryController : MonoBehaviour
    {
        [SerializeField] GameObject categoryPanel;
        [SerializeField] GameObject quizPanel;
        [SerializeField] GameObject button;

        private void Start()
        {
            //спавн кнопок выбора категории
            string[] categories = QuestionParserFromJson.Categories;
            foreach (string name in categories)
                Initbutton(name);
        }
        private void Initbutton(string name)
        {
            GameObject btn = Instantiate(button, this.transform);
            btn.GetComponent<CategoryButton>().Init(name, categoryPanel);
        }
    }
}

