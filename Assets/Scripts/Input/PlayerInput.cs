using System;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerInput : MonoBehaviour
{
    public static event Action<int, int> OnTouchBoard;
    
    [SerializeField] private BoxCollider planeBoxCollider;

    private float _boardWidth;
    
    private float _boardHeight;
    
    private Vector3 _boxColliderOffset;

    
    private void Awake()
    {
        _boardWidth = planeBoxCollider.size.x;
        _boardHeight = planeBoxCollider.size.z;

        var planeColliderPosition = planeBoxCollider.gameObject.transform.position;
        _boxColliderOffset = new Vector3( planeColliderPosition.x - _boardWidth / 2f, 
            0f, planeColliderPosition.z - _boardHeight / 2f);
    }

    private void Update()
    {
        CheckMouseDown();
    }

    private void CheckMouseDown()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var raycastHit, 20f))
        {
            return;
        }

        FindPointOnBoard(raycastHit.point);
    }

    private void FindPointOnBoard(Vector3 hitPosition)
    {
        var localHitPositionX = hitPosition.x - _boxColliderOffset.x;
        var localHitPositionY = hitPosition.z - _boxColliderOffset.z;

        var x = (int) (localHitPositionX / _boardWidth * 8f);
        var y = (int) (localHitPositionY / _boardHeight * 8f);

        x = Mathf.Clamp(x, 0, 8);
        y = Mathf.Clamp(y, 0, 8);

        OnTouchBoard?.Invoke(x, y);

        Debug.Log($"Mouse down on x: {x} y: {y}");
    }
}
