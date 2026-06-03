using UnityEngine;

public class TestBuilder : MonoBehaviour
{
    private BuildSpot _spot;
    [SerializeField] private TestBuilderPrefab _tower;

    private void Start()
    {
        _spot = GetComponent<BuildSpot>();
        _spot.BuildTower(_tower.testTower);
    }

}
