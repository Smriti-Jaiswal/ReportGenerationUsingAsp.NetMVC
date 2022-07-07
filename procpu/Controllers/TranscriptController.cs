using Microsoft.Reporting.WebForms;
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
using WebGrease.Css.Extensions;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using procpu.rdlc;

namespace procpu.Controllers

{
    [Authorize]
    public class TranscriptController : Controller
    {
        // GET: Transcript
        db_luccpuEntities db = new db_luccpuEntities();
        public ActionResult generatetranscript()
        {
            var stdtls = (from std in db.tblstdapplicationdtls
                          select new DropdownViewModel
                          {
                              transid = std.transid,
                              stdname = std.stdfname + " " + std.stdmname + " " + std.stdlname + " (" + std.citizenship_id + ")"
                          }).ToList();
            if (stdtls != null)
            {
                ViewBag.ListOfStudent = new SelectList(stdtls, "transid", "stdname");
            }          
            List<tblattachmenttype> attac = db.tblattachmenttypes.ToList();
            List<SelectListItem> list = new List<SelectListItem>();         
            for (int i = 0; i < attac.Count; i++)
            {
                list.Add(new SelectListItem { Text = attac[i].attachmentname, Value = attac[i].transid.ToString() });
            }
            ViewBag.item = new MultiSelectList(list.OrderBy(a => a.Text), "Value", "Text");
            return View();
        }

        public ActionResult studentinformation(Guid value)
        {
            string result = "";
            List<string> msg = new List<string>();
            var exists = db.tblstdapplicationdtls.OrderBy(a => a.rcm).Where(x => x.transid == value).FirstOrDefault();
            if (exists != null)
            {
                var detail = (from x in db.tblstdapplicationdtls
                              join y in db.tblcountrymasts on x.country equals y.transid
                              join z in db.tblstdparentdtls on x.transid equals z.frmmodtransid
                              where x.transid == value
                              select new DropdownViewModel
                              {
                                  stdfname = x.stdfname,
                                  stdmname = x.stdmname,
                                  stdlname = x.stdlname,
                                  phno = x.phno,
                                  address = x.address,
                                  country = y.country_name,
                                  fname = z.fname,
                                  lname = z.lname,
                                  email = z.email,
                                  dob = x.dob,
                              }).ToList();
                ViewBag.detailsby_id = detail;
                result = "sucess";
                msg.Add(result.ToString());
                return View();

            }
            else
            {
                result = "error";
                msg.Add(result.ToString());
                msg.Add("Entered Name is invalid.");
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymousAttribute]
        public JsonResult SaveTranscriptRecord(TranscriptViewModel model)
        {
            string result = "";
            List<string> msg = new List<string>();
            Guid id = Guid.NewGuid();
            //var ids = Guid.Parse(model.stdfname);
            //var exists = db.tblstdtranscripts.OrderBy(a => a.rcm).Where(x => x.frmmodtransid == ids).FirstOrDefault();
            if (ModelState.IsValid)
                {
                //if (exists == null)
                //{
                        tblstdtranscript Tra = new tblstdtranscript();
                        Tra.transid = id;
                        Tra.frmmodtransid = Guid.Parse(model.stdfname);
                        Tra.transgendt = model.transgendt;
                        Tra.rco = "Admin";
                        Tra.rcm = DateTime.Now;
                        Tra.delsts = "0";
                        db.tblstdtranscripts.Add(Tra);

                        tblstdacademicrecord Aca = new tblstdacademicrecord();
                        for (int i = 0; i < model.coursetitle.Count; i++)
                        {
                            if (model.coursetitle[i] != null && model.assignments[i] != null && model.quizzes[i] != null && model.marksobtained[i] != null && model.totalmarks[i] != null && model.finalgrade[i] != null)
                            {
                            db.tblstdacademicrecords.Add(new tblstdacademicrecord()
                            {
                                transid = Guid.NewGuid(),
                                frmmodtransid = id,
                                coursetitle = model.coursetitle[i],
                                //creditattempted = Convert.ToDecimal(model.creditattempted[i]),
                                //creditearned = Convert.ToDecimal(model.creditearned[i]),
                                assignments = model.assignments[i],
                                quizzes = model.quizzes[i],
                                marksobtained = model.marksobtained[i],
                                totalmarks = model.totalmarks[i],
                                    finalgrade = model.finalgrade[i],
                                    rco = "Admin",                       
                                    rcm = DateTime.Now,
                                    delsts = "0",
                                    studenttransid = Guid.Parse(model.stdfname)

                                }
                                );

                            }
                        }
                        tblstdacademicgradedtl Gra = new tblstdacademicgradedtl();
                        Gra.transid = Guid.NewGuid();
                        Gra.frmmodtransid = id;
                        Gra.schoolyear = model.schoolyear;
                        Gra.gradelvl = model.gradelvl;
                        Gra.totalcredits = model.totalcredits;
                        Gra.gpa = model.gpa;
                        Gra.cumulativegpa = model.cumulativegpa;
                        Gra.rco = "Admin";
                        Gra.rcm = DateTime.Now;
                        Gra.delsts = "0";
                        Gra.studenttransid = Guid.Parse(model.stdfname);
                        db.tblstdacademicgradedtls.Add(Gra);

                        tblstdacademicsummary Sum = new tblstdacademicsummary();
                        Sum.transid = Guid.NewGuid();
                        Sum.frmmodtransid = id;
                    Sum.cumugpa = model.cumugpa;
                    Sum.creditsattempted = model.creditsattempted;
                    Sum.creditsearned = model.creditsearned;
                    Sum.diplomaearned = model.diplomaearned;
                    Sum.totalsubjects = model.totalsubjects;
                    Sum.totalmarksattempted = model.totalmarksattempted;
                    Sum.totalmarksobtained = model.totalmarksobtained;
                    Sum.totalpercentage = model.totalpercentage;
                        Sum.graduationdate = model.graduationdate;
                        Sum.rco = "Admin";
                        Sum.rcm = DateTime.Now;
                        Sum.delsts = "0";
                        Sum.studenttransid = Guid.Parse(model.stdfname);
                        db.tblstdacademicsummaries.Add(Sum);

                        for (int i = 0; i < model.attachmenttype.Count; i++)
                        {
                            tblstdattachment Attach = new tblstdattachment();
                            Attach.transid = Guid.NewGuid();
                            Attach.frmmodtransid = id;
                            Attach.attachmenttype = model.attachmenttype[i];
                            if (model.attachmenttype[i].ToUpper() == "7177259E-60CF-4006-83D5-1ACA6F0440C5")
                            {
                                Attach.attachmentotherdtls = model.attachmentotherdtls;
                            }
                            else
                            {
                                Attach.attachmentotherdtls = null;
                            }
                            Attach.rco = "Admin";
                            Attach.rcm = DateTime.Now;
                            Attach.delsts = "0";
                            Attach.studenttransid = Guid.Parse(model.stdfname);
                           db.tblstdattachments.Add(Attach);
                        }

                        db.SaveChanges();
                        result = "true";
                        msg.Add(result.ToString());
                        msg.Add("All the records have been added sucessfully.");
                    //}
                //else
                //{
                //    result = "false";
                //    msg.Add(result.ToString());
                //    msg.Add("This Student is already exists.");
                //}
            }

                else
                {
                    result = "false";
                    msg.Add(result.ToString());
                    msg.Add("Please fill up all the fields correctly.");
                }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateTranscriptRecord(EditViewModel model)
        {
            string result = "";
            List<string> msg = new List<string>();
            var transcriptid = model.transid;
            if (ModelState.IsValid)
            {
                tblstdacademicgradedtl tblstdacademicgradedtls = db.tblstdacademicgradedtls.Where(x => x.frmmodtransid == transcriptid).FirstOrDefault();
                db.tblstdacademicgradedtls.Remove(tblstdacademicgradedtls);
                db.tblstdacademicrecords.RemoveRange(db.tblstdacademicrecords.Where(x => x.frmmodtransid == transcriptid));
                tblstdacademicsummary tblstdacademicsummary = db.tblstdacademicsummaries.Where(x => x.frmmodtransid == transcriptid).FirstOrDefault();
                db.tblstdacademicsummaries.Remove(tblstdacademicsummary);
                db.tblstdattachments.RemoveRange(db.tblstdattachments.Where(x => x.frmmodtransid == transcriptid));

                tblstdtranscript Tra = db.tblstdtranscripts.Where(x => x.transid == model.transid).FirstOrDefault();
                Tra.transid = model.transid;
                Tra.transgendt = model.transgendt;
                Tra.luo = "CPUAdmin";
                Tra.lum = DateTime.Now;
                Tra.delsts = "0";
                db.Entry(Tra).State = EntityState.Modified;

                tblstdacademicrecord Aca = new tblstdacademicrecord();
                for (int i = 0; i < model.coursetitle.Count; i++)
                {
                    if (model.coursetitle[i] != null && model.assignments[i] != null && model.quizzes[i] != null && model.marksobtained[i] != null && model.totalmarks[i] != null && model.finalgrade[i] != null)
                    {
                        db.tblstdacademicrecords.Add(new tblstdacademicrecord()
                        {
                            transid = Guid.NewGuid(),
                            frmmodtransid = model.transid,
                            coursetitle = model.coursetitle[i],
                            //creditattempted = Convert.ToDecimal(model.creditattempted[i]),
                            //creditearned = Convert.ToDecimal(model.creditearned[i]),
                            assignments = model.assignments[i],
                            quizzes = model.quizzes[i],
                            marksobtained = model.marksobtained[i],
                            totalmarks = model.totalmarks[i],
                            finalgrade = model.finalgrade[i],
                            rco = "Admin",
                            rcm = DateTime.Now,
                            luo = "CPUAdmin",
                            lum = DateTime.Now,
                            delsts = "0",
                            studenttransid = model.stdid


                        }
                            );

                    }
                }
                tblstdacademicgradedtl Gra = new tblstdacademicgradedtl();
                Gra.transid = Guid.NewGuid();
                Gra.frmmodtransid = model.transid;
                Gra.schoolyear = model.schoolyear;
                Gra.gradelvl = model.gradelvl;
                Gra.totalcredits = model.totalcredits;
                Gra.gpa = model.gpa;
                Gra.cumulativegpa = model.cumulativegpa;
                Gra.rco = "Admin";
                Gra.rcm = DateTime.Now;
                Gra.luo = "CPUAdmin";
                Gra.lum = DateTime.Now;
                Gra.delsts = "0";
                Gra.studenttransid = model.stdid;
                db.tblstdacademicgradedtls.Add(Gra);

                tblstdacademicsummary Sum = new tblstdacademicsummary();
                Sum.transid = Guid.NewGuid();
                Sum.frmmodtransid = model.transid;
                Sum.cumugpa = model.cumugpa;
                Sum.creditsattempted = model.creditsattempted;
                Sum.creditsearned = model.creditsearned;
                Sum.diplomaearned = model.diplomaearned;
                Sum.totalsubjects = model.totalsubjects;
                Sum.totalmarksattempted = model.totalmarksattempted;
                Sum.totalmarksobtained = model.totalmarksobtained;
                Sum.totalpercentage = model.totalpercentage;
                Sum.graduationdate = model.graduationdate;
                Sum.rco = "Admin";
                Sum.rcm = DateTime.Now;
                Sum.luo = "CPUAdmin";
                Sum.lum = DateTime.Now;
                Sum.delsts = "0";
                Sum.studenttransid = model.stdid;
                db.tblstdacademicsummaries.Add(Sum);

                for (int j = 0; j < model.attachmenttype.Count; j++)
                {
                    tblstdattachment Attach = new tblstdattachment();
                    Attach.transid = Guid.NewGuid();
                    Attach.frmmodtransid = model.transid;
                    Attach.attachmenttype = model.attachmenttype[j];
                    if (model.attachmenttype[j].ToUpper() == "7177259E-60CF-4006-83D5-1ACA6F0440C5")
                    {
                        Attach.attachmentotherdtls = model.attachmentotherdtls;
                    }
                    else
                    {
                        Attach.attachmentotherdtls = null;
                    }
                    Attach.rco = "Admin";
                    Attach.rcm = DateTime.Now;
                    Attach.luo = "CPUAdmin";
                    Attach.lum = DateTime.Now;
                    Attach.delsts = "0";
                    Attach.studenttransid = model.stdid;
                    db.tblstdattachments.Add(Attach);

                    db.SaveChanges();


                    result = "true";
                    msg.Add(result.ToString());
                    msg.Add("All the records have been updated sucessfully.");
                   
                }
                return Json(msg, JsonRequestBehavior.AllowGet);

            }

            else
            {
                    result = "false";
                    msg.Add(result.ToString());
                    msg.Add("Please fill up all the fields correctly.");
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }

           // return Json(msg, JsonRequestBehavior.AllowGet);

        }
        public ActionResult downloadtranscript()
        {
            return View();
        }
        public ActionResult download(Guid id)
        {
            luccpu ds = new luccpu();
            DataTable dtbl = new DataTable();
            DataTable dtbl1 = new DataTable();
            DataTable dtbl2 = new DataTable();
            DataViewModel model = new DataViewModel();
            DataViewModel viewtable = (from x in db.tblstdapplicationdtls
                                       join y in db.tblstdtranscripts on x.transid equals y.frmmodtransid
                                       join z in db.tblcountrymasts on x.country equals z.transid
                                       join a in db.tblstdparentdtls on x.transid equals a.frmmodtransid
                                       join b in db.tblstdacademicgradedtls on y.transid equals b.frmmodtransid
                                       join c in db.tblstdacademicsummaries on y.transid equals c.frmmodtransid
                                       join d in db.tblstdattachments on x.transid equals d.studenttransid
                                       where x.delsts == "0" && y.transid == id
                                       select new DataViewModel
                                       {
                                                       transid = x.transid,
                                                       stdfname = x.stdfname,
                                                       stdmname = x.stdmname,
                                                       stdlname = x.stdlname,
                                                       //citizenship_id = x.citizenship_id,
                                                       transgendt = y.transgendt.Value,
                                                       address = x.address,
                                                       phno = x.phno,
                                                       dob = x.dob,
                                                       country_name = z.country_name,
                                                       email = a.email,
                                                       fname = a.fname,
                                                       lname = a.lname,
                                                       schoolyear = b.schoolyear,
                                                       gradelvl = b.gradelvl,
                                                       totalcredits = b.totalcredits.ToString(),
                                                       gpa = b.gpa,
                                                       cumulativegpa = b.cumulativegpa,
                                                       cumugpa = c.cumugpa,
                                                       creditsattempted = c.creditsattempted.ToString(),
                                                       creditsearned = c.creditsearned.ToString(),
                                                       diplomaearned = c.diplomaearned,
                                                       graduationdate = c.graduationdate.Value,
                                           totalsubjects = c.totalsubjects,
                                           totalmarksattempted = c.totalmarksattempted,
                                           totalmarksobtained = c.totalmarksobtained,
                                           totalpercentage = c.totalpercentage,
                                       }).FirstOrDefault();
            //model.citizenship_id = viewtable.citizenship_id;
            model.stdfname = viewtable.stdfname;
            model.stdmname = viewtable.stdmname;
            model.stdlname = viewtable.stdlname;
            model.address = viewtable.address;
            model.country_name = viewtable.country_name;
            model.phno = viewtable.phno;
            model.email = viewtable.email;
            model.dob = viewtable.dob;
            model.fname = viewtable.fname;
            model.lname = viewtable.lname;
            model.schoolyear = viewtable.schoolyear;
            model.gradelvl = viewtable.gradelvl;
            model.totalcredits = viewtable.totalcredits;
            model.gpa = viewtable.gpa;
            model.cumulativegpa = viewtable.cumulativegpa;
            model.transgendt = viewtable.transgendt;
            model.cumugpa = viewtable.cumugpa;
            model.creditsattempted = viewtable.creditsattempted;
            model.creditsearned = viewtable.creditsearned;
            model.diplomaearned = viewtable.diplomaearned;
            model.graduationdate = viewtable.graduationdate;
            model.totalsubjects = viewtable.totalsubjects;
            model.totalmarksattempted = viewtable.totalmarksattempted;
            model.totalmarksobtained = viewtable.totalmarksobtained;
            model.totalpercentage = viewtable.totalpercentage;

            ReportViewer reportViewer = new ReportViewer();
            List<ReportParameter> param_li;
            param_li = new List<ReportParameter>(19);
            param_li.Add(new ReportParameter("name", model.stdfname + " " + model.stdmname + " " + model.stdlname));
            param_li.Add(new ReportParameter("address", model.address + ", " + model.country_name));
            param_li.Add(new ReportParameter("phonenumber", model.phno));
            param_li.Add(new ReportParameter("email", model.email));
            param_li.Add(new ReportParameter("dateofbirth", model.dob.ToString("dd-MM-yyyy")));
            param_li.Add(new ReportParameter("parent", model.fname + " " + model.lname));
            param_li.Add(new ReportParameter("schoolyear", model.schoolyear));
            param_li.Add(new ReportParameter("gradelevel", model.gradelvl));
            param_li.Add(new ReportParameter("totalcredits", model.totalcredits));
            param_li.Add(new ReportParameter("gpa", model.gpa));
            param_li.Add(new ReportParameter("cumulativegpa", model.cumulativegpa));
            param_li.Add(new ReportParameter("date", model.transgendt.ToString("dd-MM-yyyy")));
            param_li.Add(new ReportParameter("year", model.schoolyear + "."));
            param_li.Add(new ReportParameter("fullname", model.stdfname + " " + model.stdmname + " " + model.stdlname));
            //param_li.Add(new ReportParameter("cumugpa", model.cumugpa));
            //param_li.Add(new ReportParameter("creditsattempted", model.creditsattempted));
            //param_li.Add(new ReportParameter("creditsearned", model.creditsearned));
            //param_li.Add(new ReportParameter("diplomaearned", model.diplomaearned));
            param_li.Add(new ReportParameter("totalsubjects", model.totalsubjects));
            param_li.Add(new ReportParameter("totalmarksattempted", model.totalmarksattempted));
            param_li.Add(new ReportParameter("totalmarksobtained", model.totalmarksobtained));
            param_li.Add(new ReportParameter("totalpercentage", model.totalpercentage));
            param_li.Add(new ReportParameter("graduationdate", model.graduationdate.ToString("dd-MM-yyyy")));
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            var connectionString = ConfigurationManager.ConnectionStrings["lncseccnn"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select acarod.coursetitle, acarod.assignments, acarod.quizzes, acarod.marksobtained, acarod.totalmarks, acarod.finalgrade from [cpu].[tblstdacademicrecord] acarod inner join[master].[tblstdtranscript] trans on acarod.frmmodtransid = trans.transid inner join[cpu].[tblstdapplicationdtls] appli on trans.frmmodtransid = appli.transid where trans.transid = @transid and acarod.delsts = '0' order by acarod.rcm", conx);
            SqlDataAdapter ada = new SqlDataAdapter("select a.attachmentname, b.attachmenttype from [master].[tblattachmenttype] a left outer join[cpu].[tblstdattachment] b on a.transid = b.attachmenttype inner join[master].[tblstdtranscript] c on b.frmmodtransid = c.transid inner join[cpu].[tblstdapplicationdtls] d on c.frmmodtransid = d.transid where c.transid = @transid and b.delsts = '0' order by a.rcm; ", conx);
            SqlDataAdapter ada1 = new SqlDataAdapter("select attachmentotherdtls from [cpu].[tblstdattachment] where frmmodtransid = @transid and attachmenttype = '7177259E-60CF-4006-83D5-1ACA6F0440C5' order by rcm; ", conx);
            adp.SelectCommand.CommandType = CommandType.Text;
            adp.SelectCommand.Parameters.AddWithValue("@transid", id);
            adp.Fill(dtbl);
            ada.SelectCommand.CommandType = CommandType.Text;
            ada.SelectCommand.Parameters.AddWithValue("@transid", id);
            ada.Fill(dtbl1);
            ada1.SelectCommand.CommandType = CommandType.Text;
            ada1.SelectCommand.Parameters.AddWithValue("@transid", id);
            ada1.Fill(dtbl2);
            reportViewer.LocalReport.ReportPath = Server.MapPath("~/rdlc/transcript.rdlc");
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Academics", dtbl));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Attachment", dtbl1));
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Other", dtbl2));
            reportViewer.LocalReport.SetParameters(param_li);
            reportViewer.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType,
                    out encoding, out extension, out streamids, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename= transcript.pdf");
            Response.BinaryWrite(bytes); // create the file    
            Response.Flush();
            return View();

        }
        public ActionResult alltranscript()
        {
            string result = "";
            List<string> msg = new List<string>();
            var exist = db.tblstdapplicationdtls.OrderBy(a => a.rcm).Where(x => x.delsts == "0").FirstOrDefault();

            var viewtable = from x in db.tblstdapplicationdtls
                             join y in db.tblstdtranscripts on x.transid equals y.frmmodtransid
                             join z in db.tblstdacademicgradedtls on y.transid equals z.frmmodtransid
                             where x.delsts == "0" orderby x.rcm
                             select new TableViewModel
                             {
                                 stdfname = x.stdfname,
                                 stdmname = x.stdmname,
                                 stdlname = x.stdlname,
                                 citizenship_id = x.citizenship_id,
                                 transgendt = y.transgendt,
                                 transid = y.transid,
                                 frmmodtransid = y.frmmodtransid.ToString(),
                                 schoolyear = z.schoolyear,
                             };
            ViewBag.tblview = viewtable;
            result = "sucess";
            msg.Add(result.ToString());
            return View();
        }
        public ActionResult edittranscript(Guid id)
        {
           // var id=TempData["id"].ToString();
            ViewData["id"] = id;
            var exists = db.tblstdtranscripts.OrderBy(a => a.rcm).Where(x => x.transid == id).FirstOrDefault();
            EditViewModel editmodel = new EditViewModel();
            if (exists != null)
            {
                var edit = (from x in db.tblstdtranscripts
                            join y in db.tblstdacademicgradedtls on x.transid equals y.frmmodtransid
                            join z in db.tblstdacademicrecords on y.frmmodtransid equals z.frmmodtransid
                            join a in db.tblstdacademicsummaries on y.frmmodtransid equals a.frmmodtransid
                            join b in db.tblstdapplicationdtls on x.frmmodtransid equals b.transid
                            join c in db.tblstdparentdtls on b.transid equals c.frmmodtransid
                            join d in db.tblcountrymasts on b.country equals d.transid
                            where x.transid == id
                            select new EditViewModel
                            {   transid = x.transid,
                                transgendt = x.transgendt,
                                schoolyear = y.schoolyear,
                                gradelvl = y.gradelvl,
                                totalsubjects = a.totalsubjects,
                                totalmarksattempted = a.totalmarksattempted,
                                totalmarksobtained = a.totalmarksobtained,
                                totalpercentage = a.totalpercentage,
                                graduationdate = a.graduationdate,
                                stdfname = b.stdfname,
                                stdmname = b.stdmname,
                                stdlname = b.stdlname,
                                phno = b.phno,
                                stdid = b.transid,
                                address = b.address,
                                dob = b.dob,
                                fname = c.fname,
                                lname = c.lname,
                                email = c.email,
                                country_name = d.country_name,
                            }).FirstOrDefault();
                editmodel = edit;
                ViewBag.aceroc = db.tblstdacademicrecords.Where(m => m.frmmodtransid == id).ToList();
                ViewBag.editby_id = edit;
                List<tblattachmenttype> attac = db.tblattachmenttypes.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                for (int i = 0; i < attac.Count; i++)
                {
                    list.Add(new SelectListItem { Text = attac[i].attachmentname, Value = attac[i].transid.ToString() });
                }
                ViewBag.item = new MultiSelectList(list.OrderBy(a => a.Text), "Value", "Text");
                ViewBag.chk = db.tblstdattachments.Where(m => m.frmmodtransid == id).ToList();
            }
            return View(editmodel);

            }
        public ActionResult editbyid(Guid id)
        {
            TempData["id"] = id;
            string result = "";
            List<string> msg = new List<string>();
            EditViewModel editModel = new EditViewModel();
            var exists = db.tblstdtranscripts.OrderBy(a => a.rcm).Where(x => x.transid == id).FirstOrDefault();

            if (exists != null)
            {
                var edit = (from x in db.tblstdtranscripts
                            join y in db.tblstdacademicgradedtls on x.transid equals y.frmmodtransid
                            // join z in db.tblstdacademicrecords on y.frmmodtransid equals z.frmmodtransid
                            join a in db.tblstdacademicsummaries on y.frmmodtransid equals a.frmmodtransid
                            join b in db.tblstdapplicationdtls on x.frmmodtransid equals b.transid
                            join c in db.tblstdparentdtls on b.transid equals c.frmmodtransid
                            join d in db.tblcountrymasts on b.country equals d.transid
                            // join e in db.tblstdattachments on x.transid equals e.frmmodtransid
                            // join f in db.tblattachmenttypes on e.attachmenttype equals f.attachmentname
                            where x.transid == id
                            select new EditViewModel
                            {
                                transgendt = x.transgendt,
                                schoolyear = y.schoolyear,
                                gradelvl = y.gradelvl,
                                //coursetitle = z.coursetitle,
                                //assignments = z.assignments,
                                //quizzes = z.quizzes,
                                //marksobtained = z.marksobtained,
                                //totalmarks = z.totalmarks,
                                //finalgrade = z.finalgrade,
                                totalsubjects = a.totalsubjects,
                                totalmarksattempted = a.totalmarksattempted,
                                totalmarksobtained = a.totalmarksobtained,
                                totalpercentage = a.totalpercentage,
                                graduationdate = a.graduationdate,
                                stdfname = b.stdfname,
                                stdmname = b.stdmname,
                                stdlname = b.stdlname,
                                stdid = b.transid,
                                phno = b.phno,
                                address = b.address,
                                dob = b.dob,
                                fname = c.fname,
                                lname = c.lname,
                                email = c.email,
                                country_name = d.country_name,
                                //attachmentname = f.attachmentname,
                            }).ToList();
                //editModel.schoolyear = edit.schoolyear;
                //editModel.gradelvl = edit.gradelvl;
                ViewBag.editby_id = edit;
                List<tblattachmenttype> attac = db.tblattachmenttypes.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                for (int i = 0; i < attac.Count; i++)
                {
                    list.Add(new SelectListItem { Text = attac[i].attachmentname, Value = attac[i].transid.ToString() });
                }
                ViewBag.item = new MultiSelectList(list.OrderBy(a => a.Text), "Value", "Text");
                result = "sucess";
                msg.Add(result.ToString());

            }
            return View(editModel);
        }
    }
    }
