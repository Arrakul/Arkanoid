using System;
using UnityEngine;

[Serializable]
public class EventGame : MonoBehaviour
{
    public delegate void OnEvent();
    public event OnEvent onEvent;
    
    public void Play()
    {
        onEvent?.Invoke();
    }
}
