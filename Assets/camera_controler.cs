using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controler : MonoBehaviour
{

    GameObject player;
    public int scrollMode;//1:ノーマル　2:戻らない　3:強制スクロール
    public float scrollSpeed = 1.0f;
    public Vector3 cameraPosMin;//カメラの初期位置
    private float goX, goY;


    // Start is called before the first frame update
    void Start()
    {
        cameraPosMin = transform.position;
        this.player = GameObject.Find("player");
        scrollMode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();   
    }

    private void MoveCamera()
    {
        Vector3 playerPos = this.player.transform.position;
        Vector3 cameraPos = transform.position;
        goX = playerPos.x;
        goY = playerPos.y;
        

        switch (scrollMode)
        {
            case 1:
                if (goY < cameraPosMin.y)
                    goY = cameraPosMin.y;
                break;

            case 2:
                if (goX < cameraPos.x)
                    goX = cameraPos.x;
                if (goY < cameraPosMin.y)
                    goY = cameraPosMin.y;
                break;

            case 3:
                goX = cameraPos.x + scrollSpeed;
                if (goY< cameraPosMin.y)
                    goY = cameraPosMin.y;
                break;
        }
        //Debug.Log(goX + ":" + goY);
        transform.position=new Vector3(goX, goY ,transform.position.z);
    }

    public void resetCamera()
    {
        transform.position = cameraPosMin;
    }
}
