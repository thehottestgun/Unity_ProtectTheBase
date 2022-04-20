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
        private GameObject _StatsUI, _StartUI, _PauseUI;


        private void OnEnable()
        {
            PlayerData.onHealthChange += ChangeHealth;
            PlayerData.onScoreChange += ChangeScore;
            GameState.onGameOver += PauseUIChange;
        }

        void Start()
        {
            _StatsUI = GameObject.Find("UI").transform.GetChild(0).gameObject;
            _StartUI = GameObject.Find("UI").transform.GetChild(1).gameObject;
            _HealthUI = _StatsUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
            _ScoreUI = _StatsUI.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();
            _HealthUI.text = 50.ToString();
            _ScoreUI.text = 0.ToString();
            _StatsUI.SetActive(false);

        }
        private void Update()
        {
            if(Input.GetButtonDown("Fire1") && GameState.instance.State == GameStateType.STOP)
            {
                Time.timeScale = 1;
                GameState.instance.State = GameStateType.PLAY;
                StartGameUIChange();
            }
            
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

        public void StartGameUIChange()
        {
            _StartUI.SetActive(false);
            _StatsUI.SetActive(true);
        }

        public void PauseUIChange()
        {
            _StartUI.SetActive(true);
            _StatsUI.SetActive(false);
        }
    }
}
