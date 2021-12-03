using UnityEngine;


public class EnemySpawn : MonoBehaviour
{
	public string[] PossibleEnemyTags;
	//public bool IsDefault;


	void OnDrawGizmos()
	{
		//var scale = 1.0f;

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.125f);
	}
}