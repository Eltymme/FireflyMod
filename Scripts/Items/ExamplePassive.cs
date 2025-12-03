using Alexandria.ItemAPI;
using UnityEngine;

namespace Eltymme.FireflyMod.Scripts.Items
{
    public class ExamplePassive : PassiveItem
    {
        private const string NAME = "ExamplePassive";
        private const string RESOURCE_NAME = "Eltymme/FireflyMod/Resources/example_item_sprite";
        private const string SHORT_DESC = "Example Short Desc.";
        private const string LONG_DESC = "Example Long Description\n\n" +
                                         "Wow this description is really looooooooooong!";
        
        public static void Register()
        {
            var go = new GameObject(NAME);
            var item = go.AddComponent<ExamplePassive>();
            
            ItemBuilder.AddSpriteToObject(NAME, RESOURCE_NAME, go);
            item.SetupItem(SHORT_DESC, LONG_DESC, "example");
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Main.Log($"Player picked up {DisplayName}");
        }

        public override void DisableEffect(PlayerController player)
        {
            Main.Log($"Player dropped or got rid of {DisplayName}");
        }
    }
}