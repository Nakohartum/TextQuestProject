using TextProject.ConfigsScripts;
using TextProject.Game;
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
        private readonly GameFactory _gameFactory;
        private readonly GameConfig _gameConfig;
        private MainMenuController _mainMenuController;
        private GameController _gameController;

        public MainController(PlayerProfile playerProfile, Transform placeForUI, GameConfig gameConfig)
        {
            _placeForUI = placeForUI;
            _gameConfig = gameConfig;
            _playerProfile = playerProfile;
            _mainMenuFactory = new MainMenuFactory(_placeForUI, _gameConfig.MainMenuPrefab);
            _gameFactory = new GameFactory(_gameConfig.GameViewPrefab, _placeForUI);
            _playerProfile.CurrentState.SubscribeOnChange(ChangeCurrentState);
            ChangeCurrentState(_playerProfile.CurrentState.Value);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            DisposeAllControllers();
            DisposeAllFactories();
            _playerProfile.CurrentState.UnsubscribeFromChange(ChangeCurrentState);
        }

        private void DisposeAllFactories()
        {
            _gameFactory?.Dispose();
            _mainMenuFactory?.Dispose();
        }
        private void DisposeAllControllers()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
        }
        
        private void ChangeCurrentState(GameState state)
        {
            DisposeAllControllers();
            switch (state)
            {
                case GameState.MainMenu:
                    _mainMenuController = _mainMenuFactory.CreateMenu(_playerProfile);
                    break;
                case GameState.Game:
                    _gameController = _gameFactory.CreateGame(_playerProfile);
                    break;
            }
        }
    }
}