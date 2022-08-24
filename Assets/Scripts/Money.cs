public static class Money
{
    private static float money = 500;

    public static void Income(float income)
    {
        money += income;
        UI.PrintMoney(money.ToString());
    }

    public static bool Lose(int outcome)
    {
        if(outcome <= money)
        {
            money -= outcome;
            UI.PrintMoney(money.ToString());
            return true;
        }
        else
        {
            return false;
        }
    }

    public static float Get()
    {
        return money;
    }

    public static void Set(float count)
    {
        money = count;
        UI.PrintMoney(money.ToString());
    }
}
