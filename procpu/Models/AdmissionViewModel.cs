using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace procpu.Models
{
    public class AdmissionViewModel
    {
        public System.Guid transid { get; set; }
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "Please enter first name.")]
        public string stdfname { get; set; }
        [Display(Name = "Middle Name:")]

        public string stdmname { get; set; }
        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Please enter last name.")]
        public string stdlname { get; set; }
        [Display(Name = "Date of Birth:")]
        [Required(ErrorMessage = "Please enter date of birth.")]
        public DateTime dob { get; set; }
        [Display(Name = "Citizenship ID:")]
        [Required(ErrorMessage = "Please enter id.")]
        public string citizenship_id { get; set; }
        [Display(Name = "Gender:")]
        [Required(ErrorMessage = "Please enter gender.")]
        public string gender { get; set; }
        [Display(Name = "Home Address:")]
        [Required(ErrorMessage = "Please enter address.")]
        public string address { get; set; }
        [Display(Name = "Country:")]
        [Required(ErrorMessage = "Please enter country.")]
        public string country { get; set; }
        [Display(Name = "Phone Number:")]
        [Required(ErrorMessage = "Please enter phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone number is not valid.")]
        public string phno { get; set; }
        [Display(Name = "Grade level applied for:")]
        [Required(ErrorMessage = "Please enter grade level.")]
        public string grade { get; set; }
        [Display(Name = "Subjects Taken:")]
        [Required(ErrorMessage = "Please enter subjects taken.")]
        public string subjectstaken { get; set; }
        [Display(Name = "Science / Business/ Medical/Engineering or Accounting or computer science pathway:")]
        [Required(ErrorMessage = "Please enter pathway.")]
        public string pathway { get; set; }
        [Display(Name = "Centre Name:")]
        [Required(ErrorMessage = "Please enter centre name.")]
        public string centrename { get; set; }
        [Display(Name = "Centre Address:")]
        [Required(ErrorMessage = "Please enter centre address.")]
        public string centreaddress { get; set; }
        [Display(Name = "Centre Phone Number:")]
        [Required(ErrorMessage = "Please enter centre phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered centre phone number is not valid.")]
        public string centrephno { get; set; }

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "Please enter first name.")]
        public string fname { get; set; }
        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Please enter last name.")]
        public string lname { get; set; }
        [Display(Name = "ID Number:")]
        [Required(ErrorMessage = "Please enter id number.")]
        public string idnumber { get; set; }
        [Display(Name = "Email Address:")]
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please enter a valid email address.")]
        public string email { get; set; }
        [Display(Name = "Relationship to Student:")]
        [Required(ErrorMessage = "Please enter relationship to student.")]
        public string relation { get; set; }
        [Display(Name = "Home Address:")]
        [Required(ErrorMessage = "Please enter home address.")]
        public string homeaddress { get; set; }
        [Display(Name = "Mobile / Home Phone:")]
        [Required(ErrorMessage = "Please enter mobile / home phone.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone number is not valid.")]
        public string mobile { get; set; }
        [Display(Name = "Centre Name:")]
        [Required(ErrorMessage = "Please enter centre name.")]
        public string cname { get; set; }
        [Display(Name = "Centre Address:")]
        [Required(ErrorMessage = "Please enter centre address.")]
        public string caddress { get; set; }
        [Display(Name = "Apt / Suite#:")]
        [Required(ErrorMessage = "Please enter apt / suite#.")]
        public string suite { get; set; }
        [Display(Name = "State:")]
        [Required(ErrorMessage = "Please enter state.")]
        public string state { get; set; }
        [Display(Name = "Postal / Zip:")]
        [Required(ErrorMessage = "Please enter postal / zip.")]
        public string postalcode { get; set; }
        [Display(Name = "City:")]
        [Required(ErrorMessage = "Please enter city.")]
        public string city { get; set; }
        [Display(Name = "Country:")]
        [Required(ErrorMessage = "Please enter country.")]
        public System.Guid cuntry { get; set; }
        [Display(Name = "Centre Phone:")]
        [Required(ErrorMessage = "Please enter centre phone.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered centre phone number is not valid.")]
        public string cphno { get; set; }
        [Display(Name = "How did you hear about us?:")]
        [Required(ErrorMessage = "Please enter how did you hear about us?.")]
        public string howdidyouhearaboutus { get; set; }
        [Display(Name = "Signed by Parent /Guardian/ Teacher:")]
        [Required(ErrorMessage = "Please upload a file.")]
        public HttpPostedFileBase fileupload { get; set; }
      

    }
    public class DucomentsUploadModel
    {

        [Required(ErrorMessage = "Please select document type!")]
        public string doctype { get; set; }
        public string frmmodtransid { get; set; }
        [Required(ErrorMessage = "Please upload document")]
        public HttpPostedFileBase upload { get; set; }
    }
    public class tblstddocuploadviewmodel
    {
        public System.Guid transid { get; set; }
        public string frmmodtransid { get; set; }
        public string docid { get; set; }
        public string uploaddocs { get; set; }
        public string rco { get; set; }
        public Nullable<System.DateTime> rcm { get; set; }
        public string delsts { get; set; }
        public string luo { get; set; }


    }
    public class DocumentViewModel
    {
        public System.Guid frmmodtransid { get; set; }
        public string docid { get; set; }
        public string uploaddocs { get; set; }
    }
     public class ParentViewModel
    {
        public System.Guid transid { get; set; }
        public System.Guid frmmodtransid { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string idnumber { get; set; }
        public string email { get; set; }
        public string relation { get; set; }
        public string homeaddress { get; set; }
        public string mobile { get; set; }
        public string cname { get; set; }
        public string caddress { get; set; }
        public string suite { get; set; }
        public string state { get; set; }
        public string postalcode { get; set; }
        public string city { get; set; }
        public string cuntry { get; set; }
        public string cphno { get; set; }
        public string howdidyouhearaboutus { get; set; }
        public string fileupload { get; set; }
    }

    public class StudentViewModel
    {
        public System.Guid transid { get; set; }
        public string stdfname { get; set; }
        public string stdmname { get; set; }
        public string stdlname { get; set; }
        public System.DateTime dob { get; set; }
        public string citizenship_id { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string phno { get; set; }
        public string grade { get; set; }
        public string subjectstaken { get; set; }
        public string pathway { get; set; }
        public string centrename { get; set; }
        public string centreaddress { get; set; }
        public string centrephno { get; set; }
    }

    public class GeneralViewModel
    {

        [Display(Name = "Subject:")]
        [Required(ErrorMessage = "Please enter subject.")]
        public string cpugeninfo { get; set; }
        [Display(Name = "Description:")]
        [Required(ErrorMessage = "Please enter description.")]
        public string cpugeninfodesc { get; set; }
        [Display(Name = "File Upload:")]
        [Required(ErrorMessage = "Please upload a file.")]
        public HttpPostedFileBase geninfoupld { get; set; }
    }

    public class GenViewModel
    {
        public System.Guid transid { get; set; }
        [Display(Name = "Subject:")]
        [Required(ErrorMessage = "Please enter subject.")]
        public string cpugeninfo { get; set; }
        [Display(Name = "Description:")]
        [Required(ErrorMessage = "Please enter description.")]
        public string cpugeninfodesc { get; set; }
        [Display(Name = "File Upload:")]
        [Required(ErrorMessage = "Please upload a file.")]
        public string geninfoupld { get; set; }
        public string rco { get; set; }
        public Nullable<System.DateTime> rcm { get; set; }
        public string delsts { get; set; }
        public string luo { get; set; }
        public Nullable<System.DateTime> lcm { get; set; }
    }
    public class PartnerViewModel
    {
   
        [Display(Name = "Partner ID:")]
        [Required(ErrorMessage = "Please enter partner id.")]
        public string partnerid { get; set; }
        [Display(Name = "Partner Name:")]
        [Required(ErrorMessage = "Please enter partner name.")]
        public string partnername { get; set; }
        [Display(Name = "Partner Email ID:")]
        [Required(ErrorMessage = "Please enter partner email id.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please enter a valid email address.")]
        public string partneremail { get; set; }
        [Display(Name = "Partner Contact Number:")]
        [Required(ErrorMessage = "Please enter partner contact number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered number is not valid.")]
        public string partnercnumber { get; set; }
        [Display(Name = "Partner Country:")]
        [Required(ErrorMessage = "Please enter country.")]
        public string partnercountry { get; set; }
        [Display(Name = "MoU Upload:")]
        [Required(ErrorMessage = "Please upload partner's MoU.")]
        public HttpPostedFileBase partnermouupld { get; set; }
      
    }

    public class PartViewModel
    {
        public System.Guid transid { get; set; }
        [Display(Name = "Partner ID:")]
        [Required(ErrorMessage = "Please enter partner id.")]
        public string partnerid { get; set; }
        [Display(Name = "Partner Name:")]
        [Required(ErrorMessage = "Please enter partner name.")]
        public string partnername { get; set; }
        [Display(Name = "Partner Email ID:")]
        [Required(ErrorMessage = "Please enter partner email id.")]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please enter a valid email address.")]
        public string partneremail { get; set; }
        [Display(Name = "Partner Contact Number:")]
        [Required(ErrorMessage = "Please enter partner contact number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered number is not valid.")]
        public string partnercnumber { get; set; }
        [Display(Name = "Partner Country:")]
        [Required(ErrorMessage = "Please enter country.")]
        public string partnercountry { get; set; }
        [Display(Name = "MoU Upload:")]
        [Required(ErrorMessage = "Please upload partner's MoU.")]
        public string partnermouupld { get; set; }
        public string rco { get; set; }
        public Nullable<System.DateTime> rcm { get; set; }
        public string delsts { get; set; }
        public string luo { get; set; }
        public Nullable<System.DateTime> lcm { get; set; }
    }
}