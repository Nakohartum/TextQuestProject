using System.Collections.Generic;
using UnityEngine;

namespace TextProject.ConfigsScripts
{
    [CreateAssetMenu(fileName = nameof(TextConfig), menuName = "Game/Configs/"+nameof(TextConfig), order = 0)]
    public class TextConfig : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public List<string> TextQuotes { get; private set; }
        [field: SerializeField] public List<ButtonToChoose> ButtonsToChoose { get; private set; }
    }
}