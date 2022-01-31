using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using StationEdit.Things.Structures;
using System.Xml.Linq;

namespace StationEdit
{
    internal class StationStructureFactory
    {
        public static StationStructure MakeStructure(string prefabName, XElement thing)
        {
            StationStructure retval;
            switch (prefabName)
            {

                case "StructureCompositeFloorGrating":
                case "StructureCompositeFloorGrating2":
                case "StructureCompositeFloorGrating3":
                case "StructureCompositeFloorGrating4":
                    retval = new StructureFloorGrating(prefabName, thing);
                    break;

                case "StructureCompositeWall":
                case "StructureCompositeWall02":
                case "StructureCompositeWall03":
                case "StructureCompositeWall04":
                case "StructureCompositeRollCover":
                case "StructureWallFlat":
                    retval = new StructureCompositeWall(prefabName, thing);
                    break;

                case "StructureCompositeWindow":
                case "StructureWindowShutter":
                    retval = new StructureCompositeWindow(prefabName, thing);
                    break;

                case "StructureElevatorShaft":
                case "StructureElevatorLevelFront":
                    retval = new StructureElevator(prefabName, thing);
                    break;

                case "StructureWallPadding":
                case "StructureWallPaddingThin":
                case "StructureWallPaddingLightFitting":
                case "StructureWallPaddedThinNoBorder":
                    retval = new StructurePaddedWall(prefabName, thing);
                    break;

                case "StructureWallPaddedWindow":
                case "StructureWallPaddedWindowThin":
                    retval = new StructurePaddedWindow(prefabName, thing);
                    break;

                case "StructureAirlock":
                case "StructureAirlockGate":
                case "StructureBlastDoor":
                    retval = new StructureAirlock(prefabName, thing);
                    break;

                case "StructureCompositeDoor":
                case "StructureGlassDoor":
                case "StructureInteriorDoorGlass":
                case "StructureInteriorDoorTriangle":
                    retval = new StructureCompositeDoor(prefabName, thing);
                    break;


                case "StructureCompositeCladdingPanel":
                    retval = new StructureCladding(prefabName, thing);
                    break;

                case "StructureFrameIron":
                    retval = new StructureFrameIron(prefabName, thing);
                    break;

                case "StructureFrame":
                    retval = new StructureFrameSteel(prefabName, thing);
                    break;

                case "StructureLadder":
                    retval = new StructureLadder(prefabName, thing);
                    break;

                case "StructureWallIron":
                case "StructureWallIron02":
                case "StructureWallIron03":
                case "StructureWallIron04":
                    retval = new StructureIronWall(prefabName, thing);
                    break;

                case "StructureCompositeWindowIron":
                    retval = new StructureIronWindow(prefabName, thing);
                    break;

                case "StructureWallPlating":
                case "StructureWallLargePanel":
                case "StructureWallLargePanelArrow":
                case "StructureWallSmallPanelsArrow":
                case "StructureWallSmallPanelsTwoTone":
                case "StructureWallSmallPanelsMonoChrome":
                case "StructureWallSmallPanelsAndHatch":
                    retval = new StructureWallPlating(prefabName, thing);
                    break;

                case "StructureTankBig":
                case "StructureLiquidTankBigInsulated":
                    retval = new StructureTankBig(prefabName, thing);
                    break;

                case "StructureTankSmall":
                case "StructureTankSmallInsulated":
                    retval = new StructureTankSmall(prefabName, thing);
                    break;

                /*case "StructureGasTankStorage":
                    retval = new StructureGasTankStorage(prefabName, thing);
                    break;*/

                default:
                    //Debug.WriteLine("Structure prefab not handled: " + prefabName);
                    retval = new StationStructure(prefabName, thing);
                    break;
            }
            return retval;
        }
    }
}
