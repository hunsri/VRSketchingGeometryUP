using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.Meshing;
using VRSketchingGeometry.Serialization;
using VRSketchingGeometry.SketchObjectManagement;

namespace VRpen.Scripts.Examples
{
    public class BrushExample : MonoBehaviour
    {
        //Various custom brushes that will be defined and used in this example
        private LineBrush _minimalisticBrush;
        private LineBrush _fineBrush;
        private LineBrush _roughBrush;
        private LineBrush _bigBrush;
        
        //The LineSketchObject which will form the reference for all brushes
        private static LineSketchObject _brushReference;
        
        //Holds the amount of lines that have been drawn already
        private int _linesDrawn = 0;

        //A Default holds samples for all elements required to draw, such as the LineSketchObjectPrefab 
        [SerializeField]
        private DefaultReferences defaults;
        
        //A material that can be used to change the appearance of the drawn lines
        [SerializeField]
        private Material customMaterial;
        
        //Many commands require a SketchWorld to be present
        private SketchWorld _sketchWorld;

        //Creates a CommandInvoker that is necessary to execute commands.
        private static readonly CommandInvoker Invoker = new CommandInvoker();
        
        void Start()
        {
            //Creating the LineSketchObject that will act as the default reference for new brushes.
            //Deactivation of the reference will prevent it from rendering in the scene itself
            _brushReference = Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            _brushReference.name = "BrushReferenceObject";
            _brushReference.gameObject.SetActive(false);
            
            //Create a SketchWorld, many commands require a SketchWorld to be present
            _sketchWorld = Instantiate(defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
            
            //Defining the properties of the various brushes
            _minimalisticBrush = CreateLineBrush(3, 1f, 2);
            _fineBrush = CreateLineBrush(32, 1f, 64);
            _roughBrush = CreateLineBrush(8, 1f, 8); ;
            _bigBrush = CreateLineBrush(32, 4f, 32);
            
            //Drawing the lines and applying colors to them
            ChangeLineMaterialColorTo(Color.red, DrawLineWithBrush(_minimalisticBrush));
            ChangeLineMaterialColorTo(Color.green, DrawLineWithBrush(_roughBrush));
            ChangeLineMaterialColorTo(Color.blue, DrawLineWithBrush(_fineBrush));
            
            //Drawing a line and applying a custom material to it
            ChangeLineMaterialTo(customMaterial, DrawLineWithBrush(_bigBrush));
        }

        /// <summary>
        /// Creates a LineBrush that can be used to modify the appearance of drawn lines
        /// </summary>
        private LineBrush CreateLineBrush(int resolution, float scale, int interpolationSteps)
        {
            //A LineSketchObject is necessary to get the default values for the brush.
            //Initialising the brush by obtaining a brush object from from the LineSketchObject. 
            LineBrush ret = _brushReference.GetBrush() as LineBrush;

            //Defining the values of the new brush
            ret.CrossSectionVertices = CircularCrossSection.GenerateVertices(resolution, scale);
            ret.CrossSectionNormals = CircularCrossSection.GenerateVertices(resolution);
            ret.InterpolationSteps = interpolationSteps;
            
            return ret;
        }
        
        /// <summary>
        /// Creates a LineSketchObject and applies a given brush to it.
        /// The modified LineSketchObject gets placed into the scene.
        /// The given LineBrush object defines the modification.
        /// </summary>
        /// <param name="lineBrush"></param>
        /// <returns></returns>
        private LineSketchObject DrawLineWithBrush(LineBrush lineBrush)
        {
            //Create a LineSketchObject
            LineSketchObject lineSketchObject =
                Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            
            //Setting the properties of the new LineSketchObject with the given LineBrush
            Invoker.ExecuteCommand(new SetBrushCommand(lineSketchObject, lineBrush));
            
            //This can also be done without the Invoker.
            //In that case the change of a brush won't be affected by undo commands.
            //lineSketchObject.SetBrush(lineBrush);
            
            DrawLine(lineSketchObject);
            
            return lineSketchObject;
        }
        
        /// <summary>
        /// Creates a LineSketchObject and puts it into the scene.
        /// </summary>
        /// <param name="lineSketchObject"></param>
        private void DrawLine(LineSketchObject lineSketchObject)
        {
            //Defining the position of the new line
            int offset = _linesDrawn * 2;
            Vector3 position = new Vector3(offset, 0, 0);
            
            //Attaching the new LineSketchObject to the SketchWorld
            Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(lineSketchObject, _sketchWorld));
            //Drawing the actual line
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, -2) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0.2f, -1) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, -0.2f, 1) + position));
            Invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 2) + position));
            
            _linesDrawn += 1;
        }
        
        /// <summary>
        /// Changes the material of a given LineSketchObject.
        /// </summary>
        /// <param name="material"></param>
        /// <param name="lineSketchObject"></param>
        private static void ChangeLineMaterialTo(Material material, LineSketchObject lineSketchObject)
        {
            lineSketchObject.GetComponent<Renderer>().material = material;
        }
        
        /// <summary>
        /// Changes the color of the material of a given LineSketchObject.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="lineSketchObject"></param>
        private static void ChangeLineMaterialColorTo(Color color, LineSketchObject lineSketchObject)
        {
            lineSketchObject.GetComponent<Renderer>().material.color = color;
        }
        
        /// <summary>
        /// Allows the creation and insertion of a line with defined properties into the scene.
        /// </summary>
        /// <param name="resolution"></param>
        /// <param name="scale"></param>
        /// <param name="interpolationSteps"></param>
        /// <param name="color"></param>
        public void CreateCustomLineWithColor(int resolution, float scale, int interpolationSteps, Color color)
        {
            LineSketchObject lineSketchObject =
                Instantiate(defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            
            //LineSketchObjects can also be modified directly without using predefined brushes.
            lineSketchObject.SetLineCrossSection
            (
                CircularCrossSection.GenerateVertices(resolution, scale),
                CircularCrossSection.GenerateVertices(resolution),
                0.2f //This is the default diameter value.
            );
            lineSketchObject.SetInterpolationSteps(interpolationSteps);
            
            //This would be the equivalent action with using a brush
            //ChangeLineMaterialColorTo(color, AddLineToScene(CreateLineBrush(resolution, scale, interpolationSteps)));
            
            ChangeLineMaterialColorTo(color, lineSketchObject);
            DrawLine(lineSketchObject);
        }
        
    }
}