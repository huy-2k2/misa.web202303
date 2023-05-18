using Microsoft.AspNetCore.Mvc;
using misa.web202303.Entities;
using misa.web202303.Extentions;
using MySqlConnector;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace misa.web032023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly MySqlConnection _mySqlConnection;

        public AssetsController(IConfiguration configuration)
        {
            _mySqlConnection = new MySqlConnection(configuration["ConnectionString"]);
        }

        /// <summary>
        /// lấy ra 1 tài sản theo mã tài sản
        /// </summary>
        /// <param name="assetCode"></param>
        /// <returns></returns>
        // GET api/<AssetController>/ts00003
        [HttpGet]
        [Route("{assetId}")]
        public Asset Get(Guid assetId)
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procgGetAssetByAssetId";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // truyền tham số kiểu in
            cmd.Parameters.AddWithValue("assetId", assetId);

            // thực hiện query
            var rdr = cmd.ExecuteReader();

            // lưu kết quả vào biến asset
            var asset = new Asset();
            while (rdr.Read())
            {
                // đối tượng asset để lưu 1 dòng dữ liệu
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
            }
            _mySqlConnection.Close();
            rdr.Close();
            return asset;
        }

        // POST api/<AssetController>
        [HttpPost]
        public int Post([FromBody] Asset asset)
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procInsertAsset";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // truyền vào tham số kiểu int
            cmd.Parameters.AddWithValue("assetCode", asset.AssetCode);
            cmd.Parameters.AddWithValue("assetName", asset.AssetName);
            cmd.Parameters.AddWithValue("departmentId", asset.DepartmentId);
            cmd.Parameters.AddWithValue("assetTypeId", asset.AssetTypeId);
            cmd.Parameters.AddWithValue("quantity", asset.Quantity);
            cmd.Parameters.AddWithValue("price", asset.Price);
            cmd.Parameters.AddWithValue("useDuration", asset.UseDuration);
            cmd.Parameters.AddWithValue("trackYear", asset.TrackYear);
            cmd.Parameters.AddWithValue("buyDate", asset.BuyDate);
            cmd.Parameters.AddWithValue("useDate", asset.UseDate);
            cmd.Parameters.AddWithValue("loseRate", asset.LoseRate);
            cmd.Parameters.AddWithValue("loseRateYear", asset.LoseRateYear);

            // thực hiện query
            var result = cmd.ExecuteNonQuery();

            // đóng kết nối
            _mySqlConnection.Close();

            return result;

        }

        // PUT api/<AssetController>/5
        [HttpPut("{assetId}")]
        public int Put(Guid assetId, [FromBody] Asset asset)
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procUpdateAsset";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // truyền vào tham số kiểu int
            cmd.Parameters.AddWithValue("assetId", assetId);
            cmd.Parameters.AddWithValue("assetCode", asset.AssetCode);
            cmd.Parameters.AddWithValue("assetName", asset.AssetName);
            cmd.Parameters.AddWithValue("departmentId", asset.DepartmentId);
            cmd.Parameters.AddWithValue("assetTypeId", asset.AssetTypeId);
            cmd.Parameters.AddWithValue("quantity", asset.Quantity);
            cmd.Parameters.AddWithValue("price", asset.Price);
            cmd.Parameters.AddWithValue("useDuration", asset.UseDuration);
            cmd.Parameters.AddWithValue("trackYear", asset.TrackYear);
            cmd.Parameters.AddWithValue("buyDate", asset.BuyDate);
            cmd.Parameters.AddWithValue("useDate", asset.UseDate);
            cmd.Parameters.AddWithValue("loseRate", asset.LoseRate);
            cmd.Parameters.AddWithValue("loseRateYear", asset.LoseRateYear);

            // thực hiện query
            var result = cmd.ExecuteNonQuery();

            // đóng kết nối
            _mySqlConnection.Close();

            return result;
        }

        // DELETE api/<AssetController>/5
        [HttpDelete("{assetId}")]
        public int Delete(Guid assetId)
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procDeleteAssets";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // truyền vào tham số kiểu int
            cmd.Parameters.AddWithValue("assetId", assetId);

            // thực hiện query
            var result = cmd.ExecuteNonQuery();

            // đóng kết nối
            _mySqlConnection.Close();

            return result;
        }
    }
}
