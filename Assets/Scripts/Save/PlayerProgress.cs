using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using Boilerplate.FuncChannels;
using Boilerplate.SaveCommons;
using UnityEngine;

namespace Boilerplate.Save
{
    public class PlayerProgress : MonoBehaviour
    {
        #region Variables

        [Foldout("Delegates")]
        [SerializeField] private PlayerSaveVoidFuncChannel _getPlayerSaveFunc;

        [Foldout("Listeners")]
        [SerializeField] private VoidEventChannel _saveProgressEvent;
        [SerializeField] private VoidEventChannel _loadProgressEvent;

        private PlayerSave _playerSave;

        #endregion

        #region Messages

        private void OnEnable()
        {
            LoadProgress();

            EventUtils.AddEventListener(_saveProgressEvent, SaveProgress);
            EventUtils.AddEventListener(_loadProgressEvent, LoadProgress);

            FuncUtils.SetDelegate(_getPlayerSaveFunc, GetPlayerSave);
        }

        private void OnDisable()
        {
            EventUtils.RemoveEventListener(_saveProgressEvent, SaveProgress);
            EventUtils.RemoveEventListener(_loadProgressEvent, LoadProgress);

            FuncUtils.ResetDelegate(_getPlayerSaveFunc);
        }

        #endregion

        #region Methods

        [ContextMenu("Test Save")]
        private void SaveProgress() => SaveLoad.Save(_playerSave);

        [ContextMenu("Test Load")]
        private void LoadProgress()
        {
            _playerSave = SaveLoad.Load();
            _playerSave ??= new PlayerSave();
        }

        [ContextMenu("Reset Save")]
        public void ResetSave()
        {
            SaveLoad.Save(new PlayerSave());
        }

        private PlayerSave GetPlayerSave() => _playerSave;

        #endregion
    }
}