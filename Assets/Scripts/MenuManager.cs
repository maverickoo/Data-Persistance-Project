using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Security.Permissions;
using System.Data.Common;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public int bestScore;
    public string bestPlayerName;

    public string playerName;

    public TMP_InputField playerNameField;
    public TextMeshProUGUI bestScoreText;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string bestPlayerName;
        public string playerName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.bestPlayerName = bestPlayerName;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.bestScore;
            bestPlayerName = data.bestPlayerName;
            playerName = data.playerName;

            playerNameField.text = playerName;
            bestScoreText.text = "Best Score: " + bestPlayerName + ":" + bestScore;
        }
    }


    public void ReadStringInput(string input)
    {
        playerName = input;
        Debug.Log("playerName set to " + playerName);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        SaveScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void SetNewBest(int score)
    {
        bestPlayerName = playerName;
        bestScore = score;
        SaveScore();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
