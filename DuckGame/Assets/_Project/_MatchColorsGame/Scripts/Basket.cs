using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchColorGame
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _basktColorableSprite;
        [SerializeField] private GameObject _basketObject;
        [SerializeField] private ParticleSystem _initializationParticleEffect;
        [SerializeField] private ParticleSystem _scoringParticleEffect;

        public ColorData ColorData { get; private set; }


        public void Initialize(ColorData colorData)
        {
            ColorData = colorData;
            _basktColorableSprite.color = ColorData.Color;
            _basketObject.SetActive(true);
            _initializationParticleEffect.Play();
        }

        public void PlayerScored()
        {
            _scoringParticleEffect.Play();
        }
    }
}
