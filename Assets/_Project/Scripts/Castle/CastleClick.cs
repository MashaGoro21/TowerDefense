using System;
using UnityEngine;

namespace Castle
{
    public class CastleClick : MonoBehaviour
    {
        public event Action OnCastleClicked;

        public void HandleClick()
        {
            OnCastleClicked?.Invoke();
        }
    }
}

