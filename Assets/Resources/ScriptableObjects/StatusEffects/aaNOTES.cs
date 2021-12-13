/*
 * THIS IS A DESCRIPTION OF HOW TO MAKE STATUS EFFECTS
 * 
 * CREATE EMPTY COMPONENTS
 * 1. Create a scriptable object within this folder
 * 2. Create a prefab element
 * 3. Create a C# script.
 * 
 * CAN START CONNECTING THINGS
 * 1. select the scriptable object.
 *      in the inspector there is an element called UIObject.
 *      drag the prefab into that spot.
 * 2. select the C# script you made and drag it onto the prefab.  It should now be an element in it's inspector.
 * 
 * CAN START FILLING OUT DATA
 * 1. fill out scriptable object fields.
 * 2. can create a UI element for the status bar in the prefab.
 * 3. can set up the effect of the condition in the C# script.
 *      delete everything in the { } in that script.  Make it inherit StatusScript instead of MonoBehavior
 *       create a function...
 *        public override void effectOverride()
 *          this function will be called every second while the status condition is active.  Whatever you want to happen there.  Put there.
 *          
 *        OPTIONAL:
 *          public override void effectStatckOverride()
 *              this function will be called if another instance of the status condition is applied.  
 *              If you do not override this, by default the duration is just reset.
 *          public override void onStartOverride()
 *              this will trigger once as the effect starts.
 *          public override void onStopOverride()
 *              this will trigger once as the effect stops.
 * 
 */