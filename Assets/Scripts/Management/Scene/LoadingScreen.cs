using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    #region Variables

    [Foldout("References")]
    [SerializeField] private GameObject _loadingScreenCanvas;

    [Foldout("Listeners")]
    [SerializeField] private BoolEventChannel _toggleLoadingScreenEvent;

    #endregion

    #region Messages

    private void OnEnable()
    {
        EventUtils.AddEventListener(_toggleLoadingScreenEvent, ToggleLoadingScreen);
    }

    private void OnDisable()
    {
        EventUtils.RemoveEventListener(_toggleLoadingScreenEvent, ToggleLoadingScreen);
    }

    #endregion

    #region Methods

    private void ToggleLoadingScreen(bool value)
    {
        if (_loadingScreenCanvas.activeSelf != value)
            _loadingScreenCanvas.SetActive(value);
    }

    #endregion
}