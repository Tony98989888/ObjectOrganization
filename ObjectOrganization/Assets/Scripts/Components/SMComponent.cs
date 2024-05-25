// Scene Markup Component

// - Scene Markup Component(SMComponents):
// - Instance name
//     - FreeFrom information List of Tuple:
// - Type (fed by centralized and centrally configurable collection)
//     - Value(Free form text)
//     - Short description
//     - Long description
//     - priority override
// - Predefined information List of Tuple:
// - Type (fed by centralized and centrally configurable collection)
//     - Value (fed by centralized and centrally configurable collection) should be read only
//     - Short description
//     - Long description
//     - Interaction range, radius in meters around the object at which point the AI can perceive it.
// - Think about whether a a quad tree might be helpful for the lookups from a performance perspective.
// - OnStart()
//     - Register itself with the SceneMarkupSystem
//     - receive and store running ID from SceneMarkupSystem
// - Should be a Object that only handles start up and shutdown, all other functionality should eb in the handling system.
// - Interaction priority


using UnityEngine;

public class SMComponent : SceneObject
{
    [SerializeField]
    private SMData data;
}