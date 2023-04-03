using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boilerplate.Utilities
{
    public abstract class DescriptionScriptableObject : ScriptableObject
    {
        [TextArea] [SerializeField] private string description;
    }
}