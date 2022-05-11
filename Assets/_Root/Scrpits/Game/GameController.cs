using TextProject.Player;
using TextProject.Utils;
using UnityEngine;

namespace TextProject.Game
{
    public class GameController : BaseController
    {
        private PlayerProfile _playerProfile;
        private Transform _placeForUI;
        private GameView _gameView;

        public GameController(PlayerProfile playerProfile, Transform placeForUI, GameView gameView)
        {
            _playerProfile = playerProfile;
            _placeForUI = placeForUI;
            _gameView = gameView;
            AddGameObject(_gameView.gameObject);
        }
        
        
    }
}