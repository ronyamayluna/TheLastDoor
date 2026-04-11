using UnityEngine;
using System.Collections.Generic;
using System;

namespace Inventory
{
    public interface IReadOnlyInventorySlot
    {
        event Action<string> ItemIdChanged;
        event Action<int> AmountChanged;

        string ItemId { get; }
        int Amount { get; }
        bool IsEmpty { get; }
    }
}
