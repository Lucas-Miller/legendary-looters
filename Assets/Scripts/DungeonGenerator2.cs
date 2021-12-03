using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator2 : MonoBehaviour
{
    public Module[] possibleRooms; //Array of all possible rooms
    public Module startingRoom; //The first room placed to be built off of
    public int roomCount = 5; //Number of desired rooms
    public List<GameObject> allPlacedRooms; //A list of every room that is already successfully placed
    public GameObject[] Enemies;

    // Start is called before the first frame update
    void Start()
    {
        Module startModule = Instantiate(startingRoom); // Place first room, mark it as starting room
        startModule.isStartRoom = true;         
        //allPlacedRooms.Add(startModule.gameObject);     // add it to list of placed rooms

        List<ModuleConnector> pendingConnections = new List<ModuleConnector>(startModule.GetConnectors()); //List of all connectors of startRoom
        int placedRooms = 1;
        //int i = 0;
        while(placedRooms <= roomCount)
        {

            var newConnectors = new List<ModuleConnector>();
            foreach(var connector in pendingConnections)
            {
                if(connector.isConnected == false)
                {
                    var newTag = GetRandom(connector.Tags);
                    Module newModulePrefab = GetRandomWithTag(possibleRooms, newTag);
                    Module newModule = Instantiate(newModulePrefab);
                    placedRooms++;
                    ModuleConnector[] newModuleConnectors = newModule.GetConnectors();
                    var connectorsToMatch = newModuleConnectors.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newModuleConnectors);
                    MatchConnectors(connector, connectorsToMatch);
                    newConnectors.AddRange(newModuleConnectors.Where(e => e != connectorsToMatch));
                }

            }
            pendingConnections = newConnectors;

        }
        spawnEnemies();



    }

    private void MatchConnectors(ModuleConnector currentConnection, ModuleConnector newConnection)
    {
        var newModule = newConnection.transform.parent;               //Get the new module from newConnections parent
        var forwardVectorToMatch = -currentConnection.transform.forward;  //Get opposite forward vector from the exit we need to match to
        var correctiveRotation = signedRotationAngle(forwardVectorToMatch) - signedRotationAngle(newConnection.transform.forward);
        newModule.RotateAround(newConnection.transform.position, Vector3.up, correctiveRotation);
        var correctiveTranslation = currentConnection.transform.position - newConnection.transform.position;
        newModule.transform.position += correctiveTranslation;
        currentConnection.isConnected = true;
        newConnection.isConnected = true;

    }

    private void spawnEnemies()
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach (var point in spawnPoints)
        {
            //Instantiate(Enemies[0], point.transform.position, Quaternion.identity);
            Debug.Log("Placing Enemy");
            Instantiate(Enemies[0], point.transform.position, Quaternion.identity);
        }
    }

    private static float signedRotationAngle(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }

    private static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
    {
        var matchingModules = modules.Where(m => m.Tags.Contains(tagToMatch)).ToArray(); // Find modules that match the tagToMatch in an array 
        return GetRandom(matchingModules);  // Return one of the modules in the array
    }

    void makeConnections(List<ModuleConnector> pendingConnections)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static TItem GetRandom<TItem>(TItem[] array)
    {
        //Debug.Log()
        return array[Random.Range(0, array.Length)];
    }


}
