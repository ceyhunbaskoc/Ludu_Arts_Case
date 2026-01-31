using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using LuduArtsCase.Runtime.ScriptableObjects;

namespace LuduArtsCase.Runtime.Player
{
    public class InventoryController : MonoBehaviour
    {
        #region Fields

        [Tooltip("Storage for the items in the inventory.")]
        [SerializeField] private List<ItemData> m_Items = new List<ItemData>();

        #endregion

        #region Events
        public event Action OnInventoryChanged;
        #endregion

        #region Public Methods
        public void AddItem(ItemData item)
        {
            m_Items.Add(item);
            OnInventoryChanged?.Invoke();
        }

        public void RemoveItem(ItemData item)
        {
            m_Items.Remove(item);
            OnInventoryChanged?.Invoke();
        }

        public bool HasItem(string itemID)
        {
            foreach (var item in m_Items)
            {
                if (item.ID.Trim() == itemID.Trim())
                    return true;
            }

            return false;
        }
        
        #endregion
    }
}