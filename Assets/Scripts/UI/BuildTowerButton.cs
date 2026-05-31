using UnityEngine;
using UnityEngine.UI;

public class BuildTowerButton : MonoBehaviour
{
    [SerializeField] private PlayerResources _resources;
    [SerializeField] private Tower _tower;

    private Button _button;
    private int _towerCost;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _towerCost = _tower.Cost;

        _resources.GoldAmountChanged += OnGoldAmountChange;
    }

    private void OnDestroy()
    {
        _resources.GoldAmountChanged -= OnGoldAmountChange;
    }

    private void OnGoldAmountChange(int currentAmount)
    {
        
        if (_towerCost <= currentAmount)
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }
}
