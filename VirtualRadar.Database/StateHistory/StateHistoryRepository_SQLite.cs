﻿// Copyright © 2020 onwards, Andrew Whewell
// All rights reserved.
//
// Redistribution and use of this software in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//    * Neither the name of the author nor the names of the program's contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHORS OF THE SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Data;
using System.IO;
using System.Linq;
using Dapper;
using InterfaceFactory;
using VirtualRadar.Interface.Settings;
using VirtualRadar.Interface.SQLite;
using VirtualRadar.Interface.StateHistory;

namespace VirtualRadar.Database.StateHistory
{
    /// <summary>
    /// The SQLite implementation of <see cref="IStateHistoryRepository"/>.
    /// </summary>
    class StateHistoryRepository_SQLite : IStateHistoryRepository_SQLite
    {
        /// <summary>
        /// The connection string that the repository will always use.
        /// </summary>
        private string _ConnectionString;

        /// <summary>
        /// The database instance that holds configuration variables etc. for us.
        /// </summary>
        private IStateHistoryDatabaseInstance _DatabaseInstance;

        /// <summary>
        /// See interface docs.
        /// </summary>
        public bool IsMissing { get; private set; }

        /// <summary>
        /// See interface docs.
        /// </summary>
        public bool WritesEnabled => _DatabaseInstance?.WritesEnabled ?? false;

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="databaseInstance"></param>
        public void Initialise(IStateHistoryDatabaseInstance databaseInstance)
        {
            if(_DatabaseInstance != null) {
                throw new InvalidOperationException($"You cannot initialise a {nameof(IStateHistoryRepository)} twice");
            }
            _DatabaseInstance = databaseInstance ?? throw new ArgumentNullException(nameof(databaseInstance));
            _ConnectionString = BuildConnectionString();
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="icao"></param>
        /// <param name="registration"></param>
        /// <param name="modelSnapshotID"></param>
        /// <param name="constructionNumber"></param>
        /// <param name="yearBuilt"></param>
        /// <param name="operatorSnapshotID"></param>
        /// <param name="countrySnapshotID"></param>
        /// <param name="isMilitary"></param>
        /// <param name="isInteresting"></param>
        /// <param name="userNotes"></param>
        /// <param name="userTag"></param>
        /// <returns></returns>
        public AircraftSnapshot AircraftSnapshot_GetOrCreate(
            byte[] fingerprint,
            DateTime createdUtc,
            string icao,
            string registration,
            long? modelSnapshotID,
            string constructionNumber,
            string yearBuilt,
            long? operatorSnapshotID,
            long? countrySnapshotID,
            bool? isMilitary,
            bool? isInteresting,
            string userNotes,
            string userTag
        )
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<AircraftSnapshot>(Scripts.AircraftSnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    icao,
                    registration,
                    modelSnapshotID,
                    constructionNumber,
                    yearBuilt,
                    operatorSnapshotID,
                    countrySnapshotID,
                    isMilitary,
                    isInteresting,
                    userNotes,
                    userTag,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="aircraftList"></param>
        public void AircraftList_Insert(AircraftList aircraftList)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                aircraftList.AircraftListID = connection.Query<long>(Scripts.AircraftList_Insert, new {
                    aircraftList.VrsSessionID,
                    aircraftList.IsKeyList,
                    aircraftList.ReceiverSnapshotID,
                    aircraftList.CreatedUtc,
                    aircraftList.UpdatedUtc,
                }).Single();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="flight"></param>
        public void Flight_Insert(Flight flight)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                flight.FlightID = connection.Query<long>(Scripts.Flight_Insert, new {
                    flight.Preserve,
                    flight.IntervalSeconds,
                    flight.CreatedUtc,
                    flight.UpdatedUtc,
                }).Single();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <returns></returns>
        public DatabaseVersion DatabaseVersion_GetLatest()
        {
            using(var connection = OpenConnection(forWrite: false)) {
                return connection.Query<DatabaseVersion>(Scripts.DatabaseVersion_GetLatest)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="record"></param>
        public void DatabaseVersion_Save(DatabaseVersion record)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                connection.Execute(Scripts.DatabaseVersion_Save, record);
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public CountrySnapshot CountrySnapshot_GetOrCreate(byte[] fingerprint, DateTime createdUtc, string countryName)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<CountrySnapshot>(Scripts.CountrySnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    countryName,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="enumValue"></param>
        /// <param name="enginePlacementName"></param>
        /// <returns></returns>
        public EnginePlacementSnapshot EnginePlacementSnapshot_GetOrCreate(byte[] fingerprint, DateTime createdUtc, int enumValue, string enginePlacementName)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<EnginePlacementSnapshot>(Scripts.EnginePlacementSnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    enumValue,
                    enginePlacementName,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="enumValue"></param>
        /// <param name="engineTypeName"></param>
        /// <returns></returns>
        public EngineTypeSnapshot EngineTypeSnapshot_GetOrCreate(byte[] fingerprint, DateTime createdUtc, int enumValue, string engineTypeName)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<EngineTypeSnapshot>(Scripts.EngineTypeSnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    enumValue,
                    engineTypeName,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="manufacturerName"></param>
        /// <returns></returns>
        public ManufacturerSnapshot ManufacturerSnapshot_GetOrCreate(byte[] fingerprint, DateTime createdUtc, string manufacturerName)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<ManufacturerSnapshot>(Scripts.ManufacturerSnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    manufacturerName,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="icao"></param>
        /// <param name="modelName"></param>
        /// <param name="numberOfEngines"></param>
        /// <param name="manufacturerSnapshotID"></param>
        /// <param name="wakeTurbulenceCategorySnapshotID"></param>
        /// <param name="engineTypeSnapshotID"></param>
        /// <param name="enginePlacementSnapshotID"></param>
        /// <param name="speciesSnapshotID"></param>
        /// <returns></returns>
        public ModelSnapshot ModelSnapshot_GetOrCreate(
            byte[] fingerprint,
            DateTime createdUtc,
            string icao,
            string modelName,
            string numberOfEngines,
            long? manufacturerSnapshotID,
            long? wakeTurbulenceCategorySnapshotID,
            long? engineTypeSnapshotID,
            long? enginePlacementSnapshotID,
            long? speciesSnapshotID
        )
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<ModelSnapshot>(Scripts.ModelSnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    icao,
                    manufacturerSnapshotID,
                    modelName,
                    wakeTurbulenceCategorySnapshotID,
                    engineTypeSnapshotID,
                    enginePlacementSnapshotID,
                    numberOfEngines,
                    speciesSnapshotID,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="icao"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        public OperatorSnapshot OperatorSnapshot_GetOrCreate(byte[] fingerprint, DateTime createdUtc, string icao, string operatorName)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<OperatorSnapshot>(Scripts.OperatorSnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    icao,
                    operatorName,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        public void Schema_Update()
        {
            using(var connection = OpenConnection(forWrite: true)) {
                connection.Execute(Scripts.Schema);
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="receiverID"></param>
        /// <param name="key"></param>
        /// <param name="receiverName"></param>
        /// <returns></returns>
        public ReceiverSnapshot ReceiverSnapshot_GetOrCreate(byte[] fingerprint, DateTime createdUtc, int receiverID, Guid key, string receiverName)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<ReceiverSnapshot>(Scripts.ReceiverSnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    receiverID,
                    key,
                    receiverName,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="enumValue"></param>
        /// <param name="speciesName"></param>
        /// <returns></returns>
        public SpeciesSnapshot SpeciesSnapshot_GetOrCreate(byte[] fingerprint, DateTime createdUtc, int enumValue, string speciesName)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<SpeciesSnapshot>(Scripts.SpeciesSnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    enumValue,
                    speciesName,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="session"></param>
        public void VrsSession_Insert(VrsSession session)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                session.VrsSessionID = connection.Query<long>(Scripts.VrsSession_Insert, new {
                    session.DatabaseVersionID,
                    session.CreatedUtc,
                }).Single();
            }
        }

        /// <summary>
        /// See interface docs.
        /// </summary>
        /// <param name="fingerprint"></param>
        /// <param name="createdUtc"></param>
        /// <param name="enumValue"></param>
        /// <param name="wakeTurbulenceCategoryName"></param>
        /// <returns></returns>
        public WakeTurbulenceCategorySnapshot WakeTurbulenceCategorySnapshot_GetOrCreate(byte[] fingerprint, DateTime createdUtc, int enumValue, string wakeTurbulenceCategoryName)
        {
            using(var connection = OpenConnection(forWrite: true)) {
                return connection.Query<WakeTurbulenceCategorySnapshot>(Scripts.WakeTurbulenceCategorySnapshot_GetOrCreate, new {
                    createdUtc,
                    fingerprint,
                    enumValue,
                    wakeTurbulenceCategoryName,
                })
                .FirstOrDefault();
            }
        }

        /// <summary>
        /// Creates an open connection to the state history database.
        /// </summary>
        /// <param name="forWrite"></param>
        /// <returns></returns>
        protected IDbConnection OpenConnection(bool forWrite)
        {
            if(IsMissing) {
                throw new InvalidOperationException($"The {nameof(IStateHistoryRepository)} cannot be called, the database is missing");
            }
            if(!WritesEnabled && forWrite) {
                throw new InvalidOperationException($"Write operations on {nameof(IStateHistoryRepository)} are invalid, writes have been disabled");
            }

            var result = Factory.Resolve<ISQLiteConnectionProvider>()
                .Create(_ConnectionString);
            result.Open();

            return result;
        }

        private string BuildConnectionString()
        {
            var folder = _DatabaseInstance.NonStandardFolder;
            if(String.IsNullOrEmpty(folder)) {
                folder = Factory.ResolveSingleton<IConfigurationStorage>()
                    .Folder;
            }
            var fullPath = Path.Combine(folder, "StateHistory.sqb");

            if(_DatabaseInstance.WritesEnabled) {
                if(!Directory.Exists(folder)) {
                    Directory.CreateDirectory(folder);
                }
                if(!File.Exists(fullPath)) {
                    File
                        .Create(fullPath)
                        .Dispose();
                }
            }

            IsMissing = !File.Exists(fullPath);

            var result = "";
            if(!IsMissing) {
                var builder = Factory.Resolve<ISQLiteConnectionStringBuilder>()
                        .Initialise();
                builder.DataSource = fullPath;
                builder.DateTimeFormat = SQLiteDateFormats.ISO8601; // <-- not the most efficient but having different date formats in different SQLite files blows the ADO.NET adapter's mind
                builder.FailIfMissing = true;
                builder.JournalMode = SQLiteJournalModeEnum.Default;
                builder.ReadOnly = !_DatabaseInstance.WritesEnabled;

                result = builder.ConnectionString;
            }

            return result;
        }
    }
}
