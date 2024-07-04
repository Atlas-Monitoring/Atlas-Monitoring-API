using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Atlas_Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersController : ControllerBase
    {
        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Publics Methods
        #region Create
        [HttpPost]
        public async Task<ActionResult<Device>> AddNewComputer(Device computer)
        {
            //_context.TodoItems.Add(todoItem);
            //await _context.SaveChangesAsync();

            //    return CreatedAtAction("PostTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(AddNewComputer), new { id = computer.Id }, computer);
        }
        #endregion

        #region Read
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Device>>> GetAllComputers()
        {
            List<Device> computers = new()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    DeviceStatus = DeviceStatus.InProduction,
                    DeviceType = DeviceType.Computer,
                    Name = "MYCOMPUTER1",
                    Ip = "192.168.1.10",
                    Domain = "WORKGROUP",
                    MaxRam = 16000,
                    NumberOfLogicalProcessors = 16,
                    OS = "Windows",
                    OSVersion = "Windows 10 20H2",
                    UserName = "test",
                    DateAdd = DateTime.Now.AddDays(-30),
                    DateUpdated = DateTime.Now.AddDays(-16),
                }
            };

            return computers;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetComputerById(Guid id)
        {
            Device computer = new()
            {
                Id = Guid.NewGuid(),
                DeviceStatus = DeviceStatus.InProduction,
                DeviceType = DeviceType.Computer,
                Name = "MYCOMPUTER1",
                Ip = "192.168.1.10",
                Domain = "WORKGROUP",
                MaxRam = 16000,
                NumberOfLogicalProcessors = 16,
                OS = "Windows",
                OSVersion = "Windows 10 20H2",
                UserName = "test",
                DateAdd = DateTime.Now.AddDays(-30),
                DateUpdated = DateTime.Now.AddDays(-16),
            };

            return computer;
        }
        #endregion

        #region Update
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, Device computer)
        {
            if (id != computer.Id)
            {
                return BadRequest();
            }

            //_context.Entry(todoItem).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TodoItemExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            //var todoItem = await _context.TodoItems.FindAsync(id);
            //if (todoItem == null)
            //{
            //    return NotFound();
            //}

            //_context.TodoItems.Remove(todoItem);
            //await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        #endregion
    }
}
