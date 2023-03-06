using System.Collections;
using UnityEngine;

public class Chef : MonoBehaviour
{
    private Animator _animator;
    private Coroutine _cookCoroutine;
    [SerializeField] private Machine _machine;
    [SerializeField] private StaffData _staffData;

    private void Start()
    {
        _animator = GetComponent<Animator>();
       InvokeRepeating(nameof(ControlCapacity),0.1f,0.4f);
    }
    private void ControlCapacity()
    {
        if (_cookCoroutine==null && !_machine._stackZone.IsCapacityFull(_staffData.ChefCapacity))
        {
            StartCook();
            CancelInvoke(nameof(ControlCapacity));
        }
    }

    private void StartCook()
    {
        _cookCoroutine = StartCoroutine(MakeFood());
    }

    private void StopCook()
    {
        InvokeRepeating(nameof(ControlCapacity),0.1f,0.4f);
        StopCoroutine(_cookCoroutine);
        _cookCoroutine = null;
        ChangeAnim(ChefAnim.idle);
    }

    private IEnumerator MakeFood()
    {
        ChangeAnim(ChefAnim.cook);
        while (!_machine._stackZone.IsCapacityFull(_staffData.ChefCapacity))
        {
            yield return new WaitForSeconds(8f-(_staffData.ChefSpeed*0.5f));
            _machine._stackZone.AddNewFood(_machine.SpawnFood());
        }
        StopCook();
        yield return new WaitForSeconds(0.1f);
    }
    public void ChangeAnim(ChefAnim anim)
    {
        _animator.SetInteger("Anim", (int) anim);
    }
}


public enum ChefAnim
{
    idle=1,
    cook=2
}