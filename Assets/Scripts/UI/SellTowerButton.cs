using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellTowerButton : MonoBehaviour
{
    [SerializeField] private PlayerResources _resources;
    [SerializeField] private Tower _tower;
    [SerializeField] private TextMeshProUGUI _towerSellGoldText;

    private Button _button;
    private int _towerCost;

    public void SetSellGold(int sellGold)
    {
        _towerSellGoldText.text = sellGold.ToString();
    }
}
