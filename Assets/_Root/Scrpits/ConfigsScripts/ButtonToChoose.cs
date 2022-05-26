using UnityEngine;

namespace TextProject.ConfigsScripts
{
    [CreateAssetMenu(fileName = nameof(ButtonToChoose), menuName = "Game/Configs/"+nameof(ButtonToChoose) , order = 0)]
    public class ButtonToChoose : ScriptableObject
    {
        [field: SerializeField] public string ChooseText { get; private set; }
        [field: SerializeField] public TextConfig NextTextConfig { get; private set; }
    }
}