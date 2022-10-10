using System;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.SketchObjectManagement;
using VRSketchingGeometry.Commands.Group;

namespace VRpen.Scripts.Examples
{
    public class LineExample : MonoBehaviour
    {
        //A Default holds samples for all elements required to draw, such as the LineSketchObjectPrefab 
        [SerializeField]
        private DefaultReferences defaults;
        
        //Many commands require a SketchWorld to be present
        private SketchWorld _sketchWorld;
        
        //Creates a CommandInvoker that is necessary to execute commands.
        //A CommandInvoker memorizes all actions performed on it.
        private static readonly CommandInvoker Invoker = new CommandInvoker();
        
        void Start()
        {
            //Create a SketchWorld, many commands require a SketchWorld to be present
            _sketchWorld = Instantiate(defaults.SketchWorldPrefab).GetComponent<SketchWorld>();

            CreateSnailLine(new Vector3(0, 0, 0));

            CreateFrameWithGroupedLines(new Vector3(4, 0, 4));
            CreateFrameWithOneLine(new Vector3(4, 0, 0));
        }

        /// <summary>
        /// Creates a simple line that uses the default values for LineSketchObject.
        /// </summary>
        /// <param name="position"></param>
        private void CreateSnailLine(Vector3 position)
        {
            //Create a LineSketchObject
            LineSketchObject lineSketchObject = Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            
            //Adds a new object to the sketch world root. The sketch object is deleted when undoing this command
            Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(lineSketchObject, _sketchWorld));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(1, 0, 0) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(1, 0, 1) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(-1, 0, 1) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(-1, 0, -1) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, -1) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 2) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(-2, 0, 2) + position));
        }
        
        /// <summary>
        /// Creates a simple line that uses the default values for LineSketchObject.
        /// Draws a square with a single line.
        /// </summary>
        /// <param name="position"></param>
        private void CreateFrameWithOneLine(Vector3 position)
        {
            //Create a LineSketchObject
            LineSketchObject lineSketchObject = Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            
            //Adds a new object to the sketch world root. The sketch object is deleted when undoing this command
            Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(lineSketchObject, _sketchWorld));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 2) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 2) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 0) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
        }
        
        /// <summary>
        /// Draws a square with four individual lines that use the default values for LineSketchObject.
        /// All four lines get grouped together and form one SketchObjectGroup.
        /// </summary>
        /// <param name="position"></param>
        private void CreateFrameWithGroupedLines(Vector3 position)
        {
            //Using another CommandInvoker to prevent that undo actions collide.
            //The only action that we want to be added to the command stack is the adding of the group to the
            //SketchWorld instance
            //By creating the line objects with another invoker, their creation won't be tracked by the invoker we
            //perform the undo/redo actions on
            CommandInvoker localInvoker = new CommandInvoker();
            
            //Create a new SketchObjectGroup
            //A Group can hold multiple SketchObjects, such as lines
            SketchObjectGroup sketchObjectGroup = Instantiate(defaults.SketchObjectGroupPrefab).GetComponent<SketchObjectGroup>();
            
            //Create the first LineSketchObject...
            LineSketchObject lineSketchObject = Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            //...and add it to the SketchObject Group
            localInvoker.ExecuteCommand(new AddToGroupCommand(sketchObjectGroup, lineSketchObject));
            //Drawing the points, as usual
            localInvoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
            localInvoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 2) + position));
            
            //Create the second LineSketchObject, and add it to the group, just like the first one
            lineSketchObject = Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            localInvoker.ExecuteCommand(new AddToGroupCommand(sketchObjectGroup, lineSketchObject));
            localInvoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 2) + position));
            localInvoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 2) + position));
            
            //Create and add the third LineSketchObject
            lineSketchObject = Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            localInvoker.ExecuteCommand(new AddToGroupCommand(sketchObjectGroup, lineSketchObject));
            localInvoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 2) + position));
            localInvoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 0) + position));
            
            //And finally create and add the fourth LineSketchObject
            lineSketchObject = Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            localInvoker.ExecuteCommand(new AddToGroupCommand(sketchObjectGroup, lineSketchObject));
            localInvoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 0) + position));
            localInvoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
            
            //Adding the completed group with all its SketchObjects to the command list.
            //This is the only action that lands on the command stack of the invoker we perform undo/redo on.
            //Undoing this command removes all structures within the group.
            Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(sketchObjectGroup, _sketchWorld));
        }

        /// <summary>
        /// Removes a given amount of the latest actions invoked on Invoker
        /// </summary>
        /// <param name="amount"></param>
        public void UndoActions(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                try
                {
                    Invoker.Undo();
                }
                catch (ArgumentException e)
                {
                    //The error handled here happens when there are only two points in a LineSketchObject left,
                    //and the last action was a redo.
                    //The issue is created by the necessity of 3 existing waypoints for re-rendering a LineSketchObject.

                    //This is necessary because of an issue within the modeling kernel
                    //Please note that this won't resolve all problems
                    //(such as losing the second waypoint)
                    Invoker.Redo();
                    Invoker.Undo();
                    Invoker.Undo();
                }
            }
        }
        
        /// <summary>
        /// Reverses the removal of a given amount of actions invoked on Invoker
        /// </summary>
        /// <param name="amount"></param>
        public void RedoActions(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Invoker.Redo();
            }
        }
    }
}