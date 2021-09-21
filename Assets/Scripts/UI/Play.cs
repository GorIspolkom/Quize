using Quiz.QuizManagerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.UI
{
    public class Play : MonoBehaviour
    {
        [SerializeField] GameObject logPanel;
        Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("IsAbleStart", true);

            GetComponent<Button>().onClick.AddListener(() =>
            {

                QuizManager.Instance.SpawnLogPanel();
                Destroy(transform.parent.gameObject);
            });
        }
    }
}
