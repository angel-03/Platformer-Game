
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    public float Range;
    public Transform Target;
    bool Detected = false;

    Vector2 Direction;
    public GameObject Gun;
    public GameObject bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform Shootpoint;
    public float Force;
    public float speed;
    bool isRight;
    bool check = true;
    Vector3 dir;


    Rigidbody2D rb;

    void start()
    {
        rb.GetComponent<Rigidbody2D>();
    }    
    void Update()
    {
        if(check)
        {
            StartCoroutine(Movement());
        }
        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position,Direction,Range);
        if (rayInfo)
        {
            if(rayInfo.collider.gameObject.tag == "Player")
            {
                if (Detected == false)
                {
                    Detected = true;
                }
            }
            else
            {
                if (Detected == true)
                {
                    Detected = false;
                }
            }
        }
        if (Detected)
        {
            //Gun.transform.up = Direction;
            if(Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                shoot();
            }
        }
        if(isRight)
        {
            transform.localScale = new Vector3(-10,5,1);
        }
        else if(!isRight)
        {
            transform.localScale = new Vector3(10,5,1);
        }
        Gun.transform.Translate(dir * speed * Time.deltaTime);
        Shootpoint.Translate(dir * speed * Time.deltaTime);
    }
    void shoot()
    {
        GameObject BulletIns = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }

    IEnumerator Movement()
    {
        check = false;
        yield return new WaitForSeconds(2);
        isRight = false;
        dir = Vector3.left;
        yield return new WaitForSeconds(2);
        isRight = true;
        dir = Vector3.right;
        yield return new WaitForSeconds(0.5f);
        check = true;
    }
}
