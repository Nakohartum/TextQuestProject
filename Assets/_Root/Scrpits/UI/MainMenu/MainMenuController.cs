using System;
using TextProject.Player;
using TextProject.Utils;

namespace TextProject.UI
{
    public class MainMenuController : BaseController
    {
        private MainMenuView _view;
        private PlayerProfile _playerProfile;
        public Action OnDestroy = () => { };

        public MainMenuController(MainMenuView view, PlayerProfile playerProfile)
        {
            _view = view;
            _playerProfile = playerProfile;
            AddGameObject(_view.gameObject);
        }

        private void InitViewButtons()
        {
            _view.StartButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _playerProfile.CurrentState.Value = GameState.Game;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _view.StartButton.onClick.RemoveAllListeners();
            OnDestroy.Invoke();
        }
    }
}