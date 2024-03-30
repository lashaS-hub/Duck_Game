using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class SceneController : MonoBehaviour
{
    public static SceneController Singleton;

    [SerializeField] private GameData _lobbyData;

    public List<GameData> Games;


    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadGame(GameData game)
    {
        Screen.orientation = game.ScreenOrientation switch
        {
            ScreenOrientationType.Landscape => ScreenOrientation.LandscapeLeft,
            ScreenOrientationType.Portrait => ScreenOrientation.Portrait,
            _ => Screen.orientation
        };

        SceneManager.LoadScene(game.SceneName);
    }

    internal void LoadLobby()
    {
        LoadGame(_lobbyData);
    }
}