﻿namespace ItemRarities
{
    public static class BuildInfo
    {
        #region Mandatory
        /// <summary>The machine readable name of the mod (no special characters or spaces)</summary>
        public const string Name                            = "ItemRarities";
        /// <summary>Who made the mod</summary>
        public const string Author                          = "Deadman";
        /// <summary>Current version (Using Major.Minor.Build) </summary>
        public const string Version                         = "0.1.0";
        /// <summary>Name used on GUI's, like ModSettings</summary>
        public const string GUIName                         = "Item Rarities";
        /// <summary>The minimum Melon Loader version that your mod requires. Latest MelonLoader is 0.6.1 as of 1-September-2023</summary>
        public const string MelonLoaderVersion              = "0.6.1";
        #endregion

        #region Optional
        /// <summary>What the mod does</summary>
        public const string Description                     = null;
        /// <summary>Company that made it</summary>
        public const string Company                         = null;
        /// <summary>A valid download link</summary>
        public const string DownloadLink                    = null;
        /// <summary>Copyright info</summary>
        /// <remarks>When updating the year, use the StartYear-CurrentYear format</remarks>
        public const string Copyright                       = "Copyright © 2023";
        /// <summary>Trademark info</summary>
        public const string Trademark                       = null;
        /// <summary>Product Name (Generally use the Name)</summary>
        public const string Product                         = "ItemRarities";
        /// <summary>Culture info</summary>
        public const string Culture                         = null;
        /// <summary>Any incompatible mods should be listed here</summary>
        public const string[] IncompatibleMods              = null;
        /// <summary>Priority of your mod. Most of the time you should not need to change this</summary>
        public const int Priority                           = 0;
        #endregion
    }
}