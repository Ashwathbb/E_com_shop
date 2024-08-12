using Dept.Repository.Repositories;
using Dept.DataAcess.Models;
using Dept.DataAcess.Dto;
using System.Collections.Generic;
  
using System.Threading.Tasks;
using System.Data.Common;


namespace Dept.Services.Services
{
    public class DeptServices : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        private readonly DepartmentDbContext _context;
        public DeptServices(IDepartmentRepository departmentRepository, DepartmentDbContext context)
        {
            _departmentRepository = departmentRepository;
            _context = context; 
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var department = await _departmentRepository.GetAllDepartmentsAsync();
            return department;
        }
        public async Task<DepartmentDto> GetDepartmentByidAsync(int id)
        {
            var department = await _departmentRepository.GetDepartmentByidAsync(id);

            return department;
        }
        public async Task<DepartmentDto> GetDepartmentByGuidAsync(Guid departmentGuid)
        {
            var department = await _departmentRepository.GetDepartmentByGuidAsync(departmentGuid);
            return department;
        }
        public async Task AddDepartmentAsync(DepartmentDto departmentdto)
        {

            /****************************************
              * with the help of dept dto name will be stored in dept_name
              *  like new Guid will be created using Guid
              *  then with help of the adddept_reposi... we add the department and id.
             ***************************************/
            var dept = new DepartmentDto
            {
                DepartmentName = departmentdto.DepartmentName,
                DepartmentGuid = Guid.NewGuid() // Generate a new GUID for the department
            };
            await _departmentRepository.AddDepartmentAsync(dept);

        }

        public async Task<UpdatedeptDto> UpdateDepartmentAsync(UpdatedeptDto departmentDto)
        {
            var department = new UpdatedeptDto
            {
                DepartmentId = departmentDto.DepartmentId,
                DepartmentName = departmentDto.DepartmentName,
                DepartmentGuid = departmentDto.DepartmentGuid
            };
            return await _departmentRepository.UpdateDepartmentAsync(departmentDto);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
        }

        public async Task AddRolesAsync(int departmentId, IEnumerable<RoleDto> roleDtos)
        {
            await _departmentRepository.AddRolesAsync(departmentId, roleDtos);
        }

        public async Task<IEnumerable<AllTablesDto>> Get_Seven_Table_Data()
        {
            //throw new NotImplementedException();
           
                return await _departmentRepository.Get_Seven_Table_Fileds();
           
        }

        public async Task InsertCustomerAsync(CustomerDto customerDto)
        {
            await _departmentRepository.InsertCustomerAsync(customerDto);
        }
        public async Task InsertDepartmentAsync(DepartmentDto departmentDto)
        {
            await _departmentRepository.InsertDepartmentAsync(departmentDto);

        }

        //public async Task InsertComprehensiveDataAsync(CustomerDto customerDto, DepartmentDto departmentDto)
        //{
        //    using (var transaction = await _departmentRepository.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            // Map DepartmentDto to Department entity
        //            var department = new Department
        //            {
        //                DepartmentGuid = departmentDto.DepartmentGuid,
        //                DepartmentName = departmentDto.DepartmentName,
        //                Roles = departmentDto.Roles.Select(r => new Role
        //                {
        //                    RoleGuid = r.RoleGuid,
        //                    RoleName = r.RoleName
        //                    // Map other Role properties as needed
        //                }).ToList()
        //            };

        //            // Insert department
        //            await _departmentRepository.InsertDepartmentAsync(department);

        //            // Map CustomerDto to Customer entity
        //            var customer = new Customer
        //            {
        //                DepartmentGuid = customerDto.DepartmentGuid,
        //                FirstName = customerDto.FirstName,
        //                //Addresses = customerDto.Addresses.Select(a => new Address
        //                //{
        //                //    ZipCode = a.ZipCode
        //                //    // Map other Address properties as needed
        //                //}).ToList(),
        //                //Orders = customerDto.Orders.Select(o => new Order
        //                //{
        //                //    OrderDate = o.OrderDate
        //                //    // Map other Order properties as needed
        //                //}).ToList()
        //            };

        //            // Insert customer
        //            await _departmentRepository.InsertCustomerAsync(customer);

        //            // Commit transaction if all operations succeed
        //            await transaction.CommitAsync();
        //        }
        //        catch (Exception ex)
        //        {
        //            // Rollback transaction if there's an exception
        //            await transaction.RollbackAsync();
        //            throw new Exception("Failed to insert comprehensive data.", ex);
        //        }
        //    }
        //}
     }
}
