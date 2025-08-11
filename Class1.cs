using BepInEx;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

namespace FontReplacer
{
    [BepInPlugin("com.yourname.fontreplacer", "Font Replacer", "1.1.0")]
    public class FontReplacer : BaseUnityPlugin
    {
        private TMP_FontAsset customFont;

        void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Load font bundle from plugin folder
            string pluginPath = Path.GetDirectoryName(Info.Location);
            string bundlePath = Path.Combine(pluginPath, "myfontbundle");
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);

            if (bundle == null)
            {
                Logger.LogError("Failed to load AssetBundle!");
                return;
            }

            // Change this to your actual font asset name inside Unity
            customFont = bundle.LoadAsset<TMP_FontAsset>("MyFontTMP");

            if (customFont == null)
            {
                Logger.LogError("Failed to load TMP_FontAsset from bundle!");
            }
            else
            {
                Logger.LogInfo("Custom font loaded successfully!");
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (customFont == null) return;

            // Replace fonts in all TMP_Text objects in the scene
            foreach (TMP_Text tmpText in GameObject.FindObjectsOfType<TMP_Text>(true))
            {
                tmpText.font = customFont;
            }

            Logger.LogInfo($"Fonts replaced in scene: {scene.name}");
        }
    }
}
