using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour
{
    // Start is called before the first frame update

    //初期値
    private int MaxHp_player_initial = 100;
    private float speed_initial = 0.015f;
    private int MaxJumpNum_initial = 2;
    private float jumpF_initial=750.0f;
    //--------

    public int MaxHp_player;
    public int hp;
    public float speed;
    public int MaxJumpNum;
    public float jumpF;

    private int moving = 0, jumpNum = 0;
    public float muteki = 0.0f;

    Rigidbody2D PlayerRigid;

    [SerializeField] GameObject respawnPoint;
    [SerializeField]GameObject mainCamera;
    

    void Start()
    {
        init();
        this.PlayerRigid = GetComponent<Rigidbody2D>();
        //this.mainCamera = GameObject.Find("Main Camera");
        //this.respawnPoint = GameObject.Find("respawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
        checkMuteki();
        if (transform.position.y < -10)
        {
            respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jumpLiset();
            //Debug.Log("collision");
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Length")
        {
            respawn();
        } 
    }
    */
    private void init()
    {
        this.MaxHp_player = this.MaxHp_player_initial;
        this.hp = this.MaxHp_player;
        this.speed = this.speed_initial;
        this.jumpF =this.jumpF_initial;
        this.MaxJumpNum = this.MaxJumpNum_initial;
    }

    private void move()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            moving = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
            moving = -1;
        if (Input.GetKey(KeyCode.RightArrow))
            if (moving != -1) moving = 1;

        transform.Translate(speed * moving, 0, 0);
    }

    void jump()
    {
        if (this.jumpNum <MaxJumpNum && Input.GetKeyDown(KeyCode.UpArrow) && this.PlayerRigid.velocity.y <= 0)
        {
            this.PlayerRigid.AddForce(transform.up * (jumpF -20*jumpNum));
            jumpNum++;
        }
    }
    public void jumpLiset()
    {
        jumpNum =0;
    }
    void damage(int attack)
    {
        if(muteki<=0)
        this.hp -= attack;
        muteki = 1.0f;
    }
    public int GetHP()
    {
        return this.hp;
    }
    void checkMuteki()
    {
        if (muteki > 0.0f)
        {
            muteki -= Time.deltaTime;
        }
        else muteki = 0;
    }

    public void respawn()
    {
        this.hp = MaxHp_player;
        muteki = 1.0f;
        transform.position=respawnPoint.transform.position;
        mainCamera.GetComponent<camera_controler>().resetCamera();
    }
}
