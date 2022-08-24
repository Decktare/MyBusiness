using UnityEngine;

[CreateAssetMenu]
public class BusinessNames : ScriptableObject
{
    [SerializeField]
    private string[] businessName = new string[5];
    [SerializeField]
    private string[] firstImprovementName = new string[5];
    [SerializeField]
    private string[] secondImprovementName = new string[5];

    public string BusinessName(int index)
    {
        return businessName[index];
    }

    public string FirstImprovementName(int index)
    {
        return firstImprovementName[index];
    }

    public string SecondImprovementName(int index)
    {
        return secondImprovementName[index];
    }
}
