using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEqupmentChanged += OnEquipmentChanged;
    }

  

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armourValue.AddModifier(newItem.armourModifier);
            damageValue.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armourValue.RemoveModifier(oldItem.armourModifier);
            damageValue.RemoveModifier(oldItem.damageModifier);
        }
    }
}
