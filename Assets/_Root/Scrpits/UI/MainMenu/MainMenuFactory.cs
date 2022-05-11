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
            AsyncOperationHandle<GameObject> addressable = Addressables.LoadAssetAsync<GameObject>(_mainMenuPrefab);
            addressable.Completed += SetMainMenu;
            addressable.WaitForCompletion();
            Object.Instantiate(MainMenuView.gameObject, _placeForUI);
            MainMenuController = new MainMenuController(MainMenuView, playerProfile);
            MainMenuController.OnDestroy += OnDestroy;
            return MainMenuController;
        }

        private void OnDestroy()
        {
            MainMenuController.OnDestroy -= OnDestroy;
            foreach (var prefab in _addressablePrefabs)
            {
                Addressables.Release(prefab);
            }
            _addressablePrefabs.Clear();
        }


        private  void SetMainMenu(AsyncOperationHandle<GameObject> obj)
        {
            _addressablePrefabs.Add(obj);
            MainMenuView = obj.Result.GetComponent<MainMenuView>();
        }
    }
}