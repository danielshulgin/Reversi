using System;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    [SerializeField] private Animator cameraAnimator;
    
    
    private void Awake()
    {
        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleStartGame()
    {
        cameraAnimator.Play("fly_to_board", 0, 0f);
    }
    
    private void HandleEndGame()
    {
        cameraAnimator.Play("Idle", 0, 0f);
    }
}
