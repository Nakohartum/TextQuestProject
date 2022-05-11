using System.Collections.Generic;
using TextProject.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TextProject.Game
{
    public class GameFactory
    {
        private AssetReference _gameViewReference;
        private Transform _placeForUI;
        private GameView _gameView;
        private List<AsyncOperationHandle<GameObject>> _addressablesPrefabs =
            new List<AsyncOperationHandle<GameObject>>();
        public GameController GameController { get; private set; }
        public GameFactory(AssetReference gameViewReference, Transform placeForUI)
        {
            _gameViewReference = gameViewReference;
            _placeForUI = placeForUI;
        }

        public GameController CreateGame(PlayerProfile playerProfile)
        {
            if (_addressablesPrefabs.Count == 0)
            {
                AsyncOperationHandle<GameObject> addressable =
                    Addressables.LoadAssetAsync<GameObject>(_gameViewReference);
                addressable.Completed += SetGameView;
                addressable.WaitForCompletion();
            }

            GameController = new GameController(playerProfile, _placeForUI, _gameView);
            return GameController;
        }

        private void SetGameView(AsyncOperationHandle<GameObject> obj)
        {
            var go = Object.Instantiate(obj.Result, _placeForUI);
            _gameView = go.GetComponent<GameView>();
        }
    }
}