using Beijersbergen.Library.GSE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Gse
{
    public class SqlBucketRepository : IBucketRepository
    {
        private readonly string _connectionString;
        //private readonly IComponentNameRepository _componentNameRepository;
        private readonly IFormulaNameRepository _formulaNameRepository;

        public SqlBucketRepository(string connectionString, IFormulaNameRepository formulaNameRepository)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            if (formulaNameRepository == null)
                throw new ArgumentNullException(nameof(formulaNameRepository));

            _connectionString = connectionString;
            //_componentNameRepository = componentNameRepository;
            _formulaNameRepository = formulaNameRepository;
        }

        public IEnumerable<Bucket> FindByBucketNumber(int bucketNumber)
        {
            //if (string.IsNullOrWhiteSpace(bucketNumber))
            //    throw new Exception("U dient een bucket nummer op te geven");

            var entries = new List<BucketEntry>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = GetBucketCommand(bucketNumber))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {        
                    while (reader.Read())
                    {
                        var entry = ReadBucketEntry(reader);
                        entries.Add(entry);
                    }
                }
            }

            var buckets = entries
                .GroupBy(x => x.BucketIndex)
                .OrderBy(x => x.First().EventDate)
                .Select(x => new Bucket(
                    bucketNumber: x.First().BucketNr,
                    bucketIndex: x.First().BucketIndex,
                    date: x.First().EventDate,
                    formulaCode: x.First().FormulaCode,
                    formulaName: _formulaNameRepository.GetFormulaName(x.First().FormulaCode),
                    orderCode: x.First().OrderCode,
                    baseColors: x.Select(xx => new BucketBaseColor()
                    {
                        ComponentCode = xx.ComponentCode,
                        ComponentName = "", //_componentNameRepository.GetComponentName(xx.ComponentCode),
                        LotCode = xx.LotCode,
                        DispensedWeightKg = xx.DispensedWeight,
                        RequiredWeightKg = xx.RequiredWeight
                    }).ToList())
                )
                .ToList();

            return buckets;
        }

        public IEnumerable<Bucket> SelectLatest(int numberOfBuckets)
        {
            var bucketNumbers = GetLatestBucketNumbers();

            var buckets = bucketNumbers
                .SelectMany(nr => FindByBucketNumber(nr))
                .ToList();

            return buckets;
        }


        private IEnumerable<int> GetLatestBucketNumbers()
        {
            var numbers = new List<int>();

            using(var connection = new SqlConnection(_connectionString))
            using(var command = GetLatestBucketNumbersCommand())
            {
                command.Connection = connection;
                connection.Open();

                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        numbers.Add((int)reader["BucketNr"]);
                    }
                }

                return numbers;
            }
        }


        private SqlCommand GetBucketCommand(int bucketNumber)
        {
            var query =
                @"SELECT
                    e.[PkId], 
	                e.[EventType], 
	                e.[EventDate], 
                    cd.[FormulaCode], 
	                cd.[BucketNr],
	                cd.[BucketIndex],
	                cd.[ComponentCode],
	                cd.[LotCode],
	                cd.[RequiredWeight],
	                cd.[DispensedWeight],
                    cd.[OrderCode]
                FROM
                    [dbo].[ImsExportEventType] e
                INNER JOIN
                    [dbo].[ImsExportComponentDispense] cd
                    ON cd.[PkId] = e.[PkId]
                WHERE
                    e.[EventType] = 100
                    AND cd.[BucketNr] = @bucketnummer
                ORDER BY
                    --e.EventDate desc,
	                cd.[BucketNr] desc,
	                cd.[BucketIndex] asc";

            var bucketNumberParam = new SqlParameter("@bucketnummer", System.Data.SqlDbType.Int);
            bucketNumberParam.Value = bucketNumber;

            var command = new SqlCommand(query);
            command.Parameters.Add(bucketNumberParam);

            return command;
        }

        private SqlCommand GetLatestBucketNumbersCommand()
        {
            var query =
                @"SELECT 
	                DISTINCT 
	                TOP 20
	                cd.[BucketNr]
                FROM 
	                [dbo].[ImsExportEventType] e
                INNER JOIN
	                [dbo].[ImsExportComponentDispense] cd
	                    ON cd.[PkId] = e.[PkId]
                WHERE 
	                e.[EventType] = 100
                ORDER BY 
	                cd.[BucketNr] desc";

            var command = new SqlCommand(query);
            return command;
        }

        private BucketEntry ReadBucketEntry(SqlDataReader reader) =>
            new BucketEntry
            {
                PkId = (Int64)reader["PkId"],
                EventType = (Int16)reader["EventType"],
                EventDate = (DateTime)reader["EventDate"],
                FormulaCode = (string)reader["FormulaCode"],
                BucketNr = (Int32)reader["BucketNr"],
                BucketIndex = (Int16)reader["BucketIndex"],
                ComponentCode = (string)reader["ComponentCode"],
                LotCode = (string)reader["LotCode"],
                RequiredWeight = (decimal)reader["RequiredWeight"],
                DispensedWeight = (decimal)reader["DispensedWeight"],
                OrderCode = (string)reader["OrderCode"],
            };

        class BucketEntry
        {
            public Int64 PkId { get; set; }
            public Int16 EventType { get; set; }
            public DateTime EventDate { get; set; }
            public string FormulaCode { get; set; }
            public Int32 BucketNr { get; set; }
            public Int16 BucketIndex { get; set; }
            public string ComponentCode { get; set; }
            public string LotCode { get; set; }
            public decimal RequiredWeight { get; set; }
            public decimal DispensedWeight { get; set; }
            public string OrderCode { get; set; }
        }
    }
}
