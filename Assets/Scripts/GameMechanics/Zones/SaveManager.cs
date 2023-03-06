using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]private int defaultState;
    public int MachineState => PlayerPrefs.GetInt(name, defaultState);
    public SaveManager[] nextAreas;

    private void Awake()
    {
        transform.GetChild(MachineState).gameObject.SetActive(true);
    }


    public void AreaOpenNextState()
    {
        if (MachineState == transform.childCount - 1) return;
        transform.GetChild(MachineState).gameObject.SetActive(false);
        PlayerPrefs.SetInt(name, MachineState + 1);
        transform.GetChild(MachineState).gameObject.SetActive(true);
    }
}