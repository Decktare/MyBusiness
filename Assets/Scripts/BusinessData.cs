using UnityEngine;

[CreateAssetMenu(fileName = "BusinessData", menuName = "BusinessData")]
public class BusinessData : ScriptableObject
{
    [SerializeField]
    private int price;
    [SerializeField]
    private int revenue;
    [SerializeField]
    private int incomeDelay;
    [SerializeField]
    private int firstImprovementPrice;
    [SerializeField]
    private int firstImprovementMultiplier;
    [SerializeField]
    private int secondImprovementPrice;
    [SerializeField]
    private int secondImprovementMultiplier;

    public int Price
    {
        get
        {
            return price;
        }
    }

    public int Revenue
    {
        get
        {
            return revenue;
        }
    }

    public int IncomeDelay
    {
        get
        {
            return incomeDelay;
        }
    }

    public int FirstImprovementPrice
    {
        get
        {
            return firstImprovementPrice;
        }
    }

    public int FirstImprovementMultiplier
    {
        get
        {
            return firstImprovementMultiplier;
        }
    }

    public int SecondImprovementPrice
    {
        get
        {
            return secondImprovementPrice;
        }
    }

    public int SecondImprovementMultiplier
    {
        get
        {
            return secondImprovementMultiplier;
        }
    }
}
