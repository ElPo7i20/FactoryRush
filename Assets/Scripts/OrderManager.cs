using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private OrderData[] orders;
    [SerializeField] private ProductionMachine machine;
    [SerializeField] private EconomySystem economy;
    [SerializeField] private MachineUpgradeSystem upgradeSystem;
    private int _currentOrderIndex;
    public int CurrentOrderIndex => _currentOrderIndex;

    public OrderData CurrentOrder { get; private set; }
    public bool HasOrders => CurrentOrder != null;
    public bool IsProducing => machine != null && machine.IsProducing;
    public float Progress => machine != null ? machine.Progress : 0f;

    private void Start()
    {
        _currentOrderIndex = 0;
        SetCurrentOrder();
    }

    public bool StartCurrentOrder()
    {
        if (CurrentOrder == null || machine == null)
            return false;

        float duration = CurrentOrder.ProductionDuration;

        if (upgradeSystem != null)
        {
            duration *= upgradeSystem.ProductionSpeedMultiplier;
        }

        return machine.StartProduction(
            duration,
            CompleteCurrentOrder);
    }

    private void CompleteCurrentOrder()
    {
        economy.AddMoney(CurrentOrder.Reward);
        _currentOrderIndex++;
        if(_currentOrderIndex >= orders.Length)
        {
            CurrentOrder = null;
            Debug.Log("Все заказы выполнены!");
            return;
        }
        SetCurrentOrder();
    }
    private void SetCurrentOrder()
    {
        if(orders == null || orders.Length == 0)
        {
            CurrentOrder = null;
            Debug.Log("Список заказов пуст!");
            return;
        }
        CurrentOrder = orders[_currentOrderIndex];
    }
    public void SetOrderIndexForLoad(int index)
    {
        if (orders == null || orders.Length == 0)
        {
            _currentOrderIndex = 0;
            CurrentOrder = null;
            return;
        }

        _currentOrderIndex = Mathf.Clamp(index, 0, orders.Length);

        SetCurrentOrder();
    }
}