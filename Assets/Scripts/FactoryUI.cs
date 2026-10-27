using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactoryUI : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private EconomySystem economy;
    [SerializeField] private OrderManager orderManager;
    [Header("UI")]
    [SerializeField] private TMP_Text balanceText, orderText, statusText;
    [SerializeField] private Slider productionSlider;
    [SerializeField] private Button startButton;
    [SerializeField] private MachineUpgradeSystem upgradeSystem;

    [Header("Upgrade UI")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TMP_Text machineLevelText;
    [SerializeField] private TMP_Text upgradeCostText;

    private void OnEnable()
    {
        if (economy != null) economy.BalanceChanged += UpdateBalance;
    }
    private void Start()
    {
        UpdateBalance(economy.Balance);

        productionSlider.minValue = 0f;
        productionSlider.maxValue = 1f;
        RefreshOrder();
        if (upgradeSystem != null)
        {
            upgradeSystem.UpgradeChanged += RefreshUpgradeUI;
        }

        RefreshUpgradeUI();
    }
    private void OnDisable()
    {
        if (economy != null) economy.BalanceChanged -= UpdateBalance;
        if (upgradeSystem != null)
        {
            upgradeSystem.UpgradeChanged -= RefreshUpgradeUI;
        }
    }
    private void Update()
    {
        if (orderManager == null) return;
        productionSlider.value = orderManager.Progress;
        startButton.interactable = orderManager.HasOrders && !orderManager.IsProducing;
        if(!orderManager.HasOrders)
        {
            statusText.text = "Все заказы выполнены";
        }
        else if (orderManager.HasOrders)
        {
            statusText.text = "Производство...";
        }
        else
        {
            statusText.text = "Станок готов";
        }
        RefreshOrder();
        RefreshUpgradeButtonState();
    }
    public void OnStartButtonClicked()
    {
        if (orderManager != null)
            orderManager.StartCurrentOrder();
    }
    private void UpdateBalance(int balance)
    {
        balanceText.text = $"Баланс: ${balance}";
    }
    private void RefreshOrder()
    {
        if (!orderManager.HasOrders)
        {
            orderText.text = "Заказов больше нет";
            return;
        }
        OrderData order = orderManager.CurrentOrder;
        orderText.text = $"Заказ: {order.OrderName}\n" + 
            $"Количество: {order.Quanity}\n" + 
            $"Награда: ${order.Reward}";
    }

    public void OnUpgradeButtonClicked()
    {
        if (upgradeSystem == null)
            return;

        upgradeSystem.TryUpgrade();

        RefreshUpgradeUI();
    }

    private void RefreshUpgradeUI()
    {
        if (upgradeSystem == null)
            return;

        machineLevelText.text =
            $"Уровень станка: {upgradeSystem.Level}";

        if (upgradeSystem.IsMaxLevel)
        {
            upgradeCostText.text = "Максимальный уровень";
            upgradeButton.interactable = false;
            return;
        }

        int cost = upgradeSystem.UpgradeCost;

        upgradeCostText.text = $"Улучшение: ${cost}";

        upgradeButton.interactable =
            economy.Balance >= cost && cost > 0;
    }
    private void RefreshUpgradeButtonState()
    {
        if (upgradeSystem == null || upgradeButton == null)
            return;

        upgradeButton.interactable =
            !upgradeSystem.IsMaxLevel &&
            upgradeSystem.UpgradeCost > 0 &&
            economy.Balance >= upgradeSystem.UpgradeCost;
    }
}