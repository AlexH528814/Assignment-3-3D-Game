using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEqupmentChanged;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        
        Equipment oldItem = Unequip(slotIndex);



        if (onEqupmentChanged != null)
        {
            onEqupmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);

        currentEquipment[slotIndex] = newItem;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;

    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }


            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEqupmentChanged != null)
            {
                onEqupmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Unequip"))
        {
            UnequipAll();
        }
    }
}
