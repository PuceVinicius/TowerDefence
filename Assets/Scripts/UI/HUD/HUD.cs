using System;
using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using TowerDefence.EnemiesCommons;
using TowerDefence.TowersCommons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefence.HUD
{
    public class HUD : MonoBehaviour
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private GameObject _towersCanvas;
        [SerializeField] private Button _areaTowerButton;
        [SerializeField] private Button _burstTowerButton;
        [SerializeField] private Button _fastTowerButton;

        [Foldout("Broadcaster")]
        [SerializeField] private TowerTypeEventChannel _onClickTowerEvent;

        [Foldout("Debug")]
        [SerializeField] private Button _lastButton;

        #endregion

        #region Messages

        private void OnEnable()
        {
            _areaTowerButton.onClick.AddListener(OnAreaTowerClick);
            _burstTowerButton.onClick.AddListener(OnBurstTowerClick);
            _fastTowerButton.onClick.AddListener(OnFastTowerClick);
        }

        private void OnDisable()
        {
            _areaTowerButton.onClick.RemoveListener(OnAreaTowerClick);
            _burstTowerButton.onClick.RemoveListener(OnBurstTowerClick);
            _fastTowerButton.onClick.RemoveListener(OnFastTowerClick);
        }

        #endregion

        #region Methods

        private void OnAreaTowerClick() => OnTowerClick(TowerType.Area, _areaTowerButton);

        private void OnBurstTowerClick() => OnTowerClick(TowerType.Burst, _burstTowerButton);

        private void OnFastTowerClick() => OnTowerClick(TowerType.Fast, _fastTowerButton);

        private void OnTowerClick(TowerType towerType, Button button)
        {
            ProcessColors(button);
            EventUtils.BroadcastEvent(_onClickTowerEvent, towerType);
        }

        private void ProcessColors(Button button)
        {
            if (button != _lastButton)
            {
                if (_lastButton)
                    SetButtonColors(_lastButton, Color.white);
                SetButtonColors(button, Color.green);
                _lastButton = button;
                return;
            }

            _lastButton = null;
            SetButtonColors(button, Color.white);
            EventSystem.current.SetSelectedGameObject(null);
        }

        private void SetButtonColors(Button button, Color color)
        {
            var colorBlock = button.colors;
            colorBlock.normalColor = color;
            button.colors = colorBlock;
        }

        #endregion
    }
}