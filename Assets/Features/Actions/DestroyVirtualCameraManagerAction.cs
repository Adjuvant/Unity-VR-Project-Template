using NaughtyAttributes;
using UnityEngine;

namespace GameplayIngredients.Actions
{
    // [AddComponentMenu(ComponentMenu.actionsPath + "Destroy Type of Object Action")]
    [Callable("Game Objects", "Actions/ic-action-trash.png")]
    public class DestroyVirtualCameraManagerAction : ActionBase
    {
        public override void Execute(GameObject instigator = null)
        {
            var ObjectsToDestroy = FindObjectsOfType<VirtualCameraManager>();
            if (ObjectsToDestroy != null )
            {
                foreach(var obj in ObjectsToDestroy)
                    Destroy(obj.gameObject);
            }

            
        }
        
        public override string GetDefaultName()
        {
            return $"Destroying Virtual Camera Managers";
        }
    }
}
