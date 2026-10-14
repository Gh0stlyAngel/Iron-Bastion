using UnityEngine;
using UnityEngine.UIElements;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] private BuildSelector _selector;
    [SerializeField] private PlayerResources _resources;
    [SerializeField] private GameObject _buildMenuUI;
    [SerializeField] private Vector3 _buildMenuOffset;
    private RectTransform menuTransform;
    private BuildSpot _currentSpot;

    private void Start()
    {
        menuTransform = _buildMenuUI.GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        _selector.SelectionChanged += OnSelectionChanged;
    }

    private void OnDisable()
    {
        _selector.SelectionChanged -= OnSelectionChanged;
    }

    private void OnSelectionChanged(BuildSpot spot)
    {
        if (spot != null && !spot.IsOccupied)
        {
            _currentSpot = spot;
            SetMenuPosition(_currentSpot);
        }
        else
        {
            Hide();
        }
    }


    private void SetMenuPosition(BuildSpot spot)
    {
        Vector3 spotPosition = Camera.main.WorldToScreenPoint(spot.transform.position);

        menuTransform.position = spotPosition + _buildMenuOffset;
        _buildMenuUI.SetActive(true);
        
    }

    public void TryBuildTower(Tower towerPrefab)
    {
        if (_resources.TrySpendGold(towerPrefab.Cost))
        {
            AudioManager.Instance.PlaySFX(SoundType.ButtonClick);
            _currentSpot.BuildTower(towerPrefab);
        }
        

        Hide();
        _selector.ResetSelector();
    }

    private void Hide()
    {
        _currentSpot = null;
        _buildMenuUI.SetActive(false);
        
    }
}
