using Alexandria.ItemAPI;
using Eltymme.FireflyMod.Common;
using UnityEngine;

namespace Eltymme.FireflyMod.Scripts.Items
{
    public class SeasAblaze : PassiveItem
    {
        private const string NAME = "Seas Ablaze";
        private const string RESOURCE_NAME = "Eltymme/FireflyMod/Resources/example_item_sprite";
        private const string SHORT_DESC = "Example Short Desc.";
        private const string LONG_DESC = "Example Long Description\n\n" +
                                         "Wow this description is really looooooooooong!";

        private const float DAMAGE_BOOST_MULTIPLIER = 3f;
        private const float DAMAGE_BOOST_BALANCER = -0.5f; //lower from -0.5 to start with damage boost, upper to start with damage debuff
        
        public static void Register()
        {
            var go = new GameObject(NAME);
            var item = go.AddComponent<SeasAblaze>();

            ItemBuilder.AddSpriteToObject(NAME, RESOURCE_NAME, go);
            item.SetupItem(SHORT_DESC, LONG_DESC, "eltymme-fireflymod");
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            player.OnReceivedDamage += PlayerOnReceivedDamage;
        }

        public override void DisableEffect(PlayerController player)
        {
            base.DisableEffect(player);
            this.RemovePassiveStatModifier(PlayerStats.StatType.Damage);
            player.OnReceivedDamage -= PlayerOnReceivedDamage;
        }

        private void PlayerOnReceivedDamage(PlayerController player)
        {
            this.RemovePassiveStatModifier(PlayerStats.StatType.Damage);
            
            var damageBoost = 1f + DAMAGE_BOOST_MULTIPLIER *
                              (1f - (player.healthHaver.currentHealth - 0.5f) /
                                  (player.healthHaver.maximumHealth + DAMAGE_BOOST_BALANCER));
            
            Main.Log($"damage boost: {damageBoost}", HexColor.Red);
            this.AddPassiveStatModifier(PlayerStats.StatType.Damage, damageBoost, StatModifier.ModifyMethod.MULTIPLICATIVE);
        }
    }
}