using Microsoft.AspNetCore.Mvc;
using misa.web202303.Entities;
using misa.web202303.Extentions;
using MySqlConnector;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace misa.web202303.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginateAssetsController : ControllerBase
    {
        private readonly MySqlConnection _mySqlConnection;

        public PaginateAssetsController(IConfiguration configuration)
        {
            _mySqlConnection = new MySqlConnection(configuration["ConnectionString"]);
        }
        // GET: api/<PaginateAssetsController>
        [HttpGet]
        public PaginateAssets Get([FromQuery] int pageSize, [FromQuery] int currentPage)
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procPaginateGetAssets";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // truyền vào tham số kiểu int
            cmd.Parameters.AddWithValue("pageSize", pageSize);
            cmd.Parameters.AddWithValue("currentPage", currentPage);

            // truyền vào các tham số kiểu out
            var outParam = new MySqlParameter("totalAsset", MySqlDbType.VarChar);
            outParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outParam);

            // thực hiện query
            var rdr = cmd.ExecuteReader();

            // lưu dữ liệu vào entity
            var assets = new List<Asset>();
            while (rdr.Read())
            {
                // đối tượng asset để lưu 1 dòng dữ liệu
                var asset = new Asset();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    // lấy ra tên cột
                    string colName = rdr.GetName(i);

                    // lấy ra giá trị cột
                    var value = rdr.GetValue(i);

                    // lấy ra property ứng với tên cột và gán giá trị cho thuộc tính trong asset
                    var property = asset.GetType().GetProperty(colName.SnakeCaseToPascalCase());
                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(asset, value);
                    }
                }
                assets.Add(asset);

            }
            rdr.Close();
            _mySqlConnection.Close();
            var totalAsset = int.Parse(cmd.Parameters["totalAsset"].Value.ToString());

            return new PaginateAssets() { Assets = assets, TotalAsset = totalAsset};

        }

        [HttpGet]
        [Route("department")]
        public PaginateAssets FilterByDepartment([FromQuery] int pageSize, [FromQuery] int currentPage, Guid departmentId)
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procPaginateFilterAssetsByDepartmentId";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // truyền vào tham số kiểu int
            cmd.Parameters.AddWithValue("pageSize", pageSize);
            cmd.Parameters.AddWithValue("currentPage", currentPage);
            cmd.Parameters.AddWithValue("departmentId", departmentId);

            // truyền vào các tham số kiểu out
            var outParam = new MySqlParameter("totalAsset", MySqlDbType.VarChar);
            outParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outParam);

            // thực hiện query
            var rdr = cmd.ExecuteReader();

            // lưu dữ liệu vào entity
            var assets = new List<Asset>();
            while (rdr.Read())
            {
                // đối tượng asset để lưu 1 dòng dữ liệu
                var asset = new Asset();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    // lấy ra tên cột
                    string colName = rdr.GetName(i);

                    // lấy ra giá trị cột
                    var value = rdr.GetValue(i);

                    // lấy ra property ứng với tên cột và gán giá trị cho thuộc tính trong asset
                    var property = asset.GetType().GetProperty(colName.SnakeCaseToPascalCase());
                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(asset, value);
                    }
                }
                assets.Add(asset);

            }
            rdr.Close();
            _mySqlConnection.Close();
            var totalAsset = int.Parse(cmd.Parameters["totalAsset"].Value.ToString());

            return new PaginateAssets() { Assets = assets, TotalAsset = totalAsset };
        }

        [HttpGet]
        [Route("assetType")]
        public PaginateAssets FilterByAssetType([FromQuery] int pageSize, [FromQuery] int currentPage, Guid assetTypeId)
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procPaginateFilterAssetsByAssetTypeId";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // truyền vào tham số kiểu int
            cmd.Parameters.AddWithValue("pageSize", pageSize);
            cmd.Parameters.AddWithValue("currentPage", currentPage);
            cmd.Parameters.AddWithValue("assetTypeId", assetTypeId);

            // truyền vào các tham số kiểu out
            var outParam = new MySqlParameter("totalAsset", MySqlDbType.VarChar);
            outParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outParam);

            // thực hiện query
            var rdr = cmd.ExecuteReader();

            // lưu dữ liệu vào entity
            var assets = new List<Asset>();
            while (rdr.Read())
            {
                // đối tượng asset để lưu 1 dòng dữ liệu
                var asset = new Asset();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    // lấy ra tên cột
                    string colName = rdr.GetName(i);

                    // lấy ra giá trị cột
                    var value = rdr.GetValue(i);

                    // lấy ra property ứng với tên cột và gán giá trị cho thuộc tính trong asset
                    var property = asset.GetType().GetProperty(colName.SnakeCaseToPascalCase());
                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(asset, value);
                    }
                }
                assets.Add(asset);

            }
            rdr.Close();
            _mySqlConnection.Close();
            var totalAsset = int.Parse(cmd.Parameters["totalAsset"].Value.ToString());

            return new PaginateAssets() { Assets = assets, TotalAsset = totalAsset };
        }

        [HttpGet]
        [Route("all")]
        public PaginateAssets FilterByAll([FromQuery] int pageSize, [FromQuery] int currentPage, Guid assetTypeId, Guid departmentId)
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procPaginateFilterAssetsByAll";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // truyền vào tham số kiểu int
            cmd.Parameters.AddWithValue("pageSize", pageSize);
            cmd.Parameters.AddWithValue("currentPage", currentPage);
            cmd.Parameters.AddWithValue("assetTypeId", assetTypeId);
            cmd.Parameters.AddWithValue("departmentId", departmentId);


            // truyền vào các tham số kiểu out
            var outParam = new MySqlParameter("totalAsset", MySqlDbType.VarChar);
            outParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outParam);

            // thực hiện query
            var rdr = cmd.ExecuteReader();

            // lưu dữ liệu vào entity
            var assets = new List<Asset>();
            while (rdr.Read())
            {
                // đối tượng asset để lưu 1 dòng dữ liệu
                var asset = new Asset();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    // lấy ra tên cột
                    string colName = rdr.GetName(i);

                    // lấy ra giá trị cột
                    var value = rdr.GetValue(i);

                    // lấy ra property ứng với tên cột và gán giá trị cho thuộc tính trong asset
                    var property = asset.GetType().GetProperty(colName.SnakeCaseToPascalCase());
                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(asset, value);
                    }
                }
                assets.Add(asset);

            }
            rdr.Close();
            _mySqlConnection.Close();
            var totalAsset = int.Parse(cmd.Parameters["totalAsset"].Value.ToString());

            return new PaginateAssets() { Assets = assets, TotalAsset = totalAsset };
        }

    }
}
