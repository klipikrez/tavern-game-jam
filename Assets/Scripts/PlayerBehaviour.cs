using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new player beh", menuName = "gameManager/player")]
public class PlayerBehaviour : BaseBehaviour
{

    float timer;
    private bool timerpause = false;
    public override void StartBehaviour(GameManager manager)
    {
        //throw new System.NotImplementedException();
        timerpause = false;
        timer = 10;
    }

    public override void UpdateBehaviour(GameManager manager)
    {
        if (manager.reset)
        {
            timer = 0;
            timerpause = true;
            manager.tajmer.text = "0";
            manager.player.transform.position = manager.portals[0].transform.position + new Vector3(0.5f, 0, 0);
            
        }
        if (!timerpause)
        {
            timer -= Time.deltaTime;
            manager.tajmer.text = timer.ToString("00.00");
        }
        if(timer < 0)
        {
            manager.Lose();
        }
    }
    public override void EndBehaviour(GameManager manager)
    {
        
        //  throw new System.NotImplementedException();
    }

}
