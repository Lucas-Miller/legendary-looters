using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This tutorial was used for the basis of the random generation system
//https://gamedevelopment.tutsplus.com/tutorials/bake-your-own-3d-dungeons-with-procedural-recipes--gamedev-14360

public class DungeonGenerator : MonoBehaviour
{
    public Module[] rooms;      // Array of all possible rooms/hallways/etc
    public Module startingRoom; // Room where player will start and where dungeon is built off of
    public int Iterations = 10;

    // Start is called before the first frame update
    void Start()
    {
        Module startModule = Instantiate(startingRoom, transform.position, transform.rotation); // Instantiate the starting room

        var pendingConnectors = new List<ModuleConnector>(startModule.GetConnectors()); //Get a list of starting rooms connectors

        for(int i = 0; i < Iterations; ++i)
        {
            var newConnectors = new List<ModuleConnector>();

            foreach(var connector in pendingConnectors)
            {
                var newTag = GetRandom(connector.Tags); // For each possible connector, get one of the tags that can possibly be connected to it
                Module newModulePrefab = GetRandomWithTag(rooms, newTag); // With that tag, get the associated prefab that matches
                Module newModule = Instantiate(newModulePrefab); //Instantiate a new gameobject of the randomly selected prefab
                ModuleConnector[] newModuleConnectors = newModule.GetConnectors(); //Get the connectors for the newly instantiated prefab
                var connectorsToMatch = newModuleConnectors.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newModuleConnectors); // Set equal to either first con or default con in editor
                
                MatchConnectors(connector, connectorsToMatch);
                newConnectors.AddRange(newModuleConnectors.Where(e => e != connectorsToMatch));
            }
            pendingConnectors = newConnectors;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private static TItem GetRandom<TItem>(TItem[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    private static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
    {
        var matchingModules = modules.Where(m => m.Tags.Contains(tagToMatch)).ToArray(); // Find modules that match the tagToMatch in an array 
        return GetRandom(matchingModules);  // Return one of the modules in the array
    }

    private void MatchConnectors(ModuleConnector currentConnection, ModuleConnector newConnection)
    {
        var newModule = newConnection.transform.parent;               //Get the new module from newConnections parent
        var forwardVectorToMatch = -currentConnection.transform.forward;  //Get opposite forward vector from the exit we need to match to
        var correctiveRotation = signedRotationAngle(forwardVectorToMatch) - signedRotationAngle(newConnection.transform.forward);
        newModule.RotateAround(newConnection.transform.position, Vector3.up, correctiveRotation);
        var correctiveTranslation = currentConnection.transform.position - newConnection.transform.position;
        newModule.transform.position += correctiveTranslation;
        
    }


    private static float signedRotationAngle(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}
