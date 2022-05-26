using System.Collections.Generic;
using _Root.Scrpits.Game;
using GamesTan.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TextProject.Game
{
    public class GameView : MonoBehaviour
    {
        [field: Header("Pool for text")] 
        [field: SerializeField] public SuperScrollRect PoolOfObjects { get; private set; }
        [field: SerializeField] public int CountObjects { get; set; }
        [field: SerializeField] public ChoosingButtons[] Buttons { get; private set; }
        
    }
}