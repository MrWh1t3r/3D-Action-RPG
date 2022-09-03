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
            //attack
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
    }
}
