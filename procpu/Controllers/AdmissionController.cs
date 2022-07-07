using procpu.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Net;
using procpu;

namespace procpu.Controllers
{
    public class AdmissionController : Controller
    {
        db_luccpuEntities db = new db_luccpuEntities();
        public ActionResult stdadmissiondtls()

        {
            List<tblcountrymast> CountryList = db.tblcountrymasts.ToList();
            ViewBag.ListOfCountry = new SelectList(CountryList, "transid", "country_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymousAttribute]
        public JsonResult SaveDataInDatabase(AdmissionViewModel model)
        {
            string result = "";
            List<string> msg = new List<string>();
            Guid id = Guid.NewGuid();

            var query = db.tblstdapplicationdtls.Where(x => x.citizenship_id == model.citizenship_id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (query == null)
                {
                    string fileup = "../uploadfile/" + Guid.NewGuid() + System.IO.Path.GetExtension(model.fileupload.FileName);
                    tblstdapplicationdtl Stu = new tblstdapplicationdtl();
                    Stu.transid = id;
                    Stu.stdfname = model.stdfname;
                    Stu.stdmname = model.stdmname;
                    Stu.stdlname = model.stdlname;
                    Stu.dob = model.dob;
                    Stu.citizenship_id = model.citizenship_id;
                    Stu.gender = model.gender;
                    Stu.address = model.address;
                    Stu.country = Guid.Parse(model.country);
                    Stu.phno = model.phno;
                    Stu.grade = model.grade;
                    Stu.subjectstaken = model.subjectstaken;
                    Stu.pathway = model.pathway;
                    Stu.centrename = model.centrename;
                    Stu.centreaddress = model.centreaddress;
                    Stu.centrephno = model.centrephno;
                    Stu.rco = "Admin";
                    Stu.rcm = DateTime.Now;
                    Stu.delsts = "0";
                    db.tblstdapplicationdtls.Add(Stu);


                    tblstdparentdtl Pre = new tblstdparentdtl();
                    Pre.transid = Guid.NewGuid();
                    Pre.frmmodtransid = id;
                    Pre.fname = model.fname;
                    Pre.lname = model.lname;
                    Pre.idnumber = model.idnumber;
                    Pre.email = model.email;
                    Pre.relation = model.relation;
                    Pre.homeaddress = model.homeaddress;
                    Pre.mobile = model.mobile;
                    Pre.cname = model.cname;
                    Pre.caddress = model.caddress;
                    Pre.suite = model.suite;
                    Pre.state = model.state;
                    Pre.postalcode = model.postalcode;
                    Pre.city = model.city;
                    Pre.cuntry = model.cuntry;
                    Pre.cphno = model.cphno;
                    Pre.howdidyouhearaboutus = model.howdidyouhearaboutus;
                    Pre.fileupload = fileup;
                    Pre.rco = "Admin";
                    Pre.rcm = DateTime.Now;
                    Pre.delsts = "0";
                    db.tblstdparentdtls.Add(Pre);
                    db.SaveChanges();
                    model.fileupload.SaveAs(Server.MapPath(fileup));
                    result = "true";
                    msg.Add(result.ToString());
                    msg.Add("All details has been added sucessfully.");
                }
                else
                {
                    result = "false";
                    msg.Add(result.ToString());
                    msg.Add("This Citizenship ID already exists.");
                }
            }
            else
            {
                result = "false";
                msg.Add(result.ToString());
                msg.Add("Please fill up all the fields correctly.");
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        public ActionResult StudentDucomentsUpload()
        {
            return View();
        }


        public ActionResult UploadDucoments(string citizenship_id)
        {

            var stddetails = db.tblstdapplicationdtls.OrderBy(a => a.rcm).Where(x => x.citizenship_id == citizenship_id && x.delsts == "0").FirstOrDefault();

            if (stddetails != null)
            {
                var query = db.tblstdapplicationdtls.OrderBy(a => a.rcm).Where(x => x.citizenship_id == citizenship_id && x.delsts == "0");
                ViewBag.studentdetails = query;
                var list = (from x in db.tblstddocuploads
                            join y in db.tbldoctypemasts on x.docid equals y.transid.ToString()
                            join z in db.tblstdapplicationdtls on x.frmmodtransid equals z.transid
                            where z.citizenship_id == citizenship_id
                            select new tblstddocuploadviewmodel
                            {
                                transid = x.transid,

                                frmmodtransid = z.stdfname + " " + z.stdmname + " " + z.stdlname,
                                docid = y.doctype,
                                uploaddocs = x.uploaddocs,
                                rco = x.rco

                            }).ToList();

                ViewBag.docuploaddtls = list;
                List<tbldoctypemast> Ducoments = db.tbldoctypemasts.OrderBy(a => a.rcm).ToList();
                ViewBag.Ducoments = new SelectList(Ducoments, "transid", "doctype");
                return View();
            }
            else
            {
                string result = "Error";
                List<string> msg = new List<string>();
                msg.Add(result);
                msg.Add("Entered ID is invalid.");
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UploadDucomentsbyid(DucomentsUploadModel uploadmodel)
        {
            bool result = false;
            List<string> msg = new List<string>();
            var filepath = "../uploadfile/" + Guid.NewGuid().ToString() + Path.GetExtension(uploadmodel.upload.FileName);
            if (ModelState.IsValid)
            {
                var details = db.tblstddocuploads.OrderBy(a => a.rcm).Where(x => x.frmmodtransid == new Guid(uploadmodel.frmmodtransid) && x.docid == uploadmodel.doctype).FirstOrDefault();
                if (details == null)
                {
                    tblstddocupload docupload = new tblstddocupload();
                    docupload.transid = Guid.NewGuid();
                    docupload.frmmodtransid = new Guid(uploadmodel.frmmodtransid);
                    docupload.docid = uploadmodel.doctype;
                    docupload.uploaddocs = filepath;
                    docupload.rco = "Applicant";
                    docupload.rcm = DateTime.Now;
                    docupload.delsts = "0";
                    db.tblstddocuploads.Add(docupload);
                    db.SaveChanges();
                    result = true;
                    msg.Add(result.ToString());
                    msg.Add("File Uploaded Successfully...!");
                    uploadmodel.upload.SaveAs(Server.MapPath(filepath));
                }
                else
                {
                    result = true;
                    msg.Add(result.ToString());
                    msg.Add("This Document already exist..");
                }


                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteDocument(string id)
        {
            var result = false;
            List<string> msg = new List<string>();
            if (id != "")
            {
                Guid docid = new Guid(id);
                tblstddocupload doc = db.tblstddocuploads.OrderBy(a => a.rcm).Where(x => x.transid == new Guid(id)).SingleOrDefault();
                if (System.IO.File.Exists(doc.uploaddocs))
                {
                    System.IO.File.Delete(doc.uploaddocs);
                }
                db.tblstddocuploads.Remove(doc);
                db.SaveChanges();
                result = true;
                msg.Add(result.ToString());
                msg.Add("Document deleted successfully!!");
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult applicationdtls()
        {
            return View();
        }

        public ActionResult applicationdtlsbyid(string value)
        {
            string result = "";
            List<string> msg = new List<string>();
            var exist = db.tblstdapplicationdtls.OrderBy(a => a.rcm).Where(x => x.citizenship_id == value).FirstOrDefault();
            if (exist != null)
            {
                var details = (from x in db.tblstdapplicationdtls
                               join y in db.tblcountrymasts on x.country equals y.transid
                               where x.citizenship_id == value
                               select new StudentViewModel
                               {
                                   stdfname = x.stdfname,
                                   stdmname = x.stdmname,
                                   stdlname = x.stdlname,
                                   dob = x.dob,
                                   citizenship_id = x.citizenship_id,
                                   gender = x.gender,
                                   address = x.address,
                                   country = y.country_name,
                                   phno = x.phno,
                                   grade = x.grade,
                                   subjectstaken = x.subjectstaken,
                                   pathway = x.pathway,
                                   centrename = x.centrename,
                                   centreaddress = x.centreaddress,
                                   centrephno = x.centrephno
                               }).ToList();
                ViewBag.deateilsby_id = details;
                result = "sucess";
                msg.Add(result.ToString());
                return View();

            }
            else
            {
                result = "error";
                msg.Add(result.ToString());
                msg.Add("Entered ID is invalid.");
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult parentdtls(string value)
        {

            var query = (from x in db.tblstdapplicationdtls
                         join y in db.tblstdparentdtls on x.transid equals y.frmmodtransid
                         join z in db.tblcountrymasts on y.cuntry equals z.transid
                         where x.citizenship_id == value
                         select new ParentViewModel
                         {
                             fname = y.fname,
                             lname = y.lname,
                             idnumber = y.idnumber,
                             email = y.email,
                             relation = y.relation,
                             homeaddress = y.homeaddress,
                             mobile = y.mobile,
                             cname = y.cname,
                             caddress = y.caddress,
                             suite = y.suite,
                             state = y.state,
                             postalcode = y.postalcode,
                             city = y.city,
                             cphno = y.cphno,
                             howdidyouhearaboutus = y.howdidyouhearaboutus,
                             fileupload = y.fileupload,
                             cuntry = z.country_name
                         }).ToList();
            ViewBag.pdetailsby_id = query;

            return View();
        }

        public ActionResult documentslsbyid(string value)
        {

            var list = (from x in db.tblstddocuploads
                        join y in db.tbldoctypemasts on x.docid equals y.transid.ToString()
                        join z in db.tblstdapplicationdtls on x.frmmodtransid equals z.transid
                        where z.citizenship_id == value
                        select new tblstddocuploadviewmodel
                        {
                            transid = x.transid,
                            frmmodtransid = z.stdfname + " " + z.stdmname + " " + z.stdlname,
                            docid = y.doctype,
                            uploaddocs = x.uploaddocs,
                            rco = x.rco

                        }).ToList();

            ViewBag.docdtls = list;
            return View();
        }
        public ActionResult allids()
        {
            var test = db.tblstdapplicationdtls.OrderBy(a => a.rcm).Where(x => x.delsts == "0").ToList();
            ViewBag.data = test;
            return View();

        }

        public ActionResult cpugeneralinformation()

        {
            return View();
        }

        public ActionResult partmoudtls()

        {
            List<tblcountrymast> CountryList = db.tblcountrymasts.ToList();
            ViewBag.ListOfCountry = new SelectList(CountryList, "transid", "country_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymousAttribute]
        public JsonResult SaveData(GeneralViewModel model)
        {
            string result = "";
            List<string> msg = new List<string>();

            if (ModelState.IsValid)
            {

                string fileup = "../uploadfile/" + Guid.NewGuid() + System.IO.Path.GetExtension(model.geninfoupld.FileName);
                tblcpugeninfo Gen = new tblcpugeninfo();
                Gen.transid = Guid.NewGuid();
                Gen.cpugeninfo = model.cpugeninfo;
                Gen.cpugeninfodesc = model.cpugeninfodesc;
                Gen.geninfoupld = fileup;
                Gen.rco = "Admin";
                Gen.rcm = DateTime.Now;
                Gen.delsts = "0";
                db.tblcpugeninfoes.Add(Gen);
                db.SaveChanges();
                model.geninfoupld.SaveAs(Server.MapPath(fileup));
                result = "true";
                msg.Add(result.ToString());
                msg.Add("All cpu general information has been added sucessfully.");
            }
            else
            {
                result = "false";
                msg.Add(result.ToString());
                msg.Add("Please fill up all the fields correctly.");
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymousAttribute]
        public JsonResult SavePartnerData(PartnerViewModel model)
        {
            string result = "";
            List<string> msg = new List<string>();

            if (ModelState.IsValid)
            {

                string fileup = "../uploadfile/" + Guid.NewGuid() + System.IO.Path.GetExtension(model.partnermouupld.FileName);
                tblcpupartmoudtl Par = new tblcpupartmoudtl();
                Par.transid = Guid.NewGuid();
                Par.partnerid = model.partnerid;
                Par.partnername = model.partnername;
                Par.partneremail = model.partneremail;
                Par.partnercnumber = model.partnercnumber;
                Par.partnercountry = Guid.Parse(model.partnercountry);
                Par.partnermouupld = fileup;
                Par.rco = "Admin";
                Par.rcm = DateTime.Now;
                Par.delsts = "0";
                db.tblcpupartmoudtls.Add(Par);
                db.SaveChanges();
                model.partnermouupld.SaveAs(Server.MapPath(fileup));
                result = "true";
                msg.Add(result.ToString());
                msg.Add("All partner information has been added sucessfully.");
            }
            else
            {
                result = "false";
                msg.Add(result.ToString());
                msg.Add("Please fill up all the fields correctly.");
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        public ActionResult generalinformationview()
        {
            string result = "";
            List<string> msg = new List<string>();
            var exist = db.tblcpugeninfoes.OrderBy(a => a.rcm).Where(x => x.delsts == "0").FirstOrDefault();

            var view = (from x in db.tblcpugeninfoes
                        where x.delsts == "0"
                        select new GenViewModel
                        {
                            transid = x.transid,
                            cpugeninfo = x.cpugeninfo,
                            cpugeninfodesc = x.cpugeninfodesc,
                            geninfoupld = x.geninfoupld,
                            rco = x.rco,
                            rcm = x.rcm
                        }).ToList();
            ViewBag.genview = view;
            result = "sucess";
            msg.Add(result.ToString());
            return View();

        }

        public ActionResult partnerinformationview()
        {
            string result = "";
            List<string> msg = new List<string>();
            var exist = db.tblcpupartmoudtls.OrderBy(a => a.rcm).Where(x => x.delsts == "0").FirstOrDefault();

            var pview = (from x in db.tblcpupartmoudtls
                         join y in db.tblcountrymasts on x.partnercountry equals y.transid
                         where x.delsts == "0"
                         select new PartViewModel
                         {
                             transid = x.transid,
                             partnerid = x.partnerid,
                             partnername = x.partnername,
                             partneremail = x.partneremail,
                             partnercnumber = x.partnercnumber,
                             partnercountry = y.country_name,
                             partnermouupld = x.partnermouupld,
                             rco = x.rco,
                             rcm = x.rcm
                         }).ToList();
            ViewBag.partnerview = pview;
            result = "sucess";
            msg.Add(result.ToString());
            return View();

        }

        public JsonResult DeleteRecord(Guid transid)
        {
            string result = "";
            List<string> msg = new List<string>();
            tblcpugeninfo Del = db.tblcpugeninfoes.SingleOrDefault(x => x.transid == transid);

            Del.delsts = "1";
            Del.luo = "CPU Admin";
            Del.lcm = DateTime.Now;
            db.Entry(Del).State = EntityState.Modified;
            db.SaveChanges();
            result = "success";
            msg.Add(result.ToString());
            msg.Add("Data deleted sucessfully.");
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePartner(Guid transid)
        {
            string result = "";
            List<string> msg = new List<string>();
            tblcpupartmoudtl Vw = db.tblcpupartmoudtls.SingleOrDefault(x => x.transid == transid);

            Vw.delsts = "1";
            Vw.luo = "CPU Admin";
            Vw.lcm = DateTime.Now;
            db.Entry(Vw).State = EntityState.Modified;
            db.SaveChanges();
            result = "success";
            msg.Add(result.ToString());
            msg.Add("Data deleted sucessfully.");
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}




