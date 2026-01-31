using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using LuduArtsCase.Runtime.ScriptableObjects;
using TMPro;

namespace LuduArtsCase.Runtime.Player
{
    public class InventoryController : MonoBehaviour
    {
        #region Fields

        [Tooltip("Storage for the items in the inventory.")]
        [SerializeField] private List<ItemData> m_Items = new List<ItemData>();
        
        [SerializeField] private TextMeshProUGUI m_DebugText;

        #endregion

        #region Properties

        public System.Collections.ObjectModel.ReadOnlyCollection<ItemData> Items => m_Items.AsReadOnly();

        #endregion
        #region Events
        public event Action OnInventoryChanged;
        #endregion

        #region Public Methods
        public void AddItem(ItemData item)
        {
            m_Items.Add(item);
            UpdateInventoryText();
            OnInventoryChanged?.Invoke();
        }

        public void RemoveItem(ItemData item)
        {
            m_Items.Remove(item);
            UpdateInventoryText();
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
        #region Private Methods
        private void UpdateInventoryText()
        {
            m_DebugText.text = "Inventory:";
            foreach (var item in m_Items)
            {
                m_DebugText.text += "\n" + item.DisplayName;
            }
        }
        #endregion
    }
}