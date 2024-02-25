using UnityEngine;


namespace Vald
{
    public class Cage : InteractableObject
    {
        public override void Interact(Player player)
        {
            // TODO: check key from player
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player") {
                var player = collision.gameObject.GetComponent<MonoPlayer>();

                if (!player.isInCage)
                    Interact(player.rawPlayer);
            }
        }
    }
}
