using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Player player;
    public float attackDistance;
    public int damage;
    public int health;

    private bool _isAttacking;
    private bool _isDead;

    public NavMeshAgent agent;

    private void Update()
    {
        if(_isDead)
            return;

        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            agent.isStopped = true;
            if(!_isAttacking)
                Attack();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
    }

    void Attack()
    {
        _isAttacking = true;
        
        Invoke("TryDamage", 1.3f);
        Invoke("DisableIsAttacking", 2.66f);
    }

    void TryDamage()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            player.TakeDamage(damage);
        }
    }

    void DisableIsAttacking()
    {
        _isAttacking = false;
    }
}
