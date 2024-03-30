using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameNameText;
    [SerializeField] private Button _gameButton;

    private Action<GameData> _gameButtonAction;
    private GameData _gameData;

    private void Start()
    {
        _gameButton.onClick.AddListener(() => _gameButtonAction.Invoke(_gameData));
    }

    public void Initialize(GameData gameData, Action<GameData> onClickAction)
    {
        _gameData = gameData;
        _gameNameText.text = gameData.GameName;
        _gameButtonAction = onClickAction;
    }
}
