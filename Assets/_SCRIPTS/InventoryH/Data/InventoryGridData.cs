using UnityEngine;
using System.Collections.Generic;
using System;

namespace Inventory
{
    [Serializable]
    public class InventoryGridData : MonoBehaviour
    {
        public string OwnerId;
        public List<InventorySlotData> Slots;
        public Vector2Int Size;  
    }
}