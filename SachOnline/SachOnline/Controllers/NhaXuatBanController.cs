using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;

namespace SachOnline.Controllers
{
    public class NhaXuatBanController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();

        private List<NHAXUATBAN> layNhaXuatBan()
        {
            return data.NHAXUATBANs.ToList();
        }


        // GET: NhaXuatBan
        public ActionResult Index()
        {
            var listNXB = layNhaXuatBan();
            return View(listNXB);
        }


        public ActionResult Details()
        {
            int manxb = int.Parse(Request.QueryString["id"]);
            var results = data.NHAXUATBANs.Where(nxb => nxb.MaNXB == manxb).SingleOrDefault();
            return View(results);
        }

        [HttpGet]
        public ActionResult Edit (int id)
        {
            NHAXUATBAN nxb = data.NHAXUATBANs.Where(n => n.MaNXB == id).SingleOrDefault();
            return View(nxb);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NHAXUATBAN model)
        {

            if (ModelState.IsValid)
            {
                var nxb = data.NHAXUATBANs.Where(x => x.MaNXB == model.MaNXB).SingleOrDefault();
                nxb.TenNXB = model.TenNXB;
                nxb.DiaChi = model.DiaChi;
                nxb.DienThoai = model.DienThoai;

                data.SubmitChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }

           
           
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            NHAXUATBAN nxb = data.NHAXUATBANs.Where(n => n.MaNXB == id).SingleOrDefault();
            return View(nxb);
        }

        [HttpPost]
        public ActionResult Delete(NHAXUATBAN model)
        {

            NHAXUATBAN nxb = data.NHAXUATBANs.Single(n => n.MaNXB == 1);
            data.NHAXUATBANs.DeleteOnSubmit(nxb);
            data.SubmitChanges();

            return RedirectToAction("Index");


        }

    }
}