using System;
using TextProject.ConfigsScripts;
using TextProject.Player;
using UnityEngine;

namespace TextProject
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private Transform _placeForUI;
        private MainController _mainController;
        private void Start()
        {
            var playerProfile = new PlayerProfile(_gameConfig.InitialState);
            _mainController = new MainController(playerProfile, _placeForUI, _gameConfig);
        }

        private void OnDestroy()
        {
            _mainController.Dispose();
        }
    }
}

