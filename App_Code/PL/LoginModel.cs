using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PL_Login
/// </summary>
public class LoginModel
{
    public int Ind { get; set; }
    public int DateUserCnt { get; set; }
    public int GroupID { get; set; }
    public string GroupDesc { get; set; }
    public int ItemID { get; set; }
    public string ItemDesc { get; set; }
    public int OrderPriority { get; set; }
    public bool IsActive { get; set; }
    public string OldPassword { get; set; }
    public DateTime UserValidity { get; set; }
    public int UserId { get; set; }
    public string UserLoginId { get; set; }
    public string LoginPwd { get; set; }
    public DataTable dt { get; set; }
    public string MacAddress { get; set; }
    public int LockId { get; set; }
    public string NewPassword { get; set; }
    public int UserInd { get; set; }
    public string UserLoginPwd { get; set; }
    public string Remark { get; set; }
    public int UserTypeId { get; set; }
}