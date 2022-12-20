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
        List<IMyReactor> AllReactors = new List<IMyReactor>();
        List<IMyBatteryBlock> AllBatterys = new List<IMyBatteryBlock>();
        List<IMyThrust> AllThruster = new List<IMyThrust>();
        List<IMyRefinery> AllRefinerys = new List<IMyRefinery>();
        List<IMyAssembler> AllAssembler = new List<IMyAssembler>();
        List<IMyAirVent> AllAirVents = new List<IMyAirVent>();
        List<IMyRadioAntenna> AllAntennas = new List<IMyRadioAntenna>();
        List<IMyMotorAdvancedRotor> AllRotors = new List<IMyMotorAdvancedRotor>();
        List<IMyArtificialMassBlock> AllMass = new List<IMyArtificialMassBlock>();
        List<IMyBeacon> AllBeacons = new List<IMyBeacon>();
        List<IMyShipDrill> AllDrills = new List<IMyShipDrill>();
        List<IMyTerminalBlock> AllWeapons = new List<IMyTerminalBlock>();
        List<IMyTerminalBlock> AllLights = new List<IMyTerminalBlock>();


        #endregion




        #region Startup
        public Program()
        {

        }

        public void Findblocks()
        {
            List<IMyTerminalBlock> allblocks = new List<IMyTerminalBlock>();
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
                        IMyMotorAdvancedRotor AddBlock = (IMyMotorAdvancedRotor)Block;
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



                    hier //alle  Blöcke hinzufügen
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
