using BepInEx;
using Eltymme.FireflyMod.Scripts.Items;

namespace Eltymme.FireflyMod.Scripts
{
    [BepInDependency(ETGModMainBehaviour.GUID)]
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        private const string GUID = "eltymme.etg.fireflymod";
        private const string NAME = "Firefly Mod";
        private const string VERSION = "0.0.0";
        private const string COLOR = "#00FFFF";

        public void Start()
        {
            ETGModMainBehaviour.WaitForGameManagerStart(GmStart);
        }

        public void GmStart(GameManager g)
        {
            ExamplePassive.Register();
            Log($"{NAME} v{VERSION} started successfully.", COLOR);
        }

        public static void Log(string text, string color="FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }
    }
}
