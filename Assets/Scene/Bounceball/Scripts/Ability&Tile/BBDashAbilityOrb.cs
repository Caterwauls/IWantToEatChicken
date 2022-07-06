using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBDashAbilityOrb : BBAbilityOrb
{
    protected override void OnAbsorb(BBPlayer player)
    {
        player.ability.dashAbilityOn = true;
    }
}
