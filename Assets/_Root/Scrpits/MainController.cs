using TextProject.ConfigsScripts;
using TextProject.Player;
using TextProject.UI;
using TextProject.Utils;
using UnityEngine;

namespace TextProject
{
    public class MainController : BaseController
    {
        private readonly PlayerProfile _playerProfile;
        private readonly Transform _placeForUI;
        private readonly MainMenuFactory _mainMenuFactory;
        private readonly GameConfig _gameConfig;
        private MainMenuController _mainMenuController;
        
        public MainController(PlayerProfile playerProfile, Transform placeForUI, GameConfig gameConfig)
        {
            _placeForUI = placeForUI;
            _gameConfig = gameConfig;
            _playerProfile = playerProfile;
            _mainMenuFactory = new MainMenuFactory(_placeForUI, _gameConfig.MainMenuPrefab);
            _playerProfile.CurrentState.SubscribeOnChange(ChangeCurrentState);
            ChangeCurrentState(_playerProfile.CurrentState.Value);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            DisposeAllControllers();
            _playerProfile.CurrentState.UnsubscribeFromChange(ChangeCurrentState);
        }

        private void DisposeAllControllers()
        {
            _mainMenuController?.Dispose();
        }
        
        private void ChangeCurrentState(GameState state)
        {
            DisposeAllControllers();
            switch (state)
            {
                case GameState.MainMenu:
                    _mainMenuController = _mainMenuFactory.CreateMenu(_playerProfile);
                    break;
            }
        }
    }
}