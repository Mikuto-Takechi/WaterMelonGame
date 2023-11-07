namespace Takechi
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class Example
    {
        [MenuItem("GameObject/UI/FillAmountSlider", false, 0)]
        private static void Create(MenuCommand menuCommand)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Scripts/CreateMenu/Fill Amount Slider.prefab");
            var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            instance.transform.SetParent(Selection.activeTransform, false);
            Undo.RegisterCreatedObjectUndo(instance, "Fill Amount Slider");
            Selection.activeObject = instance;
        }
    }
}