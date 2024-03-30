using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public string GameName;
    public string SceneName;
    public ScreenOrientationType ScreenOrientation;
}

public enum ScreenOrientationType
{
    Landscape,
    Portrait
}
