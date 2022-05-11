using TextProject.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TextProject.ConfigsScripts
{
    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Game/Configs/"+nameof(GameConfig), order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReference MainMenuPrefab { get; private set; }
        [field: SerializeField] public AssetReference GameViewPrefab { get; private set; }
        [field: SerializeField] public GameState InitialState { get; private set; }
    }
}