using UnityEngine;
using UnityEngine.UI;

public class Business : MonoBehaviour
{
    // UI
    public Text businessNameText;
    public Text levelText;
    public Text revenueText;
    public Text levelUpPriceText;

    public Text firstImprovementNameText;
    public Text firstImprovementMultiplierText;
    public Text firstImprovementPriceText;

    public Text secondImprovementNameText;
    public Text secondImprovementMultiplierText;
    public Text secondImprovementPriceText;

    [SerializeField]
    private int businessID;
    [SerializeField]
    private BusinessData businessData;
    [SerializeField]
    private BusinessNames businessNames;

    private string businessName;
    private int level;
    private int price;
    private int revenue;
    private int incomeDelay;

    private Improvement firstImprovement;

    private Improvement secondImprovement;

    private void Awake()
    {
        Initialization();
    }

    private void Initialization()
    {
        businessName = businessNames.BusinessName(businessID);
        price = businessData.Price;
        revenue = businessData.Revenue;
        incomeDelay = businessData.IncomeDelay;

        firstImprovement = new Improvement();
        firstImprovement.SetName(businessNames.FirstImprovementName(businessID));
        firstImprovement.SetPrice(businessData.FirstImprovementPrice);
        firstImprovement.SetMultiplier(businessData.FirstImprovementMultiplier);

        secondImprovement = new Improvement();
        secondImprovement.SetName(businessNames.SecondImprovementName(businessID));
        secondImprovement.SetPrice(businessData.SecondImprovementPrice);
        secondImprovement.SetMultiplier(businessData.SecondImprovementMultiplier);
        UIInitialization();
    }

    private void UIInitialization()
    {
        businessNameText.text = businessName;
        levelText.text = level.ToString();
        revenueText.text = revenue.ToString();
        levelUpPriceText.text = price.ToString();

        firstImprovementNameText.text = firstImprovement.GetName();
        firstImprovementMultiplierText.text = firstImprovement.GetMultiplier().ToString();
        firstImprovementPriceText.text = firstImprovement.GetPrice().ToString();

        secondImprovementNameText.text = secondImprovement.GetName();
        secondImprovementMultiplierText.text = secondImprovement.GetMultiplier().ToString();
        secondImprovementPriceText.text = secondImprovement.GetPrice().ToString();
    }
}
