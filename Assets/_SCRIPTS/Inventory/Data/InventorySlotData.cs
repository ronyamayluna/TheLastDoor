using UnityEngine;
using System.Collections.Generic;
using System;


namespace Inventory
{
    [Serializable]
    public class InventorySlotData : MonoBehaviour
    {
        public string ItemId;
        public int Amount;
    }
}
