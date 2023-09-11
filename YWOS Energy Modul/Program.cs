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
        //Code by Ywer

        //INFO:ModulName = Energy;
        //SETTING:Blala = 20:10|20|30;
        //INFO:EnergyUse = 20;
        //WARNING:Energy = Low Battery!:1;


        #region Settings
        int MaxTicks = 10;
        string ModuleName = "Energy";
        int MaxLoggerRows = 200;//Set this too high will cause lag
        string MainLCDName = "MainLCD"; //LCD here used to Temp Logging
        #endregion




        #region Private
        List<IMyTerminalBlock> allblocks = new List<IMyTerminalBlock>();
        List<IMyReactor> AllReactors = new List<IMyReactor>();
        List<IMyBatteryBlock> AllBatterys = new List<IMyBatteryBlock>();
        List<IMySolarPanel> AllSolarPanels = new List<IMySolarPanel>();
        List<IMyThrust> AllThruster = new List<IMyThrust>();
        List<IMyRefinery> AllRefinerys = new List<IMyRefinery>();
        List<IMyAssembler> AllAssembler = new List<IMyAssembler>();
        List<IMyAirVent> AllAirVents = new List<IMyAirVent>();
        List<IMyRadioAntenna> AllAntennas = new List<IMyRadioAntenna>();
        List<IMyMotorStator> AllRotors = new List<IMyMotorStator>();
        List<IMyArtificialMassBlock> AllMass = new List<IMyArtificialMassBlock>();
        List<IMyBeacon> AllBeacons = new List<IMyBeacon>();
        List<IMyLargeInteriorTurret> AllIntTurrets = new List<IMyLargeInteriorTurret>();
        List<IMyLargeMissileTurret> AllLargeMissleturrets = new List<IMyLargeMissileTurret>();
        List<IMySmallMissileLauncher> AllSmallMissleturrets = new List<IMySmallMissileLauncher>();
        List<IMySmallMissileLauncherReload> AllSmallRocketLauncher = new List<IMySmallMissileLauncherReload>();
        List<IMyLargeGatlingTurret> AllLargeGatlingTurrets = new List<IMyLargeGatlingTurret>();
        List<IMySmallGatlingGun> AllSmallGatlinTurrets = new List<IMySmallGatlingGun>();
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
        List<IMyGravityGenerator> AllGravityGenerators = new List<IMyGravityGenerator>();
        List<IMyGravityGeneratorSphere> AllGravityGeneratorsSphere = new List<IMyGravityGeneratorSphere>();
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
        List<CDValues> CDData = new List<CDValues>();
        int WMessageID = 0;


        class CDValues
        {
            public string Tag { get; set; }

            public string Name { get; set; }

            public string Value { get; set; }

            public string Value2 { get; set; }
        }

        #endregion




        #region Startup

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Once;
            Me.CustomName = "M1337";
            Findblocks();

            ReadFromCustomData();



            Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }

        IMyTextPanel DebugLCD;
        public void Findblocks()
        {
            allblocks.Clear();
            AllReactors.Clear();
            AllBatterys.Clear();
            AllSolarPanels.Clear();
            AllThruster.Clear();
            AllRefinerys.Clear();
            AllAssembler.Clear();
            AllAirVents.Clear();
            AllAntennas.Clear();
            AllRotors.Clear();
            AllMass.Clear();
            AllBeacons.Clear();
            AllIntTurrets.Clear();
            AllLargeMissleturrets.Clear();
            AllSmallMissleturrets.Clear();
            AllSmallRocketLauncher.Clear();
            AllLargeGatlingTurrets.Clear();
            AllSmallGatlinTurrets.Clear();
            AllLights.Clear();
            AllJumpDrives.Clear();
            AllLaserAntennas.Clear();
            AllOreDetector.Clear();
            AllOxygenFarms.Clear();
            AllOxygenGenerators.Clear();
            AllPistons.Clear();
            AllProg.Clear();
            AllProjectors.Clear();
            AllRemotes.Clear();
            AllSensors.Clear();
            AllGravityGenerators.Clear();
            AllGravityGeneratorsSphere.Clear();
            AllGrinder.Clear();
            AllWelder.Clear();
            AllDrills.Clear();
            AllCameras.Clear();
            AllButtonPanels.Clear();
            AllShipController.Clear();
            AllCollectors.Clear();
            AllConnectors.Clear();
            AllSorter.Clear();
            ALlDoors.Clear();
            AllGyros.Clear();
            AllGears.Clear();
            AllMedbays.Clear();
            AllMergeBlocks.Clear();
            AllOxygentanks.Clear();
            AllSpaceballs.Clear();
            AllLCD.Clear();
            AllTimer.Clear();
            AllWarheads.Clear();



            GridTerminalSystem.GetBlocks(allblocks);
            foreach (IMyTerminalBlock Block in allblocks)
            {
                if (Block.IsSameConstructAs(Me))
                {
                    if (Block is IMyReactor)
                    {
                        IMyReactor Reactor = (IMyReactor)Block;
                        AllReactors.Add(Reactor);
                    }
                    if (Block is IMyBatteryBlock)
                    {
                        IMyBatteryBlock Battery = (IMyBatteryBlock)Block;
                        if (EnableEmergencyShutoff)
                        {
                            if (EmergengyBattery == null)
                            {
                                EmergengyBattery = Battery;
                                EmergengyBattery.CustomName = "EmergencyBattery Dont Toutch me!";
                            }
                        }
                        AllBatterys.Add(Battery);
                    }
                    if (Block is IMySolarPanel)
                    {
                        IMySolarPanel AddBlock = (IMySolarPanel)Block;
                        AllSolarPanels.Add(AddBlock);
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
                        IMyMotorStator AddBlock = (IMyMotorStator)Block;
                        AllRotors.Add(AddBlock);
                    }
                    if (Block is IMyMotorRotor)
                    {
                        IMyMotorStator AddBlock = (IMyMotorStator)Block;
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
                    if (Block is IMySmallGatlingGun)
                    {
                        IMySmallGatlingGun AddBlock = (IMySmallGatlingGun)Block;
                        AllSmallGatlinTurrets.Add(AddBlock);
                    }
                    if (Block is IMyLargeGatlingTurret)
                    {
                        IMyLargeGatlingTurret AddBlock = (IMyLargeGatlingTurret)Block;
                        AllLargeGatlingTurrets.Add(AddBlock);
                    }
                    if (Block is IMySmallMissileLauncher)
                    {
                        IMySmallMissileLauncher AddBlock = (IMySmallMissileLauncher)Block;
                        AllSmallMissleturrets.Add(AddBlock);
                    }
                    if (Block is IMyLargeMissileTurret)
                    {
                        IMyLargeMissileTurret AddBlock = (IMyLargeMissileTurret)Block;
                        AllLargeMissleturrets.Add(AddBlock);
                    }
                    if (Block is IMyLargeInteriorTurret)
                    {
                        IMyLargeInteriorTurret AddBlock = (IMyLargeInteriorTurret)Block;
                        AllIntTurrets.Add(AddBlock);
                    }
                    if (Block is IMySmallMissileLauncherReload)
                    {
                        IMySmallMissileLauncherReload AddBlock = (IMySmallMissileLauncherReload)Block;
                        AllSmallRocketLauncher.Add(AddBlock);
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
                        AllGravityGeneratorsSphere.Add(AddBlock);
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
                        if(Block.CustomName.Contains(MainLCDName))
                        {
                            DebugLCD = (IMyTextPanel)Block;
                        }
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
                }


            }
            return;
        }


        #endregion

        #region CustomData
        public int ReturnIndexFromStoredCustomData(string SearchName, string SearchTag)
        {
            int C1 = 0;
            do
            {

                if (CDData[C1].Name == SearchName && CDData[C1].Tag == SearchTag)
                {

                    break;
                }

                C1++;

            } while (C1 < CDData.Count - 1);

            return C1;
        }


        public void SaveToStoredCustomData(string Name, string Tag, string Value1, string Value2)
        {
            int Index = -1;
            Index = ReturnIndexFromStoredCustomData(Name, Tag);
            if(Index != -1)
            {
                CDData[Index].Name = Name;
                CDData[Index].Tag = Tag;
                CDData[Index].Value = Value1;
                CDData[Index].Value2 = Value2;
            }
            else
            {
                CDData.Add(new CDValues { Name = Name, Tag = Tag, Value = Value1, Value2 = Value2 });

            }

            return;
        }

        public void RemoveFromStoredCustomData(string Name, string Tag)
        {
            int Index = -1;
            Index = ReturnIndexFromStoredCustomData(Name, Tag);
            if (Index != -1)
            {
                CDData.RemoveAt(Index);
            }
            return;
        }

        public void ChangeSetting(string Name, string Value , string ValueRange)
        {
            foreach (CDValues CDValue in CDData)
            {
                if (CDValue.Tag == "SETTING")
                {
                    if (CDValue.Name.Contains(Name))
                    {
                        CDValue.Value = Value;
                    }
                    if(ValueRange != null && ValueRange != "")
                    {
                        CDValue.Value2 = ValueRange;
                    }


                }



            }

            return;
        }


        public void ReadFromCustomData()
        {
            /*
INFO:ModulName = Energy:;
SETTING:UranSaverModeOverride = Off:On|Off;
SETTING:EmergencyModeOverride = Off:On|Off;
SETTING:PreEmergencyModeOverride = Off:On|Off;
SETTING:EmergencyModeSetting = Off:10|20|30|40|50|60|70|80|90|Off;
SETTING:UranSaveModeSetting = Off:10|20|30|40|50|60|70|80|90|Off;
INFO:ReactorRunning = Loading..:;
INFO:PowerUsed = Loading..:;
INFO:MaxPower = Loading..:;
INFO:SolarPanelsRunning = Loading..:;
INFO:SolarOutput = Loading..:;
INFO:BatteryCountRunning = Loading..:;
INFO:BatteryMaxLoad = Loading..:;
INFO:BatteryInput = Loading..:;
INFO:BatteryLoadPercent = Loading..:;
            */


            string CustomData = Me.CustomData;
            string CD = CustomData.Replace(" ", "");
            if (CD != "")
            {
                string[] Split = CD.Split(';');
                if (Split.Length > 1)
                {

                    //CDData.Clear();
                    foreach (string Data in Split)
                    {
                        string[] Split1 = Data.Split('=');
                        if (Split1.Length > 1)
                        {
 
                            string[] Split2 = Split1[0].Split(':');//vor =
                            string[] Split3 = Split1[1].Split(':');//nach =

                                    int Index = CDData.FindIndex(a => a.Name == Split2[1]);
                                    if (Index != -1)
                                    {
                                        if (Split3.Length > 1)
                                        {
                                            CDData[Index].Value = Split3[0];
                                            CDData[Index].Value2 = Split3[1];
                                        }else if(Split3.Length == 1)
                                        {
                                            CDData[Index].Value = Split3[0];
                                            CDData[Index].Value2 = null;
                                        }
                                        else
                                        {
                                            CDData[Index].Value = null;
                                            CDData[Index].Value2 = null;

                                        }



                                    }
                                    else
                                    {
                                        if (Split2[1].Contains("INFO"))
                                        {
                                            CDData.Add(new CDValues { Name = Split2[1], Tag = Split2[0], Value = Split3[0] });


                                        }
                                        else
                                        {

                                    if (Split3.Length > 1)
                                    {
                                        CDData.Add(new CDValues { Name = Split2[1], Tag = Split2[0], Value = Split3[0], Value2 = Split3[1] });
                                    }
                                    else if (Split3.Length == 1)
                                    {
                                        CDData.Add(new CDValues { Name = Split2[1], Tag = Split2[0], Value = Split3[0] });
                                    }
                                    else
                                    {
                                        CDData.Add(new CDValues { Name = Split2[1], Tag = Split2[0]});
                                    }
                                        }

                                    }


                            
                        }
                        else
                        {
                            //Echo("Wrong Data lenght!");
                          //  WriteToLog("Info:Wrong Data lenght");
                            //Me.CustomData = "";
                           // WriteNewCustomData();
                            //ReadFromCustomData();
                            return;
                        }
                    }

                    return;
                }
                else
                {
                    WriteNewCustomData();
                    ReadFromCustomData();
                }
            }
            else
            {
                WriteNewCustomData();
                ReadFromCustomData();
            }

            return;
        }

        public void WriteToCustomData()
        {
            if(CDData.Count > 0)
            {

                string Out = "";
                int i= 0;
                foreach(CDValues Value in CDData)
                {
                    if (Value.Value2 != null && Value.Value2 != "")
                    {

                        Out = Out + Value.Tag + ":" + Value.Name + "=" + Value.Value + ":" + Value.Value2 + ";";
                    }
                    else
                    {
                        Out = Out + Value.Tag + ":" + Value.Name + "=" + Value.Value + ";";
                    }
                    i++;
                }
                Me.CustomData = Out;

            }
            return;
        }

        public int AddWarning(string Text)
        {
            //WARNING:Energy = Low Battery!:1;

            CDData.Add(new CDValues { Name = ModuleName, Tag = "WARNING", Value = Text, Value2 = WMessageID.ToString() });
            WMessageID++;
            return WMessageID;
        }
        #endregion

        #region Save/Logging
        public void Save()
        {

        }

        public void WriteToLog(string Text)
        {
            if (DebugLCD != null)
            {
                string Data = DebugLCD.CustomData;
                string[] Lines = Data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string[] NewData = new string[Lines.Length + 2];
                DateTime TimeNow = System.DateTime.Now;
                if (Lines.Length >= MaxLoggerRows - 1)
                {
                    if (Lines.Length > 0)
                    {

                        Array.Copy(Lines, 1, NewData, 0, Lines.Length - 1);
                    }
                    NewData[MaxLoggerRows] = TimeNow + ":" + ModuleName + ":" + Text + Environment.NewLine;
                }
                else
                {
                    if (Lines.Length > 0)
                    {

                        Array.Copy(Lines, 0, NewData, 0, Lines.Length);
                    }
                    NewData[Lines.Length + 1] = TimeNow + ":" + ModuleName + ":" + Text + Environment.NewLine;
                }
                DebugLCD.CustomData = string.Join(Environment.NewLine, NewData);
            }
            else
            {
                Echo("no Logging! no Main LCD found!");
            }
            return;
        }

        #endregion


        #region main

        #region Script specific Values
        //edit per new  modul 
        double Version = 0.2; 
        bool PreemergencyMode = false;
        bool PreemergencyModeOverride = false;
        bool EmergencyMode = false;
        bool EmergencyModeOverride = false;
        bool UranSaveMode = false;
        bool UranSaveModeOverride = false;
        bool EnableEmergencyShutoff = false;
        int EmergencyModeSetting = -1;
        string EmergencyModeSettingRange;
        int UranSavingSetting = -1;
        string UranSavingSettingRange;
        IMyBatteryBlock EmergengyBattery;

        public void WriteNewCustomData()
        {
            /*
        int ReactorRunning = -1;
        int AllReactorsCount = -1;
        float PowerUsed = -1;
        float MaxPower = -1;
        string PowerUsageIndicator = "[]";
        int SolarPanelsRunning = -1;
        int SolarAllCount = -1;
        float SolarOutput = -1;
        float SolarMaxOutput = -1;
        string SolarPowerIndicator = "[]";
        int BatteryCountRunning = -1;
        int BatteryAllCount = -1;
        float BatteryMaxLoad = -1;
        float BatteryCurrentLoad = -1;
        string BatteryLoadIndicator = "[]";
        float BatteryInput = -1;
        float BatterOutput = -1;
        string BatteryInputOutPutIndicator = "[]";
        int BatteryLoadPercent = -1;
            */
            string Out = "INFO:ModulName = " + ModuleName + ";" + Environment.NewLine ;
            Out = Out + "SETTING:UranSaverModeOverride = Off:On|Off;" + Environment.NewLine;
            Out = Out + "SETTING:EmergencyModeOverride = Off:On|Off;" + Environment.NewLine;
            //Out = Out + "SETTING:PreEmergencyModeOverride = Off:On|Off;" + Environment.NewLine;
            Out = Out + "SETTING:EmergencyModeSetting = Off:10|20|30|40|50|60|70|80|90|Off;" + Environment.NewLine;
            Out = Out + "SETTING:UranSaveModeSetting = Off:10|20|30|40|50|60|70|80|90|Off;" + Environment.NewLine;
            Out = Out + "INFO:ReactorRunning = Loading..;" + Environment.NewLine;
            Out = Out + "INFO:PowerUsed = Loading..;" + Environment.NewLine;
           // Out = Out + "INFO:MaxPower = Loading..;" + Environment.NewLine;
            Out = Out + "INFO:SolarPanelsRunning = Loading..;" + Environment.NewLine;
            Out = Out + "INFO:SolarOutput = Loading..;" + Environment.NewLine;
            //Out = Out + "INFO:SolarMaxOutput = Loading.." + Environment.NewLine;
            Out = Out + "INFO:BatteryCountRunning = Loading..;" + Environment.NewLine;
            Out = Out + "INFO:BatteryVoltage = Loading..;" + Environment.NewLine;
           // Out = Out + "INFO:BatteryCurrentLoad = Loading.." + Environment.NewLine;
           // Out = Out + "INFO:BatteryInput = Loading..;" + Environment.NewLine;
           // Out = Out + "INFO:BatteryOutput = Loading.." + Environment.NewLine;
            Out = Out + "INFO:BatteryLoadPercent = Loading..;" + Environment.NewLine;
            Me.CustomData = Out;


            /*
INFO:ModulName = Energy:;
SETTING:UranSaverModeOverride = Off:On|Off;
SETTING:EmergencyModeOverride = Off:On|Off;
SETTING:PreEmergencyModeOverride = Off:On|Off;
SETTING:EmergencyModeSetting = Off:10|20|30|40|50|60|70|80|90|Off;
SETTING:UranSaveModeSetting = Off:10|20|30|40|50|60|70|80|90|Off;
INFO:ReactorRunning = Loading..:;
INFO:PowerUsed = Loading..:;
INFO:MaxPower = Loading..:;
INFO:SolarPanelsRunning = Loading..:;
INFO:SolarOutput = Loading..:;
INFO:BatteryCountRunning = Loading..:;
INFO:BatteryVoltage = Loading..:;
INFO:BatteryLoadPercent = Loading..:;
             * 
             */

        }

        #endregion

        public void Main(string argument, UpdateType updateSource)
        {


            DoEveryTime();
            return;
        }

        int Tick = 0;
        public void DoEveryTime()
        {
            
            if(Tick == 0)
            {
                Findblocks();
                ReadFromCustomData();
                GetEnergyData();
                WriteToCustomData();
            }

            if(Tick == 5)
            {
                ReadSetting();
                UseSettings();
            }






            Tick++;
            if (Tick >= MaxTicks)
            {
                Tick = 0;
            }
            return;
        }


        #endregion


        #region Settings
        
        public void ReadSetting()
        {
            foreach(CDValues Value in CDData)
            {
                if (Value.Tag == "SETTING")
                {
                    if (Value.Name.Contains("UranSaveModeSetting"))
                    {
                        if (Value.Value == "Off")
                        {
                            UranSaveMode = false;

                        }
                        else
                        {

                            UranSaveMode = true;
                            try
                            {
                                UranSavingSetting = Convert.ToInt32(Value.Value);
                            }
                            catch (FormatException e)
                            {
                                Echo("Wrong Value FOrmat!1");
                            }
                        }
                    }
                    if (Value.Name.Contains("EmergencyModeSetting"))
                    {
                        if (Value.Value == "Off")
                        {
                            EmergencyMode = false;

                        }
                        else
                        {
                            EmergencyMode = true;

                            try
                            {
                                EmergencyModeSetting = Convert.ToInt32(Value.Value);
                            }
                            catch (FormatException e)
                            {
                                Echo("Wrong Value FOrmat!2");
                            }


                        }
                    }
                    if (Value.Name.Contains("PreEmergencyModeOverride"))
                    {
                        if (Value.Value == "On")
                        {
                            PreemergencyModeOverride = true;

                        }
                        else if (Value.Value == "Off")
                        {
                            PreemergencyModeOverride = false;
                        }
                    }
                    if (Value.Name.Contains("EmergencyModeOverride"))
                    {
                        if (Value.Value == "On")
                        {
                            EmergencyModeOverride = true;
                            EmergencyMode = true;
                            EmergencymodeFunc(true);

                        }
                        else if (Value.Value == "Off")
                        {
                            EmergencyModeOverride = false;
                            EmergencyMode = false;
                            EmergencymodeFunc(false);
                        }
                    }
                    if (Value.Name.Contains("UranSaverModeOverride"))
                    {
                        if (Value.Value == "On")
                        {
                            UranSaveModeOverride = true;
                            UranSaverModeFunc(true);
                        }
                        else if (Value.Value == "Off")
                        {
                            UranSaveModeOverride = false;
                            UranSaverModeFunc(false);
                        }
                    }
                }else if(Value.Tag == "INFO")
                { 

                    if (Value.Name.Contains("ReactorRunning"))
                    {
                        Value.Value = ReactorRunning.ToString() + "/" + AllReactorsCount;
                    }
                    if (Value.Name.Contains("PowerUsed"))
                    {
                        Value.Value = PowerUsed.ToString() +" " + PowerUsageIndicator + "" + MaxPower;
                    }
                    if (Value.Name.Contains("SolarPanelsRunning"))
                    {
                        Value.Value = SolarPanelsRunning.ToString() + "/" + SolarAllCount;
                    }
                    if (Value.Name.Contains("SolarOutput"))
                    {
                        Value.Value = SolarOutput.ToString() + " " + SolarPowerIndicator + " " + SolarMaxOutput;
                    }
                    /*
                    if (Value.Name.Contains("SolarMaxOutput"))
                    {
                        Value.Value = SolarMaxOutput.ToString();
                    }
                    */
                    if (Value.Name.Contains("BatteryCountRunning"))
                    {
                        Value.Value = BatteryCountRunning.ToString() + "/" + BatteryAllCount;
                    }
                    if (Value.Name.Contains("BatteryMaxLoad"))
                    {
                        Value.Value = BatteryCurrentLoad.ToString() + " " + BatteryLoadIndicator + " " + BatteryMaxLoad;
                    }
                    /*
                    if (Value.Name.Contains("BatteryCurrentLoad"))
                    {
                        Value.Value = BatteryCurrentLoad.ToString();
                    }
                    */
                    if (Value.Name.Contains("BatteryInput"))
                    {
                        Value.Value = BatteryInput.ToString() + " " + BatteryInputOutPutIndicator + " " + BatterOutput;
                    }
                    /*
                    if (Value.Name.Contains("BatteryOutput"))
                    {
                        Value.Value = BatteryInput.ToString();
                    }
                    */
                    if (Value.Name.Contains("BatteryLoadPercent"))
                    {
                        Value.Value = BatteryLoadPercent.ToString() + "%";
                    }

                }



            }

            return;
        }

        public void UseSettings()
        {
            if (!EmergencyModeOverride)
            {
                if (EmergencyMode)
                {
                    if (BatteryLoadPercent >= EmergencyModeSetting)
                    {
                        EmergencymodeFunc(false);

                    }
                    else
                    {
                        EmergencymodeFunc(true);
                    }
                }
            }
            else
            {
                EmergencymodeFunc(true);
            }
            
            if (!UranSaveModeOverride)
            {
                if (UranSaveMode)
                {
                    if (BatteryLoadPercent >= UranSavingSetting)
                    {
                        UranSaverModeFunc(false);
                    }
                    else
                    {
                        UranSaverModeFunc(true);
                    }
                }
            }
            else
            {
                UranSaverModeFunc(true);
            }
            /*
            if(EmergencyModeOverride)
            {
                EmergencymodeFunc(true);
            }

            if(UranSaveModeOverride)
            {
                UranSaverModeFunc(true);
            }
            */
        }
        int EmergencyMessageId = -1;
        int UNEmergencyMessageId = -1;
        List<IMyTerminalBlock> AllDisabeldEmergency = new List<IMyTerminalBlock>();
        List<IMyTerminalBlock> AllEnabeldEmergency = new List<IMyTerminalBlock>();
        public void EmergencymodeFunc(bool Activate)
        {

            int Test = -1;
            int I = 0;
            if (Activate)
            {
                I = 0;
                Test = -1;
                EmergencyMessageId = AddWarning("Warning! Energy Emergency Activated!Only Emergency Battery is On!");
                if (UNEmergencyMessageId != -1)
                {
                    foreach (CDValues Value in CDData)
                    {
                        try
                        {
                            Test = Convert.ToInt32(Value.Value2);
                        }
                        catch (FormatException e)
                        {

                            Echo("Wrong ID Format!");
                            return;
                        }

                        if (Test == UNEmergencyMessageId)
                        {
                            CDData.RemoveAt(I);
                            break;
                        }

                        I++;
                    }
                }

                foreach (IMyReactor Block in AllReactors)
                {
                    if(!Block.Enabled)
                    {
                        Block.Enabled = true;
                        AllDisabeldEmergency.Add(Block);
                    }
                }
                foreach(IMyBatteryBlock Block in AllBatterys)
                {
                    if(EmergengyBattery == Block)
                    {
                        Echo("Enable emergency Battery..");
                        Block.Enabled = true;
                        EmergengyBattery.ChargeMode = ChargeMode.Auto;
                    }
                    else
                    {
                            Block.ChargeMode = ChargeMode.Recharge;
                            Block.Enabled = true;
                        
                    }
                }
                foreach(IMyRefinery Block in AllRefinerys)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach(IMyAssembler Block in AllAssembler)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach(IMyAirVent Block in AllAirVents)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach(IMyRadioAntenna Block in AllAntennas)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }

                }
                foreach (IMyMotorStator Block in AllRotors)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyArtificialMassBlock Block in AllMass)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyBeacon Block in AllBeacons)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyLargeInteriorTurret Block in AllIntTurrets)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyLargeGatlingTurret Block in AllLargeGatlingTurrets)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMySmallGatlingGun Block in AllSmallGatlinTurrets)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyLargeMissileTurret Block in AllLargeMissleturrets)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMySmallMissileLauncher Block in AllSmallMissleturrets)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMySmallMissileLauncherReload Block in AllSmallRocketLauncher)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyInteriorLight Block in AllLights)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyJumpDrive Block in AllJumpDrives)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyLaserAntenna Block in AllLaserAntennas)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyOreDetector Block in AllOreDetector)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyOxygenFarm Block in AllOxygenFarms)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyGasGenerator Block in AllOxygenGenerators)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyProjector Block in AllProjectors)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyGravityGenerator Block in AllGravityGenerators)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyGravityGeneratorSphere Block in AllGravityGeneratorsSphere)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyShipGrinder Block in AllGrinder)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyShipDrill Block in AllDrills)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }
                foreach (IMyShipWelder Block in AllWelder)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllEnabeld.Add(Block);
                    }
                }

               return;
            }
            else
            {
                I = 0;
                Test = -1;
                UNEmergencyMessageId = AddWarning("Emergency Mode Deactivated...");

                if (EmergencyMessageId != -1)
                {
                    foreach (CDValues Value in CDData)
                    {
                        try
                        {
                            Test = Convert.ToInt32(Value.Value2);
                        } catch (FormatException e)
                        {

                            Echo("Wrong ID Format!");
                            return;
                        }

                        if (Test == EmergencyMessageId)
                        {
                            CDData.RemoveAt(I);
                            break;
                        }

                        I++;
                    }
                }

                foreach (IMyReactor Block in AllReactors)
                {
                    if(AllDisabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = false;
                        AllDisabeldEmergency.Remove(Block);
                    }
                }
               
            foreach (IMyBatteryBlock Block in AllBatterys)
                {
                    if (EmergengyBattery == Block)
                    {
                        Echo("Enable emergency Battery..");
                        Block.Enabled = true;
                        EmergengyBattery.ChargeMode = ChargeMode.Recharge;
                    }
                    else
                    {
                        Block.ChargeMode = ChargeMode.Auto;
                        Block.Enabled = true;
                    }
                }
                foreach (IMyRefinery Block in AllRefinerys)
                {
                    if(AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyAssembler Block in AllAssembler)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyAirVent Block in AllAirVents)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyRadioAntenna Block in AllAntennas)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }

                }
                foreach (IMyMotorStator Block in AllRotors)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyArtificialMassBlock Block in AllMass)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyBeacon Block in AllBeacons)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyLargeInteriorTurret Block in AllIntTurrets)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyLargeGatlingTurret Block in AllLargeGatlingTurrets)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMySmallGatlingGun Block in AllSmallGatlinTurrets)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyLargeMissileTurret Block in AllLargeMissleturrets)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMySmallMissileLauncher Block in AllSmallMissleturrets)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMySmallMissileLauncherReload Block in AllSmallRocketLauncher)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyInteriorLight Block in AllLights)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyJumpDrive Block in AllJumpDrives)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyLaserAntenna Block in AllLaserAntennas)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyOreDetector Block in AllOreDetector)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyOxygenFarm Block in AllOxygenFarms)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyGasGenerator Block in AllOxygenGenerators)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyProjector Block in AllProjectors)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyGravityGenerator Block in AllGravityGenerators)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyGravityGeneratorSphere Block in AllGravityGeneratorsSphere)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyShipGrinder Block in AllGrinder)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyShipDrill Block in AllDrills)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                foreach (IMyShipWelder Block in AllWelder)
                {
                    if (AllEnabeldEmergency.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllEnabeldEmergency.Remove(Block);
                    }
                }
                return;
            }
        }

        List<IMyTerminalBlock> AllDisabeld = new List<IMyTerminalBlock>();
        List<IMyTerminalBlock> AllEnabeld = new List<IMyTerminalBlock>();
        public void UranSaverModeFunc(bool Activate)
        {

            if(Activate)
            {
                foreach(IMyReactor Block in AllReactors)
                {
                    if (!Block.Enabled)
                    {
                        Block.Enabled = true;
                        AllEnabeld.Add(Block);
                    }
                }

                foreach(IMySolarPanel Block in AllSolarPanels)
                {
                    if (!Block.Enabled)
                    {
                        Block.Enabled = true;
                        AllEnabeld.Add(Block);
                    }
                }

                foreach(IMyBatteryBlock Block in AllBatterys)
                {
                    if (!Block.Enabled)
                    {
                        Block.Enabled = true;
                        AllEnabeld.Add(Block);
                    }
                }

                foreach(IMyRefinery Block in AllRefinerys)
                {
                    if(Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllDisabeld.Add(Block);
                    }

                }

                foreach(IMyAssembler Block in AllAssembler)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllDisabeld.Add(Block);
                    }
                }

                foreach(IMyShipDrill Block in AllDrills)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllDisabeld.Add(Block);
                    }
                }

                foreach(IMyShipGrinder Block in AllGrinder)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllDisabeld.Add(Block);
                    }
                }

                foreach(IMyBeacon Block in AllBeacons)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllDisabeld.Add(Block);
                    }
                }
                foreach(IMyRadioAntenna Block in AllAntennas)
                {
                    if (Block.Enabled)
                    {
                        Block.Enabled = false;
                        AllDisabeld.Add(Block);
                    }
                }
                return;
            }
            else
            {

                foreach (IMyReactor Block in AllReactors)
                {
                    if(AllEnabeld.Contains(Block))
                    {
                        Block.Enabled = false;
                        AllEnabeld.Remove(Block);
                    }
                }

                foreach (IMySolarPanel Block in AllSolarPanels)
                {
                    if (AllEnabeld.Contains(Block))
                    {
                        Block.Enabled = false;
                        AllEnabeld.Remove(Block);
                    }
                }

                foreach (IMyBatteryBlock Block in AllBatterys)
                {
                    if (AllEnabeld.Contains(Block))
                    {
                        Block.Enabled = false;
                        AllEnabeld.Remove(Block);
                    }
                }

                foreach (IMyRefinery Block in AllRefinerys)
                {
                    if(AllDisabeld.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllDisabeld.Remove(Block);
                    }

                }

                foreach (IMyAssembler Block in AllAssembler)
                {
                    if (AllDisabeld.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllDisabeld.Remove(Block);
                    }
                }

                foreach (IMyShipDrill Block in AllDrills)
                {
                    if (AllDisabeld.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllDisabeld.Remove(Block);
                    }
                }

                foreach (IMyShipGrinder Block in AllGrinder)
                {
                    if (AllDisabeld.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllDisabeld.Remove(Block);
                    }
                }

                foreach (IMyBeacon Block in AllBeacons)
                {
                    if (AllDisabeld.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllDisabeld.Remove(Block);
                    }
                }
                foreach (IMyRadioAntenna Block in AllAntennas)
                {
                    if (AllDisabeld.Contains(Block))
                    {
                        Block.Enabled = true;
                        AllDisabeld.Remove(Block);
                    }
                }
                AllEnabeld.Clear();
                AllDisabeld.Clear();
                return;
            }

        }


        #endregion


        #region EnergyData
        int ReactorRunning = -1;
        int AllReactorsCount = -1;
        float PowerUsed = -1;
        float MaxPower = -1;
        string PowerUsageIndicator = "[]";
        int SolarPanelsRunning = -1;
        int SolarAllCount = -1;
        float SolarOutput = -1;
        float SolarMaxOutput = -1;
        string SolarPowerIndicator = "[]";
        int BatteryCountRunning = -1;
        int BatteryAllCount = -1;
        float BatteryMaxLoad = -1;
        float BatteryCurrentLoad = -1;
        string BatteryLoadIndicator = "[]";
        float BatteryInput = -1;
        float BatterOutput = -1;
        string BatteryInputOutPutIndicator = "[]";
        int BatteryLoadPercent = -1;
        string BatteryRunningIndicator = "[]";
        string SolarRunningIndicator = "[]";
        string ReactorRunningIndiscator = "[]";




        public void GetEnergyData()
        {

            ReactorRunning = 0;
             AllReactorsCount = 0;
             PowerUsed = 0;
             MaxPower = 0;
             PowerUsageIndicator = "[]";
             SolarPanelsRunning = 0;
             SolarAllCount = 0;
             SolarOutput = 0;
             SolarMaxOutput = 0;
             SolarPowerIndicator = "[]";
             BatteryCountRunning = 0;
             BatteryAllCount = 0;
             BatteryMaxLoad = 0;
             BatteryCurrentLoad = 0;
             BatteryLoadIndicator = "[]";
             BatteryInput = 0;
             BatterOutput = 0;
             BatteryInputOutPutIndicator = "[]";
             BatteryLoadPercent = 0;
             BatteryRunningIndicator = "[]";
             SolarRunningIndicator = "[]";
             ReactorRunningIndiscator = "[]";

            if (AllReactors.Count > 0)
            {
                foreach (IMyReactor Block in AllReactors)
                {
                    if (Block.Enabled)
                    {
                        ReactorRunning++;
                    }
                    PowerUsed = PowerUsed + Block.CurrentOutput;
                    MaxPower = MaxPower + Block.MaxOutput;

                }
                int Max22 = AllReactors.Count;
                int Current22 = ReactorRunning;
                int Perc22 = ReturnPercent(Max22, Current22);
                ReactorRunningIndiscator = ReturnIndicator(Perc22);



                int Max = Convert.ToInt32(MaxPower);
                int Current = Convert.ToInt32(PowerUsed);
                if (Max != -1 && Current != -1)
                {
                    int Perc = ReturnPercent(Max, Current);

                    PowerUsageIndicator = ReturnIndicator(Perc);
                }
                else
                {
                    PowerUsageIndicator = ReturnIndicator(0);
                }

            }

            if (AllSolarPanels.Count > 0)
            {
                foreach (IMySolarPanel Block in AllSolarPanels)
                {
                    if (Block.Enabled)
                    {
                        SolarPanelsRunning++;
                    }
                    SolarOutput = SolarOutput + Block.CurrentOutput;
                    SolarMaxOutput = SolarMaxOutput + Block.MaxOutput;

                }
                int Max22 = AllSolarPanels.Count;
                int Current22 = SolarPanelsRunning;
                int Perc22 = ReturnPercent(Max22, Current22);
                SolarRunningIndicator = ReturnIndicator(Perc22);

                int Max = Convert.ToInt32(SolarMaxOutput);
                int Current = Convert.ToInt32(SolarOutput);
                if (Max != -1 && Current != -1)
                {

                    int Perc = ReturnPercent(Max, Current);

                    SolarPowerIndicator = ReturnIndicator(Perc);
                }
                else
                {
                    SolarPowerIndicator = ReturnIndicator(0);
                }

            }

            if (AllBatterys.Count > 0)
            {
                foreach (IMyBatteryBlock Block in AllBatterys)
                {
                    if (Block.Enabled)
                    {
                        BatteryCountRunning++;
                    }
                    BatterOutput = BatterOutput + Block.CurrentOutput;
                    BatteryInput = BatteryInput + Block.CurrentInput;
                    BatteryCurrentLoad = BatteryCurrentLoad + Block.CurrentStoredPower;
                    BatteryMaxLoad = BatteryMaxLoad + Block.MaxStoredPower;
                    BatteryAllCount = AllBatterys.Count;
                }

                int Max22 = AllBatterys.Count;
                int Current22 = BatteryCountRunning;
                int Perc22 = ReturnPercent(Max22, Current22);
                BatteryRunningIndicator = ReturnIndicator(Perc22);

                int Max = Convert.ToInt32(BatteryMaxLoad);
                int Current = Convert.ToInt32(BatteryCurrentLoad);

                int Perc = ReturnPercent(Max, Current);
                BatteryLoadPercent = Perc;
                BatteryLoadIndicator = ReturnIndicator(Perc);
 

            }

            /*
INFO:ModulName = Energy:;
SETTING:UranSaverModeOverride = Off:On|Off;
SETTING:EmergencyModeOverride = Off:On|Off;
SETTING:PreEmergencyModeOverride = Off:On|Off;
SETTING:EmergencyModeSetting = Off:10|20|30|40|50|60|70|80|90|Off;
SETTING:UranSaveModeSetting = Off:10|20|30|40|50|60|70|80|90|Off;
INFO:ReactorRunning = Loading..:;
INFO:PowerUsed = Loading..:;
INFO:MaxPower = Loading..:;
INFO:SolarPanelsRunning = Loading..:;
INFO:SolarOutput = Loading..:;
INFO:BatteryCountRunning = Loading..:;
INFO:BatteryVoltage = Loading..:;
INFO:BatteryLoadPercent = Loading..:;
*/

            foreach (CDValues Data in CDData)
            {

                if(Data.Name == "ReactorRunning")
                {
                    if (AllReactors.Count > 0)
                    {
                        int Perc = ReturnPercent(AllReactors.Count, ReactorRunning);
                        Data.Value = ReactorRunning + "/" + AllReactors.Count + Environment.NewLine + ReactorRunningIndiscator + " " + Perc + "%";
                    }
                    else
                    {
                        Data.Value = "No Data";
                    }
                }

                if(Data.Name == "PowerUsed")
                {
                    if (AllReactors.Count > 0)
                    {
                        decimal Max = Convert.ToDecimal(MaxPower);
                        decimal Current = Convert.ToDecimal(PowerUsed);


                        int Perc = ReturnPercent(Max, Current);
                        Data.Value = PowerUsed + "/" + MaxPower + " MwH" + Environment.NewLine + PowerUsageIndicator + " " + Perc + "%";
                    }
                    else
                    {
                        Data.Value = "No Data";
                    }

                }

                if (Data.Name == "SolarPanelsRunning")
                {
                    if (AllSolarPanels.Count > 0)
                    {
                        decimal Max = Convert.ToDecimal(AllSolarPanels.Count);
                        decimal Current = Convert.ToDecimal(SolarPanelsRunning);
                        int Perc = ReturnPercent(Max, Current);
                        Data.Value = SolarPanelsRunning + "/" + AllSolarPanels.Count + Environment.NewLine + SolarRunningIndicator + " " + Perc + "%" ;
                    }
                    else
                    {
                        Data.Value = "No Data";
                    }
                }

                if (Data.Name == "SolarOutput")
                {
                    if (AllSolarPanels.Count > 0)
                    {
                        decimal Max = Convert.ToDecimal(SolarMaxOutput);
                        decimal Current = Convert.ToDecimal(SolarOutput);
                        double maxMath = SolarMaxOutput * 1000 / AllSolarPanels.Count;
                        double currentMath = SolarOutput * 1000 / AllSolarPanels.Count;

                        double maxfix = Math.Round(maxMath, 2);
                        double currentfix = Math.Round(currentMath, 2);

                        

                        int Perc = ReturnPercent(Max, Current);
                        Data.Value = currentfix + "/" + maxfix + " kW"+ Environment.NewLine + SolarPowerIndicator + " " + Perc + "%";
                    }
                    else
                    {
                        Data.Value = "No Data";
                    }
                }

                if (Data.Name == "BatteryCountRunning")
                {
                    if (AllBatterys.Count > 0)
                    {
                        decimal Max = Convert.ToDecimal(BatteryAllCount);
                        decimal Current = Convert.ToDecimal(BatteryCountRunning);
                        int Perc = ReturnPercent(Max, Current);
                        Data.Value = BatteryCountRunning + "/" + BatteryAllCount + Environment.NewLine + BatteryRunningIndicator + " " + Perc + "%";
                    }
                    else
                    {
                        Data.Value = "No Data";
                    }
                }

                if (Data.Name == "BatteryVoltage")
                {
                    if (AllBatterys.Count > 0)
                    {
                       
                        int Input = Convert.ToInt32(BatteryInput);
                        int Output = Convert.ToInt32(BatterOutput);
                        double InputMath = BatteryInput * 1000 / AllBatterys.Count;
                        double OutPutMath = BatterOutput * 1000 / AllBatterys.Count;

                        double Inputfix = Math.Round(InputMath,2);
                        double Outputfix = Math.Round(OutPutMath,2);

                        Data.Value =  "(In/Out )" + Environment.NewLine + Inputfix + " / " + Outputfix + " kW";
                    }
                    else
                    {
                        Data.Value = "No Data";
                    }
                }

                if (Data.Name == "BatteryLoadPercent")
                {
                    if (AllBatterys.Count > 0)
                    {

                        Data.Value = BatteryCurrentLoad + "/" + BatteryMaxLoad + " MWH" + Environment.NewLine + BatteryLoadIndicator + " " + BatteryLoadPercent + "%";
                    }
                    else
                    {
                        Data.Value = "No Data";
                    }
                }

            }
 



            return;
        }

        public string ReturnIndicator(int Percent)
        {

            string Out = "[|";
            int Mathe = 0;
            if(Percent == 0)
            {
                Out  = Out + "                    ]";
                return Out;
            }


            Mathe = (Percent / 5);
            //Mathe = Math.Round(Mathe);
            int I = 0;
            do
            {
                if (I < Mathe)
                {
                    Out = Out + "|";
                }
                else
                {
                    Out = Out + " ";
                }
                I++;

            } while (I < 20);
            Out = Out + "]";

            // string Out = "";
            return Out;
        }

        public int ReturnPercent(decimal Max, decimal Current)
        {
            if(Current == Max)
            {

                return 100;
            }
            decimal Percent = 0;

            decimal Math = (Max / 100);
            int PercentInt = 0;

            if (Math != 0)
            {

                Percent = (Current / Math);

                PercentInt = Convert.ToInt32(Percent);
            }

            return PercentInt;
        }



        #endregion

    }
}
