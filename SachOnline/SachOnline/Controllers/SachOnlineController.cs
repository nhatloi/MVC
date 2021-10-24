
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        dbSachOnlineDataContext data = new dbSachOnlineDataContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        private List<SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        private IEnumerable<SACH> LaySachBanNhieu(int count)
        {
            var query = (from
                           a in ((from g1 in data.CHITIETDATHANGs.ToList()
                                  group g1 by g1.MaSach into g
                                  select new
                                  {
                                      MaSach = g.Key,
                                      SoLuong = g.Sum(s => s.SoLuong)
                                  }).OrderByDescending(x => x.SoLuong).Take(count)
                           )
                         join b in data.SACHes.ToList() on a.MaSach equals b.MaSach
                         select new SACH
                         {
                             MaSach = a.MaSach,
                             TenSach = b.TenSach,
                             GiaBan = b.GiaBan,
                             MoTa = b.MoTa,
                             AnhBia = b.AnhBia,
                             NgayCapNhat = b.NgayCapNhat,
                             SoLuongBan = b.SoLuongBan,
                             MaCD = b.MaCD,
                             MaNXB = b.MaNXB
                         });
            return query;
        }



        // GET: SachOnline
        public ActionResult Index(int? page)
        {
            int iSize = 20;
            int iPageNum = (page ?? 1);
            var listSachMoi = LaySachMoi(6);
            return View(listSachMoi.ToPagedList(iPageNum, iSize));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }

        public ActionResult SachTheoChuDe(int? page,int id = 1)
        {
            var cd = (from x in data.CHUDEs where x.MaCD == id select x.TenChuDe).First();
            ViewBag.cd = cd;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var listChuDe = (from x in data.SACHes where x.MaCD == id select x);
            return View(listChuDe.ToPagedList(iPageNum,iSize));
        }

        public ActionResult SachTheoNhaXuatBan(int? page,int id = 1)
        {
            var nxb = (from x in data.NHAXUATBANs where x.MaNXB == id select x.TenNXB).First();
            ViewBag.nxb = nxb;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var listNXB = (from x in data.SACHes where x.MaNXB == id select x);
            return View(listNXB.ToPagedList(iPageNum, iSize)); 
        }

        public ActionResult ChiTietSach(int id=1)
        {
            var sach = from s in data.SACHes
                       where s.MaSach == id
                       select s;
            return View(sach);
        }

        public ActionResult NavPartial()
        {

            var nxb = (from x in data.NHAXUATBANs select x).ToArray();
            ViewBag.nxb = nxb;
            var cd = (from x in data.CHUDEs select x);
            ViewBag.cd = cd;
            return PartialView();
        }
        public ActionResult SliderPartial()
        {
            return PartialView();
        }
         public ActionResult NhaXuatBanPartial()
        {
            var listNhaXuatBan = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(listNhaXuatBan);
        }

        public ActionResult SachBanNhieuPartial()
        {
            var listSachBanNhieu = LaySachBanNhieu(6);
            return PartialView(listSachBanNhieu);
        }
        public ActionResult LoginLogoutPartial()
        {
            
            return PartialView("LoginLogoutPartial");
        }
    }
}