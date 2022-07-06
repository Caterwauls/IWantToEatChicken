﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBDoubleJumpOrb : BBAbilityOrb
{
    protected override void OnAbsorb(BBPlayer player)
    {
        player.ability.doubleJumpOn = true;
    }
}
