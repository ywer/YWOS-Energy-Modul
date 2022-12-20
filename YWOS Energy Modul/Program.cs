using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {




        #region Private
        List<IMyTerminalBlock> allblocks = new List<IMyTerminalBlock>();
        List<IMyReactor> AllReactors = new List<IMyReactor>();
        List<IMyBatteryBlock> AllBatterys = new List<IMyBatteryBlock>();
        List<IMyThrust> AllThruster = new List<IMyThrust>();
        List<IMyRefinery> AllRefinerys = new List<IMyRefinery>();
        List<IMyAssembler> AllAssembler = new List<IMyAssembler>();
        List<IMyAirVent> AllAirVents = new List<IMyAirVent>();
        List<IMyRadioAntenna> AllAntennas = new List<IMyRadioAntenna>();
        List<IMyTerminalBlock> AllRotors = new List<IMyTerminalBlock>();
        List<IMyArtificialMassBlock> AllMass = new List<IMyArtificialMassBlock>();
        List<IMyBeacon> AllBeacons = new List<IMyBeacon>();
        List<IMyTerminalBlock> AllWeapons = new List<IMyTerminalBlock>();
        List<IMyTerminalBlock> AllLights = new List<IMyTerminalBlock>();
        List<IMyJumpDrive> AllJumpDrives = new List<IMyJumpDrive>();
        List<IMyLaserAntenna> AllLaserAntennas = new List<IMyLaserAntenna>();
        List<IMyOreDetector> AllOreDetector = new List<IMyOreDetector>();
        List<IMyOxygenFarm> AllOxygenFarms = new List<IMyOxygenFarm>();
        List<IMyGasGenerator> AllOxygenGenerators = new List<IMyGasGenerator>();
        List<IMyPistonBase> AllPistons = new List<IMyPistonBase>();
        List<IMyProgrammableBlock> AllProg = new List<IMyProgrammableBlock>();
        List<IMyProjector> AllProjectors = new List<IMyProjector>();
        List<IMyRemoteControl> AllRemotes = new List<IMyRemoteControl>();
        List<IMySensorBlock> AllSensors = new List<IMySensorBlock>();
        List<IMyTerminalBlock> AllGravityGenerators = new List<IMyTerminalBlock>();
        List<IMyShipGrinder> AllGrinder = new List<IMyShipGrinder>();
        List<IMyShipWelder> AllWelder = new List<IMyShipWelder>();
        List<IMyShipDrill> AllDrills = new List<IMyShipDrill>();
        List<IMyCameraBlock> AllCameras = new List<IMyCameraBlock>();
        List<IMyButtonPanel> AllButtonPanels = new List<IMyButtonPanel>();
        List<IMyShipController> AllShipController = new List<IMyShipController>();
        List<IMyCollector> AllCollectors = new List<IMyCollector>();
        List<IMyShipConnector> AllConnectors = new List<IMyShipConnector>();
        List<IMyConveyorSorter> AllSorter = new List<IMyConveyorSorter>();
        List<IMyDoor> ALlDoors = new List<IMyDoor>();
        List<IMyGyro> AllGyros = new List<IMyGyro>();
        List<IMyLandingGear> AllGears = new List<IMyLandingGear>();
        List<IMyMedicalRoom> AllMedbays = new List<IMyMedicalRoom>();
        List<IMyShipMergeBlock> AllMergeBlocks = new List<IMyShipMergeBlock>();
        List<IMyGasTank> AllOxygentanks = new List<IMyGasTank>();
        List<IMySpaceBall> AllSpaceballs = new List<IMySpaceBall>();
        List<IMyTextPanel> AllLCD = new List<IMyTextPanel>();
        List<IMyTimerBlock> AllTimer = new List<IMyTimerBlock>();
        List<IMyWarhead> AllWarheads = new List<IMyWarhead>();

        #endregion




        #region Startup
        public Program()
        {
            Me.CustomName = "M1337";


        }

        public void Findblocks()
        {
            Runtime.UpdateFrequency = UpdateFrequency.None;
            allblocks.Clear();


            GridTerminalSystem.GetBlocks(allblocks);
            foreach (IMyTerminalBlock Block in allblocks)
            {
                if(Block.IsSameConstructAs(Me))
                {
                    if(Block is IMyReactor)
                    {
                        IMyReactor Reactor = (IMyReactor)Block;
                        AllReactors.Add(Reactor);
                    }
                    if(Block is IMyBatteryBlock)
                    {
                        IMyBatteryBlock Battery = (IMyBatteryBlock)Block;
                        AllBatterys.Add(Battery);
                    }
                    if (Block is IMyThrust)
                    {
                        IMyThrust AddBlock = (IMyThrust)Block;
                        AllThruster.Add(AddBlock);
                    }
                    if (Block is IMyRefinery)
                    {
                        IMyRefinery AddBlock = (IMyRefinery)Block;
                        AllRefinerys.Add(AddBlock);
                    }
                    if (Block is IMyAssembler)
                    {
                        IMyAssembler AddBlock = (IMyAssembler)Block;
                        AllAssembler.Add(AddBlock);
                    }
                    if (Block is IMyAirVent)
                    {
                        IMyAirVent AddBlock = (IMyAirVent)Block;
                        AllAirVents.Add(AddBlock);
                    }
                    if (Block is IMyRadioAntenna)
                    {
                        IMyRadioAntenna AddBlock = (IMyRadioAntenna)Block;
                        AllAntennas.Add(AddBlock);
                    }
                    if (Block is IMyMotorAdvancedRotor)
                    {
                        IMyTerminalBlock AddBlock = (IMyTerminalBlock)Block;
                        AllRotors.Add(AddBlock);
                    }
                    if (Block is IMyMotorRotor)
                    {
                        IMyTerminalBlock AddBlock = (IMyTerminalBlock)Block;
                        AllRotors.Add(AddBlock);
                    }
                    if (Block is IMyArtificialMassBlock)
                    {
                        IMyArtificialMassBlock AddBlock = (IMyArtificialMassBlock)Block;
                        AllMass.Add(AddBlock);
                    }
                    if (Block is IMyBeacon)
                    {
                        IMyBeacon AddBlock = (IMyBeacon)Block;
                        AllBeacons.Add(AddBlock);
                    }
                    if (Block is IMyShipDrill)
                    {
                        IMyShipDrill AddBlock = (IMyShipDrill)Block;
                        AllDrills.Add(AddBlock);
                    }
                    if(Block is IMySmallGatlingGun)
                    {
                        IMySmallGatlingGun AddBlock = (IMySmallGatlingGun)Block;
                        AllWeapons.Add(AddBlock);
                    }
                    if (Block is IMyLargeGatlingTurret)
                    {
                        IMyLargeGatlingTurret AddBlock = (IMyLargeGatlingTurret)Block;
                        AllWeapons.Add(AddBlock);
                    }
                    if (Block is IMySmallMissileLauncher)
                    {
                        IMySmallMissileLauncher AddBlock = (IMySmallMissileLauncher)Block;
                        AllWeapons.Add(AddBlock);
                    }
                    if (Block is IMyLargeGatlingTurret)
                    {
                        IMyLargeGatlingTurret AddBlock = (IMyLargeGatlingTurret)Block;
                        AllWeapons.Add(AddBlock);
                    }
                    if (Block is IMyLargeInteriorTurret)
                    {
                        IMyLargeInteriorTurret AddBlock = (IMyLargeInteriorTurret)Block;
                        AllWeapons.Add(AddBlock);
                    }
                    if (Block is IMySmallMissileLauncherReload)
                    {
                        IMySmallMissileLauncherReload AddBlock = (IMySmallMissileLauncherReload)Block;
                        AllWeapons.Add(AddBlock);
                    }
                    if (Block is IMyInteriorLight)
                    {
                        IMyInteriorLight AddBlock = (IMyInteriorLight)Block;
                        AllLights.Add(AddBlock);
                    }
                    if (Block is IMyInteriorLight)
                    {
                        IMyInteriorLight AddBlock = (IMyInteriorLight)Block;
                        AllLights.Add(AddBlock);
                    }
                    if (Block is IMyReflectorLight)
                    {
                        IMyReflectorLight AddBlock = (IMyReflectorLight)Block;
                        AllLights.Add(AddBlock);
                    }
                    if (Block is IMyJumpDrive)
                    {
                        IMyJumpDrive AddBlock = (IMyJumpDrive)Block;
                        AllJumpDrives.Add(AddBlock);
                    }
                    if (Block is IMyLaserAntenna)
                    {
                        IMyLaserAntenna AddBlock = (IMyLaserAntenna)Block;
                        AllLaserAntennas.Add(AddBlock);
                    }
                    if (Block is IMyOreDetector)
                    {
                        IMyOreDetector AddBlock = (IMyOreDetector)Block;
                        AllOreDetector.Add(AddBlock);
                    }
                    if (Block is IMyOxygenFarm)
                    {
                        IMyOxygenFarm AddBlock = (IMyOxygenFarm)Block;
                        AllOxygenFarms.Add(AddBlock);
                    }
                    if (Block is IMyGasGenerator)
                    {
                        IMyGasGenerator AddBlock = (IMyGasGenerator)Block;
                        AllOxygenGenerators.Add(AddBlock);
                    }
                    if (Block is IMyPistonBase)
                    {
                        IMyPistonBase AddBlock = (IMyPistonBase)Block;
                        AllPistons.Add(AddBlock);
                    }
                    if (Block is IMyProgrammableBlock)
                    {
                        IMyProgrammableBlock AddBlock = (IMyProgrammableBlock)Block;
                        AllProg.Add(AddBlock);
                    }
                    if (Block is IMyProjector)
                    {
                        IMyProjector AddBlock = (IMyProjector)Block;
                        AllProjectors.Add(AddBlock);
                    }
                    if (Block is IMyRemoteControl)
                    {
                        IMyRemoteControl AddBlock = (IMyRemoteControl)Block;
                        AllRemotes.Add(AddBlock);
                    }
                    if (Block is IMySensorBlock)
                    {
                        IMySensorBlock AddBlock = (IMySensorBlock)Block;
                        AllSensors.Add(AddBlock);
                    }
                    if (Block is IMyGravityGenerator)
                    {
                        IMyGravityGenerator AddBlock = (IMyGravityGenerator)Block;
                        AllGravityGenerators.Add(AddBlock);
                    }
                    if (Block is IMyGravityGeneratorSphere)
                    {
                        IMyGravityGeneratorSphere AddBlock = (IMyGravityGeneratorSphere)Block;
                        AllGravityGenerators.Add(AddBlock);
                    }
                    if (Block is IMyShipGrinder)
                    {
                        IMyShipGrinder AddBlock = (IMyShipGrinder)Block;
                        AllGrinder.Add(AddBlock);
                    }
                    if (Block is IMyShipWelder)
                    {
                        IMyShipWelder AddBlock = (IMyShipWelder)Block;
                        AllWelder.Add(AddBlock);
                    }
                    if (Block is IMyCameraBlock)
                    {
                        IMyCameraBlock AddBlock = (IMyCameraBlock)Block;
                        AllCameras.Add(AddBlock);
                    }
                    if (Block is IMyButtonPanel)
                    {
                        IMyButtonPanel AddBlock = (IMyButtonPanel)Block;
                        AllButtonPanels.Add(AddBlock);
                    }
                    if (Block is IMyShipController)
                    {
                        IMyShipController AddBlock = (IMyShipController)Block;
                        AllShipController.Add(AddBlock);
                    }
                    if (Block is IMyCollector)
                    {
                        IMyCollector AddBlock = (IMyCollector)Block;
                        AllCollectors.Add(AddBlock);
                    }
                    if (Block is IMyShipConnector)
                    {
                        IMyShipConnector AddBlock = (IMyShipConnector)Block;
                        AllConnectors.Add(AddBlock);
                    }
                    if (Block is IMyConveyorSorter)
                    {
                        IMyConveyorSorter AddBlock = (IMyConveyorSorter)Block;
                        AllSorter.Add(AddBlock);
                    }
                    if (Block is IMyDoor)
                    {
                        IMyDoor AddBlock = (IMyDoor)Block;
                        ALlDoors.Add(AddBlock);
                    }
                    if (Block is IMyGyro)
                    {
                        IMyGyro AddBlock = (IMyGyro)Block;
                        AllGyros.Add(AddBlock);
                    }
                    if (Block is IMyLandingGear)
                    {
                        IMyLandingGear AddBlock = (IMyLandingGear)Block;
                        AllGears.Add(AddBlock);
                    }
                    if (Block is IMyMedicalRoom)
                    {
                        IMyMedicalRoom AddBlock = (IMyMedicalRoom)Block;
                        AllMedbays.Add(AddBlock);
                    }
                    if (Block is IMyShipMergeBlock)
                    {
                        IMyShipMergeBlock AddBlock = (IMyShipMergeBlock)Block;
                        AllMergeBlocks.Add(AddBlock);
                    }
                    if (Block is IMyGasTank)
                    {
                        IMyGasTank AddBlock = (IMyGasTank)Block;
                        AllOxygentanks.Add(AddBlock);
                    }
                    if (Block is IMySpaceBall)
                    {
                        IMySpaceBall AddBlock = (IMySpaceBall)Block;
                        AllSpaceballs.Add(AddBlock);
                    }
                    if (Block is IMyTextPanel)
                    {
                        IMyTextPanel AddBlock = (IMyTextPanel)Block;
                        AllLCD.Add(AddBlock);
                    }
                    if (Block is IMyTimerBlock)
                    {
                        IMyTimerBlock AddBlock = (IMyTimerBlock)Block;
                        AllTimer.Add(AddBlock);
                    }
                    if (Block is IMyWarhead)
                    {
                        IMyWarhead AddBlock = (IMyWarhead)Block;
                        AllWarheads.Add(AddBlock);
                    }



                    hier //alle  Blöcke hinzufügen





                        Runtime.UpdateFrequency = UpdateFrequency.Update100;
                    return;
                }


            }
        }

        #endregion

        #region Save
        public void Save()
        {

        }
        #endregion
        #region main
        public void Main(string argument, UpdateType updateSource)
        {




        }

        #endregion

        #region Energydata


        #endregion

    }
}
