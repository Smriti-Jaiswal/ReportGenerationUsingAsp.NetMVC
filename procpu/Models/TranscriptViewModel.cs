using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace procpu.Models
{
    public class TranscriptViewModel
    {
        public System.Guid transid { get; set; }
        [Display(Name = "Choose Name:")]
        [Required(ErrorMessage = "Please choose name.")]
        public string stdfname { get; set; }
        public string stdmname { get; set; }
        public string stdlname { get; set; }
        public Nullable<System.Guid> frmmodtransid { get; set; }
        public int transcriptnumber { get; set; }
        [Display(Name = "Transcript Generation Date:")]
        [Required(ErrorMessage = "Please enter date.")]
        public Nullable<System.DateTime> transgendt { get; set; }
        [Required(ErrorMessage = "Please enter course title.")]
        public List<string> coursetitle { get; set; }
        public List<string> creditattempted { get; set; }
        public List<string> creditearned { get; set; }
        [Required(ErrorMessage = "Please enter final grade.")]
        public List<string> finalgrade { get; set; }
        public string rco { get; set; }
        public Nullable<System.DateTime> rcm { get; set; }
        public string delsts { get; set; }
        public string luo { get; set; }
        public Nullable<System.DateTime> lum { get; set; }

        [Display(Name = "School Year:")]
        [Required(ErrorMessage = "Please enter school year.")]
        public string schoolyear { get; set; }
        [Display(Name = "Grade Level:")]
        [Required(ErrorMessage = "Please enter grade level.")]
        public string gradelvl { get; set; }
        public Nullable<decimal> totalcredits { get; set; }
        public string gpa { get; set; }
        public string cumulativegpa { get; set; }
        public string cumugpa { get; set; }
        public Nullable<decimal> creditsattempted { get; set; }
        public Nullable<decimal> creditsearned { get; set; }
        public string diplomaearned { get; set; }
        [Display(Name = "Graduation Date:")]
        [Required(ErrorMessage = "Please enter graduation date.")]
        public Nullable<System.DateTime> graduationdate { get; set; }
        public List<string> attachmenttype { get; set; }
        [Display(Name = "Other Details:")]
        public string attachmentotherdtls { get; set; }
        [Display(Name = "Total Subjects:")]
        [Required(ErrorMessage = "Please enter total subjects.")]
        public string totalsubjects { get; set; }
        [Display(Name = "Total Marks Attempted:")]
        [Required(ErrorMessage = "Please enter total marks attempted.")]
        public string totalmarksattempted { get; set; }
        [Display(Name = "Total Marks Obtained / Earned:")]
        [Required(ErrorMessage = "Please enter total marks obtained / earned.")]
        public string totalmarksobtained { get; set; }
        [Display(Name = "Total Percentage:")]
        [Required(ErrorMessage = "Please enter total percentage.")]
        public string totalpercentage { get; set; }
        [Required(ErrorMessage = "Please enter assignments.")]
        public List<string> assignments { get; set; }
        [Required(ErrorMessage = "Please enter quizzes.")]
        public List<string> quizzes { get; set; }
        [Required(ErrorMessage = "Please enter marksobtained.")]
        public List<string> marksobtained { get; set; }
        [Required(ErrorMessage = "Please enter totalmarks.")]
        public List<string> totalmarks { get; set; }
    }
    public class DropdownViewModel
    {
        public System.Guid transid { get; set; }
        public string stdfname { get; set; }
        public string stdmname { get; set; }
        public string stdlname { get; set; }
        public System.DateTime dob { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string phno { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        [Display(Name = "Choose Name:")]
        [Required(ErrorMessage = "Please choose name.")]
        public string stdname { get; set; }
        public string citizenship_id { get; set; }
        public string email { get; set; }
    }
    public class TableViewModel
    {
        public System.Guid transid { get; set; }
        public string stdfname { get; set; }
        public string stdmname { get; set; }
        public string stdlname { get; set; }
        public string citizenship_id { get; set; }
        public string schoolyear { get; set; }
        public string frmmodtransid { get; set; }
        public Nullable<System.DateTime> transgendt { get; set; }
    }
    public class DataViewModel
    {
        public System.Guid transid { get; set; }
        public string stdfname { get; set; }
        public string stdmname { get; set; }
        public string stdlname { get; set; }
        public string address { get; set; }
        public string citizenship_id { get; set; }
        public string email { get; set; }
        public string phno { get; set; }
        public DateTime dob { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string country_name { get; set; }
        public string schoolyear { get; set; }
        public string gradelvl { get; set; }
        public string gpa { get; set; }
        public string cumulativegpa { get; set; }
        public string country { get; set; }
        public string totalcredits { get; set; }
        public System.Guid frmmodtransid { get; set; }
        public DateTime transgendt { get; set; }
        public string cumugpa { get; set; }
        public string creditsattempted { get; set; }
        public string creditsearned { get; set; }
        public string diplomaearned { get; set; }
        public DateTime graduationdate { get; set; }
        public string attachmenttype { get; set; }
        public string attachmentotherdtls { get; set; }
        public string totalsubjects { get; set; }
        public string totalmarksattempted { get; set; }
        public string totalmarksobtained { get; set; }
        public string totalpercentage { get; set; }
    }
    public class EditViewModel
    {
        public System.Guid transid { get; set; }

        public System.Guid stdid { get; set; }
        public string stdfname { get; set; }
        public string stdmname { get; set; }
        public string stdlname { get; set; }
        public System.DateTime dob { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string phno { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string stdname { get; set; }
        public string citizenship_id { get; set; }
        public string email { get; set; }
        public string cumugpa { get; set; }
        public Nullable<decimal> creditsattempted { get; set; }
        public Nullable<decimal> creditsearned { get; set; }
        public string diplomaearned { get; set; }
        [Display(Name = "Transcript Generation Date:")]
        [Required(ErrorMessage = "Please enter date.")]
        public Nullable<System.DateTime> transgendt { get; set; }
        [Display(Name = "School Year:")]
        [Required(ErrorMessage = "Please enter school year.")]
        public string schoolyear { get; set; }
        [Display(Name = "Grade Level:")]
        [Required(ErrorMessage = "Please enter grade level.")]
        public string gradelvl { get; set; }
        public List<string> coursetitle { get; set; }
        public List<string> assignments { get; set; }
        public List<string> quizzes { get; set; }
        public List<string> marksobtained { get; set; }
        public List<string> totalmarks { get; set; }
        public List<string> finalgrade { get; set; }
        public string cumulativegpa { get; set; }
        public string gpa { get; set; }
        public Nullable<decimal> totalcredits { get; set; }
        [Display(Name = "Total Subjects:")]
        [Required(ErrorMessage = "Please enter total subjects.")]
        public string totalsubjects { get; set; }
        [Display(Name = "Total Marks Attempted:")]
        [Required(ErrorMessage = "Please enter total marks attempted.")]
        public string totalmarksattempted { get; set; }
        [Display(Name = "Total Marks Obtained / Earned:")]
        [Required(ErrorMessage = "Please enter total marks obtained / earned.")]
        public string totalmarksobtained { get; set; }
        [Display(Name = "Total Percentage:")]
        [Required(ErrorMessage = "Please enter total percentage.")]
        public string totalpercentage { get; set; }
        [Display(Name = "Graduation Date:")]
        [Required(ErrorMessage = "Please enter graduation date.")]
        public Nullable<System.DateTime> graduationdate { get; set; }
        public string country_name { get; set; }
        public List <AcademicViewModel> table { get; set; }
        public List<string> attachmenttype { get; set; }
        public string attachmentname { get; set; }
        public string attachmentotherdtls { get; set; }

    }

    public class AcademicViewModel
    {
        public System.Guid transid { get; set; }
        public Nullable<System.Guid> frmmodtransid { get; set; }
        public string coursetitle { get; set; }
        public Nullable<decimal> creditattempted { get; set; }
        public Nullable<decimal> creditearned { get; set; }
        public string finalgrade { get; set; }
        public string rco { get; set; }
        public Nullable<System.DateTime> rcm { get; set; }
        public string delsts { get; set; }
        public string luo { get; set; }
        public Nullable<System.DateTime> lum { get; set; }
        public Nullable<System.Guid> studenttransid { get; set; }
        public string assignments { get; set; }
        public string quizzes { get; set; }
        public string marksobtained { get; set; }
        public string totalmarks { get; set; }
    }
}