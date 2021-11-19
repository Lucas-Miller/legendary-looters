using UnityEngine;

public class Module : MonoBehaviour
{
	public string[] Tags;
	public bool isStartRoom = false;

	public ModuleConnector[] GetConnectors()
	{
		return GetComponentsInChildren<ModuleConnector>();
	}



}

