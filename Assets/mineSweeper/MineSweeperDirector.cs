using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MineSweeperDirector : MonoBehaviour
{

    public int MAX_x, MAX_y;
    public float time;
    public int bombNum;
    public int remainTile;
    public int flagNum;
    public bool DoesGameRun;
    public bool IsFirstClick;

    [SerializeField] public GameObject MessagePanel;
    [SerializeField] public GameObject MessagePanelText;

    [SerializeField] public GameObject flagCount;
    [SerializeField] public GameObject bombCount;
    [SerializeField] public GameObject tileCount;

    private Text FlagCText;
    private Text BombCText;
    private Text TileCText;
   

    [SerializeField]public GameObject tilePrefab;
    [SerializeField]public GameObject tileSummary;
    private GameObject[,] tiles;

    public Sprite bombImage;
    public Sprite tileImage;
    public GameObject MessagePImageL;
    public GameObject MessagePImageR;

    [SerializeField]private AudioSource gameSetSE;
    [SerializeField] private AudioClip gameClearSE;
    [SerializeField] private AudioClip gameOverSE;
    [SerializeField] private AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        init();
        
        setSummary();
        putTiles();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (this.DoesGameRun == true)
        {
            this.time += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void init()
    {
        this.time = 0;
        this.DoesGameRun = true;
        this.IsFirstClick = true;

        this.MAX_x = DataBase.Max_x;
        this.MAX_y = DataBase.Max_y;


        this.tiles = new GameObject[DataBase.Limit_y, DataBase.Limit_x];

        this.flagNum = DataBase.BombNum;
        this.bombNum = DataBase.BombNum;
        this.remainTile = MAX_x * MAX_y;


        this.FlagCText = this.flagCount.GetComponent<Text>();
        this.BombCText = this.bombCount.GetComponent<Text>();
        this.TileCText = this.tileCount.GetComponent<Text>();

        this.BombCText.text = "Bomb :" + this.bombNum.ToString();
        CounterUpdate();

    }

    public void GameOver(int a)
    {
        this.DoesGameRun = false;
        this.BGM.Stop();

        if (this.MessagePanel.activeSelf == false)
        {
            switch (a)
            {
                case 0:
                    this.MessagePanelText.GetComponent<Text>().text = "Game Over";
                    this.MessagePImageL.GetComponent<Image>().sprite = this.bombImage;
                    this.MessagePImageR.GetComponent<Image>().sprite = this.bombImage;
                    this.gameSetSE.PlayOneShot(this.gameOverSE);
                    break;

                case 1:
                    this.MessagePanelText.GetComponent<Text>().text = "Game Clear!!";
                    this.MessagePImageL.GetComponent<Image>().sprite = this.tileImage;
                    this.MessagePImageR.GetComponent<Image>().sprite = this.tileImage;
                    this.gameSetSE.PlayOneShot(this.gameClearSE);
                    break;

            }
        }

        this.MessagePanel.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene("mine_sweeper");
    }

    public void returnManu()
    {
        SceneManager.LoadScene("MS_MainMenu");
    }

    private void setSummary()
    {
        this.tileSummary.GetComponent<GridLayoutGroup>().constraintCount= this.MAX_x;
    }


    public void putTiles()
    {
        int x, y;
        GameObject obj;

        for (y = 0; y < MAX_y; y++)
        {
            for (x = 0; x < MAX_x; x++)
            {
               
                obj = Instantiate(tilePrefab, this.tileSummary.transform);
                obj.transform.localScale = new Vector3(0.3f, 0.3f, 0);
                obj.transform.SetParent(this.tileSummary.transform, false);
                obj.GetComponent<minePanel>().coordinate.x = x;
                obj.GetComponent<minePanel>().coordinate.y = y;

                tiles[y, x]=obj;
            }
        }

        Vector2 size = this.tileSummary.transform.localScale;
        if (this.MAX_x >= 14 || this.MAX_y >= 7)
        {
            size = new Vector2(1.5f, 1.5f);
        }
        if (this.MAX_x >=21 || this.MAX_y >=11)
        {
            size = new Vector2(1.0f, 1.0f);
        }
        this.tileSummary.transform.localScale =size;
    }

    

    public void setBomb(int firstx, int firsty)
    {
        int randx, randy, randCount;



        for (randCount = this.bombNum; randCount > 0; randCount--)
        {
            do
            {
                randx = Random.Range(0, this.MAX_x);
                randy = Random.Range(0, this.MAX_y);
            } while (CannotSetBomb(firstx,firsty,randx,randy));

            this.tiles[randy, randx].GetComponent<minePanel>().tileNumber = -1;
        }
    }

    private bool CannotSetBomb(int fx, int fy, int rx, int ry)
    {
        if(fx == rx && fy == ry)
        {
            return true;
        }
        if(this.remainTile > this.bombNum + 15) //セーフが14以上->(9+5)以上セーフ
        {
            if(fx - 1 <= rx && rx <= fx + 1 &&
                fy - 1 <= ry && ry <= fy + 1)//周囲8マスの安全保障
            {
                return true;
            }
        }
        
        if (this.tiles[ry, rx].GetComponent<minePanel>().tileNumber == -1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool IntPercent(int x)
    {
        int a;
        a = Random.Range(1, 100);

        if (a <= x) return true;
        else return false;
    }


    public void setTiles()
    {
        int x, y, num;


        for(y=0; y<this.MAX_y; y++)
        {
            for(x=0; x<this.MAX_x; x++)
            {
                if (this.tiles[y, x].GetComponent<minePanel>().tileNumber != -1)
                {
                    num = 0;
                    num += checkBomb(x - 1, y - 1);
                    num += checkBomb(x - 1, y);
                    num += checkBomb(x - 1, y + 1);

                    num += checkBomb(x, y - 1);
                    num += checkBomb(x, y + 1);

                    num += checkBomb(x + 1, y - 1);
                    num += checkBomb(x + 1, y);
                    num += checkBomb(x + 1, y + 1);

                    this.tiles[y, x].GetComponent<minePanel>().tileNumber = num;

                }

                this.tiles[y, x].GetComponent<minePanel>().setTileNumber();
            }
        }
    }

    private int checkBomb(int x, int y)
    {
        if ( 0<=x && x<this.MAX_x  && 0<=y && y<this.MAX_y)
        {
            if (this.tiles[y, x].GetComponent<minePanel>().tileNumber == -1)
            {
                return 1;
            }
        }
        return 0;
    }

    public void checkOpen(GameObject cover)
    {
        int tn = cover.GetComponent<minePanel>().tileNumber;
        int x = cover.GetComponent<minePanel>().coordinate.x;
        int y = cover.GetComponent<minePanel>().coordinate.y;
        int cf = cover.GetComponent<minePanel>().coverFlag;
        


        if (cf != 1)
        {
            if (IsFirstClick == true)
            {
                setBomb(x, y);
                setTiles();

                IsFirstClick = false;
                checkOpen(cover);
            }
            else
            {
                cover.GetComponent<minePanel>().coverdTile.SetActive(false);
                cover.GetComponent<minePanel>().coverFlag = 3;
                this.remainTile -= 1;
                CounterUpdate();

                if (tn == -1)
                {
                    GameOver(0);
                }
                else if (tn == 0)
                {
                    checkOpenAround(x - 1, y - 1);
                    checkOpenAround(x - 1, y);
                    checkOpenAround(x - 1, y + 1);

                    checkOpenAround(x, y - 1);
                    checkOpenAround(x, y + 1);

                    checkOpenAround(x + 1, y - 1);
                    checkOpenAround(x + 1, y);
                    checkOpenAround(x + 1, y + 1);

                }
            }
        }

        if(this.remainTile == this.bombNum)
        {
            GameOver(1);
        }
    }

    public void checkOpenAround(int x, int y)
    {
        if(0<=x && x<MAX_x && 0<=y && y<MAX_y)
        {
            if(tiles[y,x].GetComponent<minePanel>().coverFlag!=3)
            {
                tiles[y, x].GetComponent<minePanel>().openCover();
            }
        }
    }

    public void CounterUpdate()
    {
        this.FlagCText.text = "Flag :" + this.flagNum.ToString();
        this.TileCText.text = "Tile :" + this.remainTile.ToString();

    }

    public void appClose()
    {
        Application.Quit();
    }

}
