using System;
using System.Collections;
using System.Collections.Generic;
using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using Boilerplate.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Boilerplate.Pause
{
    public class MainMenuUI : MonoBehaviour
    {
        #region Variables

        [Foldout("UI")]
        [SerializeField] private GameObject _pauseSubcanvas;
        [SerializeField] private GameObject _firstToSelectGameObject;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;

        [Foldout("References")]
        [SerializeField] private GenericScenes _genericScenes;

        [Foldout("Broadcasters")]
        [SerializeField] private GameSceneEventChannel _loadSceneEvent;
        [SerializeField] private VoidEventChannel _setUIActionMapEvent;
        [SerializeField] private VoidEventChannel _setGameplayActionMapEvent;

        #endregion Variables

        #region Messages

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _quitButton.onClick.AddListener(OnQuitClick);

            OpenMainMenu();
        }

        private void OnDisable()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _quitButton.onClick.AddListener(OnQuitClick);
        }

        #endregion Messages

        #region Methods

        private void OpenMainMenu()
        {
            _pauseSubcanvas.SetActive(true);

            EventSystem.current.SetSelectedGameObject(_firstToSelectGameObject);

            EventUtils.BroadcastEvent(_setUIActionMapEvent);
        }

        private void CloseMainMenu()
        {
            _pauseSubcanvas.SetActive(false);

            EventUtils.BroadcastEvent(_setGameplayActionMapEvent);
        }

        private void OnStartClick()
        {
            CloseMainMenu();
            EventUtils.BroadcastEvent(_loadSceneEvent, _genericScenes.GameplayScene);
        }

        private void OnQuitClick()
        {
            CloseMainMenu();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion Methods
    }
}