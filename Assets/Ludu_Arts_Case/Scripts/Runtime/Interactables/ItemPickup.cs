using LuduArtsCase.Runtime.Core;
using UnityEngine;
using LuduArtsCase.Runtime.Player;
using LuduArtsCase.Runtime.ScriptableObjects;

namespace LuduArtsCase.Runtime.Interactables
{
    public class ItemPickup : InteractableBase
    {
        #region Fields

        [SerializeField] private ItemData m_ItemData;

        #endregion

        #region Public Methods
        public override bool Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out InventoryController inventory))
            {
                inventory.AddItem(m_ItemData);
                Debug.Log($"Picked up: {m_ItemData.DisplayName}");
                Destroy(gameObject);
                return true;
            }

            return false;
        }
        #endregion
    }
}
