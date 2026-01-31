using System;
using System.Collections;
using UnityEngine;
using LuduArtsCase.Runtime.Core;
namespace LuduArtsCase.Runtime.Interactables
{
    public class Door : InteractableBase
    {
        #region Fields
        [Header("Door Settings")]
        [SerializeField] private float m_OpenAngle = 90f;
        [SerializeField] private bool m_IsLocked = false;
        [SerializeField] private float m_AnimationDuration = 1f;
        
        // TODO: Key types will be implemented in the future
        
        private bool m_IsOpen = false;
        private Quaternion m_ClosedRotation;
        private Quaternion m_OpenRotation;
        private bool m_IsAnimating = false;
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
                // TODO: When connecting the inventory system, a “Is there a key?” check will be added here.
                Debug.Log("Door is locked! Find the key.");
                return false; 
            }
            if(m_CurrentAnimationCoroutine != null)
                StopCoroutine(m_CurrentAnimationCoroutine);
            m_CurrentAnimationCoroutine = StartCoroutine(PlayAnimation());
            return true;
        }

        public void Unlock()
        {
            m_IsLocked = false;
            Debug.Log("Door unlocked!");
        }
        
        #endregion
        
        #region Coroutines
        
        private IEnumerator PlayAnimation()
        {
            m_IsAnimating = true;
            Quaternion startRotation = transform.localRotation;
            Quaternion endRotation = m_IsOpen ? m_ClosedRotation : m_OpenRotation;
            float elapsedTime = 0f;
            
            m_IsOpen = !m_IsOpen;
            Debug.Log(m_IsOpen ? "Door Opened" : "Door Closed");

            while (elapsedTime < m_AnimationDuration)
            {
                transform.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / m_AnimationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localRotation = endRotation;
            m_IsAnimating = false;
        }
        
        #endregion
    }
}
