using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject HPgauge;
    [SerializeField] GameObject HPmater;
    GameObject player;
    [SerializeField] GameObject mainCamera;
    private int hp;
    private float hpFill;
    private int MaxHp_player;//playerから

    void Start()
    {
        //this.HPgauge = GameObject.Find("HPgage_G");
        //this.HPmater = GameObject.Find("HPmater");
        this.player = GameObject.Find("player");
        //this.mainCamera = GameObject.Find("Main Camera");
        this.MaxHp_player = this.player.GetComponent<player_controler>().MaxHp_player;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseHP();
    }

    void DecreaseHP()
    {
        this.hp = this.player.GetComponent<player_controler>().GetHP();//playerからHP取得
        //Debug.Log("hp=" + this.hp);
        this.hpFill = this.HPgauge.GetComponent<Image>().fillAmount;//HPgaugeGからfill具合取得
        //Debug.Log("hpfill="+this.hpFill);
        if(this.hpFill>(this.hp /(float)MaxHp_player)  && this.hp>=0)
            this.HPgauge.GetComponent<Image>().fillAmount-=0.0005f;

        if (this.hp < 0) this.hp = 0;//-を表示しない
        this.HPmater.GetComponent<Text>().text = this.hp + "/100";

    }

    private void killPlayer()
    {
        this.player.GetComponent<player_controler>().respawn();
        mainCamera.transform.Translate(this.player.transform.position);
    }



}
