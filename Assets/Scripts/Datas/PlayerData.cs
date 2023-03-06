using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Datas/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int capacityMax;
    public int collectSpeedMax;
    public int MoveSpeedMax;
    public int Capacity => PlayerPrefs.GetInt("PlayerCapacity", 0
    );

    public int CollectSpeed => PlayerPrefs.GetInt("PlayerCollectSpeed", 0);

    public int MovementSpeed => PlayerPrefs.GetInt("PlayerMovementSpeed", 0);


    public void SetCapacity(int money)
    {
        OrderManager.Instance.SetMoney(-money);
        PlayerPrefs.SetInt("PlayerCapacity", Capacity + 1);
    }

    public void SetCollectSpeed(int money)
    {
        OrderManager.Instance.SetMoney(-money);
        PlayerPrefs.SetInt("PlayerCollectSpeed", CollectSpeed + 1);
    }

    public void SetMovementSpeed(int money)
    {
        OrderManager.Instance.SetMoney(-money);
        PlayerPrefs.SetInt("PlayerMovementSpeed", MovementSpeed+1);
    }
}