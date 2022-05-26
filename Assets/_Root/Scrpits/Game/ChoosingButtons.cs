using TextProject.ConfigsScripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Root.Scrpits.Game
{
    public class ChoosingButtons : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TextConfig NextTextConfig { get; set; }
    }
}