using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //Possible and current weapon variables
    public enum weapons { pistol, shotgun, machinegun, rpg }
    public weapons curWeapon;

    //Animation
    SpriteRenderer rend;
    public Rigidbody2D playerBod;
    public float force;

    //Bullets
    public GameObject[] spawns;
    public GameObject bullet;
    public int bulAmt;
    public List<GameObject> bulList;

    //Rotation
    public float lerpSpd;


    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();

        bulList = new List<GameObject>();
        for (int i = 0; i < bulAmt; i++)
        {
            GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
            obj.SetActive(false);
            bulList.Add(obj);
        }
    }

    GameObject GetBullet()
    {
        //Return hit obj
        for (int i = 0; i < bulList.Count; i++)
        {
            if (!bulList[i].activeInHierarchy)
            {
                return bulList[i];
            }
        }

        GameObject obj = Instantiate(bullet, transform.position, Quaternion.identity);
        bulList.Add(obj);
        obj.SetActive(false);

        return obj;
    }

    private void Update()
    {
        //Sprite position
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        rend.sortingOrder = (mousePos.y < 0.5f) ? 3 : 1;
        rend.flipY = (mousePos.x < 0.5f) ? true : false;

        //Look at mouse
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * lerpSpd);

        //Firing
        if (Input.GetButtonDown("Fire1"))
        {
            FirePistol();
        }

        if (Input.GetButton("Fire1"))
        {
            //InvokeRepeating("FirePistol", 0.01f, 0.25f);
        }
    }

    void FirePistol()
    {
        float ran = Random.Range(-5f, 5f);
        GameObject obj = GetBullet();
        obj.transform.position = spawns[0].transform.position;
        playerBod.AddForceAtPosition(-transform.right * force, transform.position);
        obj.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90 + ran);
        obj.SetActive(true);
    }
}
