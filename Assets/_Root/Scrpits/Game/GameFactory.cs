using System;
using System.Collections.Generic;
using TextProject.ConfigsScripts;
using TextProject.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace TextProject.Game
{
    public class GameFactory : IDisposable
    {
        private AssetReference _gameViewReference;
        private readonly GameConfig _gameConfig;
        private Transform _placeForUI;
        private GameView _gameView;
        private List<AsyncOperationHandle<GameObject>> _addressablesPrefabs =
            new List<AsyncOperationHandle<GameObject>>();
        public GameController GameController { get; private set; }
        public GameFactory(GameConfig gameConfig, Transform placeForUI)
        {
            _gameViewReference = gameConfig.GameViewPrefab;
            _gameConfig = gameConfig;
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

            GameController = new GameController(playerProfile, _placeForUI, _gameView, _gameConfig.InitialConfig);
            return GameController;
        }

        private void SetGameView(AsyncOperationHandle<GameObject> obj)
        {
            var go = Object.Instantiate(obj.Result, _placeForUI);
            _gameView = go.GetComponent<GameView>();
        }
        
        public void Dispose()
        {
            foreach (var asyncOperationHandle in _addressablesPrefabs)
            {
                Addressables.Release(asyncOperationHandle);
            }
            _addressablesPrefabs.Clear();
        }
    }
}