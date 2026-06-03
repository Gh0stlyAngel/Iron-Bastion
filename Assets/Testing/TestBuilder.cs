using UnityEngine;

public class TestBuilder : MonoBehaviour
{
    private BuildSpot _spot;
    [SerializeField] private TestBuilderPrefab _tower;

    private void Start()
    {
        _spot = GetComponent<BuildSpot>();
        Tower tower = _tower.testTower.GetComponent<Tower>();
        _spot.BuildTower(tower);
    }

}
