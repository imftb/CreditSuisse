using Carga.Generica.Core.Enum;
using Carga.Generica.Core.Interface.Factory;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Carga.Generica.Infra.Factory
{
    public class DataFactory : IDataFactory
    {
        private readonly IConfiguration _config;

        public DataFactory(IConfiguration configuration)
        {
            _config = configuration;
        }

        private string OracleConnString { get; set; }

        private IDbConnection CreateConnection(ProjetosEnum.CONNECTION con)
        {
            switch (con)
            {
                case ProjetosEnum.CONNECTION.CAMDEP:
                    OracleConnString = _config["CAMDEPConnection"];
                    break;
                case ProjetosEnum.CONNECTION.SPFDPF:
                    OracleConnString = _config["SPFDPFConnection"];
                    break;
                case ProjetosEnum.CONNECTION.COFEN:
                    OracleConnString = _config["COFENConnection"];
                    break;
                case ProjetosEnum.CONNECTION.CFM:
                    OracleConnString = _config["CFMConnection"];
                    break;
                default:
                    break;
            }

            var conn = new OracleConnection(OracleConnString);

            CloseConnection(conn);

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            return conn;
        }

        private static void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
                conn.Close();
        }

        public async Task<int> ExecuteCommand(string sqlString, ProjetosEnum.CONNECTION con)
        {
            using (var conn = CreateConnection(con))
                return conn.Execute(sqlString);
        }

        public async Task<T> GetFirst<T>(string sqlString, ProjetosEnum.CONNECTION con)
        {
            using (var conn = CreateConnection(con))
                return conn.Query<T>(sqlString, commandType: CommandType.Text).First();
        }

        public async Task<IEnumerable<T>> Query<T>(string sqlString, ProjetosEnum.CONNECTION con)
        {
            using (var conn = CreateConnection(con))
                return conn.Query<T>(sqlString);
        }

        public async Task<int> ExecuteCommand(string sqlString, object objectParams, ProjetosEnum.CONNECTION con)
        {
            using (var conn = CreateConnection(con))
                return conn.Execute(sqlString, objectParams);
        }

        public async Task<T> GetFirst<T>(string sqlString, object objectParams, ProjetosEnum.CONNECTION con)
        {
            using (var conn = CreateConnection(con))
                return conn.Query<T>(sqlString, objectParams, commandType: CommandType.Text).First();
        }

        public async Task<IEnumerable<T>> Query<T>(string sqlString, object objectParams, ProjetosEnum.CONNECTION con)
        {
            using (var conn = CreateConnection(con))
                return conn.Query<T>(sqlString, objectParams);
        }

    }
}
