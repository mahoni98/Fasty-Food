using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int coloumn;
    [SerializeField] private int rowLength;
    void Start()
    {
        Vector3 targetScale = transform.localScale;
        transform.localScale=Vector3.zero;
        int index = transform.GetSiblingIndex();
        Vector3 target=Vector3.zero;
        target.y = index * 0.17f;
        // target.x = -(index / rowLength* 0.6f) % (coloumn*0.6f);
        // target.y = index / (rowLength*coloumn) * 0.3f;
        // target.z = -(index * 0.4f) % (rowLength*0.4f);
        transform.localPosition = target;
        transform.DOScale(targetScale, 0.5f);
    }


}
