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

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();   
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
}
