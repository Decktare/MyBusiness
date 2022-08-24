public class Improvement
{
    private bool isBought = false;
    private string improvementName;
    private int price;
    private double multiplier;

    public void Buying()
    {
        isBought = true;
    }

    public void SetName(string name)
    {
        improvementName = name;
    }

    public void SetPrice(int price)
    {
        this.price = price;
    }

    public void SetMultiplier(double multiplier)
    {
        this.multiplier = multiplier;
    }

    public string GetName()
    {
        return improvementName;
    }

    public bool IsBought()
    {
        return isBought;
    }

    public int GetPrice()
    {
        return price;
    }

    public double GetMultiplier()
    {
        return multiplier;
    }
}
