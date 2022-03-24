using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_controler : MonoBehaviour
{
    public Text pressSpace;
    public Text textScore;

    public Player player;

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score: " + player.score.ToString();
        if(player.gameOver){
            pressSpace.enabled = true;
        }
        else {
            pressSpace.enabled = false;
        }
    }
}
