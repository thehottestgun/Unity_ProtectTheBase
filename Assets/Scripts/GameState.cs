using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class GameState
    {
        public delegate void OnGameOver();
        public static event OnGameOver onGameOver;

        private static GameState _instance;
        public GameStateType State;
        public static GameState instance
        {
            get
            {
                if (GameState._instance == null)
                    GameState._instance = new GameState();
                return GameState._instance;
            }
            set => GameState._instance = value;
        }
        protected GameState() => this.NewGameState();

        private void NewGameState()
        {
            UnityEngine.Time.timeScale = 0;
            this.State = GameStateType.STOP;
        }
        public void Reset()
        {
            onGameOver?.Invoke();
            UnityEngine.Time.timeScale = 0;
            this.State = GameStateType.STOP;
        }
    }

    


}
