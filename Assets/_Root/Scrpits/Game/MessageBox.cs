using GamesTan.UI;
using TMPro;
using UnityEngine;

namespace _Root.Scrpits.Game
{
    public class MessageBox : MonoBehaviour, IScrollCell
    {
        [SerializeField] private TMP_Text _text;
        public void BindData(string text)
        {
            _text.text = text;
        }
    }
}