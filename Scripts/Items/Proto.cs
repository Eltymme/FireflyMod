using Alexandria.ItemAPI;
using Alexandria.Misc;
using Eltymme.FireflyMod.Common;
using UnityEngine;

namespace Eltymme.FireflyMod.Scripts.Items
{
    public class Proto : PlayerItem
    {
        private const string NAME = "Proto";
        private const string RESOURCE_NAME = "Eltymme/FireflyMod/Resources/example_item_sprite";
        private const string SHORT_DESC = "Who Said Glass Cannon?";
        private const string LONG_DESC = "Example Long Description\n\n" +
                                         "Wow this description is really looooooooooong!";
        
        private const float COOLDOWN = 3f;
        private const float HEALTH_REMOVING = 1f;
        private const float ARMOR_ADDING = 1f;
        
        public static void Register()
        {
            var go = new GameObject(NAME);
            var item = go.AddComponent<Proto>();

            ItemBuilder.AddSpriteToObject(NAME, RESOURCE_NAME, go);
            item.SetupItem(SHORT_DESC, LONG_DESC, "eltymme-fireflymod");
            item.SetCooldownType(ItemBuilder.CooldownType.Timed, COOLDOWN);
            item.quality = ItemQuality.D;
        }

        public override bool CanBeUsed(PlayerController player) => 
            player.healthHaver.Armor != 0f || player.healthHaver.GetCurrentHealth() > HEALTH_REMOVING;

        public override void DoEffect(PlayerController player)
        {
            if (player.healthHaver.Armor != 0f)
            {
                var currentArmor = player.healthHaver.Armor;
                
                player.healthHaver.Armor = 0f;
                player.healthHaver.ApplyHealing(currentArmor/2);
            }
            else
            {
                player.healthHaver.ApplyDamage(
                    HEALTH_REMOVING, Vector2.zero, "ProtoActivation", ignoreInvulnerabilityFrames: true, ignoreDamageCaps: true);
                player.healthHaver.Armor += ARMOR_ADDING;
            }
        }
    }
}