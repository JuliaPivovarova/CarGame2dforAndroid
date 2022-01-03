using UnityEngine;

namespace Code
{
    public static class ResourcesLoader
    {
        public static GameObject LoadPrefab(ResourcesPath path)
        {
            return Resources.Load<GameObject>(path.PathResources);
        }
    }
}