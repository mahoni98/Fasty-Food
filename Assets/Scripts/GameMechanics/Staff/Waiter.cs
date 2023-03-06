using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour, IWaiter
{
    [SerializeField] private StaffData _staffData;
    [SerializeField] private Transform _waitPoint;
    [SerializeField] private Transform _servicePoint;
    public Transform _trash;
    [SerializeField] private List<StackZone> _stackZones;
    [SerializeField] private StackZone _stackZone= null;
    [SerializeField] private Transform _handStackPoint;
    [SerializeField] private List<Food> _foods;
    private Coroutine _capacityCoroutine;
    [SerializeField] private float _timeCounter;
    [SerializeField] private float _timeFlag;
    private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    private Collider _collider;
    private int _openStackZoneCount = 0;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    { 
   
        StartCoroutine(FindFood());
    }

    private IEnumerator FindFood()
    {
        _openStackZoneCount=0;
        _stackZones = _stackZones.OrderBy(a => System.Guid.NewGuid()).ToList();
        yield return new WaitForSeconds(0.3f);
        _stackZone = null;
        for (int i = 0; i < _stackZones.Count; i++)
        {
            if (!_stackZones[i].transform.parent.gameObject.activeSelf) continue;
            if (_openStackZoneCount<3)
            {
                _openStackZoneCount++;
            }
            if (_stackZone == null)
            {
                _stackZone = _stackZones[i];
                continue;
            }
            if (_stackZones[i]._foods.Count>=_stackZone._foods.Count)
            {
                _stackZone = _stackZones[i];
            }
        }
        if (_stackZone!=null && _stackZone._foods.Count>0)
        {
            GoTarget(_stackZone.transform.GetChild(_stackZone.transform.childCount - 1));
        }
        else
        {
            StartCoroutine(FindFood());
        }
    }

    private void GoTarget(Transform target)
    {
        // StartCoroutine(ChangeCollider());
        _navMeshAgent.SetDestination(target.position);
        SetWalkAnim();
    }

    IEnumerator ChangeCollider()
    {
        _collider.enabled = false;
        yield return new WaitForSeconds(3f);
        _collider.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            // _collider.enabled = true;
            SetIdleAnim();
        }
        if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
        {
            SetWalkAnim();
            return;
        }
        if (other.CompareTag("WaiterZone"))
        {
            if (IsCapacityFull())
            {
                GoTarget(_servicePoint);
                return;
            }
            
            if (_stackZone._foods.Count==0 && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                // StartCoroutine(FindFood());
                if (_openStackZoneCount>1)
                {
                    StartCoroutine(FindFood());
                    return;
                }
                GoTarget(_servicePoint);
                return;
            }
        }
      

        if ((other.CompareTag("ServicePoint")) && _foods.Count==0)
        {
            GoTarget(_waitPoint);
            StartCoroutine(FindFood());
            return;
        }
        
        ServiceTable serviceTable = other.GetComponent<ServiceTable>();
        if (CountTime() && serviceTable!=null && IsHaveFood() &&serviceTable.foodType==GetFood().foodType)
        {
            _timeCounter = 0;
            if (!serviceTable.IsFull())
            {
                serviceTable.TakeFood(FoodService());
            }
            else
            {
                GoTarget(_trash);
            }
        }
    }

    public void TakeFood(Food food)
     {
         food.transform.SetParent(_handStackPoint);
         food.transform.localEulerAngles = Vector3.zero;
         DOTween.Kill(food.transform);
         food.transform
             .DOLocalJump(Vector3.up*(_handStackPoint.childCount-1)*0.3f, 1, 1, 0.5f);
         _foods.Add(food);
         if (_capacityCoroutine!=null) StopCoroutine(_capacityCoroutine);
     }

     public Food FoodService()
     {
         Food food = _foods[_foods.Count - 1];
         _foods.Remove(food);
         food.transform.SetParent(null);
         if (_capacityCoroutine!=null) StopCoroutine(_capacityCoroutine);
         return food;
     }

     public float WaiterCollectSpeed()
     {
         return 4f+(_staffData.WaiterCollectSpeed*0.3f);
     }


     public bool IsWaiter(bool trash)
     {
         if (trash)
         {
         GoTarget(_servicePoint);
             
         }
         return true;
     }

     public bool IsHaveFood()
     {
         if (_foods.Count > 0) return true;
         return false;
     }

     public List<Food> GetFoods()
     {
         return _foods;
     }

     public bool IsCapacityFull()
     {
         if (_foods.Count >= 3 + _staffData.WaiterCapacity) return true;
         return false;
     }

     public Food GetFood()
     {
         return _foods[_foods.Count - 1];
     }
     
     private bool CountTime()
     {
         _timeCounter += Time.deltaTime;
         if (_timeCounter>=_timeFlag)
         {
             return true;
         }
        
         return false;
     }
     
     public void ChangeAnim(PlayerAnim playerAnim)
     {
         _animator.SetInteger("Anim", (int)playerAnim);
     }
     
     void SetWalkAnim()
     { 
         _animator.SetFloat("WalkingSpeed",1* (_staffData.WaiterCollectSpeed+4)*0.25f);
         _navMeshAgent.speed = _staffData.WaiterCollectSpeed+3;
         if (IsHaveFood())
         {
             ChangeAnim(PlayerAnim.boxWalking); return;
         }
         ChangeAnim(PlayerAnim.walking);

     }
     
     void SetIdleAnim()
     {
         if (IsHaveFood())
         {
             ChangeAnim(PlayerAnim.boxIdle);
             return;
         }
         ChangeAnim(PlayerAnim.idle);
     }
     
    
}
