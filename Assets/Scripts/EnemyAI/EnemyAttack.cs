using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCD = 1f;
    [SerializeField] private float attackRange = 0.25f;
    [SerializeField] private int attackDamage = 1;
    private bool canAttack = true;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject playerHealthSystem;
    public LayerMask hittableLayers;
    
    // Animation
    [SerializeField] private Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        if (canAttack)
        {
            // trigger attack animation here
            anim.SetBool("Attacking", true);

            Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hittableLayers);
            bool playerDamaged = false;
            foreach(Collider2D hit in hits)
            {
                Debug.Log(hit);
                if (hit.tag == "Player" && !playerDamaged)
                {
                    playerHealthSystem.GetComponent<HeartsVisual>().healthSystem.Damage(attackDamage);
                    playerDamaged = true;
                }
            }

            // anim.ResetTrigger();
            canAttack = false;
            StartCoroutine(DelayAttack());
        }

    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
