using System.Collections.Generic;
using UnityEngine;

namespace TextProject.Game
{
    public class GameView : MonoBehaviour
    {
        [field: Header("Pool for text")] 
        [field: SerializeField] private List<GameObject> _poolOfGameObjects;
        
    }
}