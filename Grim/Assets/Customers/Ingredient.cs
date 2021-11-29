using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "TeaMaking/Create new ingredient")]
public class Ingredient : ScriptableObject
{
    [SerializeField] new string name;

    [SerializeField] string code;
    [SerializeField] string response;

    public string Name {
        get { return name; }
    }

    public string Code {
        get { return code; }
    }

    public string Response {
        get { return response; }
    }
}
