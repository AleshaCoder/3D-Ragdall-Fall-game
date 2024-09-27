using Io.AppMetrica;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Analytics
{
    public static class AnalyticsSender
    {
        public static bool IsFirstOpeningMainMenu = true;

        public static void OpenMainMenu()
        {
            if (IsFirstOpeningMainMenu)
                AppMetrica.ReportEvent("Main Menu Opened First");
            else
                AppMetrica.ReportEvent("Main Menu Opened");
            AppMetrica.SendEventsBuffer();
            IsFirstOpeningMainMenu = false;
        }

        public static void OpenSettings()
        {
            AppMetrica.ReportEvent("Settings Opened");
            AppMetrica.SendEventsBuffer();
        }

        public static void OpenSkins()
        {
            AppMetrica.ReportEvent("Skins Menu Opened");
            AppMetrica.SendEventsBuffer();
        }

        public static void SelectSkin(string name)
        {
            AppMetrica.ReportEvent($"Skin {name} Selected");
            AppMetrica.SendEventsBuffer();
        }

        public static void CancelSelectSkin()
        {
            AppMetrica.ReportEvent($"Skin Selection Canceled");
            AppMetrica.SendEventsBuffer();
        }

        public static void OpenMapsMenu()
        {
            AppMetrica.ReportEvent($"Maps Menu Opened");
            AppMetrica.SendEventsBuffer();
        }

        public static void SelectMap(string name)
        {
            AppMetrica.ReportEvent($"Map {name} Selected");
            AppMetrica.SendEventsBuffer();
        }

        public static void CancelSelectMap()
        {
            AppMetrica.ReportEvent($"Map Selection Canceled");
            AppMetrica.SendEventsBuffer();
        }
        
        public static void NewGame(string name)
        {
            AppMetrica.ReportEvent($"Map {name} - New Game");
            AppMetrica.SendEventsBuffer();
        }
        
        public static void LoadSavedGame(string name)
        {
            AppMetrica.ReportEvent($"Map {name} - Saved Game Loaded");
            AppMetrica.SendEventsBuffer();
        }





        public static void OpenEditorMenu()
        {
            AppMetrica.ReportEvent($"MapEditor Menu Opened");
            AppMetrica.SendEventsBuffer();
        }

        public static void CloseEditorMenu()
        {
            AppMetrica.ReportEvent($"MapEditor Menu Closed");
            AppMetrica.SendEventsBuffer();
        }

        public static void SelectItem(string name)
        {
            AppMetrica.ReportEvent($"Item {name} Selected For Spawn");
            AppMetrica.SendEventsBuffer();
        }
        
        public static void SpawnItem(string name)
        {
            AppMetrica.ReportEvent($"Item {name} Spawned");
            AppMetrica.SendEventsBuffer();
        }

        public static void TimeScaleChanged()
        {
            AppMetrica.ReportEvent($"Time Scale Changed");
        }

        public static void ResetCharacter()
        {
            AppMetrica.ReportEvent($"Character Reset");
            AppMetrica.SendEventsBuffer();
        }
        
        public static void KillCharacter()
        {
            AppMetrica.ReportEvent($"Character Killed");
            AppMetrica.SendEventsBuffer();
        }

        public static void SaveMap()
        {
            AppMetrica.ReportEvent($"Map Saved");
            AppMetrica.SendEventsBuffer();
        }
        
        public static void LoadMap()
        {
            AppMetrica.ReportEvent($"Map Save Loaded");
            AppMetrica.SendEventsBuffer();
        }
    }
}
