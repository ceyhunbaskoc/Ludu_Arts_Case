using UnityEngine;

namespace LuduArtsCase.Runtime.Core
{
    /// <summary>
    /// Interface for interactable objects in the game.
    /// </summary>
    public interface IInteractable
    {
        /// <summary>
        /// Type of interaction.
        /// </summary>
        InteractionType InteractionType { get; }
        
        /// <summary>
        /// Interaction text to be displayed to the player.
        /// </summary>
        string PromptText { get; }
        
        /// <summary>
        /// Indicates if the object is currently interactable.
        /// </summary>
        bool IsInteractable { get; }
        
        /// <summary>
        /// Duration required to hold for interaction (if applicable).
        /// </summary>
        float HoldDuration { get; }
        
        /// <summary>
        /// Method to handle interaction logic.
        /// </summary>
        /// <param name="interactor">Object initiating interaction</param>
        /// <returns></returns>
        bool Interact(GameObject interactor);
    }
}
