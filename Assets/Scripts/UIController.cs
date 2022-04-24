using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIController : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI _HealthUI, _ScoreUI, _ArmourUI;
        private GameObject _StatsUI, _StartUI, _LevelUpUI, _LevelUpUIS;

        private void OnEnable()
        {
            PlayerData.onHealthChange += ChangeHealth;
            PlayerData.onScoreChange += ChangeScore;
            PlayerData.onArmourChange += ChangeArmour; 
            PlayerData.onPointsTresholdReached += LevelUp;
            GameState.onGameOver += PauseUIChange;
        }

        void Start()
        {
            _StatsUI = GameObject.Find("UI").transform.GetChild(0).gameObject;
            _StartUI = GameObject.Find("UI").transform.GetChild(1).gameObject;
            _LevelUpUI = GameObject.Find("UI").transform.GetChild(2).gameObject;
            _LevelUpUIS = GameObject.Find("UI").transform.GetChild(3).gameObject;
            _HealthUI = _StatsUI.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
            _ArmourUI = _StatsUI.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>();
            _ScoreUI = _StatsUI.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>();
            _HealthUI.text = PlayerData.instance.maxHealth.ToString();
            _ScoreUI.text = 0.ToString();
            _ArmourUI.text = 0.ToString();
            _StatsUI.SetActive(false);
            _LevelUpUI.SetActive(false);
            _LevelUpUIS.SetActive(false);

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

        void LevelUp(int points)
        {
            StartCoroutine(LevelUpUi(points));
        }

        private IEnumerator LevelUpUi(int points)
        {
            GameState.instance.State = GameStateType.PAUSE;
            _LevelUpUI.SetActive(true);
            yield return new WaitForSeconds(2);
            if(points == 20 || points == 60 || points % 100 == 0)
            {
                _LevelUpUI.SetActive(false);
                _LevelUpUIS.SetActive(true);
                yield return new WaitForSeconds(2);
                _LevelUpUIS.SetActive(false);
            }
            else
            {
                _LevelUpUI.SetActive(false);
            }
            GameState.instance.State = GameStateType.PLAY;
            
            StopCoroutine(LevelUpUi(points));
        }
        public void ChangeArmour(int armour)
        {
            _ArmourUI.text = armour.ToString();
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
