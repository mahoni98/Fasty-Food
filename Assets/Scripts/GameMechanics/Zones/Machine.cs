using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour, IFoodZone
{

    [SerializeField] private AreaType _areaType;
    [SerializeField] private FoodType _foodType;
    public GameObject _foodPrefab;
    [SerializeField] private Chef _chef;
    public StackZone _stackZone;
    public Transform spawnPoint;
    private Collider _collider;

    [SerializeField]
    private bool  _isSelfServis;


    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }


    private void Start()
    {
        Invoke(nameof(OpenCollider),5f);
        if (_isSelfServis) return;
        if (isHaveChef())
        {
            _chef.gameObject.SetActive(true);
        }
        else
        {
            StaffManager.Instance.AddMachine(this);
        }
        OrderManager.Instance.SetNewMaterial(_areaType,_foodPrefab);
    }
    
    
    void OpenCollider()
    {
        _collider.enabled = true;
    }

    public Food SpawnFood()
    {
        GameObject food = Instantiate(_foodPrefab, spawnPoint);
        return food.GetComponent<Food>();
    }
     

    public FoodType GetFoodType()
    {
        return _foodType;
    }

    public bool isHaveChef()
    {
        if (PlayerPrefs.GetInt(name+"chef",0)==0)
        {
            return false;
        }
        return true;
    }

    public void AddChef()
    {
        PlayerPrefs.SetInt(name + "chef", 1);
        _chef.gameObject.SetActive(true);
    }
     
     
}