using AutoMapper;
using Business.Abstract.ApplicationClasses;
using Business.Abstract.CustomerClasses;
using Entities.Concrete.CustomerClasses;
using Entities.Dtos.CustomerDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly IApplicationService applicationService;
        private readonly IMapper mapper;

        public CustomerController(ICustomerService customerService, IApplicationService applicationService, IMapper mapper)
        {
            this.customerService = customerService;
            this.applicationService = applicationService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var customerResult = await customerService.GetListAsync(x => !x.IsDeleted, null, "Application");
            return View(customerResult.Data);
        }

        public async Task<IActionResult> Create()
        {
            await GetDefinitions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            var applicationResult = await applicationService.GetListAsync(x => !x.IsDeleted && x.Id == customerDto.ApplicationId);
            if (applicationResult is null)
                return Redirect("/");

            var mapp = mapper.Map<Customer>(customerDto);
            if (!applicationResult.Success)
            {
                await GetDefinitions();

                customerDto.ErrorMessage = string.Format("Beklenmedik bir hata oldu. Detay: {0}", applicationResult.Message);

                return View(customerDto);
            }

            if (applicationResult.Data.First().IsNviActive)
            {
                var nviResult = await customerService.GetNviControl(mapp);
                if (!nviResult.Success)
                {
                    await GetDefinitions();

                    customerDto.ErrorMessage = string.Format("Tc kimlik numarasına mersis sisteminde bulunamadı.");

                    return View(customerDto);
                }
            }

            var customerResult = await customerService.AddAsync(mapp);
            if (!customerResult.Success)
            {
                await GetDefinitions();

                customerDto.ErrorMessage = string.Format("Müşteri kayıt etme sırasında hata oluştu.");

                return View(customerDto);
            }

            return RedirectToAction(nameof(Index), "Customer");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return Redirect("/");

            var result = await customerService.GetAsync(x => x.Id == id);
            if (!result.Success)
            {
                return View(new CustomerDto() { ErrorMessage = "Kullanıcı bulunamadı." });
            }

            await GetDefinitions();

            return View(mapper.Map<CustomerDto>(result.Data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CustomerDto customerDto)
        {
            customerDto.ModificationDate = DateTime.Now;

            var mapp = mapper.Map<Customer>(customerDto);

            var result = customerService.Update(mapp);
            if (!result.Success)
                return View(new CustomerDto() { ErrorMessage = "Kullanıcı güncellenirken hata oluştu." });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return Redirect("/");

            var result = await customerService.GetAsync(x => x.Id == id);
            if (!result.Success)
            {
                return View(new CustomerDto() { ErrorMessage = "Kullanıcı bulunamadı." });
            }

            await GetDefinitions();

            return View(mapper.Map<CustomerDto>(result.Data));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = customerService.DeleteById(id);
            if (!result.Success)
                return View(new CustomerDto() { ErrorMessage = "Kullanıcı silirken hata oluştu." });

            return RedirectToAction(nameof(Index));
        }

        private async Task GetDefinitions()
        {
            var birthYearList = new List<int>();

            for (int i = DateTime.Now.Year; i >= 1900; i--)
                birthYearList.Add(i);

            var result = await applicationService.GetListAsync(x => !x.IsDeleted);

            ViewData["Applications"] = new SelectList(from x in result?.Data select new { x.Id, x.Name, Definition = x.Name }, "Id", "Definition");
            ViewData["BirthYears"] = new SelectList(from x in birthYearList ?? null select new { Id = x, Definition = x }, "Id", "Definition");
        }
    }
}
