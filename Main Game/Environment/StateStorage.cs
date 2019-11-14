using UnityEngine;

public class StateStorage : MonoBehaviour
{
    public bool state;
    NewGodScriptEnvironment godEnvironment;
    NewGodScript god;

    private void Start()
    {
        godEnvironment = GameObject.Find("gOD").GetComponent<NewGodScriptEnvironment>();
        god = GameObject.Find("gOD").GetComponent<NewGodScript>();
    }

    public void UpdateState()
    {
        state = !state;
        godEnvironment.updates.Add(god.godTime, this.gameObject);
    }
}
