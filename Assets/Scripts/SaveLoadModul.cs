using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveLoadModul : MonoBehaviour
{
    [SerializeField]
    private List<Business> businessList;

    private SaveBusiness SaveBusiness = new SaveBusiness();
    private SaveEco SaveEco = new SaveEco();
    private SaveDate SaveDate = new SaveDate();

    public static string pathBusiness;
    public static string pathEco;
    public static string pathDate;

    public void Save()
    {
        SaveBusiness.levels.Clear();
        SaveBusiness.firstImprovementBought.Clear();
        SaveBusiness.secondImprovementBought.Clear();
        SaveDate.timers.Clear();
        foreach(Business business in businessList)
        {
            SaveBusiness.levels.Add(business.GetLevel());
            SaveBusiness.firstImprovementBought.Add(business.firstImprovement.IsBought());
            SaveBusiness.secondImprovementBought.Add(business.secondImprovement.IsBought());
            SaveDate.timers.Add(business.timer);
        }
        SaveEco.money = Money.Get();
        SaveDate.date[0] = DateTime.Now.Year;
        SaveDate.date[1] = DateTime.Now.Month;
        SaveDate.date[2] = DateTime.Now.Day;
        SaveDate.date[3] = DateTime.Now.Hour;
        SaveDate.date[4] = DateTime.Now.Minute;
        SaveDate.date[5] = DateTime.Now.Second;
    }

    public void Load()
    {
        if(File.Exists(pathBusiness))
        {
            SaveBusiness = JsonUtility.FromJson<SaveBusiness>(File.ReadAllText(pathBusiness));
            foreach(Business business in businessList)
            {
                business.SetLevel(SaveBusiness.levels.ElementAt(businessList.IndexOf(business)));
                business.firstImprovement.SetIsBought(SaveBusiness.firstImprovementBought.ElementAt(businessList.IndexOf(business)));
                business.secondImprovement.SetIsBought(SaveBusiness.secondImprovementBought.ElementAt(businessList.IndexOf(business)));
                business.Checkcing();
            }
        }
        if (File.Exists(pathEco))
        {
            SaveEco = JsonUtility.FromJson<SaveEco>(File.ReadAllText(pathEco));
            Money.Set(SaveEco.money);
        }
        if (File.Exists(pathDate))
        {
            SaveDate = JsonUtility.FromJson<SaveDate>(File.ReadAllText(pathDate));

            foreach (Business business in businessList)
            {
                if(TimeSpanReturn() / business.timerEnd >= 1)
                {
                    for(int i = 0; i < Convert.ToInt32(TimeSpanReturn() / business.timerEnd); i++)
                    {
                        Money.Income(business.GetRevenue());
                    }
                }
                else 
                {
                    if (business.timer + TimeSpanReturn() + SaveDate.timers.ElementAt(businessList.IndexOf(business)) < business.timerEnd)
                    {
                        business.timer += TimeSpanReturn() + SaveDate.timers.ElementAt(businessList.IndexOf(business));
                    }
                    else
                    {
                        business.timer = TimeSpanReturn() + SaveDate.timers.ElementAt(businessList.IndexOf(business)) - business.timerEnd;
                        Money.Income(business.GetRevenue());
                    }                    
                }
            }
        }
    }

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        pathBusiness = Path.Combine(Application.persistentDataPath, "Business.json");
        pathEco = Path.Combine(Application.persistentDataPath, "Eco.json");
        pathDate = Path.Combine(Application.persistentDataPath, "Date.json");
#else
        pathBusiness = Path.Combine(Application.dataPath, "Business.json");
        pathEco = Path.Combine(Application.dataPath, "Eco.json");
        pathDate = Path.Combine(Application.dataPath, "Date.json");
#endif
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause (bool pause)
    {
        Save();
        if(pause)
        {
            File.WriteAllText(pathBusiness, JsonUtility.ToJson(SaveBusiness));
            File.WriteAllText(pathEco, JsonUtility.ToJson(SaveEco));
            File.WriteAllText(pathDate, JsonUtility.ToJson(SaveDate));
        }
    }
#else
    private void OnApplicationQuit()
    {
        Save();
        File.WriteAllText(pathBusiness, JsonUtility.ToJson(SaveBusiness));
        File.WriteAllText(pathEco, JsonUtility.ToJson(SaveEco));
        File.WriteAllText(pathDate, JsonUtility.ToJson(SaveDate));
    }
#endif

    private void Start()
    {
        Load();
    }

    private int TimeSpanReturn()
    {
        DateTime dt = new DateTime(SaveDate.date[0], SaveDate.date[1], SaveDate.date[2], SaveDate.date[3], SaveDate.date[4], SaveDate.date[5]);
        TimeSpan ts = DateTime.Now - dt;
        return Convert.ToInt32(ts.TotalSeconds);
    }
}

[Serializable]
public class SaveBusiness
{
    public List<int> levels = new();
    public List<bool> firstImprovementBought = new();
    public List<bool> secondImprovementBought = new();
}

[Serializable]
public class SaveEco
{
    public float money;
}

[Serializable]
public class SaveDate
{
    public int[] date = new int[6];
    public List<float> timers = new();    
}
