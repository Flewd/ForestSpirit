namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        public static T GetComponentWithInterface<T>(this GameObject go) where T : class
        {
            return go.GetComponent(typeof(T)) as T;
        }
        
        public static T[] GetComponentsWithInterface<T>(this GameObject go) where T : class
        {
            Component[] components = go.GetComponents(typeof(T));
            T[] interfaceComponents = new T[components.Length];
            
            // convert to IOnTriggerListener interfaces
            for (int i = 0; i < components.Length; i++){
                if (components[i] is T){
                    interfaceComponents[i] = components[i] as T;
                }
            }

            return interfaceComponents;
        }
        
        public static T[] GetComponentsInChildrenWithInterface<T>(this GameObject go) where T : class
        {
            Component[] components = go.GetComponentsInChildren(typeof(T));
            T[] interfaceComponents = new T[components.Length];
            
            // convert to IOnTriggerListener interfaces
            for (int i = 0; i < components.Length; i++){
                if (components[i] is T){
                    interfaceComponents[i] = components[i] as T;
                }
            }

            return interfaceComponents;
        }
    }
}