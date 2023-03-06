using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderBox : MonoBehaviour
{
    private HorizontalLayoutGroup _horizontalLayout;
    [SerializeField] private Image _completed;
    [SerializeField] private Image _backGround;
    public bool isCompleted;
    [SerializeField] Sprite _sprite;
    public List<Image> _Images;

    private void Awake()
    {
        _backGround = GetComponent<Image>();
    }

    void Start()
    {
        _horizontalLayout = GetComponent<HorizontalLayoutGroup>();
        InvokeRepeating(nameof(FollowPosition),0,0.2f);
    }
    
    
    public void CompletedOrder(AreaType areaType)
    {
        if (!transform.parent.gameObject.activeSelf)
        {
            Destroy(gameObject);
            return;
        }
        transform.SetParent(transform.parent.parent.GetChild(transform.parent.parent.childCount-1));
        isCompleted = true;
        CancelInvoke(nameof(FollowPosition));
        _horizontalLayout.enabled = false;
        Instantiate(_completed, transform);
        if (areaType==AreaType.Cafe)
        {
            StartCoroutine(SendBox(-500));
            return;
        }
        StartCoroutine(SendBox(500));
    }

    IEnumerator SendBox(float target)
    {
        yield return new WaitForSeconds(0.4f);
        transform.DOMoveX(transform.position.x+target, 1f)
            .OnComplete((() => Destroy(gameObject)));
    }

    void FollowPosition()
    {
        if (isCompleted) return;
        transform.DOLocalMoveY(-500 + (transform.GetSiblingIndex() * 200), 0.4f);
        if (transform.GetSiblingIndex()!=0) return;
        if (_backGround.sprite==_sprite) return;
        _backGround.sprite = _sprite;

    }
    
}
