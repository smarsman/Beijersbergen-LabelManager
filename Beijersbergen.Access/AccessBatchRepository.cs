using Beijersbergen.Library.Fluid;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Beijersbergen.Fluid.Access
{
    public class AccessBatchRepository : IBatchRepository
    {
        private readonly string _databasePath;
        private readonly List<string> _mixingSystemsToExclude;

        public AccessBatchRepository(string databasePath, List<string> mixingSystemsToExclude)
        {
            if (string.IsNullOrEmpty(databasePath))
                throw new ArgumentNullException(nameof(databasePath));

            _databasePath = databasePath;
            _mixingSystemsToExclude = mixingSystemsToExclude;
        }

        public IEnumerable<Batch> FindByBatchNumber(string batchNumber)
        {
            var command = FindByBatchNumberCommand(batchNumber);
            var batches = SearchByQuery(command);

            return batches;
        }

        public IEnumerable<Batch> SelectLatest(int numberOfResults)
        {
            var command = GetLatestCommand(numberOfResults);
            var batches = SearchByQuery(command);

            return batches;
        }

        private IEnumerable<Batch> SearchByQuery(OleDbCommand batchCommand)
        {
            if (!File.Exists(_databasePath))
            {
                throw new FileNotFoundException($"Access database at path '{_databasePath}' does not exist");
            }

            try
            {
                var batches = new List<Batch>();

                using (var connection = new OleDbConnection(ConnectionString))
                {
                    using (batchCommand)
                    {
                        batchCommand.Connection = connection;

                        connection.Open();

                        // Get batch
                        using (var batchReader = batchCommand.ExecuteReader())
                        {
                            while (batchReader.Read())
                            {
                                var batch = ReadBatch(batchReader);

                                // Get base colors
                                using (var baseColorCommand = SelectBatchBaseColorsCommand(batch.BatchNumber))
                                {
                                    baseColorCommand.Connection = connection;

                                    using (var baseColorReader = baseColorCommand.ExecuteReader())
                                    {
                                        while (baseColorReader.Read())
                                        {
                                            var batchBaseColor = ReadBatchBaseColor(baseColorReader);
                                            batch.BaseColors.Add(batchBaseColor);
                                        }
                                    }
                                }

                                batches.Add(batch);
                            }
                        }

                        return batches;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to query Access database: {ex.Message}", ex);
            }
        }


        private string ConnectionString
            => $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={_databasePath}";

        private OleDbCommand FindByBatchNumberCommand(string batchNumber)
        {
            var mixingSystemToExcludeArray = string.Join(",", _mixingSystemsToExclude.Select(x => $"'{x}'"));

            var query =
               $@"SELECT 
                    [Batch - Menginkt].[Batchnummer] as batch_nummer,
                    [Batch - Menginkt].[Datum] as batch_datum,
                    [Batch - Menginkt].[Kleur] as batch_code,
                    [Kleur].[Naam] as batch_omschrijving
                  FROM
                    [Batch - Menginkt]
                  INNER JOIN
                    [Kleur]
                  ON
                    [Kleur].[Code] = [Batch - Menginkt].[Kleur]
                  WHERE
                    [Batch - Menginkt].[Mengsysteem] NOT IN ({mixingSystemToExcludeArray}) AND
                    [Batch - Menginkt].[Batchnummer] = @batchnummer;";

            var batchNumberParam = new OleDbParameter("@batchnummer", OleDbType.BSTR);
            batchNumberParam.Value = batchNumber;

            var command = new OleDbCommand(query);
            command.Parameters.Add(batchNumberParam);

            return command;
        }

        private OleDbCommand GetLatestCommand(int numberOfResults)
        {
            var mixingSystemToExcludeArray = string.Join(",", _mixingSystemsToExclude.Select(x => $"'{x}'"));

            var query =
               $@"SELECT
                    TOP 20
                    [Batch - Menginkt].[Batchnummer] as batch_nummer,
                    [Batch - Menginkt].[Datum] as batch_datum,
                    [Batch - Menginkt].[Kleur] as batch_code,
                    [Kleur].[Naam] as batch_omschrijving
                  FROM
                    [Batch - Menginkt]
                  INNER JOIN
                    [Kleur]
                  ON
                    [Kleur].[Code] = [Batch - Menginkt].[Kleur]
                  WHERE
                    [Batch - Menginkt].[Mengsysteem] NOT IN ({mixingSystemToExcludeArray})
                  ORDER BY
                    [Batch - Menginkt].[Datum] DESC,
                    [Batch - Menginkt].[Batchnummer] DESC;";

            //var numberOfResultsParam = new OleDbParameter("@numberOfResults", OleDbType.Integer);
            //numberOfResultsParam.Value = numberOfResults;

            var command = new OleDbCommand(query);
            //command.Parameters.Add(numberOfResultsParam);

            return command;
        }

        private OleDbCommand SelectBatchBaseColorsCommand(string batchNumber)
        {
            var query =
                @"SELECT 
                    [Menginkt_Batches_Basiskleuren].[Batchnummer] as batch_nummer,
                    [Menginkt_Batches_Basiskleuren].[Mengsysteem] as mengsysteem,
                    [Menginkt_Batches_Basiskleuren].[Basiskleur] as basiskleur, 
                    [Basiskleuren].[Omschrijving] as basiskleur_omschrijving, 
                    [Menginkt_Batches_Basiskleuren].[Basiskleur_Batch] as basiskleur_batch
                  FROM 
                    [Menginkt_Batches_Basiskleuren] 
                  INNER JOIN 
                    [Basiskleuren] 
                  ON 
                    [Menginkt_Batches_Basiskleuren].[Basiskleur] = [Basiskleuren].[Basiskleur]
                  WHERE
                    [Menginkt_Batches_Basiskleuren].[Batchnummer] = @batchnummer
                  ORDER BY 
                    [Menginkt_Batches_Basiskleuren].[Basiskleur];";

            var batchNumberParam = new OleDbParameter("@batchnummer", OleDbType.BSTR);
            batchNumberParam.Value = batchNumber;

            var command = new OleDbCommand(query);
            command.Parameters.Add(batchNumberParam);

            return command;
        }

        private Batch ReadBatch(OleDbDataReader reader) =>
            new Batch
            {
                BatchNumber = (string)reader["batch_nummer"],
                Code = (string)reader["batch_code"],
                Date = (DateTime)reader["batch_datum"],
                Description = (string)reader["batch_omschrijving"]
            };

        private BatchBaseColor ReadBatchBaseColor(OleDbDataReader reader) =>
            new BatchBaseColor
            {
                BatchNumber = (string)reader["batch_nummer"],
                MixingSystem = (string)reader["mengsysteem"],
                BaseColorName = (string)reader["basiskleur"],
                BaseColorDescription = (string)reader["basiskleur_omschrijving"],
                BaseColorBatchNumber = reader["basiskleur_batch"].ToString()
            };
    }
}
