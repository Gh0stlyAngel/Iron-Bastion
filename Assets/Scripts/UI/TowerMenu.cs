using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    [SerializeField] private BuildSelector _selector;
    [SerializeField] private PlayerResources _resources;
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private Vector3 _menuOffset;
    [SerializeField] private SellTowerButton _sellButton;
    private RectTransform menuTransform;
    private BuildSpot _currentSpot;

    private void Start()
    {
        menuTransform = _menuUI.GetComponent<RectTransform>();
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
        if (spot != null && spot.IsOccupied)
        {
            _currentSpot = spot;
            _sellButton.SetSellGold(_currentSpot.CurrentTower.SellPrice);
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

        menuTransform.position = spotPosition + _menuOffset;
        _menuUI.SetActive(true);

    }

    private void Hide()
    {
        _currentSpot = null;
        _menuUI.SetActive(false);

    }

    public void SellTower()
    {
        if (_currentSpot != null && _currentSpot.CurrentTower != null)
        {
            _resources.AddGold(_currentSpot.CurrentTower.SellPrice);
            _currentSpot.RemoveTower();
            Hide();
            _selector.ResetSelector();
        }
    }
}