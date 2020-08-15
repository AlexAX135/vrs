﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VirtualRadar.Database.StateHistory {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Scripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Scripts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VirtualRadar.Database.StateHistory.Scripts", typeof(Scripts).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [CountrySnapshot] (
        ///    [CreatedUtc]
        ///   ,[Fingerprint]
        ///   ,[CountryName]
        ///) VALUES (
        ///    @CreatedUtc
        ///   ,@Fingerprint
        ///   ,@CountryName
        ///)
        ///ON CONFLICT ([Fingerprint]) DO NOTHING;
        ///
        ///SELECT *
        ///FROM   [CountrySnapshot]
        ///WHERE  [Fingerprint] = @Fingerprint;
        ///.
        /// </summary>
        internal static string CountrySnapshot_GetOrCreate {
            get {
                return ResourceManager.GetString("CountrySnapshot_GetOrCreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT   *
        ///FROM     [DatabaseVersion]
        ///ORDER BY [DatabaseVersionID] DESC
        ///LIMIT    1;
        ///.
        /// </summary>
        internal static string DatabaseVersion_GetLatest {
            get {
                return ResourceManager.GetString("DatabaseVersion_GetLatest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [DatabaseVersion] (
        ///    [DatabaseVersionID]
        ///   ,[CreatedUtc]
        ///) VALUES (
        ///    @DatabaseVersionID
        ///   ,@CreatedUtc
        ///)
        ///ON    CONFLICT ([DatabaseVersionID])
        ///DO    UPDATE
        ///SET   [CreatedUtc] = @CreatedUtc;
        ///.
        /// </summary>
        internal static string DatabaseVersion_Save {
            get {
                return ResourceManager.GetString("DatabaseVersion_Save", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [EnginePlacementSnapshot] (
        ///    [CreatedUtc]
        ///   ,[Fingerprint]
        ///   ,[EnumValue]
        ///   ,[EnginePlacementName]
        ///) VALUES (
        ///    @CreatedUtc
        ///   ,@Fingerprint
        ///   ,@EnumValue
        ///   ,@EnginePlacementName
        ///)
        ///ON CONFLICT ([Fingerprint]) DO NOTHING;
        ///
        ///SELECT *
        ///FROM   [EnginePlacementSnapshot]
        ///WHERE  [Fingerprint] = @Fingerprint;
        ///.
        /// </summary>
        internal static string EnginePlacementSnapshot_GetOrCreate {
            get {
                return ResourceManager.GetString("EnginePlacementSnapshot_GetOrCreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [EngineTypeSnapshot] (
        ///    [CreatedUtc]
        ///   ,[Fingerprint]
        ///   ,[EnumValue]
        ///   ,[EngineTypeName]
        ///) VALUES (
        ///    @CreatedUtc
        ///   ,@Fingerprint
        ///   ,@EnumValue
        ///   ,@EngineTypeName
        ///)
        ///ON CONFLICT ([Fingerprint]) DO NOTHING;
        ///
        ///SELECT *
        ///FROM   [EngineTypeSnapshot]
        ///WHERE  [Fingerprint] = @Fingerprint;
        ///.
        /// </summary>
        internal static string EngineTypeSnapshot_GetOrCreate {
            get {
                return ResourceManager.GetString("EngineTypeSnapshot_GetOrCreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [ManufacturerSnapshot] (
        ///    [CreatedUtc]
        ///   ,[Fingerprint]
        ///   ,[ManufacturerName]
        ///) VALUES (
        ///    @CreatedUtc
        ///   ,@Fingerprint
        ///   ,@ManufacturerName
        ///)
        ///ON CONFLICT ([Fingerprint]) DO NOTHING;
        ///
        ///SELECT *
        ///FROM   [ManufacturerSnapshot]
        ///WHERE  [Fingerprint] = @Fingerprint;
        ///.
        /// </summary>
        internal static string ManufacturerSnapshot_GetOrCreate {
            get {
                return ResourceManager.GetString("ManufacturerSnapshot_GetOrCreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [OperatorSnapshot] (
        ///    [CreatedUtc]
        ///   ,[Fingerprint]
        ///   ,[Icao]
        ///   ,[OperatorName]
        ///) VALUES (
        ///    @CreatedUtc
        ///   ,@Fingerprint
        ///   ,@Icao
        ///   ,@OperatorName
        ///)
        ///ON CONFLICT ([Fingerprint]) DO NOTHING;
        ///
        ///SELECT *
        ///FROM   [OperatorSnapshot]
        ///WHERE  [Fingerprint] = @Fingerprint;
        ///.
        /// </summary>
        internal static string OperatorSnapshot_GetOrCreate {
            get {
                return ResourceManager.GetString("OperatorSnapshot_GetOrCreate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to --
        ///-- Database Version (v1)
        ///--
        ///CREATE TABLE IF NOT EXISTS [DatabaseVersion]
        ///(
        ///    [DatabaseVersionID] INTEGER NOT NULL PRIMARY KEY
        ///   ,[CreatedUtc]        DATETIME NOT NULL
        ///);
        ///
        ///
        ///--
        ///-- VrsSession (v1)
        ///--
        ///CREATE TABLE IF NOT EXISTS [VrsSession]
        ///(
        ///    [VrsSessionID]      INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT
        ///   ,[DatabaseVersionID] BIGINT NOT NULL
        ///   ,[CreatedUtc]        DATETIME NOT NULL
        ///);
        ///
        ///
        ///--
        ///-- CountrySnapshot (v1)
        ///--
        ///CREATE TABLE IF NOT EXISTS [CountrySnapshot]
        ///(
        ///    [Count [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Schema {
            get {
                return ResourceManager.GetString("Schema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [VrsSession] (
        ///    [DatabaseVersionID]
        ///   ,[CreatedUtc]
        ///) VALUES (
        ///    @DatabaseVersionID
        ///   ,@CreatedUtc
        ///);
        ///SELECT last_insert_rowid();
        ///.
        /// </summary>
        internal static string VrsSession_Insert {
            get {
                return ResourceManager.GetString("VrsSession_Insert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO [WakeTurbulenceCategorySnapshot] (
        ///    [CreatedUtc]
        ///   ,[Fingerprint]
        ///   ,[EnumValue]
        ///   ,[WakeTurbulenceCategoryName]
        ///) VALUES (
        ///    @CreatedUtc
        ///   ,@Fingerprint
        ///   ,@EnumValue
        ///   ,@WakeTurbulenceCategoryName
        ///)
        ///ON CONFLICT ([Fingerprint]) DO NOTHING;
        ///
        ///SELECT *
        ///FROM   [WakeTurbulenceCategorySnapshot]
        ///WHERE  [Fingerprint] = @Fingerprint;
        ///.
        /// </summary>
        internal static string WakeTurbulenceCategorySnapshot_GetOrCreate {
            get {
                return ResourceManager.GetString("WakeTurbulenceCategorySnapshot_GetOrCreate", resourceCulture);
            }
        }
    }
}
