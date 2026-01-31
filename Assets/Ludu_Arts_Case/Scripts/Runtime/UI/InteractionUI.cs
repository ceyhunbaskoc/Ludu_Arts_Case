using System;
using LuduArtsCase.Runtime.Core;
using LuduArtsCase.Runtime.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuduArtsCase.Runtime.UI
{
    public class InteractionUI : MonoBehaviour
    {
        #region Fields

        [Header("References")]
        [SerializeField] private InteractionDetector m_PlayerInteractionDetector;
        [SerializeField] private CanvasGroup m_PromptContainer;
        [SerializeField] private TextMeshProUGUI m_InteractionPromptText;
        [SerializeField] private Image m_HoldProgressImage;
        

        #endregion

        #region Unity Methods


        private void OnEnable()
        {
            if (m_PlayerInteractionDetector != null)
            {
                m_PlayerInteractionDetector.OnInteractableFound += HandleInteractableFound;
                m_PlayerInteractionDetector.OnHoldProgress += HandleHoldProgress;
            }
        }

        private void OnDisable()
        {
            if (m_PlayerInteractionDetector != null)
            {
                m_PlayerInteractionDetector.OnInteractableFound -= HandleInteractableFound;
                m_PlayerInteractionDetector.OnHoldProgress -= HandleHoldProgress;
            }
        }

        private void Start()
        {
            HidePrompt();
            HandleHoldProgress(0f);
        }

        #endregion

        #region private Methods

        private void HandleInteractableFound(IInteractable interactable)
        {
            if (interactable != null && interactable.IsInteractable)
            {
                m_InteractionPromptText.text = interactable.PromptText;
                ShowPrompt();
            }
            else
            {
                m_InteractionPromptText.text = String.Empty;
                HidePrompt();
            }
        }
        
        private void HandleHoldProgress(float progress)
        {
            if (m_HoldProgressImage != null)
            {
                m_HoldProgressImage.fillAmount = progress;
            }
        }
        
        private void ShowPrompt()
        {
            if (m_PromptContainer.alpha == 0f)
            {
                m_PromptContainer.alpha = 1f;
            }
        }

        private void HidePrompt()
        {
            if (m_PromptContainer.alpha != 0f)
            {
                m_InteractionPromptText.text = string.Empty;
                m_PromptContainer.alpha = 0f;
            }
        }

        #endregion
    }
}