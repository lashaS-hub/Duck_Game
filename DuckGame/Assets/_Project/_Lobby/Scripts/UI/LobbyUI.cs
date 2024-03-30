using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameButton _gameButtonPrefab; 
    [SerializeField] private Transform _gamePanel;

    private void Start()
    {
        foreach (GameData gameData in SceneController.Singleton.Games)
        {
            GameButton gameButton = Instantiate(_gameButtonPrefab, _gamePanel);

            gameButton.Initialize(gameData, SelectGame);
        }
    }

    public void SelectGame(GameData gameData)
    {
        SceneController.Singleton.LoadGame(gameData);
    }
}
