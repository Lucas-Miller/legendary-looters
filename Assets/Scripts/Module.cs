using UnityEngine;

public class Module : MonoBehaviour
{
	public string[] Tags;

	public ModuleConnector[] GetConnectors()
	{
		return GetComponentsInChildren<ModuleConnector>();
	}
}
