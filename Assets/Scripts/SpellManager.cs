﻿using UnityEngine;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour
{
    public static SpellManager Instanse { get; private set; }


    void Awake()
    {
        if (Instanse == null)
            Instanse = this;
        else
        {
            Destroy(this);
            return;
        }
    }


    public static void ApplySpellVisuals(Unit target, TrinitySpellInfo spellInfo)
    {
        if (WarcraftDatabase.SpellVisuals.ContainsKey(spellInfo.VisualId))
            Instantiate(WarcraftDatabase.SpellVisuals[spellInfo.VisualId], target.transform.position, target.transform.rotation);
        else
            Debug.LogError("No visuals for spell #" + spellInfo.Id);
    }

    public static void ApplySpellCastSound(Unit target, TrinitySpellInfo spellInfo)
    {
        if (WarcraftDatabase.SpellCastSounds.ContainsKey(spellInfo.Id))
            AudioSource.PlayClipAtPoint(WarcraftDatabase.SpellCastSounds[spellInfo.Id], target.transform.position);
    }

    public static void SpellDamageEvent(Unit caster, Unit target, int damage, bool isCrit)
    {
        if (caster == ArenaManager.PlayerInterface.PlayerUnit)
        {
            GameObject damageEvent = Instantiate(Resources.Load("Prefabs/UI/DamageEvent")) as GameObject;
            damageEvent.GetComponent<UnitDamageUIEvent>().Initialize(damage, target, isCrit, ArenaManager.PlayerInterface);
        }
    }

}