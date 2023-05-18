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
    public class AssetTypesController : ControllerBase
    {
        private readonly MySqlConnection _mySqlConnection;

        public AssetTypesController(IConfiguration configuration)
        {
            _mySqlConnection = new MySqlConnection(configuration["ConnectionString"]);
        }

        // GET: api/<AssetTypesController>
        [HttpGet]
        public IEnumerable<AssetType> Get()
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procgGetAssetTypes";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // thực hiện query
            var rdr = cmd.ExecuteReader();

            // lưu kết quả vào biến assetTypes
            var assetTypes = new List<AssetType>();
            while (rdr.Read())
            {
                // đối tượng assetType để lưu 1 dòng dữ liệu
                var assetType = new AssetType();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    // // lấy ra tên cột
                    string colName = rdr.GetName(i);

                    // lấy ra giá trị cột
                    var value = rdr.GetValue(i);

                    // lấy ra property ứng với tên cột và gán giá trị cho thuộc tính trong asset
                    var property = assetType.GetType().GetProperty(colName.SnakeCaseToPascalCase());
                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(assetType, value);
                    }
                }
                assetTypes.Add(assetType);
            }
            _mySqlConnection.Close();
            rdr.Close();
            return assetTypes;
        }
    }

}


