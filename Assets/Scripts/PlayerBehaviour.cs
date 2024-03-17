using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new player beh", menuName = "gameManager/player")]
public class PlayerBehaviour : BaseBehaviour
{

    public override void StartBehaviour(GameManager manager)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateBehaviour(GameManager manager)
    {
        if (manager.reset)
        {
            manager.player.transform.position = manager.portals[0].transform.position + new Vector3(0.5f, 0, 0);
        }
    }
    public override void EndBehaviour(GameManager manager)
    {
        //  throw new System.NotImplementedException();
    }

}
