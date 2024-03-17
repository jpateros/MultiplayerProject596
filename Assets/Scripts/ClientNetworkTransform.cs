using Unity.Netcode.Components;
using UnityEngine;

namespace Unity.Multiplayer.Samples.Utilities.ClientAuthority
{
    /// <summary>
    /// Used for syncing a transform with client side changes. This includes host. Pure server as owner isn't supported by this. Please use NetworkTransform
    /// for transforms that'll always be owned by the server.
    /// </summary>
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {

        public event System.Action<GameObject> OnPlayerSpawned;

        /// <summary>
        /// Used to determine who can write to this transform. Owner client only.
        /// This imposes state to the server. This is putting trust on your clients. Make sure no security-sensitive features use this transform.
        /// </summary>
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }

        // Call this method when the player is spawned by the network manager
        public void PlayerSpawned(GameObject player)
        {
            if (OnPlayerSpawned != null)
            {
                OnPlayerSpawned(player);
            }
        }

        //ensurew that the client is spawned
        void Start()
        {
            // Check if this instance belongs to the client
            if (IsHost)
            {
                // Shift the client's spawn position to the right by 2 units
                transform.position += Vector3.right * 2f;
            }


        }
    }
}

