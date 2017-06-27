using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Data.DataObject
{
    public class MenuDAO
    {
        //List<SqlParameter> param = new List<SqlParameter>();
        //param.Add(new SqlParameter("@id", id));
        //new SqlHelper().ExecuteNoneQuery("delete Merchant_Product_Price where id = @id", param, CommandType.Text);

        public DataTable getAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Menu", param);
        }

        public IEnumerable<Menu> getRootMenu(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Menu where ParentID = 0 And Status = 1", param) as IEnumerable<Menu>;
        }

        public IEnumerable<Menu> getSubMenu(int parentID, string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Menu where ParentID = " + parentID + " and Status = 1", param) as IEnumerable<Menu>;
        }
    }
}
