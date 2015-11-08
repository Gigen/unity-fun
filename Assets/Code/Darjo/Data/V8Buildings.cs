using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class V8Buildings : ScriptableObject {
    [System.Serializable]
    public class BuildingData {
        public string Name;
        public Building Prefab;
    }

    public List<BuildingData> BuildingsData;
}
