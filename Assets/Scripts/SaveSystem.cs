using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class SaveSystem : MonoBehaviour
{
    private const string BalanceKey = "Save_Balance";
    private const string UpdgradeLevelKey = "Save_UpdgradeLevel";
    private const string OrderIndexKey = "Save_OrderIndex";
    private const string HasSaveKey = "Save_Exists";

    [SerializeField] private EconomySystem economy;
    [SerializeField] private MachineUpgradeSystem upgradeSystem;
    [SerializeField] private OrderManager orderManager;

    private bool _isLoading;

    private void Start()
    {
        LoadGame();
    }
    public void SaveGame()
    {
        if (_isLoading) return;

        PlayerPrefs.SetInt(BalanceKey, economy.Balance);
        PlayerPrefs.SetInt(UpdgradeLevelKey, upgradeSystem.Level);
        PlayerPrefs.SetInt(OrderIndexKey, orderManager.CurrentOrderIndex);
        PlayerPrefs.SetInt(HasSaveKey, 1);

        PlayerPrefs.Save();
    }
    public void LoadGame()
    {
        if(!PlayerPrefs.HasKey(HasSaveKey))
        {
            SaveGame();
            return;
        }
        _isLoading = true;
        economy.SetBalanceForLoad(PlayerPrefs.GetInt(BalanceKey, 250));
        upgradeSystem.SetLevelForLoad(PlayerPrefs.GetInt(UpdgradeLevelKey, 1));
        orderManager.SetOrderIndexForLoad(PlayerPrefs.GetInt(OrderIndexKey, 0));
        _isLoading = false;
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(BalanceKey);
        PlayerPrefs.DeleteKey(UpdgradeLevelKey);
        PlayerPrefs.DeleteKey(OrderIndexKey);
        PlayerPrefs.DeleteKey(HasSaveKey);
        PlayerPrefs.Save();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}