using ShopLinhKien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ShopLinhKien.Controllers
{
    public class TaiKhoanController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();

        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var TenKH = collection["TenKH"];
            var GioiTinh = collection["GioiTinh"];
            var SDT = collection["SDT"];
            var Email = collection["Email"];
            var DiaChi = collection["DiaChi"];
            var UserKH = collection["UserKH"];
            var PassKH = collection["PassKH"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if (!PassKH.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    kh.TenKH = TenKH;
                    kh.GioiTinh = GioiTinh;
                    kh.SDT = SDT;
                    kh.Email = Email;
                    kh.DiaChi = DiaChi;
                    kh.UserKH = UserKH;
                    kh.PassKH = PassKH;
                    data.KhachHangs.InsertOnSubmit(kh);
                    data.SubmitChanges();
                    return RedirectToAction("DangNhap");
                }
            }
            return this.DangKy();
        }

         public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var TenKH = collection["tendangnhap"];
            var PassKH = collection["matkhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TenKH == TenKH && n.PassKH == PassKH);

            if( kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = kh;
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return RedirectToAction("Index", "Home"); 

        }
    }
}