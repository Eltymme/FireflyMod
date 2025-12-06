using BepInEx;
using Eltymme.FireflyMod.Common;
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

        public void Start()
        {
            ETGModMainBehaviour.WaitForGameManagerStart(GmStart);
        }

        public void GmStart(GameManager g)
        {
            SeasAblaze.Register();
            Proto.Register();
            
            Log($"{NAME} v{VERSION} started successfully.", HexColor.Cyan);
        }

        public static void Log(string text, string color= HexColor.White)
        {
            ETGModConsole.Log($"<color={color}>{text}</color>");
        }
    }
}
