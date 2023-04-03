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
    public class PauseUI : MonoBehaviour
    {
        #region Variables

        [Foldout("UI")]
        [SerializeField] private GameObject _iconSubcanvas;
        [SerializeField] private GameObject _pauseSubcanvas;
        [SerializeField] private GameObject _firstToSelectGameObject;

        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _quitButton;

        [Foldout("References")]
        [SerializeField] private GenericScenes _genericScenes;

        [Foldout("Listeners")]
        [SerializeField] private VoidEventChannel _inputGameplayPauseEvent;
        [SerializeField] private VoidEventChannel _inputUIPauseEvent;
        [SerializeField] private VoidEventChannel _inputUIBackEvent;

        [Foldout("Broadcasters")]
        [SerializeField] private GameSceneEventChannel _loadSceneEvent;
        [SerializeField] private VoidEventChannel _setUIActionMapEvent;
        [SerializeField] private VoidEventChannel _setGameplayActionMapEvent;

        #endregion Variables

        #region Messages

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(OnResumeClick);
            _menuButton.onClick.AddListener(OnMenuClick);
            _quitButton.onClick.AddListener(OnQuitClick);

            EventUtils.AddEventListener(_inputGameplayPauseEvent, OpenPause);
            EventUtils.AddEventListener(_inputUIPauseEvent, ClosePause);
            EventUtils.AddEventListener(_inputUIBackEvent, ClosePause);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.AddListener(OnResumeClick);
            _menuButton.onClick.AddListener(OnMenuClick);
            _quitButton.onClick.AddListener(OnQuitClick);

            EventUtils.RemoveEventListener(_inputGameplayPauseEvent, OpenPause);
            EventUtils.RemoveEventListener(_inputUIPauseEvent, ClosePause);
            EventUtils.RemoveEventListener(_inputUIBackEvent, ClosePause);
        }

        #endregion Messages

        #region Methods

        private void OpenPause()
        {
            _iconSubcanvas.SetActive(false);
            _pauseSubcanvas.SetActive(true);

            EventSystem.current.SetSelectedGameObject(_firstToSelectGameObject);

            EventUtils.BroadcastEvent(_setUIActionMapEvent);
        }

        private void ClosePause()
        {
            _iconSubcanvas.SetActive(true);
            _pauseSubcanvas.SetActive(false);

            EventUtils.BroadcastEvent(_setGameplayActionMapEvent);
        }

        private void OnResumeClick() => ClosePause();

        private void OnMenuClick()
        {
            ClosePause();
            EventUtils.BroadcastEvent(_loadSceneEvent, _genericScenes.MenuScene);
        }

        private void OnQuitClick()
        {
            ClosePause();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion Methods
    }
}