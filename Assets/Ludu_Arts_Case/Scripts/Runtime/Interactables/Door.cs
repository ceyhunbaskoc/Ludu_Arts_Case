using System;
using System.Collections;
using UnityEngine;
using LuduArtsCase.Runtime.Core;
using LuduArtsCase.Runtime.Player;

namespace LuduArtsCase.Runtime.Interactables
{
    public class Door : InteractableBase
    {
        #region Fields
        [Header("Door Settings")]
        [SerializeField] private float m_OpenAngle = 90f;
        [SerializeField] private float m_AnimationDuration = 1f;
        
        [Header("Lock Settings")]
        [SerializeField] private string m_KeyItemID = "DoorKey";
        [SerializeField] private bool m_IsLocked = false;
        
        private bool m_IsOpen = false;
        private Quaternion m_ClosedRotation;
        private Quaternion m_OpenRotation;
        private Coroutine m_CurrentAnimationCoroutine;
        
        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            m_ClosedRotation = transform.localRotation;
            m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0, m_OpenAngle, 0);
        }

        #endregion
        
        #region Public Methods
        
        public override bool Interact(GameObject interactor)
        {
            if (m_IsLocked)
            {
                if (interactor.TryGetComponent(out InventoryController inventory))
                {
                    if (inventory.HasItem(m_KeyItemID))
                    {
                        Unlock();
                        ToggleDoor();
                        return true;
                    }
                }
                
                Debug.Log($"Door is locked! Requires key: {m_KeyItemID}");
                return false; 
            }

            ToggleDoor();
            return true;
        }

        public void Unlock()
        {
            m_IsLocked = false;
            Debug.Log("Door unlocked!");
        }
        
        #endregion
        
        #region Private Methods
        private void ToggleDoor()
        {
            m_IsOpen = !m_IsOpen;
            
            if (m_CurrentAnimationCoroutine != null)
                StopCoroutine(m_CurrentAnimationCoroutine);
            
            m_CurrentAnimationCoroutine = StartCoroutine(PlayAnimationRoutine(m_IsOpen));
        }

        private IEnumerator PlayAnimationRoutine(bool targetOpen)
        {
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = targetOpen ? m_OpenRotation : m_ClosedRotation;
            float elapsedTime = 0f;

            while (elapsedTime < m_AnimationDuration)
            {
                transform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / m_AnimationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localRotation = endRotation;
        }
        #endregion
    }
}
