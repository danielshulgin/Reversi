using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private GameObject firstPlayerCellPrefab;
    
    [SerializeField] private GameObject secondPlayerCellPrefab;
        
    [SerializeField] private BoxCollider planeBoxCollider;

    private float _boardWidth;
    
    private float _boardHeight;
    
    private Vector3 _boxColliderOffset;

    private List<GameObject> _chips = new List<GameObject>();

    
    private void Awake()
    {
        _boardWidth = planeBoxCollider.size.x;
        _boardHeight = planeBoxCollider.size.z;

        var planeColliderPosition = planeBoxCollider.gameObject.transform.position;
        _boxColliderOffset = new Vector3( planeColliderPosition.x - _boardWidth / 2f, 
            planeColliderPosition.y, planeColliderPosition.z - _boardHeight / 2f);
    }


    public void HandleUpdateBoard(Cell[,] cells)
    {
        foreach (var oldCell in _chips)
        {
            Destroy(oldCell);
        }
        _chips.Clear();
        
        foreach (var cell in cells)
        {
            if (cell.cellOwner == CellOwner.None)
            {
                continue;
            }
            var cellPrefab = cell.cellOwner == CellOwner.First ? firstPlayerCellPrefab : secondPlayerCellPrefab;
            var newChip = Instantiate(cellPrefab, transform);
            newChip.transform.position = _boxColliderOffset + new Vector3(cell.x * _boardWidth / 8f, 0f, cell.y * _boardHeight / 8f);
            _chips.Add(newChip);
        }
    }
}
