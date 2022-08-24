using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private static Text moneyTextStatic;
    public Text moneyText;
    private void Start()
    {
        PrintMoney(Money.Get().ToString());
    }
    public static void PrintMoney(string money)
    {
        moneyTextStatic.text = $"Balance: {money}$";
    }
    private void Awake()
    {
        ToStatic();
    }

    private void ToStatic()
    {
        moneyTextStatic = moneyText;
    }    
}
