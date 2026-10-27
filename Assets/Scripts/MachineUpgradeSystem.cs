using System;
using UnityEngine;

public class MachineUpgradeSystem : MonoBehaviour
{
    [SerializeField] private EconomySystem economy;

    [Header("Upgrade Settings")]
    [SerializeField, Min(1)] private int startingLevel = 1;
    [SerializeField, Min(1)] private int maxLevel = 5;
    [SerializeField, Min(0)]
    private int[] upgradeCosts =
    {
        150, 250, 400, 600
    };

    [SerializeField, Range(0.1f, 1f)]
    private float timeMultiplierPerLevel = 0.85f;

    public int Level { get; private set; }

    public bool IsMaxLevel => Level >= maxLevel;

    public int UpgradeCost
    {
        get
        {
            if (IsMaxLevel)
                return 0;

            int costIndex = Level - startingLevel;

            if (costIndex < 0 || costIndex >= upgradeCosts.Length)
                return 0;

            return upgradeCosts[costIndex];
        }
    }

    public float ProductionSpeedMultiplier =>
        Mathf.Pow(timeMultiplierPerLevel, Level - 1);

    public event Action UpgradeChanged;

    private void Awake()
    {
        Level = Mathf.Clamp(startingLevel, 1, maxLevel);
    }

    public bool TryUpgrade()
    {
        if (IsMaxLevel || economy == null)
            return false;

        int cost = UpgradeCost;

        if (cost <= 0 || !economy.TrySpendMoney(cost))
            return false;

        Level++;

        UpgradeChanged?.Invoke();

        return true;
    }
    public void SetLevelForLoad(int level)
    {
        Level = Mathf.Clamp(level, startingLevel, maxLevel);
        UpgradeChanged?.Invoke();
    }
}
