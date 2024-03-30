using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MatchColorGame
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private Button _lobbyButton;
        [SerializeField] private GameObject _endPanel;

        private void Start()
        {
            _lobbyButton.onClick.AddListener(SceneController.Singleton.LoadLobby);

            DuckGame.OnGameEnd += ShowEndPanel;
        }

        private void OnDestroy()
        {
            DuckGame.OnGameEnd -= ShowEndPanel;
        }

        public void ShowEndPanel()
        {
            _endPanel.SetActive(true);
        }
    }
}