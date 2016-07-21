using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using HutongGames.PlayMaker;
using I2.MiniGames;

public class Example_Reward : MonoBehaviour 
{
	public MiniGame _Game;
    public GameObject gameManager;
	public UnityEventString _OnResult = new UnityEventString();
    public Text wheelResultDisplay;
    public Text playerpoints;
    public static int pointwheelresult = 0;
    public static bool hidekeyboard = false;
	public void CollectReward( int Index )
	{
        
		MiniGame_Reward r1 = _Game.mRewards [Index];
        GameObject res = r1.gameObject;
        string rewardvalue = res.GetComponentInChildren<Text>().text;
        //rewardvalue.Replace("$", "");
        PrizeWheel_Reward f = res.GetComponent<PrizeWheel_Reward>();
        wheelResultDisplay.text = f.displaystring;
        //string result = string.Format ("{0}({1})", Index, r1.name);
        int prizerewardvalue = res.GetComponent<PrizeWheel_Reward>().cashvalue;

        string tmp = prizerewardvalue.ToString();
       // tmp = "34";
       // prizerewardvalue = 4621;
        if (!tmp.Contains("0"))
        {
            hidekeyboard = true;
            pointwheelresult = 0;
            switch (prizerewardvalue)
            {
                case 1234:
                    wheelResultDisplay.text = "Free Hint";
                    PlayMakerFSM.BroadcastEvent("freehint");
                    break;
                case 4321:
                    wheelResultDisplay.text = "Bonus Prize";
                    PlayMakerFSM.BroadcastEvent("bonusprize");
                    break;
                case 4621:
                    wheelResultDisplay.text = "Free Vowel";
                    PlayMakerFSM.BroadcastEvent("freevowel");
                    break;

            }
        }
        else
        {
            hidekeyboard = false;
            pointwheelresult = prizerewardvalue;
            //FsmVariables.GlobalVariables.GetFsmInt("playerpoints").Value += prizerewardvalue;
            playerpoints.text = FsmVariables.GlobalVariables.GetFsmInt("playerpoints").Value.ToString()+" Points";
            FsmVariables.GlobalVariables.GetFsmString("spinresulttext").Value = f.displaystring+" Points";

       //     PlayMakerFSM.BroadcastEvent("spincomplete");

        }



        //gameManager.GetComponent<WheelWords>().currentreward = prizerewardvalue;
       // gameManager.SendMessage("processWheelResult");
		//_OnResult.Invoke (result);
	}
}
