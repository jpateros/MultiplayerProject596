using Unity.Netcode.Components;
using UnityEngine;

namespace Unity.Multiplayer.Samples.Utilities.ClientAuthority
{

    /// <summary>
    /// This class handles Netcode networking issues such as spawning client and host next to each other.
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

        //Ensure that the client is host and client are spawned at seperate locations
        void Start()
        {
             //Host gets spawned to the right of the client at start of race
            if (IsHost)
            {
                transform.position += Vector3.right * 2f;
            }


        }
    }
}

