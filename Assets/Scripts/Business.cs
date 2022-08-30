using UnityEngine;
using UnityEngine.UI;

public class Business : MonoBehaviour
{
    // UI
    [SerializeField]
    private Text businessNameText;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text revenueText;
    [SerializeField]
    private Text levelUpPriceText;

    [SerializeField]
    private Text firstImprovementNameText;
    [SerializeField]
    private Text firstImprovementMultiplierText;
    [SerializeField]
    private Text firstImprovementPriceText;

    [SerializeField]
    private Text secondImprovementNameText;
    [SerializeField]
    private Text secondImprovementMultiplierText;
    [SerializeField]
    private Text secondImprovementPriceText;

    [SerializeField]
    private Slider progressBar;

    // Performance
    [SerializeField]
    private int businessID;
    [SerializeField]
    private int level;
    [SerializeField]
    private BusinessData businessData;
    [SerializeField]
    private BusinessNames businessNames;

    private string businessName;
    private int price;
    private float revenue;    
    private int incomeDelay;

    private bool isBusinessWorking;

    
    private float timerStart;
    [HideInInspector]
    public float timer;
    [HideInInspector]
    public float timerEnd;

    private double multiplierPercent;

    [HideInInspector]
    public Improvement firstImprovement;
    [HideInInspector]
    public Improvement secondImprovement;

    public void Checking()
    {
        if(level > 0)
        {
            isBusinessWorking = true;
        }
        if(firstImprovement.IsBought())
        {
            firstImprovementPriceText.text = $"Куплено";
            multiplierPercent += firstImprovement.GetMultiplier();
            Revenue();
        }
        if (secondImprovement.IsBought())
        {
            secondImprovementPriceText.text = $"Куплено";
            multiplierPercent += secondImprovement.GetMultiplier();
            Revenue();
        }
    }

    public float GetRevenue()
    {
        return revenue;
    }

    public int GetLevel()
    {
        return level;
    }
    public void SetLevel(int level)
    {
        this.level = level;
        levelText.text = $"Level\n{level}";
        price = (level + 1) * businessData.Price;
        levelUpPriceText.text = $"Level up\nPrice: {price}$";
        Revenue();
    }
    public void BusinessLevelUp()
    {        
        if(Money.Lose(price))
        {
            if (level == 0)
            {
                isBusinessWorking = true;
                TimerStart();
            }
            level++;
            levelText.text = $"Level\n{level}";
            price = (level + 1) * businessData.Price;
            levelUpPriceText.text = $"Level up\nPrice: {price}$";
            Revenue();
        }
    }

    public void BuyImprovement(int index)
    {
        if (index == 1)
        {
            if(!firstImprovement.IsBought())
            {
                if(Money.Lose(firstImprovement.GetPrice()))
                {
                    firstImprovement.Buying();
                    firstImprovementPriceText.text = $"Куплено";
                    multiplierPercent += firstImprovement.GetMultiplier();
                    Revenue();
                }
            }
        }
        if (index == 2)
        {
            if (!secondImprovement.IsBought())
            {
                if(Money.Lose(secondImprovement.GetPrice()))
                {
                    secondImprovement.Buying();
                    secondImprovementPriceText.text = $"Куплено";
                    multiplierPercent += secondImprovement.GetMultiplier();
                    Revenue();
                }
            }
        }
    }

    private void Awake()
    {
        Initialization();
    }

    private void Update()
    {
        Timer();
    }

    private void Initialization()
    {
        businessName = businessNames.BusinessName(businessID);
        price = businessData.Price;
        revenue = businessData.Revenue;
        incomeDelay = businessData.IncomeDelay;

        isBusinessWorking = level > 0;

        firstImprovement = new Improvement();
        firstImprovement.SetName(businessNames.FirstImprovementName(businessID));
        firstImprovement.SetPrice(businessData.FirstImprovementPrice);
        firstImprovement.SetMultiplier(businessData.FirstImprovementMultiplier);

        secondImprovement = new Improvement();
        secondImprovement.SetName(businessNames.SecondImprovementName(businessID));
        secondImprovement.SetPrice(businessData.SecondImprovementPrice);
        secondImprovement.SetMultiplier(businessData.SecondImprovementMultiplier);
        UIInitialization();
        TimerStart();
    }

    private void UIInitialization()
    {
        businessNameText.text = $"{businessName}";
        levelText.text = $"Level\n{level}";
        Revenue();
        levelUpPriceText.text = $"Level up\nPrice: {price}$";

        firstImprovementNameText.text = $"{firstImprovement.GetName()}";
        firstImprovementMultiplierText.text = $"Multiplier: + {firstImprovement.GetMultiplier()}%";
        firstImprovementPriceText.text = $"Price: {firstImprovement.GetPrice()}$";

        secondImprovementNameText.text = $"{secondImprovement.GetName()}";
        secondImprovementMultiplierText.text = $"Multiplier: + {secondImprovement.GetMultiplier()}%";
        secondImprovementPriceText.text = $"Price: {secondImprovement.GetPrice()}$";
    }

    private void TimerStart()
    {
        timerStart = 0;
        timerEnd = incomeDelay;
        timer = timerStart;
        progressBar.maxValue = timerEnd;
        progressBar.value = timer;
    }

    private void Timer()
    {
        if (isBusinessWorking)
        {
            if(timer <= timerEnd)
            {
                timer += Time.deltaTime;
                progressBar.value = timer;
            }
            else
            {
                TimerStart();
                Profit();
            }
        }
    }

    private void Profit()
    {
        Revenue();
        Money.Income(revenue);
    }

    private void Revenue()
    {
        revenue = (((float)multiplierPercent / 100) + 1) * (level * businessData.Revenue);
        revenueText.text = $"Revenue\n{revenue}$";
    }
}
