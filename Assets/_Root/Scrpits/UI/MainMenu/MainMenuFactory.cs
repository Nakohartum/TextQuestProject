using System.Collections.Generic;
using System.Threading.Tasks;
using TextProject.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TextProject.UI
{
    public class MainMenuFactory
    {
        private Transform _placeForUI;
        private AssetReference _mainMenuPrefab;

        private List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();
        public MainMenuView MainMenuView { get; private set; }
        public MainMenuController MainMenuController { get; private set; }
        public MainMenuFactory(Transform placeForUI, AssetReference mainMenuPrefab)
        {
            _placeForUI = placeForUI;
            _mainMenuPrefab = mainMenuPrefab;
        }

        public MainMenuController CreateMenu(PlayerProfile playerProfile)
        {
            if (_addressablePrefabs.Count == 0)
            {
                AsyncOperationHandle<GameObject> addressable = Addressables.LoadAssetAsync<GameObject>(_mainMenuPrefab);
                addressable.Completed += SetMainMenu;
                addressable.WaitForCompletion();
            }
            MainMenuController = new MainMenuController(MainMenuView, playerProfile);
            return MainMenuController;
        }
        
        private  void SetMainMenu(AsyncOperationHandle<GameObject> obj)
        {
            _addressablePrefabs.Add(obj);
            var go = Object.Instantiate(obj.Result, _placeForUI);
            MainMenuView = go.GetComponent<MainMenuView>();
        }
    }
}