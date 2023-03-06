using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int lastLevel;
    [SerializeField] private WelterGamesSettings WelterSettings = null;

    [SerializeField] private GameObject updateReqPanel;

    public Slider loadingSlider;
    private bool isUpdateRequired;




    void Awake()
    {
    
        WelterSettings = Resources.Load<WelterGamesSettings>("WelterSettings");
    }

    public void OnUpdateRequired()
    {
        isUpdateRequired = true;
       // updateReqPanel.SetActive(true);

    }
 
    void Start()
    {
        if (!isUpdateRequired)
        {
            lastLevel = PlayerPrefs.GetInt("lastLevel", 1);

            if (SceneController.instance.firstLogin)
            {
                if (PlayerPrefs.GetInt("firstLevelCompleted") == 0 && WelterSettings.tutorialType == WelterGamesSettings.TutorialType.TutorialScene)
                {
                    StartCoroutine(AsyncSceneLoader(1, WelterSettings.loadingSecond));
                }
                else
                {
                    StartCoroutine(AsyncSceneLoader((lastLevel % WelterSettings.levelCount) == 0 ? WelterSettings.levelCount +1 : (lastLevel % WelterSettings.levelCount), WelterSettings.loadingSecond));
                }
                SceneController.instance.firstLogin = false;
            }
            else
                StartCoroutine(AsyncSceneLoader(SceneController.instance.levelIndex, WelterSettings.loadingSecond));
        }
    }

    IEnumerator AsyncSceneLoader(int BuildIndex, float seconds)
    {
        if (WelterSettings.loadingType == WelterGamesSettings.LoadingType.FakeLoading)
        {
            float currentTime = 0;

            while (currentTime < seconds)
            {
                currentTime += Time.deltaTime;
                loadingSlider.value = currentTime;
                yield return null;
            }

            AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Single);
        }
        else
        {
            AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync(BuildIndex, LoadSceneMode.Single);
            while (!asyncLoadScene.isDone)
            {
                loadingSlider.value = asyncLoadScene.progress;
                yield return null;
            }
        }

        

        
    }
}