using UnityEngine;

namespace Ludu_Arts_Case.Scripts.Runtime.Core
{
    /// <summary>
    /// Interface for interactable objects in the game.
    /// </summary>
    public interface IInteractable
    {
        InteractionType InteractionType { get; }
        string PromptText { get; }
        bool IsInteractable { get; }
        float HoldDuration { get; }
        
        /// <summary>
        /// Method to handle interaction logic.
        /// </summary>
        /// <param name="interactor">Object initiating interaction</param>
        /// <returns></returns>
        bool Interact(GameObject interactor);
    }
}
