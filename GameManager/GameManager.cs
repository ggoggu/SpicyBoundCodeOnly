using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private bool[] clearStatge = new bool[10];
    public StatData currentStat { get; set; }

    string[] StageName;
    
    public int lastSceneIndex = 0;

    private GameObject Player;

    public static GameManager Instance
    {
        get
        {
            if(instance == null) instance = new GameManager();
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPlayer()
    {
        if(Player == null)
        {
            Player = GameObject.FindWithTag("Player");
            if (Player == null)
            {
                Debug.LogError("Threr is no Plyer");
            }
        }
            
        return Player;
    }
    

    public void InitGameManager()
    {
        // StageName ����
        for(int i = 0; i < 8; i++)
        {
            clearStatge[i] = false;
        }

        

        StatData newstat = new StatData();
        newstat.maxAD = 1;
        newstat.maxHP = 3;


        currentStat = newstat;
    }

    public void SaveData()
    {
        
        GameData saveData = new GameData();
        saveData.playerStat = currentStat;
        saveData.clearStatge = clearStatge;

        string jsonData = JsonUtility.ToJson(saveData);
        string path = Path.Combine(Application.dataPath, "playerData.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "playerData.json");

        if (File.Exists(path))
        {
            GameData gameData = JsonUtility.FromJson<GameData>(path);
            clearStatge = gameData.clearStatge;
            currentStat = gameData.playerStat;
        }
        else
        {
            Debug.Log("There is No playerData File");
        }
    }

    public void GotoStage(int stageLevel)
    {
        SceneManager.LoadScene(StageName[stageLevel]);
    }

    public void ClearStage(int level)
    {
        clearStatge[level] = true;
    }

    public bool CheckStageClear(int level)
    {
        return clearStatge[level];
    }
}
