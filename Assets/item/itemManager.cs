using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    GameObject player;
    


    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useItemL(int mode, GameObject L)
    {
        switch(mode)
        {
            case 1:
                player.GetComponent<player_controler>().speed *= 2;
                break;

            case -1:
                player.GetComponent<player_controler>().speed /= 2;
                break;

            default:
                Debug.Log("itemL mode has error.");
                break;

        }
        Destroy(L);
    }

    
}
