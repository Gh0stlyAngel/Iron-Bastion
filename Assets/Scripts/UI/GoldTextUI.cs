using TMPro;
using UnityEngine;

public class GoldTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldAmountText;
    [SerializeField] private PlayerResources _playerResources;

    private void Start()
    {
        _playerResources.GoldAmountChanged += OnGoldAmountChanged;

        OnGoldAmountChanged(_playerResources.GoldAmount);
    }

    public void OnGoldAmountChanged(int newAmount)
    {
        _goldAmountText.text = newAmount.ToString();
    }
}
