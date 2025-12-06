using Alexandria.ItemAPI;
using Alexandria.Misc;
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

        private const float DAMAGE_BOOST_MULTIPLIER = 1.5f;
        private const float DAMAGE_BOOST_BALANCER = -0.5f; //upper from -0.5 to start with damage boost, lower to start with damage debuff
        
        public static void Register()
        {
            var go = new GameObject(NAME);
            var item = go.AddComponent<SeasAblaze>();

            ItemBuilder.AddSpriteToObject(NAME, RESOURCE_NAME, go);
            item.SetupItem(SHORT_DESC, LONG_DESC, "eltymme-fireflymod");
            item.quality = ItemQuality.A;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            
            this.AddPassiveStatModifier(PlayerStats.StatType.Damage, 
                CalculateDamageBoostMultiplier(player.healthHaver.currentHealth, player.healthHaver.maximumHealth),
                StatModifier.ModifyMethod.MULTIPLICATIVE);
            
            player.stats.RecalculateStatsWithoutRebuildingGunVolleys(player);
            player.healthHaver.OnHealthChanged += PlayerHealthHaverOnHealthChanged;
        }
        
        public override void DisableEffect(PlayerController player)
        {
            base.DisableEffect(player);
            
            this.RemovePassiveStatModifier(PlayerStats.StatType.Damage);
            player.stats.RecalculateStatsWithoutRebuildingGunVolleys(player);
            player.healthHaver.OnHealthChanged -= PlayerHealthHaverOnHealthChanged;
        }

        private void PlayerHealthHaverOnHealthChanged(float resultValue, float maxValue)
        {
            this.RemovePassiveStatModifier(PlayerStats.StatType.Damage);
            this.AddPassiveStatModifier(PlayerStats.StatType.Damage, 
                CalculateDamageBoostMultiplier(resultValue, maxValue), StatModifier.ModifyMethod.MULTIPLICATIVE);
            
            Owner.stats.RecalculateStatsWithoutRebuildingGunVolleys(Owner);
        }

        private static float CalculateDamageBoostMultiplier(float current, float max) => 
            1f + DAMAGE_BOOST_MULTIPLIER * (1f - (current - 0.5f) / (max + DAMAGE_BOOST_BALANCER));
    }
}