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
    public class DepartmentsController : ControllerBase
    {
        private readonly MySqlConnection _mySqlConnection;

        public DepartmentsController(IConfiguration configuration)
        {
            _mySqlConnection = new MySqlConnection(configuration["ConnectionString"]);
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Departmennt> Get()
        {
            // khởi tạo query
            _mySqlConnection.Open();
            string proc = "procgGetDepartments";
            var cmd = new MySqlCommand(proc, _mySqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // thực hiện query
            var rdr = cmd.ExecuteReader();

            // lưu kết quả vào biến departments
            var departments = new List<Departmennt>();
            while (rdr.Read())
            {
                // đối tượng department để lưu 1 dòng dữ liệu
                var department = new Departmennt();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    // // lấy ra tên cột
                    string colName = rdr.GetName(i);

                    // lấy ra giá trị cột
                    var value = rdr.GetValue(i);

                    // lấy ra property ứng với tên cột và gán giá trị cho thuộc tính trong asset
                    var property = department.GetType().GetProperty(colName.SnakeCaseToPascalCase());
                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(department, value);
                    }
                }
                departments.Add(department);
            }
            _mySqlConnection.Close();
            rdr.Close();
            return departments;
        }
    }
}
