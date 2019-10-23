using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float atkLow;
    public float atkHigh;
    float atk;
    public float spd;
    Rigidbody2D bod;
    //GameController cont;
    Vector3 startSize;
    PlayerController player;
    WeaponController weapon;
    float dmg = 0;

    private void Awake()
    {
        weapon = FindObjectOfType<WeaponController>();
        //cont = FindObjectOfType<GameController>();
        bod = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        atk = Random.Range(atkLow, atkHigh);
        Invoke("Disable", 2f);
        bod.AddForce(transform.up * spd);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //collision.GetComponent<EnemyController>().TakeDamage(dmg);
            Invoke("Disable", 0.001f);
        }
    }
}
