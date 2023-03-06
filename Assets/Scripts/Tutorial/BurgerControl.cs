using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerControl : MonoBehaviour
{
    [SerializeField] private PlayerService _playerService;
    private bool ishaveFood;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ishaveFood)
        {
            return;
        }

        if (_playerService.IsHaveFood())
        {
            ishaveFood = true;
            transform.parent.GetComponent<SaveManager>().AreaOpenNextState();
        }
    }
}
