using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minePanel : MonoBehaviour
{

    [SerializeField] public GameObject coverdTile;

    /*[SerializeField] public Sprite sprite_openBomb;
    [SerializeField] public Sprite sprite_open0;
    [SerializeField] public Sprite sprite_open1;
    [SerializeField] public Sprite sprite_open2;
    [SerializeField] public Sprite sprite_open3;
    [SerializeField] public Sprite sprite_open4;
    [SerializeField] public Sprite sprite_open5;
    [SerializeField] public Sprite sprite_open6;
    [SerializeField] public Sprite sprite_open7;
    [SerializeField] public Sprite sprite_open8;
    [SerializeField] public Sprite sprite_cover;
    [SerializeField] public Sprite sprite_coverFlag;
    [SerializeField] public Sprite sprite_coverQueston;*/

    public GameObject Director;

    public int coverFlag = 0;
    public int tileNumber = 0;

    public struct Coordinate
    {
        public int x;
        public int y;
    }

    public Coordinate coordinate = new Coordinate();


    // Start is called before the first frame update
    void Start()
    {
        this.Director = GameObject.Find("MineSweeperDirector");
    }


    public void changeCover()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Sprite sprite;
            if (coverFlag == 0)
            {
                sprite = DataBase.sprite_coverFlag;
                this.Director.GetComponent<MineSweeperDirector>().flagNum--;
                this.Director.GetComponent<MineSweeperDirector>().CounterUpdate();
            }
            else if (coverFlag == 1)
            {
                sprite = DataBase.sprite_coverQueston;
                this.Director.GetComponent<MineSweeperDirector>().flagNum++;
                this.Director.GetComponent<MineSweeperDirector>().CounterUpdate();
            }
            else //coverFlag ==2
            {
                sprite = DataBase.sprite_cover;
                coverFlag = -1;
            }
            coverFlag++;
            this.coverdTile.GetComponent<Image>().sprite = sprite;
        }
    }

    public void openCover()
    {
        this.Director.GetComponent<MineSweeperDirector>().checkOpen(gameObject);
        
    }

    public void setTileNumber()
    {
        Sprite sprite= DataBase.sprite_open0;

        switch(tileNumber)
        {
            case 0:
                sprite = DataBase.sprite_open0;
                break;
            case 1:
                sprite = DataBase.sprite_open1;
                break;
            case 2:
                sprite = DataBase.sprite_open2;
                break;
            case 3:
                sprite = DataBase.sprite_open3;
                break;
            case 4:
                sprite = DataBase.sprite_open4;
                break;
            case 5:
                sprite = DataBase.sprite_open5;
                break;
            case 6:
                sprite = DataBase.sprite_open6;
                break;
            case 7:
                sprite = DataBase.sprite_open7;
                break;
            case 8:
                sprite = DataBase.sprite_open8;
                break;
            case -1:
                sprite = DataBase.sprite_openBomb;
                break;
        }

        this.GetComponent<Image>().sprite = sprite;
    }
}
