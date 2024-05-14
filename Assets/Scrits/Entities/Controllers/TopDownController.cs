using System;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
   
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;

   
    public event Action<AttackSO> OnAttackEvent;

    private float timeSinceLastAttack = float.MaxValue;
    protected bool isAttacking;

    protected CharacterStatHandler stats { get; private set; }

    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatHandler>();
    }

    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack <= stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0;
          
            CallAttackEvent(stats.CurrentStat.attackSO);
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
       
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        
        OnLookEvent?.Invoke(direction);
    }

    public void CallAttackEvent(AttackSO attackSO)
    {
      
        OnAttackEvent?.Invoke(attackSO);
    }
}
