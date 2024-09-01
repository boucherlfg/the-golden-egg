using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(SkeletonPlacer))]
public class SkeletonPlacerEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("delete player prefs")) {
            PlayerPrefs.DeleteAll();
            Debug.Log("player prefs have been deleted");
        }
    }
}
#endif
public class SkeletonPlacer : MonoBehaviour {
    private const string memory_tag = "skeletons";
    private List<Vector2> skeletons = new();
    [SerializeField]
    private GameObject skeleton;
    void Start() 
    {
        Load();

        foreach(var skeleton in skeletons) {
            Instantiate(this.skeleton, skeleton, Quaternion.identity, transform);
        }
        ServiceManager.Instance.Get<OnDeath>().Subscribe(HandleDeath);
    }

    void HandleDeath() {
        ServiceManager.Instance.Get<OnDeath>().Unsubscribe(HandleDeath);
        Save();
    }
    private void Load() {
        skeletons.Clear();
        var value = PlayerPrefs.GetString(memory_tag, "");
        foreach(var line in value.Split("\n", StringSplitOptions.RemoveEmptyEntries)) {
            var x = float.Parse(line.Split(' ').First());
            var y = float.Parse(line.Split(' ').Skip(1).First());
            skeletons.Add(new Vector2(x, y));
        }
    }
    private void Save() {
        Load();

        var death = FindObjectOfType<MoveScript>(true);
        skeletons.Add(death.transform.position);
        var saveString = string.Join("\n", skeletons.Select(sk => $"{sk.x} {sk.y}"));
        PlayerPrefs.SetString(memory_tag, saveString);
    }
}