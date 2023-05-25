using MegaMindSolutionPractical.Classes;
using MegaMindSolutionPractical.Classes.DAL;
using MegaMindSolutionPractical.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MegaMindSolutionPractical.Controllers
{

    public class HomeController : Controller
    {
        #region Declarations
        DataAccessLayer dataAccessLayer = new DataAccessLayer();
        tblUserRegistrationMetadata userRegistration = new tblUserRegistrationMetadata();
        #endregion

        #region Default
        public ActionResult Index()
        {
            try
            {
                userRegistration.IEStateNameDropDownData = GetState();
                userRegistration.DSUserData = dataAccessLayer.GetUserData("SelectAll", null, null, null);
                userRegistration.lstUserRegistrationData = userRegistration.DSUserData.Tables[0].AsEnumerable()
                .Select(dataRow => new tblUserRegistrationMetadata
                {
                    SrNo = dataRow.Field<long>("SrNo"),
                    Id = dataRow.Field<long>("Id"),
                    Name = dataRow.Field<string>("Name"),
                    Phone = dataRow.Field<string>("Phone"),
                    Email = dataRow.Field<string>("Email"),
                    Address = dataRow.Field<string>("Address"),
                    StateName = dataRow.Field<string>("StateName"),
                    CityName = dataRow.Field<string>("CityName")
                }).ToList();
            }
            catch (Exception ex)
            {

            }
            return View(userRegistration);
        }
        #endregion

        #region Add User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertUser(tblUserRegistrationMetadata tblUserRegistrationMetadata)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                if (tblUserRegistrationMetadata.Id == 0)
                {
                    dataAccessLayer.InsertUpdateUserData("Insert", null, tblUserRegistrationMetadata.Name, tblUserRegistrationMetadata.Phone, tblUserRegistrationMetadata.Email, tblUserRegistrationMetadata.strStateID, tblUserRegistrationMetadata.CityID.ToString(), tblUserRegistrationMetadata.Address);
                    tblUserRegistrationMetadata.Message = "Inserted";
                }
                else
                {
                    dataAccessLayer.InsertUpdateUserData("Update", tblUserRegistrationMetadata.Id.ToString(), tblUserRegistrationMetadata.Name, tblUserRegistrationMetadata.Phone, tblUserRegistrationMetadata.Email, tblUserRegistrationMetadata.strStateID, tblUserRegistrationMetadata.CityID.ToString(), tblUserRegistrationMetadata.Address);
                    tblUserRegistrationMetadata.Message = "Updated";
                }

                userRegistration.IEStateNameDropDownData = GetState();
                userRegistration.DSUserData = dataAccessLayer.GetUserData("SelectAll", null, null, null);
                userRegistration.lstUserRegistrationData = userRegistration.DSUserData.Tables[0].AsEnumerable()
                .Select(dataRow => new tblUserRegistrationMetadata
                {
                    SrNo = dataRow.Field<long>("SrNo"),
                    Id = dataRow.Field<long>("Id"),
                    Name = dataRow.Field<string>("Name"),
                    Phone = dataRow.Field<string>("Phone"),
                    Email = dataRow.Field<string>("Email"),
                    Address = dataRow.Field<string>("Address"),
                    StateName = dataRow.Field<string>("StateName"),
                    CityName = dataRow.Field<string>("CityName")
                }).ToList();
                return View("Index", tblUserRegistrationMetadata);
            }
            else
            {
                tblUserRegistrationMetadata.Message = "Error";
            }
            return RedirectToAction("Index", tblUserRegistrationMetadata);
        } 
        #endregion

        #region Get User
        [HttpGet]
        public JsonResult GetUserData(string Id)
        {
            List<Dictionary<string, object>> lstUsers = null;
            try
            {
                userRegistration.IEStateNameDropDownData = GetState();
                userRegistration.DSUserData = dataAccessLayer.GetUserData("SelectAll", Id.ToString(), null, null);
                DataTable dt = userRegistration.DSUserData.Tables[0];
                lstUsers = GetTableRows(dt);

            }
            catch (Exception ex)
            {

            }
            return Json(lstUsers, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delete User
        [HttpPost]
        public JsonResult UserDelete(string Id)
        {
            try
            {
                userRegistration.IEStateNameDropDownData = GetState();
                userRegistration.DSUserData = dataAccessLayer.GetUserData("SelectAll", null, null, null);

                if (Id != null)
                {
                    dataAccessLayer.DeleteUserData("Delete", Id.ToString());
                    
                }
                else
                {
                    userRegistration.Message = "Error";
                }
            }
            catch (Exception ex)
            {
                
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Bind State Dropdown
        public IEnumerable<SelectListItem> GetState()
        {
            IEnumerable<SelectListItem> StateDropDownData = null;
            try
            {
                DataSet DS = dataAccessLayer.GetStateData("SelectAll");

                if (DS != null & DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    List<DropDownHelper> lstStateDropDownData = new List<DropDownHelper>();
                    lstStateDropDownData = (from DataRow dr in DS.Tables[0].Rows
                                            select new DropDownHelper()
                                            {
                                                Selected = true,
                                                Value = Convert.ToString(dr.Field<long>("StateID")),
                                                Text = dr.Field<string>("StateName")
                                            }).ToList();
                    StateDropDownData = ConvertListDataToSelectListItemData(lstStateDropDownData);
                }
            }
            catch (Exception ex)
            {

            }
            return StateDropDownData;
        } 
        #endregion

        #region Bind City By State
        [HttpPost]
        public ActionResult GetCityByState(int StateID)
        {
            IEnumerable<SelectListItem> CityDropDownData = null;
            try
            {
               DataSet DS = dataAccessLayer.GetCityData("SelectAll",null, StateID.ToString());

                if (DS != null & DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    List<DropDownHelper> lstCityNameDropDownData = new List<DropDownHelper>();
                    lstCityNameDropDownData = (from DataRow dr in DS.Tables[0].Rows
                                                 select new DropDownHelper()
                                                 {
                                                     Value = Convert.ToString(dr.Field<long>("CityID")),
                                                     Text = dr.Field<string>("CityName")
                                                 }).ToList();
                    CityDropDownData = ConvertListDataToSelectListItemData(lstCityNameDropDownData);
                }
                else
                {
                    List<DropDownHelper> lstCityNameDropDownData = new List<DropDownHelper>();
                    lstCityNameDropDownData = (from DataRow dr in DS.Tables[0].Rows
                                                 select new DropDownHelper()
                                                 {
                                                     Value = "Select",
                                                     Text = "Select"
                                                 }).ToList();
                    CityDropDownData = ConvertListDataToSelectListItemData(lstCityNameDropDownData);

                }

                return Json(CityDropDownData);
            }
            catch (Exception ex)
            {
               
            }
            return new EmptyResult();
        }
        #endregion

        #region Custom Functions
        public IEnumerable<SelectListItem> ConvertListDataToSelectListItemData(IEnumerable<DropDownHelper> dropDownHelper)
        {
            var selectListItem = new List<SelectListItem>();
            try
            {
                if (dropDownHelper != null)
                {
                    foreach (var item in dropDownHelper)
                    {

                        selectListItem.Add(new SelectListItem
                        {
                            Selected = item.Selected,
                            Text = item.Text,
                            Value = item.Value
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return selectListItem;
        }

        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            List<Dictionary<string, object>>
            lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;

            foreach (DataRow dr in dtData.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in dtData.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        } 
        #endregion
    }
}