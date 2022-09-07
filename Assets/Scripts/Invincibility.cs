using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{

    protected override void PowerUp(Player player)
    {
        player.PowerUp(Color.white, Color.cyan);
    }

    protected override void PowerDown(Player player)
    {
        Color color = new Color32(69, 195, 56, 255);
        player.PowerDown(Color.black, color);
    }

}
