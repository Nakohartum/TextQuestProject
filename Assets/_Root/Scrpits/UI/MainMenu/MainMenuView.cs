using UnityEngine;
using UnityEngine.UI;

namespace TextProject.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [field: Header("Buttons")]
        [field: SerializeField] public Button StartButton { get; private set; }
    }
}