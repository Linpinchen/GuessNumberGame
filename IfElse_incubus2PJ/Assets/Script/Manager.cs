using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    /*
     
     
     終極密碼

        1. 提示一個範圍(1~100)給玩家猜數字，猜中即結束。

        2. 每次猜完時，重新提示數字範圍給玩家。

        3. 計算玩家總共猜了幾次，直到猜中。
     
     
     
     
     */


    public Text ShowText;

    public Text StartText;

    public Text Winnertext;

    public Text[] GameCountText;

    public InputField ipt;

    public Button StartGame;

    private int index;

    private int indexMin;

    private int indexMax;

    private bool isRest;

    public System.Action RestAllCount;

    Player[] players;
   
    int NowPlayercount;

    // Start is called before the first frame update
    void Start()
    {

      

       
        init();

        StartGame.onClick.AddListener(delegate ()
        {

            if (!isRest)
            {
                GaameControl();
            }
            else
            {
                GameRest();
            }



        });


    }


    /// <summary>
    /// 資料初始設定
    /// </summary>
    void init()
    {

        players = new Player[]
        {
            new Player("PlayerOne", ref RestAllCount),
            new Player("PlayerTwo", ref RestAllCount)
        };

        GameRest();
    }

    /// <summary>
    /// 遊戲重置
    /// </summary>
    void GameRest()
    {
        indexMin = 1;

        indexMax = 100;

        NowPlayercount = 0;

        isRest = false;

        index = Random.Range(indexMin, indexMax + 1);

        Debug.Log($"偷看一下答案：{index}");

        RestAllCount();

        StartText.text = $"{ players[NowPlayercount].PlayerName }開始遊戲";
        ipt.text = "";


        for (int i = 0; i < players.Length; i++)
        {

            GameCountText[i].text = $"{ players[i].PlayerName }遊玩次數：{ players[i].Gamecount }";
        }

        ShowText.text = "輸入數字";
        Winnertext.text = "";

    }

    /// <summary>
    /// 遊戲控制
    /// </summary>
    void GaameControl()
    {
        int Playerinput = 0;

        if (ipt.text != "")
        {
            Playerinput = int.Parse(ipt.text);

            if (Playerinput >= indexMin && Playerinput <= indexMax)
            {

                Player Temp;

                Temp = players[NowPlayercount];

                Temp.Gamecount++;

                GameCountText[NowPlayercount].text = $"{ players[NowPlayercount].PlayerName }遊玩次數 ：{ Temp.Gamecount }";

                indexCheck(Playerinput,Temp);

                if (NowPlayercount == 0)
                {
                    NowPlayercount++;
                   
                }
                else
                {
                    if (players[1].Gamecount>1)
                    {
                        NowPlayercount--;
                    }

                }
                if (!isRest)
                {
                    StartText.text = $"{players[NowPlayercount].PlayerName}開始遊戲";
                    ipt.text = "";

                }

            }
            else
            {
                ShowText.text = "數入數字超出範圍,請重新輸入數字";

            }

        }
        else
        {
            ShowText.text = "數入錯誤,請輸入數字";
        }






    }

    /// <summary>
    /// 數值判斷
    /// </summary>
    /// <param name="Playerinput"></param>
    /// <param name="player"></param>
    void indexCheck(int Playerinput,Player player)
    {

        if (index == Playerinput)
        {
            ShowText.text = $"恭喜猜對！！ 正確的數字為:{index} ,以猜次數：{player.Gamecount}";
            Winnertext.text = $"！！！{ players[NowPlayercount].PlayerName }獲勝！！！";
            StartText.text = "重新開始";
            isRest = true;


        }
        else if (Playerinput < index)
        {

            indexMin = Playerinput;

            ShowText.text = $"輸入數字範圍：{indexMin} ~ {indexMax}";

        }
        else
        {

            indexMax = Playerinput;

            ShowText.text = $"輸入數字範圍：{indexMin} ~ {indexMax}";

        }



    }





}

/// <summary>
/// 玩家模板
/// </summary>
public class Player
{

    public string PlayerName;

    public int Gamecount;

    

    public Player(string PlayerName ,ref System.Action action)
    {


        this.PlayerName = PlayerName;

        action += RestCount;

    }

    public void RestCount()
    {

        Gamecount = 0;
        Debug.Log($"PlayerName:{PlayerName} ,GameCount:{Gamecount}");

    }

}
