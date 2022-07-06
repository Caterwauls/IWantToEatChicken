using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBFlyAbilityOrb : BBAbilityOrb
{
    protected override void OnAbsorb(BBPlayer player)
    {
        player.ability.flyAbilityOn = true;
    }
}
