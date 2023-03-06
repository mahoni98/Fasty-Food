using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;


public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;

    [SerializeField] private WelterGamesSettings WelterSettings = null;

    private int lastLevel =0;
    private int _isFirstLevelFirstlyCompleted;
    int _tryNum;


    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                new GameObject("LevelManager").AddComponent<LevelManager>();
            }
            return instance;
        }
    }


    void Awake()
    {
        instance = this;
        WelterSettings = Resources.Load<WelterGamesSettings>("WelterSettings");
        lastLevel = PlayerPrefs.GetInt("lastLevel", 1);
        _tryNum = PlayerPrefs.GetInt("tryNum", 1);
        _isFirstLevelFirstlyCompleted = PlayerPrefs.GetInt("firstLevelCompleted", 0);
    }


    private void Start()
    {

        LevelStart();
        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,"Level_Start"+lastLevel);
    }

    public void LevelComplete()
    {
        if (_isFirstLevelFirstlyCompleted == 0)
        {
            _isFirstLevelFirstlyCompleted = 1;
            PlayerPrefs.SetInt("firstLevelCompleted", _isFirstLevelFirstlyCompleted);
        }
     
        UIManager.Instance.victoryPanel.SetActive(true);


    }

    public void LevelFailed()
    {
       
        UIManager.Instance.failPanel.SetActive(true);
        
        
    }

    public void LevelRestart()
    {
        PlayerPrefs.SetInt("tryNum", _tryNum++);

        StartCoroutine(AsyncSceneLoader(SceneManager.GetActiveScene().buildIndex));
    }
    void LevelPause()
    {
       
    }

    void LevelStart()
    {
       
       // Time.timeScale = 1;
    }

    public void NextLevel()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,"Level_Complete"+lastLevel);
        StartCoroutine(AsyncSceneLoader((lastLevel % WelterSettings.levelCount) + 1));
        lastLevel++;
        PlayerPrefs.SetInt("lastLevel", lastLevel);
    }

    IEnumerator AsyncSceneLoader(int BuildIndex)
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Single);

        while (!asyncLoadScene.isDone)
        {
            yield return null;
        }
    }
}
