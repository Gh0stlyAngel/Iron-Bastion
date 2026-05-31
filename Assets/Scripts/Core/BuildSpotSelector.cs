using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildSpotSelector : MonoBehaviour
{
    private BuildSpot _currnetBuildSpot;
    [SerializeField] private LayerMask _buildSpotMask;

    public event Action<BuildSpot> SelectionChanged;

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            else
            {
                TryToSelectSpot();
            }
            
        }
    }

    private void TryToSelectSpot()
    {
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint();
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100f, Color.red);


        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _buildSpotMask))
        {
            if (hit.collider.gameObject.TryGetComponent<BuildSpot>(out var buildSpot))
            {
                if (_currnetBuildSpot == buildSpot)
                {
                    return;
                }
                if(buildSpot.IsOccupied)
                {
                    return;
                }
                _currnetBuildSpot = buildSpot;
            }
        }
        else
        {
            _currnetBuildSpot = null;
        }

        SelectionChanged?.Invoke(_currnetBuildSpot);
    }

    public void ResetSelector()
    {
        _currnetBuildSpot = null;
    }
}
