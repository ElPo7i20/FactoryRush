using UnityEngine;
[CreateAssetMenu(fileName = "NewOrder", menuName = "Factory Rush/Order")]
public class OrderData : ScriptableObject
{
    [SerializeField] private string orderName;
    [Min(1)]
    [SerializeField] private int quanity = 5;
    [Min(1)]
    [SerializeField] private int reward = 100;
    [Min(0.1f)]
    [SerializeField] private float productionDuration = 5f;

    public string OrderName => orderName;
    public int Quanity => quanity;
    public int Reward => reward;
    public float ProductionDuration => productionDuration;
}
