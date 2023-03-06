using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private GameObject startPanel;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                //new GameObject("Canvas").AddComponent<UIManager>();
            }
            return instance;
        }
    }


    [Header("In Game Panel")]
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Slider gameSlider;

    [Header("Fail Panel")]
    public GameObject failPanel;
    [SerializeField] private GameObject FailGlow;

    [Header("Victory Panel")]
    public GameObject victoryPanel;
    [SerializeField] private GameObject VictoryGlow;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject claimButton;
    [SerializeField] private GameObject adv;
    [SerializeField] private TextMeshProUGUI advText;

    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsPanel;

    [Header("Button Scale Animation Settings")]
    [SerializeField] float buttonScaleAnimationSpeed = 0.3f;
    [SerializeField] float buttonScaleAnimationLength = 0.2f;
    [SerializeField] float buttonScaleAnimationStartPosition = 0.8f;

    [Header("Glow Turn Animation Settings")]

    [SerializeField] private float glowSpeed = 50;



    private void Awake()
    {
        instance = this;
        victoryPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    void Start()
    {
        levelText.text = "Level" + PlayerPrefs.GetInt("lastLevel", 1).ToString();
        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        

    }
    public void Update()
    {
        GlowTurn();
        ScaleButton(nextButton);
    }

    public void StartLevel()
    {

    }
    public void NextLevel()
    {
        LevelManager.Instance.NextLevel();
    }
    public void RestartLevel()
    {
        LevelManager.Instance.LevelRestart();
    }

    public void OpenStore()
    {
       
    }

    public void OpenSettingsButton()
    {

    }

    void GlowTurn()
    {
        var rotationVector = VictoryGlow.transform.rotation.eulerAngles;
        rotationVector.z += Time.deltaTime *glowSpeed;
        VictoryGlow.transform.rotation = Quaternion.Euler(rotationVector);
        FailGlow.transform.rotation = Quaternion.Euler(rotationVector);

    }

    void ScaleButton(GameObject button)
    {
        float t = Time.time * buttonScaleAnimationSpeed;
        button.transform.localScale = new Vector3(Mathf.PingPong(t, buttonScaleAnimationLength) + buttonScaleAnimationStartPosition, Mathf.PingPong(t, buttonScaleAnimationLength) + buttonScaleAnimationStartPosition, 1);   
    }


}