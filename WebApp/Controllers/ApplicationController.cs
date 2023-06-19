using AutoMapper;
using Business.Abstract.ApplicationClasses;
using Entities.Concrete.ApplicationClasses;
using Entities.Dtos.ApplicationDtos;
using Entities.Dtos.CustomerDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService applicationService;
        private readonly IMapper mapper;

        public ApplicationController(IApplicationService applicationService, IMapper mapper)
        {
            this.applicationService = applicationService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var applicationResult = await applicationService.GetListAsync(x => !x.IsDeleted);
            return View(applicationResult.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationDto applicationDto)
        {
            var applicationExistsResult = await applicationService.GetAsync(x => !x.IsDeleted && x.Name == applicationDto.Name);
            if (applicationExistsResult.Success)
            {
                applicationDto.ErrorMessage = "Girmiş olduğunuz uygulama zaten mevcuttur.";
                return View(applicationDto);
            }

            var mapp = mapper.Map<Application>(applicationDto);

            var applicationResult = await applicationService.AddAsync(mapp);               
            if (!applicationResult.Success)
            {
                applicationDto.ErrorMessage = string.Format("Uygulama eklenirken bir hata oldu. Detay: {0}", applicationResult.Message);

                return View(applicationDto);
            }

            return RedirectToAction(nameof(Index), "Application");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return Redirect("/");

            var result = await applicationService.GetAsync(x => x.Id == id);
            if (!result.Success)
            {
                return View(new ApplicationDto() { ErrorMessage = "Uygulama bulunamadı." });
            }

            return View(mapper.Map<ApplicationDto>(result.Data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ApplicationDto applicationDto)
        {
            applicationDto.ModificationDate = DateTime.Now;

            var mapp = mapper.Map<Application>(applicationDto);

            var result = applicationService.Update(mapp);
            if (!result.Success)
                return View(new ApplicationDto() { ErrorMessage = "Kullanıcı güncellenirken hata oluştu." });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return Redirect("/");

            var result = await applicationService.GetAsync(x => x.Id == id);
            if (!result.Success)
            {
                return View(new ApplicationDto() { ErrorMessage = "Kullanıcı bulunamadı." });
            }

            return View(mapper.Map<ApplicationDto>(result.Data));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = applicationService.DeleteById(id);
            if (!result.Success)
                return View(new ApplicationDto() { ErrorMessage = "Kullanıcı silirken hata oluştu." });

            return RedirectToAction(nameof(Index));
        }
    }
}
