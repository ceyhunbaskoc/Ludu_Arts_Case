using System.Collections;
using LuduArtsCase.Runtime.Core;
using LuduArtsCase.Runtime.Player;
using LuduArtsCase.Runtime.ScriptableObjects;
using UnityEngine;

namespace LuduArtsCase.Runtime.Interactables
{
    public class Chest : InteractableBase
    {
        #region Fields
        [Header("Chest Settings")]
        [SerializeField] private ItemData m_OutputItem;
        [SerializeField] private float m_OpenAngle = 110f;
        [SerializeField] private float m_AnimationDuration = 1f;
        
        [Header("References")]
        [SerializeField] private Transform m_CoverTransform;
        
        private bool m_IsOpen = false;
        
        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            if (m_OutputItem == null)
                Debug.LogError($"Chest '{name}' has no Output Item assigned!", this);

            if (m_CoverTransform == null)
                Debug.LogError($"Chest '{name}' has no Cover Transform assigned!", this);
        }

        #endregion
        
        #region Public Methods
        public override bool Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out InventoryController inventory))
            {
                if (!m_IsOpen)
                {
                    OpenChest();
                    inventory.AddItem(m_OutputItem);
                    Debug.Log($"Chest opened! Received: {m_OutputItem.DisplayName}");
                    SetInteractable(false);
                    SetPromptText("Empty");
                    return true;
                }
            }

            return false;
        }
        #endregion
        
        #region Private Methods
        private void OpenChest()
        {
            StartCoroutine(AnimateChestOpen());
        }

        private IEnumerator AnimateChestOpen()
        {
            m_IsOpen = true;
            Quaternion startRotation = m_CoverTransform.localRotation;
            Quaternion endRotation = startRotation * Quaternion.Euler(m_OpenAngle, 0, 0);
            float elapsedTime = 0f;

            while (elapsedTime < m_AnimationDuration)
            {
                m_CoverTransform.localRotation =
                    Quaternion.Slerp(startRotation, endRotation, elapsedTime / m_AnimationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            m_CoverTransform.localRotation = endRotation;
        }
        #endregion
    }
}
