using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] SerializableKeyPair<string, CanvasGroup>[] _serializablePanels;
    Dictionary<string, CanvasGroup> _panels = new();
    void Start()
    {
        _panels = _serializablePanels.ToDictionary(pair => pair.Key, pair => pair.Value);
    }
    void ChangePanel(string key)
    {
        if (_panels.ContainsKey(key) == false)
        {
            Debug.LogError("指定された名前のパネルが見つかりませんでした");
            return;
        }
        foreach (var panel in _panels)
        {
            panel.Value.alpha = 0;
            panel.Value.blocksRaycasts = false;
            panel.Value.interactable = false;
            if(panel.Key == key)
            {
                panel.Value.alpha = 1;
                panel.Value.blocksRaycasts = true;
                panel.Value.interactable = true;
            }
        }
    }
}
