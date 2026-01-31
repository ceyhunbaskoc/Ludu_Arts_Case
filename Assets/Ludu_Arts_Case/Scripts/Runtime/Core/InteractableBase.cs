using System;
using UnityEngine;

namespace LuduArtsCase.Runtime.Core
{
    /// <summary>
    /// Base class for interactable objects in the game.
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Fields
        [SerializeField]
        private InteractionType m_InteractionType = InteractionType.Instant;

        [SerializeField]
        private string m_PromptText = "Interact";

        [SerializeField]
        private bool m_IsInteractable = true;

        [SerializeField]
        private float m_HoldDuration = 0f;
        
        #endregion
        
        #region Properties
        
        /// <inheritdoc />
        public InteractionType InteractionType => m_InteractionType;
        /// <inheritdoc />
        public string PromptText => m_PromptText;
        /// <inheritdoc />
        public bool IsInteractable => m_IsInteractable;
        /// <inheritdoc />
        public float HoldDuration => m_HoldDuration;
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the interactable state of the object.
        /// </summary>
        /// <param name="interactable">new state</param>
        public void SetInteractable(bool interactable)
        {
            m_IsInteractable = interactable;
        }
        
        #endregion
        
        #region Interface Implementations
        
        /// <inheritdoc />
        public abstract bool Interact(GameObject interactor);
        #endregion
        
        #region Unity Methods
        protected virtual void Awake()
        {
            if (GetComponent<Collider>() == null)
            {
                Debug.LogError($"Interactable object '{name}' missing a Collider component!", this);
            }
        }
        #endregion
    }
}
