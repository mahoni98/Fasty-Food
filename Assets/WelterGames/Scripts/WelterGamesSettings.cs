using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName="New Welter Games Settings")]

public class WelterGamesSettings : ScriptableObject
{

    public enum LoadingType { FakeLoading, RealLoading }
    public enum TutorialType { TutorialInLevel1, TutorialScene }
    public enum TutorialMechanic { TutorialInLevel1, TutorialScene }


    [Header("Level Settings")]
    public int levelCount = 0;

    [Header("Loading Scene Settings")]

    public LoadingType loadingType;
    public float loadingSecond=1f;

    [Header("Tutorial Settings")]
  
    public TutorialType tutorialType;
    public TutorialMechanic tutorialMechanic;


}
