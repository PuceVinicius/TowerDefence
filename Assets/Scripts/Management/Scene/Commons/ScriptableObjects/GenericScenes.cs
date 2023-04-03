using Boilerplate.Attributes;
using Boilerplate.Utilities;
using UnityEngine;

namespace Boilerplate.SceneManagement
{
    [CreateAssetMenu(fileName = "New GenericScenes", menuName = "ScriptableObjects/GenericScenes")]
    public class GenericScenes : DescriptionScriptableObject
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private GameScene _managersScene;
        [SerializeField] private GameScene _onboardingScene;
        [SerializeField] private GameScene _menuScene;
        [SerializeField] private GameScene _gameplayScene;

        #endregion

        #region Properties

        public GameScene ManagersScene => _managersScene;
        public GameScene OnboardingScene => _onboardingScene;
        public GameScene MenuScene => _menuScene;
        public GameScene GameplayScene => _gameplayScene;

        #endregion
    }
}