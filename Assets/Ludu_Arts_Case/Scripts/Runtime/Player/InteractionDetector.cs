using System;
using UnityEngine;
using LuduArtsCase.Runtime.Core;

namespace LuduArtsCase.Runtime.Player
{
    public class InteractionDetector : MonoBehaviour
    {
        #region Fields
        
        [Header("Detection Settings")]
        
        [SerializeField] private float m_InteractionRange = 3f;
        [SerializeField] private LayerMask m_InteractionLayerMask;
        [SerializeField] private KeyCode m_InteractionKey = KeyCode.E;
        [SerializeField] private Camera m_PlayerCamera;
        
        private IInteractable m_CurrentInteractable;
        private float m_CurrentHoldTime;
        private bool m_IsHolding;
        private bool m_HasInteractedThisPress;
        
        #endregion
        
        #region Events
        /// <summary>
        /// Invoked when an interactable object is detected or lost.
        /// </summary>
        public event Action<IInteractable> OnInteractableFound;
        /// <summary>
        /// Invoked to report hold interaction progress (0 to 1).
        /// </summary>
        public event Action<float> OnHoldProgress;
        /// <summary>
        /// Invoked when an interaction is successfully performed.
        /// </summary>
        public event Action OnInteractionPerformed;
        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            if (m_PlayerCamera == null)
            {
                Debug.LogError($"[{nameof(InteractionDetector)}] Player Camera is not assigned!", this);
                enabled = false;
            }
        }
        
        private void Update()
        {
            DetectTarget();
            HandleInput();
        }
        #endregion
        
        #region Private Methods
        
        private void DetectTarget()
        {
            Ray ray = m_PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hitInfo, m_InteractionRange, m_InteractionLayerMask))
            {
                if (hitInfo.collider.TryGetComponent<IInteractable>(out var interactable) && interactable.IsInteractable)
                {
                    if (m_CurrentInteractable != interactable)
                    {
                        m_CurrentInteractable = interactable;
                        OnInteractableFound?.Invoke(m_CurrentInteractable);
                    }
                    return;
                }
            }

            if (m_CurrentInteractable != null)
            {
                ClearInteraction();
            }
        }

        private void HandleInput()
        {
            if (m_CurrentInteractable == null) return;

            // one press interactions
            if (m_CurrentInteractable.InteractionType == InteractionType.Instant || 
                m_CurrentInteractable.InteractionType == InteractionType.Toggle)
            {
                if (Input.GetKeyDown(m_InteractionKey))
                {
                    PerformInteraction();
                }
            }
            // hold interactions
            else if (m_CurrentInteractable.InteractionType == InteractionType.Hold)
            {
                HandleHoldInteraction();
            }
        }
        private void HandleHoldInteraction()
        {
            if (Input.GetKey(m_InteractionKey))
            {
                if (m_HasInteractedThisPress) return;
                
                m_IsHolding = true;
                m_CurrentHoldTime += Time.deltaTime;
                
                float progress = Mathf.Clamp01(m_CurrentHoldTime / m_CurrentInteractable.HoldDuration);
                OnHoldProgress?.Invoke(progress);

                if (m_CurrentHoldTime >= m_CurrentInteractable.HoldDuration)
                {
                    PerformInteraction();
                    m_HasInteractedThisPress = true;
                    OnHoldProgress?.Invoke(0f);
                }
            }
            else if (Input.GetKeyUp(m_InteractionKey))
            {
                ResetHoldState();
            }
            else if (m_IsHolding && !Input.GetKey(m_InteractionKey))
            {
                ResetHoldState();
            }
        }
        
        private void PerformInteraction()
        {
            m_CurrentInteractable.Interact(gameObject);
            OnInteractionPerformed?.Invoke();
        }

        private void ClearInteraction()
        {
            m_CurrentInteractable = null;
            OnInteractableFound?.Invoke(null);
            ResetHoldState();
        }

        private void ResetHoldState()
        {
            m_IsHolding = false;
            m_HasInteractedThisPress = false;
            m_CurrentHoldTime = 0f;
            OnHoldProgress?.Invoke(0f);
        }
        
        #endregion
        
        #region Debugging
        private void OnDrawGizmos()
        {
            if (m_PlayerCamera == null) return;
            
            Gizmos.color = Color.green;
            Ray ray = m_PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Gizmos.DrawRay(ray.origin, ray.direction * m_InteractionRange);
        }
        #endregion
    }
}
