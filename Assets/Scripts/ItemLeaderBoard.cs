using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLeaderBoard : MonoBehaviour
{
    public Text txNum;
    public Text txName;
    public Text txScore;
    public Image imgBg;
    public List<Color> listColorText;
    public List<Color> listColorBg;


    public void Init(int num,string name,int time)
    {
        txNum.text = num.ToString();
        txName.text = name;
        txScore.text = time.ToString();

        if (num < 8)
        {
            txNum.color = listColorText[num - 1];
            txName.color = listColorText[num - 1];
            txScore.color = listColorText[num - 1];
            imgBg.color = listColorBg[num - 1];
        }
        else
        {
            txNum.color = listColorText[7];
            txName.color = listColorText[7];
            txScore.color = listColorText[7];
            imgBg.color = listColorBg[7];
        }

    }
}
