using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public Vector3 PlacementPosition => _placement.transform.position;
    [SerializeField] private GameObject _placement;

    public bool IsOccupied => _isOccupied;
    private bool _isOccupied;

    public void BuildTower(Tower towerPrefab)
    {
        Instantiate(towerPrefab, PlacementPosition, towerPrefab.transform.rotation);
        _isOccupied = true;
    }
}
