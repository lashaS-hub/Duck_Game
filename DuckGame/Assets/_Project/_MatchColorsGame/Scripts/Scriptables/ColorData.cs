using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchColorGame
{
    [CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
    public class ColorData : ScriptableObject
    {
        public Color Color;
        public ColorType ColorType;
    }

    public enum ColorType
    {
        Purple,
        Yellow,
        Blue
    }
}