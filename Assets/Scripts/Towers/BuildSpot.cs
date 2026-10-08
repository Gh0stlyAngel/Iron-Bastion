using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public Vector3 PlacementPosition => _placement.transform.position;
    [SerializeField] private GameObject _placement;

    public Tower CurrentTower {  get; private set; }

    public bool IsOccupied => CurrentTower != null;

    public void BuildTower(Tower towerPrefab)
    {
        CurrentTower = Instantiate(towerPrefab, PlacementPosition, towerPrefab.transform.rotation, transform);
    }

    public void RemoveTower()
    {
        if (CurrentTower != null)
        {
            Destroy(CurrentTower.gameObject);
        }

        CurrentTower = null;
    }
}
