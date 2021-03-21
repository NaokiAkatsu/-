using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{


    [SerializeField] public Sprite[] tileSprite;//インスペクターで操作する用
    
    static public Sprite sprite_open0;
    static public Sprite sprite_open1;
    static public Sprite sprite_open2;
    static public Sprite sprite_open3;
    static public Sprite sprite_open4;
    static public Sprite sprite_open5;
    static public Sprite sprite_open6;
    static public Sprite sprite_open7;
    static public Sprite sprite_open8;
    static public Sprite sprite_openBomb;
    static public Sprite sprite_cover;
    static public Sprite sprite_coverFlag;
    static public Sprite sprite_coverQueston;


    public static int Max_y, Max_x, BombNum;

    public static int Limit_x, Limit_y;





    private void Start()
    {
        sprite_open0 = this.tileSprite[0];
        sprite_open1 = this.tileSprite[1];
        sprite_open2 = this.tileSprite[2];
        sprite_open3 = this.tileSprite[3];
        sprite_open4 = this.tileSprite[4];
        sprite_open5 = this.tileSprite[5];
        sprite_open6 = this.tileSprite[6];
        sprite_open7 = this.tileSprite[7];
        sprite_open8 = this.tileSprite[8];
        sprite_openBomb = this.tileSprite[9];
        sprite_cover = this.tileSprite[10];
        sprite_coverFlag = this.tileSprite[11];
        sprite_coverQueston = this.tileSprite[12];

        Limit_x = 30;
        Limit_y = 15;

    }

}
