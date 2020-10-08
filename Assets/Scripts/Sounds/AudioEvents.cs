using System;
using UnityEngine;
using Random = System.Random;

public class AudioEvents : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayWithOverlay("background_menu");
        PlayerInput.OnTouchBoard += HandlePlayerClick;
    }

    private void OnDestroy()
    {
        PlayerInput.OnTouchBoard -= HandlePlayerClick;
    }

    private void HandlePlayerClick(int x, int y)
    {
        AudioManager.Instance.PlayWithOverlay("chip");
    }
}
