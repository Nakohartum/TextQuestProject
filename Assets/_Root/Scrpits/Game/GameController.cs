using System.Collections.Generic;
using System.Threading.Tasks;
using _Root.Scrpits.Game;
using GamesTan.UI;
using TextProject.ConfigsScripts;
using TextProject.Player;
using TextProject.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
        private GameView _gameView;
        private TextConfig _currentConfig;
        private List<MessageData> _listOfMessages = new List<MessageData>();
        private int _textConfigIndex = 0;
        private Vector3 Offset = new Vector3(0, 2f, 0);
        private Button _currentButton;
        public GameController(PlayerProfile playerProfile, Transform placeForUI, GameView gameView,
            TextConfig currentConfig)
        {
            _playerProfile = playerProfile;
            _placeForUI = placeForUI;
            _gameView = gameView;
            _currentConfig = currentConfig;
            AddGameObject(_gameView.gameObject);
            InitGame();
            InitButtons();
        }

        private void InitButtons()
        {
            for (int i = 0; i < _gameView.Buttons.Length; i++)
            {
                _gameView.Buttons[i].Button.onClick.AddListener(ResumeGame);
            }
        }
        
        private void InitGame()
        {
            for (int i = 0; i < _gameView.CountObjects; i++)
            {
                _listOfMessages.Add(new MessageData{text = "Lorem Ipsum " + (i + 1)});
            }
            _gameView.PoolOfObjects.DoAwake(this);
        }

        private async void ResumeGame()
        {
            if (_textConfigIndex != 0)
            {
                _currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            }
            if (_currentButton != null)
            {
                _currentConfig = _currentButton.GetComponent<ChoosingButtons>().NextTextConfig;
                _gameView.CountObjects++;
                _listOfMessages.Add(new MessageData {text = _currentButton.GetComponentInChildren<TMP_Text>().text});
                _gameView.PoolOfObjects.ReloadData();
                
            }
            var count = _listOfMessages.Count;
            var textCount = 0;
            for (int i = 0; i < _gameView.Buttons.Length; i++)
            {
                _gameView.Buttons[i].gameObject.SetActive(false);
            }
            await Task.Delay(2000);
            for (int i = 0; i < _currentConfig.TextQuotes.Count; i++)
            {
                _gameView.CountObjects++;
                _listOfMessages.Add(new MessageData{text = _currentConfig.TextQuotes[textCount]});
                textCount++;
                var transform = _gameView.PoolOfObjects.content.position; 
                _gameView.PoolOfObjects.ReloadData();
                if (_textConfigIndex != 0)
                {
                    _gameView.PoolOfObjects.content.position = transform + Offset;
                }
                await Task.Delay(2000);
            }
            for (int j = 0; j < _currentConfig.ButtonsToChoose.Count; j++)
            {
                _gameView.Buttons[j].gameObject.SetActive(true);
                _gameView.Buttons[j].GetComponentInChildren<TMP_Text>().text =
                    _currentConfig.ButtonsToChoose[j].ChooseText;
                _gameView.Buttons[j].NextTextConfig = _currentConfig.ButtonsToChoose[j].NextTextConfig;
            }
            
            _textConfigIndex++;
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