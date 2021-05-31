using Beijersbergen.Library.GSE;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Gse
{
    public class SqlFormulaNameRepository : IFormulaNameRepository
    {
        private readonly string _connectionString;

        public SqlFormulaNameRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public string GetFormulaName(string formulaCode)
        {
            if (string.IsNullOrWhiteSpace(formulaCode))
            {
                throw new Exception("U dient een formule code op te geven");
            }

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = FindByFormulaCodeCommand(formulaCode))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    (string FormulaCode, string FormulaName)? result = null;

                    while (reader.Read())
                    {
                        if (result.HasValue)
                            throw new InvalidOperationException($"Formule code '{formulaCode}' bestaat meerdere keren in de database");

                        result = (
                            FormulaCode: (string)reader["formula_code"],
                            FormulaName: (string)reader["formula_name"]);
                    }

                    return result?.FormulaName;
                }
            }
        }

        // todo: parameters
        private string ConnectionString =>
             _connectionString;

        private SqlCommand FindByFormulaCodeCommand(string formulaCode)
        {
            var query =
                @"SELECT
                    [FormulaCode] as formula_code,
                    [Name] as formula_name
                FROM
                    [ImsFormula]
                WHERE 
                    [FormulaCode] = @formulaCode;";

            var formulaCodeParam = new SqlParameter("@formulaCode", System.Data.SqlDbType.NVarChar);
            formulaCodeParam.Value = formulaCode;

            var command = new SqlCommand(query);
            command.Parameters.Add(formulaCodeParam);

            return command;
        }
    } 

    public class SqlComponentNameRepository : IComponentNameRepository
    {
        private readonly string _connectionString;

        public SqlComponentNameRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connectionString = connectionString;
        }

        public string GetComponentName(string componentCode)
        {
            if (string.IsNullOrWhiteSpace(componentCode))
            {
                throw new Exception("U dient een component code op te geven");
            }

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = FindByComponentCodeCommand(componentCode))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    (string ComponentCode, string ComponentName)? result = null;

                    while (reader.Read())
                    {
                        if (result.HasValue)
                            throw new InvalidOperationException($"Component code '{componentCode}' bestaat meerdere keren in de database");

                        result = (
                            ComponentCode: (string)reader["component_code"], 
                            ComponentName: (string)reader["name"]);
                    }

                    return result?.ComponentName;
                }
            }
        }

        // todo: parameters
        private string ConnectionString =>
             _connectionString;

        private SqlCommand FindByComponentCodeCommand(string componentCode)
        {
            var query =
                @"SELECT
                    [ComponentCode] as component_code,
                    [Name] as name
                FROM
                    [ImsComponent]
                WHERE 
                    [ComponentCode] = @componentCode;";

            var componentCodeParam = new SqlParameter("@componentCode", System.Data.SqlDbType.NVarChar);
            componentCodeParam.Value = componentCode;

            var command = new SqlCommand(query);
            command.Parameters.Add(componentCodeParam);

            return command;
        }
    }
}
