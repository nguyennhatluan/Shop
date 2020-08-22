using AutoMapper;
using BotDetect.Web.Mvc;
using Shop.Common;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Extensions;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedBackService _feedBackService;
        ContactAndFeedBackViewModel model = new ContactAndFeedBackViewModel();

        public ContactController(IContactDetailService contactDetailService,IFeedBackService feedBackService)
        {
            _contactDetailService = contactDetailService;
            _feedBackService = feedBackService;
            
        }
        // GET: Contact
        public ActionResult Index()
        {
            model.ContactDetailViewModel = GetDefaultContactDetail();
            return View(model);
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedBack(FeedBackViewModel feedBackViewModel)
        {
            if (ModelState.IsValid)
            {
                FeedBack feedBack = new FeedBack();
                feedBack.CreatedDate = DateTime.Now;
                feedBack.UpdateFeedBack(feedBackViewModel);
                _feedBackService.Create(feedBack);
                _feedBackService.Save();

                var content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/contact_template.html"));
                content = content.Replace("{{Name}}", feedBackViewModel.Name);
                content = content.Replace("{{Email}}", feedBackViewModel.Email);
                content = content.Replace("{{Message}}", feedBackViewModel.Message);
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                if(MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content)){
                    MailHelper.SendMail(feedBackViewModel.Email, "Thông tin từ website", "Chúng tôi đã tiếp nhận thông tin phản hồi của bạn, xin cảm ơn");
                    ViewBag.Status = true;
                }
                else
                {
                    ViewBag.Status = false;
                }

                
                
            }
            feedBackViewModel.Name = "";
            feedBackViewModel.Email = "";
            feedBackViewModel.Message = "";
            model.ContactDetailViewModel = GetDefaultContactDetail();
            model.FeedBackViewModel = feedBackViewModel;
            return View("Index",model);
        }

        public ContactDetailViewModel GetDefaultContactDetail()
        {
            var contactDetail = _contactDetailService.GetSingle();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ContactDetail, ContactDetailViewModel>(); });
            IMapper imapper = config.CreateMapper();
            var contactDetailViewModel = imapper.Map<ContactDetail, ContactDetailViewModel>(contactDetail);
          //  model.ContactDetailViewModel = contactDetailViewModel;
            return contactDetailViewModel;
        }
    }
}