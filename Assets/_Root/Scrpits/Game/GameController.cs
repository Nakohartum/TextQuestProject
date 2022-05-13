using System.Collections.Generic;
using _Root.Scrpits.Game;
using GamesTan.UI;
using TextProject.Player;
using TextProject.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace TextProject.Game
{
    public struct MessageData
    {
        public string text;
    }
    public class GameController : BaseController, ISuperScrollRectDataProvider
    {
        private PlayerProfile _playerProfile;
        private Transform _placeForUI;
        private int _resumer = 100;
        private GameView _gameView;
        private List<MessageData> _listOfMessages = new List<MessageData>();

        public GameController(PlayerProfile playerProfile, Transform placeForUI, GameView gameView)
        {
            _playerProfile = playerProfile;
            _placeForUI = placeForUI;
            _gameView = gameView;
            AddGameObject(_gameView.gameObject);
            InitGame();
            InitButtons();
        }

        public void InitButtons()
        {
            _gameView.Button.onClick.AddListener(ResumeGame);
        }
        
        private void InitGame()
        {
            for (int i = 0; i < _gameView.CountObjects; i++)
            {
                _listOfMessages.Add(new MessageData{text = "Lorem Ipsum " + (i + 1)});
            }
            _gameView.PoolOfObjects.DoAwake(this);
        }

        private void ResumeGame()
        {
            _gameView.CountObjects += 5;
            var count = _listOfMessages.Count;
            for (int i = count; i < count + 5; i++)
            {
                _listOfMessages.Add(new MessageData{text = "Lorem Ipsum " + (i + 1)});
            }

            var transform = _gameView.PoolOfObjects.content.position; 
            _gameView.PoolOfObjects.ReloadData();
            _gameView.PoolOfObjects.content.position = transform;
        }
        
        public int GetCellCount()
        {
            return _gameView.CountObjects;
        }

        public void SetCell(GameObject cell, int index)
        {
            var item = cell.GetComponent<MessageBox>();
            item.BindData(_listOfMessages[index].text);
        }
    }
}