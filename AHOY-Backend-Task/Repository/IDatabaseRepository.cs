using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AHOY_Backend_Task.Repository
{
    public interface IDatabaseRepository
    {
        public SqlConnection GetSqlConnection();
    }
}
