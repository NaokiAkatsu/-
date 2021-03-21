using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public InputField field_x, field_y, field_bomb;
    public GameObject errorMessagePanel;
    public GameObject errorMessageText;

    void Start()
    {
        field_x = field_x.GetComponent<InputField>();
        field_y = field_y.GetComponent<InputField>();
        field_bomb = field_bomb.GetComponent<InputField>();

        field_x.text = "13";
        field_y.text = "6";
        field_bomb.text = "15";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            appClose();
        }
    }

    public void gameStart()
    {
        DataBase.Max_x = int.Parse(field_x.text);
        DataBase.Max_y = int.Parse(field_y.text);
        DataBase.BombNum = int.Parse(field_bomb.text);

        
        if (DataBase.Max_x <= 0 || DataBase.Limit_x < DataBase.Max_x ||
            DataBase.Max_y <= 0 || DataBase.Limit_y < DataBase.Max_y)
        {
            errorMassege("縦横の幅は15×30以下にしてください");
        }
        else if (DataBase.BombNum < 3)
        {
            errorMassege("爆弾の数は３個以上にしてください");
        }
        else if (DataBase.BombNum > (DataBase.Max_x * DataBase.Max_y - 5))
        {
            errorMassege("爆弾が多すぎます。爆弾を減らすか、縦横の幅を増やしてください");
            this.field_bomb.text = (DataBase.Max_x * DataBase.Max_y - 5).ToString();
        }
        else
        {
            SceneManager.LoadScene("mine_sweeper");
        }
    }


    public void errorMassege(string message)
    {
        this.errorMessageText.GetComponent<Text>().text = message.ToString();
        this.errorMessagePanel.SetActive(true);

    }

    public void closeErrorMessage()
    {
        this.errorMessagePanel.SetActive(false);
    }
    
    public void ModeEasy()
    {
        this.field_x.text = "5";
        this.field_y.text = "5";
        this.field_bomb.text = "4";//16%
    }

    public void ModeNormal()
    {
        this.field_x.text = "13";
        this.field_y.text = "6";
        this.field_bomb.text = "15";//19.2%


    }

    public void ModeHard()
    {
        this.field_x.text = "15";
        this.field_y.text = "8";
        this.field_bomb.text = "28";//23.3%

    }

    public void ModeExtra()
    {
        this.field_x.text = "20";
        this.field_y.text = "10";
        this.field_bomb.text = "50";//25%

    }


    public void appClose()
    {
        
            Application.Quit();
        
    }

}




