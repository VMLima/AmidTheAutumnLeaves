/*
 * THIS IS A DESCRIPTION OF HOW TO MAKE AN EFFECT
 *  !!!!!ONLY HAVE PREFABS MADE IN THE FOLLOWING MANNER IN THIS FOLDER!!!!
 * 
 * CREATE EMPTY COMPONENTS
 * 1. Create a prefab element
 * 2. Create a C# script.
 * 
 * CONNECTING THINGS
 * 1. select the C# script you made and drag it onto the prefab.  It should now be an element in it's inspector.
 * 
 * CAN START FILLING OUT DATA
 * 1. in Prefab inspector, fill out script fields.
 * 2. in Prefab, can OPTIONALLY create a UI element for the status bar.
 * 3. in the C# script MUST setup the effects.
 *      delete everything in the { } in that script.  Make it inherit StatusScript instead of MonoBehavior
 *       create a function...
 *        public override void effectOverride()
 *          this function will be called X seconds while the status condition is active.  Whatever you want to happen then.  Put here.
 *          
 *        OPTIONAL:
 *          public override void effectStatckOverride()
 *              this function will be called if another instance of the status condition is applied.
 *              ATM the other status condition will have no effect.  
 *              The only thing that will happen when it is applied again while still is active..
 *              is what happens in effectStackOverride()
 *              If you do not override this, by default the duration of the condition is just reset.
 *          public override void onStartOverride()
 *              this will trigger once as the effect starts.
 *          public override void onStopOverride()
 *              this will trigger once as the effect stops.
 * 
 */