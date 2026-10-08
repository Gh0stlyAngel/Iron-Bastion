using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class BuildSelector : MonoBehaviour
{
    private BuildSpot _currntBuildSpot;
    [SerializeField] private LayerMask _buildObjectMask;

    public event Action<BuildSpot> SelectionChanged;

    private void Update()
    {
        if (!TryGetPointerPosition(out Vector2 position))
            return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        TryToSelectObject(position);
    }

    private bool TryGetPointerPosition(out Vector2 position)
    {
        position = default;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            position = Touchscreen.current.primaryTouch.position.ReadValue();
            return true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            position = Mouse.current.position.ReadValue();
            return true;
        }

        return false;
    }

    private void TryToSelectObject(Vector2 position)
    {
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint();
        Ray ray = Camera.main.ScreenPointToRay(position);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100f, Color.red);


        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _buildObjectMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.TryGetComponent<BuildSpot>(out var buildSpot))
            {
                if (_currntBuildSpot == buildSpot)
                {
                    return;
                }
                _currntBuildSpot = buildSpot;
            }
            else if (hit.collider.gameObject.TryGetComponent<Tower>(out var tower))
            {
                buildSpot = tower.GetComponentInParent<BuildSpot>();
                if (_currntBuildSpot == buildSpot)
                {
                    return;
                }
                _currntBuildSpot = buildSpot;
            }
        }
        else
        {
            _currntBuildSpot = null;
        }

        SelectionChanged?.Invoke(_currntBuildSpot);
    }

    public void ResetSelector()
    {
        _currntBuildSpot = null;
    }
}
