using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIController : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI _HealthUI, _ScoreUI;


        private void OnEnable()
        {
            PlayerData.onHealthChange += ChangeHealth;
            PlayerData.onScoreChange += ChangeScore;
        }

        void Start()
        {
            _HealthUI = GameObject.Find("UI").transform.GetChild(0).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
            _ScoreUI = GameObject.Find("UI").transform.GetChild(0).GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();
            _HealthUI.text = 50.ToString();
            _ScoreUI.text = 0.ToString();
        }

        public void ChangeHealth(int max, int current)
        {
            _HealthUI.text = current.ToString();
            if (current < (max * 0.3f))
                _HealthUI.color = Color.red;
            else if (current < (max * 0.6f))
                _HealthUI.color = Color.yellow;
        }
        public void ChangeScore(int score)
        {
            _ScoreUI.text = score.ToString();
        }
    }
}
