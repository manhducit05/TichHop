using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tichhop.Models;

namespace Tichhop.Controllers
{
    public class NhanVienController : ApiController
    {
        QLNV1Entities db = new QLNV1Entities();
        public List<NhanVien> GetAllNhanViens()
        {
            return db.NhanViens.ToList();
        }
        public IHttpActionResult Post(NhanVien nv)
        {
            var x = db.NhanViens.FirstOrDefault(z => z.MaNV == nv.MaNV);
            if (x == null)
            {
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return Ok(x);
            }
            else
            {
                return BadRequest();
            }
        }
        public IHttpActionResult Put(NhanVien nv) {
            var x = db.NhanViens.FirstOrDefault(z => z.MaNV == nv.MaNV);
            if (x == null)
            {
                return NotFound();
            }
            else
            {
                x.HoTen = nv.HoTen;
                x.TrinhDoHoc = nv.TrinhDoHoc; 
                x.Luong= nv.Luong;
                db.SaveChanges();
                return Ok(x);
            }
        }
        public IHttpActionResult Delete(string id) {
            var x = db.NhanViens.FirstOrDefault(z => z.MaNV == id);
            if (x == null)
            {
                return NotFound();
            }
            else
            {
                db.NhanViens.Remove(x);
                db.SaveChanges() ;
                return Ok();
            }
        }
        public NhanVien GetSearch(string id) { 
            return db.NhanViens.FirstOrDefault(z => z.MaNV == id);
        }
    }
    
}
