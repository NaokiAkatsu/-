using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_L_controler : MonoBehaviour
{
    public int funcMode;
    public int moveMode;
    private int moveCount;
    private float speed = 0.001f;
    [SerializeField]GameObject gameDirector;


    // Start is called before the first frame update
    void Start()
    {
        //this.gameDirector = GameObject.Find("GameDirector");
        this.funcMode = 1;
        this.moveMode = 0;
        this.moveCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        moveItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameDirector.GetComponent<itemManager>().useItemL(funcMode,gameObject);
        }
    }

    public void moveItem()
    {
        

        switch (moveMode)
        {
            case 0:
                gameObject.transform.Translate(0, this.speed, 0);
                break;

            case 1:
                gameObject.transform.Translate(this.speed,0, 0);
                break;
        }
        moveCount++;

        if (moveCount >= 500)
        {
            this.speed *= -1.0f;
            moveCount = 0;
        }
    }
}
