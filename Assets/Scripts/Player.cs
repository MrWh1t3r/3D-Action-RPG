using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public int curHp;
    public int maxHp;
    
    public float moveSpeed;
    public Rigidbody rig;
    public float jumpForce;

    public float attackRange;
    public int damage;
    private bool _isAttacking;

    public Animator anim;

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetMouseButtonDown(0) &&!_isAttacking)
            Attack();
        
        if(!_isAttacking)
            UpdateAnimator();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * x + transform.forward * z;
        dir *= moveSpeed;
        dir.y = rig.velocity.y;

        rig.velocity = dir;
    }

    void Jump()
    {
        if (CanJump())
        {
            rig.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        }
    }

    bool CanJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.1f))
        {
            return hit.collider != null;
        }

        return false;
    }

    public void TakeDamage(int damageToTake)
    {
        curHp -= damageToTake;
        
        //update helth bar UI

        if (curHp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Attack()
    {
        _isAttacking = true;
        anim.SetTrigger("Attack");
        
        Invoke("TryDamage", 1.7f);
        Invoke("DisableIsAttacking", 1.5f);
    }

    void TryDamage()
    {
        Ray ray = new Ray(transform.position + transform.forward, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, attackRange,1, 1<<6);

        foreach (RaycastHit hit in hits)
        {
            hit.collider.GetComponent<Enemy>()?.TakeDamage(damage);
        }
       
    }

    void DisableIsAttacking()
    {
        _isAttacking = false;
        anim.ResetTrigger("Attack");
    }

    void UpdateAnimator()
    {
        anim.SetBool("MovingForward", false);
        anim.SetBool("MovingBackwards", false);
        anim.SetBool("MovingLeft", false);
        anim.SetBool("MovingRight", false);

        Vector3 localVel = transform.InverseTransformDirection(rig.velocity);
        
        
        if(localVel.z >0.1f)
            anim.SetBool("MovingForward", true);
        else if(localVel.z < -0.1f)
            anim.SetBool("MovingBackwards", true);
        else if(localVel.x >0.1f)
            anim.SetBool("MovingRight", true);
        else if(localVel.x<-0.1f)
            anim.SetBool("MovingLeft", true);
    }
}
