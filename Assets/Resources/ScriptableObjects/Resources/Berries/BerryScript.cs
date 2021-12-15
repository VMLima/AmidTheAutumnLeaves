using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryScript : EffectScript
{
    private ResourceSO berries;
    private SkillSO foraging;

    public override void onStart(int oldNumStacks, int addingStacks)
    {
        //berries = ResourceManager.instance.Get("Berries");
        foraging = IncManager.instance.Get<SkillSO>("Foraging");
    }
    public override void onTick(int numEffects)
    {
        //slowly gain health.
        //Player.instance.GainHealth( 0.01f * (float)numEffects));

        //slowly lose berries.
        berries.addAmount(-0.01f * (float)(numEffects));

        //gain skill xp
        foraging.addAmount(0.01f * (float)(numEffects));
    }
}