using System;
using Boilerplate.Attributes;
using TMPro;
using UnityEngine;

namespace Boilerplate.InputLayoutCommons
{
    [CreateAssetMenu(fileName = "New InputLayoutData", menuName = "ScriptableObjects/InputLayoutData")]
    public class InputLayoutData : ScriptableObject
    {
        #region Variables

        [Foldout("Sprite Asset")]
        [SerializeField] private TMP_SpriteAsset _spriteAsset;

        [Foldout("Layouts Sprite Atlas")]
        [SerializeField] private Texture _spriteAtlasXB;
        [SerializeField] private Texture _spriteAtlasPS;
        [SerializeField] private Texture _spriteAtlasPC;

        [Foldout("Sprite Index")]
        [SerializeField] private int _northIndex;
        [SerializeField] private int _eastIndex;
        [SerializeField] private int _southIndex;
        [SerializeField] private int _westIndex;
        [Space(10)]
        [SerializeField] private int _rightShoulderIndex;
        [SerializeField] private int _leftShoulderIndex;
        [Space(10)]
        [SerializeField] private int _rightTriggerIndex;
        [SerializeField] private int _leftTriggerIndex;
        [Space(10)]
        [SerializeField] private int _pauseIndex;
        [SerializeField] private int _selectIndex;
        [Space(10)]
        [SerializeField] private int _rightStickIndex;
        [SerializeField] private int _leftStickIndex;
        [SerializeField] private int _directionalIndex;

        #endregion Variables

        #region Properties

        public TMP_SpriteAsset SpriteAsset => _spriteAsset;

        public Texture SpriteAtlasXB => _spriteAtlasXB;
        public Texture SpriteAtlasPS => _spriteAtlasPS;
        public Texture SpriteAtlasPC => _spriteAtlasPC;

        public int NorthIndex => _northIndex;
        public int EastIndex => _eastIndex;
        public int SouthIndex => _southIndex;
        public int WestIndex => _westIndex;

        public int RightShoulderIndex => _rightShoulderIndex;
        public int LeftShoulderIndex => _leftShoulderIndex;

        public int RightTriggerIndex => _rightTriggerIndex;
        public int LeftTriggerIndex => _leftTriggerIndex;

        public int PauseIndex => _pauseIndex;
        public int SelectIndex => _selectIndex;

        public int RightStickIndex => _rightStickIndex;
        public int LeftStickIndex => _leftStickIndex;
        public int DirectionalIndex => _directionalIndex;

        #endregion Properties
    }
}