using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Stats
    public float maxHp;
    float curHp;
    public float maxStam;
    float curStam;

    //UI
    public Image hpImg;
    public Image stamImg;
    public float uiLerpSpd;

    //Movement
    public float spd;
    public float runSpd;
    float curSpd;
    Rigidbody2D bod;
    Vector2 move;

    private void Awake()
    {
        bod = GetComponent<Rigidbody2D>();
        ResetHp();
        curStam = maxStam;
    }

    private void Update()
    {
        //Movement
        //Get Inputs
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Move if any move buttons are pressed
        if (move.x != 0) bod.AddForce(Vector2.right * curSpd * move.x * Time.deltaTime);
        if (move.y != 0) bod.AddForce(Vector2.up * curSpd * move.y * Time.deltaTime);

        //Sprinting
        if (Input.GetButton("Fire3") && curStam > 0)
        {
            curStam -= Time.deltaTime;
            curSpd = runSpd;
        }
        else
        {
            curStam += Time.deltaTime;
            curSpd = spd;
        }
        curStam = Mathf.Clamp(curStam, 0, maxStam);

        //UI
        hpImg.fillAmount = Mathf.Lerp(hpImg.fillAmount, (curHp / maxHp), uiLerpSpd * Time.deltaTime);
        stamImg.fillAmount = Mathf.Lerp(stamImg.fillAmount, (curStam / maxStam), uiLerpSpd * Time.deltaTime);

        //Debugging
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                TakeDamage(10);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                ResetHp();
            }
        }
    }

    void ResetHp()
    {
        hpImg.color = Color.yellow;
        Invoke("hpToRed", 0.5f);
        curHp = maxHp;
    }

    void hpToRed()
    {
        hpImg.color = Color.red;
    }

    public void TakeDamage(float amt)
    {
        hpImg.color = Color.white;
        Invoke("hpToRed", 0.05f);
        curHp -= amt;
    }
}
