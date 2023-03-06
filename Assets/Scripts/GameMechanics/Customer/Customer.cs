using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] private Machine _machine;
    [SerializeField] private MoneyArea _moneyArea;
    [SerializeField] private Transform _characters;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _chairPoint;
    [SerializeField] private Transform _tablePoint;
    private Vector3 _homePos;
    private Transform _character;

    private void Awake()
    {
        _homePos = transform.position;
        StartCoroutine(SetCharacter(0));
    }

    private IEnumerator SetCharacter(float delay)
    {
        yield return new WaitForSeconds(delay);
        _character =  _characters.GetChild(Random.Range(0, _characters.childCount));
        _character.gameObject.SetActive(true);
        _animator = _character.GetComponent<Animator>();
        GoChair();
    }
    

    private void GoChair()
    {
        transform.LookAt(new Vector3(_chairPoint.position.x,transform.position.y,_chairPoint.position.z));
        _animator.SetInteger("Anim",1);
        transform.DOMove(_chairPoint.position, 2.5f).SetSpeedBased()
            .OnComplete((() =>
            {
                    _animator.SetInteger("Anim",2);
                    transform.SetParent(_chairPoint);
                    transform.localEulerAngles=Vector3.zero;
                    StartCoroutine(WantFood());
            }));
    }

    private IEnumerator WantFood()
    {
        yield return new WaitForSeconds(0.5f);
        Transform food = _machine.SpawnFood().transform;
        yield return new WaitForSeconds(0.5f);
        food.DOJump(_tablePoint.position, 1.3f, 1, 0.5f);
        yield return new WaitForSeconds(10f);
        _moneyArea.AddMoney();
        Destroy(food.gameObject);
        ReturnHome();
    }

  

    private void ReturnHome()
    {
        transform.LookAt(_homePos);
        _animator.SetInteger("Anim",1);
        transform.DOMove(_homePos, 2.5f).SetSpeedBased()
            .OnComplete((() =>
            {
                _character.gameObject.SetActive(false);
                StartCoroutine(SetCharacter(0));
            }));
    }
  
}
