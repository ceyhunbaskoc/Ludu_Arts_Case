using System;
using UnityEngine;

namespace Ludu_Arts_Case.Scripts.Runtime.Core
{
    /// <summary>
    /// Base class for interactable objects in the game.
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Fields
        [SerializeField]
        private InteractionType m_interactionType = InteractionType.Instant;

        [SerializeField]
        private string m_promptText = "Interact";

        [SerializeField]
        private bool m_isInteractable = true;

        [SerializeField]
        private float m_holdDuration = 0f;
        
        #endregion
        
        #region Properties
        
        /// <inheritdoc />
        public InteractionType InteractionType => m_interactionType;
        /// <inheritdoc />
        public string PromptText => m_promptText;
        /// <inheritdoc />
        public bool IsInteractable => m_isInteractable;
        /// <inheritdoc />
        public float HoldDuration => m_holdDuration;
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the interactable state of the object.
        /// </summary>
        /// <param name="interactable">new state</param>
        public void SetInteractable(bool interactable)
        {
            m_isInteractable = interactable;
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
