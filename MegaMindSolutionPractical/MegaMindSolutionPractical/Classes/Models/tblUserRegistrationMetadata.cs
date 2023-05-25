using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaMindSolutionPractical.Models
{
    public class tblUserRegistrationMetadata
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<long> StateID { get; set; }
        public Nullable<long> CityID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public virtual tblCity tblCity { get; set; }
        public virtual tblState tblState { get; set; }
        public DataSet DSUserData { get; set; }
        public List<tblUserRegistrationMetadata> lstUserRegistrationData { get; set; }

        public long SrNo { get; set; }

        public string StateName { get; set; }
        public string CityName { get; set; }

        public string Message { get; set; }

        public string strStateID { get; set; }
        public IEnumerable<SelectListItem> IEStateNameDropDownData { get; set; }
        public IEnumerable<SelectListItem> IECityNameDropDownData { get; set; }
    }
}